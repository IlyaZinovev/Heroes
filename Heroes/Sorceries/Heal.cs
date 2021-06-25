using System;

namespace Heroes.Sorceries
{
    public class Heal : Sorcery
    {
        public Heal() : base("Heal")
        { }
        public override void SingleCast(BattleUnitsStack target)
        {
            target.Healing(1 * Owner.Number);
        }
    }
}
