<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuanDuiFuWuEdit.aspx.cs"
    Title="团队服务" Inherits="EyouSoft.Web.QC.TuanDuiFuWuEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="/UserControl/TuanHaoXuanYong.ascx" TagName="TuanHaoXuanYong" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form runat="server" id="form1">
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        团号：
                    </td>
                    <td width="39%" align="left">
                        <uc1:TuanHaoXuanYong ID="txtTuanHaoXuanYong" runat="server" SelectFrist="false" />
                    </td>
                    <td width="11%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        导游1：
                    </td>
                    <td width="36%" align="left">
                        <asp:TextBox runat="server" ID="txtGuideOneName" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        导游2：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtGuideTwoName" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <td align="right" class="alertboxTableT">
                        日期：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtQCTime" CssClass="inputtext formsize140" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        领队：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtLeaderName" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        领队电话：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtLeaderTel" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        行程：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTrip" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtTripRemark" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        景点：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlScenic" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtScenicRemark" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        各地酒店：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlHotel" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtHotelRemark" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        餐饮：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFood" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtFoodRemark" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        导游1：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlGuideOne" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtGuideOneRemark" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        导游2：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlGuideTwo" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtGuideTwoRemark" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        附件上传：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:UploadControl ID="txtFuJian" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                            IsUploadMore="true" IsUploadSelf="true" />
                    </td>
                </tr>
                <tr>
                    <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        建议：
                    </td>
                    <td height="58" align="left" colspan="3">
                        <asp:TextBox runat="server" ID="txtAdvice" TextMode="MultiLine" CssClass="inputtext formsize450"
                            Height="40"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
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
                var _v = ValiDatorForm.validator($("#<%=this.form1.ClientID %>").get(0), "parent");
                if (!_v) return false;

                $(obj).unbind("click").css({ "color": "#999999" });

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit", data: $("#<%=this.form1.ClientID %>").serialize(), cache: false, dataType: "json", async: false,
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
