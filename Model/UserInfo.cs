using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserInfo
    {
        private int userID;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string userPass;

        public string UserPass
        {
            get { return userPass; }
            set { userPass = value; }
        }
        private string userInvite;

        public string UserInvite
        {
            get { return userInvite; }
            set { userInvite = value; }
        }
        private int userAuthority;

        public int UserAuthority
        {
            get { return userAuthority; }
            set { userAuthority = value; }
        }
        private int userIntegration;

        public int UserIntegration
        {
            get { return userIntegration; }
            set { userIntegration = value; }
        }

    }
}
