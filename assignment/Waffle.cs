using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Waffle : IceCream
    {
        public string WaffleFlavour;

        public Waffle() { }

        public Waffle(int s, List<Flavour> f, List<Topping> t, string waffleFlavour)
            : base("Waffle", s, f, t)
        {
            WaffleFlavour = waffleFlavour;
        }

        public override double CalculatePrice()
        {
            // Additional cost for scoops
            double scoopPrice = Scoops switch
            {
                1 => 7.00,
                2 => 8.50,
                3 => 9.50,
            };

            // Additional cost for premium flavors
            double premiumFlavorPrice = 0;
            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Type == "Durian" || flavour.Type == "Ube" || flavour.Type == "Sea Salt")
                {
                    premiumFlavorPrice += 2 * flavour.Quantity;
                }
            }

            // Additional cost for each topping
            double toppingsPrice = Toppings.Count * 1.0;

            // Additional cost for waffle flavor
            double PremiumWaffle = 0;
            if (WaffleFlavour != "Original")
            {
                PremiumWaffle = 3.0;
            }

            double totalPrice = scoopPrice + premiumFlavorPrice + toppingsPrice + PremiumWaffle;
            return totalPrice;
        }
        
        public override string ToString()
        {
            return base.ToString() + $"\nWaffle Flavour: {WaffleFlavour}";
        }

    }
}
