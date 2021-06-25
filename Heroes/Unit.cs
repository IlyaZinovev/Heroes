using System;
using System.Collections.Generic;

namespace Heroes
{
    enum UnitType
    {
        Berserker,
        Dwarf,
        Orc,
        Fairy,
        Harpy,
        Mage,
        Medusa,
        Elf,
        //here now
        Cyclops,
        Naga,
        Efreet,
        Unicorn,
        Behemoth,
        Titan,
        Dragon,
        Phoenix
    }
    public class Unit : Object
    {
        public string Type
        { get;}
        public int HitPoints
        { get; }
        public int Attack
        { get; }
        public int Defense
        { get; }
        public Tuple<int, int> Damage
        { get; }
        public double Initiative
        { get; }
        protected List<Sorcery> SorceryBook;
        public IList<Sorcery> ShowSorceries => SorceryBook.AsReadOnly();
        protected List<Modificator> Abylities;
        public IList<Modificator> ShowAbylities => Abylities.AsReadOnly();
        public Unit(string type, int hp, int attack, int defense, Tuple<int, int> damage, double initiative)
        {
            Type = type;
            HitPoints = hp;
            Attack = attack;
            Defense = defense;
            Damage = damage;
            Initiative = initiative;
            SorceryBook = new List<Sorcery>();
            Abylities = new List<Modificator>();
        }
    }
}
