using System;
using System.Collections.Generic;

namespace Heroes
{
    public class Army
    {
        private List<UnitsStack> Legion;
        public List<UnitsStack> Show
        {
            get
            {
                List<UnitsStack> result = new List<UnitsStack>();
                foreach (UnitsStack i in Legion)
                {
                    result.Add(i);
                }
                return result;
            }
        }
        public Army(List<UnitsStack> Legion_)
        {
            if (Legion_.Count > 6)
                throw new Exception();
            Legion = Legion_;
        }
        public Army(UnitsStack Units)
        {
            Legion = new List<UnitsStack> {Units};
        }
        public void Add(UnitsStack Units)
        {
            if (Legion.Count == 6)
                throw new Exception();
            Legion.Add(Units);
        }
        public void Delete(int index)
        {
            if (Legion.Count == 0)
                throw new Exception();
            Legion.RemoveAt(index);
        }
    }
}
