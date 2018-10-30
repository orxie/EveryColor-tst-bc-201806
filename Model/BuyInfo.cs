using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BuyInfo
    {
        private int buyID;

        public int BuyID
        {
            get { return buyID; }
            set { buyID = value; }
        }
        private int userID;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string lotteryTcketID;

        public string LotteryTcketID
        {
            get { return lotteryTcketID; }
            set { lotteryTcketID = value; }
        }
        private int ntesItegration;

        public int NtesItegration
        {
            get { return ntesItegration; }
            set { ntesItegration = value; }
        }
        private int ntesID;

        public int NtesID
        {
            get { return ntesID; }
            set { ntesID = value; }
        }
        private int ntesGade;

        public int NtesGade
        {
            get { return ntesGade; }
            set { ntesGade = value; }
        }
        private string ntesTimer;

        public string NtesTimer
        {
            get { return ntesTimer; }
            set { ntesTimer = value; }
        }


    }
}
