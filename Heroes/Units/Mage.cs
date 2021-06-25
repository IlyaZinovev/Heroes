using System;

namespace Heroes.Units
{
    public class Mage : Unit
    {
        public Mage() : base("Mage", 16, 6, 12, new Tuple<int, int>(3, 4), 1.8)
        {
            SorceryBook.Add(new Sorceries.FireBall());
            SorceryBook.Add(new Sorceries.Inspire());
            SorceryBook.Add(new Sorceries.Stun());
        }
    }
}
