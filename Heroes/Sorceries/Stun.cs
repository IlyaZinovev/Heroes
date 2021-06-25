using System;

namespace Heroes.Sorceries
{
    class Stun : Sorcery
    {
        public Stun() : base("Stun") { }
        public override void SingleCast(BattleUnitsStack target)
        {
            target.Class.Initiative -= 0.05 * Owner.Number;
        }
    }
}
