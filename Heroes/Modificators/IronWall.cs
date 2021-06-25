using System;

namespace Heroes.Modificators
{
    class IronWall : Modificator
    {
        public IronWall() : base("IronWall", ModificatorType.Defending) { }
        public override void Do(Action action)
        {
            if (action.Type != ActionType.Defend)
                throw new Exception();
            action.Owner.Class.Defense *= 2;
        }
    }
}
