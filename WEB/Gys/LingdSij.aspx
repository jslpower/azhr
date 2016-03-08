<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LingdSij.aspx.cs" Inherits="EyouSoft.Web.Gys.LingdSij"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    名称：
                    <input type="text" id="txtName" name="txtName" class="input-txt" maxlength="50" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
                        <input type="hidden" name="sl" value="<%=this.SL %>"/>
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
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" valign="middle">
                        姓名
                    </th>
                    <th align="center" valign="middle">
                        评价
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr i_gysid="<%#Eval("GysId") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="left">
                                <%#Eval("PingJiaNeiRong")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="12">
                            暂无信息
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
            insert: function() {
                var _data = { sl: "<%=SL %>" };
                Boxy.iframeDialog({ title: "新增", iframeUrl: "LingdSijEdit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj[0]);
                var _data = { sl: "<%=SL %>", gysid: _$tr.attr("i_gysid") };
                Boxy.iframeDialog({ title: "修改", iframeUrl: "LingdSijEdit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj);
                var _data = [];
                _$tr.each(function() { _data.push($(this).attr("i_gysid")); });

                $(".toolbar_delete").unbind("click");

                $.ajax({
                    type: "GET", url: "LingdSij.aspx?sl=<%=SL %>&doType=Delete&deleteids=" + _data.join(','), cache: false, dataType: "json", async: false,
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
                toXls.init({ "selector": "#i_a_toxls" });
            }
        };

        $(document).ready(function() {
            utilsUri.initSearch();
            iPage.initToolbar();
            $("#i_div_tool_paging_1").children().clone(true).prependTo("#i_div_tool_paging_2");
        });
    
    </script>

</asp:Content>
