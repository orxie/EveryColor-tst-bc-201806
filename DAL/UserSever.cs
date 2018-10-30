using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Interfaces;
using Model;

namespace DAL
{
    public class UserSever:InterfaceUser
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;
        public int Login(UserInfo userInfo)
        {

            string sqltext = "select count(*) from UserInfo where UserName=@UserName and UserPass=@UserPass";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName),
                    new SqlParameter("@UserPass", userInfo.UserPass)

                };
            return (int)SQLHelper.ExecuteNonQuery(this.connection, CommandType.Text, sqltext, para);
        }
    }
}
