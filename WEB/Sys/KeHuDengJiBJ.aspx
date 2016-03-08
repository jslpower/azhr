<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="KeHuDengJiBJ.aspx.cs"
    Inherits="Web.Sys.KeHuDengJiBJ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ޱ����ĵ�</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css">

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="19%" height="28" align="right" class="alertboxTableT">
                        �ȼ����ƣ�
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtLevelName" runat="server" CssClass=" inputtext formsize180" Valid="request"
                            errmsg="�ȼ����Ʋ���Ϊ�գ�"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        �ȼ���ע��
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_backMark" runat="server" CssClass=" inputtext formsize350"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        ����%��
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_price" runat="server" CssClass=" inputtext"></asp:TextBox>%
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <a hidefocus="true" href="javascript:" id="btnSave" onclick="return GuestLevelEdit.Save();">
                <s class="baochun"></s>�� ��</a> <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                    <s class="chongzhi"></s>�� ��</a>
        </div>
    </div>

    <script type="text/javascript">
           var GuestLevelEdit = {
            Params:{sl:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",id:"<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>"
            },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>�ύ��...");
                $("#btnSave").unbind("click");
                $("#btnSave").css("background-position", "0-57px");
            },
            //��ť���¼�
            BindBtn: function() {
                $("#btnSave").bind("click");
                $("#btnSave").css("background-position", "0-28px");
                $("#btnSave").html("<s class='baochun'></s>�� ��");
                $("#btnSave").click(function() {
                    GuestLevelEdit.Save();
                    return false;
                });
            },
            CheckDate:function(){
                var msg = "";
                var levelname = document.getElementById("<%=txtLevelName.ClientID%>").value;
                if (levelname == "") {
                    msg += "�ȼ����Ʋ���Ϊ��!<br/>";
                }
                if(msg!=""){
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                return true;
            },
            Save: function() {
                GuestLevelEdit.CheckDate();
                GuestLevelEdit.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/KeHuDengJiBJ.aspx?dotype=save&"+$.param(GuestLevelEdit.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax�ط���ʾ
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg,function(){parent.window.location.href="/Sys/KeHuDengJi.aspx?&"+$.param(GuestLevelEdit.Params);});
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                        GuestLevelEdit.BindBtn();
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("����ʧ�ܣ����Ժ�����!");
                        GuestLevelEdit.BindBtn();
                    }
                });
            }
        };
    </script>

    </form>
</body>
</html>
