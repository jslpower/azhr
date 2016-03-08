<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouDaiTuanPaiMing.aspx.cs"
    Inherits="EyouSoft.Web.Sta.DaoYouDaiTuanPaiMing" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <div class="tablehead border-bot">
            <ul class="fixed">
                <li><a href="DaoYouYeJi.aspx?sl=<%=SL %>" class="ztorderform"><s class="orderformicon">
                </s><span>业绩统计</span></a></li>
                <li><a href="DaoYouYeJiPaiMing.aspx?sl=<%=SL %>" class="ztorderform"><s class="orderformicon">
                </s><span>业绩排名</span></a></li>
                <li><a href="DaoYouDaiTuanPaiMing.aspx?sl=<%=SL %>" class="ztorderform  de-ztorderform"><s class="orderformicon">
                </s><span>带团人数排名</span></a></li>
            </ul>
        </div>
        
        <div class="searchbox border-bot fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    抵达时间：
                    <input type="text" class="formsize80 input-txt" id="txtSTime" name="txtSTime" onfocus="WdatePicker()" />
                    -
                    <input type="text" class="formsize80 input-txt" id="txtETime" name="txtETime" onfocus="WdatePicker()" />
                    排序：
                    <select name="txtPaiXu" id="txtPaiXu">
                        <option value="0">人数越高</option>
                        <option value="1">人数越低</option>
                    </select>                   
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
                    id="i_a_toxls"><span>导出</span></a></li>
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
                        排名序号
                    </th>
                    <th align="center">
                        导游
                    </th>
                    <th align="center">
                        带团人数
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%# Container.ItemIndex + 1%>
                    </td>
                    <td align="center">
                        <%#Eval("DaoYouName") %>
                    </td>
                    <td align="center">
                        <%#Eval("RS")%>
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
        });    
    </script>

</asp:Content>
