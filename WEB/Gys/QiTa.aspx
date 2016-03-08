<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiTa.aspx.cs" Inherits="EyouSoft.Web.Gys.QiTa"
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
                    国家：
                    <select id="txtCountryId" name="txtCountryId">
                    </select>
                    省份：
                    <select id="txtProvinceId" name="txtProvinceId">
                    </select>
                    城市：
                    <select id="txtCityId" name="txtCityId">
                    </select>
                    单位名称：
                    <input type="text" id="txtName" name="txtName" class="input-txt" maxlength="50" />
                    <button type="submit" class="search-btn">
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
                    <th rowspan="2" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th rowspan="2" align="center" valign="middle">
                        所在地
                    </th>
                    <th rowspan="2" align="center" valign="middle">
                        单位名称
                    </th>
                    <th rowspan="2" align="center" valign="middle">
                        联系人
                    </th>
                    <th colspan="4" align="center" valign="middle">
                        交易情况
                    </th>
                </tr>
                <tr>
                    <th height="30" align="center" valign="middle" class="th-line nojiacu">
                        交易次数
                    </th>
                    <th align="center" valign="middle" class="th-line nojiacu">
                        交易人数
                    </th>
                    <th align="center" valign="middle" class="th-line nojiacu">
                        结算金额
                    </th>
                    <th align="center" valign="middle" class="th-line nojiacu">
                        未付金额
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr i_gysid="<%#Eval("GysId") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("CPCD.ProvinceName") %>-<%#Eval("CPCD.CityName") %>
                            </td>
                            <td align="left">
                                <%#Eval("GysName") %>
                                <%#EyouSoft.Web.Gys.GY.GetTuiJianHtml(Eval("IsTuiJian"), Eval("IsQianDan"))%>
                            </td>
                            <td align="center">
                                <span style="display: none;">
                                    <%#EyouSoft.Web.Gys.GY.GetLxrHtml(Eval("Lxrs"))%></span> <a href="javascript:void(0)"
                                        class="i_fd_lxr">
                                        <%#Eval("LxrName") %></a>
                            </td>
                            <td align="center">
                                <%#Eval("JiaoYiXX.JiaoYiCiShu")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaoYiXX.JiaoYiShuLiang")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="i_jiaoyimingxi">
                                    <%# ToMoneyString(Eval("JiaoYiXX.JieSuanJinE"))%></a>
                            </td>
                            <td align="center">
                                <%#  ToMoneyString(Eval("JiaoYiXX.WeiZhiFuJinE"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="12">
                            暂无供应商信息
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
                Boxy.iframeDialog({ title: "新增供应商", iframeUrl: "qitaedit.aspx", width: "940px", height: "650px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj[0]);
                var _data = { sl: "<%=SL %>", gysid: _$tr.attr("i_gysid") };
                Boxy.iframeDialog({ title: "修改供应商", iframeUrl: "qitaedit.aspx", width: "940px", height: "650px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj);
                var _data = [];
                _$tr.each(function() { _data.push($(this).attr("i_gysid")); });

                $(".toolbar_delete").unbind("click");

                $.ajax({
                    type: "GET", url: "/ashx/handler.ashx?sl=<%=SL %>&doType=DeleteGys&deletegysids=" + _data.join(','), cache: false, dataType: "json", async: false,
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
            },
            jiaoYiMingXi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", gysid: _$tr.attr("i_gysid") };
                Boxy.iframeDialog({ title: "交易明细", iframeUrl: "jiaoyimingxi.aspx", width: "840px", height: "550px", data: _data });
                return false;
            }
        };

        $(document).ready(function() {
            pcToobar.init({ gID: "#txtCountryId", pID: "#txtProvinceId", cID: "#txtCityId", gSelect: '<%=Utils.GetQueryStringValue("txtCountryId") %>', pSelect: '<%=Utils.GetQueryStringValue("txtProvinceId") %>', cSelect: '<%=Utils.GetQueryStringValue("txtCityId") %>' });
            utilsUri.initSearch();
            iPage.initToolbar();
            $("#i_div_tool_paging_1").children().clone(true).prependTo("#i_div_tool_paging_2");
            $(".i_fd_lxr").bt({ contentSelector: function() { return $(this).prev("span").html(); }, positions: ['bottom'], fill: '#FFF2B5', strokeStyle: '#D59228', noShadowOpts: { strokeStyle: "#D59228" }, spikeLength: 5, spikeGirth: 15, width: 550, overlap: 0, centerPointY: 4, cornerRadius: 4, shadow: true, shadowColor: 'rgba(0,0,0,.5)', cssStyles: { color: '#00387E', 'line-height': '200%'} });
            $(".i_jiaoyimingxi").click(function() { iPage.jiaoYiMingXi(this); });
        });
    
    </script>

</asp:Content>
