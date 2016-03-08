<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShangTuanTongJi.aspx.cs"
    Inherits="EyouSoft.Web.Guide.ShangTuanTongJi" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    导游姓名：<input type="text" size="20" class="input-txt" name="txtDaoYouName" id="txtDaoYouName">
                    团号：<input type="text" size="20" class="input-txt" name="txtTourCode" id="txtTourCode">
                    线路名称：<input type="text" size="20" class="input-txt" name="txtRouteName" id="txtRouteName">
                    上团时间：
                    <input type="text" class="formsize80 input-txt" id="txtSDTTime" name="txtSDTTime"
                        onfocus="WdatePicker()">
                    -
                    <input type="text" class="formsize80 input-txt" id="txtEDTTime" name="txtEDTTime"
                        onfocus="WdatePicker()">
                    排序：<select name="txtPaiXu" id="txtPaiXu">
                        <option value="0">团队数从高到低</option>
                        <option value="1">团队数从低到高</option>
                        <option value="2">团天数从高到低</option>
                        <option value="3">团天数从低到高</option>
                    </select>
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">               
                <li><s class="daochu"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_daochu"
                    id="i_a_toxls"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table border="0" cellspacing="0" id="liststyle" width="100%">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center">
                        序号
                    </th>
                    <th align="center">
                        导游姓名
                    </th>
                    <th align="center">
                        团队数
                    </th>
                    <th align="center">
                        团天数
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr i_daoyouid="<%#Eval("DaoYouId") %>">
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                    </td>
                    <td align="center">
                        <%#Eval("DaoYouName") %>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="i_daituanxx"><%#Eval("TuanDuiShu")%></a>
                    </td>
                    <td align="center">
                        <%#Eval("TuanTianShu") %>
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
        <div class="tablehead border-bot" id="i_div_tool_paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            initToolbar: function() {
                tableToolbar.init({});
                toXls.init({ "selector": "#i_a_toxls" });
            },
            daiTuanXX: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", daoyouid: _$tr.attr("i_daoyouid"), txtSDTTime: '<%=Utils.GetQueryStringValue("txtSDTTime") %>', txtEDTTime: '<%=Utils.GetQueryStringValue("txtEDTTime") %>', txtTourCode: '<%=Utils.GetQueryStringValue("txtTourCode") %>', txtRouteName: '<%=Utils.GetQueryStringValue("txtRouteName") %>' };
                Boxy.iframeDialog({ title: "带团明细", iframeUrl: "DaoYouDaiTuanXX.aspx", width: "940px", height: "500px", data: _data, afterHide: function() { } });
                return false;
            }
        };

        $(document).ready(function() {
            utilsUri.initSearch();
            iPage.initToolbar();
            $("#i_div_tool_paging_1").children().clone(true).prependTo("#i_div_tool_paging_2");
            $(".i_daituanxx").click(function() { iPage.daiTuanXX(this); });
        });
    
    </script>

</asp:Content>
