<%@ Page Title="入境报价" Language="C#" AutoEventWireup="true" CodeBehind="BaoJia.aspx.cs"
    Inherits="EyouSoft.Web.Quote.BaoJia" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox  border-bot fixed">
            <form method="get" action="/Quote/BaoJia.aspx">
            <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    <%--线路区域
                    <select class="inputselect" id="ddlArea" name="ddlArea">
                        <%=BindArea(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea"))%>
                    </select>--%>
                    团队名称：
                    <input type="text" name="txtRouteName" class="inputtext formsize120" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>" />
                    询价单位：
                    <uc1:CustomerUnitSelect runat="server" ID="CustomerUnitSelect1" SelectFrist="false"/>
                    对方业务员：
                    <input type="text" name="txtContact" class="inputtext formsize50" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtContact") %>" />
                    询价编号：
                    <input type="text" name="txtBuyId" class="inputtext formsize80" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBuyId") %>" />
                    <br>
                    询价时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtBeginBuyTime"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginBuyTime") %>">
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtEndBuyTime"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndBuyTime") %>">
                    成团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtBeginTourTime"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginTourTime") %>">
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtEndTourTime"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndTourTime") %>">
                    &nbsp;人数：
                    <input type="text" name="txtMinAdults" class="inputtext formsize40" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtMinAdults") %>" />
                    -
                    <input type="text" name="txtMaxAdults" class="inputtext formsize40" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtMaxAdults") %>" />
                    业务员：
                    <uc1:SellsSelect runat="server" ID="SellsSelect1" SelectFrist="false"/>
                    <br>
                    报价状态：
                    <select name="ddlQuoteStatus" name="ddlQuoteStatus">
                        <%=BindQuoteStatus(EyouSoft.Common.Utils.GetQueryStringValue("ddlQuoteStatus"))%>
                    </select>
                    <button class="search-btn" type="submit">
                        搜索</button>
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="i_div_tool_paging_1">
            <ul class="fixed">
                <asp:PlaceHolder ID="phForAdd" runat="server">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="BaoJiaEdit.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&act=add">
                        <span>新增</span></a> </li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForCopy" runat="server">
                    <li><s class="copyicon"></s><a class="toolbar_copy" hidefocus="true"><span>复制</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForDelete" runat="server">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                    </s><span>删除</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForDifference" runat="server">
                    <li><a class="toolbar_chybj" hidefocus="true" href="javascript:void(0)"><s class="chayibjicon">
                    </s><span>差异性比较</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="left" class="th-line">
                            团队名称
                        </th>
                        <th align="left" class="th-line">
                            有效期
                        </th>
                        <th align="center" class="th-line">
                            抵达城市
                        </th>
                        <th align="center" class="th-line">
                            离开城市
                        </th>
                        <th align="center" class="th-line">
                            询价单位
                        </th>
                        <th align="center" class="th-line">
                            价格
                        </th>
                        <th align="center" class="th-line">
                            天数
                        </th>
                        <th align="center" class="th-line">
                            人数
                        </th>
                        <th align="center" class="th-line">
                            咨询时间
                        </th>
                        <th align="center" class="th-line">
                            报价状态
                        </th>
                        <th align="center" class="th-line">
                            明细
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpQuote">
                        <ItemTemplate>
                            <tr data-count="<%#Eval("TimeCount") %>"
                                data-id="<%#Eval("QuoteId") %>" data-parentid='<%#Eval("ParentId")%>'>
                                <td align="center">
                                    <input name="checkbox" id="checkbox" type="checkbox">
                                </td>
                                <td align="left">
                                    <%#Eval("RouteName")%>
                                </td>
                                <td align="left">
                                    <%#this.ToDateTimeString(Eval("StartEffectTime")) %>至<%#this.ToDateTimeString(Eval("EndEffectTime")) %>
                                </td>
                                <td align="center">
                                    <%#Eval("ArriveCity")%>
                                </td>
                                <td align="center">
                                    <%#Eval("LeaveCity")%>
                                </td>
                                <td align="center">
                                    <a class="a_company_info" href="#" bt-xtitle="" title="">
                                        <%#Eval("BuyCompanyName")%></a> <span style="display: none;"><b>
                                            <%#Eval("BuyCompanyName")%></b><br />
                                            联系人：<%#Eval("Contact")%><br />
                                            联系方式：<%#Eval("Phone")%><br />
                                            联系传真：<%#Eval("Fax")%></span>
                                </td>
                                <td align="right">
                                    <%#GetPrice(Eval("QuotePriceList"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Days") %>
                                </td>
                                <td align="center">
                                    <b>
                                        <%#Eval("MinAdults")%>-
                                        <%#Eval("MaxAdults")%></b>
                                </td>
                                <td align="center">
                                    <%#ToDateTimeString( Eval("BuyTime")) %>
                                </td>
                                <td align="center">
                                    <%#GetHtmlByState(Eval("TimeCount").ToString(), (EyouSoft.Model.EnumType.TourStructure.QuoteState)(int)Eval("QuoteStatus"), Eval("CancelReason").ToString())%>
                                </td>
                                <td align="center">
                                    <a href="BaoJiaEdit.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&id=<%#Eval("QuoteId") %>&act=update"
                                        class="check-btn" title="查看"></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server" Text="<tr><td align='center' colspan='12'>暂无报价信息!</td></tr>"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead border-bot" id="i_div_tool_paging_2">
        </div>
</div>
        <script type="text/javascript">
            var BaoJia = {
                sl: '<%=Request.QueryString["sl"] %>',
                //使用通用按钮下获取数据并执行异步删除
                //删除(批量)
                DelAll: function(objArr) {

                    var list = new Array();
                    var msgList = new Array();
                    var state = "";
                    //遍历按钮返回数组对象
                    for (var i = 0; i < objArr.length; i++) {
                        state = objArr[i].find("span[data-class='QuoteState']").attr("data-state");
                        if (state == "<%=(int)EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功 %>") {
                            msgList.push("当前选中项中第" + (i + 1) + "行已成功报价,无法删除!");
                        }
                        else {
                            list.push(objArr[i].attr("data-id"));
                        }
                    }
                    // alert(list.join(","));


                    if (msgList.length > 0) {
                        tableToolbar._showMsg(msgList.join("<br />"));
                        return false;
                    }
                    //执行
                    $.newAjax({
                        type: "get",
                        cache: false,
                        url: "/Quote/BaoJia.aspx?dotype=delete&ids=" + list.join(',') + "&sl=" + BaoJia.sl,
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                tableToolbar._showMsg(ret.msg, function() {
                                    window.location.href = window.location.href;
                                });
                            } else {
                                tableToolbar._showMsg(ret.msg);
                            }
                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    });

                },
                PageInit: function() {

                    tableToolbar.init({
                    		tableContainerSelector: "#liststyle", //表格选择器
                        objectName: "报价",
                        deleteCallBack: function(objsArr) {
                            //删除(批量)
                            BaoJia.DelAll(objsArr);
                        },
                        copyCallBack: function(objsArr) {
                            location.href = "BaoJiaEdit.aspx?&sl=" + BaoJia.sl + "&id=" + objsArr[0].attr("data-id") + "&act=copy";
                        }, otherButtons: [{
                            button_selector: '.toolbar_chybj',
                            sucessRulr: 2,
                            msg: '未选中任何 报价 ',
                            msg2: '只能选择一报价 ',
                            buttonCallBack: function(arr) {
                                if (arr.length == 1) {
                                    var parentId = arr[0].attr("data-parentid");
                                   // alert(parentId);
                                   // alert(BaoJia.sl);
                                    var Count = arr[0].attr("data-count");
                                    if (Count > 1) {
                                        Boxy.iframeDialog({ iframeUrl: "/CommonPage/QuoteChaYi.aspx?sl=" + BaoJia.sl + "&ParentId=" + parentId, title: "差异比较", modal: true, width: "300px", height: "100px" });
                                        return false;
                                    }
                                    else {
                                        tableToolbar._showMsg("只存在一次报价 无差异比较");
                                    }
                                } else if (arr.length == 2) {
                                    var ids = new Array();

                                    ids[0] = arr[0].attr("data-parentid");
                                    //如果第一次就报价成功了取报价编号
                                    if (arr[0].attr("data-parentid") == 0) {
                                        ids[0] = arr[0].attr("data-id");
                                    }

                                    ids[1] = arr[1].attr("data-parentid");
                                    if (arr[1].attr("data-parentid") == 0) {
                                        ids[1] = arr[1].attr("data-id");
                                    }


                                 //   alert(ids.join(","));

                                    Boxy.iframeDialog({ iframeUrl: "/CommonPage/QuoteChaYi.aspx?sl=" + BaoJia.sl + "&ParentId=" + ids.join(','), title: "差异比较", modal: true, width: "300px", height: "100px" });
                                    return false;


                                } else {
                                    tableToolbar._showMsg("最多选择二次不同报价的数据进行比较");

                                }

                                return false;


                            } }]
                        });

                    }
                }

                $(function() {

                    BaoJia.PageInit();

                    //操作——分页
                    $("#i_div_tool_paging_1").children().clone(true).prependTo("#i_div_tool_paging_2");

                    //询价单位
                    $(".a_company_info").bt({
                        contentSelector: function() {
                            return $(this).next().html();
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 200,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });

                    //价格
                    $(".a_price_info").bt({
                        contentSelector: function() {
                            return $(this).next().html();
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 200,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });

                    //报价取消原因
                    $("#liststyle").find("a[data-class='cancelReason']").each(function() {
                        if ($.trim($(this).next().html()) != "") {
                            $(this).bt({
                                contentSelector: function() {
                                    return $(this).next().html();
                                },
                                positions: ['left', 'right', 'bottom'],
                                fill: '#FFF2B5',
                                strokeStyle: '#D59228',
                                noShadowOpts: { strokeStyle: "#D59228" },
                                spikeLength: 10,
                                spikeGirth: 15,
                                width: 200,
                                overlap: 0,
                                centerPointY: 1,
                                cornerRadius: 4,
                                shadow: true,
                                shadowColor: 'rgba(0,0,0,.5)',
                                cssStyles: { color: '#00387E', 'line-height': '180%' }
                            });
                        }
                    })

                });
        </script>
</asp:Content>
