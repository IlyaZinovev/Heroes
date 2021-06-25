using System;

namespace Heroes.Units
{
    public class Elf : Unit
    {
        public Elf() : base("Elf", 18, 8, 16, new Tuple<int, int>(3, 4), 3.5)
        {
            Abylities.Add(new Modificators.NonCounter());
        }
    }
}
