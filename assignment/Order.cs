using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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
            List<Flavour> flavours = new List<Flavour>();
            List<Topping> toppings = new List<Topping>();
            IceCream currentIC = iceCreamList[c - 1];

            while (true)
            {
                Console.WriteLine("[1] Option");
                Console.WriteLine("[2] Scoops");
                Console.WriteLine("[3] Flavours");
                Console.WriteLine("[4] Toppings");
                Console.WriteLine("[0] Exit");
                Console.Write("What would you like to modify?");
                string option = Console.ReadLine();
                Console.WriteLine();

                if (option == "1")
                {
                    currentIC = ChangeOption(currentIC);
                }
                else if (option == "2")
                {
                    int s = Program.ChooseScoops();
                    currentIC.Scoops = s;
                }
                else if (option == "3")
                {
                    flavours = Program.ChooseFlavours(currentIC.Scoops);
                    currentIC.Flavours = flavours;
                    flavours.Clear();
                }
                else if (option == "4")
                {
                    toppings = Program.ChooseToppings();
                    currentIC.Toppings = toppings;
                    toppings.Clear();
                }
                else if (option == "0")
                {
                    Console.WriteLine("Modifications finalised.");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter 1, 2, 3, 4, or 0.");
                    continue;
                }
                iceCreamList[c - 1] = currentIC;
                Console.WriteLine("Modified Ice Cream: " + currentIC.ToString());
                Console.WriteLine();
            }
            
            IceCream ChangeOption(IceCream cIC)
            {
                Console.WriteLine("Options:\n[1] Cup\n[2] Cone\n[3] Waffle\n[0] Back");
                while (true)
                {
                    Console.Write("Please choose your ice cream option: ");
                    string o = Console.ReadLine();
                    if (o == "1")
                    {
                        if (cIC is Cup)
                        {
                            Console.WriteLine("Your ice cream is already in a cup!");
                            return cIC;
                        }
                        else
                        {
                            return new Cup(cIC.Scoops, cIC.Flavours, cIC.Toppings);
                        }
                    }
                    else if (o == "2")
                    {
                        if (cIC is Cone)
                        {
                            Console.WriteLine("Your ice cream is already in a cone.");
                        }

                        while (true)
                        {
                            Console.Write("Would you like a dipped cone? (Y/N): ");
                            string oD = Console.ReadLine();
                            if (oD.ToUpper() == "Y")
                            {
                                return new Cone(cIC.Scoops, cIC.Flavours, cIC.Toppings, true);
                            }
                            else if (oD.ToUpper() == "N")
                            {
                                return new Cone(cIC.Scoops, cIC.Flavours, cIC.Toppings, false);
                            }
                            else
                            {
                                Console.WriteLine("Invalid option. Please enter Y or N.");
                            }
                        }
                    }
                    else if (o == "3")
                    {
                        if (cIC is Waffle)
                        {
                            Console.WriteLine("Your ice cream is already in a waffle.");
                        }
                        Console.WriteLine("Waffle Flavours:\n[1] Original\n[2] Red Velvet\n[3] Charcoal\n[4] Pandan");
                        
                        while (true)
                        {
                            Console.Write("What flavour would you like?: "); 
                            string oF = Console.ReadLine();
                            if (oF == "1")
                            {
                                return new Waffle(cIC.Scoops, cIC.Flavours, cIC.Toppings, "Original");
                            }
                            else if (oF == "2")
                            {
                                return new Waffle(cIC.Scoops, cIC.Flavours, cIC.Toppings, "Red Velvet");
                            }
                            else if (oF == "3")
                            {
                                return new Waffle(cIC.Scoops, cIC.Flavours, cIC.Toppings, "Charcoal");
                            }
                            else if (oF == "4")
                            {
                                return new Waffle(cIC.Scoops, cIC.Flavours, cIC.Toppings, "Pandan");
                            }
                            else
                            {
                                Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 0.");
                    }
                }
            }
        }
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }
        public void DeleteIceCream(int c)
        {
            if (IceCreamList.Count == 1)
            {
                Console.WriteLine("Cannot have 0 ice creams in the order.");
                return;
            }

            IceCreamList.RemoveAt(c - 1);
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
            if (TimeFulfilled != null)
            {
                return $"ID: {Id}, Time Received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}, Ice Cream List:\n{ic}";
            }
            else
            {
                return $"ID: {Id}, Time Received: {TimeReceived}, Time Fulfilled: Unfulfilled, Ice Cream List:\n{ic}";
            }
        }
    }
}
