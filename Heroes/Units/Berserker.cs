using System;

namespace Heroes.Units
{
    public class Berserker : Unit
    {
        public Berserker(): base("Berserker", 18, 12, 8, new Tuple<int, int>(2, 3), 3.0)
        {}
    }
}
