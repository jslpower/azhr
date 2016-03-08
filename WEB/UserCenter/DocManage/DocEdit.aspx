<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocEdit.aspx.cs" Inherits="Web.UserCenter.DocManage.DocEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

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
                        &nbsp;文档名称：
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtFileName" runat="server" CssClass="formsize450" errmsg="请输入文档名称!" valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        文档描述：
                    </td>
                    <td height="125" align="left">
                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Height="120px" runat="server" CssClass="formsize450" errmsg="请输入文档描述!" valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        文档上传：
                    </td>
                    <td align="left">
                        <input type="file" style="height: 20px;" class="formsize180" name="fileField">
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        上传人：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddUser" runat="server" CssClass="formsize120" errmsg="请输入上传人!" valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        上传时间：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddDate" runat="server" CssClass="formsize120" errmsg="请输入上传时间!" valid="required"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <asp:LinkButton ID="btnSave" runat="server"><s class="baochun"></s>保 存</asp:LinkButton>
            <a hidefocus="true" href="javascript:void(0);"><s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $(function() {
            FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
        })
        var WorkExEdit = {
            FormCheck: function() {
                var form = FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
                return ValiDatorForm.validator(form, "alert");
            }
        }    
    </script>
</body>
</html>
