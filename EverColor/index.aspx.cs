using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Sockets;
using System.Xml;

using Model;
using BLL;
using Simplemaster;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;

namespace CaiPiao
{
    public partial class index : System.Web.UI.Page
    {
        static Double userjif = 0; // 用户积分
        static DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btn1.Click += btn1_Click;
            SView();
            ThreadInfo();
        }

        public void SView()
        {
            //判断是否可以投注
            if (BLL.UserMessage.SelectSateState() == 0)
            {
                Button8.Enabled = false;
            }
            else if (BLL.UserMessage.SelectSateState() == 1)
            {
                Button8.Enabled = true;
            }
        }
        //登录事件
        private void btn1_Click(object sender, EventArgs e)
        {
            UserInfo user = new UserInfo();
            user.UserName = UseTex.Text;
            if (IsValidUserName(user.UserName)== false)
            {
                Response.Write("<script type='text/javascript'>alert('登录失败,请填写正确的用户名');</script>");
                return;
            }
            user.UserPass = PasTex.Text;
            
            if (IsValidPassword(user.UserPass) == true)
            {
                Response.Write("<script type='text/javascript'>alert('登录失败,请填写正确的密码');</script>");
                return;
            }
            if (UserMessage.Login(user) == 1)
            {
                Session["User"] = user.UserName;
                string integral = UserMessage.SelectUserInt(user).ToString();
                LoginBTN.Attributes["data-target"] = "#login";
                LoginBTN.InnerText = "您好，您当前积分为:" + integral+"。点击查看操作历史";
                userjif = Convert.ToDouble(integral);
                user.UserID= BLL.UserMessage.selectID(user);
                dataTable = BLL.UserMessage.Selectlishi(user).Tables[0];
                GridView1.DataSource= BLL.UserMessage.Selectlishi(user);

                GridView1.DataBind();
                Label3.Text = "";
                if (BLL.UserMessage.AdminLogin(user)==0)
                {
                    Response.Redirect("~/admin/adminPanel.aspx");
                }

            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('登录失败,请重新登录');</script>");
            }
        }
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string stringValue)
        {
            return Encoding.Default.GetBytes(stringValue).Length;
        }
        /// <summary>
        /// 检测用户名格式是否有效
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsValidUserName(string userName)
        {
            int userNameLength = GetStringLength(userName);
            if (Regex.IsMatch(userName, @"^([\u4e00-\u9fa5A-Za-z_0-9]{0,})$"))
            {   // 判断用户名的长度（4-20个字符）及内容（只能是汉字、字母、下划线、数字）是否合法
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 密码有效性
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^[A-Za-z_0-9]{6,16}$");
        }
        /// <summary>
        /// 获取北京网络时间
        /// </summary>
        /// <returns></returns>
        public static string GetNetDateTime()
        {
           
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                { if (h == "Date") { datetime = headerCollection[h]; } }
                return datetime;
            }
            catch (Exception) { return datetime; }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }


        /// <summary>
        /// 获取网页源码
        /// </summary>
        /// <param name="url">要获取到的网页网址</param>
        /// <param name="encoding">网址编码格式</param>
        /// <returns></returns>
        public static string GetHTML(string url, string encoding)
        {
            WebClient web = new WebClient();
            Encoding enc = Encoding.GetEncoding(encoding);
            web.Encoding = enc;
            string buffer;
            try
            {
                buffer = web.DownloadString(url);

            }
            catch (Exception e)
            {
                buffer = null;

            }
            return buffer;
        }

        //////////////////////////////////////////////

        ///数据库获取特定时间的时时彩开奖号码和时间
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetDataBySQL()
        {
          
            DataSet ds = BLL.UserMessage.SelectNewLotterys();
            DataTable dt = ds.Tables[0];
            string wb = @"<?xml version='1.0' encoding='gb2312' ?><xml>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wb += "<row  expect='" + dt.Rows[i]["LotteryTcket"] + "' opencode='" + dt.Rows[i]["LotteryTcketNmber"] + "' opentime='" + dt.Rows[i]["Timer"] + "'/>";
            }

            wb += "</xml>";
            return wb;
        }


        //获取网络时间
        static string intertime = "";
        //获取网络时间和开奖时间
        [WebMethod]
        public static string ShowTimeBySQL()
        {
            DateTime Dt = DateTime.Now;
            string dt = Dt.ToString("yyyyMMdd");
            //最新开奖时间
            string a ;

            DataSet ds = BLL.UserMessage.SelectNewLotterys();
            DataTable datat = ds.Tables[0];
            if (datat.Rows.Count == 0)
            {
                a = DateTime.Now.ToString();
            }
            else
            {
                //开奖时间
                 a = datat.Rows[1]["Timer"].ToString();
            }
            string NetTime = GetNetDateTime();
            DateTime Te;

            if (DateTime.TryParse(NetTime,out Te))
            {
                intertime=Te.ToString("yyyy-MM-dd HH:mm:ss")+ "/" + a;
                //网络时间
            }
            else
            {
                intertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "/" + a;
                //系统时间
            }
            return intertime;
        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('请登录');</script>");
                return;
            }
            Label1.Text = "大";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('请登录');</script>");
                return;
            }
            Label1.Text = "小";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('请登录');</script>");
                return;
            }
            Label1.Text = "单";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('请登录');</script>");
                return;
            }
            Label1.Text = "双";
        }
        static int jif = 0;//积分
        protected void Button5_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('请登录');</script>");
                return;
            }
            if (jif==0)
            {
                Label3.Text = "已经是最小数";
            }
            if (jif>=1)
            {
                jif = jif - 1;

            }
            Label2.Text = jif.ToString();
            Button6.Text = jif.ToString();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('请登录');</script>");
                return;
            }

            if (jif + 1 <= userjif)
            {
                jif = jif + 1;
            }
            else if (jif + 1 > userjif)
            {
                Response.Write("<script type='text/javascript'>alert('您的积分不够，请联系管理员充值');</script>");
            }


            Label2.Text = jif.ToString();
            Button6.Text = jif.ToString();
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            
            if (Session["User"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('请登录');</script>");
                return;
            }
            //if (jif==Convert.ToInt16(Label2.Text))
            if ("暂未选择" == Label2.Text || jif != Convert.ToInt16(Label2.Text))
            {
                Response.Write("<script type='text/javascript'>alert('请加注');</script>");
                return;
            }
            if (Label1.Text=="")
            {
                Response.Write("<script type='text/javascript'>alert('请选注');</script>");
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = Session["User"].ToString();
            

            BuyInfo buyInfo = new BuyInfo();
            DataSet ds = BLL.UserMessage.SelectNewLotterys();
            DataTable dt = ds.Tables[0];
            long QHnumberss =  Convert.ToInt64( dt.Rows[0][0].ToString());
            //if (Regex.IsMatch(QHnumberss.ToString(), @"^[0-9]*$") == true)
            //{
            //    Response.Write("<script type='text/javascript'>alert('数据获取中');</script>");
            //    return;
            //}
            buyInfo.LotteryTcketID = QHnumberss.ToString();
            buyInfo.UserID= BLL.UserMessage.AddselectID(userInfo);
            buyInfo.NtesItegration = Convert.ToInt16(Label2.Text);
            if (Label1.Text == "大")
            {
                buyInfo.NtesID =1;
            }
            if (Label1.Text == "小")
            {
                buyInfo.NtesID = 2;
            }
            if (Label1.Text == "单")
            {
                buyInfo.NtesID = 3;
            }
            if (Label1.Text == "双")
            {
                buyInfo.NtesID = 4;
            }
            buyInfo.NtesGade = 1;
            buyInfo.NtesTimer=  Convert.ToDateTime(GetNetDateTime()).ToString();
            if (BLL.UserMessage.ADDbuy(buyInfo)==1)
            {
                userInfo.UserIntegration = Convert.ToInt16(UserMessage.SelectUserInt(userInfo).ToString()) - Convert.ToInt16(Label2.Text);
                if (BLL.UserMessage.jianUserInt(userInfo) == 1)
                {
                    Response.Write("<script type='text/javascript'>alert('加注成功');</script>");
                    LoginBTN.InnerText = "当前积分为:" + UserMessage.SelectUserInt(userInfo).ToString();
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('加注失败');</script>");
            }
        }
        public void ThreadInfo()
        {
            int n = 2;

            ThreadStart info = new ThreadStart(Simplemaster.GetData.Updata);
            Thread[] infoStart = new Thread[n]; //(info);
            for (int i = 0; i < n; i++)
            {
                infoStart[i] = new Thread(info);
                infoStart[i].Start();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            ThreadInfo();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = dataTable;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            login.Style["display"] = "block";
        }
    }
}