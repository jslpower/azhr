<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WuPinLingYong.aspx.cs"
    Inherits="EyouSoft.Web.Gys.WuPinLingYong" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox02">
        <table width="99%" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="80" height="28" align="right" class="alertboxTableT">
                    物品名称：
                </td>
                <td height="28" align="left">
                    <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    领用时间：
                </td>
                <td height="28" align="left">
                    <input name="txtTime" type="text" class="formsize120 input-txt" id="txtTime" runat="server"
                        onfocus="WdatePicker()" valid="required" errmsg="请输入时间！" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    领用人：
                </td>
                <td height="28" align="left">
                    <uc1:SellsSelect ID="txtRen" runat="server" SelectFrist="false" ReadOnly="true" SetTitle="领用人" />
                </td>
            </tr>
            <tr>
                <td align="right" class="alertboxTableT">
                    领用数量：
                </td>
                <td height="28" align="left">
                    <input name="txtShuLiang" type="text" class="formsize80 input-txt" id="txtShuLiang"
                        runat="server" valid="RegInteger" errmsg="请输入正确的物品数量！" maxlength="10" />
                </td>
            </tr>
            <tr>
                <td align="right" class="alertboxTableT">
                    用途说明：
                </td>
                <td height="46" align="left">
                    <textarea name="txtYongTu" id="txtYongTu" style="width: 300px; height: 40px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" class="alertboxTableT">
                    经 办 人：
                </td>
                <td height="28" align="left">
                    <asp:Literal runat="server" ID="ltrOperatorName"></asp:Literal>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phSubmit"><a href="javascript:void(0)" hidefocus="true"
                id="i_a_submit"><s class="baochun"></s>领用</a> </asp:PlaceHolder>
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
