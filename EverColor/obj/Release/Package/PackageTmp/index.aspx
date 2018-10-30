<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CaiPiao.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
    

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/common.css" rel="stylesheet" />
    <link href="css/index.css" rel="stylesheet" />

    <script src="js/jquery-2.1.4.min.js"></script>
    <script src="js/Index_js.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/time.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="5000"></asp:Timer>
        <nav class="navbar navbar-default bg-color">
            <div class="container">
                <div class="navbar-header">
                    <a href="index.aspx" class="navbar-brand" style="color:#eee;">重庆时时彩</a>
                </div>
                <div class="navbar-right">
                    <%--show--%>
                     <div id="divLogin1">
                        <%--<a href="#" class="navbar-text" data-toggle="modal" data-target="#login-lishi" id="A1" runat="server">历史</a>--%>
                    </div>
                    <div id="divLogin">
                        <a href="#" class="navbar-text" data-toggle="modal" data-target="#login-frame" id="LoginBTN" runat="server" style="color:#eee;">登录</a>
                    </div>
                    <%--hidden--%>
                </div>
            </div>
        </nav>
        <%--模态框--%>
        <div class="modal fade" id="login-frame" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>登录</h4>
                    </div>
                    <div class="modal-body">
                        <%--账号--%>
                        <label class="input-group input-group-lg">
                            <span class="input-group-addon">
                                <i class="glyphicon glyphicon-user" aria-hidden="true"></i>
                            </span>
                              <asp:TextBox ID="UseTex"  placeholder="请输入用户名"  class="form-control" aria-describedby="username"  runat="server"></asp:TextBox>
                        </label>
                        <%--密码--%>
                        <label class="input-group input-group-lg">
                            <span class="input-group-addon">
                                <i class="glyphicon glyphicon-lock" aria-hidden="true"></i>
                            </span>
                            <asp:TextBox ID="PasTex"  placeholder="请输入密码"  class="form-control" aria-describedby="password"  runat="server" TextMode="Password"></asp:TextBox>
                        </label>
                    </div>
                    <div class="modal-footer">
                        <a href="#">忘记密码？</a>
                         <asp:Button ID="btn1" runat="server" class="btn btn-primary btn-lg"  Text="登录" />
                    </div>
                </div>
            </div>
        </div>
         <%--模态框历史操作--%>
        <div class="modal fade" id="login-lishi" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>历史操作</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridView1" CssClass="table table-responsive table-striped" runat="server" AllowPaging="True"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="container margin-divider">
            <div class="col-md-7 col-lg-7 col-sm-12 col-xs-12">
                <div>
                    <div class="btn-group" role="group">
                        <asp:Button ID="Button1" runat="server" Text="大" class="btn btn-default" OnClick="Button1_Click"/>
                        <asp:Button ID="Button2" runat="server" Text="小" class="btn btn-default" OnClick="Button2_Click"/>
                    </div>
                    <div class="btn-group" role="group">
                        <asp:Button ID="Button3" runat="server" Text="单" class="btn btn-default" OnClick="Button3_Click"/>
                        <asp:Button ID="Button4" runat="server" Text="双" class="btn btn-default" OnClick="Button4_Click"/>
                    </div>
                    <div class="btn-group" role="group">
                        <asp:Button ID="Button5" runat="server" Text="-" class="btn btn-default" OnClick="Button5_Click"/>
                        <asp:Button ID="Button6" runat="server" Text="0" class="btn btn-default"/>
                        <asp:Button ID="Button7" runat="server" Text="+" class="btn btn-default" OnClick="Button7_Click"/>
                         <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="pull-right">
                        您一共选中了
                        <asp:Label ID="Label1" runat="server" Text="暂未选择"></asp:Label>
                        注，共
                        <asp:Label ID="Label2" runat="server" Text="暂未选择"></asp:Label>
                        积分。
                    </div>
                </div>
                 <asp:Button ID="Button8" runat="server" Text="投注" CssClass="btn btn-default pull-right" OnClick="Button8_Click"/>
            </div>
            <div class="col-md-5 col-lg-5 col-xm-12 col-xs-12 text-right" style="font-size: 20px;">
                <div>
                    第<asp:Label runat="server" id="QHnumber"></asp:Label>
                    期
                </div>
                <div id="DjTimeDiv">
                    剩余时间
                </div>
            </div>
        </div>

        <div class="container margin-divider">
            <div class="row">
                <%--今日开奖--%>
                <div class="col-md-7 col-lg-7 col-xs-12 col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color:#4b4239;color:#fff;">
                            <span>今日开奖</span>
                            <a href="#" class="pull-right" style="font-size: 12px;">更多&gt;&gt;</a>
                        </div>
                        <div class="panel-body table-responsive">
                            <div <%--class="col-md-12 col-lg-12 col-xs-12 col-sm-12 "--%> id="zstable">
                                <table class="table table-bordered table-striped table-condensed">
                                    <thead>
                                        <tr>
                                            <td>期号</td>
                                            <td>开奖号码</td>
                                            <td>个位</td>
                                            <td>大小</td>
                                            <td>单双</td>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                           <%-- <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12" id="zstable1">
                                <table class="table table-bordered table-striped table-condensed">
                                    <thead>
                                        <tr>
                                            <td>期号</td>
                                            <td>开奖号码</td>
                                            <td>个位</td>
                                            <td>大小</td>
                                            <td>单双</td>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                            <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12" id="zstable2">
                                <table class="table table-bordered table-striped table-condensed">
                                    <thead>
                                        <tr>
                                            <td>期号</td>
                                            <td>开奖号码</td>
                                            <td>个位</td>
                                            <td>大小</td>
                                            <td>单双</td>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>--%>
                        </div>
                    </div>
                </div>
                <%--今日数据统计--%>
                <div class="col-md-5 col-lg-5 col-xs-12 col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color:#4b4239;color:#fff;">
                            <span>今日数据统计</span>
                            <a href="#" class="pull-right" style="font-size: 12px;">更多&ge;&ge;</a>
                        </div>
                        <div class="panel-body">
                            <table class="table table-bordered table-striped table-condensed" id="CountTable">
                                <thead>
                                    <tr>
                                        <td>0</td>
                                        <td>1</td>
                                        <td>2</td>
                                        <td>3</td>
                                        <td>4</td>
                                        <td>5</td>
                                        <td>6</td>
                                        <td>7</td>
                                        <td>8</td>
                                        <td>9</td>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="canvasdiv">
                </div>
            </div>
        </div>
     
        <footer class="navbar navbar-default bg-color no-margin" style="text-align: center; vertical-align: middle;">
            <span style="margin-left: 15px; margin-right: 15px; line-height: 50px; min-height: 50px;">&copy;All rights reserved.</span>
        </footer>
    </form>
</body>
</html>
