<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenTianShi.aspx.cs" Inherits="EyouSoft.Web.Sta.RenTianShi"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    抵达时间：
                    <input type="text" class="formsize80 input-txt" id="txtSTime" name="txtSTime" onfocus="WdatePicker()" />
                    -
                    <input type="text" class="formsize80 input-txt" id="txtETime" name="txtETime" onfocus="WdatePicker()" />
                    部门：
                    <select name="txtDeptId" id="txtDeptId">
                        <asp:Literal runat="server" ID="ltrDept"></asp:Literal>
                    </select>
                    国家：
                    <select id="txtCountryId" name="txtCountryId">
                    </select>
                    省份：
                    <select id="txtProvinceId" name="txtProvinceId">
                    </select>
                    城市：
                    <select id="txtCityId" name="txtCityId">
                    </select>
                    县区：
                    <select id="CountyId" name="CountyId">
                    </select>
                    <%--住宿晚数：
                    <select name="txtWanShu1" id="txtWanShu1">
                        <option value="">请选择</option>
                        <option value="2">≤</option>
                        <option value="0">≥</option>
                        <option value="1">＝</option>
                    </select>
                    <input type="text" class="formsize40 input-txt" name="txtWanShu2" id="txtWanShu2" />--%>
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
                <li><s class="daochu"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_daochu"
                    id="i_a_toxls"><span>
                    导出列表</span></a></li>
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
                        国家或地区
                    </th>
                    <th align="center">
                        部门
                    </th>
                    <th align="center">
                        团号
                    </th>
                    <th align="center">
                        人数
                    </th>
                    <th align="center">
                        （地域）
                    </th>
                    <th align="center">
                        到（地域）时间
                    </th>
                    <th align="center">
                        离（地域）日期
                    </th>
                    <th align="center">
                        在（地域）住宿（晚）
                    </th>
                    <th align="center">
                        人天数
                    </th>
                    <th align="center">
                        入住酒店
                    </th>
                    <th align="center">
                        入境航班号
                    </th>
                    <th align="center">
                        出境航班号
                    </th>
                    <th align="center">
                        领队
                    </th>
                    <th align="center">
                        导游
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%#Eval("GuoJi")%>
                    </td>
                    <td align="center">
                        <%#Eval("DeptName")%>
                    </td>
                    <td align="center">
                        <%#Eval("TourCode")%>
                    </td>
                    <td align="center">
                        <%#Eval("RJCR")%>+<%#Eval("RJET")%>+<%#Eval("RJLD")%>
                    </td>
                    <td align="center">
                        <%#Eval("CountyName")%>
                    </td>
                    <td align="center">
                        <%#Eval("XCSTime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <%#Eval("XCETime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                       <%#Eval("ZhuSuWanShu")%>
                    </td>
                    <td align="center">
                        <%#Eval("RTS")%>
                    </td>
                    <td align="center">
                        <%#Eval("JiuDianName")%>
                    </td>
                    <td align="center">
                        <%#Eval("RHangBan")%>
                    </td>
                    <td align="center">
                        <%#Eval("CHangBan")%>
                    </td>
                    <td align="center">
                        <%#Eval("LingDuiName")%>
                    </td>
                    <td align="center">
                        <%#Eval("DaoYouName")%>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
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
            pcToobar.init({ gID: "#txtCountryId", pID: "#txtProvinceId", cID: "#txtCityId",xID:"#CountyId", gSelect: '<%=Utils.GetQueryStringValue("txtCountryId") %>', pSelect: '<%=Utils.GetQueryStringValue("txtProvinceId") %>', cSelect: '<%=Utils.GetQueryStringValue("txtCityId") %>',xSelect:"<%=Utils.GetQueryStringValue("CountyId") %>" });
        });    
    </script>
</asp:Content>
