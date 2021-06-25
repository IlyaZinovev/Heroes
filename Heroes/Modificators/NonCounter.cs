using System;

namespace Heroes.Modificators
{
    public class NonCounter : Modificator
    {
        public NonCounter() : base("NonCounter", ModificatorType.Attacking)
        { }
        public override void Do(Action action)
        {
            if (action.Type != ActionType.Attack)
                throw new Exception();
            action.CounterFlag = false;
        }
    }
}
