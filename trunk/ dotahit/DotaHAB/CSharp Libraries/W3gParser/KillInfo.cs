using System;

namespace Deerchao.War3Share.W3gParser
{
    public class KillInfo
    {
        private readonly TimeSpan time;
        private readonly Player killer;        
        private readonly Player victim;

        public KillInfo(int time, Player killer, Player victim)
        {
            this.time = new TimeSpan(0, 0, 0, 0, time);
            this.killer = killer;
            this.victim = victim;          
        }

        public TimeSpan Time
        {
            get { return time; }
        }

        public Player Killer
        {
            get { return killer; }
        }

        public Player Victim
        {
            get { return victim; }
        }
    }
}