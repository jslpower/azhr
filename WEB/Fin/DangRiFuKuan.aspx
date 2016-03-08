<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangRiFuKuan.aspx.cs" Inherits="EyouSoft.Web.Fin.DangRiFuKuan" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="DangRiDuiZhang.aspx" method="get">
        <div id="div_skipBtn" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="YingFu.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform de-ztorderform"><span>应付账款</span></a></li>
                <li><s class="orderformicon"></s><a href="YingFuShenPi.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>付款审批</span></a></li>
                <li><s class="orderformicon"></s><a href="YingFu.aspx?sl=<%=Request.QueryString["sl"] %>&IsClean=1"
                    hidefocus="true" class="ztorderform"><span>已结清账款</span></a></li>
            </ul>
        </div>
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：
                    <input type="text" name="TourCode" value="<%=Request.QueryString["TourCode"] %>" class="inputtext formsize140">
                    供应商：
                    <input type="text" name="Supplier"  value="<%=Request.QueryString["Supplier"] %>" class="inputtext formsize180">
                    请款人：
                    <uc1:SellsSelect ID="txt_SellsSelect1" runat="server" SetTitle="请款人" SelectFrist="false" />
                    <button class="search-btn" type="submit">
                        搜索</button></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a class="toolbar_dayin" hidefocus="true" href="#"><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="toolbar_daochu" hidefocus="true" id="toolbar_daochu"
                    href="javascript:void(0);"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" class="th-line">
                            计调项
                        </th>
                        <th align="center" class="th-line">
                            团号
                        </th>
                        <th align="left" class="th-line">
                            供应商
                        </th>
                        <th align="center" class="th-line">
                            销售员
                        </th>
                        <th align="center" class="th-line">
                            OP
                        </th>
                        <th align="center" class="th-line">
                            请款人
                        </th>
                        <th align="right" class="th-line">
                            请款金额
                        </th>
                        <th align="right" class="th-line">
                            支付方式
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    <input type="checkbox">
                                </td>
                                <td align="center">
                                    <%#Eval("PlanTyp")%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0);">
                                        <%#Eval("TourCode")%></a>
                                </td>
                                <td align="left">
                                    <%#Eval("Supplier")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Salesman")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Planer")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Dealer")%>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PaymentAmount"), ProviderToMoney)%></b>
                                </td>
                                <td align="right">
                                    <%#Eval("PaymentName")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_msg" runat="server">
                        <tr align="center">
                            <td colspan="9">
                                暂无数据!
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr class="">
                        <td align="right" colspan="7">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <asp:Label ID="lbl_sum" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            $("#toolbar_daochu").click(function() {
                toXls1();
                return false;
            })
        })
    </script>

</asp:Content>
