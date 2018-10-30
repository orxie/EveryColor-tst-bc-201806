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
    public partial class registerSecondUser : System.Web.UI.Page
    {
        /// <summary>
        /// 二级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (SecondUser.Text=="")
            {
                Response.Write("<script type='text/javascript'>alert('用户名不能为空');</script>");
                return;
            }
            if (SecondPwd.Text == "")
            {
                Response.Write("<script type='text/javascript'>alert('密码不能为空');</script>");
                return;
            }
            UserInfo userinfo = new UserInfo();
            userinfo.UserName = SecondUser.Text;
            userinfo.UserPass = SecondPwd.Text;
            userinfo.UserInvite = "";
            userinfo.UserAuthority = 1;
            userinfo.UserIntegration = 0;
            //用户名查重
            if (BLL.UserMessage.AddUserName(userinfo) == 0)
            {
                //成功判断
                if (BLL.UserMessage.AdminAddUser(userinfo) == 1)
                {
                    //权限判断  添加邀请码
                    if (userinfo.UserAuthority == 1)
                    {
                        Random rd = new Random();

                        InvitelInfo invitelInfo = new InvitelInfo();
                        invitelInfo.Invite = rd.Next(100000, 1000000);
                        invitelInfo.UserID = BLL.UserMessage.AddselectID(userinfo);
                        //添加
                        if (BLL.UserMessage.AddinvitelID(invitelInfo) == 1)
                        {
                            Response.Write("<script type='text/javascript'>alert('添加成功');</script>");
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>alert('添加失败');</script>");
                        }

                    }
                    else
                    {
                        //添加邀请码失败
                        Response.Write("<script type='text/javascript'>alert('添加邀请码失败');</script>");
                    }
                }
                else
                {
                    //注册失败
                    Response.Write("<script type='text/javascript'>alert('注册失败');</script>");
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('重名');</script>");
            }
        }
    }
}