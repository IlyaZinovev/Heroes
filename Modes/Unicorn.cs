using System;
using System.Collections.Generic;
using Heroes;

namespace Modes
{
    public class Unicorn : Unit
    {
        public Unicorn() : base("Unicorn", 80, 20, 20, new Tuple<int, int>(12, 20), 2.7)
        {
            SorceryBook = new List<Sorcery>();
            Abylities = new List<Modificator>();
        }
    }
}
