using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal abstract class IceCream
    {
        private string option;
        public string Option
        {
            get => option; set => option = value;
        }
        private int scoops;
        public int Scoops
        {
            get => scoops; set => scoops = value;
        }
        private List<Flavour> flavours;
        public List<Flavour> Flavours
        {
            get => flavours; set => flavours = value;
        }
        private List<Topping> toppings;
        public List<Topping> Toppings
        {
            get => toppings; set => toppings = value;
        }
        public IceCream() { }
        public IceCream(string o, int s, List<Flavour> f, List<Topping> t)
        {
            Option = o;
            Scoops = s;
            Flavours = f;
            Toppings = t;
        }
        public abstract double CalculatePrice();
        public override string ToString()
        {
            string f = "";
            foreach (var item in Flavours)
            {
                f += item.Type + " ";
            }
            f = f.Remove(f.Length - 1);
            string t = "";
            if (Toppings.Count > 0)
            {
                foreach (var item in Toppings)
                {
                    t += item.Type + " ";
                }
                t = t.Remove(t.Length - 1);
                return $"Option: {Option}\nScoops: {Scoops}\nFlavours: {f}\nToppings: {t}";
            }
            else
            {
                return $"Option: {Option}\nScoops: {Scoops}\nFlavours: {f}\nToppings: None";
            }
        }
    }
}
