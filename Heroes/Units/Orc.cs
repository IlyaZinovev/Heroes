using System;

namespace Heroes.Units
{
    public class Orc : Unit
    {
        public Orc() : base("Orc", 12, 11, 9, new Tuple<int, int>(1, 3), 2.5)
        {
            Abylities.Add(new Modificators.IronWall());
        }
    }
}
