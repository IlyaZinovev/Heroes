using System;
using System.Collections.Generic;
using System.Reflection;


namespace Heroes
{
    class Program
    {
        public static void Main()
        {
            Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName("Heroes.dll"));
            Type[] types = assembly.GetTypes();
            Type[] mode_types = { };

            Console.WriteLine("Hello!");
            Console.WriteLine("Do you want to download modes? y|n");
            if (Console.ReadLine() == "y")
            {
                assembly = Assembly.Load(AssemblyName.GetAssemblyName("Modes.dll"));
                mode_types = assembly.GetTypes();
            }
            List<Type> all_units = new List<Type>();
            foreach (Type t in types)
                if (t.IsSubclassOf(typeof(Unit)))
                    all_units.Add(t);
            foreach (Type t in mode_types)
                if (t.IsSubclassOf(typeof(Unit)))
                    all_units.Add(t);

            Console.WriteLine("All units and their codes");
            for (int i = 0; i < all_units.Count; ++i)
                Console.WriteLine(i.ToString() + " " + all_units[i].Name);

            Console.WriteLine("Start building armies!");
            Console.WriteLine("Red side");

            List<UnitsStack> RedList = new List<UnitsStack>();
            string s = "";
            int code, number;
            while (s != "q")
            {
                Console.WriteLine("Enter a unit code and number to add new stack or press q to exit");
                s = Console.ReadLine();
                string[] splited = s.Split();
                if (splited.Length == 2 && Int32.TryParse(splited[0], out code) && Int32.TryParse(splited[1], out number))
                {
                    Unit pers = (Unit)Activator.CreateInstance(all_units[code]);
                    RedList.Add(new UnitsStack(pers, number));
                }
                else if (s == "q")
                    break;
                else
                {
                    throw new Exception();
                }
            }

            Console.WriteLine("Blue side");

            List<UnitsStack> BlueList = new List<UnitsStack>();
            s = "";
            while (s != "q")
            {
                Console.WriteLine("Enter a unit code and number to add new stack or press q to exit");
                s = Console.ReadLine();
                string[] splited = s.Split();
                if (splited.Length == 2 && Int32.TryParse(splited[0], out code) && Int32.TryParse(splited[1], out number))
                {
                    Unit pers = (Unit)Activator.CreateInstance(all_units[code]);
                    BlueList.Add(new UnitsStack(pers, number));
                }
                else if (s == "q")
                    break;
                else
                {
                    throw new Exception();
                }
            }



            
            Army Red_Army = new Army(RedList);
            Army Blue_Army = new Army(BlueList);
            BattleArmy Red_Battle_Army = new BattleArmy(Red_Army);
            BattleArmy Blue_Battle_Army = new BattleArmy(Blue_Army);
            Battle fight = new Battle(Red_Battle_Army, Blue_Battle_Army);
            string k;
            Console.WriteLine("Fight begins!");
            while (!fight.IsEnded)
            {
                fight.Refresh();
                Console.WriteLine(fight.PrintCurrent);
                k = Console.ReadLine();
                switch (k)
                {
                    case "a":
                        {
                            Console.WriteLine("Choose target");
                            List<BattleUnitsStack> targets = new List<BattleUnitsStack>();
                            foreach (BattleUnitsStack stack in fight.ShowQueuePartly)
                                if (fight.CurrentStack.IsRed != stack.IsRed)
                                    targets.Add(stack);
                            for (int i = 0; i < targets.Count; ++i)
                                Console.WriteLine(i.ToString() + " " + targets[i].Class.Type);
                            if (!Int32.TryParse(Console.ReadLine(), out int a))
                                throw new Exception();
                            int HPbefore = targets[a].SummaryHP;
                            fight.NextAction(ActionType.Attack, new Target(TargetType.Stack, targets[a]));
                            int HPafter = targets[a].SummaryHP;
                            Console.WriteLine("Dealt " + (HPbefore - HPafter).ToString() + " Damage");
                            break;
                        }
                    case "d":
                        {
                            fight.NextAction(ActionType.Defend, new Target(TargetType.Nobody));
                            break;
                        }
                    case "gu":
                        {
                            if (fight.CurrentStack.IsRed)
                                fight.NextAction(ActionType.GiveUp, new Target(TargetType.Army, fight.red));
                            else
                                fight.NextAction(ActionType.GiveUp, new Target(TargetType.Army, fight.blue));
                            break;
                        }
                    case "w":
                        {
                            fight.NextAction(ActionType.Wait, new Target(TargetType.Nobody));
                            break;
                        }
                    case "c":
                        {
                            if (fight.CurrentStack.Class.ShowSorceries.Count == 0)
                                Console.WriteLine("This unit can't cast sorceries");
                            else
                            {
                                Console.WriteLine("Choose sorcery");
                                for (int i = 0; i < fight.CurrentStack.Class.ShowSorceries.Count; ++i)
                                    Console.WriteLine(i.ToString() + " " + fight.CurrentStack.Class.ShowSorceries[i].Name);
                                if (!Int32.TryParse(Console.ReadLine(), out int a))
                                    throw new Exception();
                                Console.WriteLine("Choose target");
                                for (int i = 0; i < fight.ShowQueuePartly.Count; ++i)
                                    Console.WriteLine(i.ToString() + " " + fight.ShowQueuePartly[i].Class.Type);
                                if (!Int32.TryParse(Console.ReadLine(), out int b))
                                    throw new Exception();
                                Sorcery sorcery1 = fight.CurrentStack.Class.ShowSorceries[a];
                                sorcery1.Cast(new Target(TargetType.Stack, fight.ShowQueuePartly[b]), fight.CurrentStack);
                                fight.NextAction(ActionType.Cast, new Target(TargetType.Stack, fight.ShowQueuePartly[b]));
                            }
                            break;
                        }
                    case "sq":
                        {
                            Console.WriteLine(fight.PrintQueue());
                            break;
                        }
                    default:
                        throw new Exception();
                }
            }
            Console.WriteLine(fight.WhoWinner());
            Console.WriteLine("Results");
            Console.WriteLine("Red's losses");
            foreach (BattleUnitsStack stack in fight.ShowQueuePartly)
                if (stack.IsRed)
                    Console.WriteLine(stack.Class.Type + " " + (stack.StartingNumber - stack.Number).ToString() + " died");
            Console.WriteLine();
            Console.WriteLine("Blue's losses");
            foreach (BattleUnitsStack stack in fight.ShowQueuePartly)
                if (!stack.IsRed)
                    Console.WriteLine(stack.Class.Type + " " + (stack.StartingNumber - stack.Number).ToString() + " died");
        }
    }
}
