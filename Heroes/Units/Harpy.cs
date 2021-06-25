using System;

namespace Heroes.Units
{
    public class Harpy : Unit
    {
        public Harpy() : base("Harpy", 24, 16, 15, new Tuple<int, int>(4, 5), 3.5)
        {
            Abylities.Add(new Modificators.NonCounter());
        }
    }
}
