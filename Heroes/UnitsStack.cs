using System;

namespace Heroes
{
    public class UnitsStack
    {
        public Unit Class
        { get; private set; }
        public int Number
        { get; private set; }
        public UnitsStack (Unit Class_, int Number_)
        {
            Class = Class_;
            if (Number_ > 999999)
                Number_ = 999999;
            if (Number_ < 1)
                Number_ = 1;
            Number = Number_;
        }
    }
}
