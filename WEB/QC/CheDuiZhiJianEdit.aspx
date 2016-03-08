<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheDuiZhiJianEdit.aspx.cs"
    Inherits="EyouSoft.Web.QC.CheDuiZhiJianEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="/UserControl/TuanHaoXuanYong.ascx" TagName="TuanHaoXuanYong" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号：
                </td>
                <td width="39%" align="left">
                    <uc1:TuanHaoXuanYong ID="txtTuanHaoXuanYong" runat="server" SelectFrist="false" />
                </td>
                <td width="11%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    车队：
                </td>
                <td width="36%" align="left">
                    <input name="txtCheDuiName" type="text" class="formsize140 input-txt" id="txtCheDuiName"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td width="14%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    车号：
                </td>
                <td align="left">
                    <input name="txtCheHao" type="text" class="formsize140 input-txt" id="txtCheHao"
                        runat="server" />
                </td>
                <td align="right" class="alertboxTableT">
                    日期：
                </td>
                <td align="left">
                    <input name="txtRiQi" type="text" class="formsize80 input-txt" id="txtRiQi" runat="server"
                        onfocus="WdatePicker()" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    领队：
                </td>
                <td align="left">
                    <input name="txtLingDuiName" type="text" class="formsize80 input-txt" id="txtLingDuiName"
                        runat="server" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    领队电话：
                </td>
                <td align="left">
                    <input name="txtLingDuiTelephone" type="text" class="formsize80 input-txt" id="txtLingDuiTelephone"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    司机服务：
                </td>
                <td align="left">
                    <%=DriverService %>
                   <%-- <select name="txtSiJiFuWu" id="txtSiJiFuWu">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.DriverService))) %>
                    </select>--%>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    认路情况：
                </td>
                <td align="left">
                <%=FindWay %>
                    <%--<select name="txtRenLu" id="txtRenLu">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.FindWay))) %>
                    </select>--%>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件上传：
                </td>
                <td colspan="3" align="left">
                    <uc1:UploadControl ID="txtFuJian" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                        IsUploadMore="true" IsUploadSelf="true" />
                </td>
            </tr>
            <tr>
                <td width="14%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    建议：
                </td>
                <td height="58" colspan="3" align="left">
                    <textarea name="txtJianYi" class="formsize450" style="height: 40px; padding: 3px;"
                        id="txtJianYi" runat="server"></textarea>
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
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
//            iPage.init();
        });
    </script>

</asp:Content>
