﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    public class Cone : IceCream
    {
        public bool Dipped;

        public Cone() { }


        public Cone(string option, int scoop, List<string> flavours, List<string> toppings, bool dipped)
            : base(option, scoop, flavours, toppings)
        {
            this.Dipped = dipped;
        }

        public override double CalculatePrice()
        {
            double basePrice = 2.5;
            double scoopPrice = 1.5 * Scoops;
            double premiumFlavours = 2 * Scoops;
            double toppingPrice = Toppings * 1;

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
            return $"Cone: {base.ToString()}";
        }
    }
}
