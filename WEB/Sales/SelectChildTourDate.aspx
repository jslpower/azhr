<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectChildTourDate.aspx.cs" Inherits="EyouSoft.Web.Sales.SelectChildTourDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择出团日期</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <style>
        body
        {
            margin: auto 0;
            padding: 0;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
        table
        {
            border-collapse: collapse;
            width: 100%;
            background-color: #FFFFFF;
            border-collapse: collapse;
        }
        img
        {
            border: none;
        }
        .hui
        {
            color: #aaaaaa;
        }
        .calendarTable td
        {
            border-collapse: collapse;
            border: solid 1px #999999;
        }
    </style>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="div1" class="alertbox-outbox03" style="height: 285px;">
    </div>
    <div id="divTourCodeHTML" style="width: 780px; margin-top: 10px;">
    </div>
    <div class="alertbox-btn" style="position: fixed;">
        <span style="float: left; color: Red; margin-left:30px;">*日期最多只能选择60个</span> <a hidefocus="true" href="javascript:void(0);"
            id="btnSelect"><s class="xuanzhe"></s>选 择</a> <a hidefocus="true" href="javascript:void(0);"
                onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a></div>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/groupdate.js"></script>

    <script type="text/javascript">
        var SelectCalendar = {
            hide: parent.$('#<%=Request.QueryString["hide"] %>'),
            show: parent.$('#<%=Request.QueryString["show"] %>')
        }

        $(function() {
            $("#btnSelect").click(function() {
                QGD.GetCheckedDate();
                var date = QGD.config.arrLeaveDate.join(',');
                var length = QGD.config.arrLeaveDate.length;
                if (length == 0) {
                    parent.tableToolbar._showMsg("请选择出团日期!");
                    return false;
                }
                if (length > 60) {
                    parent.tableToolbar._showMsg("请选择少于60个日期!");
                    return false;
                }

                SelectCalendar.hide.val(date);
                SelectCalendar.show.html("共选择了:" + QGD.config.arrLeaveDate.length + "个日期");
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();

                return false;
            })
        })
    </script>

    <script type="text/javascript">

        $(document).ready(function() {
         QGD.initCalendar({
         containerId:"div1",
         currentDate:<%=CurrentDate %>,
         firstMonthDate:<%=CurrentDate %>,
         nextMonthDate:<%=NextDate %>,
         listcontainer:"divTourCodeHTML",
         arrOldLeaveDate:'<%=Request.QueryString["old"] %>'
        });
        });
    </script>
</body>
</html>
