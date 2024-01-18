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
            double premiumFlavorPrice = Flavours.Count(f => f.ToLower() == "durian" || f.ToLower() == "ube" || f.ToLower() == "sea salt") * 2.0;

            // Additional cost for each topping
            double toppingsPrice = Toppings.Count * 1.0;

            // Additional cost for waffle flavor
            double waffleFlavorPrice = PremiumWaffleFlavor(WaffleFlavour) ? 3.0 : 0.0;

            return basePrice + scoopPrice + premiumFlavorPrice + toppingsPrice + waffleFlavorPrice;
        }
        private bool PremiumWaffleFlavor(string flavor)
        {
            // Check if the flavor is a special waffle flavor
            return flavor.ToLower() == "red velvet" || flavor.ToLower() == "charcoal" || flavor.ToLower() == "pandan";
        }

        public override string ToString()
        {
            double totalPrice = CalculatePrice();
            return $"Waffle ({WaffleFlavour}): {base.ToString()}, Total Price: {totalPrice:C2}";
        }

    }
}
