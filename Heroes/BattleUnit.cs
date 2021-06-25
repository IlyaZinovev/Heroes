using System;
using System.Collections.Generic;

namespace Heroes
{
    public class BattleUnit
    {
        public string Type
        { get; set; }
        public int HitPoints
        { get; set; }
        public int Attack
        { get; set; }
        public int Defense
        { get; set; }
        public Tuple<int, int> Damage
        { get; set; }
        public double Initiative
        { get; set; }
        private List<Sorcery> SorceryBook;
        public IList<Sorcery> ShowSorceries => SorceryBook.AsReadOnly();
        private List<Modificator> Abylities;
        public IList<Modificator> ShowAbylities => Abylities.AsReadOnly();
        public BattleUnit(Unit unit)
        {
            Type = unit.Type;
            HitPoints = unit.HitPoints;
            Attack = unit.Attack;
            Defense = unit.Defense;
            Damage = unit.Damage;
            Initiative = unit.Initiative;
            SorceryBook = new List<Sorcery>();
            foreach (Sorcery sorcery in unit.ShowSorceries)
                SorceryBook.Add(sorcery);
            Abylities = new List<Modificator>();
            foreach (Modificator modificator in unit.ShowAbylities)
                Abylities.Add(modificator);
        }
    }
}
