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
                    currentIC = ChangeScoops(currentIC);
                }
                else if (option == "3")
                {
                    currentIC = ChangeFlavours(currentIC);
                }
                else if (option == "4")
                {
                    currentIC = ChangeToppings(currentIC);
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

            IceCream ChangeScoops(IceCream cIC)
            {
                while (true)
                {
                    try
                    {
                        Console.Write("How many scoops would you like? (1/2/3): ");
                        int scoops = int.Parse(Console.ReadLine());
                        if (scoops > 0 && scoops < 4)
                        {
                            cIC.Scoops = scoops;
                            return cIC;
                        }
                        else
                        {
                            Console.WriteLine("You may only have 1, 2, or 3 scoops. Please try again.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    }
                }
            }

            IceCream ChangeFlavours(IceCream cIC)
            {
                Console.WriteLine("Flavours:\n[1] Vanilla\n[2] Chocolate\n[3] Strawberry\n[4] Durian (Premium)\n[5] Ube (Premium)\n[6] Sea Salt (Premium)");
                for (int i = 1; i <= cIC.Scoops; i++)
                {
                    while (true)
                    {
                        Console.Write($"Choose flavour {i}: "); 
                        string oF = Console.ReadLine();
                        if (oF == "1")
                        {
                            AddFlavour("Vanilla", false);
                        }
                        else if (oF == "2")
                        {
                            AddFlavour("Chocolate", false);
                        }
                        else if (oF == "3")
                        {
                            AddFlavour("Strawberry", false);
                        }
                        else if (oF == "4")
                        {
                            AddFlavour("Durian", true);
                        }
                        else if (oF == "5")
                        {
                            AddFlavour("Ube", true);
                        }
                        else if (oF == "6")
                        {
                            AddFlavour("Sea Salt", true);

                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1, 2, 3, 4, 5, or 6.");
                            continue;
                        }
                        break;
                    }
                }
                cIC.Flavours = flavours;
                flavours.Clear();
                return cIC;
            }

            IceCream ChangeToppings(IceCream cIC)
            {
                int oT;

                Console.WriteLine("Toppings:\n[1] Sprinkles\n[2] Mochi\n[3] Sago\n[4] Oreos");

                while (true)
                {
                    try
                    {
                        Console.Write("How many toppings would you like? (0/1/2/3/4): ");
                        oT = int.Parse(Console.ReadLine());
                        if (oT == 0)
                        {
                            Console.WriteLine("Removing all toppings...");
                            cIC.Toppings = toppings;
                            return cIC;
                        }
                        else if (oT == 4)
                        {
                            Console.WriteLine("Adding all toppings...");
                            toppings.Add(new Topping("Sprinkles"));
                            toppings.Add(new Topping("Mochi"));
                            toppings.Add(new Topping("Sago"));
                            toppings.Add(new Topping("Oreos"));
                            cIC.Toppings = toppings;
                            return cIC;
                        }
                        else if (oT > 0 && oT < 4)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You may only have 0, 1, 2, 3, or 4 toppings. Please try again.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid option. Please enter 0, 1, 2, 3, or 4.");
                    }
                }
                
                for (int i = 1; i <= oT; i++)
                {
                    while (true)
                    {
                        Console.Write($"Choose topping {i}: ");
                        string oTT = Console.ReadLine();
                        if (oTT == "1")
                        {
                            if (HasTopping("Sprinkles"))
                            {
                                continue;
                            }
                            toppings.Add(new Topping("Sprinkles"));
                        }
                        else if (oTT == "2")
                        {
                            if (HasTopping("Mochi"))
                            {
                                continue;
                            }
                            toppings.Add(new Topping("Mochi"));
                        }
                        else if (oTT == "3")
                        {
                            if (HasTopping("Sago"))
                            {
                                continue;
                            }
                            toppings.Add(new Topping("Sago"));
                        }
                        else if (oTT == "4")
                        {
                            if (HasTopping("Oreos"))
                            {
                                continue;
                            }
                            toppings.Add(new Topping("Oreos"));
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.");
                            continue;
                        }
                        break;
                    }
                }
                cIC.Toppings = toppings;
                toppings.Clear();
                return cIC;
            }

            void AddFlavour(string flavour, bool premium)
            {
                if (flavours.Count > 0)
                {
                    foreach (Flavour listFlavour in flavours)
                    {
                        if (listFlavour.Type == flavour)
                        {
                            listFlavour.Quantity += 1;
                            return;
                        }
                    }
                }
                flavours.Add(new Flavour(flavour, premium, 1));
                return;
            }

            bool HasTopping(string topping)
            {
                if (toppings.Count > 0)
                {
                    foreach (Topping listTopping in toppings)
                    {
                        if (listTopping.Type == topping)
                        {
                            Console.WriteLine("Cannot have multiple of the same topping.");
                            return true;
                        }
                    }
                }
                return false;
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
