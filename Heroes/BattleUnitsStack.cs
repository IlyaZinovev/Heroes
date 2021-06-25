using System;

namespace Heroes
{
    public class BattleUnitsStack : IComparable<BattleUnitsStack>
    {
        public BattleUnit Class
        { get; private set; }
        public int Number
        { get; private set; }
        public BattleUnitsStack(UnitsStack start)
        {
            FirstHeroHP = start.Class.HitPoints;
            StartingNumber = start.Number;
            Number = start.Number;
            Class = new BattleUnit(start.Class);
        }
        public int FirstHeroHP
        { get; private set; }
        public bool Countered
        { get; set; }
        public bool Acted
        { get; set; }
        public bool IsRed
        { get; set; }
        public int SummaryHP
        {
            get
            {
                return FirstHeroHP + Class.HitPoints * (Number - 1);
            }
        }
        public string Print
        { get
            {
                string result = "Type:" + Class.Type + " HP:" + SummaryHP.ToString() + " Side:";
                if (IsRed)
                    result += "Red";
                else
                    result += "Blue";
                return result;
            }
        }
        public int StartingNumber
        { get; }
        public bool IsAlive
        {
            get
            {
                return (Number > 0);
            }
        }
        public void ReceiveDamage(int Dmg)
        {
            if (Dmg >= Number * Class.HitPoints + FirstHeroHP)
            {
                Number = 0;
                FirstHeroHP = 0;
                return;
            }
            Number -= Dmg / Class.HitPoints;
            FirstHeroHP -= Dmg % Class.HitPoints;
            if (FirstHeroHP <= 0)
            {
                FirstHeroHP += Class.HitPoints;
                Number--;
            }
        }
        public void Reviving(int heal)
        {
            Number += heal / Class.HitPoints;
            FirstHeroHP += heal % Class.HitPoints;
            if (FirstHeroHP > Class.HitPoints)
            {
                FirstHeroHP -= Class.HitPoints;
                Number++;
            }
            if (Number > StartingNumber)
            {
                Number = StartingNumber;
                FirstHeroHP = Class.HitPoints;
            }
        }
        public void Healing(int heal)
        {
            if (heal > Class.HitPoints - FirstHeroHP)
                Reviving(Class.HitPoints - FirstHeroHP);
            else
                Reviving(heal);
        }
        public void ReceiveEffect()
        {

        }
        public int CompareTo(BattleUnitsStack stack)
        {
            return Class.Initiative.CompareTo(stack.Class.Initiative);
        }
    }
}
