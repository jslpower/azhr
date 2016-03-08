<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WuPin.aspx.cs" Inherits="EyouSoft.Web.Gys.WuPin"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    物品名称：<input type="text" class="inputtext formsize120" name="txtName" id="txtName"
                        maxlength="50" />
                    入库时间：<input type="text" name="txtSTime" id="txtSTime" class="inputtext formsize120"
                        onfocus="WdatePicker()" />-
                    <input type="text" name="txtETime" id="txtETime" class="inputtext formsize120" onfocus="WdatePicker()" />
                    <input type="hidden" name="sl" value='<%=SL %>' />
                    <button type="submit" id="btnSubmit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="phInsert">
                    <li><s class="addicon"></s><a href="javascript:void(0);" hidefocus="true" class="i_insert">
                        <span>入库</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phUpdate">
                    <li class="line"></li>
                    <li><s class="updateicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_update">
                        <span>修改</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phDelete">
                    <li class="line"></li>
                    <li><s class="delicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_delete">
                        <span>删除</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phLingYong">
                    <li class="line"></li>
                    <li><s class="lyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_lingyong">
                        <span>领用</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phFaFang">
                    <li class="line"></li>
                    <li><s class="fficon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_fafang">
                        <span>发放</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phJieYue">
                    <li class="line"></li>
                    <li><s class="jieyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_jieyue">
                        <span>借阅</span></a> </li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        物品名称
                    </th>
                    <th align="center" class="th-line">
                        物品数量
                    </th>
                    <th align="center" class="th-line">
                        入库时间
                    </th>
                    <th align="center" class="th-line">
                        领用数量
                    </th>
                    <th align="center" class="th-line">
                        发放数量
                    </th>
                    <th align="center" class="th-line">
                        借阅数量
                    </th>
                    <th align="center" class="th-line">
                        剩余数量
                    </th>
                    <th align="center" class="th-line">
                        登记人
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <tr i_wupinid="<%#Eval("WuPinId") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%#Eval("ShuLiangRK")%>
                            </td>
                            <td align="center">
                                <%#Eval("RuKuTime", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="i_linyong"><%#Eval("ShuLiang.LingYong")%></a>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="i_fafang">
                                    <%#Eval("ShuLiang.FaFang")%></a>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="i_jieyue">
                                    <%#Eval("ShuLiang.JieYue1")%></a>
                            </td>
                            <td align="center">
                                <%# Eval("ShuLiang.KuCun")%>
                            </td>
                            <td align="center">
                                <%#Eval("OperatorName")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                <td colspan="9">暂无相关信息</td>
                </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" id="i_div_tool_paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            insert: function() {
                var _data = { sl: "<%=SL %>" };
                Boxy.iframeDialog({ title: "物品入库", iframeUrl: "wupinedit.aspx", width: "600px", height: "270px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj[0]);
                var _data = { sl: "<%=SL %>", wupinid: _$tr.attr("i_wupinid") };
                Boxy.iframeDialog({ title: "物品修改", iframeUrl: "wupinedit.aspx", width: "600px", height: "270px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj);
                var _data = [];
                _$tr.each(function() { _data.push($(this).attr("i_wupinid")); });

                $(".toolbar_delete").unbind("click");

                $.ajax({
                    type: "GET", url: "WuPin.aspx?sl=<%=SL %>&doType=Delete&deleteids=" + _data.join(','), cache: false, dataType: "json", async: false,
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

                var _linYong = { button_selector: '.toolbar_lingyong'
                    , sucessRulr: 1
                    , msg: '请选择一个物品信息'
                    , buttonCallBack: function(objs) {
                        var _data = { sl: "<%=SL %>", wupinid: "", leixing: 0 };
                        var _$tr = $(objs[0]);
                        _data.wupinid = _$tr.attr("i_wupinid");
                        Boxy.iframeDialog({ title: "物品领用", iframeUrl: "wupinlingyong.aspx", width: "500px", height: "300px", data: _data, afterHide: function() { iPage.reload(); } });
                        return false;
                    }
                };

                var _faFang = { button_selector: '.toolbar_fafang'
                    , sucessRulr: 1
                    , msg: '请选择一个物品信息'
                    , buttonCallBack: function(objs) {
                        var _data = { sl: "<%=SL %>", wupinid: "", leixing: 1 };
                        var _$tr = $(objs[0]);
                        _data.wupinid = _$tr.attr("i_wupinid");
                        Boxy.iframeDialog({ title: "物品发放", iframeUrl: "wupinfafang.aspx", width: "500px", height: "300px", data: _data, afterHide: function() { iPage.reload(); } });
                        return false;
                    }
                };

                var _jieYue = { button_selector: '.toolbar_jieyue'
                    , sucessRulr: 1
                    , msg: '请选择一个物品信息'
                    , buttonCallBack: function(objs) {
                        var _data = { sl: "<%=SL %>", wupinid: "", leixing: 2 };
                        var _$tr = $(objs[0]);
                        _data.wupinid = _$tr.attr("i_wupinid");
                        Boxy.iframeDialog({ title: "物品借阅", iframeUrl: "wupinjieyue.aspx", width: "500px", height: "300px", data: _data, afterHide: function() { iPage.reload(); } });
                        return false;
                    }
                };

                _options.otherButtons.push(_linYong);
                _options.otherButtons.push(_faFang);
                _options.otherButtons.push(_jieYue);

                tableToolbar.init(_options);
            },
            lingYongXX: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", wupinid: _$tr.attr("i_wupinid") };
                Boxy.iframeDialog({ title: "领用信息", iframeUrl: "wupinlingyongxx.aspx", width: "800px", height: "500px", data: _data, afterHide: function() { /*iPage.reload();*/ } });
                return false;
            },
            faFangXX: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", wupinid: _$tr.attr("i_wupinid") };
                Boxy.iframeDialog({ title: "发放信息", iframeUrl: "wupinfafangxx.aspx", width: "800px", height: "500px", data: _data, afterHide: function() { /*iPage.reload();*/ } });
                return false;
            },
            jieYueXX: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", wupinid: _$tr.attr("i_wupinid") };
                Boxy.iframeDialog({ title: "借阅信息", iframeUrl: "wupinjieyuexx.aspx", width: "800px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            }
        };

        $(document).ready(function() {
            utilsUri.initSearch();
            iPage.initToolbar();
            $("#i_div_tool_paging_1").children().clone(true).prependTo("#i_div_tool_paging_2");
            $(".i_linyong").click(function() { iPage.lingYongXX(this); });
            $(".i_fafang").click(function() { iPage.faFangXX(this); });
            $(".i_jieyue").click(function() { iPage.jieYueXX(this); });
        });
     
    </script>

</asp:Content>
