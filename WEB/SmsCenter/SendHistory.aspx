<%@ Page Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SendHistory.aspx.cs" Inherits="Web.SmsCenter.SendHistory" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/SysTop.ascx" TagName="SysTop" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div class="mainbox">
        <uc1:SysTop ID="SysTop1" runat="Server"></uc1:SysTop>
        <form id="form1" method="get">
        <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
        <div class="searchbox fixed" style="border-bottom: none;">
            <span class="searchT">
                <p>
                    发送时间：
                    <input type="text" id="iptStartTime" name="iptStartTime" onfocus="WdatePicker()"
                        class="inputtext formsize100" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iptStartTime") %>" />-
                    <input type="text" id="iptEndTime" name="iptEndTime" onfocus="WdatePicker()" class="inputtext formsize100"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iptEndTime") %>" />
                    关键字：
                    <input id="iptKeyWord" name="iptKeyWord" type="text" class="inputtext formsize120"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iptKeyWord") %>" />
                    发送状态：
                    <select id="ddlStatus" name="ddlStatus" class=" inputselect">
                        <option value="1">发送成功</option>
                        <option value="2">发送失败</option>
                    </select>
                    <input class="search-btn" type="submit" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a href="javascript:" hidefocus="true" class="toolbar_dayin"
                    id="toolbar_dayin"><span>打印</span> </a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:" hidefocus="true" class="toolbar_daochu"
                    id="toolbar_daochu"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellpadding="0" cellspacing="0" id="liststyle">
                <tr>
                    <th align="center" class="th-line" width="15%">
                        发送时间
                    </th>
                    <th align="center" class="th-line" width="5%">
                        号码
                    </th>
                    <th align="center" class="th-line" >
                        发送内容
                    </th>
                    <th align="center" class="th-line" width="7%">
                        费用
                    </th>
                    <th align="center" class="th-line" width="7%">
                        状态
                    </th>
                </tr>
                <cc2:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("IssueTime")%>
                            </td>
                            <td align="center">
                                <a href="javascript:" onclick="return GoSendMobileList('<%#Eval("PlanId").ToString()%>');">查看</a>
                            </td>
                            <td align="center">
                                <%#Eval("Content").ToString()%>
                            </td>
                            <td align="right">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Amount"), this.ProviderToMoney)%>
                            </td>
                            <td align="center">
                                <%#Eval("Status").ToString()%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc2:CustomRepeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">

        $("#ddlStatus").val('<%=Request.QueryString["ddlStatus"] %>');
        function GoSendMobileList(PlanId) {
            parent.Boxy.iframeDialog({
                iframeUrl: "SendMoblieList.aspx?PlanId="+PlanId, title: '短信中心-发送历史', modal: true, width: "600px", height: "348px"
            }); return false
        }
        $("#toolbar_dayin").click(function() {
            PrintPage("#a_print");
            return false;
        });
        $("#toolbar_daochu").click(function() {
            toXls1();
            return false;
        });
        $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <!-- InstanceEndEditable -->
</asp:Content>
