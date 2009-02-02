#define SUPRESS_HASH_CALCULATION

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using DotaHIT.Jass.Types;
using DotaHIT.Core.Resources;
using DotaHIT.Jass.Native.Types;
using DotaHIT.DatabaseModel.Data;
using DotaHIT.DatabaseModel.DataTypes;
using DotaHIT.DatabaseModel.Abilities;
using DotaHIT;


namespace Deerchao.War3Share.W3gParser
{
    public class Replay
    {
        static readonly string HeaderString = "Warcraft III recorded game\u001A\0";

        private bool isPreview = false;
#if !SUPRESS_HASH_CALCULATION 
        private long hash;
#endif
        private string filename;
        private MapInfo map;
        private int length;
        private int version;
        private int buildNo;
        private GameType type;
        private string name;
        private Player host;
        private GameSettings settings;
        private readonly List<Team> teams = new List<Team>();
        private readonly List<Player> players = new List<Player>();
        private readonly List<ChatInfo> chats = new List<ChatInfo>();
        private readonly List<KillInfo> kills = new List<KillInfo>();
        Dictionary<int, Dictionary<string, int>> dcShopItemsStockRegen = new Dictionary<int, Dictionary<string, int>>();
        internal Dictionary<int, string> dcObjectsCodeIDs = new Dictionary<int, string>();
        internal Dictionary<int, Hero> dcHeroCache = new Dictionary<int, Hero>();

        private readonly long size;
        public long Size
        {
            get { return size; }
        }

        public string FileName
        {
            get { return filename; }
        }

        public Player Host
        {
            get
            {
                return host;
            }
        }
        public MapInfo Map
        {
            get { return map; }
        }
        public string GameName
        {
            get { return name; }
        }
        public GameType GameType
        {
            get { return type; }
        }
        public TimeSpan GameLength
        {
            get { return new TimeSpan(0, 0, 0, 0, length); }
        }
        public int Version
        {
            get { return version; }
        }
        public List<Player> Players
        {
            get
            {
                return players;
            }
        }
        public List<Team> Teams
        {
            get { return teams; }
        }
        public List<ChatInfo> Chats
        {
            get { return chats; }
        }
        public List<KillInfo> Kills
        {
            get { return kills; }
        }

#if !SUPRESS_HASH_CALCULATION 
        public long Hash
        {
            get { return hash; }
        }
#endif

        public bool IsPreview
        {
            get { return isPreview; }
        }

#if !SUPRESS_HASH_CALCULATION 
        private BinaryWriter hashWriter;        
#endif

        public KeyValuePair<int, long> GetMapKey()
        {
            return new KeyValuePair<int, long>(version, Map.Hash);
        }

        private event MapRequiredEventHandler mapRequired;
        private void OnMapRequired()
        {
            if (mapRequired != null) mapRequired(this, EventArgs.Empty);
        }

        public Replay(string fileName)
            : this(File.OpenRead(fileName))
        {
            this.filename = fileName;
        }

        public Replay(string fileName, MapRequiredEventHandler mapRequiredEvent)
            : this(File.OpenRead(fileName), mapRequiredEvent)
        {
            this.filename = fileName;
        } 

        public Replay(string fileName, bool preview)
            : this(File.OpenRead(fileName), preview)
        {
            this.filename = fileName;
        }

        public Replay(Stream stream)
        {
            using (stream)
            {
                try { size = stream.Length; }
                catch (NotSupportedException)
                { }
                Load(stream);
            }
        }

        public Replay(Stream stream, MapRequiredEventHandler mapRequiredEvent)
        {
            using (stream)
            {
                try { size = stream.Length; }
                catch (NotSupportedException)
                { }
                this.mapRequired += mapRequiredEvent;
                Load(stream);
            }
        }

        public Replay(Stream stream, bool preview)
        {
            this.isPreview = preview;

            using (stream)
            {
                try { size = stream.Length; }
                catch (NotSupportedException)
                { }

                if (preview)
                    LoadPreview(stream);
                else 
                    Load(stream);
            }
        }

        private void Load(Stream stream)
        {
            MemoryStream blocksData = LoadHeader(stream);
#if !SUPRESS_HASH_CALCULATION 
            hashWriter = new BinaryWriter(new MemoryStream());
#endif
            using (BinaryReader reader = new BinaryReader(blocksData))
            {
                LoadPlayers(reader);
                this.OnMapRequired(); // load map database event
                LoadActions(reader);
            }
            //strangely, I found a ladder replay with -49 seconds length..
            if (length < 0)
            {
                foreach (Player player in players)
                {
                    if (player.Time > length)
                        length = player.Time;
                }
            }

            // end research sessions for all players
            foreach (Player player in players)
            if(!player.IsObserver)
            {
                player.State.EndResearch();
                player.Hero = SummonHero(player.GetMostUsedHero().Name, player);
                if (!GetInventoryFromCache(player))
                    FillUpPlayerInventory(player);
            }
            Current.player = GetPlayerBySpecialSlot(1).player;
            Current.player.units.Clear();
            foreach (Player player in players)
            if(!player.IsObserver)
            {
                player.Hero.Level = player.GetMostUsedHero().Level;
                foreach (OrderItem skill in player.GetMostUsedHero().Abilities.BuildOrders)
                    foreach (DBABILITY ability in player.Hero.heroAbilities)
                        if (skill.Name == ability.Alias)
                        {
                            ability.level_up();
                            break;
                        }
                player.Hero.Updated = true;
                Current.player.units.Add(player.Id, player.Hero);
            }
            // fix player slots for -sp mode
            this.SpModeFix();

            UnitsForm.ReloadUnits();
#if !SUPRESS_HASH_CALCULATION 
            hashWriter.Write(version);
            hashWriter.Write(map.Hash);
            hashWriter.Flush();

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] hashedData = provider.ComputeHash(((MemoryStream)hashWriter.BaseStream).ToArray());
            hashWriter.Close();
            hash = 0;
            for (int i = 4; i < 12; i++)
            {
                hash = hash << 8;
                hash |= hashedData[i];
            }
#endif
        }

        private void LoadPreview(Stream stream)
        {
            MemoryStream blocksData = LoadHeader(stream);     
            using (BinaryReader reader = new BinaryReader(blocksData, System.Text.Encoding.Default))
            {
                LoadPlayers(reader);             
            }                        
        }

        private void LoadActions(BinaryReader reader)
        {
            int time = 0;
            bool isPaused = false;
            while (reader.BaseStream.Length - reader.BaseStream.Position > 0)
            {
                byte blockId = reader.ReadByte();
                switch (blockId)
                {
                    case 0x1A:
                    case 0x1B:
                    case 0x1C:
                        reader.BaseStream.Seek(reader.BaseStream.Position + 4, SeekOrigin.Begin);
                        break;
                    case 0x22:
                        reader.BaseStream.Seek(reader.BaseStream.Position + 5, SeekOrigin.Begin);
                        break;
                    case 0x23:
                        reader.BaseStream.Seek(reader.BaseStream.Position + 10, SeekOrigin.Begin);
                        break;
                    case 0x2F:
                        reader.BaseStream.Seek(reader.BaseStream.Position + 8, SeekOrigin.Begin);
                        break;
                    //leave game
                    case 0x17:
                        reader.ReadInt32();
                        byte playerId = reader.ReadByte();
                        Player p = GetPlayerById(playerId);
                        p.Time = time;
                        reader.ReadInt64();
                        chats.Add(new ChatInfo(time, p, TalkTo.System, null, "leave")); 
                        break;
                    //chat
                    case 0x20:
                        byte fromId = reader.ReadByte();
                        reader.ReadBytes(2);
                        byte chatType = reader.ReadByte();
                        TalkTo to = TalkTo.All;
                        if (chatType != 0x10)
                        {
                            to = (TalkTo)reader.ReadInt32();
                        }
                        string message = ParserUtility.ReadString(reader);
                        if (chatType != 0x10)
                        {
                            ChatInfo chat = new ChatInfo(time, GetPlayerById(fromId), to, GetPlayerById((byte)(to - 3)), message);
                            chats.Add(chat);
                        }
                        break;
                    //time slot
                    case 0x1E:
                    case 0x1F:
                        short rest = reader.ReadInt16();
                        short increasedTime = reader.ReadInt16();
                        if (!isPaused)
                            time += increasedTime;
                        rest -= 2;
                        LoadTimeSlot(reader, rest, time, ref isPaused);
                        break;
                    case 0:
                        return;
                    default:
                        throw new W3gParserException("Unknown Action code:" + blockId);
                }
            }
        }

        private void LoadTimeSlot(BinaryReader reader, short rest, int time, ref bool isPaused)
        {
            bool wasDeselect = false;

//#pragma warning disable TooWideLocalVariableScope
            short flag;
            uint itemId;
            float x;
            float y;
            int objectId1;
            int objectId2;
            uint itemId1;
            uint itemId2;
            float x2;
            float y2;
            short unitCount;
            byte groupNo;
            byte slotNo;
            int len;
            string gamecache;
            string missonKey;
            string key;
            int value;
            List<int> units;            
//#pragma warning restore TooWideLocalVariableScope

            while (rest > 0)
            {
                byte playerId = reader.ReadByte();
                Player player = GetPlayerById(playerId);
                player.Time = time;
                short playerBlockRest = reader.ReadInt16();
                rest -= 3;
                short prest = playerBlockRest;

                while (prest > 0)
                {
                    #region
                    byte actionId = reader.ReadByte();
                    switch (actionId)
                    {
                        //pause game
                        case 0x01:
                            isPaused = true;                            
                            chats.Add(new ChatInfo(time, player, TalkTo.System, null, "pause"));                            
                            prest--;                            
                            break;
                        //resume game
                        case 0x02:
                            isPaused = false;                            
                            chats.Add(new ChatInfo(time, player, TalkTo.System, null, "resume"));
                            prest--;
                            break;
                        //set game speed
                        case 0x03:
                            prest -= 2;
                            break;
                        //icrease, decrease game speed
                        case 0x04:
                        case 0x05:
                            prest--;
                            break;
                        //save game
                        case 0x06:
                            len = 0;
                            while (reader.ReadByte() != 0)
                                len++;
                            chats.Add(new ChatInfo(time, player, TalkTo.System, null, "save"));
                            prest -= (short)(len + 2);
                            break;
                        //game saved
                        case 0x07:
                            reader.ReadInt32();
                            prest -= 5;
                            break;
                        //unit ability without target
                        case 0x10:
                            flag = reader.ReadInt16();
                            itemId = reader.ReadUInt32();
                            //unknownA, unknownB
                            reader.ReadInt64();

                            player.ActionsCount++;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(flag);
                            hashWriter.Write(itemId);
#endif

                            //if (string.Compare(DHJassInt.int2id(itemId),"A00S", true)==0)

                            OrderItem(player, itemId, time, 0,0, -1, -1);
                            prest -= 15;
                            break;
                        //unit ability with target position
                        case 0x11:
                            flag = reader.ReadInt16();
                            itemId = reader.ReadUInt32();
                            //unknownA, unknownB
                            reader.ReadInt64();
                            x = reader.ReadSingle();
                            y = reader.ReadSingle();

                            player.ActionsCount++;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(flag);
                            hashWriter.Write(itemId);
                            hashWriter.Write(x);
                            hashWriter.Write(y);
#endif

                            OrderItem(player, itemId, time, x, y, -1, -1);
                            prest -= 23;
                            break;
                        //unit ability with target position and target object
                        case 0x12:
                            flag = reader.ReadInt16();
                            itemId = reader.ReadUInt32();
                            //unknownA, unknownB
                            reader.ReadInt64();
                            x = reader.ReadSingle();
                            y = reader.ReadSingle();
                            objectId1 = reader.ReadInt32();
                            objectId2 = reader.ReadInt32();

                            player.ActionsCount++;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(flag);
                            hashWriter.Write(itemId);
                            hashWriter.Write(x);
                            hashWriter.Write(y);
                            hashWriter.Write(objectId1);
                            hashWriter.Write(objectId2);
#endif
                            OrderItem(player, itemId, time, x, y, objectId1, objectId2);
                            prest -= 31;
                            break;
                        //unit ability with target position, target object, and target item (give item action)
                        case 0x13:
                            flag = reader.ReadInt16();
                            itemId = reader.ReadUInt32();
                            //unknownA, unknownB
                            reader.ReadInt64();
                            x = reader.ReadSingle();
                            y = reader.ReadSingle();
                            objectId1 = reader.ReadInt32();
                            objectId2 = reader.ReadInt32();
                            itemId1 = reader.ReadUInt32();
                            itemId2 = reader.ReadUInt32();

                            player.ActionsCount++;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(flag);
                            hashWriter.Write(itemId);
                            hashWriter.Write(x);
                            hashWriter.Write(y);
                            hashWriter.Write(objectId1);
                            hashWriter.Write(objectId2);
                            hashWriter.Write(itemId1);
                            hashWriter.Write(itemId2);
#endif
                            prest -= 39;
                            break;
                        //unit ability with two target positions and two item IDs
                        case 0x14:
                            flag = reader.ReadInt16();
                            itemId = reader.ReadUInt32();
                            //unknownA, unknownB
                            reader.ReadInt64();
                            x = reader.ReadSingle();
                            y = reader.ReadSingle();
                            itemId1 = reader.ReadUInt32();
                            reader.ReadBytes(9);
                            x2 = reader.ReadSingle();
                            y2 = reader.ReadSingle();

                            player.ActionsCount++;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(flag);
                            hashWriter.Write(itemId);
                            hashWriter.Write(x);
                            hashWriter.Write(y);
                            hashWriter.Write(itemId1);
                            hashWriter.Write(x2);
                            hashWriter.Write(y2);
#endif
                            prest -= 44;
                            break;
                        //change selection
                        case 0x16:
                            byte selectMode = reader.ReadByte();
                            unitCount = reader.ReadInt16();
                            //object ids
                            //reader.ReadBytes(unitCount * 8);
                            for (int i = 0; i < unitCount; i++)
                            {
                                objectId1 = reader.ReadInt32();
                                objectId2 = reader.ReadInt32();

                                if (selectMode!=2)
                                    player.State.CurrentSelection.Add(objectId1);
                                else
                                    player.State.CurrentSelection.Remove(objectId1);
                            }

                            //if is deselect
                            if (selectMode == 2)
                            {
                                wasDeselect = true;
                                player.ActionsCount++;
                            }
                            else
                            {
                                if (!wasDeselect)
                                    player.ActionsCount++;
                                wasDeselect = false;
                            }
                            player.Units.Multiplier = unitCount;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(selectMode);
                            hashWriter.Write(unitCount);
#endif
                            prest -= (short)(unitCount * 8 + 4);
                            break;
                        //create group
                        case 0x17:
                            groupNo = reader.ReadByte();
                            unitCount = reader.ReadInt16();
                            //unit ids
                            //reader.ReadBytes(unitCount * 8);
                            units = new List<int>(unitCount);
                            for (int i = 0; i < unitCount; i++)
                            {
                                objectId1 = reader.ReadInt32();
                                objectId2 = reader.ReadInt32();

                                units.Add(objectId1);
                            }

                            player.ActionsCount++;
                            player.Groups.SetGroup(groupNo, units);
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(groupNo);
                            hashWriter.Write(unitCount);
#endif
                            prest -= (short)(unitCount * 8 + 4);
                            break;
                        //select group
                        case 0x18:
                            groupNo = reader.ReadByte();
                            //unknown
                            reader.ReadByte();

                            player.State.CurrentSelection = new List<int>(player.Groups[groupNo]);

                            player.ActionsCount++;
                            player.Units.Multiplier = (short)player.Groups[groupNo].Count;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(groupNo);
#endif
                            prest -= 3;
                            break;
                        //select sub group
                        case 0x19:
                            //itemId, objectId1, objectId2
                            itemId = reader.ReadUInt32();
                            objectId1 = reader.ReadInt32();
                            reader.ReadInt32();

                            string codeID = DHJassInt.int2id(itemId);

                            // write this objectID-codeID pair to cache
                            if (!dcObjectsCodeIDs.ContainsKey(objectId1) || dcObjectsCodeIDs[objectId1]!=codeID)
                            {                                
                                dcObjectsCodeIDs[objectId1] = codeID;

                                if (DHLOOKUP.dcHeroesTaverns.ContainsKey(codeID))
                                {
                                    Hero cachedHero;
                                    if (!dcHeroCache.TryGetValue(objectId1, out cachedHero) || cachedHero.Name != codeID)
                                        dcHeroCache[objectId1] = new Hero(codeID);                                
                                }
                            }

                            // update current selection for player
                            //player.State.CurrentSelection = objectId1;

                            //no way to find how many buildings is in a subgroup..
                            player.Units.Multiplier = 1;
                            prest -= 13;
                            break;
                        //pre select sub group
                        case 0x1A:
                            prest--;
                            break;
                        //unknown
                        case 0x1B:
                            //unknown, objectid1, objectid2
                            reader.ReadByte();
                            reader.ReadInt64();
                            prest -= 10;
                            break;
                        //select ground item
                        case 0x1C:
                            //unknown, objectid1, objectid2
                            reader.ReadByte();
                            reader.ReadInt64();

                            player.ActionsCount++;
                            prest -= 10;
                            break;
                        //cancel hero revival
                        case 0x1D:
                            reader.ReadInt64();
                            player.ActionsCount++;
                            prest -= 9;
                            break;
                        //remove unit from order queue
                        case 0x1E:
                            slotNo = reader.ReadByte();
                            itemId = reader.ReadUInt32();

                            player.ActionsCount++;
#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(slotNo);
                            hashWriter.Write(itemId);
#endif
                            CancelItem(player, itemId, time);
                            prest -= 6;
                            break;
                        //unknown
                        case 0x21:
                            reader.ReadInt64();
                            prest -= 9;
                            break;
                        //cheats
                        case 0x20:
                        case 0x22:
                        case 0x23:
                        case 0x24:
                        case 0x25:
                        case 0x26:
                        case 0x29:
                        case 0x2A:
                        case 0x2B:
                        case 0x2C:
                        case 0x2F:
                        case 0x30:
                        case 0x31:
                        case 0x32:
                            prest--;
                            break;
                        //cheats
                        case 0x27:
                        case 0x28:
                        case 0x2D:
                            reader.ReadByte();
                            reader.ReadInt32();
                            prest -= 6;
                            break;
                        //cheats
                        case 0x2E:
                            reader.ReadInt32();
                            prest -= 5;
                            break;
                        //change ally option
                        case 0x50:
                            reader.ReadByte();
                            reader.ReadInt32();
                            prest -= 6;
                            break;
                        //transfer resource
                        case 0x51:
                            slotNo = reader.ReadByte();
                            int gold = reader.ReadInt32();
                            int lumber = reader.ReadInt32();

#if !SUPRESS_HASH_CALCULATION 
                            hashWriter.Write(time);
                            hashWriter.Write(actionId);
                            hashWriter.Write(slotNo);
                            hashWriter.Write(gold);
                            hashWriter.Write(lumber);
#endif
                            prest -= 10;
                            break;
                        //trigger chat
                        case 0x60:
                            //unknownA, unknownB
                            reader.ReadInt64();
                            len = 0;
                            while (reader.ReadByte() != 0)
                                len++;

                            prest -= (short)(10 + len);
                            break;
                        //esc pressed
                        case 0x61:
                            // exit research mode for this player
                            if (player.State.IsResearching)
                                player.State.EndResearch();
                            player.ActionsCount++;
                            prest--;
                            break;
                        //Scenario Trigger
                        case 0x62:
                            //unknownABC
                            reader.ReadInt32();
                            reader.ReadInt64();

                            prest -= 13;
                            break;
                        //begin choose hero skill
                        case 0x66:
                            // end any previous research session
                            player.State.EndResearch();
                            // make sure that there is a hero in player's current selection
                            Hero hero;                            
                            if (!MakeSureHeroExists(player, time, out hero)) 
                                Console.WriteLine("No hero selected for choose skill action!");
                            else
                                player.State.BeginResearch(hero);
                            player.ActionsCount++;
                            prest--;
                            break;
                        //begin choose building
                        case 0x67:
                            player.ActionsCount++;
                            prest--;
                            break;
                        //ping mini map
                        case 0x68:
                            //x, y
                            reader.ReadInt64();
                            //unknown
                            reader.ReadInt32();

                            prest -= 13;
                            break;
                        //continue game
                        case 0x69:
                        case 0x6A:
                            reader.ReadInt64();
                            reader.ReadInt64();
                            prest -= 17;
                            break;
                        // SyncStoredInteger actions
                        case 0x6B:
                            gamecache = ParserUtility.ReadString(reader);
                            missonKey = ParserUtility.ReadString(reader);
                            key = ParserUtility.ReadString(reader);
                            value = reader.ReadInt32();
                            prest -= (short)((gamecache.Length + 1) + (missonKey.Length + 1) + (key.Length + 1) + 4 + 1);

                            switch (missonKey)
                            {
                                //case "0":
                                case "1":
                                case "2":
                                case "3":
                                case "4":
                                case "5":                                    
                                    int slot = int.Parse(missonKey);
                                    Player p = GetPlayerBySlot(slot - 1);
                                    string stringId = DHJassInt.int2id(value);
                                    stringId = "";
                                    if (p!=null) p.gameCacheValues[key] = value;                                    
                                    break;                                
                                case "7":
                                case "8":
                                case "9":
                                case "10":
                                case "11":
                                    slot = int.Parse(missonKey);
                                    p = GetPlayerBySlot(slot - 2);                                    
                                    if (p!= null) p.gameCacheValues[key] = value;                                    
                                    break;
                                case "Global":
                                    if (key == "Winner")                                                                            
                                        this.GetTeamByType((TeamType)value).IsWinner = true;                                    
                                    break;
                                case "Data":
                                    // hero death
                                    if (key.StartsWith("Hero"))
                                    {
                                        int deadId = int.Parse(key.Substring(4));
                                        Player victim = GetPlayerBySpecialSlot(deadId);
                                        Player killer = GetPlayerBySpecialSlot(value);

                                        int gcValue;
                                        victim.gameCacheValues.TryGetValue("deaths", out gcValue);
                                        victim.gameCacheValues["deaths"] = gcValue + 1;

                                        // if killer is a player
                                        if (killer != null)
                                        {
                                            killer.gameCacheValues.TryGetValue("kills", out gcValue);
                                            killer.gameCacheValues["kills"] = gcValue + 1;
                                        }

                                        kills.Add(new KillInfo(time, killer, victim));
                                    }
                                    else
                                        // player disconnected
                                        if (key.StartsWith("CK"))
                                        {
                                            int deniesIndex = key.IndexOf('D');
                                            int neutralsIndex = key.IndexOf('N');

                                            int creepKills = int.Parse(key.Substring(2, deniesIndex - 2));
                                            int creepDenies = int.Parse(key.Substring(deniesIndex + 1, neutralsIndex - deniesIndex - 1));

                                            Player leavingPlayer = GetPlayerBySpecialSlot(value);
                                            if (leavingPlayer != null)
                                            {
                                                leavingPlayer.gameCacheValues["creeps"] = creepKills;
                                                leavingPlayer.gameCacheValues["denies"] = creepDenies;
                                            }
                                        }                                        
                                    break;
                            }
                            break;
                        case 0x70:
                            gamecache = ParserUtility.ReadString(reader);
                            missonKey = ParserUtility.ReadString(reader);
                            key = ParserUtility.ReadString(reader);
                            prest -= (short)((gamecache.Length + 1) + (missonKey.Length + 1) + (key.Length + 1) + 1);
                            break;
                        case 0x75:
                            reader.ReadByte();
                            prest -= 2;
                            break;
                        default:
                            throw new W3gParserException("Unknown ActionID:"+actionId);
                    }
                    #endregion
                }
                rest -= playerBlockRest;
            }
        }

        private void CancelItem(Player player, uint itemId, int time)
        {
            if (itemId < 0x41000000 || itemId > 0x7a000000)
                return;
            string stringId = DHJassInt.int2id(itemId);  //ParserUtility.StringFromUInt(itemId);
            switch (ParserUtility.ItemTypeFromId(stringId))
            {
                case ItemType.None:
                    break;
                case ItemType.Hero:
                    player.Heroes.Cancel(stringId, time);
                    break;
                case ItemType.Building:
                    player.Buildings.Cancel(new OrderItem(stringId, time, true)); ;
                    break;
                case ItemType.Research:
                    player.Researches.Cancel(new OrderItem(stringId, time, true)); ;
                    break;
                case ItemType.Unit:
                    player.Units.Cancel(new OrderItem(stringId, time, true)); ;
                    break;
                case ItemType.Upgrade:
                    player.Upgrades.Cancel(new OrderItem(stringId, time, true)); ;
                    break;
            }
        }

        private void OrderItem(Player player, uint itemId, int time, double x, double y, int objectID1, int objectID2)
        {
            // if this is orderID (not an objectID)
            if ((itemId >> 16) == 0x000D)
            {
                switch ((short)itemId)
                {
                    case 0x0003: // rightclick
                        player.Actions.Add(new PlayerAction(
                            PlayerActionType.RightClick,
                            x,y,
                            time, 
                            objectID1, objectID2));
                        break;

                    case 0x000F: // attack
                        player.Actions.Add(new PlayerAction(
                            PlayerActionType.Attack,
                            x, y,
                            time,
                            objectID1, objectID2));
                        break;
                }

                return;
            }
            
            string stringId = DHJassInt.int2id(itemId);//ParserUtility.StringFromUInt(itemId);
            switch (ParserUtility.ItemTypeFromId(stringId))
            {
                case ItemType.None:
                    if ((itemId >> 16) == 0x00000233)
                        player.Units.Order(new OrderItem("ubsp", time));
                    break;
                case ItemType.Hero:
                    player.Heroes.Order(stringId, time);
                    break;
                case ItemType.HeroAbility:                
                    Hero hero;
                    if (player.State.IsResearching 
                        && (TryFindHeroByCache(player.State.CurrentSelection, out hero) || (hero = player.GetMostUsedHero())!=null)
                        && hero.Train(stringId, time, player.State.MaxAllowedLevelForResearch))// player.Heroes.Train(stringId, time))
                    {                        
                        player.State.CompleteResearch();
                    }
                    break;
                case ItemType.Ability:                    
                    if (player.State.IsResearching
                        && (TryFindHeroByCache(player.State.CurrentSelection, out hero) || (hero = player.GetMostUsedHero()) != null)
                        && hero.Train(ParserUtility.GetBestMatchAbilityForHero(hero, stringId, ItemType.Ability), time, player.State.MaxAllowedLevelForResearch))
                    {
                        player.State.CompleteResearch();
                    }
                    break;
                case ItemType.Building:
                    player.Buildings.Order(new OrderItem(stringId, time));
                    break;
                case ItemType.Item:
                    if (PlayerCanBuyItem(player, stringId, time))
                    {
                        BuyItemForPlayer(player, stringId, time);
                        player.Items.Order(new OrderItem(stringId, time));
                    }

                    if (stringId == "tret")
                        player.Heroes.PossibleRetrained(time);

                    break;
                case ItemType.Unit:
                    player.Units.Order(new OrderItem(stringId, time));
                    break;
                case ItemType.Upgrade:
                    player.Upgrades.Order(new OrderItem(stringId, time)); ;
                    break;
                case ItemType.Research:
                    player.Researches.Order(new OrderItem(stringId, time)); ;
                    break;
            }
        }

        bool PlayerCanBuyItem(Player p, string itemID, int time)
        {
            // only one shop can be selected at a time
            if (p.State.CurrentSelection.Count != 1) return false;

            Dictionary<string, int> dcItemsStockRegen;
            if (!dcShopItemsStockRegen.TryGetValue(p.State.CurrentSelection[0], out dcItemsStockRegen)) return true;

            int readyTime;
            if (!dcItemsStockRegen.TryGetValue(itemID, out readyTime)) return true;

            return time > readyTime;
        }

        bool BuyItemForPlayer(Player p, string itemID, int time)
        {
            // only one shop can be selected at a time
            if (p.State.CurrentSelection.Count != 1) return false;

            Dictionary<string, int> dcItemsStockRegen;
            if (!dcShopItemsStockRegen.TryGetValue(p.State.CurrentSelection[0], out dcItemsStockRegen))
            {
                dcItemsStockRegen = new Dictionary<string, int>();
                dcShopItemsStockRegen.Add(p.State.CurrentSelection[0], dcItemsStockRegen);
            }

            // ready_time = time + stock_regen + 300 (buy delay)
            dcItemsStockRegen[itemID] = time + ParserUtility.GetItemStockRegen(itemID) + 350;

            return true;
        }

        bool GetInventoryFromCache(Player p)
        {
            int _item;
            if (!p.gameCacheValues.TryGetValue("8_0",out _item))
                return false;
            for (int i = 0; i < 6; i++)
            {
                _item = p.gameCacheValues["8_" + i.ToString()];
                if (_item == 0)
                    continue;
                item item = new item(_item);
                item.set_owningPlayer(p.player);
                p.Hero.Inventory.put_item(item);
            }
                return true;
        }

        bool FillUpPlayerInventory(Player p)
        {
            //TODO: insert inventory stuff here
            IRecord item;
            unit shop = null;
            Current.player = p.player;
            Current.unit = p.Hero;
            String itemID = null;

            foreach (OrderItem orderitem in p.Items.BuildOrders)
            {
                itemID = orderitem.Name;
                bool isNewVersionItem = DHHELPER.IsNewVersionItem(itemID);
                foreach (unit shop_ in DHLOOKUP.shops)
                    if (DHHELPER.IsNewVersionItemShop(shop_))
                    {
                        if (shop_.sellunits.Contains(itemID))
                        {
                            shop = shop_;
                            break;
                        }
                    }
                    else
                    {
                        if (shop_.sellitems.Contains(itemID))
                        {
                            shop = shop_;
                            break;
                        }
                    }

                p.Hero.set_location(shop.get_location());


                item = isNewVersionItem ? (IRecord)new unit(itemID) : (IRecord)new item(itemID);

                item = item.Clone();

                if (isNewVersionItem)
                {
                    (item as unit).DoSummon = true;
                    (item as unit).set_owningPlayer(p.player);

                    shop.OnSell(item as unit); // sell item as unit
                }
                else
                    shop.OnSellItem(item as item, p.Hero);

                Thread.Sleep(10); // to pass control to item handling script thread
                System.Windows.Forms.Application.DoEvents();
                for (int i = 0; i < p.Hero.Inventory.Capacity-2; i++)
                {
                    if (p.Hero.Inventory.itemAt(i) != null)
                    {
                        // || p.Hero.Inventory[i].Item.iconName.ToString().ToLower().Contains("scroll")
                        if (!p.Hero.Inventory[i].Item.uses.IsNull/* || p.Hero.Inventory[i].Item.goldCost == 57*/)
                        {
                            p.Hero.Inventory[i].drop_item();
                            break;
                        }
                    }
                }
                // Shift down
                for (int i = 0; i < p.Hero.Inventory.Capacity-2; i++)
                    if (p.Hero.Inventory.itemAt(i) == null && p.Hero.Inventory.itemAt(i + 1) != null)
                    {
                        p.Hero.Inventory.swap_items(p.Hero.Inventory[i], p.Hero.Inventory[i + 1]);
                        p.Hero.OnPickupItem(p.Hero.Inventory[i].Item);
                    }

                for (int i = 6; i < p.Hero.Inventory.Capacity - 2; i++)
                { 
                    int index = FindCheapestItemIndex(p.Hero.Inventory);
                    if (p.Hero.Inventory[i].Item!=null)
                        if (p.Hero.Inventory[i].Item.goldCost > p.Hero.Inventory[index].Item.goldCost/* && !p.Hero.Inventory[index].Item.Title.ToString().ToLower().Contains("boots")*/)
                        {
                            p.Hero.Inventory.swap_items(p.Hero.Inventory[i], p.Hero.Inventory[FindCheapestItemIndex(p.Hero.Inventory)]);
                            p.Hero.OnPickupItem(p.Hero.Inventory[0].Item);
                        }
                }
                System.Windows.Forms.Application.DoEvents();
            }

            for (int i = 6; i < p.Hero.Inventory.Capacity - 2; i++)
            {
                if (p.Hero.Inventory[i].Item != null)
                {
                    //item tmp = p.Hero.Inventory[i].Item;
                    //p.Hero.Inventory[i].Item = null;
                    //p.Hero.Inventory.put_item(tmp, i);
                    //shop.OnSellItem(p.Hero.Inventory[i].Item, p.Hero);
                    int index = FindMostExpensiveItemIndex(p.Hero.Inventory);
                    p.Hero.Inventory.swap_items(p.Hero.Inventory[i], p.Hero.Inventory[index]);
                    p.Hero.OnPickupItem(p.Hero.Inventory[0].Item);
                    Thread.Sleep(2);
                    System.Windows.Forms.Application.DoEvents();
                    p.Hero.Inventory.swap_items(p.Hero.Inventory[i], p.Hero.Inventory[index]);
                    p.Hero.OnPickupItem(p.Hero.Inventory[0].Item);
                    Thread.Sleep(2);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            // Shift down
            for (int i = 0; i < p.Hero.Inventory.Capacity - 2; i++)
            {
                if (p.Hero.Inventory.itemAt(i) == null && p.Hero.Inventory.itemAt(i + 1) != null)
                {
                    p.Hero.Inventory.swap_items(p.Hero.Inventory[i], p.Hero.Inventory[i + 1]);
                    p.Hero.OnPickupItem(p.Hero.Inventory[i].Item);
                }
            }


            //Comparison<DBITEMSLOT> UpSorter = delegate(DBITEMSLOT a, DBITEMSLOT b)
            //{
            //    return (a.Item == null ? 20000 : (int)a.Item.goldCost) - (b.Item == null ? 10000 : (int)b.Item.goldCost);
            //};

            //Comparison<DBITEMSLOT> DownSorter = delegate(DBITEMSLOT a, DBITEMSLOT b)
            //{
            //    return (b.Item == null ? 0 : (int)b.Item.goldCost) - (a.Item == null ? 0 : (int)a.Item.goldCost);
            //};


            //p.Hero.Inventory.Sort(UpSorter);
            //p.Hero.OnPickupItem(p.Hero.Inventory[0].Item);

            //p.Hero.Inventory.Sort(DownSorter);
            //p.Hero.OnPickupItem(p.Hero.Inventory[0].Item);
          
//            Thread.Sleep(2); // to pass control to item handling script thread
//            System.Windows.Forms.Application.DoEvents();
            for (int i = 0; i < 4; i++ )
            {
                p.Hero.Inventory.swap_items(p.Hero.Inventory[6+i],p.Hero.Inventory[p.Hero.Inventory.Capacity-4+i]);
            }
            p.Hero.Inventory.RemoveRange(6, p.Hero.Inventory.Capacity - 10);

            System.Windows.Forms.Application.DoEvents();
            System.Console.WriteLine(p.Hero.Title);
            for (int i = 0; i < 6; i++)
            if (p.Hero.Inventory[i].Item!=null)
                Console.WriteLine(p.Hero.Inventory[i].Item.Title);
            return true;
        }
        

        int GetLastItemIndex(DBINVENTORY inventory)
        {
            for (int i = 0; i < inventory.Capacity - 2; i++)
                if (inventory[i].Item != null && inventory[i + 1] == null && inventory[i + 2] == null)
                    return i;
            return 0;
        }

        int FindMostExpensiveItemIndex(DBINVENTORY inventory)
        {
            int cost = 0, index = 0;
            for (int i = 0; i < 6; i++)
                if (inventory[i].Item != null)
                    if (inventory[i].Item.goldCost >= cost)
                    {
                        cost = inventory[i].Item.goldCost;
                        index = i;
                    }
            return index;
        }

        int FindCheapestItemIndex(DBINVENTORY inventory)
        {
            int cost=10000, index=0;
            for(int i=0;i<6;i++)
                if (inventory[i].Item!=null)
                if (inventory[i].Item.goldCost < cost)
                {
                    cost = inventory[i].Item.goldCost;
                    index = i;
                }
            return index;
        }

        bool TryFindHeroByCache(List<int> unitList, out Hero hero)
        {
            foreach (int objectID in unitList)
                if (dcHeroCache.TryGetValue(objectID, out hero))                                    
                    return true;                
            
            hero = null;
            return false;
        }
        bool TryFindHeroByCache(List<int> unitList, out Hero hero, out int heroObjectID)
        {
            foreach (int objectID in unitList)
                if (dcHeroCache.TryGetValue(objectID, out hero))
                {
                    heroObjectID = objectID;
                    return true;
                }

            heroObjectID = 0;
            hero = null;
            return false;
        }

        bool MakeSureHeroExists(Player player, int time, out Hero hero)
        {
            // check if the list of currently selected units contains objectId
            // that references hero in dcHeroCache
            
            int objectId1;

            if (TryFindHeroByCache(player.State.CurrentSelection, out hero, out objectId1))
            {
                // if the hero object is empty                              
                if (hero.ObjectId == -1)
                {
                    string heroName = hero.Name;

                    // get hero owned by this player
                    Hero playerHero = player.Heroes[heroName];

                    // if this player does not own hero with specified name
                    if (playerHero == null)
                    {
                        // then use the object from the hero-cache
                        playerHero = hero;

                        // add this hero to current player
                        player.Heroes.Order(playerHero, time);
                    }
                    else
                        hero = playerHero;

                    // assign object id to this hero
                    hero.ObjectId = objectId1;

                    dcHeroCache[objectId1] = hero;
                }

                IncreaseHeroUseCount(player, hero);

                return true;
            }
            else
            {
                // if hero cannot be found, just try to get most used hero
                hero = player.GetMostUsedHero();
                return hero != null;
            }
        }

        void IncreaseHeroUseCount(Player player, Hero hero)
        {
            KeyValuePair<Hero,int> kvp;
            if (player.UsedHeroes.TryGetValue(hero.Name, out kvp))
                player.UsedHeroes[hero.Name] = new KeyValuePair<Hero, int>(kvp.Key, kvp.Value + 1);
            else            
                player.UsedHeroes.Add(hero.Name, new KeyValuePair<Hero, int>(hero, 1));            
        }

        void SpModeFix()
        {
            // make sure both teams are present and have players 
            // that have the "id" gamecache value

            Team t1 = GetTeamByType(TeamType.Sentinel);
            Team t2 = GetTeamByType(TeamType.Scourge);

            if (t1.TeamNo == -1 || t1.Players.Count == 0 || !t1.Players[0].gameCacheValues.ContainsKey("id")) return;
            if (t2.TeamNo == -1 || t2.Players.Count == 0 || !t2.Players[0].gameCacheValues.ContainsKey("id")) return;

            // now collect all "id" values for each player

            Dictionary<int, Player> dcColorPlayer = new Dictionary<int, Player>();

            int colorId;
            foreach (Player p in players)
                if (p.gameCacheValues.TryGetValue("id", out colorId))
                    dcColorPlayer.Add(colorId, p);
                else
                    return; // "id" not found, then not "-sp" mode maybe

            // ... and recreate teams

            Player player;
            t1.Players.Clear();
            for (int i = 1; i <= 5; i++)
                if (dcColorPlayer.TryGetValue(i, out player))
                {
                    player.Color = (PlayerColor)i;
                    player.SlotNo = (byte)(i - 1);

                    t1.Add(player);
                }

            t2.Players.Clear();
            for (int i = 6; i <= 10; i++)
                if (dcColorPlayer.TryGetValue(i, out player))
                {
                    player.Color = (PlayerColor)(i + 1);
                    player.SlotNo = (byte)(i - 1);

                    t2.Add(player);
                }
        }

        private MemoryStream LoadHeader(Stream stream)
        {
            MemoryStream blocksData = new MemoryStream();
            using (BinaryReader reader = new BinaryReader(stream))
            {
                #region 2.0 [Header]

                #region doc

                //offset | size/type | Description
                //-------+-----------+-----------------------------------------------------------
                //0x0000 | 28 chars  | zero terminated string "Warcraft III recorded game\0x1A\0"
                //0x001c |  1 dword  | fileoffset of first compressed data block (header size)
                //       |           |  0x40 for WarCraft III with patch <= v1.06
                //       |           |  0x44 for WarCraft III patch >= 1.07 and TFT replays
                //0x0020 |  1 dword  | overall size of compressed file
                //0x0024 |  1 dword  | replay header version:
                //       |           |  0x00 for WarCraft III with patch <= 1.06
                //       |           |  0x01 for WarCraft III patch >= 1.07 and TFT replays
                //0x0028 |  1 dword  | overall size of decompressed data (excluding header)
                //0x002c |  1 dword  | number of compressed data blocks in file
                //0x0030 |  n bytes  | SubHeader (see section 2.1 and 2.2)

                #endregion

                ValidateHeaderString(reader.ReadBytes(28));

                int headerSize = reader.ReadInt32();
                //overall size of compressed file
                reader.ReadInt32();
                int versionFlag = reader.ReadInt32();
                //overall size of decompressed data (excluding header)
                reader.ReadInt32();
                int nBlocks = reader.ReadInt32();

                #endregion

                #region SubHeader

                if (versionFlag == 0)
                {
                    throw new W3gParserException("本软件不支持1.06及更低版本录像.");
                }
                else if (versionFlag == 1)
                {
                    #region 2.2 [SubHeader] for header version 1

                    #region doc

                    //offset | size/type | Description
                    //-------+-----------+-----------------------------------------------------------
                    //0x0000 |  1 dword  | version identifier string reading:
                    //       |           |  'WAR3' for WarCraft III Classic
                    //       |           |  'W3XP' for WarCraft III Expansion Set 'The Frozen Throne'
                    //0x0004 |  1 dword  | version number (corresponds to patch 1.xx so far)
                    //0x0008 |  1  word  | build number (see section 2.3)
                    //0x000A |  1  word  | flags
                    //       |           |   0x0000 for single player games
                    //       |           |   0x8000 for multiplayer games (LAN or Battle.net)
                    //0x000C |  1 dword  | replay length in msec
                    //0x0010 |  1 dword  | CRC32 checksum for the header
                    //       |           | (the checksum is calculated for the complete header
                    //       |           |  including this field which is set to zero)

                    #endregion

                    string war3string = DHJassInt.int2id(reader.ReadUInt32()); //ParserUtility.StringFromUInt(reader.ReadUInt32());
                    if (war3string != "W3XP")
                        throw new W3gParserException("本软件只支持冰封王座录像,不支持混乱之治录像.");

                    version = reader.ReadInt32();
                    buildNo = reader.ReadInt32();
                    //flags
                    reader.ReadInt16();
                    //length = reader.ReadInt32();
                    reader.ReadInt32(); length = -1;// will be calculated manually
                    //CRC32 checksum for the header
                    reader.ReadInt32();

                    #endregion
                }

                #endregion

                reader.BaseStream.Seek(headerSize, SeekOrigin.Begin);
                for (int i = 0; i < nBlocks; i++)
                {
                    #region [Data block header]

                    #region doc

                    //offset | size/type | Description
                    //-------+-----------+-----------------------------------------------------------
                    //0x0000 |  1  word  | size n of compressed data block (excluding header)
                    //0x0002 |  1  word  | size of decompressed data block (currently 8k)
                    //0x0004 |  1 dword  | unknown (probably checksum)
                    //0x0008 |  n bytes  | compressed data (decompress using zlib)

                    #endregion

                    ushort compressedSize = reader.ReadUInt16();
                    ushort decompressedSize = reader.ReadUInt16();
                    //unknown (probably checksum)
                    reader.ReadInt32();

                    byte[] decompressed = new byte[decompressedSize];
                    byte[] compressed = reader.ReadBytes(compressedSize);
                    using (InflaterInputStream zipStream = new InflaterInputStream(new MemoryStream(compressed)))
                    {
                        zipStream.Read(decompressed, 0, decompressedSize);
                    }
                    blocksData.Write(decompressed, 0, decompressedSize);

                    #endregion
                }
            }
            blocksData.Seek(0, SeekOrigin.Begin);
            //Stream s= File.Create("replayDecoded.dta");
            //s.Write(blocksData.GetBuffer(), 0, blocksData.GetBuffer().Length);
            //s.Close();
            return blocksData;
        }

        private void LoadPlayers(BinaryReader reader)
        {
            #region 4.0 [Decompressed data]

            #region doc

            // # |   Size   | Name
            //---+----------+--------------------------
            // 1 |   4 byte | Unknown (0x00000110 - another record id?)
            // 2 | variable | PlayerRecord (see 4.1)
            // 3 | variable | GameName (null terminated string) (see 4.2)
            // 4 |   1 byte | Nullbyte
            // 5 | variable | Encoded String (null terminated) (see 4.3)
            //   |          |  - GameSettings (see 4.4)
            //   |          |  - Map&CreatorName (see 4.5)
            // 6 |   4 byte | PlayerCount (see 4.6)
            // 7 |   4 byte | GameType (see 4.7)
            // 8 |   4 byte | LanguageID (see 4.8)
            // 9 | variable | PlayerList (see 4.9)
            //10 | variable | GameStartRecord (see 4.11)

            #endregion
            //Unknown
            reader.ReadInt32();

            host = new Player();
            host.Load(reader);
            players.Add(host);
            type = host.GameType;

            name = ParserUtility.ReadString(reader);
            //nullbyte
            reader.ReadByte();

            byte[] decoded = ParserUtility.GetDecodedBytes(reader);

            settings = new GameSettings(decoded);
            map = new MapInfo(ParserUtility.GetUInt(decoded, 9), ParserUtility.GetString(decoded, 13));
            #endregion

            //playerCount, gameType, langId            
            reader.ReadBytes(12);

            #region Player List
            while (reader.PeekChar() == 0x16)
            {
                #region doc
                //offset | size/type | Description
                //-------+-----------+-----------------------------------------------------------
                //0x0000 | 4/11 byte | PlayerRecord (see 4.1)
                //0x000? |    4 byte | unknown
                //       |           |  (always 0x00000000 for patch version >= 1.07
                //       |           |   always 0x00000001 for patch version <= 1.06)
                #endregion

                Player p = new Player();
                p.Load(reader);
                players.Add(p);
                reader.ReadBytes(4);
            }
            #endregion

            #region 4.10 [GameStartRecord]
            #region doc
            //offset | size/type | Description
            //-------+-----------+-----------------------------------------------------------
            //0x0000 |  1 byte   | RecordID - always 0x19
            //0x0001 |  1 word   | number of data bytes following
            //0x0003 |  1 byte   | nr of SlotRecords following (== nr of slots on startscreen)
            //0x0004 |  n bytes  | nr * SlotRecord (see 4.11)
            //   n+4 |  1 dword  | RandomSeed (see 4.12)
            //   n+8 |  1 byte   | SelectMode
            //       |           |   0x00 - team & race selectable (for standard custom games)
            //       |           |   0x01 - team not selectable
            //       |           |          (map setting: fixed alliances in WorldEditor)
            //       |           |   0x03 - team & race not selectable
            //       |           |          (map setting: fixed player properties in WorldEditor)
            //       |           |   0x04 - race fixed to random
            //       |           |          (extended map options: random races selected)
            //       |           |   0xcc - Automated Match Making (ladder)
            //   n+9 |  1 byte   | StartSpotCount (nr. of start positions in map)
            #endregion

            //RecordId, number of data bytes following
            reader.ReadBytes(3);
            byte nSlots = reader.ReadByte();
            for (byte i = 0; i < nSlots; i++)
            {
                #region 4.11 [SlotRecord]

                #region doc
                //offset | size/type | Description
                //-------+-----------+-----------------------------------------------------------
                //0x0000 |  1 byte   | player id (0x00 for computer players)
                //0x0001 |  1 byte   | map download percent: 0x64 in custom, 0xff in ladder
                //0x0002 |  1 byte   | slotstatus:
                //       |           |   0x00 empty slot
                //       |           |   0x01 closed slot
                //       |           |   0x02 used slot
                //0x0003 |  1 byte   | computer player flag:
                //       |           |   0x00 for human player
                //       |           |   0x01 for computer player
                //0x0004 |  1 byte   | team number:0 - 11
                //       |           | (team 12 == observer or referee)
                //0x0005 |  1 byte   | color (0-11):
                //       |           |   value+1 matches player colors in world editor:
                //       |           |   (red, blue, cyan, purple, yellow, orange, green,
                //       |           |    pink, gray, light blue, dark green, brown)
                //       |           |   color 12 == observer or referee
                //0x0006 |  1 byte   | player race flags (as selected on map screen):
                //       |           |   0x01=human
                //       |           |   0x02=orc
                //       |           |   0x04=nightelf
                //       |           |   0x08=undead
                //       |           |   0x20=random
                //       |           |   0x40=race selectable/fixed (see notes below)
                //0x0007 |  1 byte   | computer AI strength: (only present in v1.03 or higher)
                //       |           |   0x00 for easy
                //       |           |   0x01 for normal
                //       |           |   0x02 for insane
                //       |           | for non-AI players this seems to be always 0x01
                //0x0008 |  1 byte   | player handicap in percent (as displayed on startscreen)
                //       |           | valid values: 0x32, 0x3C, 0x46, 0x50, 0x5A, 0x64
                //       |           | (field only present in v1.07 or higher)
                #endregion

                #region
                byte playerId = reader.ReadByte();
                reader.ReadByte();
                byte slotStatus = reader.ReadByte();
                byte computerFlag = reader.ReadByte();
                byte teamNo = reader.ReadByte();
                PlayerColor color = (PlayerColor)reader.ReadByte();
                Race race = (Race)reader.ReadByte();
                if ((uint)race > 0x40)
                    race -= 0x40;
                AIStrength strength = (AIStrength)reader.ReadByte();
                int handicap = reader.ReadByte();
                #endregion

                #region
                if (slotStatus == 0x02)
                {
                    Player player = GetPlayerById(playerId);
                    if (playerId == 0 && /*player == null &&*/ computerFlag == 0x01)
                    {
                        player = new Player();
                        player.Race = race;
                        player.Id = i;
                        if (strength == AIStrength.Easy)
                            player.Name = "Computer(Easy)";
                        else if (strength == AIStrength.Normal)
                            player.Name = "Computer(Normal)";
                        else
                            player.Name = "Computer(Insane)";
                        players.Add(player);
                    }
                    else if (player == null)
                    {
                        continue;
                    }                   
                    player.SlotNo = i;
                    player.Color = color;
                    player.Handicap = handicap;
                    player.Id = playerId;
                    player.IsComputer = computerFlag == 0x01;
                    player.IsObserver = teamNo == 12;
                    player.Race = race;
                    player.TeamNo = teamNo; //+ 1;
                }
                #endregion
                #endregion
            }


            if (buildNo == 0 && nSlots == 0)
            {
                #region 6.1 Notes on official Blizzard Replays
                #region doc
                //o Since the lack of all slot records, one has to generate these data:
                //iterate slotNumber from 1 to number of PlayerRecords (see 4.1)
                //  player id    = slotNumber
                //  slotstatus   = 0x02                   (used)
                //  computerflag = 0x00                   (human player)
                //  team number  = (slotNumber -1) mod 2  (team membership alternates in
                //                                                          PlayerRecord)
                //  color        = unknown                (player gets random colors)
                //  race         = as in PlayerRecord
                //  computerAI   = 0x01                   (non computer player)
                //  handicap     = 0x64                   (100%)
                #endregion
                foreach (Player player in players)
                {
                    player.SlotNo = player.Id;
                    player.TeamNo = (player.SlotNo - 1) % 2;
                    player.Handicap = 100;
                }
                #endregion
            }            

            // sort players by their teams
            foreach (Player player in players)
            {
                // mark uninitialized players as Observers to avoid errors
                if (player.Race == 0 && player.TeamNo == 0)
                {
                    Console.WriteLine("Warning: Player '" + player.Name + "' was not initialized and thus marked as Observer");

                    player.TeamNo = 12;
                    player.IsObserver = true;
                }

                Team team = GetTeamByNo(player.TeamNo);
                team.AddSortedBySlot(player);
            }
            //random seed, select mode, startspotcount
            reader.ReadBytes(6);
            #endregion
        }

        private Team GetTeamByNo(int teamNo)
        {
            foreach (Team team in teams)
                if (team.TeamNo == teamNo)
                    return team;
            Team t = new Team(teamNo);
            teams.Add(t);
            return t;
        }

        public Team GetTeamByType(TeamType teamType)
        {
            foreach (Team team in teams)
                if (team.Type == teamType)
                    return team;
            return new Team(-1);
        }

        private Player GetPlayerById(byte playerId)
        {
            foreach (Player player in players)
            {
                if (player.Id == playerId)
                    return player;
            }
            return null;
        }
        private Player GetPlayerBySpecialSlot(int slot)
        {
            if (slot > 0 && slot < 6)
                return GetPlayerBySlot(slot - 1);

            if (slot > 6 && slot < 12)
                return GetPlayerBySlot(slot - 2);

            return GetPlayerBySlot(slot);
        }
        private Player GetPlayerBySlot(int slot)
        {
            foreach (Player player in players)
            {
                if (player.SlotNo == slot)
                    return player;
            }
            return null;
        }        

        private void ValidateHeaderString(byte[] header)
        {
            for (int i = 0; i < 28; i++)
            {
                if (HeaderString[i] != (char)header[i])
                    throw new W3gParserException("指定的文件不是合法的Warcraft III Replay文件。");
            }
        }

        private unit SummonHero(string StringId, Player p)
        {
            //code based on SellHero proc in HeroListForm.cs
            Current.player = p.player;
            unit sellingTavern = null;
            unit hero = null;

            // find the tavern that sold this hero

            string tavernID = DHLOOKUP.dcHeroesTaverns[StringId];
            foreach (unit tavern in DHLOOKUP.taverns)
                if (tavern.ID == tavernID)
                {
                    sellingTavern = tavern;
                    break;
                }

            // create new hero

            hero = new unit(StringId, p.Items.BuildOrders.Count);
            hero.DoSummon = true;
            hero.set_owningPlayer(p.player); //!

            // only new heroes process onsell event

            sellingTavern.OnSell(hero);
            Current.unit = hero;
    //        Current.player.units.Add(0,hero);
            return hero;
        }
    }

    public delegate void MapRequiredEventHandler(object sender, EventArgs e);                
}