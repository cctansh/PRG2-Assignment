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
            this.WaffleFlavour = waffleFlavour;
        }

        public override double CalculatePrice()
        {
            // Base price for Waffle
            double basePrice = 7.00;

            // Additional cost for scoops
            double scoopPrice = Scoops switch
            {
                1 => 7.00,
                2 => 8.50,
                3 => 9.50,
            };

            // Additional cost for premium flavors
            double premiumFlavor = Flavours.Count(flavour => flavour.Type == "Durian" || flavour.Type == "Ube" || flavour.Type == "Sea Salt") * 2.0;

            // Additional cost for each topping
            double toppingsPrice = Toppings.Count * 1.0;

            // Additional cost for waffle flavor
            double PremiumWaffle;
            if (WaffleFlavour == "Red Velvet" || WaffleFlavour == "Charcoal" || WaffleFlavour == "Pandan")
            {
                PremiumWaffle = 3.0;
            }
            else
            {
                PremiumWaffle = 0.0;
            }
            double totalPrice = basePrice + scoopPrice + premiumFlavor + toppingsPrice + PremiumWaffle;
            return totalPrice;
        }
        
        public override string ToString()
        {
            double totalPrice = CalculatePrice();
            return $"Waffle ({WaffleFlavour}), Total Price: {totalPrice:C2}";
        }

    }
}
