using System;

namespace Heroes
{
    public enum ModificatorType
    {
        Attacking,
        BeingAttacked,
        Cast,
        CastingOnThis,
        Defending,
        Waiting
    }
    public class Modificator
    {
        public ModificatorType Type
        { get; protected set; }
        public BattleUnitsStack Owner
        { get; set; }
        public string Name
        { get; protected set; }
        public Modificator(string name, ModificatorType type)
        {
            Name = name;
            Type = type;
        }
        virtual public void Do(Action action) { }
    }
}
