using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Flavour
    {
        private string type;
        public string Type
        {
            get => type; set => type = value;
        }
        private bool premium;
        public bool Premium
        {
            get => premium; set => premium = value;
        }
        private int quantity;
        public int Quantity
        {
            get => quantity; set => quantity = value;
        }
        public Flavour() { }
        public Flavour(string t, bool p, int q)
        {
            Type = t;
            Premium = p;
            Quantity = q;
        }
        public override string ToString()
        {
            return $"Flavour: {Type}, Premium?: {Premium}, Quantity: {Quantity}";
        }
    }
}
