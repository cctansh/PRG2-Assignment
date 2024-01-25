﻿using assignment;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Metadata.Ecma335;
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
            List<Order> orderList = new List<Order>();

            InitialiseCustomers(customerList);

            while (true)
            {
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
                    RegisterNewCustomer();
                }
                else if (o == "4")
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine("Create a customer's order");
                    Console.WriteLine("=========================");
                    Console.WriteLine();
                    // method goes here
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
                }
                else if (o == "0")
                {
                    Console.WriteLine("======================================");
                    Console.WriteLine("Thank you for shopping at I.C. Treats!");
                    Console.WriteLine("======================================");
                    break;
                }
                else
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
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Current orders in regular queue");
                Console.WriteLine("-------------------------------");
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
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Current orders in gold members queue");
                Console.WriteLine("------------------------------------");
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
        }

        // Q3:
        static void RegisterNewCustomer()
        {
            Console.WriteLine("Register a new customer:");

            // Prompt user for information
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter ID number: ");
            int memberId = int.Parse(Console.ReadLine());

            Console.Write("Enter date of birth (e.g., MM/dd/yyyy): ");
            DateTime dob = DateTime.Parse(Console.ReadLine());

            // Create a new customer object
            Customer newCustomer = new Customer
            {
                Name = name,
                MemberId = memberId,
                Dob = dob,
                Rewards = new PointCard(0, 0),  // Creating a new PointCard for the customer
            };

            // Append customer information to customers.csv file
            AppendCustomerToCsv(newCustomer);

            Console.WriteLine("Customer registered successfully!");
        }

        static void AppendCustomerToCsv(Customer customer)
        {
            // Specify the path to the CSV file
            string csvFilePath = "customers.csv";

            // Append customer information to the file
            using (StreamWriter writer = File.AppendText(csvFilePath))
            {
                int i = 1;
                writer.WriteLine($"[{i}] " + customer.ToString());
                i++;
            }
        }

        // Q4:
        //for testing
        static void MakeCOrder(List<Customer> customerList, List<Order> orderList, Queue<Order> regQ, Queue<Order> goldQ)
        {
            int cIndex = SelectCustomer(customerList);

            Customer selectedC = customerList[cIndex - 1];

            Console.WriteLine();

            Console.WriteLine("------------");
            Console.WriteLine("Making Order");
            Console.WriteLine("------------");
            Console.WriteLine();
            Order newO = selectedC.MakeOrder();

            newO.Id = orderList.Count + 1;
            customerList[cIndex - 1].CurrentOrder = newO;
            orderList.Add(newO);
            if (selectedC.Rewards.Tier == "Gold")
            {
                goldQ.Enqueue(newO);
            }
            else
            {
                regQ.Enqueue(newO);
            }

            // all code executed successfully, end method
            return;
        }


        // Q5: Display order details of a customer
        static void DisplayCustomerOrder(List<Customer> customerList)
        {
            int cIndex = SelectCustomer(customerList);
            
            Customer selectedC = customerList[cIndex - 1];

            Console.WriteLine();

            // display customer current order
            if (selectedC.CurrentOrder != null)
            {
                Console.WriteLine("-------------");
                Console.WriteLine("Current Order");
                Console.WriteLine("-------------");
                Console.WriteLine();
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
                Console.WriteLine("------------");
                Console.WriteLine("Past Orders:");
                Console.WriteLine("------------");
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

            // all code executed successfully, end method
            return;
        }

        // Q6: Modify order details
        static void ModifyCustomerOrder(List<Customer> customerList)
        {
            int cIndex = SelectCustomer(customerList);

            Customer selectedC = customerList[cIndex - 1];

            Console.WriteLine();

            // checking if there is current order
            // if no, print message and go to return
            if (selectedC.CurrentOrder == null)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("No current order. Make an order using [4] Create Order.");
                Console.WriteLine("-------------------------------------------------------");
            }
            // if yes, execute this code
            else
            {
                Order cOrder = selectedC.CurrentOrder;

                string option = DisplayModifyOrderMenu(cOrder);

                if (option == "1")
                {
                    // select ice cream
                    int icIndex = ChooseIceCream(cOrder.IceCreamList);

                    // modify selected ice cream
                    cOrder.ModifyIceCream(icIndex);
                }
                else if (option == "2")
                {
                    IceCream newIC = CreateIceCream();
                    cOrder.AddIceCream(newIC);
                    Console.WriteLine("----------------");
                    Console.WriteLine("Ice cream added.");
                    Console.WriteLine("----------------");
                }
                // if option == 3 (already data validated in method)
                else
                {
                    if (cOrder.IceCreamList.Count == 1)
                    {
                        Console.WriteLine("You cannot have 0 ice creams in the order!");
                    }
                    else
                    {
                        int icIndex = ChooseIceCream(cOrder.IceCreamList);
                        cOrder.DeleteIceCream(icIndex);
                        Console.WriteLine("------------------");
                        Console.WriteLine("Ice cream deleted.");
                        Console.WriteLine("------------------");
                    }
                }

                Console.WriteLine();

                // update customer order and display
                selectedC.CurrentOrder = cOrder;
                customerList[cIndex - 1] = selectedC;
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
            Console.WriteLine("---------------------------");
            Console.WriteLine("Ice creams in current order");
            Console.WriteLine("---------------------------");
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

            while (true)
            {
                Console.Write("What would you like to do?: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                // checks if valid option, repeating if not
                if (option == "1" || option == "2" || option == "3")
                {
                    return option;
                }
                else
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
                    Console.Write("Please choose your ice cream: ");
                    int option = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (option <= 0 || option > icList.Count)
                    {
                        Console.WriteLine($"Invalid option. Please enter a number from 1 to {icList.Count}.");
                        Console.WriteLine();
                    }
                    else
                    {
                        return option;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid option. Please enter an integer value.\n");
                }
            }
        }

        public static IceCream CreateIceCream()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Ice Cream Options");
            Console.WriteLine("-----------------");
            Console.WriteLine("[1] Cup\n[2] Cone\n[3] Waffle");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Choose your ice cream option: ");
                string o = Console.ReadLine();
                Console.WriteLine();
                if (o == "1")
                {
                    int s = ChooseScoops();
                    List<Flavour> fList = ChooseFlavours(s);
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
                        Console.WriteLine();
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
                            Console.WriteLine("Invalid option. Please enter Y or N.\n");
                        }
                    }
                    int s = ChooseScoops();
                    List<Flavour> fList = ChooseFlavours(s);
                    List<Topping> tList = ChooseToppings();
                    return new Cone(s, fList, tList, dipped);
                }
                else if (o == "3")
                {
                    string wf;

                    Console.WriteLine("---------------");
                    Console.WriteLine("Waffle Flavours");
                    Console.WriteLine("---------------");
                    Console.WriteLine("[1] Original\n[2] Red Velvet\n[3] Charcoal\n[4] Pandan");
                    Console.WriteLine();
                    while (true)
                    {
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
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.\n");
                            continue;
                        }
                        break;
                    }
                    int s = ChooseScoops();
                    List<Flavour> fList = ChooseFlavours(s);
                    List<Topping> tList = ChooseToppings();
                    return new Waffle(s, fList, tList, wf);
                }
                else
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
                    Console.Write("Choose your number of scoops (1/2/3): ");
                    int s = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (s < 1 || s > 3)
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, or 3.\n");
                    }
                    else
                    {
                        return s;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid option. Please enter an integer value.\n");
                }
            }
        }

        public static List<Flavour> ChooseFlavours(int s)
        {
            List<Flavour> fList = new List<Flavour>();

            Console.WriteLine("------------------");
            Console.WriteLine("Ice Cream Flavours");
            Console.WriteLine("------------------");
            Console.WriteLine("[1] Vanilla\n[2] Chocolate\n[3] Strawberry\n[4] Durian (Premium)\n[5] Ube (Premium)\n[6] Sea Salt (Premium)");
            Console.WriteLine();
            
            for (int i = 1; i <= s; i++)
            {
                while (true)
                {
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
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter 1, 2, 3, 4, 5, or 6.\n");
                        continue;
                    }
                    break;
                }
            }
            return fList;
        }
        public static void AddFlavour(string flavour, bool premium, List<Flavour> flavours)
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

        public static List<Topping> ChooseToppings()
        {
            List<Topping> toppings = new List<Topping>();
            int oT;

            Console.WriteLine("--------");
            Console.WriteLine("Toppings");
            Console.WriteLine("--------");
            Console.WriteLine("[1] Sprinkles\n[2] Mochi\n[3] Sago\n[4] Oreos");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    Console.Write("How many toppings would you like? (0/1/2/3/4): ");
                    oT = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (oT == 0)
                    {
                        Console.WriteLine("No toppings chosen.\n");
                        return toppings;
                    }
                    else if (oT == 4)
                    {
                        Console.WriteLine("Adding all toppings...\n");
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
                        Console.WriteLine("You may only have 0, 1, 2, 3, or 4 toppings. Please try again.\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid option. Please enter 0, 1, 2, 3, or 4.\n");
                }
            }

            for (int i = 1; i <= oT; i++)
            {
                while (true)
                {
                    Console.Write($"Choose topping {i}: ");
                    string oTT = Console.ReadLine();
                    Console.WriteLine();
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
                        Console.WriteLine("Invalid option. Please enter 1, 2, 3, or 4.\n");
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
                        Console.WriteLine("Cannot have multiple of the same topping.\n");
                        return true;
                    }
                }
            }
            return false;
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
            else
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("No orders in queue.");
                Console.WriteLine("-------------------");
                return;
            }

            int cIndex = 0;
            // finding associated customer
            for (int i = 0; i < cList.Count; i++)
            {
                if (cList[i].CurrentOrder.Id == order.Id)
                {
                    customer = cList[i];
                    cIndex = i; // tracks the customer's index in list
                    break;
                }
            }

            Console.WriteLine($"Customer Name: {customer.Name}\nMember ID: {customer.MemberId}");
            Console.WriteLine();
            Console.WriteLine("-------------------");
            Console.WriteLine("Ice Creams In Order");
            Console.WriteLine("-------------------");

            // display all ice creams and add to bill
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

            // this checks whether bday ice cream is the first ice cream
            int bdayIC = 0;
            // check if bday
            if (customer.IsBirthday())
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine("Happy birthday! As a gift, the most expensive ice cream in your order will be free of charge.");
                Console.WriteLine("---------------------------------------------------------------------------------------------");

                double free = 0;
                // checking for most expensive ice cream, then minus its cost from total
                foreach (IceCream ic in order.IceCreamList)
                {
                    if (ic.CalculatePrice() > free)
                    {
                        free = ic.CalculatePrice();
                        bdayIC++; // if the most expensive ice cream is icecreamlist[0], value will stay at 1. else, > 1
                    }
                }
                total -= free;

                Console.WriteLine();
            }

            // check if punch card complete, and if there are remaining ice creams to give discount to (ie. birthday hasnt already made order free)
            if (customer.Rewards.PunchCard == 10 && total != 0)
            {
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

            // checks if customer can redeem point (is silver or gold member)
            if (customer.Rewards.Tier != "Ordinary")
            {
                while (true)
                {
                    Console.Write("How many of your points would you like to redeem to offset your final bill total? (1pt = $0.02): ");
                    try
                    {
                        int pts = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        double discount = pts * 0.02;
                        if (pts < 0)
                        {
                            Console.WriteLine("You may not enter a negative number. Please try again.\n");
                        }
                        else if (pts > customer.Rewards.Points)
                        {
                            Console.WriteLine($"You don't have enough points for that. Please try again. (Current point total: {customer.Rewards.Points})\n");
                        }
                        else if (discount > total)
                        {
                            Console.WriteLine($"You may not redeem for more than your bill total. Please try again. (Current bill total: ${total:F2}, equivalent to {Math.Floor(total/0.02)} points)\n");
                        }
                        else
                        {
                            total += discount;
                            customer.Rewards.RedeemPoints(pts);
                            break;
                        }
                    }
                    catch (FormatException)
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

            cList[cIndex] = customer;
            return;
        }

        static int SelectCustomer(List<Customer> customerList)
        {
            DisplayCustomers(customerList);
            Console.WriteLine();

            while (true)
            {
                try
                {
                    // user selects customer
                    Console.Write("Select a customer: ");
                    int cIndex = int.Parse(Console.ReadLine());

                    if (cIndex > 0 && cIndex <= customerList.Count)
                    {
                        return cIndex;
                    }

                    Console.WriteLine($"\nInvalid option. Please enter a number from 1 to {customerList.Count}\n");
                }
                // if user entered non-int value
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid option. Please enter an integer value.\n");
                }
            }
        }
    }
}
