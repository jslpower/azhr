<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiuDianJiaGeEdit.aspx.cs"
    Inherits="EyouSoft.Web.Gys.JiuDianJiaGeEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 70px;">
                        宾客类型：
                    </td>
                    <td align="left">
                        <select name="txtBinKeLeiXing" id="txtBinKeLeiXing" valid="required" errmsg="请选择宾客类型">
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.CustomType)),"","","请选择") %>
                        </select>
                    </td>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 70px;">
                        房型：
                    </td>
                    <td align="left">
                        <select name="txtFangXing" id="txtFangXing" valid="required" errmsg="请选择房型">
                            <asp:Literal runat="server" ID="ltrFangXing"></asp:Literal>
                        </select>
                    </td>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 70px;">
                        是否含早：
                    </td>
                    <td align="left">
                        <input type="radio" name="txtIsHanZao" id="txtIsHanZao1" style="border: none;" value="1" /><label
                            for="txtIsHanZao1">是</label>
                        <input type="radio" name="txtIsHanZao" id="txtIsHanZao0" style="border: none" value="0" /><label
                            for="txtIsHanZao0">否</label>
                    </td>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 70px;">
                        早餐价格：
                    </td>
                    <td align="left">
                        <input type="text" id="txtJGZC" class="formsize50 input-txt" name="txtJGZC" valid="isNumber"
                            errmsg="请输入正确的价格" runat="server" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        门市价：
                    </td>
                    <td align="left" colspan="7">
                        <input type="text" id="txtJGMS" class="formsize50 input-txt" name="txtJGMS" valid="isNumber"
                            errmsg="请输入正确的价格" runat="server" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        日期：
                    </td>
                    <td align="left" colspan="7">
                        <input type="text" id="txtSTime" class="formsize80 input-txt" name="txtSTime" onfocus="WdatePicker()"
                            runat="server" />
                        -
                        <input type="text" id="txtETime" class="formsize80 input-txt" name="txtETime" onfocus="WdatePicker()"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        星期：
                    </td>
                    <td align="left" colspan="7">
                        <input type="checkbox" style="border: none;" id="txtXQA" name="txtXQA" />
                        <label for="txtXQ">
                            全选</label>&nbsp; &nbsp;<input type="checkbox" style="border: none;" id="txtXQ1" name="txtXQ"
                                value="1" />
                        <label for="txtXQ1">
                            一</label>
                        &nbsp;<input type="checkbox" style="border: none;" id="txtXQ2" name="txtXQ" value="2" />
                        <label for="txtXQ2">
                            二</label>
                        &nbsp;<input type="checkbox" style="border: none;" id="txtXQ3" name="txtXQ" value="3" />
                        <label for="txtXQ3">
                            三</label>
                        &nbsp;<input type="checkbox" style="border: none;" id="txtXQ4" name="txtXQ" value="4" />
                        <label for="txtXQ4">
                            四</label>
                        &nbsp;<input type="checkbox" style="border: none;" id="txtXQ5" name="txtXQ" value="5" />
                        <label for="txtXQ5">
                            五</label>
                        &nbsp;<input type="checkbox" style="border: none;" id="txtXQ6" name="txtXQ" value="6" />
                        <label for="txtXQ6">
                            六</label>
                        &nbsp;<input type="checkbox" style="border: none;" id="txtXQ0" name="txtXQ" value="0" />
                        <label for="txtXQ0">
                            日</label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        团队：
                    </td>
                    <td align="left" colspan="7">
                        结算价<input type="text" id="txtJGTJS" class="formsize50 input-txt" name="txtJGTJS"
                            valid="isNumber" errmsg="请输入正确的价格" runat="server" maxlength="10" />
                        服务费<input type="text" id="txtJGTFW" class="formsize50 input-txt" name="txtJGTFW"
                            valid="isNumber" errmsg="请输入正确的价格" runat="server" maxlength="10" />
                        免房<input type="text" id="txtTMianYiM" class="formsize50 input-txt" name="txtTMianYiM"
                            valid="RegInteger" errmsg="请输入正确的数字" runat="server" maxlength="10" />免<input type="text"
                                id="txtTMianYiN" class="formsize50 input-txt" name="txtTMianYiN" valid="IsDecimalOne"
                                errmsg="请输入正确的数字" runat="server" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        散客：
                    </td>
                    <td align="left" colspan="7">
                        结算价<input type="text" id="txtJGSJS" class="formsize50 input-txt" name="txtJGSJS"
                            valid="isNumber" errmsg="请输入正确的价格" runat="server" maxlength="10" />
                        服务费<input type="text" id="txtJGSFW" class="formsize50 input-txt" name="txtJGSFW"
                            valid="isNumber" errmsg="请输入正确的价格" runat="server" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        备注：
                    </td>
                    <td align="left" colspan="7">
                        <input type="text" id="txtBZ" class="input-txt" name="txtBZ" style="width: 400px;"
                            runat="server" maxlength="255" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="i_a_submit"><s class="baochun"></s>
                保 存</a>
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
            setXingQi: function() {
                var _isAll = true;
                $("input[name='txtXQ']").each(function() {
                    if (!this.checked) _isAll = false;
                });
                if (_isAll) $("#txtXQA").attr("checked", "checked");
                else $("#txtXQA").removeAttr("checked");
            },
            init: function() {
                $("input[name='txtIsHanZao']").each(function() { if (this.value == "<%=IsHanZao %>") this.checked = true; });
                $("#txtBinKeLeiXing").val("<%=BinKeLeiXing %>");
                $("#txtFangXing").val("<%=FangXingId %>");

                var _xingQi = "<%=XingQi %>";
                if (_xingQi.length > 0) {
                    var _xingQiArr = _xingQi.split(",");

                    for (var i = 0; i < _xingQiArr.length; i++) {
                        $("#txtXQ" + _xingQiArr[i]).attr("checked", "checked");
                    }
                }

                this.setXingQi();
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
            iPage.init();
            $("#txtXQA").click(function() { if (this.checked) $("input[name='txtXQ']").attr("checked", "checked"); else $("input[name='txtXQ']").removeAttr("checked"); });
            $("input[name='txtXQ']").click(function() { iPage.setXingQi(); });
        });
    </script>

</asp:Content>
