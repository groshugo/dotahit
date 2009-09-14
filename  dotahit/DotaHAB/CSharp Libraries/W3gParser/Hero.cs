using System.Collections.Generic;
using DotaHIT.Core.Resources;
using DotaHIT.DatabaseModel.Format;

namespace Deerchao.War3Share.W3gParser
{
    public class Hero
    {
        // this delay is generally not necessary but sometimes helps in situations when
        // the replay data shows several almost instant skill researches, while in fact 
        // the hero gained new level and thus able to research only one skill. Since I cannot
        // determine the level of the hero, I've decided to use this threshold to filter out cases like described,
        // though I think it's not safe to use in all situations.
        private static readonly int trainSkillDelay = 250;//250;//2000;
        private static readonly int retrainTimeDelayAfterUsingTome = 15000;
        private int lastRetrainTime;
        private int lastTrainTime;

        private readonly string name;        
        private int level;
        private Dictionary<string, int> dcAbilities = new Dictionary<string, int>();
        readonly List<Dictionary<string, int>> abilitySets = new List<Dictionary<string, int>>();
        private readonly List<int> reviveTimes = new List<int>();
        private readonly Items abilities = new Items();
        

        int objectId = -1;

        private int possibleRetrainedTime;

        public Hero(string name)
        {
            this.name = name;
        }

        public int Level
        {
            get { return level; }
        }

        public string Name
        {
            get { return name; }
        }

        public int ObjectId
        {
            get
            {
                return objectId;
            }
            set
            {
                objectId = value;
            }
        }

        public Items Abilities
        {
            get
            {
                return abilities;
            }
        }

        public IEnumerable<string> GetAbilityNames()
        {
            foreach (string ability in dcAbilities.Keys)
                yield return ability;
        }

        public int GetAbilityLevel(string ability)
        {
            if (!dcAbilities.ContainsKey(ability))
                return 0;
            return dcAbilities[ability];
        }

        public int GetAbilitySetCount()
        {
            return abilitySets.Count;
        }

        public IEnumerable<string> GetAbilityNames(int abilitySetIndex)
        {
            foreach (string ability in abilitySets[abilitySetIndex].Keys)
                yield return ability;
        }

        public int GetAbilityLevel(string ability, int abilitySetIndex)
        {
            return abilitySets[abilitySetIndex][ability];
        }

        internal void Order(int time)
        {
            reviveTimes.Add(time);
        }

        public bool Train(string ability, int time, int maxAllowedLevelForResearch)
        {
            if (time - possibleRetrainedTime < retrainTimeDelayAfterUsingTome)
            {
                if (lastRetrainTime != possibleRetrainedTime)
                {
                    lastRetrainTime = possibleRetrainedTime;
                    level = 0;
                    dcAbilities = new Dictionary<string, int>();
                    abilitySets.Add(dcAbilities);
                }

                if (dcAbilities.ContainsKey(ability))
                    dcAbilities[ability]++;
                else
                    dcAbilities.Add(ability, 1);

                level++;

                abilities.Order(new OrderItem(ability, time));
                return true;
            }
            else if (time - lastTrainTime > trainSkillDelay)
            {
                int abilityLevel;
                if (dcAbilities.TryGetValue(ability, out abilityLevel))
                    abilityLevel++;
                else
                    abilityLevel = 1;

                int requriedHeroLevel = DHHELPER.GetRequiredHeroLevelForAbility(ability, abilityLevel);

                if (maxAllowedLevelForResearch < requriedHeroLevel)
                    return false;

                dcAbilities[ability] = abilityLevel;                

                level++;
                lastTrainTime = time;

                abilities.Order(new OrderItem(ability, time, requriedHeroLevel));
                return true;
            }

            return false;
        }

        private bool CanResearchAtThisLevel(int levelForResearch, string abilityName, int abilityLevel)
        {
            DotaHIT.Core.HabProperties hpsAbilityData = DHLOOKUP.hpcAbilityData[abilityName];

            int reqLevel = hpsAbilityData.GetIntValue("reqLevel");
            int levelSkip = hpsAbilityData.GetIntValue("levelSkip", 2);

            return levelForResearch >= (reqLevel + (levelSkip * (abilityLevel - 1)));
        }

        internal void PossibleRetrained(int time)
        {
            possibleRetrainedTime = time;
        }

        internal void Cancel(int time)
        {
            reviveTimes.Remove(time);
        }

        public string GetClass()
        {
            return DHFormatter.ToString(DHLOOKUP.hpcUnitProfiles[name, "Name"]);
        }

        public override string ToString()
        {
            return "[" + name + "]" + DHFormatter.ToString(DHLOOKUP.hpcUnitProfiles[name, "Name"]);
        }
    }
}