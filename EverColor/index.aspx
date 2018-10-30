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
    <script>

        $(function () {
            ShowView();
        });
        function ShowView() {
            /*获取时时乐数据*/
            $.ajax({
                //要用post方式  
                type: "Post",
                //方法所在页面和方法名  
                url: "index.aspx/GetDataBySQL",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //返回的数据用data.d获取内容  
                    var da = data.d;
                    CreateTableSQL(da);
                },
                error: function (err) {
                    alert(err);
                }
            });

            $.ajax({
                //要用post方式  
                type: "Post",
                //方法所在页面和方法名  
                url: "index.aspx/ShowTimeBySQL",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //返回的数据用data.d获取内容  
                    var da = data.d;
                    ShowTimes(da);
                },
                error: function (err) {
                    alert(err);
                }
            });
        }

        var time;
        var minutes;
        var seconds;
        var str;
        //获取时间
        function ShowTimes(intime) {
            var Times = intime.split("/");
            var d1 = Times[0];//网络时间
            var d2 = Times[1];//最近一期开奖时间
            var d3 = d1.slice(0, 10) + " 10:00:00";//早上十点
            var d4 = d1.slice(0, 10) + " 22:00:00";//晚上十点
            var d5 = d1.slice(0, 10) + " 02:00:00";//早上两点
            d1 = d1.replace("-", "/");//替换字符，变成标准格式  
            d2 = d2.replace("-", "/");//替换字符，变成标准格式 
            d3 = d3.replace("-", "/");//替换字符，变成标准格式  
            d4 = d4.replace("-", "/");//替换字符，变成标准格式  
            d5 = d5.replace("-", "/");//替换字符，变成标准格式  

            d1 = new Date(Date.parse(d1));
            d2 = new Date(Date.parse(d2));
            d3 = new Date(Date.parse(d3));
            d4 = new Date(Date.parse(d4));
            d5 = new Date(Date.parse(d5));

            console.log("网络时间：" + d1);
            console.log("最新一期的开奖时间：" + d2);
            //凌晨两点到早上十点
            if (d1 >= d5 && d1 < d3) {
                document.getElementById("DjTimeDiv").innerHTML = "剩余时间:暂未开盘";
            }
                //十点到晚上十点 十分钟一期
            else if (d1 >= d3 && d1 < d4) {
                if (d2 - d1 + 600000 > 0) {
                    time = (d2 - d1 + 600000) / 1000;
                    view();
                    function view() {
                        time = time - 1;
                        //document.getElementById("DjTimeDiv").value = time;
                        minutes = parseInt(time / 60);
                        seconds = time % 60;
                        if (time <= 20 && time > 0) {
                            document.getElementById("DjTimeDiv").innerHTML = "暂停投注，剩余时间" + minutes + "分钟" + seconds + "秒";;
                            document.getElementById("Button8").disabled = true;
                            setTimeout(view, 1000);
                        }
                        else if (time <= 0) {
                            document.getElementById("DjTimeDiv").innerHTML = "正在开奖";
                            setTimeout(ShowView, 1000);
                        }
                        else {
                            document.getElementById("DjTimeDiv").innerHTML = "剩余时间:" + minutes + "分钟" + seconds + "秒";
                            document.getElementById("Button8").disabled = false;
                            setTimeout(view, 1000);
                        }
                    }
                }
                else {
                    document.getElementById("DjTimeDiv").innerHTML = "正在开奖";
                    setTimeout(ShowView, 2000);
                }

            }
                //晚上十点到凌晨两点 五分钟一期
            else {
                if (d2 - d1 + 300000 > 0) {
                    time = (d2 - d1 + 300000) / 1000;
                    view();
                    function view() {
                        time = time - 1;
                        //document.getElementById("DjTimeDiv").value = time;   
                        minutes = parseInt(time / 60);
                        seconds = time % 60;
                        if (time <= 20 && time > 0) {
                            document.getElementById("DjTimeDiv").innerHTML = "暂停投注，剩余时间" + minutes + "分钟" + seconds + "秒";;
                            document.getElementById("Button8").disabled = true;
                            setTimeout(view, 1000);
                        }
                        else if (time <= 0) {
                            document.getElementById("DjTimeDiv").innerHTML = "正在开奖";
                            setTimeout(ShowView, 2000);
                        }
                        else {
                            document.getElementById("DjTimeDiv").innerHTML = "剩余时间:" + minutes + "分钟" + seconds + "秒";
                            document.getElementById("Button8").disabled = false;
                            setTimeout(view, 1000);
                        }
                    }
                }
                else {
                    document.getElementById("DjTimeDiv").innerHTML = "正在开奖";
                    setTimeout(ShowView, 2000);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="10000" ontick="Timer1_Tick">
                </asp:Timer>
              </ContentTemplate>
          </asp:UpdatePanel>
        <nav class="navbar navbar-default bg-color">
            <div class="container">
                <div class="navbar-header">
                    <a href="index.aspx" class="navbar-brand" style="color:#eee;">重庆时时彩</a>
                </div>
                <div class="navbar-right">
                    <%--show--%>
                     <%--<div id="divLogin1">
                        <a href="#" class="navbar-text" data-toggle="modal" data-target="#login-lishi" id="A1" runat="server">历史</a>
                    </div>--%>
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
        <div  runat="server" class="modal fade" id="login" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" >
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>历史操作</h4>
                    </div>
                    <div class="modal-body">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate> 
                               <asp:GridView ID="GridView1" CssClass="table table-responsive table-striped" runat="server" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="6"></asp:GridView>
                          </ContentTemplate>
                   </asp:UpdatePanel>
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
