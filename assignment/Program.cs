using assignment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace assignment
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Customer> customerList = new List<Customer>();
            Queue<Order> regularOrders = new Queue<Order>();
            Queue<Order> goldOrders = new Queue<Order>();
            // keeps track of all orders made in the program
            List<Order> orderList = new List<Order>();

            // adding customers from customer.csv
            InitialiseCustomers(customerList);

            while (true)
            {
                // display main menu and take option
                DisplayMenu();
                string o = Console.ReadLine();
                Console.WriteLine();

                if (o == "1")
                {
                    Console.WriteLine("==================");
                    Console.WriteLine("List all customers");
                    Console.WriteLine("==================");
                    Console.WriteLine();
                    DisplayCustomers(customerList);
                }
                else if (o == "2")
                {
                    Console.WriteLine("=======================");
                    Console.WriteLine("List all current orders");
                    Console.WriteLine("=======================");
                    Console.WriteLine();
                    DisplayOrderQueues(regularOrders, goldOrders);
                }
                else if (o == "3")
                {
                    Console.WriteLine("=======================");
                    Console.WriteLine("Register a new customer");
                    Console.WriteLine("=======================");
                    Console.WriteLine();
                    Dictionary<int, Customer>? customerDict = null;
                    RegisterNewCustomer(ref customerDict);
                }

                else if (o == "4")
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine("Create a customer's order");
                    Console.WriteLine("=========================");
                    Console.WriteLine();
                    MakeCOrder(customerList, orderList, regularOrders, goldOrders);
                }
                else if (o == "5")
                {
                    Console.WriteLine("===================================");
                    Console.WriteLine("Display order details of a customer");
                    Console.WriteLine("===================================");
                    Console.WriteLine();
                    DisplayCustomerOrder(customerList);
                }
                else if (o == "6")
                {
                    Console.WriteLine("====================");
                    Console.WriteLine("Modify order details");
                    Console.WriteLine("====================");
                    Console.WriteLine();
                    ModifyCustomerOrder(customerList);
                }
                else if (o == "7")
                {
                    Console.WriteLine("=============================");
                    Console.WriteLine("Process an order and checkout");
                    Console.WriteLine("=============================");
                    Console.WriteLine();
                    Checkout(regularOrders, goldOrders, customerList);
                }
                else if (o == "8")
                {
                    Console.WriteLine("================================================================================");
                    Console.WriteLine("Display monthly charged amounts breakdown and total charged amounts for the year");
                    Console.WriteLine("================================================================================");
                    Console.WriteLine();

                    //DisplayMonthlyChargedAmounts(orders);
                }
                else if (o == "0") // exit
                {
                    Console.WriteLine("======================================");
                    Console.WriteLine("Thank you for shopping at I.C. Treats!");
                    Console.WriteLine("======================================");
                    break;
                }
                else // if invalid option
                {
                    Console.WriteLine("Invalid option. Please choose one of the listed options (0 - 8).");
                }
                Console.WriteLine();
            }
        }
        // add customers from csv
        static void InitialiseCustomers(List<Customer> cList)
        {
            // Specify the path to the CSV file
            string csvFilePath = "customers.csv";

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                // Skip the header line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');

                    // Assuming the structure: Name, MemberId, DOB, MembershipStatus, MembershipPoints, PunchCard
                    string name = fields[0];
                    int memberId = int.Parse(fields[1]);
                    DateTime dob = DateTime.ParseExact(fields[2], "dd/MM/yyyy", null);
                    string membershipStatus = fields[3];
                    int membershipPoints = int.Parse(fields[4]);
                    int punchCard = int.Parse(fields[5]);

                    // Add the customer information to the list
                    Customer c = new Customer(name, memberId, dob);
                    c.Rewards.Tier = membershipStatus;
                    c.Rewards.Points = membershipPoints;
                    c.Rewards.PunchCard = punchCard;
                    cList.Add(c);

                }
            }
        }
        // print main option menu
        static void DisplayMenu()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("I.C. Treats");
            Console.WriteLine("=================================");
            Console.WriteLine("[1] List all customers");
            Console.WriteLine("[2] List all current orders");
            Console.WriteLine("[3] Register a new customer");
            Console.WriteLine("[4] Create a customer's order");
            Console.WriteLine("[5] Display order details of a customer");
            Console.WriteLine("[6] Modify order details");
            Console.WriteLine("[7] Process an order and checkout");
            Console.WriteLine("[8] Display monthly charged amounts breakdown and total charged amounts for the year");
            Console.WriteLine("[0] Exit");
            Console.WriteLine();
            Console.Write("Enter your option: ");

        }


        // Q1: List all customers

        static void DisplayCustomers(List<Customer> cList)
        {
            Console.WriteLine($"{"",-5} {"Name",-10}   {"ID",-6}   {"DoB",-10}   {"Tier"}");
            Console.WriteLine("-------------------------------------------------");

            int i = 1;
            foreach (var c in cList)
            {
                Console.WriteLine($"{$"[{i}]",-5} {c.Name,-10}   {c.MemberId,-6}   {c.Dob.ToString("dd/MM/yyyy"),-10}   {c.Rewards.Tier}");
                i++;
            }
        }


        // Q2: List all current orders
        static void DisplayOrderQueues(Queue<Order> regularOrders, Queue<Order> goldOrders)
        {
            // display regular queue
            if (regularOrders.Count > 0)
            {
                // header
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Current orders in regular queue");
                Console.WriteLine("-------------------------------");
                // orders
                int i = 1;
                foreach (var order in regularOrders)
                {
                    Console.WriteLine();
                    Console.Write($"Order {i}:\n" + order.ToString());
                    Console.WriteLine("---");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("No orders in regular queue.");
                Console.WriteLine("---------------------------");
            }

            Console.WriteLine();

            // display gold queue
            if (goldOrders.Count > 0)
            {
                // header
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Current orders in gold members queue");
                Console.WriteLine("------------------------------------");
                // orders
                int i = 1;
                foreach (var order in goldOrders)
                {
                    Console.WriteLine();
                    Console.Write($"Order {i}:\n" + order.ToString());
                    Console.WriteLine("---");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("No orders in gold members queue.");
                Console.WriteLine("--------------------------------");
            }

            return;
        }

        // Q3:

        // Modify AddCustomer to accept a nullable dictionary
        static void AddCustomer(ref Dictionary<int, Customer>? customerDict, Customer newCustomer)
        {
            // Check if the customerDict is null
            if (customerDict == null)
            {
                customerDict = new Dictionary<int, Customer>();
            }

            // Add the new customer to the dictionary
            customerDict.Add(newCustomer.MemberId, newCustomer);
        }


        static void RegisterNewCustomer(ref Dictionary<int, Customer>? customerDict)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter customer's name: ");
                    string name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Enter a valid name");
                        continue;
                    }

                    Console.Write("Enter Membership ID number: ");
                    int memberId;
                    while (!int.TryParse(Console.ReadLine(), out memberId))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for Member ID.");
                        Console.Write("Enter Membership ID number: ");
                    }

                    Console.Write("Enter Customer's Date-Of-Birth (dd/MM/yyyy): ");
                    string dateOfBirth = Console.ReadLine();
                    DateTime dob = DateTime.ParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    Customer newCustomer = new Customer(name, memberId, dob);

                    PointCard newPointCard = new PointCard(0, 0);
                    newPointCard.Tier = "Ordinary";
                    newCustomer.Rewards = newPointCard;

                    // Pass the dictionary by reference
                    AddCustomer(ref customerDict, newCustomer);

                    Console.WriteLine("Registration successful!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        // Q4:
        static void MakeCOrder(List<Customer> customerList, List<Order> orderList, Queue<Order> regQ, Queue<Order> goldQ)
        {
            // selecting customer
            int cIndex = SelectCustomer(customerList);
            Customer selectedC = customerList[cIndex];

            Console.WriteLine();

            // header
            Console.WriteLine("------------");
            Console.WriteLine("Making Order");
            Console.WriteLine("------------");
            Console.WriteLine();

            // making order (see Customer class)
            Order newO = selectedC.MakeOrder();

            // setting order id and adding it to order list
            if (selectedC.CurrentOrder != null) // if customer is overwriting their current order
            {
                for (int i = 0; i < orderList.Count; i++)
                {
                    if (orderList[i].Id == selectedC.CurrentOrder.Id) // going through order list until their order matches
                    {
                        // set the new order id to the same id
                        newO.Id = orderList[i].Id;

                        // change that order in the list to the new order
                        orderList[i] = newO;
                    }
                }
            }
            else // if customer is making brand new order
            {
                // assign new order id
                newO.Id = orderList.Count + 1;

                // add to order list
                orderList.Add(newO);
            }

            // setting order as customer current order
            customerList[cIndex].CurrentOrder = newO;

            // if customer is gold tier, queue in gold, else queue in regular
            if (selectedC.Rewards.Tier == "Gold")
            {
                goldQ.Enqueue(newO);
            }
            else
            {
                regQ.Enqueue(newO);
            }

            return;
        }


        // Q5: Display order details of a customer
        static void DisplayCustomerOrder(List<Customer> customerList)
        {
            // selecting customer
            int cIndex = SelectCustomer(customerList);
            Customer selectedC = customerList[cIndex];

            Console.WriteLine();

            // display customer current order
            if (selectedC.CurrentOrder != null)
            {
                // header
                Console.WriteLine("-------------");
                Console.WriteLine("Current Order");
                Console.WriteLine("-------------");
                Console.WriteLine();
                // order
                Console.Write(selectedC.CurrentOrder.ToString());
                Console.WriteLine("---");
            }
            else
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("No current order.");
                Console.WriteLine("-----------------");
            }

            Console.WriteLine();

            // display customer past orders
            if (selectedC.OrderHistory.Count > 0)
            {
                // header
                Console.WriteLine("------------");
                Console.WriteLine("Past Orders:");
                Console.WriteLine("------------");
                // order
                int i = 1;
                foreach (Order order in selectedC.OrderHistory)
                {
                    Console.WriteLine();
                    Console.Write($"Order [{i}]\n\n" + order.ToString());
                    Console.WriteLine("---");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("---------------");
                Console.WriteLine("No past orders.");
                Console.WriteLine("---------------");
            }

            return;
        }

        // Q6: Modify order details
        static void ModifyCustomerOrder(List<Customer> customerList)
        {
            // selecting customer
            int cIndex = SelectCustomer(customerList);
            Customer selectedC = customerList[cIndex];

            Console.WriteLine();

            // checking if there is current order
            if (selectedC.CurrentOrder == null) // if no order
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("No current order. Make an order using [4] Create Order.");
                Console.WriteLine("-------------------------------------------------------");
                // goes to return
            }
            else // if has order
            {
                // setting new order
                Order cOrder = selectedC.CurrentOrder;

                // display menu for modifiying order, and take user input
                string option = DisplayModifyOrderMenu(cOrder);

                if (option == "1") // modify ice cream
                {
                    // select ice cream
                    int icIndex = ChooseIceCream(cOrder.IceCreamList);

                    // modify selected ice cream (see Order class)
                    cOrder.ModifyIceCream(icIndex);
                }
                else if (option == "2") // add ice cream
                {
                    // creating ice cream
                    IceCream newIC = CreateIceCream();

                    // adding ice cream to order
                    cOrder.AddIceCream(newIC);

                    // message
                    Console.WriteLine("----------------");
                    Console.WriteLine("Ice cream added.");
                    Console.WriteLine("----------------");
                }
                else // delete ice cream
                {
                    if (cOrder.IceCreamList.Count == 1) // user cannot delete ice cream if there is only 1 remaining ice cream
                    {
                        Console.WriteLine("You cannot have 0 ice creams in the order!");
                    }
                    else
                    {
                        // select ice cream
                        int icIndex = ChooseIceCream(cOrder.IceCreamList);

                        // delete ice cream from order
                        cOrder.DeleteIceCream(icIndex);

                        // message
                        Console.WriteLine("------------------");
                        Console.WriteLine("Ice cream deleted.");
                        Console.WriteLine("------------------");
                    }
                }

                Console.WriteLine();

                // update customer in list
                selectedC.CurrentOrder = cOrder;
                customerList[cIndex] = selectedC;

                // display order
                Console.WriteLine("--------------");
                Console.WriteLine("Modified Order");
                Console.WriteLine("--------------");
                Console.Write(cOrder.ToString());
            }
            return;
        }

        static string DisplayModifyOrderMenu(Order cOrder)
        {
            // display ice creams
            //header 
            Console.WriteLine("---------------------------");
            Console.WriteLine("Ice creams in current order");
            Console.WriteLine("---------------------------");
            // ice cream
            for (int i = 1; i <= cOrder.IceCreamList.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Ice Cream [{i}]:\n{cOrder.IceCreamList[i - 1]}\n");
                Console.WriteLine("---");
            }

            // display modify menu
            Console.WriteLine("-------------------");
            Console.WriteLine("Order Modifications");
            Console.WriteLine("-------------------");
            Console.WriteLine("[1] Modify Existing Ice Cream");
            Console.WriteLine("[2] Add Ice Cream");
            Console.WriteLine("[3] Delete Ice Cream");
            Console.WriteLine();

            // take user input
            while (true)
            {
                Console.Write("What would you like to do?: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                // checks if valid option
                if (option == "1" || option == "2" || option == "3")
                {
                    return option; // return user input
                }
                else // repeat if invalid
                {
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    Console.WriteLine();
                }
            }
        }

        static int ChooseIceCream(List<IceCream> icList)
        {
            while (true)
            {
                try
                {
                    // user input
                    Console.Write("Please choose your ice cream: ");
                    int option = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    if (option <= 0 || option > icList.Count) // if invalid, repeat (outside of ice cream index)
                    {
                        Console.WriteLine($"Invalid option. Please enter a number from 1 to {icList.Count}.");
                        Console.WriteLine();
                    }
                    else // if valid, return user input
                    {
                        return option - 1;
                    }
                }
                catch (FormatException) // if invalid, repeat (int.parse failed)
                {
                    Console.WriteLine("\nInvalid option. Please enter an integer value.\n");
                }
            }
        }

        public static IceCream CreateIceCream()
        {
            // display ice cream options
            Console.WriteLine("-----------------");
            Console.WriteLine("Ice Cream Options");
            Console.WriteLine("-----------------");
            Console.WriteLine("[1] Cup\n[2] Cone\n[3] Waffle");
            Console.WriteLine();

            while (true)
            {
                // user select ice cream option
                Console.Write("Choose your ice cream option: ");
                string o = Console.ReadLine();
                Console.WriteLine();

                if (o == "1") // cup
                {
                    int s = ChooseScoops();
                    List<Flavour> fList = ChooseFlavours(s);
                    List<Topping> tList = ChooseToppings();
                    return new Cup(s, fList, tList);
                }
                else if (o == "2") // cone
                {
                    bool dipped;
                    while (true)
                    {
                        // check for dipped cone
                        Console.Write("Would you like a dipped cone? (Y/N): ");
                        string oD = Console.ReadLine();
                        Console.WriteLine();

                        if (oD.ToUpper() == "Y") // if yes, break loop and continue method
                        {
                            dipped = true;
                            break;
                        }
                        else if (oD.ToUpper() == "N") // if no, break loop and continue method
                        {
                            dipped = false;
                            break;
                        }
                        else // if invalid option, repeat asking for dipped cone
                        {
                            Console.WriteLine("Invalid option. Please enter Y or N.\n");
                        }
                    }

                    int s = ChooseScoops();
                    List<Flavour> fList = ChooseFlavours(s);
                    List<Topping> tList = ChooseToppings();
                    return new Cone(s, fList, tList, dipped);
                }
                else if (o == "3") // waffle
                {
                    string wf;

                    // display waffle flavours
                    Console.WriteLine("---------------");
                    Console.WriteLine("Waffle Flavours");
                    Console.WriteLine("---------------");
                    Console.WriteLine("[1] Original\n[2] Red Velvet\n[3] Charcoal\n[4] Pandan");
                    Console.WriteLine();

                    while (true)
                    {
                        // user select waffle flavour
                        Console.Write("What flavour would you like?: ");
                        string oF = Console.ReadLine();
                        Console.WriteLine();

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
                        else // if invalid option, repeat asking for flavours
                        {
                            Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.\n");
                            continue;
                        }
                        break; // if valid, break loop and continue method
                    }

                    int s = ChooseScoops();
                    List<Flavour> fList = ChooseFlavours(s);
                    List<Topping> tList = ChooseToppings();
                    return new Waffle(s, fList, tList, wf);
                }
                else // invalid ice cream option, repeat
                {
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.\n");
                }
            }
        }

        public static int ChooseScoops()
        {
            while (true)
            {
                try
                {
                    // choose scoops
                    Console.Write("Choose your number of scoops (1/2/3): ");
                    int s = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    if (s < 1 || s > 3) // if not within scoop range (invalid)
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, or 3.\n");
                    }
                    else // if valid, return scoop
                    {
                        return s;
                    }
                }
                catch (FormatException) // invalid option, int.parse failed
                {
                    Console.WriteLine("\nInvalid option. Please enter an integer value.\n");
                }
            }
        }

        public static List<Flavour> ChooseFlavours(int s)
        {
            List<Flavour> fList = new List<Flavour>();

            //display flavours
            Console.WriteLine("------------------");
            Console.WriteLine("Ice Cream Flavours");
            Console.WriteLine("------------------");
            Console.WriteLine("[1] Vanilla\n[2] Chocolate\n[3] Strawberry\n[4] Durian (Premium)\n[5] Ube (Premium)\n[6] Sea Salt (Premium)");
            Console.WriteLine();

            for (int i = 1; i <= s; i++) // choose flavour for each scoop
            {
                while (true)
                {
                    // user input
                    Console.Write($"Choose flavour {i}: ");
                    string oF = Console.ReadLine();
                    Console.WriteLine();

                    if (oF == "1")
                    {
                        AddFlavour("Vanilla", false, fList);
                    }
                    else if (oF == "2")
                    {
                        AddFlavour("Chocolate", false, fList);
                    }
                    else if (oF == "3")
                    {
                        AddFlavour("Strawberry", false, fList);
                    }
                    else if (oF == "4")
                    {
                        AddFlavour("Durian", true, fList);
                    }
                    else if (oF == "5")
                    {
                        AddFlavour("Ube", true, fList);
                    }
                    else if (oF == "6")
                    {
                        AddFlavour("Sea Salt", true, fList);

                    }
                    else // invalid flavour, repeat asking for flavour i
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, 3, 4, 5, or 6.\n");
                        continue;
                    }
                    break; // valid flavour, break loop to ask for next flavour / continue to return
                }
            }
            return fList; //return flavour list
        }
        public static void AddFlavour(string flavour, bool premium, List<Flavour> flavours)
        {
            if (flavours.Count > 0) // check if user has already selected flavours
            {
                foreach (Flavour listFlavour in flavours)
                {
                    if (listFlavour.Type == flavour) // if selected flavours already contain this flavour
                    {
                        listFlavour.Quantity += 1; // increment the flavour object quantity and return
                        return;
                    }
                }
            }
            flavours.Add(new Flavour(flavour, premium, 1)); // if new flavour, add new flavour object
            return;
        }

        public static List<Topping> ChooseToppings()
        {
            List<Topping> toppings = new List<Topping>();
            int oT; // check how many toppings user wants

            // display toppings
            Console.WriteLine("--------");
            Console.WriteLine("Toppings");
            Console.WriteLine("--------");
            Console.WriteLine("[1] Sprinkles\n[2] Mochi\n[3] Sago\n[4] Oreos");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    // user input
                    Console.Write("How many toppings would you like? (0/1/2/3/4): ");
                    oT = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    if (oT == 0) // no toppings, return empty list
                    {
                        Console.WriteLine("No toppings chosen.\n");
                        return toppings;
                    }
                    else if (oT == 4) // all 4 toppings, automatically add and return list
                    {
                        Console.WriteLine("Adding all toppings...\n");
                        toppings.Add(new Topping("Sprinkles"));
                        toppings.Add(new Topping("Mochi"));
                        toppings.Add(new Topping("Sago"));
                        toppings.Add(new Topping("Oreos"));
                        return toppings;
                    }
                    else if (oT > 0 && oT < 4) // if 1/2/3, valid option, break loop and continue method
                    {
                        break;
                    }
                    else // invalid option (not within listed range), repeat asking for amount of toppings
                    {
                        Console.WriteLine("You may only have 0, 1, 2, 3, or 4 toppings. Please try again.\n");
                    }
                }
                catch (FormatException)  // invalid option (int.parse failed), repeat asking for amount of toppings
                {
                    Console.WriteLine("\nInvalid option. Please enter 0, 1, 2, 3, or 4.\n");
                }
            }

            // for each topping, choose type
            for (int i = 1; i <= oT; i++)
            {
                while (true)
                {
                    // user input
                    Console.Write($"Choose topping {i}: ");
                    string oTT = Console.ReadLine();
                    Console.WriteLine();

                    if (oTT == "1")
                    {
                        //check if duplicate
                        if (HasTopping("Sprinkles", toppings))
                        {
                            continue; // if yes, repeat asking for topping i
                        }
                        // else, add topping, skip to break statement
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
                    else // invalid option, repeat asking for topping i
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.\n");
                        continue;
                    }
                    break; // will end loop of asking for topping i, move onto next topping or go to return
                }
            }
            return toppings; // return topping list
        }
        public static bool HasTopping(string topping, List<Topping> toppings)
        {
            if (toppings.Count > 0) // check if user has already selected toppings
            {
                foreach (Topping listTopping in toppings)
                {
                    if (listTopping.Type == topping) // if selected toppings already contain this topping
                    {
                        Console.WriteLine("Cannot have multiple of the same topping.\n"); // print error message and return true
                        return true;
                    }
                }
            }
            return false; // if all checks passed, return false
        }

        // advanced (a)
        // to associate order with member, check and match the order id (unique among all orders!!)
        static void Checkout(Queue<Order> regQ, Queue<Order> goldQ, List<Customer> cList)
        {
            Order order = new Order();
            Customer customer = new Customer();

            // prioritises gold queue, only takes from reg queue if gold is empty
            if (goldQ.Count > 0)
            {
                order = goldQ.Dequeue();
            }
            else if (regQ.Count > 0)
            {
                order = regQ.Dequeue();
            }
            else // if no orders in both queuees, display message and end method
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("No orders in queue.");
                Console.WriteLine("-------------------");
                return;
            }

            // finding associated customer
            int cIndex = 0;
            for (int i = 0; i < cList.Count; i++)
            {
                if (cList[i].CurrentOrder.Id == order.Id)
                {
                    customer = cList[i];
                    cIndex = i; // tracks the customer's index in customer list
                    break;
                }
            }

            // display customer name and ID
            Console.WriteLine($"Customer Name: {customer.Name}\nMember ID: {customer.MemberId}");
            Console.WriteLine();

            // display all ice creams and add to bill
            // header
            Console.WriteLine("-------------------");
            Console.WriteLine("Ice Creams In Order");
            Console.WriteLine("-------------------");
            // ice creams and bill
            double total = 0;
            for (int i = 0; i < order.IceCreamList.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Ice cream [{i + 1}]\n{order.IceCreamList[i].ToString()}\n");
                Console.WriteLine("---");
                total += order.IceCreamList[i].CalculatePrice();
            }

            Console.WriteLine();

            // display bill
            Console.WriteLine("--------------------------");
            Console.WriteLine($"Total bill amount: ${total:F2}");
            Console.WriteLine("--------------------------");

            Console.WriteLine();

            // display member status & points
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Membership status: {customer.Rewards.Tier}\nPoints: {customer.Rewards.Points}");
            Console.WriteLine("----------------------------");

            Console.WriteLine();

            // check if bday
            int bdayIC = 0; // this checks whether bday ice cream is the first ice cream
            if (customer.IsBirthday())
            {
                // message
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine("Happy birthday! As a gift, the most expensive ice cream in your order will be free of charge.");
                Console.WriteLine("---------------------------------------------------------------------------------------------");

                // checking for most expensive ice cream
                double free = 0; // contains price of the most expensive ice cream
                foreach (IceCream ic in order.IceCreamList)
                {
                    if (ic.CalculatePrice() > free)
                    {
                        free = ic.CalculatePrice(); // if current ic is more expensive, update value
                        bdayIC++; // if the most expensive ice cream is icecreamlist[0], value will stay at 1. else, value > 1
                    }
                }
                total -= free; // minus price from order bill

                Console.WriteLine();
            }

            // check if punch card complete, and if there are remaining ice creams to give discount to (ie. birthday hasnt already made order free)
            if (customer.Rewards.PunchCard == 10 && total != 0)
            {
                // header
                Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Congratulations! You've completed your punch card. The first ice cream in your order will be free of charge.");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------");

                // checks whether icecreamlist[0] has already been given free from bday gift
                if (bdayIC == 1)
                {
                    // if yes, minus second ice cream cost
                    total -= order.IceCreamList[1].CalculatePrice();
                }
                else
                {
                    // if no, minus first ice cream cost
                    total -= order.IceCreamList[0].CalculatePrice();
                }

                // reset punchcard to 0
                customer.Rewards.PunchCard = 0;

                Console.WriteLine();
            }

            // checks if customer can redeem points (is silver or gold member)
            if (customer.Rewards.Tier != "Ordinary")
            {
                while (true)
                {
                    try
                    {
                        // user input
                        Console.Write("How many of your points would you like to redeem to offset your final bill total? (1pt = $0.02): ");
                        int pts = int.Parse(Console.ReadLine());
                        Console.WriteLine();

                        // calculate discount
                        double discount = pts * 0.02;

                        if (pts < 0) // if user input negative number
                        {
                            Console.WriteLine("You may not enter a negative number. Please try again.\n");
                        }
                        else if (pts > customer.Rewards.Points) // if user input more points than they have. displays customers current points
                        {
                            Console.WriteLine($"You don't have enough points for that. Please try again. (Current point total: {customer.Rewards.Points})\n");
                        }
                        else if (discount > total) // if the discount is greater than the total. displays current total, and its points equivalent.
                        {
                            Console.WriteLine($"You may not redeem for more than your bill total. Please try again. (Current bill total: ${total:F2}, equivalent to {Math.Floor(total / 0.02)} points)\n");
                        }
                        else
                        {
                            // minus discount from total
                            total -= discount;

                            // redeem customer points
                            customer.Rewards.RedeemPoints(pts);

                            break; // all checks passed, break loop and STOP asking user for points, continue method
                        }
                    }
                    catch (FormatException) // invalid user input, int.parse failed
                    {
                        Console.WriteLine("\nPlease enter an integer value.\n");
                    }
                }
            }

            // display the final total bill amount
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Final total bill amount: ${total:F2}");
            Console.WriteLine("-------------------------------");
            Console.WriteLine();

            // prompt user to press any key to make payment
            Console.Write("Press any key to make payment ");
            Console.ReadKey();
            Console.WriteLine();

            // increment the punch card for every ice cream in the order 
            foreach (IceCream ic in order.IceCreamList)
            {
                customer.Rewards.Punch();
            }

            // earn points
            customer.Rewards.AddPoints((int)Math.Floor(total * 0.72));

            // while earning points, upgrade the member status accordingly
            if (customer.Rewards.Tier != "Gold") // checks if member is ordinary or silver
            {
                if (customer.Rewards.Points >= 100) // if pts are above gold threshold, upgraded to gold
                {
                    customer.Rewards.Tier = "Gold";
                }
                else if (customer.Rewards.Points >= 50) // if above silver threshold, ordinary upgrades to silver (silver stays the same)
                {
                    customer.Rewards.Tier = "Silver";
                }
            }

            // mark the order as fulfilled with the current datetime
            order.TimeFulfilled = DateTime.Now;

            // add this fulfilled order object to the customer’s order history
            customer.CurrentOrder = null;
            customer.OrderHistory.Add(order);

            //update customer in list
            cList[cIndex] = customer;

            return;
        }

        static int SelectCustomer(List<Customer> customerList)
        {
            // display customers
            DisplayCustomers(customerList);
            Console.WriteLine();

            while (true)
            {
                try
                {
                    // user selects customer
                    Console.Write("Select a customer: ");
                    int cIndex = int.Parse(Console.ReadLine());

                    if (cIndex > 0 && cIndex <= customerList.Count) // if valid option, return customer index
                    {
                        return cIndex - 1;
                    }

                    // invalid option, not within list range
                    Console.WriteLine($"\nInvalid option. Please enter a number from 1 to {customerList.Count}\n");
                }
                catch (FormatException) // invalid option, int.parse failed
                {
                    Console.WriteLine("\nInvalid option. Please enter an integer value.\n");
                }
            }
        }


        //Qu8
        void DisplayMonthlyChargedAmounts(List<Order> orders)
        {
            bool InvalidYear = false;
            int year = 0;
            int currentYear = DateTime.Now.Year;

            do
            {
                Console.Write("Enter the year: ");
                if (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Enter a valid integer.");
                    InvalidYear = true;
                }
                else if (year > currentYear)
                {
                    Console.WriteLine("Date cannot be in the future. Please try again.");
                    InvalidYear = true;
                }
                else
                {
                    InvalidYear = false;
                }
            } while (InvalidYear);

            // Initialize a dictionary to store monthly totals
            Dictionary<int, List<Order>> ordersInYear = new Dictionary<int, List<Order>>();

            // Filter orders for the inputted year
            /*
            foreach (Order order in orders)
            {
                foreach (Order historyOrder in order.OrderHistory)
                {
                    DateTime? timeReceived = historyOrder.TimeReceived;
                    if (timeReceived.HasValue && timeReceived.Value.Year == year)
                    {
                        int month = timeReceived.Value.Month;

                        // If the month is not already a key in the dictionary, add it
                        if (!ordersInYear.ContainsKey(month))
                        {
                            ordersInYear[month] = new List<Order>();
                        }

                        // Add the order to the list for that month
                        ordersInYear[month].Add(historyOrder);
                    }
                }
            }
            */

            // Initialize an array to store monthly totals
            double[] monthTotals = new double[12];

            // Compute monthly charged amounts breakdown and total charged amounts
            foreach (var monthOrdersPair in ordersInYear)
            {
                int month = monthOrdersPair.Key;
                double monthTotal = monthOrdersPair.Value.Sum(order => order.CalculateTotal());
                monthTotals[month - 1] = monthTotal;
            }

            // Display the results
            Console.WriteLine();
            for (int month = 0; month < 12; month++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1);
                Console.WriteLine($"{monthName} {year}: ${monthTotals[month].ToString("0.00")}");
            }
            Console.WriteLine();
            Console.WriteLine($"Total: ${monthTotals.Sum().ToString("0.00")}");
        }

    }
}