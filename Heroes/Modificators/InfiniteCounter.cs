using System;

namespace Heroes.Modificators
{
    class InfiniteCounter : Modificator
    {
        public InfiniteCounter() : base("InfiniteCounter", ModificatorType.BeingAttacked)
        { }
        public override void Do(Action action)
        {
            if (action.Type != ActionType.Attack)
                throw new Exception();
            action.CounterFlag = true;
        }
    }
}
