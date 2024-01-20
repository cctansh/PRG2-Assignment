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
            // Base price for Cone
            double basePrice = 4.00;

            // Additional cost for scoops
            double scoopPrice = Scoops switch
            {
                1 => 4.00,
                2 => 5.50,
                3 => 6.50,
            };

            // Additional cost for premium flavors
            double premiumFlavorPrice = Flavours.Count(flavour => flavour.Type == "Durian" || flavour.Type == "Ube" || flavour.Type == "Sea Salt") * 2.0;

            // Additional cost for each topping
            double toppingsPrice = Toppings.Count * 1.0;

            double totalPrice = basePrice + scoopPrice + premiumFlavorPrice + toppingsPrice;

            return totalPrice;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
