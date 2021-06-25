using System;

namespace Heroes.Units
{
    public class Medusa : Unit
    {
        public Medusa() : base("Medusa", 24, 19, 17, new Tuple<int, int>(3, 6), 2.4)
        {
            SorceryBook.Add(new Sorceries.Stun());
        }
    }
}
