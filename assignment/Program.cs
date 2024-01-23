using assignment;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace assignment
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Customer> customerList = new List<Customer>();
            Queue<Order> regularOrders = new Queue<Order>();
            Queue<Order> goldOrders = new Queue<Order>();

            
        }
        // add customers from csv
        void InitialiseCustomers(List<Customer> cList)
        {

        }

        // print main option menu
        void DisplayMenu()
        {

        }


        // Q1: List all customers
        void DisplayCustomers()
        {

        }

        // Q2: List all current orders
        void DisplayOrderQueues(Queue<Order> regularOrders, Queue<Order> goldOrders)
        {
            // display regular queue
            if (regularOrders.Count > 0)
            {
                Console.WriteLine("Current orders in regular queue:");
                int i = 1;
                foreach (var order in regularOrders)
                {
                    Console.WriteLine($"Order {i}: " + order.ToString());
                    i++;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No orders in regular queue.");
            }

            // display gold queue
            if (goldOrders.Count > 0)
            {
                Console.WriteLine("Current orders in gold members queue:");
                int i = 1;
                foreach (var order in goldOrders)
                {
                    Console.WriteLine($"Order {i}: " + order.ToString());
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No orders in gold members queue.");
            }
        }

        // Q3:

        // Q4:

        // Q5: Display order details of a customer
        void DisplayCustomerOrder(List<Customer> customerList)
        {
            DisplayCustomers();

            while (true)
            {
                try
                {
                    // user selects customer
                    Console.Write("Select a customer: ");
                    int cIndex = int.Parse(Console.ReadLine());
                    Customer selectedC = customerList[cIndex - 1];

                    Console.WriteLine();

                    // display customer current order
                    if (selectedC.CurrentOrder != null)
                    {
                        Console.WriteLine("Current Order: ");
                        Console.WriteLine(selectedC.CurrentOrder.ToString());
                    }
                    else
                    {
                        Console.WriteLine("No current order.");
                    }

                    Console.WriteLine();

                    // display customer past orders
                    if (selectedC.OrderHistory.Count > 0)
                    {
                        Console.WriteLine("Past Orders: ");
                        int i = 1;
                        foreach (Order order in selectedC.OrderHistory)
                        {
                            Console.WriteLine($"Order {i}: " + order.ToString());
                            i++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No past orders.");
                    }

                    // all code executed successfully, end method
                    return;
                }
                // if user entered non-int value
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option. Please enter an integer value.");
                }
                // if user selected a number outside customer list range
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Invalid option. Please enter a number from 1 to {customerList.Count}");
                }
            }
        }

        // Q6: Modify order details
        void ModifyCustomerOrder(List<Customer> customerList)
        {
            DisplayCustomers();
            while (true)
            {
                try
                {
                    // user selects customer
                    Console.Write("Select a customer: ");
                    int cIndex = int.Parse(Console.ReadLine());
                    Customer selectedC = customerList[cIndex - 1];

                    Console.WriteLine();

                    // checking if there is current order
                    // if no, print message and go to return
                    if (selectedC.CurrentOrder == null)
                    {
                        Console.WriteLine("No current order. Make an order using [4] Create Order.");
                    }
                    // if yes, execute this code
                    else
                    {
                        Order cOrder = selectedC.CurrentOrder;

                        string option = DisplayModifyOrderMenu(cOrder);

                        Console.WriteLine();

                        if (option == "1")
                        {
                            // select ice cream
                            int icIndex = ChooseIceCream(cOrder.IceCreamList);
                            Console.WriteLine();

                            // modify selected ice cream
                            cOrder.ModifyIceCream(icIndex);
                        }
                        else if (option == "2")
                        {
                            IceCream newIC = CreateIceCream();

                            Console.WriteLine();

                            cOrder.AddIceCream(newIC);
                            Console.WriteLine("Ice cream added.");
                        }
                        // if option == 3 (already data validated in method)
                        else
                        {
                            int icIndex = ChooseIceCream(cOrder.IceCreamList);
                            cOrder.DeleteIceCream(icIndex);
                            Console.WriteLine("Ice cream deleted.");
                        }

                        Console.WriteLine();

                        // update customer order and display
                        selectedC.CurrentOrder = cOrder;
                        Console.WriteLine("Modified Order: " + cOrder.ToString());
                    }

                    Console.WriteLine();

                    // all code executed successfully, end method
                    Console.WriteLine("Returning to main menu...");
                    return;
                }
                // if user entered non-int value for customer list
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option. Please enter an integer value.");
                }
                // if user selected a number not within customer list range
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Invalid option. Please enter a number from 1 to {customerList.Count}");
                }
            }
        }

        string DisplayModifyOrderMenu(Order cOrder)
        {
            // display ice creams
            Console.WriteLine("Ice creams in current order:");
            int i = 1;
            foreach (IceCream ic in cOrder.IceCreamList)
            {
                Console.WriteLine($"[{i}]: " + ic.ToString());
                i++;
            }

            Console.WriteLine();

            // display modify menu
            Console.WriteLine("[1] Modify Existing Ice Cream");
            Console.WriteLine("[2] Add Ice Cream");
            Console.WriteLine("[3] Delete Ice Cream");

            while (true)
            {
                Console.Write("What would you like to do?: ");
                string option = Console.ReadLine();
                // checks if valid option, repeating if not
                if (option == "1" || option == "2" || option == "3")
                {
                    return option;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                }
            }
        }

        int ChooseIceCream(List<IceCream> icList)
        {
            while (true)
            {
                try
                {
                    Console.Write("Please choose your ice cream: ");
                    int option = int.Parse(Console.ReadLine());
                    if (option <= 0 || option >= icList.Count)
                    {
                        Console.WriteLine($"Invalid option. Please enter a number from 1 to {icList.Count}");
                    }
                    else
                    {
                        return option;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option. Please enter an integer value.");
                }
            }
        }

        public static IceCream CreateIceCream()
        {
            Console.WriteLine("Options:\n[1] Cup\n[2] Cone\n[3] Waffle");

            while (true)
            {
                Console.Write("Choose your ice cream option: ");
                string o = Console.ReadLine();
                if (o == "1")
                {
                    int s = ChooseScoops();
                    Console.WriteLine();
                    List<Flavour> fList = ChooseFlavours(s);
                    Console.WriteLine();
                    List<Topping> tList = ChooseToppings();
                    return new Cup(s, fList, tList);
                }
                else if (o == "2")
                {
                    bool dipped;
                    while (true)
                    {
                        Console.Write("Would you like a dipped cone? (Y/N): ");
                        string oD = Console.ReadLine();
                        if (oD.ToUpper() == "Y")
                        {
                            dipped = true;
                            break;
                        }
                        else if (oD.ToUpper() == "N")
                        {
                            dipped = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter Y or N.");
                        }
                    }
                    int s = ChooseScoops();
                    Console.WriteLine();
                    List<Flavour> fList = ChooseFlavours(s);
                    Console.WriteLine();
                    List<Topping> tList = ChooseToppings();
                    return new Cone(s, fList, tList, dipped);
                }
                else if (o == "3")
                {
                    string wf;

                    Console.WriteLine("Waffle Flavours:\n[1] Original\n[2] Red Velvet\n[3] Charcoal\n[4] Pandan");
                    while (true)
                    {
                        Console.Write("What flavour would you like?: ");
                        string oF = Console.ReadLine();
                        if (oF == "1")
                        {
                            wf = "Original";
                        }
                        else if (oF == "2")
                        {
                            wf = "Red Velvet";
                        }
                        else if (oF == "3")
                        {
                            wf = "Charcoal";
                        }
                        else if (oF == "4")
                        {
                            wf = "Pandan";
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.");
                            continue;
                        }
                        break;
                    }
                    int s = ChooseScoops();
                    Console.WriteLine();
                    List<Flavour> fList = ChooseFlavours(s);
                    Console.WriteLine();
                    List<Topping> tList = ChooseToppings();
                    return new Waffle(s, fList, tList, wf);
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                }
            }
        }

        public static int ChooseScoops()
        {
            while (true)
            {
                try
                {
                    Console.Write("Choose your number of scoops (1/2/3): ");
                    int s = int.Parse(Console.ReadLine());
                    if (s < 1 || s > 3)
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    }
                    else
                    {
                        return s;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option. Please enter an integer value.");
                }
            }
        }

        public static List<Flavour> ChooseFlavours(int s)
        {
            List<Flavour> fList = new List<Flavour>();

            Console.WriteLine("Flavours:\n[1] Vanilla\n[2] Chocolate\n[3] Strawberry\n[4] Durian (Premium)\n[5] Ube (Premium)\n[6] Sea Salt (Premium)");
            for (int i = 1; i <= s; i++)
            {
                while (true)
                {
                    Console.Write($"Choose flavour {i}: ");
                    string oF = Console.ReadLine();
                    if (oF == "1")
                    {
                        fList = AddFlavour("Vanilla", false, fList);
                    }
                    else if (oF == "2")
                    {
                        fList = AddFlavour("Chocolate", false, fList);
                    }
                    else if (oF == "3")
                    {
                        fList = AddFlavour("Strawberry", false, fList);
                    }
                    else if (oF == "4")
                    {
                        fList = AddFlavour("Durian", true, fList);
                    }
                    else if (oF == "5")
                    {
                        fList = AddFlavour("Ube", true, fList);
                    }
                    else if (oF == "6")
                    {
                        fList = AddFlavour("Sea Salt", true, fList);

                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, 3, 4, 5, or 6.");
                        continue;
                    }
                    break;
                }
            }
            return fList;
        }
        public static List<Flavour> AddFlavour(string flavour, bool premium, List<Flavour> flavours)
        {
            if (flavours.Count > 0)
            {
                foreach (Flavour listFlavour in flavours)
                {
                    if (listFlavour.Type == flavour)
                    {
                        listFlavour.Quantity += 1;
                        return flavours;
                    }
                }
            }
            flavours.Add(new Flavour(flavour, premium, 1));
            return flavours;
        }

        public static List<Topping> ChooseToppings()
        {
            List<Topping> toppings = new List<Topping>();
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
                        Console.WriteLine("No toppings chosen.");
                        return toppings;
                    }
                    else if (oT == 4)
                    {
                        Console.WriteLine("Adding all toppings...");
                        toppings.Add(new Topping("Sprinkles"));
                        toppings.Add(new Topping("Mochi"));
                        toppings.Add(new Topping("Sago"));
                        toppings.Add(new Topping("Oreos"));
                        return toppings;
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
                        if (HasTopping("Sprinkles", toppings))
                        {
                            continue;
                        }
                        toppings.Add(new Topping("Sprinkles"));
                    }
                    else if (oTT == "2")
                    {
                        if (HasTopping("Mochi", toppings))
                        {
                            continue;
                        }
                        toppings.Add(new Topping("Mochi"));
                    }
                    else if (oTT == "3")
                    {
                        if (HasTopping("Sago", toppings))
                        {
                            continue;
                        }
                        toppings.Add(new Topping("Sago"));
                    }
                    else if (oTT == "4")
                    {
                        if (HasTopping("Oreos", toppings))
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
            return toppings;
        }
        public static bool HasTopping(string topping, List<Topping> toppings)
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

        // advanced (a)
        // to associate order with member, check and match the order id (unique among all orders!!)
    }
}
