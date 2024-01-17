using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    public class Cup : IceCream
    {
        public Cup() { }

        public Cup(string option, int scoop, List<string> flavours, List<string> toppings)
            : base(option, scoop, flavours, toppings)
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
