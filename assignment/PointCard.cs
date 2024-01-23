using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;

        public int Points
        {
            get { return points; }
            set { points = value; }
        } 
        public int PunchCard
        {
            get { return punchCard; }
            set { punchCard = value; }
        }
        public string Tier
        {
            get { return tier; }
            set { tier = value; }
        }

        public PointCard() { }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50)
            {
                Tier = "Silver";
            }
            else
            {
                Tier = "Ordinary";
            }
        }
  
        public void AddPoints(int i)
        {
            Points += i;
        }
        public void RedeemPoints(int i)
        {
           Points -= i;
        }
        public void Punch()
        {
            PunchCard++;
            if (PunchCard == 11)
            {
                PunchCard = 10;
            }

        }

        public override string ToString()
        {
            return $"Current Points is {Points}, your punchcard has {PunchCard} punches and your Membership Tier is {Tier}";
        }


    }
}
