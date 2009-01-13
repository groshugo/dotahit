using System.Collections.Generic;

namespace Deerchao.War3Share.W3gParser
{
    public class Heroes
    {
        readonly List<Hero> heroes = new List<Hero>();
        readonly List<OrderItem> buildOrders = new List<OrderItem>();

        internal void Order(Hero h, int time)
        {
            heroes.Add(h);
            buildOrders.Add(new OrderItem(h.Name, time));
        }

        internal void Order(string name, int time)
        {
            foreach (Hero hero in heroes)
            {
                if (hero.Name == name)
                {
                    hero.Order(time);
                    return;
                }
            }
            Hero h = new Hero(name);
            heroes.Add(h);
            buildOrders.Add(new OrderItem(name, time));
        }

        public IEnumerable<Hero> Items
        {
            get
            {
                foreach (Hero hero in heroes)
                    yield return hero;
            }
        }

        public List<OrderItem> BuildOrders
        {
            get
            {
                return buildOrders;
            }
        }

        public Hero this[string name]
        {
            get
            {
                foreach (Hero hero in heroes)
                    if (hero.Name == name)
                        return hero;
                return null;
            }
        }

        public Hero this[int index]
        {
            get
            {
                if (index < this.Count)
                    return heroes[index];    
                return null;
            }
        }

        internal void Cancel(string name, int time)
        {
            foreach (Hero hero in heroes)
            {
                if (hero.Name == name)
                {
                    hero.Cancel(time);
                    return;
                }
            }
        }

        internal bool Train(string ability, int time)
        {
            //string heroName = ParserUtility.GetHeroByAbility(ability);
            foreach (Hero hero in heroes)
            {
                //if (string.Compare(hero.Name, heroName, true)==0)
                //{
                    return hero.Train(ability, time, hero.Level);
                //}
            }

            return false;
        }

        internal void PossibleRetrained(int time)
        {
            foreach (Hero hero in heroes)
                hero.PossibleRetrained(time);
        }

        public int Count
        {
            get
            {
                return heroes.Count;
            }
        }
    }
}