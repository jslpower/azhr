<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JingDianEdit.aspx.cs" Inherits="EyouSoft.Web.Gys.JingDianEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/UserControl/GysHeTong.ascx" TagName="GysHeTong" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/GysLxr.ascx" TagName="GysLxr" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/GysJingDian.ascx" TagName="GysJingDian" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/YuCunKuan.ascx" TagName="YuCunKuan" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <asp:PlaceHolder runat="server" ID="phLng" Visible="false">
            <div style="width: 99%; margin: 0px auto;">
                <a href="javascript:void(0)" id="i_a_lng_1">中文</a> <a href="javascript:void(0)" id="i_a_lng_2">
                    英文</a> <a href="javascript:void(0)" id="i_a_lng_3" style="visibility: hidden">泰文</a></div>
        </asp:PlaceHolder>
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
                    是否签单：
                </td>
                <td align="left">
                    <input type="radio" name="txtIsQianDan" id="txtIsQianDan1" style="border: none;"
                        value="1" /><label for="txtIsQianDan1">是</label>
                    <input type="radio" name="txtIsQianDan" id="txtIsQianDan0" style="border: none" value="0" /><label
                        for="txtIsQianDan0">否</label>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    签订合同：
                </td>
                <td colspan="3" align="left">
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
        <uc1:GysJingDian runat="server" ID="GysJingDian1" />
        <div class="hr_10">
        </div>
        <uc1:GysHeTong runat="server" ID="GysHeTong1" />
        <div class="hr_10">
        </div>
        <uc1:YuCunKuan runat="server" ID="YuCunKuan" />
        <div class="hr_10"></div>
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
                $("input[name='txtJingDianTuiJian']").each(function() {
                    $(this).after('<input type="hidden" name="txtJingDianTuiJian1" value="' + (this.checked ? "1" : "0") + '" />');
                });
            	
            	$("input[name='txtIsXiu']").each(function() {
                    $(this).after('<input type="hidden" name="txtIsXiu1" value="' + (this.checked ? "1" : "0") + '" />');
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
                $("input[name='txtIsQianDan']").each(function() { if (this.value == "<%=IsQianDan %>") this.checked = true; });
                $("#txtJieSuanFangShi").val("<%=JieSuanFangShi %>");

                var _params = utilsUri.getUrlParams(["lng"]);

                $("#i_a_lng_1").attr("href", "JingDianEdit.aspx?" + $.param(_params) + "&lng=1");
                $("#i_a_lng_2").attr("href", "JingDianEdit.aspx?" + $.param(_params) + "&lng=2");
                $("#i_a_lng_3").attr("href", "JingDianEdit.aspx?" + $.param(_params) + "&lng=3");

                $("#i_a_lng_<%=(int)LngType %>").css({ "color": "#ff0000" });

                if ("<%=(int)LngType %>" != "1") {
                    $("#i_div_jiudianbaojia").find("input").attr("readonly", "readonly").css({ "background-color": "#dadada" });
                    $("#i_div_hetong").find(".i_hetongcaozuo").hide();
                    $("#i_div_hetong").find("input").attr("readonly", "readonly").css({ "background-color": "#dadada" });
                    $("#i_div_lxr").find(".i_lxrcaozuo").hide();
                    $("#i_div_lxr").find("input").attr("readonly", "readonly").css({ "background-color": "#dadada" });

                    $("#i_div_jingdian").find(".i_jingdiancaozuo").hide();
                }
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
            pcToobar.init({ gID: "#txtCountryId", pID: "#txtProvinceId", cID: "#txtCityId", xID: "#txtDistrictId", gSelect: '<%=CountryId %>', pSelect: '<%=ProvinceId%>', cSelect: '<%=CityId%>', xSelect: '<%=DistrictId %>' });
            iPage.init();
        });
    </script>

</asp:Content>
