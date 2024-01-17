using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    public class Cone : IceCream
    {
        public bool dipped;

        public Cone() { }


        public Cone(string option, int scoop, List<string> flavours, List<string> toppings, bool dipped)
            : base(option, scoop, flavours, toppings)
        {
            this.dipped = dipped;
        }

        public override double CalculatePrice()
        {
            if (dipped == true)
            {
                return base.CalculatePrice() + 2.0;
            }
            else
            {
                return base.CalculatePrice();
            }
        }

        public override string ToString()
        {
            return $"Cone{(dipped ? " (Dipped)" : "")}: {base.ToString()}";
        }
    }
}
