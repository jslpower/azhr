<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangRiShouKuan.aspx.cs" Inherits="EyouSoft.Web.Fin.DangRiShouKuan" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form action="DangRiShouKuan.aspx" method="get">
            <input type="hidden" name="sl" id="sl" value="<%=Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    客户单位：<uc1:CustomerUnitSelect ID="txtKeHuDanWei" runat="server" />
                    销售员：<uc2:SellsSelect ID="txtXiaoShouYuan" runat="server" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a id="a_print" href="javascript:void(0);" hidefocus="true"
                    class="toolbar_dayin"><span>打印列表</span> </a></li>
                <li class="line"></li>
                <li><s class="excel"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_excel"
                    id="selector_toolbar_toXls"><span>导出Excel</span> </a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        订单号
                    </th>
                    <th align="center" class="th-line">
                        客户单位/游客姓名
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        收款金额
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="left">
                                    <%#Eval("RouteName")%>

                            </td>
                            <td align="center">
                                <%#Eval("OrderCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("Customer")%>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%#Eval("Adults")%></b><sup class="fontred">+<%#Eval("Childs")%></sup>
                            </td>
                            <td align="center">
                                <%#Eval("Salesman")%>
                            </td>
                            <td align="center">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ReceivableAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#Eval("Status")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr align="center">
                        <td colspan="9">
                            暂无数据!
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var dangRiDuiZhang = {
            winParams: {}, self: this
            //初始化查询
        , initSearch: function() {
            //销售员
            $("#<%=txtXiaoShouYuan.SellsIDClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient) %>');
            $("#<%=txtXiaoShouYuan.SellsNameClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient) %>');
            //客户单位
            //window["<%=txtKeHuDanWei.ClientID %>"].SetVal({ "CustomerUnitName": '<%=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHMC) %>', "CustomerUnitId": "<%=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHBH) %>", "CustomerUnitType": "<%=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHLX) %>" })
        }
            //初始化工具栏
        , initToolbar: function() {
            //init toxls
            toXls.init({ "selector": "#selector_toolbar_toXls" });
        }
        , init: function() {
            self.winParams = utilsUri.getUrlParams();
            $("#a_print").click(function() {
                PrintPage("#a_print");
                return false;
            })
        }
        };

        $(document).ready(function() {
            dangRiDuiZhang.init();
            //init search
            dangRiDuiZhang.initSearch();
            //init toolbar
            dangRiDuiZhang.initToolbar();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
        });
    </script>

</asp:Content>
