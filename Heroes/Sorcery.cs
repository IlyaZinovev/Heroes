using System;

namespace Heroes
{
    public class Sorcery
    {
        public Sorcery(string name)
        {
            Name = name;
        }
        public string Name
        { get; private set; }
        public BattleUnitsStack Owner
        { get; protected set; }
        virtual public void SingleCast(BattleUnitsStack target) { }
        public void Cast(Target target, BattleUnitsStack owner)
        {
            Owner = owner;
            switch (target.Type)
            {
                case TargetType.Stack:
                    {
                        SingleCast(target.Stack);
                        break;
                    }
                case TargetType.Army:
                    {
                        for (int i = 0; i < target.Army.Legion.Count; ++i)
                            SingleCast(target.Army.Legion[i]);
                        break;
                    }
                default:
                    throw new Exception();
            }
        }
    }
}
