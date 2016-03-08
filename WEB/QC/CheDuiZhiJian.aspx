<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheDuiZhiJian.aspx.cs"
    Inherits="EyouSoft.Web.QC.CheDuiZhiJian" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    团号：
                    <input type="text" class="formsize120 input-txt" name="txtTourCode" id="txtTourCode" />
                    车队名称：
                    <input type="text" class="formsize120 input-txt" name="txtCheDuiName" id="txtCheDuiName" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="i_insert">
                    <span>新增</span></a></li>
                <li class="line"></li>
                <li><s class="updateicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_update">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><s class="delicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_delete"><span>
                    删除</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table border="0" cellspacing="0" id="liststyle" width="100%">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" valign="middle">
                        团号
                    </th>
                    <th align="center" valign="middle">
                        车队名称
                    </th>
                    <th align="center" valign="middle">
                        车号
                    </th>
                    <th align="center" valign="middle">
                        日期
                    </th>
                    <th align="center" valign="middle">
                        领队
                    </th>
                    <th align="center" valign="middle">
                        司机服务
                    </th>
                    <th align="center" valign="middle">
                        认路情况
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr i_zhijianid="<%#Eval("QCID") %>">
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%#Eval("TourCode")%>
                    </td>
                    <td align="center">
                        <%#Eval("CarTeamName")%>
                    </td>
                    <td align="center">
                        <%#Eval("CarCode")%>
                    </td>
                    <td align="center">
                       <%#Eval("QCTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <%#Eval("LeaderName")%>
                    </td>
                    <td align="center">
                        <%#Eval("DriverService")%>
                    </td>
                    <td align="center">
                        <%#Eval("FindWay")%>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="30">
                            暂无相关信息
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
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            insert: function() {
                var _data = { sl: "<%=SL %>" };
                Boxy.iframeDialog({ title: "新增车队质检", iframeUrl: "CheDuiZhiJianEdit.aspx", width: "680px", height: "450px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj[0]);
                var _data = { sl: "<%=SL %>", zhijianid: _$tr.attr("i_zhijianid") };
                Boxy.iframeDialog({ title: "修改车队质检", iframeUrl: "CheDuiZhiJianEdit.aspx", width: "680px", height: "450px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj);
                var _data = [];
                _$tr.each(function() { _data.push($(this).attr("i_zhijianid")); });

                $(".toolbar_delete").unbind("click");

                $.ajax({
                type: "GET", url: "CheDuiZhiJian.aspx?sl=<%=SL %>&doType=Delete&deleteids=" + _data.join(','), cache: false, dataType: "json", async: false,
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
