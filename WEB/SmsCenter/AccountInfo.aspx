<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Front.Master"
    CodeBehind="AccountInfo.aspx.cs" Inherits="Web.SysCenter.AccountInfo" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/SysTop.ascx" TagName="SysTop" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- InstanceBeginEditable name="EditRegion3" -->

    <script type="text/javascript">
        $(function() {
            $("#lnkCZ").click(function() {
                var url = $(this).attr("href");
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "我要充值",
                    modal: true,
                    width: "600px",
                    height: "348px"
                });
                return false;
            });
        })
	 
    </script>
    
    <div class="mainbox">
        <uc1:SysTop ID="SysTop1" runat="Server"></uc1:SysTop>
        <div class="hr_10">
        </div>
        <div class="sms-addbox">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk3">
                <tr>
                    <td align="center" bgcolor="#d4ecf7" class="th-line">
                        <strong>充值时间</strong>
                    </td>
                    <td align="center" bgcolor="#d4ecf7" class="th-line">
                        <strong>充值金额</strong>
                    </td>
                    <td align="center" bgcolor="#d4ecf7" class="th-line">
                        <strong>充值状态</strong>
                    </td>
                    <td align="center" bgcolor="#d4ecf7" class="th-line">
                        <strong>充值人</strong>
                    </td>
                </tr>
                <cc2:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"),ProviderToDate)%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ChargeAmount"), this.ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="center">
                                <%#Eval("Status").ToString()=="1"?"<span class=\"fontred\">充值成功("+EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("RealAmount"), this.ProviderToMoney)+")</span>":"充值审核中"%>
                            </td>
                            <td align="center">
                                <%#Eval("ChargeName")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc2:CustomRepeater>
            </table>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
            <table width="60%" border="0" cellspacing="0" cellpadding="0" style="margin: 5px auto;">
                <tr>
                    <td width="40%" align="left">
                        当前您的余额为：<span style="font-family: Arial, Helvetica, sans-serif; font-size: 28px;
                            color: #FF0000; font-weight: bold;">
                            <asp:Literal ID="litBalance" runat="server"></asp:Literal>
                        </span>元
                    </td>
                    <td width="60%" align="left">
                        <a href="/SmsCenter/AccountRecharge.aspx?sl=<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                            id="lnkCZ">
                            <img src="/images/dx-chongzhi.gif" /></a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
