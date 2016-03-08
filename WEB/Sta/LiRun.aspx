<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiRun.aspx.cs" Inherits="EyouSoft.Web.Sta.LiRun"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    线路区域：<select id="txtAreaId" name="txtAreaId">
                        <asp:Literal runat="server" ID="ltrArea"></asp:Literal>
                    </select>
                    抵达时间：
                    <input type="text" class="formsize80 input-txt" id="txtSTime" name="txtSTime" onfocus="WdatePicker()" />
                    -
                    <input type="text" class="formsize80 input-txt" id="txtETime" name="txtETime" onfocus="WdatePicker()" />
                    部门：
                    <select name="txtDeptId" id="txtDeptId">
                        <asp:Literal runat="server" ID="ltrDept"></asp:Literal>
                    </select>
                    业务员：
                    <uc1:SellsSelect ID="txtXiaoShouYuan" runat="server" SelectFrist="false" ReadOnly="true"
                        SetTitle="业务员" />
                    客户单位：
                    <uc1:CustomerUnitSelect ID="txtKeHu" runat="server" SelectFrist="false"
                        ReadOnly="true" />
                    毛利：
                    <select name="txtMaoLi1" id="txtMaoLi1">
                        <option value="">请选择</option>
                        <option value="2">≤</option>
                        <option value="0">≥</option>
                        <option value="1">＝</option>
                    </select>
                    <input type="text" class="formsize40 input-txt" id="txtMaoLi2" name="txtMaoLi2" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <%--<li><s class="dayin"></s><a href="#" hidefocus="true" class="toolbar_dayin"><span>打印</span></a></li>
                <li class="line"></li>--%>
                <li><s class="daochu"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_daochu"
                    id="i_a_toxls"><span>
                    导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" rowspan="2" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th rowspan="2" align="center">
                        团号
                    </th>
                    <th rowspan="2" align="center">
                        线路区域
                    </th>
                    <th rowspan="2" align="center">
                        线路名称
                    </th>
                    <th colspan="3" align="center">
                        入境人数
                    </th>
                    <th colspan="2" align="center">
                        签单人数
                    </th>
                    <th rowspan="2" align="left">
                        客户单位
                    </th>
                    <th rowspan="2" align="center">
                        业务员
                    </th>
                    <th rowspan="2" align="right">
                        应收款
                    </th>
                    <th rowspan="2" align="right">
                        应付款
                    </th>
                    <th rowspan="2" align="right">
                        毛利
                    </th>
                    <th rowspan="2" align="center">
                        人均毛利
                    </th>
                    <th rowspan="2" align="center">
                        GST
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line nojiacu" style="width:45px">
                        成人
                    </th>
                    <th align="center" class="th-line nojiacu" style="width: 45px">
                        儿童
                    </th>
                    <th align="center" class="th-line nojiacu" style="width: 45px">
                        领队
                    </th>
                    <th align="center" class="th-line nojiacu" style="width: 45px">
                        成人
                    </th>
                    <th align="center" class="th-line nojiacu" style="width: 45px">
                        儿童
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%#Eval("TourCode") %>
                    </td>
                    <td align="center">
                        <%#Eval("AreaName") %>
                    </td>
                    <td align="center">
                        <%#Eval("RouteName")%>
                    </td>
                    <td align="center">
                        <%#Eval("RJCR")%>
                    </td>
                    <td align="center">
                        <%#Eval("RJET")%>
                    </td>
                    <td align="center">
                        <%#Eval("RJLD")%>
                    </td>
                    <td align="center">
                        <%#Eval("GWCR")%>
                    </td>
                    <td align="center">
                        <%#Eval("GWET")%>
                    </td>
                    <td align="left">
                        <%#Eval("KeHuName")%>
                    </td>
                    <td align="center">
                        <%#Eval("XiaoShouYuanName")%>
                    </td>
                    <td align="right">
                        <%#Eval("YingShouJinE","{0:C2}")%>
                    </td>
                    <td align="right">
                        <%#Eval("YingFuJinE","{0:C2}")%>
                    </td>
                    <td align="right">
                        <%#Eval("MaoLi","{0:C2}")%>
                    </td>
                    <td align="right">
                        <%#Eval("RenJunMaoLi","{0:C2}")%>
                    </td>
                    <td align="center">
                        <%#(bool)Eval("IsTax")?"√":"×"%>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phHeJi">
                <tr>
                    <td colspan="11" align="right">
                        合计：
                    </td>
                    <td align="right">
                        <asp:Literal runat="server" ID="ltrYSHJ"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Literal runat="server" ID="ltrYFHJ"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Literal runat="server" ID="ltrMLHJ"></asp:Literal>
                    </td>
                    <td align="center">
                        
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="ltrGWFLHJ" Visible="false"></asp:Literal>
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
        });    
    </script>
</asp:Content>
