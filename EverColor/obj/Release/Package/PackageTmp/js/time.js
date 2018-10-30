
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
                    setTimeout(view, 2000);
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