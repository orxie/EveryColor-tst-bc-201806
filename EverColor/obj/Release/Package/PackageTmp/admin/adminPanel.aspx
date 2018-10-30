<%@ Page Title="首页" Language="C#" MasterPageFile="~/admin/userPanelAdmin.Master" AutoEventWireup="true" CodeBehind="adminPanel.aspx.cs" Inherits="Demo.admin.adminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $('.left-nav').children('li:eq(0)').addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-kx">
        <div class="col-lg-8 col-md-8 col-xs-12 col-sm-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    用户
                    <a href="#" class="pull-right">更多&gt;&gt;</a>
                </div>
                <div class="panel-body">
                    <label>请查询用户名</label><asp:TextBox ID="TextBox7" CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button3" CssClass="btn pull-right bg-color" runat="server" Text="查询用户" OnClick="Button3_Click" />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" CssClass="table table-responsive table-striped" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                    </asp:GridView>
                    <br />
                     <label>添加积分</label><asp:TextBox ID="TextBox8"  CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button4" runat="server" CssClass="btn pull-right bg-color" Text="添加积分" OnClick="Button4_Click" />
                    <br />

                    <label>减少积分</label><asp:TextBox ID="TextBox9"  CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button5" runat="server" CssClass="btn pull-right bg-color" Text="出售积分" OnClick="Button5_Click" />
                    <br />

                    <label>修改密码</label><asp:TextBox ID="TextBox1"  CssClass="form-control" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button7" runat="server" CssClass="btn pull-right bg-color" Text="修改密码" OnClick="Button7_Click" />
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-xs-12 col-sm-4">
            <%--开关盘--%>
            <div class="panel panel-default">
                <div class="panel-heading">开奖状态</div>
                <div class="panel-body">
                    开奖状态：
                    <asp:Label runat="server" ID="status"></asp:Label>
                    <asp:Button ID="Button6" CssClass="btn pull-right bg-color" runat="server" OnClick="Button6_Click" />
                </div>
            </div>
            <%--倍率--%>
            <div class="panel panel-default">
                <div class="panel-heading">倍率</div>
                <div class="panel-body">
                    <label class="input-group">
                        <span class="input-group-addon bg-color">大</span>
                        <asp:TextBox placeholder="请输入倍率" runat="server" ID="big" CssClass="form-control"></asp:TextBox>
                    </label>
                    <label class="input-group">
                        <span class="input-group-addon bg-color">小</span>
                        <asp:TextBox placeholder="请输入倍率" runat="server" ID="small" CssClass="form-control"></asp:TextBox>
                    </label>
                    <label class="input-group">
                        <span class="input-group-addon bg-color">双</span>
                        <asp:TextBox placeholder="请输入倍率" runat="server" ID="double" CssClass="form-control"></asp:TextBox>
                    </label>
                    <label class="input-group">
                        <span class="input-group-addon bg-color">单</span>
                        <asp:TextBox placeholder="请输入倍率" runat="server" ID="single" CssClass="form-control"></asp:TextBox>
                    </label>
                    <div class="pull-right">
                        <asp:Button runat="server" ID="confirmPower" CssClass="btn bg-color" Text="确定" />
                    </div>
                </div>
            </div>
        </div>
        <%--添加积分模态框--%>
        <div class="modal fade" id="bonusPoint" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">添加积分</h4>
                    </div>
                    <div class="modal-body">
                        <label class="input-group">
                            <span class="input-group-addon">添加积分</span>
                            <asp:TextBox placeholder="请输入添加的积分" runat="server" ID="addPoint" CssClass="form-control"></asp:TextBox>
                        </label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                        <asp:Button runat="server" ID="confirmBonusPoint" CssClass="btn bg-color" Text="确定" OnClientClick="confirmInfo('添加')" />
                    </div>
                </div>
            </div>
        </div>
        <%--删除积分模态框--%>
        <div class="modal fade" id="delPoint" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">删除积分</h4>
                    </div>
                    <div class="modal-body">
                        <label class="input-group">
                            <span class="input-group-addon">删除积分</span>
                            <asp:TextBox placeholder="请输入删除的积分" runat="server" ID="delPointNum" CssClass="form-control"></asp:TextBox>
                        </label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                        <asp:Button runat="server" ID="Button2" CssClass="btn btn-primary" Text="确定" OnClientClick="confirmInfo('删除')" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function confirmInfo(i) {
            var ret = confirm("您确定" + i + "吗?");
            if (ret == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
