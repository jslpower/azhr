<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYou.aspx.cs" Inherits="EyouSoft.Web.Guide.DaoYou"
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
                    导游姓名：<input type="text" class="formsize120 input-txt" name="txtXingMing" id="txtXingMing" />
                    性别：<select name="txtGender" id="txtGender">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender),new string[]{"2"}),"","","请选择") %>
                    </select>
                    类别：
                    <select name="txtLeiBie" id="txtLeiBie">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie)),"","","请选择") %>
                    </select>
                    级别：
                    <select name="txtJiBie" id="txtJiBie">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie)),"","","请选择") %>
                    </select>
                    语种：
                    <input name="txtYuZhong" type="text" class="formsize80 input-txt" id="txtYuZhong" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <%--<li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="i_insert">
                    <span>新增</span></a></li><li class="line"></li>--%>
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
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center">
                        姓名
                    </th>
                    <th align="center">
                        性别
                    </th>
                    <th align="center">
                        类别
                    </th>
                    <th align="center">
                        级别
                    </th>
                    <th align="center">
                        语种
                    </th>
                    <th align="center">
                        手机
                    </th>
                    <th align="center">
                        带团次数
                    </th>
                    <th align="center">
                        带团天数
                    </th>
                    <th align="left">
                        挂靠单位
                    </th>
                    <th align="left">
                        擅长线路
                    </th>
                    <th align="center">
                        年审状态
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr i_daoyouid="<%#Eval("DaoYouId") %>">
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%#Eval("XingMing")%>
                    </td>
                    <td align="center">
                        <%#Eval("Gender") %>
                    </td>
                    <td align="center">
                        <%#Eval("LeiBie")%>
                    </td>
                    <td align="center">
                        <%#Eval("JiBie") %>
                    </td>
                    <td align="center">
                        <%#Eval("YuZhong") %>
                    </td>
                    <td align="center">
                        <%#Eval("ShouJiHao") %>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="i_daituanxx"><%#Eval("DaiTuanCiShu")%></a>
                    </td>
                    <td align="center">
                        <%#Eval("DaiTuanTianShu")%>
                    </td>
                    <td align="left">
                        <%#Eval("GuaKaoDanWei") %>
                    </td>
                    <td align="left">
                        <span title="<%#Eval("ShanChangXianLu") %>"><%# EyouSoft.Common.Utils.GetText(Eval("ShanChangXianLu").ToString(),8,true)%></span>
                    </td>
                    <td align="center">
                        <%#(bool)Eval("IsNianShen")?"已审":"未审" %>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="30">
                            暂无导游信息
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
                Boxy.iframeDialog({ title: "新增导游", iframeUrl: "daoyouedit.aspx", width: "940px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj[0]);
                var _data = { sl: "<%=SL %>", daoyouid: _$tr.attr("i_daoyouid") };
                Boxy.iframeDialog({ title: "修改导游", iframeUrl: "daoyouedit.aspx", width: "940px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj);
                var _data = [];
                _$tr.each(function() { _data.push($(this).attr("i_daoyouid")); });

                $(".toolbar_delete").unbind("click");

                $.ajax({
                    type: "GET", url: "DaoYou.aspx?sl=<%=SL %>&doType=Delete&deleteids=" + _data.join(','), cache: false, dataType: "json", async: false,
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
            daiTuanXX: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", daoyouid: _$tr.attr("i_daoyouid") };
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
