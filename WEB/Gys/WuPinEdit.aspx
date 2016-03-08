<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WuPinEdit.aspx.cs" Inherits="EyouSoft.Web.Gys.WuPinEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <div style="width: 100%; margin: 0 auto;">
            <table width="99%" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        物品名称：
                    </td>
                    <td width="38%" height="28" align="left">
                        <input name="txtName" type="text" class="formsize120 input-txt" id="txtName" runat="server"
                            valid="required" errmsg="物品名称不能为空！" maxlength="50" />
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        物品数量：
                    </td>
                    <td width="32%" align="left">
                        <input name="txtShuLiang" type="text" class="formsize80 input-txt" id="txtShuLiang"
                            runat="server" valid="RegInteger" errmsg="请输入正确的物品数量！" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        物品单价：
                    </td>
                    <td height="28" align="left">
                        <input name="txtDanJia" type="text" class="formsize80 input-txt" id="txtDanJia" runat="server"
                            valid="isNumber" errmsg="请输入正确的物品单价！" maxlength="10" />
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        入库时间：
                    </td>
                    <td align="left">
                        <input name="txtTime" type="text" class="formsize120 input-txt" id="txtTime" runat="server"
                            onfocus="WdatePicker()" valid="required" errmsg="请输入入库时间！" />
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        用 途：
                    </td>
                    <td height="28" colspan="3" align="left">
                        <input name="txtYongTu" type="text" class="formsize450 input-txt" id="txtYongTu"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        登记人：
                    </td>
                    <td height="28" colspan="3" align="left">
                        <asp:Literal runat="server" ID="ltrOperatorName"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        备 注：
                    </td>
                    <td height="28" colspan="3" align="left">
                        <input name="txtBeiZhu" type="text" class="formsize450 input-txt" id="txtBeiZhu"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phSubmit">
            <a href="javascript:void(0)" hidefocus="true" id="i_a_submit"><s class="baochun"></s>保存</a>
            </asp:PlaceHolder>
        </div>
    </div>
    </form>
    
    <script type="text/javascript">
        var iPage = {
            close: function() {
                top.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            },
            submit: function(obj) {
                var _v = ValiDatorForm.validator($("#form1").get(0), "parent");
                if (!_v) return false;

                $(obj).unbind("click").css({ "color": "#999999" });

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit", data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.close();
                        } else {
                            alert(response.msg);
                            $(obj).bind("click", function() { iPage.submit(obj); }).css({ "color": "" });
                        }
                    },
                    error: function() {
                        $(obj).bind("click", function() { iPage.submit(obj); }).css({ "color": "" });
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
        });
    
    </script>
</asp:Content>
