using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LotteryTcketInfo
    {
        private int lotteryTcketID;

        public int LotteryTcketID
        {
            get { return lotteryTcketID; }
            set { lotteryTcketID = value; }
        }
        private string lotteryTcket;

        public string LotteryTcket
        {
            get { return lotteryTcket; }
            set { lotteryTcket = value; }
        }
        private string lotteryTcketNmber;

        public string LotteryTcketNmber
        {
            get { return lotteryTcketNmber; }
            set { lotteryTcketNmber = value; }
        }
        private string bigRate;

        public string BigRate
        {
            get { return bigRate; }
            set { bigRate = value; }
        }
        private string smallRate;

        public string SmallRate
        {
            get { return smallRate; }
            set { smallRate = value; }
        }
        private string singleRate;

        public string SingleRate
        {
            get { return singleRate; }
            set { singleRate = value; }
        }
        private string doubleRate;

        public string DoubleRate
        {
            get { return doubleRate; }
            set { doubleRate = value; }
        }
        private string timer;

        public string Timer
        {
            get { return timer; }
            set { timer = value; }
        }
        private string explain;

        public string Explain
        {
            get { return explain; }
            set { explain = value; }
        }
    }
}
