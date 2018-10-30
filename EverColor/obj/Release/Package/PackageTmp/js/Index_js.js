function LoadXml(da) {
    try //Internet Explorer
    {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async = "false";
        xmlDoc.loadXML(da);
    }
    catch (e) {
        try //Firefox, Mozilla, Opera, etc.
        {
            parser = new DOMParser();
            xmlDoc = parser.parseFromString(da, "text/xml");
        }
        catch (e) {
            alert(e.message);
            return;
        }
    }
}


function CreateTable(da) {
    $("#zstable tbody").html("");
    //$("#zstable1 tbody").html("");
    //$("#zstable2 tbody").html("");
    $("#CountTable tbody").html("");
    $("#QHnumber").html("");

    //加载xml文件
    LoadXml(da);
    //获取当天刮刮乐的条数
    var docelemt = xmlDoc.getElementsByTagName("row");
    var expect;//编号
    var opencode;//号码
    //var  opentime = docelemt[i].getAttribute("opentime");//时间
    var tbody = "";
    var tbody1 = "";
    var tbody2 = "";
    var tbodyCount = "";
    for (var i = 0; i < docelemt.length; i++) {
        tbodyCount += "<tr>";
        expect = docelemt[i].getAttribute("expect");
        opencode = docelemt[i].getAttribute("opencode");
        //if (i < docelemt.length / 3) {
        tbody += "<tr>";//第一列
        //期号
        tbody += "<td>" + expect + "</td>";
        //开奖号
        tbody += "<td>" + opencode + "</td>";
        //个位
        tbody += "<td>" + opencode.slice(8, 9) + "</td>";
        //大小
        if (parseInt(opencode.slice(8, 9)) >= 5)
            tbody += "<td>大</td>";
        else
            tbody += "<td>小</td>";
        //单双
        if ((parseInt(opencode.slice(8, 9)) + 4) % 2 == 1)
            tbody += "<td>奇</td>";
        else
            tbody += "<td>偶</td>";
        tbody += "</tr>";
    }
    //else if (i >= docelemt.length / 3 && i < docelemt.length / 3 * 2) {
    //    tbody1 += "<tr>";//第二列
    //    //期号
    //    tbody1 += "<td>" + expect + "</td>";
    //    //开奖号
    //    tbody1 += "<td>" + opencode + "</td>";
    //    //个位
    //    tbody1 += "<td>" + opencode.slice(8, 9) + "</td>";
    //    //大小
    //    if (parseInt(opencode.slice(8, 9)) >= 5)
    //        tbody1 += "<td>大</td>";
    //    else
    //        tbody1 += "<td>小</td>";
    //    //单双
    //    if ((parseInt(opencode.slice(8, 9)) + 4) % 2 == 1)
    //        tbody1 += "<td>奇</td>";
    //    else
    //        tbody1 += "<td>偶</td>";
    //    tbody1 += "</tr>";

    //}
    //else {
    //    tbody2 += "<tr>";//第三列
    //    //期号
    //    tbody2 += "<td>" + expect + "</td>";
    //    //开奖号
    //    tbody2 += "<td>" + opencode + "</td>";
    //    //个位
    //    tbody2 += "<td>" + opencode.slice(8, 9) + "</td>";
    //    //大小
    //    if (parseInt(opencode.slice(8, 9)) >= 5)
    //        tbody2 += "<td>大</td>";
    //    else
    //        tbody2 += "<td>小</td>";
    //    //单双
    //    if ((parseInt(opencode.slice(8, 9)) + 4) % 2 == 1)
    //        tbody2 += "<td>奇</td>";
    //    else
    //        tbody2 += "<td>偶</td>";
    //    tbody2 += "</tr>";
    //}

    //绘制统计图
    for (var j = 0; j < 10; j++) {
        if (j != opencode.slice(8, 9))
            tbodyCount += "<td id='T" + i + "_" + j + "'></td>";
        else
            tbodyCount += "<td id='T" + i + "_" + j + "'>" + j + "</td>";
    }
    tbodyCount += "</tr>";

$("#zstable tbody").html(tbody);
//$("#zstable1 tbody").html(tbody1);
//$("#zstable2 tbody").html(tbody2);
$("#CountTable tbody").html(tbodyCount);

//添加折线图点
var ids = "";
for (var i = 0; i < docelemt.length; i++) {
    opencode = docelemt[i].getAttribute("opencode");
    ids += "T" + i + "_" + opencode.slice(8, 9) + ",";
}
ids = ids.substring(0, ids.length - 1);
CreateLine(ids, 20, "#ff6600", "canvasdiv", "#d5d5d5");

//添加期号
var num = parseInt(docelemt[0].getAttribute("expect")) + 1;
$("#QHnumber").html(num);
}

function CreateTableSQL(da) {

    $("#zstable tbody").html("");
    $("#CountTable tbody").html("");
    $("#QHnumber").html("");

    //加载xml文件
    LoadXml(da);
    //获取当天刮刮乐的条数
    var docelemt = xmlDoc.getElementsByTagName("row");
    var expect;//编号
    var opencode;//号码
    //var  opentime = docelemt[i].getAttribute("opentime");//时间
    var tbody = "";
    var tbody1 = "";
    var tbody2 = "";
    var tbodyCount = "";
    //添加期号
    var num = parseInt(docelemt[0].getAttribute("expect"));
    $("#QHnumber").html(num);

    for (var i = 1; i < docelemt.length; i++) {
        tbodyCount += "<tr>";
        expect = docelemt[i].getAttribute("expect");
        opencode = docelemt[i].getAttribute("opencode");
        tbody += "<tr>";//第一列
        //期号
        tbody += "<td>" + expect + "</td>";
        //开奖号
        tbody += "<td>" + opencode + "</td>";
        //个位
        tbody += "<td>" + opencode.slice(4, 5) + "</td>";
        //大小
        if (parseInt(opencode.slice(4, 5)) >= 5)
            tbody += "<td>大</td>";
        else
            tbody += "<td>小</td>";
        //单双
        if ((parseInt(opencode.slice(4, 5)) + 4) % 2 == 1)
            tbody += "<td>奇</td>";
        else
            tbody += "<td>偶</td>";
        tbody += "</tr>";

        //绘制统计图
        for (var j = 0; j < 10; j++) {
            if (j != opencode.slice(4, 5))
                tbodyCount += "<td id='T" + i + "_" + j + "'></td>";
            else
                tbodyCount += "<td id='T" + i + "_" + j + "'>" + j + "</td>";
        }
        tbodyCount += "</tr>";
    }
    $("#zstable tbody").html(tbody);
    //$("#zstable1 tbody").html(tbody1);
    //$("#zstable2 tbody").html(tbody2);
    $("#CountTable tbody").html(tbodyCount);

    //添加折线图点
    var ids = "";
    for (var i = 1; i < docelemt.length; i++) {
        opencode = docelemt[i].getAttribute("opencode");
        ids += "T" + i + "_" + opencode.slice(4, 5) + ",";
    }
    ids = ids.substring(0, ids.length - 1);
    CreateLine(ids, 20, "#ff6600", "canvasdiv", "#d5d5d5");


}

/*描点连线*/
function CreateLine(ids, w, c, div, bg) {
    $("#" + div).html("");
    var list = ids.split(",");

    for (var j = list.length - 1; j > 0; j--) {
        var tid = $("#" + list[j]);
        var fid = $("#" + list[j - 1]);
        var f_width = fid.outerWidth();
        var f_height = fid.outerHeight();
        var f_offset = fid.offset();
        var f_top = f_offset.top;
        var f_left = f_offset.left;
        var t_offset = tid.offset();
        var t_top = t_offset.top;
        var t_left = t_offset.left;
        var cvs_left = Math.min(f_left, t_left);
        var cvs_top = Math.min(f_top, t_top);
        tid.css("background", bg).css("color", "red");
        fid.css("background", bg).css("color", "red");
        var cvs = document.createElement("canvas");
        cvs.width = Math.abs(f_left - t_left) < w ? w : Math.abs(f_left - t_left);
        cvs.height = Math.abs(f_top - t_top);
        cvs.style.top = cvs_top + parseInt(f_height / 2) + "px";
        cvs.style.left = cvs_left + parseInt(f_width / 2) + "px";
        cvs.style.position = "absolute";
        var cxt = cvs.getContext("2d");
        cxt.save();
        cxt.strokeStyle = c;
        cxt.lineWidth = 1;
        cxt.lineJoin = "round";
        cxt.beginPath();
        cxt.moveTo(f_left - cvs_left, f_top - cvs_top);
        cxt.lineTo(t_left - cvs_left, t_top - cvs_top);
        cxt.closePath();
        cxt.stroke();
        cxt.restore();
        $("#" + div).append(cvs);
    }
}