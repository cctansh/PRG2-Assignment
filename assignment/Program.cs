using assignment;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

List<Customer> customerList = new List<Customer>();
Queue<Order> regularOrders = new Queue<Order>();
Queue<Order> goldOrders = new Queue<Order>();

// Q1: List all customers
void DisplayCustomers()
{

}

// Q2: List all current orders
void DisplayOrderQueues()
{
    if (regularOrders.Count > 0)
    {
        Console.WriteLine("Current orders in regular queue:");
        foreach (var order in regularOrders)
        {
            int i = 1;
            Console.WriteLine($"Order {i}: " + order.ToString());
            i++;
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("No orders in regular queue.");
    }
    if (goldOrders.Count > 0)
    {
        Console.WriteLine("Current orders in gold members queue:");
        foreach (var order in goldOrders)
        {
            int i = 1;
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
void DisplayCustomerOrder()
{
    DisplayCustomers();
    while (true)
    {
        try
        {
            // user selects customer
            Console.Write("Select a customer: ");
            int icIndex = int.Parse(Console.ReadLine());
            Customer selectedC = customerList[icIndex - 1];

            Console.WriteLine();

            // display customer current order
            try
            {
                Console.WriteLine("Current Order: ");
                Console.WriteLine(selectedC.CurrentOrder.ToString());
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No current order.");
            }

            Console.WriteLine();

            // display customer past orders
            if (selectedC.OrderHistory.Count > 0)
            {
                Console.WriteLine("Past Orders: ");
                foreach (Order order in selectedC.OrderHistory)
                {
                    int i = 1;
                    Console.WriteLine($"Order {i}: " + order.ToString());
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No past orders.");
            }

            // all code executed successfully, break infinite loop
            break;
        }
        // if user entered non-int value
        catch (FormatException)
        {
            Console.WriteLine("Invalid option. Please enter an integer value.");
        }
        // if user selected a number not within customer list range
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine($"Invalid option. Please enter a number from 1 to {customerList.Count}");
        }
    }
}

// Q6: Modify order details


// print main option menu
void DisplayMenu()
{

}