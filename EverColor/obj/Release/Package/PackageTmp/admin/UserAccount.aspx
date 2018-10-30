<%@ Page Title="用户账号" Language="C#" MasterPageFile="~/admin/userPanelAdmin.Master" AutoEventWireup="true" CodeBehind="UserAccount.aspx.cs" Inherits="Demo.admin.UserAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $('.left-nav').children('li:eq(1)').addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container top-kx">
        <div class="panel panel-default">
            <div class="panel-heading">
                用户
            </div>
            <div class="panel-body">
                <asp:GridView ID="GridView1" runat="server" BackColor="White" CssClass="table table-responsive table-striped" BorderStyle="None" CellPadding="3" GridLines="Vertical">
                </asp:GridView>
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
    <%--注销用户模态框--%>
    <div class="modal fade" id="delUser" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">注销用户</h4>
                </div>
                <div class="modal-body">
                    您确定要注销该用户吗？
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                    <asp:Button runat="server" ID="Button3" CssClass="btn btn-primary" Text="确定" OnClientClick="confirmInfo('注销')" />
                </div>
            </div>
        </div>
    </div>
    <%--积分详情模态框--%>
    <div class="modal fade" id="detailsPoint" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">积分详情</h4>
                </div>
                <div class="modal-body">
                    <table class="table table-responsive table-striped">
                        <thead>
                            <tr>
                                <td>姓名</td>
                                <td>积分</td>
                                <td>备注</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>zmx</td>
                                <td>+1</td>
                                <td>赢了</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
