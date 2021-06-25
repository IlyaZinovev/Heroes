using System;
using System.Collections.Generic;

namespace Heroes
{
    public class Battle
    {
        public BattleArmy red
        { get; }
        public BattleArmy blue
        { get; }
        private List<BattleUnitsStack> Queue;
        private List<BattleUnitsStack> Waiting;
        private List<BattleUnitsStack> Next;
        public List<BattleUnitsStack> ShowQueuePartly
        {
            get
            {
                List<BattleUnitsStack> result = new List<BattleUnitsStack>();
                foreach (BattleUnitsStack i in Queue)
                {
                    if (i.IsAlive)
                        result.Add(i);
                }
                return result;
            }
        }
        public List<BattleUnitsStack> ShowQueue
        {
            get
            {
                List<BattleUnitsStack> result = new List<BattleUnitsStack>();
                foreach (BattleUnitsStack i in Queue)
                {
                    if (i.IsAlive)
                        result.Add(i);
                }
                for (int i = Waiting.Count - 1; i >= 0; i--)
                {
                    if (Waiting[i].IsAlive)
                        result.Add(Waiting[i]);
                }
                return result;
            }
        }
        public string PrintQueue()
        {
            string result = "";
            foreach (BattleUnitsStack stack in ShowQueue)
                result += stack.Class.Type += " ";
            return result;
        }
        public bool IsWaiting
        { get; private set; }
        public Battle(BattleArmy a, BattleArmy b)
        {
            red = a;
            blue = b;
            Current = 0;
            IsWaiting = false;
            Queue = new List<BattleUnitsStack>();
            Waiting = new List<BattleUnitsStack>();
            Next = new List<BattleUnitsStack>();
            foreach (BattleUnitsStack stack in red.Show)
            {
                stack.Countered = false;
                stack.Acted = false;
                stack.IsRed = true;
                Queue.Add(stack);
                Next.Add(stack);
            }
            foreach (BattleUnitsStack stack in blue.Show)
            {
                stack.Countered = false;
                stack.Acted = false;
                stack.IsRed = false;
                Queue.Add(stack);
                Next.Add(stack);
            }
            Queue.Sort();
            Next.Sort();
        }
        public void Refresh()
        {
            BattleUnitsStack stack1 = this.CurrentStack;
            Queue = new List<BattleUnitsStack>();
            Next = new List<BattleUnitsStack>();
            foreach (BattleUnitsStack stack in red.Show)
            {
                Queue.Add(stack);
                Next.Add(stack);
            }
            foreach (BattleUnitsStack stack in blue.Show)
            {
                Queue.Add(stack);
                Next.Add(stack);
            }
            Queue.Sort();
            Next.Sort();
        }
        public void AddWaiting(BattleUnitsStack stack)
        {
            Waiting.Add(stack);
        }
        public bool IsEnded
        {
            get
            {
                red.CheckAlive();
                blue.CheckAlive();
                return !red.IsAlive || !blue.IsAlive;
            }
        }
        public string WhoWinner()
        {
            if (red.IsAlive)
            {
                if (blue.IsAlive)
                    return "Battle isn't over";
                return "Red side wins!";
            }
            else
            {
                if (blue.IsAlive)
                    return "Blue side wins!";
                return "Draw!";
            }
        }
        public int Current
        { get; private set; }
        public string PrintCurrent
        { get
            {
                if (IsWaiting)
                    return Waiting[Current].Print;
                return Queue[Current].Print;
            }
        }
        public BattleUnitsStack CurrentStack
        {
            get
            {
                if (IsWaiting)
                    return Waiting[Current];
                return Queue[Current];
            }
        }
        public void NextQueue(ActionType action, Target target)
        {
            if (IsEnded)
                return;
            Action a = new Action(action, Queue[Current], target, this);
            a.Execute();
            Current++;
            Current %= Queue.Count;
            if (Current == 0)
            {
                if (Waiting.Count > 0)
                {
                    Current = Waiting.Count - 1;
                    IsWaiting = true;
                }
                else
                {
                    for (int i = 0; i < Queue.Count; ++i)
                    {
                        Queue[i].Acted = false;
                        Queue[i].Countered = false;
                    }
                }
            }
        }
        public void NextWaiting(ActionType action, Target target)
        {
            if (IsEnded)
                return;
            if (action == ActionType.Wait)
                throw new Exception();
            Action a = new Action(action, Waiting[Current], target, this);
            a.Execute();
            Current--;
            if (Current < 0)
            {
                Current = 0;
                Waiting = new List<BattleUnitsStack>();
                IsWaiting = false;
                for (int i = 0; i < Queue.Count; ++i)
                {
                    Queue[i].Acted = false;
                    Queue[i].Countered = false;
                }
            }
        }
        public void NextAction(ActionType action, Target target)
        {
            if (IsWaiting)
                NextWaiting(action, target);
            else
                NextQueue(action, target);
            if (IsEnded)
                return;
            if (!CurrentStack.IsAlive)
            {
                NextAction(ActionType.DoNothing, new Target(TargetType.Nobody));
            }
        }
    }
}
