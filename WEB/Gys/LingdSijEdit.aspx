<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LingdSijEdit.aspx.cs" Inherits="EyouSoft.Web.Gys.LingdSijEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="13%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    姓名：
                </td>
                <td width="25%" align="left">
                    <input name="txtName" type="text" class="formsize80 input-txt" id="txtName" runat="server"
                        valid="required" errmsg="请输入姓名！" maxlength="50" />
                </td>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    性别：
                </td>
                <td align="left">
                    <select id="txtGender" name="txtGender">
                        <option value="0">男</option>
                        <option value="1">女</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    联系电话：
                </td>
                <td width="25%" align="left">
                    <input name="txtTelephone" type="text" id="txtTelephone" class="formsize180 input-txt"
                        runat="server" maxlength="50" />
                </td>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    手机号码：
                </td>
                <td width="45%" align="left">
                    <input name="txtMobile" type="text" id="txtMobile" class="formsize180 input-txt"
                        runat="server" maxlength="50" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    地址：
                </td>
                <td colspan="3" align="left">
                    <input name="txtAddress" type="text" id="txtAddress" style="width: 200px;" class="input-txt" runat="server"
                        maxlength="50" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td colspan="3" align="left">
                    <textarea name="txtBeiZhu" cols="6" class="formsize600" style="height: 35px; padding: 3px;"
                        id="txtBeiZhu" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    评价：
                </td>
                <td colspan="3" align="left">
                    <select id="txtPingJiaLeiXing" name="txtPingJiaLeiXing">
                        <option value="1">好</option>
                        <option value="2">中</option>
                        <option value="3">差</option>
                        <option value="4">黑名单</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    评价内容：
                </td>
                <td colspan="3" align="left">
                    <textarea name="txtPingJiaNeiRong" cols="6" class="formsize600" style="height: 35px;
                        padding: 3px;" id="txtPingJiaNeiRong" runat="server"></textarea>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phSubmit"><a href="javascript:void(0)" hidefocus="true"
                id="i_a_submit"><s class="baochun"></s>保 存</a> </asp:PlaceHolder>
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
            },
            init: function() {
                $("#txtGender").val("<%=Gender %>");
                $("#txtPingJiaLeiXing").val("<%=PingJiaLeiXing %>");
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
            iPage.init();
        });
    </script>

</asp:Content>
