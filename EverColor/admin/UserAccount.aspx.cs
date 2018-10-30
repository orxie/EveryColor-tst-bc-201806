using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo.admin
{
    public partial class UserAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //管理员
            GridView1.DataSource = BLL.UserMessage.AdminSelectMYUser();
            GridView1.DataBind();
        }
    }
}