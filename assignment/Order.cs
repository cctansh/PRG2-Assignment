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
        private List<Order> orderHistory;
        public List<Order> OrderHistory
        {
            get => orderHistory ?? (orderHistory = new List<Order>());
            set => orderHistory = value;
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
            //assigning ice cream
            IceCream currentIC = iceCreamList[c - 1];

            while (true)
            {
                // display modify ice cream menu
                Console.WriteLine("-------------------");
                Console.WriteLine("Modifying Ice Cream");
                Console.WriteLine("-------------------");
                Console.WriteLine("[1] Option");
                Console.WriteLine("[2] Scoops");
                Console.WriteLine("[3] Flavours");
                Console.WriteLine("[4] Toppings");
                Console.WriteLine("[0] Exit");
                Console.WriteLine();

                // user input
                Console.Write("What would you like to modify?: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                if (option == "1")
                {
                    currentIC = ChangeOption(currentIC); // update ice cream
                    Console.WriteLine();
                }
                else if (option == "2")
                {
                    int s = Program.ChooseScoops();
                    currentIC.Scoops = s; // update ice cream

                    Console.WriteLine("Now selecting flavours to correspond to your new number of scoops...\n");

                    List<Flavour> flavours = Program.ChooseFlavours(currentIC.Scoops);
                    currentIC.Flavours = flavours; // update ice cream
                }
                else if (option == "3")
                {
                    List<Flavour> flavours = Program.ChooseFlavours(currentIC.Scoops);
                    currentIC.Flavours = flavours; // update ice cream
                }
                else if (option == "4")
                {
                    List<Topping> toppings = Program.ChooseToppings();
                    currentIC.Toppings = toppings; // update ice cream
                }
                else if (option == "0") // exit
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Modifications finalised.");
                    Console.WriteLine("------------------------");

                    return;
                }
                else // invalid option
                {
                    Console.WriteLine("Invalid option. Please enter 1, 2, 3, 4, or 0.");
                    Console.WriteLine();
                    continue;
                }
                iceCreamList[c - 1] = currentIC; // update actual ice cream in the list

                //display current state of ice cream
                // header
                Console.WriteLine("------------------");
                Console.WriteLine("Modified Ice Cream");
                Console.WriteLine("------------------");
                // ice cream
                Console.WriteLine(currentIC.ToString());
                Console.WriteLine();
            }
            
            IceCream ChangeOption(IceCream cIC)
            {
                // display ice cream options
                Console.WriteLine("-----------------");
                Console.WriteLine("Ice Cream Options");
                Console.WriteLine("-----------------");
                Console.WriteLine("[1] Cup\n[2] Cone\n[3] Waffle");
                Console.WriteLine();

                while (true)
                {
                    // user input
                    Console.Write("Please choose your ice cream option: ");
                    string o = Console.ReadLine();

                    if (o == "1") // cup
                    {
                        if (cIC is Cup)
                        {
                            Console.WriteLine("\nYour ice cream is already in a cup!");
                            return cIC; // no changes, return same ice cream
                        }
                        else
                        {
                            return new Cup(cIC.Scoops, cIC.Flavours, cIC.Toppings); 
                        }
                    }
                    else if (o == "2") // cone
                    {
                        Console.WriteLine();

                        if (cIC is Cone) // alerting user, then continue method
                        {
                            Console.WriteLine("Your ice cream is already in a cone."); 
                            Console.WriteLine();
                        }

                        while (true)
                        {
                            // check dipped cone
                            Console.Write("Would you like a dipped cone? (Y/N): ");
                            string oD = Console.ReadLine();

                            if (oD.ToUpper() == "Y") // yes
                            {
                                return new Cone(cIC.Scoops, cIC.Flavours, cIC.Toppings, true);
                            }
                            else if (oD.ToUpper() == "N") // no
                            {
                                return new Cone(cIC.Scoops, cIC.Flavours, cIC.Toppings, false);
                            }
                            else // invalid, repeat asking for dipped cone
                            {
                                Console.WriteLine("\nInvalid option. Please enter Y or N.\n");
                            }
                        }
                    }
                    else if (o == "3") // waffle
                    {
                        Console.WriteLine();

                        if (cIC is Waffle) // alerting user
                        {
                            Console.WriteLine("Your ice cream is already in a waffle.");
                            Console.WriteLine();
                        }

                        //display waffle flavours
                        Console.WriteLine("---------------");
                        Console.WriteLine("Waffle Flavours");
                        Console.WriteLine("---------------");
                        Console.WriteLine("[1] Original\n[2] Red Velvet\n[3] Charcoal\n[4] Pandan");
                        Console.WriteLine();
                        
                        while (true)
                        {
                            // user input
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
                            else // invalid option, repeat asking for waffle flavour
                            {
                                Console.WriteLine("\nInvalid option. Please enter 1, 2, 3, or 4.\n");
                            }
                        }
                    }
                    else // invalid ice cream option, repeat asking (cup/cone/waffle)
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                        Console.WriteLine();
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
            for (int i = 1; i <= IceCreamList.Count; i++)
            {
                ic += $"Ice Cream [{i}]:\n{IceCreamList[i-1]}\n\n";
            }

            if (TimeFulfilled != null)
            {
                return $"ID: {Id}\nTime Received: {TimeReceived}\nTime Fulfilled: {TimeFulfilled}\n\nIce Cream List:\n\n{ic}";
            }
            else
            {
                return $"ID: {Id}\nTime Received: {TimeReceived}\nTime Fulfilled: Unfulfilled\n\nIce Cream List:\n\n{ic}";
            }
        }
    }
}
