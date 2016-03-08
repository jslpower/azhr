<%@ Page Title="团队服务" Language="C#" AutoEventWireup="true" CodeBehind="TuanDuiFuWu.aspx.cs"
    Inherits="EyouSoft.Web.QC.TuanDuiFuWu" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form method="get" action="/QC/TuanDuiFuWu.aspx">
            <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    团号：
                    <input type="text" name="txtTourCode" class="inputtext formsize120" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />
                    导游名称：
                    <input type="text" name="txtGuideName" class="inputtext formsize120" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtGuideName") %>" />
                    <button class="search-btn" type="submit">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="i_insert">
                    <span>新增</span></a></li><li class="line"></li>
                <li><s class="updateicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_update">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><s class="delicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_delete">
                    <span>删除</span></a></li>
                <li class="line"></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellspacing="0" border="0" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th valign="middle" align="center">
                            团号
                        </th>
                        <th valign="middle" align="center">
                            导游姓名
                        </th>
                        <th valign="middle" align="center">
                            日期
                        </th>
                        <th valign="middle" align="center">
                            领队
                        </th>
                        <th valign="middle" align="center">
                            行程
                        </th>
                        <th valign="middle" align="center">
                            景点
                        </th>
                        <th valign="middle" align="center">
                            酒店
                        </th>
                        <th valign="middle" align="center">
                            餐饮
                        </th>
                        <th valign="middle" align="center">
                            导游1
                        </th>
                        <th valign="middle" align="center">
                            导游2
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %> i_zhijianid="<%#Eval("QCID") %>">
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="checkbox">
                                </td>
                                <td align="center">
                                    <%#Eval("TourCode")%>
                                </td>
                                <td align="left">
                                    <%#Eval("GuideOneName")%>,
                                    <%#Eval("GuideTwoName")%>
                                </td>
                                <td align="center">
                                    <%#this.ToDateTimeString(Eval("QCTime"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("LeaderName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Trip")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Scenic")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Hotel")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Food")%>
                                </td>
                                <td align="center">
                                    <%#Eval("GuideOne")%>
                                </td>
                                <td align="center">
                                    <%#Eval("GuideTwo")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                        <tr>
                            <td colspan="11" align="center">
                                暂无团队服务信息
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead border-bot" id="i_div_tool_paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            insert: function() {
                var _data = { sl: "<%=SL %>" };
                Boxy.iframeDialog({ title: "新增团队服务", iframeUrl: "TuanDuiFuWuEdit.aspx", width: "680px", height: "450px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj[0]);
                var _data = { sl: "<%=SL %>", zhijianid: _$tr.attr("i_zhijianid") };
                Boxy.iframeDialog({ title: "修改团队服务", iframeUrl: "TuanDuiFuWuEdit.aspx", width: "680px", height: "450px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj);
                var _data = [];
                _$tr.each(function() { _data.push($(this).attr("i_zhijianid")); });

                $(".toolbar_delete").unbind("click");

                $.ajax({
                    type: "GET", url: "TuanDuiFuWu.aspx?sl=<%=SL %>&doType=Delete&deleteids=" + _data.join(','), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                            iPage.reload();
                        }
                    },
                    error: function() {
                        alert("请求异常");
                        iPage.reload();
                    }
                });
            },
            initToolbar: function() {
                $(".i_insert").click(iPage.insert);
                var _options = { otherButtons: [] }
                var _self = this;
                _options["updateCallBack"] = function(obj) { _self.update(obj); };
                _options["deleteCallBack"] = function(obj) { _self.del(obj); };
                tableToolbar.init(_options);
            }
        };

        $(document).ready(function() {
            utilsUri.initSearch();
            iPage.initToolbar();
            $("#i_div_tool_paging_1").children().clone(true).prependTo("#i_div_tool_paging_2");
        });
    
    </script>

</asp:Content>
