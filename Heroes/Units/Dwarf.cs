using System;

namespace Heroes.Units
{
    public class Dwarf : Unit
    {
        public Dwarf() : base("Dwarf", 12, 11, 11, new Tuple<int, int>(2, 3), 2.0)
        {}
    }
}
