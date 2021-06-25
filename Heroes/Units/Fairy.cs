using System;

namespace Heroes.Units
{
    public class Fairy : Unit
    {
        public Fairy(): base("Fairy", 7, 10, 10, new Tuple<int, int>(1, 2), 4.0)
        {
            SorceryBook.Add(new Sorceries.Heal());
            Abylities.Add(new Modificators.NonCounter());

        }
    }
}
