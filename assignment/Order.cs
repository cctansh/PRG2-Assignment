using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Order
    {
        private int id;
        public int Id
        {
            get => id; set => id = value;
        }
        private DateTime timeReceived;
        public DateTime TimeReceived
        {
            get => timeReceived; set => timeReceived = value;
        }
        private DateTime? timeFulfilled;
        public DateTime? TimeFulfilled
        {
            get => timeFulfilled; set => timeFulfilled = value;
        }
        private List<IceCream> iceCreamList;
        public List<IceCream> IceCreamList
        {
            get => iceCreamList; set => iceCreamList = value;
        }
        public Order() { }
        public Order(int i, DateTime r)
        {
            Id = i;
            TimeReceived = r;
            TimeFulfilled = null;
            IceCreamList = new List<IceCream>();
        }
        public void ModifyIceCream(int c)
        {
            c -= 1;
            
            //to do: data validation

            Console.WriteLine("Would you like cup, cone, or waffle?: ");
            string o = Console.ReadLine();
            Console.WriteLine("How many scoops?: ");
            int s = int.Parse(Console.ReadLine());
            Console.WriteLine("What flavour(s) would you like?: ");
            // do things
            Console.WriteLine("What topping(s) would you like?: ");
            // do things
            if (o == "Cone")
            {
                Console.WriteLine("Would you like a dipped cone? (Y/N): ");
                // do things
            }
            else if (o == "Waffle")
            {
                Console.WriteLine("What flavour of waffle would you like?: ");
                // do things
            }
            IceCreamList[c].Option = o;
            IceCreamList[c].Scoops = s;
            //IceCreamList[c].Flavours =
            //IceCreamList[c].Toppings =
            // if else cone/waffle
        }
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }
        public void DeleteIceCream(int c)
        {
            c -= 1;

            if (IceCreamList.Count == 1)
            {
                Console.WriteLine("Cannot have 0 ice creams in the order.");
                return;
            }

            IceCreamList.RemoveAt(c);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var iceCream in IceCreamList)
            {
                total += iceCream.CalculatePrice();
            }
            return total;
        }

        public override string ToString()
        {
            string ic = "";
            foreach (var item in IceCreamList)
            {
                ic += item.ToString() + "\n";
            }
            return $"ID: {Id}, Time Received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}, Ice Cream List:\n{ic}";
        }
    }
}
