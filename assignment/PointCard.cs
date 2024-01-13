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
            Tier = "Ordinary";

        }
        public void AddPoints()
        {
            Points += Points;
        }
        public void RedeemPoints()
        {
           Points -= Points;
        }
        public void Punch()
        {
            PunchCard++;
            if (PunchCard == 11)
            {
                PunchCard = 0;
            }
            
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
