<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="PlanStaJingDian.aspx.cs" Inherits="EyouSoft.Web.Plan.PlanStaJingDian" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SupplierControl.ascx" TagName="supplierControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead border-bot">
            <ul class="fixed">
                <li><a href="PlanStaJiuDian.aspx?sl=<%=SL %>" class="ztorderform"><s
                    class="orderformicon"></s><span>酒店</span></a></li>
                <li><a href="PlanStaCanGuan.aspx?sl=<%=SL %>" class="ztorderform"><s class="orderformicon">
                </s><span>餐厅</span></a></li>
                <li><a href="PlanStaCheDui.aspx?sl=<%=SL %>" class="ztorderform"><s class="orderformicon">
                </s><span>车队</span></a></li>
                <li><a href="PlanStaJingDian.aspx?sl=<%=SL %>" class="ztorderform de-ztorderform"><s class="orderformicon">
                </s><span>景点<秀></span></a></li>
            </ul>
        </div>
        <div class="searchbox border-bot fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    景点公司名称：<uc1:supplierControl ID="supplierControl1" runat="server" SupplierType="景点"
                        IsMust="false" />
                    景点名称：<input type="text" name="txtJingDian" class="inputtext formsize120" value="<%=Utils.GetQueryStringValue("txtJingDian") %>" />
                    团号：<input type="text" name="txtTourCode" class="inputtext formsize120" value="<%=Utils.GetQueryStringValue("txtTourCode") %>" />
                    抵达时间：
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtStartTime"
                        name="txtStartTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtStartTime") %>" />
                    -
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtEndTime"
                        name="txtEndTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndTime") %>" />
                    <button type="submit" class="search-btn">
                        搜索</button>
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <li><s class="daochu"></s><a href="#" hidefocus="true" class="toolbar_daochu" id="i_a_toxls">
                    <span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" height="32" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                     <th align="center">
                        城市
                    </th>
                    <th align="center">
                        景点公司名称
                    </th>
                    <th align="center">
                        团号
                    </th>
                    <th align="center">
                        安排明细
                    </th>
                    <th align="center">
                        导游
                    </th>
                    <th align="center">
                        备注
                    </th>
                    <th align="center">
                        状态
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("CityName")%>
                            </td>
                            <td align="center">
                                <%#Eval("SourceName")%>
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <%# GetAPMX(Eval("PlanAttractionsList"))%>
                            </td>
                            <td align="center">
                                <a href='javascript:void(0);' style="text-decoration: none" data-class="GuidShow">
                                    <%# GetGuidInfo(Eval("GuidList"), "0")%>
                                </a>
                                <div style="display: none">
                                    <%# GetGuidInfo(Eval("GuidList"), "1")%>
                                </div>
                            </td>
                            <td align="center">
                                <%#Eval("GuideNotes")%>
                            </td>
                            <td align="center">
                                <%#Eval("Status").ToString()%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="8" align="center">
                            暂无统计信息
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead border-bot" id="i_div_tool_paging_2">
        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function() {
            utilsUri.initSearch();
            tableToolbar.init({});
            toXls.init({ "selector": "#i_a_toxls" });

            $("#i_div_tool_paging_1").children().clone(true).prependTo("#i_div_tool_paging_2");

            //导游位泡泡
            $("#liststyle").find("[data-class='GuidShow']").bt({
                contentSelector: function() {
                    return $(this).siblings("div").html();
                },
                positions: ['left', 'right', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 15,
                width: 200,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            });
        });    
    </script>

</asp:Content>
