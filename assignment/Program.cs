using assignment;
using System.Collections.Generic;

List<Customer> customerList = new List<Customer>();
Queue<Order> regularOrders = new Queue<Order>();
Queue<Order> goldOrders = new Queue<Order>();

// Q1: List all customers
void DisplayCustomers()
{

}

// Q2: List all current orders
void DisplayOrders()
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
    Console.WriteLine("Select a customer: ");
}

// Q6: Modify order details


// print main option menu
void DisplayMenu()
{

}