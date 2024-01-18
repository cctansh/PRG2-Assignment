using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Cup : IceCream
    {
        public Cup() { }

        public Cup(int s, List<Flavour> f, List<Topping> t) : base("Cup", s, f, t)
        {
        }

        public override double CalculatePrice()
        {
            return base.CalculatePrice();
        }

        public override string ToString()
        {
            return $"Cup: {base.ToString()}";
        }
    }
}
