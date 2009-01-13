using System;
using System.Collections.Generic;
using System.Text;
using DotaHIT.Jass.Types;
using DotaHIT.Core;
using DotaHIT.Core.Resources;

namespace DotaHIT.Jass.Native.Constants
{
    public enum OrderID
    {
        none = 0,
        smart = 851971,
        stop = 851972,
        cancel = 851976,
        attack = 851983,
        attackground = 851984,
        move = 851986,
        buildmenu = 851994,
        skillmenu = 852000,
        moveslot1 = 852002,
        moveslot2 = 852003,
        moveslot3 = 852004,
        moveslot4 = 852005,
        moveslot5 = 852006,
        moveslot6 = 852007,
        useslot1 = 852008,
        useslot2 = 852009,
        useslot3 = 852010,
        useslot4 = 852011,
        useslot5 = 852012,
        useslot6 = 852013,
        replenishlifeon = 852546,
        replenishlifeoff = 852547,
        replenishmana = 852548,
        replenishmanaon = 852549,
        replenishmanaoff = 852550
    }
    public class orderid
    {
        public static OrderID Parse(string order)
        {
            if (string.IsNullOrEmpty(order)) return OrderID.none;

            try { return (OrderID)Enum.Parse(typeof(OrderID), order, true); }
            catch { return OrderID.none; }
        }
    }

    /*public class alliancetype
    {
        static Dictionary<int, string> ValueNamePairs = new Dictionary<int, string>();
        static alliancetype()
        {
            foreach (KeyValuePair<string, DHJassValue> kvp in DHJassExecutor.War3Globals)
                if (kvp.Key.StartsWith("ALLIANCE_"))
                    ValueNamePairs[kvp.Value.IntValue] = kvp.Key;                
        }
        public static void WakeUp() { }
        public static string getName(int value)
        {
            string name;
            ValueNamePairs.TryGetValue(value, out name);
            return name;
        }
    }*/
    public class itemtype
    {
        static Dictionary<string, int> NameValuePairs = new Dictionary<string, int>();
        static itemtype()
        {
            HabProperties hpsData = DHMpqDatabase.EditorDatabase["Data"]["itemClass"];

            int value;
            string name;

            // 00=Permanent,WESTRING_ITEMCLASS_PERMANENT
            // NumValues=7  

            foreach (KeyValuePair<string, object> kvp in hpsData)
                if (int.TryParse(kvp.Key, out value))
                {
                    name = (kvp.Value as string).Split(',')[0];
                    NameValuePairs.Add(name, value);
                }
        }
        public static void WakeUp() { }
        public static int getValue(string name)
        {
            if (name == null) 
                return 0;
            int value;
            NameValuePairs.TryGetValue(name, out value);
            return value;
        }
    }
    public class unittype
    {
        static Dictionary<int, string> ValueNamePairs = new Dictionary<int, string>();
        static unittype()
        {
            foreach (KeyValuePair<string, DHJassValue> kvp in DHJassExecutor.War3Globals)
                if (kvp.Key.StartsWith("UNIT_TYPE_"))
                    ValueNamePairs[kvp.Value.IntValue] = kvp.Key;
        }
        public static void WakeUp() { }
        public static string getName(int value)
        {
            string name;
            ValueNamePairs.TryGetValue(value, out name);
            return name;
        }
    }

    public class map
    {
        public static readonly int minX = -8192;
        public static readonly int minY = -8192;
        public static readonly int maxX = 8192;
        public static readonly int maxY = 8192;
    }
}
