<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CanGuanCaiDanEdit.aspx.cs"
    Inherits="EyouSoft.Web.Gys.CanGuanCaiDanEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
<%--            <asp:PlaceHolder runat="server" ID="phLng" Visible="false">
                <div style="width: 99%; margin: 0px auto;">
                    <a href="javascript:void(0)" id="i_a_lng_1">中文</a> <a href="javascript:void(0)" id="i_a_lng_2">
                        英文</a> <a href="javascript:void(0)" id="i_a_lng_3">泰文</a></div>
            </asp:PlaceHolder>
--%>            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <asp:PlaceHolder runat="server" ID="phLng" Visible="false">
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        语言：
                    </td>
                    <td align="left">
                        <select id="ddllng" class="inputselect">
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SysStructure.LngType)), Utils.GetQueryStringValue("lng"), false)%>
                        </select>
                    </td>
                </tr>
                </asp:PlaceHolder>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        菜单名称：
                    </td>
                    <td align="left">
                        <input name="txtName" type="text" class="formsize140 input-txt" id="txtName" runat="server"
                            maxlength="50" valid="required" errmsg="请输入菜单名称" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        菜单内容：
                    </td>
                    <td align="left">
                        <textarea name="txtNeiRong" id="txtNeiRong" style="height: 80px; width: 300px" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        菜单价格：
                    </td>
                    <td height="14" align="left">
                        价格 -&gt;桌 门市价
                        <input name="txtZMS" type="text" class=" formsize40 input-txt i_lng" id="txtZMS"
                            runat="server" maxlength="10" valid="isNumber" errmsg="请输入正确的价格" />
                        同行价
                        <input name="txtZTH" type="text" class=" formsize40 input-txt i_lng" id="txtZTH"
                            runat="server" maxlength="10" valid="isNumber" errmsg="请输入正确的价格" />
                        结算价
                        <input name="txtZJS" type="text" class=" formsize40 input-txt i_lng" id="txtZJS"
                            runat="server" maxlength="10" valid="isNumber" errmsg="请输入正确的价格" />
                    </td>
                </tr>
                <tr>
                    <td height="14" align="left">
                        价格 -&gt;人 门市价
                        <input name="txtRMS" type="text" class=" formsize40 input-txt i_lng" id="txtRMS"
                            runat="server" maxlength="10" valid="isNumber" errmsg="请输入正确的价格" />
                        同行价
                        <input name="txtRTH" type="text" class=" formsize40 input-txt i_lng" id="txtRTH"
                            runat="server" maxlength="10" valid="isNumber" errmsg="请输入正确的价格" />
                        结算价
                        <input name="txtRJS" type="text" class=" formsize40 input-txt i_lng" id="txtRJS"
                            runat="server" maxlength="10" valid="isNumber" errmsg="请输入正确的价格" />
                        免餐<input type="text" id="txtTMianYiM" class="formsize30 input-txt i_lng" name="txtTMianYiM"
                            valid="RegInteger" errmsg="请输入正确的数字" runat="server" maxlength="10" />免<input type="text"
                                id="txtTMianYiN" class="formsize30 input-txt i_lng" name="txtTMianYiN" valid="RegInteger"
                                errmsg="请输入正确的数字" runat="server" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        是否可用：
                    </td>
                    <td align="left">
                         <input type="radio" name="txtIsShowOrHidden" id="txtIsShow" style="border: none;"
                        value="0" /><label for="txtIsShow">是</label>
                    <input type="radio" name="txtIsShowOrHidden" id="txtIsQianDan0" style="border: none" value="1" /><label
                        for="txtIsHidden">否</label>
                        
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
            init: function() {
                var _params = utilsUri.getUrlParams(["lng"]);
                $("input[name='txtIsShowOrHidden']").each(function() { if (this.value == "<%=txtIsShowOrHidden %>") this.checked = true; });
            	
            	//语言选择
            	$("#ddllng").change(function () {
            		window.location.href = "CanGuanCaiDanEdit.aspx?" + $.param(_params) + "&lng="+$(this).val();
            	})
//                $("#i_a_lng_1").attr("href", "CanGuanCaiDanEdit.aspx?" + $.param(_params) + "&lng=1");
//                $("#i_a_lng_2").attr("href", "CanGuanCaiDanEdit.aspx?" + $.param(_params) + "&lng=2");
//                $("#i_a_lng_3").attr("href", "CanGuanCaiDanEdit.aspx?" + $.param(_params) + "&lng=3");

                $("#i_a_lng_<%=(int)LngType %>").css({ "color": "#ff0000" });

                if ("<%=(int)LngType %>" != "1") {
                    $(".i_lng").attr("readonly", "readonly").css({ "background-color": "#dadada" });
                }
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
            iPage.init();
        });
    </script>

</asp:Content>
