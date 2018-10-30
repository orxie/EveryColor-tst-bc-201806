using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace Demo.admin
{
    public partial class registerUser : System.Web.UI.Page
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void reg_Click(object sender, EventArgs e)
        {
            if (username.Text == "")
            {
                Response.Write("<script type='text/javascript'>alert('用户名不能为空');</script>");
                return;
            }
            if (userpwd.Text == "")
            {
                Response.Write("<script type='text/javascript'>alert('密码不能为空');</script>");
                return;
            }
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = username.Text;
            userinfo.UserPass = userpwd.Text;
            userinfo.UserInvite = invitationCode.Text;
            userinfo.UserAuthority = 2;
            userinfo.UserIntegration = 0;
            //用户名查重
            if (BLL.UserMessage.AddUserName(userinfo) == 0)
            {
                //成功判断
                if (BLL.UserMessage.AdminAddUser(userinfo) == 1)
                {
                    Response.Write("<script type='text/javascript'>alert('注册成功');</script>");
                }
                else
                {
                    //注册失败
                    Response.Write("<script type='text/javascript'>alert('注册失败');</script>");
                }
            }
            else
            {
                //用户名重复
                Response.Write("<script type='text/javascript'>alert('用户名重复');</script>");
            }
        }
    }
}