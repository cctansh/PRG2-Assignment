﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Customer
    {
        private string name;
        public string Name
        {
            get => name; set => name = value;
        }
        private int memberId;
        public int MemberId
        {
            get => memberId; set => memberId = value;
        }
        private DateTime dob;
        public DateTime Dob
        {
            get => dob; set => dob = value;
        }
        private Order currentOrder;
        public Order CurrentOrder
        {
            get => currentOrder; set => currentOrder = value;
        }
        private List<Order> orderHistory;
        public List<Order> OrderHistory
        {
            get => orderHistory ?? (orderHistory = new List<Order>());
            set => orderHistory = value;
        }
        private PointCard rewards;
        public PointCard Rewards
        {
            get => rewards; set => rewards = value;
        }
        public Customer() { }
        public Customer(string n, int m, DateTime d)
        {
            Name = n;
            MemberId = m;
            Dob = d;
            currentOrder = null;
            OrderHistory = new List<Order>();
            Rewards = new PointCard(0, 0);
        }
        public Order MakeOrder()
        {
            string o;

            // id is temp id, to modify in program based on orderList
            Order cOrder = new Order(0, DateTime.Now);

            // Ensure that orderHistory is not null
            if (OrderHistory == null)
            {
                OrderHistory = new List<Order>();
            }

            while (true)
            {
                Console.WriteLine("----------------");
                Console.WriteLine("Adding ice cream");
                Console.WriteLine("----------------");
                Console.WriteLine();
                IceCream ic = Program.CreateIceCream();
                cOrder.AddIceCream(ic);
                while (true)
                {
                    Console.Write("Would you like to add another ice cream to your order? (Y/N): ");
                    o = Console.ReadLine();
                    Console.WriteLine();
                    if (o.ToUpper() == "Y" || o.ToUpper() == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter Y or N.\n");
                    }
                }
                if (o.ToUpper() == "N")
                {
                    Console.WriteLine("Order created.");
                    // Add the created order to the order history
                    OrderHistory.Add(cOrder);
                    return cOrder;
                }
            }
        }
        public bool IsBirthday()
        {
            if (Dob == DateTime.Today)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            string h = "";
            foreach (Order o in orderHistory)
            { 
                h += o.ToString();
            }
            return $"Name: {Name}\nMember ID: {MemberId}\nDoB: {Dob.ToString("dd/MM/yyyy")}\nTier: {Rewards.Tier}";
        }
    }
}
