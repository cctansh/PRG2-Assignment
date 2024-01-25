using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Cone : IceCream
    {
        public bool Dipped;

        public Cone() { }


        public Cone(int s, List<Flavour> f, List<Topping> t, bool d)
            : base("Cone", s, f, t)
        {
            Dipped = d;
        }

        public override double CalculatePrice()
        {
            // Additional cost for scoops
            double scoopPrice = Scoops switch
            {
                1 => 4.00,
                2 => 5.50,
                3 => 6.50,
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

            // Additional cost for dipped cone
            double dippedPrice;
            if (Dipped)
            {
                dippedPrice = 2.0;
            }
            else
            {
                dippedPrice = 0.0;
            }

            double totalPrice = scoopPrice + premiumFlavorPrice + toppingsPrice + dippedPrice;

            return totalPrice;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nDipped?: {Dipped}";
        }
    }
}
