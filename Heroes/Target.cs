using System;

namespace Heroes
{
    public enum TargetType
    {
        All,
        Nobody,
        Stack,
        Army
    }
    public class Target
    {
        public TargetType Type
        { get; private set; }
        public BattleUnitsStack Stack
        { get; set; }
        public BattleArmy Army
        { get; set; }
        public Target(TargetType type, BattleUnitsStack stack)
        {
            if (type == TargetType.Stack)
            {
                Type = type;
                Stack = stack;
            }
            else
                throw new Exception();
        }
        public Target(TargetType type, BattleArmy army)
        {
            if (type == TargetType.Army)
            {
                Type = type;
                Army = army;
            }
            else
                throw new Exception();
        }
        public Target(TargetType type)
        {
            if (type == TargetType.All || type == TargetType.Nobody)
                Type = type;
            else
                throw new Exception();
        }
    }
}
