using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Net;

namespace Demo
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SateInfo sateInfo = new SateInfo();
            int qw = BLL.UserMessage.SelectSate();
            if (qw == 0)
            {
                Button6.Text = "开始";
            }
            if (qw == 1)
            {
                Button6.Text = "结束";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text=="")
            {
                return;
            }
            if (TextBox2.Text == "")
            {
                return;
            }
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = TextBox1.Text;
            userinfo.UserPass = TextBox2.Text;
            //登录判断
            if (BLL.UserMessage.Login(userinfo) == 1) {
                //权限判断
                if (BLL.UserMessage.Login(userinfo) == 0)
                {
                    Session["login"]= TextBox1.Text;
                }
                else
                {

                }
            }
            else
            {
                
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = TextBox3.Text;
            userinfo.UserPass = TextBox4.Text;
            userinfo.UserInvite = TextBox5.Text;
            userinfo.UserAuthority = DropDownList1.SelectedIndex;
            userinfo.UserIntegration = Convert.ToInt16(TextBox6.Text);
            //用户名查重
            if (BLL.UserMessage.AddUserName(userinfo) == 0)
            {
                //成功判断
                if (BLL.UserMessage.AdminAddUser(userinfo) == 1)
                {
                    NewMethod();
                    //权限判断  添加邀请码
                    if (userinfo.UserAuthority == 1)
                    {
                        Random rd = new Random();
                        
                        InvitelInfo invitelInfo = new InvitelInfo();
                        invitelInfo.Invite= rd.Next(100000,1000000);
                        invitelInfo.UserID = BLL.UserMessage.AddselectID(userinfo);
                        //添加
                        if (BLL.UserMessage.AddinvitelID(invitelInfo)==1)
                        {
                            //成功
                        }
                        
                    }
                    else
                    {
                        //添加邀请码失败
                    }
                }
                else
                {
                    //注册失败
                }
            }
            else
            {
               //用户名重复
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = TextBox7.Text;
            GridView1.DataSource = BLL.UserMessage.SelectoneUser(userInfo);
            GridView1.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (TextBox8.Text=="")
            {
                //
                return;
            }
            if (Convert.ToInt16(TextBox8.Text) < 0)
            {
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserIntegration = Convert.ToInt16(TextBox8.Text) + Convert.ToInt16(GridView1.Rows[0].Cells[5].Text);
            userInfo.UserID= Convert.ToInt16(GridView1.Rows[0].Cells[0].Text);
            if (BLL.UserMessage.UPdateUserIntegration(userInfo)==1)
            {
                NewMethod();
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (TextBox9.Text == "")
            {
                //
                return;
            }
            if (Convert.ToInt16(TextBox9.Text)<0)
            {
                return;
            }
            if (Convert.ToInt16(GridView1.Rows[0].Cells[5].Text) > Convert.ToInt16(TextBox9.Text))
            {
                return;
            }
            UserInfo userInfo = new UserInfo();

            userInfo.UserIntegration =  Convert.ToInt16(GridView1.Rows[0].Cells[5].Text)- Convert.ToInt16(TextBox9.Text);
            userInfo.UserID = Convert.ToInt16(GridView1.Rows[0].Cells[0].Text);
            if (BLL.UserMessage.UPdateUserIntegration(userInfo) == 1)
            {
                NewMethod();
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            SateInfo sateInfo = new SateInfo();
            int qw= BLL.UserMessage.SelectSate();
            if (qw==0)
            {
                
                string dt = GetNetDateTime();
                sateInfo.SateDate = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateBgin = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateEnd = "";
                sateInfo.SateRmarks = "";
                sateInfo.SateState = 1;
                if (BLL.UserMessage.UPdateSate(sateInfo)==1)
                {
                    Button6.Text = "结束";
                }
            }
            if (qw==1)
            {
                
                string dt = GetNetDateTime();
                sateInfo.SateDate = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateEnd = Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
                sateInfo.SateBgin ="";
                sateInfo.SateRmarks = "";
                sateInfo.SateState = 0;
                if (BLL.UserMessage.UPdateSate(sateInfo) == 1)
                {
                    Button6.Text = "开始";
                }
            }
        }
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

        protected void Button7_Click(object sender, EventArgs e)
        {
            if (Session["login"].ToString()=="")
            {
                return;
            }
            if (TextBox10.Text=="")
            {
                return;
            }
            if (TextBox11.Text == "" || TextBox12.Text == "")
            {
                return;
            }
            if (TextBox11.Text != TextBox12.Text)
            {
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = Session["login"].ToString();
            userInfo.UserPass = TextBox10.Text;
            if (BLL.UserMessage.Login(userInfo) == 1)
            {
                userInfo.UserName = Session["login"].ToString();
                userInfo.UserPass = TextBox12.Text;
                if (BLL.UserMessage.UPDataPass(userInfo) == 1)
                {
                    //修改成功
                }
                else
                {
                    //修改失败
                }
            }
               

        }
    }
}