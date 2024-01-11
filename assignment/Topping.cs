using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Topping
    {
        private string type;
        public string Type
        {
            get => type;
            set => type = value;
        }
        public Topping() { }
        public Topping(string t)
        {
            Type = t;
        }
        public override string ToString()
        {
            return $"Topping: {Type}";
        }
    }
}
