<%@ Page Title="二级用户注册" Language="C#" MasterPageFile="~/admin/userPanelAdmin.Master" AutoEventWireup="true" CodeBehind="registerSecondUser.aspx.cs" Inherits="Demo.admin.registerSecondUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            //$('.left-nav').children('li:eq(2)').addClass('active');
            $('.second-nav-show').children('ul').removeClass('display-none');
            $('.second-nav').children('li:eq(1)').addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-kx">
        <div class="col-md-3 col-lg-3 hidden-xs col-sm-3"></div>
        <div class="col-md-6 col-lg-6 col-xs-12 col-sm-6">
            <%--二级用户注册--%>
            <div class="panel panel-default">
                <div class="panel-heading">
                    二级用户注册
                </div>
                <div class="panel-body">
                    <label class="input-group">
                        <span class="input-group-addon bg-color"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox runat="server" ID="SecondUser" placeholder="请输入用户名" CssClass="form-control"></asp:TextBox>
                    </label>
                    <label class="input-group">
                        <span class="input-group-addon bg-color"><i class="glyphicon glyphicon-lock"></i></span>
                        <asp:TextBox runat="server" ID="SecondPwd" placeholder="请输入密码" CssClass="form-control"></asp:TextBox>
                    </label>
                    <label class="input-group">
                        <span class="input-group-addon bg-color">邀请码</span>
                        <asp:TextBox runat="server" ID="SecondInvitation" CssClass="form-control" disabled="disabled"></asp:TextBox>
                    </label>
                    <div class="pull-right">
                        <asp:Button runat="server" ID="Button1" CssClass="btn bg-color" Text="注册" OnClick="Button1_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
