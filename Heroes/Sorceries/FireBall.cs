using System;

namespace Heroes.Sorceries
{
    public class FireBall : Sorcery
    {
        public FireBall() : base("FireBall") { }
        public override void SingleCast(BattleUnitsStack target)
        {
            target.ReceiveDamage(3 * Owner.Number);
        }
    }
}
