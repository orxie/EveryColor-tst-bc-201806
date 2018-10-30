using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace Demo
{
    public partial class userPanelAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    Username.Text = Session["User"].ToString();
                }
            }
        }

        protected void confirmChange_Click(object sender, EventArgs e)
        {
            if (OldPwd.Text == "")
            {
                Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "alert('必填项不能为空!')");
                return;
            }
            if (NewPwd.Text == "" || ConfirmNewPwd.Text == "")
            {
                Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "alert('必填项不能为空!')");
                return;
            }
            if (NewPwd.Text != ConfirmNewPwd.Text)
            {
                Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "alert('两次密码不同!')");
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = Session["login"].ToString();
            userInfo.UserPass = OldPwd.Text;
            if (BLL.UserMessage.Login(userInfo) == 1)
            {
                userInfo.UserName = Session["login"].ToString();
                userInfo.UserPass = NewPwd.Text;
                if (BLL.UserMessage.UPDataPass(userInfo) == 1)
                {
                    //修改成功
                    Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "alert('修改成功!')");
                }
                else
                {
                    //修改失败
                    Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "alert('修改失败!')");
                }
            }
        }
        private void LogoutUser()
        {
            if (Session["User"] == null)
            {
                Response.Redirect("./index.aspx");
            }
        }
        //注销
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            LogoutUser();
        }
    }
}