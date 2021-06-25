using System;

namespace Heroes.Sorceries
{
    class Inspire : Sorcery
    {
        public Inspire() : base("Inspire") { }
        public override void SingleCast(BattleUnitsStack target)
        {
            target.Class.Initiative += 0.05 * Owner.Number;
        }
    }
}
