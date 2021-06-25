using System;

namespace Heroes
{
    public enum ActionType
    {
        Attack,
        CounterAttack,
        Cast,
        Wait,
        Defend,
        GiveUp,
        DoNothing
    }
    public class Action
    {
        public BattleUnitsStack Owner
        { get; private set; }
        public ActionType Type
        { get; private set; }
        public Battle Battle
        { get; private set; }
        public Target Target
        { get; set; }
        public bool CounterFlag
        { get; set; }
        public Action(ActionType type, BattleUnitsStack stack, Target target, Battle battle)
        {
            Type = type;
            Owner = stack;
            Battle = battle;
            Target = target;
            CounterFlag = true;
            if (Type == ActionType.Attack && target.Stack.Countered)
                CounterFlag = false;
        }
        public void Execute()
        { 
            switch (Type)
            {
                case ActionType.Attack:
                    {
                        CounterFlag = !Target.Stack.Countered;
                        foreach (Modificator mod in Target.Stack.Class.ShowAbylities)
                            if (mod.Type == ModificatorType.BeingAttacked)
                            {
                                mod.Owner = Target.Stack;
                                mod.Do(this);
                            }
                        foreach (Modificator mod in Owner.Class.ShowAbylities)
                            if (mod.Type == ModificatorType.Attacking)
                            {
                                mod.Owner = Owner;
                                mod.Do(this);
                            }
                        Attack(Target);
                        if (CounterFlag)
                        {
                            Action a = new Action(ActionType.CounterAttack, Target.Stack, new Target(TargetType.Stack, Owner), Battle);
                            a.Execute();
                            Target.Stack.Countered = true;
                        }
                        break;
                    }
                case ActionType.CounterAttack:
                    {
                        Attack(Target);
                        break;
                    }
                case ActionType.Defend:
                    {
                        foreach (Modificator mod in Owner.Class.ShowAbylities)
                            if (mod.Type == ModificatorType.Defending)
                            {
                                mod.Owner = Owner;
                                mod.Do(this);
                            }
                        Defend();
                        break;
                    }
                case ActionType.GiveUp:
                    {
                        GiveUp(Target.Army);
                        break;
                    }
                case ActionType.Wait:
                    {
                        foreach (Modificator mod in Owner.Class.ShowAbylities)
                            if (mod.Type == ModificatorType.Waiting)
                            {
                                mod.Owner = Owner;
                                mod.Do(this);
                            }
                        Wait();
                        break;
                    }
                case ActionType.Cast:
                    {
                        break;
                    }
                case ActionType.DoNothing:
                    {
                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
        }
        public void SingleAttack(BattleUnitsStack Enemy)
        {
            int DamageMin, DamageMax;
            if (Owner.Class.Attack > Enemy.Class.Defense)
            {
                DamageMin = (int)(Owner.Number * Owner.Class.Damage.Item1 * (1 + 0.05 * (Owner.Class.Attack - Enemy.Class.Defense)));
                DamageMax = (int)(Owner.Number * Owner.Class.Damage.Item2 * (1 + 0.05 * (Owner.Class.Attack - Enemy.Class.Defense)));
            }
            else
            {
                DamageMin = (int)(Owner.Number * Owner.Class.Damage.Item1 / (1 + 0.05 * (Enemy.Class.Defense - Owner.Class.Attack)));
                DamageMax = (int)(Owner.Number * Owner.Class.Damage.Item2 / (1 + 0.05 * (Enemy.Class.Defense - Owner.Class.Attack)));
            }
            Random rnd = new Random();
            int Damage = rnd.Next(DamageMin, DamageMax + 1);
            Enemy.ReceiveDamage(Damage);
        }
        public void Attack(Target Enemy)
        {
            switch (Enemy.Type)
            {
                case TargetType.Stack:
                    {
                        SingleAttack(Enemy.Stack);
                        break;
                    }
                case TargetType.Army:
                    {
                        for (int i = 0; i < Enemy.Army.Legion.Count; i++)
                            SingleAttack(Enemy.Army.Legion[i]);
                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
        }
        public void CounterAttack(BattleUnitsStack Enemy)
        {
            Action a = new Action(ActionType.Attack, Enemy, new Target(TargetType.Stack, Owner), Battle);
            a.Execute();
        }
        public void Defend()
        {
            Owner.Class.Defense = (int)(Owner.Class.Defense * 1.3);
        }
        public void Wait()
        {
            Battle.AddWaiting(Owner);
        }
        public void GiveUp(BattleArmy Side)
        {
            Side.KillArmy();
        }

    }
}
