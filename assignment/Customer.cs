using System;
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
            get => orderHistory; set => orderHistory = value;
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
            currentOrder = new Order();
            OrderHistory = new List<Order>();
            Rewards = new PointCard();
        }
        public Order MakeOrder()
        {
            // do things
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
            return $"Name: {Name}, Member ID: {MemberId}, DoB: {Dob}\nCurrent Order is: {currentOrder}Order History is:\n{h}Point Card Details: {Rewards.ToString()}";
        }
    }
}
