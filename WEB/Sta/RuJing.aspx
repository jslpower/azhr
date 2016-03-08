<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuJing.aspx.cs" Inherits="EyouSoft.Web.Sta.RuJing"
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
                    日期：
                    <input type="text" class="formsize80 input-txt" id="txtSTime" name="txtSTime" onfocus="WdatePicker()" />
                    -
                    <input type="text" class="formsize80 input-txt" id="txtETime" name="txtETime" onfocus="WdatePicker()" />
                    部门：
                    <select name="txtDeptId" id="txtDeptId">
                        <asp:Literal runat="server" ID="ltrDept"></asp:Literal>
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
                <li><s class="daochu"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_daochu" id="i_a_toxls">
                    <span>导出</span></a></li>
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
                        日期
                    </th>
                    <th align="center">
                        入境国籍
                    </th>
                    <th align="center">
                        团队名称
                    </th>
                    <th align="center">
                        人数
                    </th>
                    <th align="center">
                        行程安排
                    </th>
                    <th align="center">
                        游览城市
                    </th>
                    <th align="center">
                        全陪
                    </th>
                    <th align="center">
                        页码号
                    </th>
                    <th align="center">
                        装订序号
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%#Eval("LDate","{0:yyyy-MM-dd}") %>
                    </td>
                    <td align="center">
                        <%#Eval("GuoJi")%>
                    </td>
                    <td align="center">
                        <%#Eval("RouteName") %>
                    </td>
                    <td align="center">
                        <%#Eval("RJRS") %>
                    </td>
                    <td align="center">
                        <%#Eval("XingChengAnPai") %>
                    </td>
                    <td align="center">
                        <%#Eval("YouLanChengShi")%>
                    </td>
                    <td align="center">
                        <%#Eval("QuanPeiName")%>
                    </td>
                    <td align="center">
                        <%--<%#Eval("YeMaHao")%>--%>
                        <%#this.pageIndex+"-"+Eval("ZhuangDingXuHao")%>
                    </td>
                    <td align="center">
                        <%#Eval("ZhuangDingXuHao")%>
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
