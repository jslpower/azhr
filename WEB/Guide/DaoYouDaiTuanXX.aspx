<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouDaiTuanXX.aspx.cs"
    Inherits="EyouSoft.Web.Guide.DaoYouDaiTuanXX" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox" id="alertbox" style="padding-top: 5px;">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto;">
            <tr>
                <td width="89%" height="28" align="left" bgcolor="#C1E5F5">
                    <form id="form1">
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    <input type="hidden" name="daoyouid" value="<%=DaoYouId %>" />
                    <input type="hidden" name="iframeId" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>" />                   
                    团号：<input type="text" size="20" class="formsize80 input-txt" name="txtTourCode" id="txtTourCode">
                    线路名称：<input type="text" size="20" class="formsize80 input-txt" name="txtRouteName" id="txtRouteName">
                    <span class="alertboxTableT">出团时间：</span>
                    <input name="txtSLDate" type="text" class="formsize80 input-txt" id="txtSLDate" onfocus="WdatePicker()" />
                    -
                    <input name="txtELDate" type="text" class="formsize80 input-txt" id="txtELDate" onfocus="WdatePicker()" />
                    <span class="alertboxTableT">带团时间：</span>
                    <input name="txtSDTTime" type="text" class="formsize80 input-txt" id="txtSDTTime"
                        onfocus="WdatePicker()" />
                    -
                    <input name="txtEDTTime" type="text" class="formsize80 input-txt" id="txtEDTTime"
                        onfocus="WdatePicker()" />
                    <input type="submit" style="cursor: pointer; height: 24px; width: 64px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;" class="search-btn" value="搜索">
                    </form>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto;">
            <tr>
                <td width="6%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路区域
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    出团时间
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    行程天数
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    人数
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    带团时间
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
            <tr>
                <td height="28" align="center" bgcolor="#FFFFFF">
                    <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                </td>
                <td align="left" bgcolor="#FFFFFF">
                    <%#Eval("TourCode") %>
                </td>
                <td align="left" bgcolor="#FFFFFF">
                    <%#Eval("AreaName") %>
                </td>
                <td align="left" bgcolor="#FFFFFF">
                    <%#Eval("RouteName") %>
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <%#Eval("LDate","{0:yyyy-MM-dd}") %>
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <%#Eval("XCTS") %>
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <%#Eval("RJRS")%>
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <%#Eval("DTSTime", "{0:yyyy-MM-dd}")%>至<%#Eval("DTETime", "{0:yyyy-MM-dd}")%>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                    <td colspan="10" style="height: 28px;">
                        暂无带团明细
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
        <div style="position: relative; height: 32px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function() {
            utilsUri.initSearch();
        });
    </script>
    
</asp:Content>
