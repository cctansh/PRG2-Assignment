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
        public PointCard(int points, int punchCard, string tier)
        {
            Points = points;
            PunchCard = punchCard;
            Tier = tier;
        }
        public int AddPoints()
        {
            return Math.Floor(totalSpent * 0.72);
        }
        public int RedeemPoints()
        {
            return usePoint * 0.02;
        }
        public Punch() { }

        
    }
}
