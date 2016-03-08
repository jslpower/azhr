<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouEdit.aspx.cs" Inherits="EyouSoft.Web.Guide.DaoYouEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <form id="form1">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 100px;">
                    姓名：
                </td>
                <td align="left" style="width: 200px;">
                    <input name="txtXingMing" type="text" class="formsize80 input-txt" id="txtXingMing"
                        runat="server" valid="required" errmsg="请输入姓名" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 100px;">
                    性别：
                </td>
                <td height="28" style="width: 200px;">
                    <select name="txtGender" id="txtGender">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender),new string[]{"2"})) %>
                    </select>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="width: 100px;">
                    级别：
                </td>
                <td height="28">
                    <select name="txtJiBie" id="txtJiBie">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie))) %>
                    </select>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    类别：
                </td>
                <td colspan="3">
                    <input type="checkbox" name="txtLeiBie" id="txtLeiBie0" style="border: none;" value="0" />
                    <label for="txtLeiBie0">
                        领队</label>
                    <input type="checkbox" name="txtLeiBie" id="txtLeiBie1" style="border: none;" value="1" />
                    <label for="txtLeiBie1">
                        全陪</label>
                    <input type="checkbox" name="txtLeiBie" id="txtLeiBie2" style="border: none;" value="2" />
                    <label for="txtLeiBie2">
                        地接
                    </label>
                    <input type="checkbox" name="txtLeiBie" id="txtLeiBie3" style="border: none;" value="3" />
                    <label for="txtLeiBie3">
                        景区
                    </label>
                    <input type="checkbox" name="txtLeiBie" id="txtLeiBie4" style="border: none;" value="4" />
                    <label for="txtLeiBie4">
                        导游兼领队</label>
                </td>
                <td align="right" class="alertboxTableT">
                    专兼职：
                </td>
                <td>
                    <select name="txtZhiWeiLeiXing" id="txtZhiWeiLeiXing">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.ZhiWeiLeiXing))) %>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    语种：
                </td>
                <td colspan="3">
                    <input name="txtYuZhong" type="text" class="input-txt" id="txtYuZhong" style="width: 450px;"
                        runat="server" />
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        照片：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <uc1:UploadControl runat="server" ID="uc_Photo" IsUploadMore="false" IsUploadSelf="true" />
                        <asp:Label runat="server" ID="lbTxtphoto" CssClass="labelFiles"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    身份证号：
                </td>
                <td align="left">
                    <input name="txtShenFenZhengHao" type="text" class="formsize120 input-txt" id="txtShenFenZhengHao"
                        runat="server" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    导游证号：
                </td>
                <td>
                    <input name="txtDaoYouZhengHao" type="text" class="formsize120 input-txt" id="txtDaoYouZhengHao"
                        runat="server" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    领队证号：
                </td>
                <td>
                    <input name="txtLingDuiZhengHao" type="text" class="formsize120 input-txt" id="txtLingDuiZhengHao"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    导游证挂靠单位：
                </td>
                <td align="left">
                    <input name="txtGuaKaoDanWei" type="text" class="formsize120 input-txt" id="txtGuaKaoDanWei"
                        runat="server" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    年审状态：
                </td>
                <td>
                    <select name="txtIsNianShen" id="txtIsNianShen">
                        <option value="0">未审</option>
                        <option value="1">已审</option>
                    </select>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    电话：
                </td>
                <td>
                    <input name="txtTelephone" type="text" class="formsize120 input-txt" id="txtTelephone"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    手机：
                </td>
                <td width="180" align="left">
                    <input name="txtShouJiHao" type="text" class="formsize120 input-txt" id="txtShouJiHao"
                        runat="server" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    QQ：
                </td>
                <td>
                    <input name="txtQQ" type="text" class="formsize120 input-txt" id="txtQQ" runat="server" />
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    E-mail：
                </td>
                <td>
                    <input name="txtEmail" type="text" class="formsize140 input-txt" id="txtEmail" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    MSN-SKYPE：
                </td>
                <td colspan="5">
                    <input name="txtMSN" type="text" class="formsize120 input-txt" id="txtMSN" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    家庭住址：
                </td>
                <td colspan="3">
                    <input name="txtJiaTingDiZhi" type="text" class="formsize450 input-txt" id="txtJiaTingDiZhi"
                        runat="server" />
                </td>
                <td class="alertboxTableT" style="text-align: right">
                    家庭电话：
                </td>
                <td>
                    <input name="txtJiaTingTelephone" type="text" class="formsize120 input-txt" id="txtJiaTingTelephone"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    性格特点：
                </td>
                <td height="40" colspan="5">
                    <textarea name="txtXingGeTeDian" rows="6"  class="formsize600" style="height: 35px;"
                        id="txtXingGeTeDian" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    擅长线路：
                </td>
                <td height="40" colspan="5">
                    <textarea name="txtShanChangXianLu"  rows="6" class="formsize600" style="height: 35px;"
                        id="txtShanChangXianLu" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    客户评价：
                </td>
                <td height="40" colspan="5">
                    <textarea name="txtKeHuPingJia" rows="6" class="formsize600" style="height: 35px;"
                        id="txtKeHuPingJia" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    特长说明：
                </td>
                <td height="40" colspan="5">
                    <textarea name="txtTeChang"  rows="6" class="formsize600" style="height: 35px;"
                        id="txtTeChang" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td height="40" colspan="5">
                    <textarea name="txtBeiZhu"  rows="6" class="formsize600" style="height: 35px;"
                        id="txtBeiZhu" runat="server"></textarea>
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
                $("#txtJiBie").val("<%=JiBie %>");
                $("#txtZhiWeiLeiXing").val("<%=ZhiWeiLeiXing %>");
                $("#txtIsNianShen").val("<%=NianShen %>");
                var _leiBie = "<%=LeiBie %>";
                if (_leiBie.length > 0) { var _arr = _leiBie.split(","); for (var i = 0; i < _arr.length; i++) { $("#txtLeiBie" + _arr[i]).attr("checked", "checked"); } }
            }
        };

        $(document).ready(function() {
            $("#i_a_submit").click(function() { iPage.submit(this); });
            iPage.init();
        });
    </script>

</asp:Content>
