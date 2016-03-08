<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeEdit.aspx.cs" Inherits="Web.UserCenter.Notice.NoticeEdit" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../../UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="9%" height="28" align="right" class="alertboxTableT">
                        标题：
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtTitle" CssClass="formsize450" runat="server" errmsg="请输入标题!"
                            valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        内容：
                    </td>
                    <td height="50" align="left">
                        <asp:TextBox ID="txtContent" Height="48px" CssClass="formsize450" runat="server"
                            TextMode="MultiLine" errmsg="请输入内容!" valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        发布对象：
                    </td>
                    <td height="28" align="left">
                        <asp:CheckBox ID="cbxAll" runat="server" />
                        公司内部&nbsp;
                        <asp:CheckBox ID="cbxSelect" runat="server" />
                        指定部门
                        <uc1:SelectSection ID="SelectSection1" runat="server" SetTitle="指定部门" SModel="1" />
                        <asp:CheckBox ID="cbxPeer" runat="server" />
                        同行社
                        <asp:CheckBox ID="cbxGrounp" runat="server" />
                        组团社
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        附件：
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        发布人：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddUser" CssClass="formsize120" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        发布时间：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddDate" CssClass="formsize120" runat="server" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return NoticeEdit.FormCheck();"><s class="baochun"></s>保 存</asp:LinkButton>
            <a hidefocus="true" href="#"><s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    <%--通知公告id--%>
    <asp:HiddenField ID="HidNoticeID" runat="server" />
    </form>

    <script type="text/javascript">
         $(function() {
             NoticeEdit.BindBtn();
             //设置发布对象
             $("#<%=cbxAll.ClientID %>").bind("click",function(){
                    if($(this).attr("checked"))
                    {
                        $("#<%=cbxSelect.ClientID %>").attr("disabled","disabled");
                        $("#spanSelectSection1 input[type='text']").attr("readonly","readonly");
                        $("#spanSelectSection1").find('a').unbind("click");
                    }
                    else
                    {
                        $("#<%=cbxSelect.ClientID %>").attr("disabled","");
                        $("#spanSelectSection1 input[type='text']").attr("readonly","");
                    }
             })
             //设置发布对象
             $("#<%=cbxSelect.ClientID %>").bind("click",function(){
                    if($(this).attr("checked"))
                        $("#<%=cbxAll.ClientID %>").attr("disabled","disabled");
                    else
                        $("#<%=cbxAll.ClientID %>").attr("disabled","");
             })
         })
         var NoticeEdit = {
             Form: null,
             FormCheck: function() {
                NoticeEdit.Form=$("#<%=btnSave.ClientID %>").closest("form").get(0);
                FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0))
                return ValiDatorForm.validator($("#<%=btnSave.ClientID %>").closest("form").get(0), "alert");                
             },
            Save: function() {
                if (NoticeEdit.FormCheck()) {
                    $("#<%=btnSave.ClientID %>").unbind("click")
                    $("#<%=btnSave.ClientID %>").attr("class", "cun-cyactive")
                    $("#<%=btnSave.ClientID %>").text("提交中...");
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "NoticeEdit.aspx",
                        data: this.Form,
                        success: function(ret) {
                            NoticeEdit.BindBtn();
                        },
                        error: function() {
                            alert("服务器忙！");
                            NoticeEdit.BindBtn();
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#<%=btnSave.ClientID %>").click(function() {
                    NoticeEdit.Save();
                    return false;
                })
                $("#<%=btnSave.ClientID %>").attr("class", "");
                $("#<%=btnSave.ClientID %>").text("提交");
            }
         }
    </script>

</body>
</html>
