<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouXuZhiBJ.aspx.cs" ValidateRequest="false"
    Inherits="EyouSoft.Web.Sys.DaoYouXuZhiBJ" %>

<%@ Register Src="../UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <title></title>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="20%" align="right" class="alertboxTableT">
                        部门：
                    </td>
                    <td align="left">
                        <uc1:SelectSection ID="SelectSection1" runat="server" IsNotValid="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        导游需知：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_area" CssClass="inputtext formsize450" runat="server" valid="request"
                            errmsg="类型名称不能为空！"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <a hidefocus="true" href="javascript:" id="btnSave" onclick="return pageOption.Save();">
                <s class="baochun"></s>保 存</a> <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
         var pageOption = {
             Params:{sl:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",memuid:"<%=EyouSoft.Common.Utils.GetQueryStringValue("memuid") %>",id:"<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>"
            },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...");
                $("#btnSave").unbind("click");
                $("#btnSave").css("background-position", "0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click");
                $("#btnSave").css("background-position", "0-28px");
                $("#btnSave").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    pageOption.Save();
                    return false;
                });
            },
            CheckDate:function(){
                var msg = "";
                var typename = document.getElementById("<%=txt_area.ClientID%>").value;
                if (typename == "") {
                    msg += "导游须知不能为空!<br/>";
                }
                if(msg!=""){
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                return true;
            },
            Save: function() {
            	//同步编辑器数据到文本框
                KEditer.sync();
                pageOption.CheckDate();
                pageOption.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/DaoYouXuZhiBJ.aspx?dotype=save&"+$.param(pageOption.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg,function(){parent.window.location.href="/Sys/DaoYouXuZhi.aspx?&"+$.param(pageOption.Params);});
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                        pageOption.BindBtn();
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后再试!");
                        pageOption.BindBtn();
                    }
                });
            }
         };
         $(function() {
         	//创建编辑器
         	KEditer.init('<%=txt_area.ClientID %>', { resizeMode: 0, items: keSimple, height: "200px", width: "500px" });
         });
    </script>

</body>
</html>
