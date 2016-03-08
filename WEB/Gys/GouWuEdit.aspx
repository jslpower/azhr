﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GouWuEdit.aspx.cs" Inherits="EyouSoft.Web.Gys.GouWuEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/UserControl/GysGwHeTong.ascx" TagName="GysGwHeTong" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/GysLxr.ascx" TagName="GysLxr" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/GysGwChanPin.ascx" TagName="GysGwChanPin" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 10%">
                    国家：
                </td>
                <td align="left">
                    <select id="txtCountryId" name="txtCountryId">
                    </select>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 10%">
                    省份：
                </td>
                <td style="width: 15%" align="left">
                    <select id="txtProvinceId" name="txtProvinceId">
                    </select>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 10%">
                    城市：
                </td>
                <td style="width: 15%" align="left">
                    <select id="txtCityId" name="txtCityId">
                    </select>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 10%">
                    县区：
                </td>
                <td align="left" style="width: 15%">
                    <select id="txtDistrictId" name="txtDistrictId">
                    </select>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    单位名称：
                </td>
                <td colspan="3" align="left">
                    <input name="txtName" type="text" id="txtName" style="width: 260px;" class="input-txt"
                        runat="server" valid="required" errmsg="请输入单位名称" maxlength="255" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    地址：
                </td>
                <td colspan="3">
                    <input name="txtDiZhi" type="text" id="txtDiZhi" style="width: 260px;" class="input-txt"
                        runat="server" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    商品类别
                </td>
                <td colspan="7">
                    <input name="txtShangPinLeiBie" type="text" class="input-txt" id="txtShangPinLeiBie"
                        style="width: 600px;" runat="server" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    报价：
                </td>
                <td colspan="7">
                    成人人头
                    <input name="txtCR" type="text" class="formsize40 input-txt" id="txtCR" runat="server"
                        valid="isNumber" errmsg="请输入正确的成人人头" maxlength="10" />
                    儿童人头
                    <input name="txtET" type="text" class="formsize40 input-txt" id="txtET" runat="server"
                        valid="isNumber" errmsg="请输入正确的儿童人头" maxlength="10" />
                    流水
                    <input name="txtLS" type="text" class="formsize40 input-txt" id="txtLS" runat="server"
                        valid="isNumber" errmsg="请输入正确的流水" maxlength="10" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    简介：
                </td>
                <td height="53" colspan="7" align="left">
                    <textarea name="txtJianJie" id="txtJianJie" class="formsize600" style="height: 40px;"
                        runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td height="53" colspan="7" align="left">
                    <textarea name="txtBeiZhu" id="txtBeiZhu" class="formsize600" style="height: 40px;"
                        runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否推荐：
                </td>
                <td align="left">
                    <input type="radio" name="txtIsTuiJian" id="txtIsTuiJian1" style="border: none;"
                        value="1" /><label for="txtIsTuiJian1">是</label>
                    <input type="radio" name="txtIsTuiJian" id="txtIsTuiJian0" style="border: none" value="0" /><label
                        for="txtIsTuiJian0">否</label>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    签订合同：
                </td>
                <td colspan="5" align="left">
                    <input type="radio" name="txtIsHeTong" id="txtIsHeTong1" style="border: none;" value="1" /><label
                        for="txtIsHeTong1">是</label>
                    <input type="radio" name="txtIsHeTong" id="txtIsHeTong0" style="border: none" value="0" /><label
                        for="txtIsHeTong0">否</label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    结算方式：
                </td>
                <td align="left">
                    <select id="txtJieSuanFangShi" name="txtJieSuanFangShi">
                        <option value="0">现付</option>
                        <option value="1">挂账</option>
                    </select>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    挂账期限：
                </td>
                <td colspan="5">
                    <input name="txtGuaZhangQiXian" id="txtGuaZhangQiXian" type="text" class="input-txt"
                        runat="server" style="width: 260px;" maxlength="255" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    最后维护人：
                </td>
                <td align="left">
                    <input name="txtWeiHuRen" type="text" class="formsize80 input-txt" id="txtWeiHuRen"
                        value="胡晓明" disabled="disabled" runat="server" />
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    维护时间：
                </td>
                <td colspan="5" align="left">
                    <input name="txtWeiHuTime" type="text" class="formsize80 input-txt" id="txtWeiHuTime"
                        value="2013-03-04" disabled="disabled" runat="server" />
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <uc1:GysGwHeTong runat="server" ID="GysGwHeTong1" />
        <div class="hr_10">
        </div>
        <uc1:GysGwChanPin runat="server" ID="GysGwChanPin1" />
        <div class="hr_10">
        </div>
        <uc1:GysLxr runat="server" ID="GysLxr1" />
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

                $("input[name='txtLxrBitrhdayTiXing']").each(function() {
                    $(this).after('<input type="hidden" name="txtLxrBitrhdayTiXing1" value="' + (this.checked ? "1" : "0") + '" />');
                });
                $("input[name='txtHeTongIsQiYong']").each(function() {
                    $(this).after('<input type="hidden" name="txtHeTongIsQiYong1" value="' + (this.checked ? "1" : "0") + '" />');
                });

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
                $("input[name='txtIsTuiJian']").each(function() { if (this.value == "<%=IsTuiJian %>") this.checked = true; });
                $("input[name='txtIsHeTong']").each(function() { if (this.value == "<%=IsHeTong %>") this.checked = true; });               
                $("#txtJieSuanFangShi").val("<%=JieSuanFangShi %>");
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
            pcToobar.init({ gID: "#txtCountryId", pID: "#txtProvinceId", cID: "#txtCityId", xID: "#txtDistrictId", gSelect: '<%=CountryId %>', pSelect: '<%=ProvinceId%>', cSelect: '<%=CityId%>', xSelect: '<%=DistrictId %>' });
            iPage.init();
        });
    </script>

</asp:Content>
