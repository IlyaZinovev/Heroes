using System;
using System.Collections.Generic;

namespace Heroes
{
    public class BattleArmy
    {
        public List<BattleUnitsStack> Legion;
        public BattleArmy(Army start)
        {
            Legion = new List<BattleUnitsStack>();
            IsAlive = true;
            foreach (UnitsStack i in start.Show)
            {
                Legion.Add(new BattleUnitsStack(i));
            }
        }
        public bool IsAlive
        { get; private set; }
        public void CheckAlive()
        {
            if (!IsAlive)
                return;
            bool result = false;
            foreach (BattleUnitsStack stack in Legion)
            {
                if (stack.IsAlive)
                {
                    result = true;
                    break;
                }
            }
            IsAlive = result;
        }
        public void KillArmy()
        {
            IsAlive = false;
        }
        public IList<BattleUnitsStack> Show => Legion.AsReadOnly();
        public void Add(BattleUnitsStack units)
        {
            if (IsFull)
                throw new Exception();
            Legion.Add(units);
        }
        public bool IsFull
        {
            get
            {
                return Legion.Count == 9;
            }
        }
    }
}
