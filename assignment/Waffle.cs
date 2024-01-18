using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    public class Waffle : IceCream
    {
        public string waffleFlavour;

        public Waffle() { }

        public Waffle(string option, int scoop, List<string> flavours, List<string> toppings, string waffleFlavour)
            : base(option, scoop, flavours, toppings)
        {
            this.waffleFlavour = waffleFlavour;
        }

        public override double CalculatePrice()
        {
            if(waffleFlavour == "original")
            {
                return base.CalculatePrice();
            }
            else
            {
                return base.CalculatePrice() + 3.0;
            }
            
        }

        public override string ToString()
        {
            return $"Waffle ({waffleFlavour}): {base.ToString()}";
        }
    }
}
