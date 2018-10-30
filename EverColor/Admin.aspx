<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Demo.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <%--登录--%>    
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" />
    </div>
    <%--创建二级 三级账户--%>
        账户<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        密码<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        邀请码<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        权限<asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="0">管理员</asp:ListItem>
            <asp:ListItem Value="1">二级代理</asp:ListItem>
            <asp:ListItem Value="2" >普通用户</asp:ListItem>
          </asp:DropDownList>
        <br />
        积分<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="注册用户" OnClick="Button2_Click" />
         <br />

    <%--购买 出售 积分--%>
        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox><asp:Button ID="Button3" runat="server" Text="查询用户" OnClick="Button3_Click" />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        添加积分<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" Text="添加积分" OnClick="Button4_Click" />
        出售积分<asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
        <asp:Button ID="Button5" runat="server" Text="出售积分" OnClick="Button5_Click" />
    <%--控制是否开奖--%>
         <br />
        <asp:Button ID="Button6" runat="server" Text="开始" OnClick="Button6_Click" />
    <%--修改密码--%>
        <br />
        以前的密码<asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
        <br />
        新密码<asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
        <br />
        确认新密码<asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button7" runat="server" Text="修改密码" OnClick="Button7_Click" />
    <%--查询操作历史--%>
    </form>
    
</body>
</html>
