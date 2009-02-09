using System;
using System.IO;
using System.Collections.Generic;
using DotaHIT.Jass.Native.Types;

namespace Deerchao.War3Share.W3gParser
{
    public class Player
    {
        byte playerId;
        string name;
        GameType gameType;
        private LineUp lineUp;
        private location lineUpLocation;
        private int time;
        private Race race;
        private byte slotNo;
        private PlayerColor color;
        private int handicap;
        private bool isComputer;
        private bool isObserver;
        private int teamNo;        
        private int actionsCount;
        private readonly Heroes heroes = new Heroes();
        private readonly Items buildings = new Items();
        private readonly Items items = new Items();
        private readonly Items abilities = new Items();
        private readonly Units units = new Units();
        private readonly Units upgrades = new Units();
        private readonly Units researches = new Units();
        private readonly Groups groups = new Groups();
        private readonly List<PlayerAction> actions = new List<PlayerAction>();
        private readonly Dictionary<string, KeyValuePair<Hero, int>> usedHeroes = new Dictionary<string, KeyValuePair<Hero, int>>();
        public Dictionary<string, int> gameCacheValues = new Dictionary<string, int>();
        public unit Hero;
        public player player;

        public byte Id
        {
            get { return playerId; }
            set { playerId = value; }
        }

        public string Name
        {
            get { return name; }
            internal set { name = value; }
        }

        public byte SlotNo
        {
            get { return slotNo; }
            internal set { slotNo = value; }
        }

        public Race Race
        {
            get { return race; }
            internal set { race = value; }
        }

        public PlayerColor Color
        {
            get { return color; }
            internal set { color = value; }
        }

        public int Handicap
        {
            get { return handicap; }
            internal set { handicap = value; }
        }

        public bool IsComputer
        {
            get { return isComputer; }
            internal set { isComputer = value; }
        }

        public bool IsObserver
        {
            get { return isObserver; }
            internal set { isObserver = value; }
        }

        public int TeamNo
        {
            get { return teamNo; }
            internal set { teamNo = value; }
        }

        public PlayerState State = new PlayerState();       

        public GameType GameType
        {
            get { return gameType; }
        }

        public TeamType TeamType
        {
            get
            {
                if (isObserver) return TeamType.Observers;
                if (slotNo < 5) return TeamType.Sentinel;
                else return TeamType.Scourge;
            }
        }

        public LineUp LineUp
        {
            get { return lineUp; }
            set { lineUp = value; }
        }

        public location LineUpLocation
        {
            get { return lineUpLocation; }
            set { lineUpLocation = value; }
        }

        public int ActionsCount
        {
            get { return actionsCount; }
            internal set { actionsCount = value; }
        }

        public List<PlayerAction> Actions
        {
            get { return actions; }
        }

        public Dictionary<string, KeyValuePair<Hero, int>> UsedHeroes
        {
            get { return usedHeroes; }
        }

        public int Time
        {
            get { return time; }
            internal set { time = value; }
        }

        public TimeSpan GetTime()
        {
            return new TimeSpan(0, 0, 0, 0, time);
        }

        public float Apm
        {
            get
            {
                if (time == 0)
                    return 0;
                return actionsCount * 1000 * 60 / time;
            }
        }

        public Heroes Heroes
        {
            get { return heroes; }
        }

        public Items Buildings
        {
            get
            {
                return buildings;
            }
        }

        public Items Items
        {
            get
            {
                return items;
            }
        }

        public Items Abilities
        {
            get
            {
                return abilities;
            }
        }

        public Units Units
        {
            get { return units; }
        }

        public Units Upgrades
        {
            get
            {
                return upgrades;
            }
        }

        public Units Researches
        {
            get
            {
                return researches;
            }
        }

        internal Groups Groups
        {
            get { return groups; }
        }       

        // 4.1 [PlayerRecord]
        internal void Load(BinaryReader reader)
        {
            #region doc
            //offset | size/type | Description
            //-------+-----------+-----------------------------------------------------------
            //0x0000 |  1 byte   | RecordID:
            //       |           |  0x00 for game host
            //       |           |  0x16 for additional players (see 4.9)
            //0x0001 |  1 byte   | PlayerID
            //0x0002 |  n bytes  | PlayerName (null terminated string)
            //   n+2 |  1 byte   | size of additional data:
            //       |           |  0x01 = custom
            //       |           |  0x08 = ladder
            #endregion
            //RecordId
            reader.ReadByte();
            playerId = reader.ReadByte();
            name = ParserUtility.ReadString(reader);
            gameType = (GameType)reader.ReadByte();

            if (gameType == GameType.Custom)
                reader.ReadByte();
            else
            {
                //time
                reader.ReadInt32();
                Race = (Race)reader.ReadUInt32();
            }
            player = player.players[playerId];
        }

        public int getGCValue(string key)
        {
            int value;
            gameCacheValues.TryGetValue(key, out value);
            return value;
        }
        public string getGCVStringValue(string key, string retOnFail)
        {
            int value;
            if (gameCacheValues.TryGetValue(key, out value))
                return value.ToString();
            else
                return retOnFail;            
        }  

        public Hero GetMostUsedHero()
        {            
            KeyValuePair<Hero,int> kvpMax = new KeyValuePair<Hero,int>(null, -1);

            foreach (KeyValuePair<Hero, int> kvp in usedHeroes.Values)
                if (kvp.Value > kvpMax.Value)
                    kvpMax = kvp;

            return kvpMax.Key != null ? kvpMax.Key : (heroes.Count > 0 ? heroes[0] : null);
        }

        public string GetMostUsedHeroClass()
        {
            Hero hero = this.GetMostUsedHero();
            if (hero != null)
                return hero.GetClass();
            else
                return "";
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}