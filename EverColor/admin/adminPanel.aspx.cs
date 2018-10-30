using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Model;
using BLL;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace Demo.admin
{
    public partial class adminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StutsForLottery();
            }
        }
        //获取网络时间
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

        protected void Button6_Click(object sender, EventArgs e)
        {
            SateInfo sateInfo = new SateInfo();
            int qw = BLL.UserMessage.SelectSate();
            if (qw == 0)
            {
                string dt = GetNetDateTime();
                sateInfo.SateDate = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateBgin = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateEnd = "";
                sateInfo.SateRmarks = "";
                sateInfo.SateState = 1;
                if (BLL.UserMessage.UPdateSate(sateInfo) == 1)
                {
                    this.status.Text = "开启";
                    Button6.Text = "关闭";
                }
            }
            if (qw == 1)
            {

                string dt = GetNetDateTime();
                sateInfo.SateDate = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateEnd = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateBgin = "";
                sateInfo.SateRmarks = "";
                sateInfo.SateState = 0;
                if (BLL.UserMessage.UPdateSate(sateInfo) == 1)
                {
                    this.status.Text="关闭";
                    Button6.Text = "开启";
                }
            }
        }
        /// <summary>
        /// 彩票开关
        /// </summary>
        private void StutsForLottery() {
            SateInfo sateInfo = new SateInfo();
            int qw = BLL.UserMessage.SelectSate();
            if (qw == 0)
            {
                this.status.Text = "关闭";
                Button6.Text = "开启";
            }
            if (qw == 1)
            {
                this.status.Text = "开启";
                Button6.Text = "关闭";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            NewMethod();
        }
        static int NUser = 0;
        private void NewMethod()
        {
            if (TextBox7.Text == "")
            {
                Response.Write("<script type='text/javascript'>alert('请填写用户名');</script>");
                return;
            }
            if (IsValidUserName(TextBox7.Text) == false)
            {
                Response.Write("<script type='text/javascript'>alert('请填写正确的用户名');</script>");
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = TextBox7.Text;
            GridView1.DataSource = BLL.UserMessage.SelectoneUser(userInfo);
            GridView1.DataBind();
            NUser = BLL.UserMessage.AddUserName(userInfo);
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
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string stringValue)
        {
            return Encoding.Default.GetBytes(stringValue).Length;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (NUser == 0)
            {
                Response.Write("<script type='text/javascript'>alert('请先查询');</script>");
                return;
            }
            if (TextBox8.Text == "")
            {
                Response.Write("<script type='text/javascript'>alert('请输入一个正整数');</script>");
                return;
            }
            if (Convert.ToInt16(TextBox8.Text) < 0)
            {
                Response.Write("<script type='text/javascript'>alert('请输入一个正整数');</script>");
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserIntegration = Convert.ToInt16(TextBox8.Text) + Convert.ToInt16(GridView1.Rows[0].Cells[5].Text);
            userInfo.UserID = Convert.ToInt16(GridView1.Rows[0].Cells[0].Text);
            if (BLL.UserMessage.UPdateUserIntegration(userInfo) == 1)
            {
                Response.Write("<script type='text/javascript'>alert('操作成功');</script>");
                NewMethod();
            }
            else {
                Response.Write("<script type='text/javascript'>alert('操作失败');</script>");
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (NUser == 0)
            {
                Response.Write("<script type='text/javascript'>alert('请先查询');</script>");
                return;
            }
            if (TextBox9.Text == "")
            {
                Response.Write("<script type='text/javascript'>alert('请输入一个正整数');</script>");
                return;
            }
            if (Convert.ToInt16(TextBox9.Text) < 0)
            {
                Response.Write("<script type='text/javascript'>alert('请输入一个正整数');</script>");
                return;
            }
            if (Convert.ToInt16(GridView1.Rows[0].Cells[5].Text) > Convert.ToInt16(TextBox9.Text))
            {
                Response.Write("<script type='text/javascript'>alert('积分不足');</script>");
                return;
            }
            UserInfo userInfo = new UserInfo();

            userInfo.UserIntegration = Convert.ToInt16(GridView1.Rows[0].Cells[5].Text) - Convert.ToInt16(TextBox9.Text);
            userInfo.UserID = Convert.ToInt16(GridView1.Rows[0].Cells[0].Text);
            if (BLL.UserMessage.UPdateUserIntegration(userInfo) == 1)
            {
                Response.Write("<script type='text/javascript'>alert('操作成功');</script>");
                NewMethod();
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('操作失败');</script>");
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            if (NUser == 0)
            {
                Response.Write("<script type='text/javascript'>alert('请先查询');</script>");
                return;
            }
            if (TextBox1.Text == "")
            {
                Response.Write("<script type='text/javascript'>alert('请填写正确的密码');</script>");
                return;
            }
            if (IsValidPassword(TextBox1.Text) == true)
            {
                Response.Write("<script type='text/javascript'>alert('请填写正确的密码');</script>");
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = GridView1.Rows[0].Cells[1].Text;
            userInfo.UserPass = TextBox1.Text;
            if (BLL.UserMessage.UPDataPass(userInfo) == 1)
            {
                Response.Write("<script type='text/javascript'>alert('修改成功');</script>");
                NewMethod();
            }
            else {
                Response.Write("<script type='text/javascript'>alert('修改失败');</script>");
                NewMethod();
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
    }
}