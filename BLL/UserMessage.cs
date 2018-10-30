
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class UserMessage
    {
        public static int Login(UserInfo userinfo)
        {
            return DAL.UserServer.Login(userinfo);
        }

        public static int AdminLogin(UserInfo userinfo)
        {
            return DAL.UserServer.AdminLogin(userinfo);
        }

        public static int AddUserName(UserInfo userinfo)
        {
            return DAL.UserServer.AddUserName(userinfo);
        }

        public static int AdminAddUser(UserInfo userinfo)
        {
            return DAL.UserServer.AdminAddUser(userinfo);
        }

        public static int AddselectID(UserInfo userinfo)
        {
            return DAL.UserServer.AddselectID(userinfo);
        }

        public static int AddinvitelID(InvitelInfo invitelInfo)
        {
            return DAL.UserServer.AddinvitelID(invitelInfo);
        }

        public static DataSet SelectoneUser(UserInfo Userinfo)
        {
            return DAL.UserServer.SelectoneUser(Userinfo);
        }

        public static int UPdateUserIntegration(UserInfo Userinfo)
        {
            return DAL.UserServer.UPdateUserIntegration(Userinfo);
        }

        public static int UPDataPass(UserInfo Userinfo)
        {
            return DAL.UserServer.UPDataPass(Userinfo);
        }

        public static int SelectSate()
        {
            return DAL.UserServer.SelectSate();
        }

        public static int UPdateSate(SateInfo sateInfo)
        {
            return DAL.UserServer.UPdateSate(sateInfo);
        }

        public static int ADDSate(LotteryTcketInfo lotteryTcketInfo)
        {
            return DAL.UserServer.ADDSate(lotteryTcketInfo);
        }
        ///////////////////////////////////////////
        /// <summary>
        /// 查询账号积分
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string SelectUserInt(UserInfo userInfo)
        {
            return DAL.UserServer.SelectUserInt(userInfo);
        }
        /// <summary>
        /// 查询最新的开奖期号
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string SelectNewLotteryTcket()
        {
            return DAL.UserServer.SelectNewLotteryTcket();
        }
        /// <summary>
        /// 修改最新一期的时间和开奖号码
        /// </summary>
        /// <param name="lotteryTcketInfo"></param>
        /// <returns></returns>
        public static int UpNewNum(LotteryTcketInfo lotteryTcketInfo)
        {
            return DAL.UserServer.UpNewNum(lotteryTcketInfo);
        }
        /// <summary>
        /// 查询当天的开奖信息
        /// </summary>
        /// <param name="lotteryTcketInfo"></param>
        /// <returns></returns>
        public static DataSet SelectNewLotterys()
        {
            return DAL.UserServer.SelectNewS();
        }
        public static int SelectSateState()
        {
            return DAL.UserServer.SelectSateState();
        }
        public static int ADDbuy(BuyInfo buyInfo)
        {
            return DAL.UserServer.ADDbuy(buyInfo);
        }

        public static int jianUserInt(UserInfo userInfo)
        {
            return DAL.UserServer.jianUserInt(userInfo);
        }

        public static int selectID(UserInfo userInfo)
        {
            return DAL.UserServer.selectID(userInfo);
        }
    
        public static DataSet Selectlishi(UserInfo userInfo)
        {
            return DAL.UserServer.Selectlishi(userInfo);
        }

        public static DataSet SelectMYUser(UserInfo userInfo)
        {
            return DAL.UserServer.SelectMYUser(userInfo);
        }

        public static DataSet AdminSelectMYUser()
        {
            return DAL.UserServer.AdminSelectMYUser();
        }
    }
}
