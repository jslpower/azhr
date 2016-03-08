<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouYeJi.aspx.cs" Inherits="EyouSoft.Web.Sta.DaoYouYeJi"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/DuoXuanGouWuDian.ascx" TagName="DuoXuanGouWuDian"
    TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <div class="tablehead border-bot">
            <ul class="fixed">
                <li><a href="DaoYouYeJi.aspx?sl=<%=SL %>" class="ztorderform de-ztorderform"><s class="orderformicon">
                </s><span>业绩统计</span></a></li>
                <li><a href="DaoYouYeJiPaiMing.aspx?sl=<%=SL %>" class="ztorderform"><s class="orderformicon"></s><span>
                    业绩排名</span></a></li>
                <li><a href="DaoYouDaiTuanPaiMing.aspx?sl=<%=SL %>" class="ztorderform"><s class="orderformicon"></s><span>
                    带团人数排名</span></a></li>
            </ul>
        </div>
        <div class="searchbox border-bot fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    抵达时间：
                    <input type="text" class="formsize80 input-txt" name="txtSTime" id="txtSTime" onfocus="WdatePicker()" />
                    -
                    <input type="text" class="formsize80 input-txt" name="txtETime" id="txtETime" onfocus="WdatePicker()" />
                    导游：
                    <uc1:SellsSelect ID="txtDaoYou" runat="server" SelectFrist="false" ReadOnly="true"
                        SetTitle="导游" />
                    <%--购物店：
                    <input type="text" class="formsize140 input-txt" name="txtGysName" id="txtGysName" />--%>
                    购物店：
                    <uc1:DuoXuanGouWuDian ID="txtGWD"  runat="server"  />
                    <button type="submit" class="search-btn">
                        搜索</button>
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <%--<li><s class="dayin"></s><a href="#" hidefocus="true" class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>--%>
                <li><s class="daochu"></s><a href="#" hidefocus="true" class="toolbar_daochu" id="i_a_toxls"><span>
                    导出列表</span></a></li>
                <%--<li class="line"></li>
                <li><s class="tongji"></s><a href="tongjitu.html" hidefocus="true"><span>统计图</span></a></li>--%>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" height="32" rowspan="2" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th rowspan="2" align="center">
                        导游
                    </th>
                    <th rowspan="2" align="center">
                        团号
                    </th>
                    <th colspan="3" align="center">
                        入境人数
                    </th>
                    <th colspan="2" align="center">
                        结算人数
                    </th>
                    <th rowspan="2" align="center">
                        签单比例
                    </th>
                    <th rowspan="2" align="center">
                        全社比例
                    </th>
                    <th rowspan="2" align="center">
                        购物明细
                    </th>
                </tr>
                <tr>
                    <th width="60" align="center" class="th-line nojiacu">
                        成人
                    </th>
                    <th width="60" align="center" class="th-line nojiacu">
                        儿童
                    </th>
                    <th width="60" align="center" class="th-line nojiacu">
                        领队
                    </th>
                    <th width="60" align="center" class="th-line nojiacu">
                        成人
                    </th>
                    <th width="60" align="center" class="th-line nojiacu">
                        儿童
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%#Eval("DaoYouName") %>
                    </td>
                    <td align="center">
                        <%#Eval("TourCode") %>
                    </td>
                    <td align="center">
                        <%#Eval("RJCR")%>
                    </td>
                    <td align="center">
                        <%#Eval("RJET")%>
                    </td>
                    <td align="center">
                        <%#Eval("RJLd")%>
                    </td>
                    <td align="center">
                        <%#Eval("GWCR")%>
                    </td>
                    <td align="center">
                        <%#Eval("GWET")%>
                    </td>
                    <td align="center">
                        <%#Eval("QDBL")%>
                    </td>
                    <td align="center">
                        <%#QSQDBL%>
                    </td>
                    <td align="center">
                        <span style="display: none;"><%#GetGWXXHtml(Eval("RJRS"), Eval("Gws"))%></span>
                        <a href="javascript:void(0)" class="i_gwxx">明细</a>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phHeJi">
                <tr>
                    <td colspan="3" align="right">
                        合计：
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="ltrRJCRHJ"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="ltrRJETHJ"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="ltrRJLDHJ"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="ltrGWCRHJ"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="ltrGWETHJ"></asp:Literal>
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="30">
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

            $(".i_gwxx").bt({ contentSelector: function() { return $(this).prev("span").html(); }, positions: ['bottom'], fill: '#FFF2B5', strokeStyle: '#D59228', noShadowOpts: { strokeStyle: "#D59228" }, spikeLength: 5, spikeGirth: 15, width: 550, overlap: 0, centerPointY: 4, cornerRadius: 4, shadow: true, shadowColor: 'rgba(0,0,0,.5)', cssStyles: { color: '#00387E', 'line-height': '200%'} });
        });    
    </script>
</asp:Content>
