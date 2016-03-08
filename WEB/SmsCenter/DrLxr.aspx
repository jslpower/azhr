<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrLxr.aspx.cs" Inherits="EyouSoft.Web.SmsCenter.DrLxr"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <div class="alertbox-outbox02">
        <table width="99%" height="79" cellspacing="0" cellpadding="0" class="alertboxbk1"
            border="0" bgcolor="#FFFFFF" align="center" style="margin: 0 auto; border-collapse: collapse;"
            id="liststyle">
            <tbody>
                <form id="form1" method="get">
                <tr>
                    <td height="30" align="left" colspan="5">
                        <input id="iframeId" name="iframeId" type="hidden" value='<%=Utils.GetQueryStringValue("iframeId")%>' />
                        类型：<select name="txtLeiXing" class="inputselect"><option value="-1">-请选择-</option>
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType)), Utils.GetQueryStringValue("txtLeiXing"))%></select>
                        国家：
                        <select id="txtCountryId" name="txtCountryId" class="inputselect">
                        </select>
                        省份:
                        <select id="txtProvinceId" name="txtProvinceId" class="inputselect">
                        </select>
                        城市:
                        <select id="txtCityId" name="txtCityId" class="inputselect">
                        </select>
                        <input id="btnSearch" type="submit" style="width: 64px; height: 24px; background: url(&quot;../images/cx.gif&quot;) no-repeat scroll center center transparent;
                            border: 0pt none; margin-left: 5px;" value="查 询" />
                    </td>
                </tr>
                </form>
                <tr>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        <input type="checkbox" name="checkbox" onclick="ImportCustomerNo.SelAll(this);">
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        单位名称-姓名
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        类型
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        所在地
                    </td>
                    <td bgcolor="#B7E0F3" align="center">
                        手机号码
                    </td>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <itemtemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox1">
                            </td>
                            <td align="center">
                                <%#!string.IsNullOrEmpty(Eval("DanWeiName").ToString())?Eval("DanWeiName").ToString()+"-":""%><%#Eval("LxrName")%>
                            </td>
                            <td align="center">
                                <%#Eval("DanWeiType")%>
                            </td>
                            <td align="center">
                                <span class="pandl3">
                                    <%#Eval("CPCD.ProvinceName") %>-<%#Eval("CPCD.CityName") %>
                                </span>
                            </td>
                            <td align="center" name="content">
                                <%#Eval("Mobile").ToString()%>
                            </td>
                        </tr>
                    </itemtemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="12">
                            暂无相关信息
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </tbody>
        </table>
        <div style="position: relative; height: 30px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:" id="btnSelect" onclick="return ImportCustomerNo.Select();">
                <s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var ImportCustomerNo = {
            PWindow: parent.document,
            Select: function() {
                var smsArr = [];
                $("[name='checkbox1']:checked").each(function() {
                    smsArr.push($.trim($(this).parent().parent().find("td[name='content']").text()));
                });
                var txtSendMobile = ImportCustomerNo.PWindow.getElementById("txtSendMobile");
                if (txtSendMobile.value == "") {
                    txtSendMobile.value +=smsArr.join(',');
                } else {
                    txtSendMobile.value +=","+smsArr.join(',');
                }
                window.parent.SendSms.fontNum(txtSendMobile);
                top.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide()
                return false;
            },

            //全选
            SelAll: function(obj) {
                $("input:checkbox").attr("checked", $(obj).attr("checked"));
            }
        };
        
        $(function() {
            pcToobar.init({ gID: "#txtCountryId", pID: "#txtProvinceId", cID: "#txtCityId", gSelect: '<%=Utils.GetQueryStringValue("txtCountryId") %>', pSelect: '<%=Utils.GetQueryStringValue("txtProvinceId") %>', cSelect: '<%=Utils.GetQueryStringValue("txtCityId") %>' });
        });
    </script>
</asp:Content>
