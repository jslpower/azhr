<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingFu.aspx.cs" Inherits="EyouSoft.Web.Fin.YingFu" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" method="get">
        <div id="div_skipBtn" class="tablehead border-bot">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="YingFu.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform de-ztorderform"><span>应付账款</span></a></li>
                <li><s class="orderformicon"></s><a href="YingFuShenPi.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>付款审批</span></a></li>
                <li><s class="orderformicon"></s><a href="YingFu.aspx?sl=<%=Request.QueryString["sl"] %>&IsClean=1"
                    hidefocus="true" class="ztorderform"><span>已结清账款</span></a></li>
            </ul>
        </div>
        <div class="searchbox border-bot fixed">
            <span class="searchT">
                <p>
                    计调项目：
                    <select id="txt_sustainType" class="inputselect" name="sustainType">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanProject), new string[] { ((int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物).ToString() }), Request.QueryString["sustainType"] ?? "-1", true)%>
                    </select>
                    团号：
                    <input type="text" value="<%=Request.QueryString["TourCode"] %>" name="TourCode"
                        class="inputtext formsize140" />
                    单位名称：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" 
                        IsUniqueness="false" selectfrist="false" />
                         销售员：<uc1:SellsSelect ID="txt_Seller" runat="server" selectfrist="false" />
                    OP：<uc1:SellsSelect SetTitle="OP" ID="txt_Plan" runat="server" selectfrist="false" />
                    <br />
                    已付金额：
                    <uc1:CaiWuShaiXuan ID="Paid" runat="server" />
                    出团时间：
                    <input value="<%=Request.QueryString["SDate"] %>" name="SDate" type="text" onclick="WdatePicker()"
                        class="inputtext formsize80" />
                    -
                    <input type="text" value="<%=Request.QueryString["EDate"] %>" name="EDate" onclick="WdatePicker()"
                        class="inputtext formsize80" />
                    状态：
                    <select id="sel_tourType" class="inputselect" name="tourType">
                        <option value="-1">-请选择-</option>
                        <option value="1">已确认</option>
                        <option value="2">未确认</option>
                    </select>
                    <br />
                    未付金额：
                    <uc1:CaiWuShaiXuan ID="Unpaid" runat="server" />
                    付款时间：
                    <input value="<%=Request.QueryString["payDateS"] %>" name="payDateS" type="text"
                        class="inputtext formsize80" onclick="WdatePicker()" />
                    -
                    <input value="<%=Request.QueryString["payDateE"] %>" name="payDateE" type="text"
                        class="inputtext formsize80" onclick="WdatePicker()" />
                    <input type="submit" id="submit_Select" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="IsClean" value="<%=Request.QueryString["IsClean"] %>" />
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <asp:PlaceHolder ID="pan_quantityRegister" runat="server">
                    <li id="li_quantityRegister"><s class="dengji"></s><a href="javascript:void(0);"
                        hidefocus="true" class="toolbar_pldengji"><span>批量登记</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="duizhang"></s><a href="/Fin/DangRiFuKuan.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true"><span>当日付款对账</span></a></li>
                <li class="line"></li>
                <li><s class="dayin"></s><a id="a_print" href="javascript:void();" hidefocus="true"
                    class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" id="toolbar_daochu" hidefocus="true"
                    class="toolbar_daochu"><span>导出列表</span></a></li>
                    <li class="line"></li>
                <li id="li_piliangchengbenqueren"><s class="shenhe"></s><a href="javascript:void(0);" id="toolbar_piliangchengbenqueren" hidefocus="true"><span>批量成本确认</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
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
                        计调项
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="left" class="th-line">
                        单位名称
                    </th>
                    <th align="center" class="th-line">
                        数量
                    </th>
                    <th align="center" class="th-line">
                        出团时间
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        OP
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                    <th align="right" class="th-line">
                        应付金额
                    </th>
                    <th align="right" class="th-line">
                        已付金额
                    </th>
                    <th align="right" class="th-line" data-class="IsClean">
                        已登待付
                    </th>
                    <th align="right" class="th-line">
                        未付金额
                    </th>
                    <th align="center" class="th-line" data-class="IsClean">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-planid="<%#Eval("PlanId") %>" data-tourid="<%#Eval("TourId") %>" data-payable="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("Payable")!=null?Eval("Payable").ToString():"0") %>"
                            data-costremark="<%#Eval("CostRemarks") %>" i_chengbenquerenstatus="<%#(bool)Eval("IsConfirmed")?"1":"0"%>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("PlanItem")%>
                                <input type="hidden" name="hidePaymentType" value="<%#(int)Eval("PaymentType") %>" />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" data-class="a_popo">
                                    <%#Eval("TourCode")%></a><span style="display: none"><b><%#Eval("TourCode")%></b><br />
                                        线路名称：<%#Eval("RouteName")%></span>
                            </td>
                            <td align="left">
                                <%#Eval("Supplier")%>
                            </td>
                            <td align="center">
                                <%#Eval("Num")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Salesman")%>
                            </td>
                            <td align="center">
                                <%#Eval("Planer")%>
                            </td>
                            <td align="center">
                                <a data-class="<%#(bool)Eval("IsConfirmed")?"已确认":"a_Confirmed"%>" href="javascript:void(0);">
                                    <span class="<%#(bool)Eval("IsConfirmed")?"":"fontred" %>">
                                        <%#(bool)Eval("IsConfirmed")?"已确认":"未确认"%></span></a>
                            </td>
                            <td align="right">
                                <span class="<%#(bool)Eval("IsConfirmed")?"":"fontred" %>"><b>
                                    <%#  EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Payable"), ProviderToMoney)%></b></span>
                            </td>
                            <td align="right" data-paid="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("Paid")!=null?Eval("Paid").ToString():"")%>">
                                <b>
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Paid"), ProviderToMoney)%></b>
                            </td>
                            <td align="right" class="fontred" data-class="IsClean">
                                <b>
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("UnChecked"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <span class="<%#(bool)Eval("IsConfirmed")?"":"fontred" %>"><b>
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("Unpaid"), ProviderToMoney)%></b></span>
                            </td>
                            <td align="center" data-class="IsClean">
                                <a class="a_Register" href="javascript:void(0);" id="dengji">登记</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="14">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server" Visible="false">
                    <tr>
                        <td align="right" colspan="9">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b>
                                <asp:Label ID="lbl_TotalPayable" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b>
                                <asp:Label ID="lbl_TotalPaid" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right" class="fontred" data-class="IsClean">
                            <b>
                                <asp:Label ID="lbl_TotalUnchecked" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right" class="fontred">
                            <b>
                                <asp:Label ID="lbl_TotalUnpaid" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="center" data-class="IsClean">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone">
        </div>
    </div>
    <div class="alertbox-outbox" id="div_Confirmed" style="display: none; padding: 0 0 0 0;">
        <div style="margin: 0 auto; width: 99%; padding: 5px 2px 3px 2px">
            <table width="350px" style="height: 80px;" border="0" cellspacing="0" cellpadding="0">
                <tr style="height: 25px">
                    <td height="34" style="width: 70px" bgcolor="#b7e0f3">
                        <span class="alertboxTableT">计调项:</span>
                    </td>
                    <td bgcolor="#b7e0f3">
                        <span id="sp_PlanItem" class="alertboxTableT"></span>
                    </td>
                    <td style="width: 50px" bgcolor="#b7e0f3">
                        <span class="alertboxTableT">供应商:</span>
                    </td>
                    <td bgcolor="#b7e0f3">
                        <span id="sp_Supplier" class="alertboxTableT"></span>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td height="34" bgcolor="#b7e0f3">
                        <span class="alertboxTableT">结算金额:</span>
                    </td>
                    <td colspan="3">
                        <input type="text" disabled="disabled" class="inputtext formsize50" id="txt_Paid" />
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td height="34" bgcolor="#b7e0f3">
                        <span class="alertboxTableT">确认说明:</span>
                    </td>
                    <td colspan="3">
                        <input type="text" id="txt_Remark" class="inputtext formsize250" />
                    </td>
                </tr>
            </table>
            <div class="alertbox-btn" style="position: static">
                <a href="javascript:void(0);" hidefocus="true" id="a_PlanCostConfirmed"><s class="baochun">
                </s>确 认</a>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var Payable = {
            sl: '<%=Request.QueryString["sl"] %>',
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/Fin",
                    title: "应付管理",
                    width: "900px",
                    height: "350px"
                }
            },
            ShowQuantityRegister: function(planIDs, tourIDs) {/*批量登记*/
                var that = this;
                var data = that.DataBoxy();
                data.title += ("批量" + EnglishToChanges.Ping("Register"));
                data.url += "/YingFuBatchDeng.aspx?" + $.param({
                    sl: that.sl,
                    planIDs: planIDs,
                    tourIDs: tourIDs
                });
                that.ShowBoxy(data);
                return false;
            },
            ShowRegister: function(planID, tourID) {/*登记*/
                var that = this;
                var data = that.DataBoxy();
                data.title += EnglishToChanges.Ping("Register");
                data.url += "/YingFuDeng.aspx?" + $.param({
                    sl: that.sl,
                    planID: planID,
                    tourID: tourID
                });
                that.ShowBoxy(data);
                return false;
            },
            PlanCostConfirmed: {/*成本确认*/
                planId: null,
                costRemark: null,
                confirmation: null
            },
            InitPlanCostConfirmed: function(div_Confirmed, obj) {
                var that = this;
                that.PlanCostConfirmed.planId = obj.closest("tr").attr("data-planid");
                that.PlanCostConfirmed.confirmation = obj.closest("tr").attr("data-payable");
                that.PlanCostConfirmed.costRemark = obj.closest("tr").attr("data-costremark");
                div_Confirmed.find("#sp_PlanItem").html(obj.closest("tr").find("td:eq(1)").html());
                div_Confirmed.find("#sp_Supplier").html(obj.closest("tr").find("td:eq(3)").html());
                div_Confirmed.find("#txt_Paid").val(that.PlanCostConfirmed.confirmation);
                div_Confirmed.find("#txt_Remark").val(that.PlanCostConfirmed.costRemark);
            },
            UnInitPlanCostConfirmed: function(div_Confirmed) {
                div_Confirmed.find("#sp_PlanItem").html("");
                div_Confirmed.find("#sp_Supplier").html("");
                div_Confirmed.find("#txt_Paid").val("");
                $("a[data-class='a_close']").click(function() {
                    $("table[data-class='table_Boxy']").remove();
                })
            },
            BindBtn: function() {
                var that = this;
                var _otherbtns = [];
                //批量登记
                var _otherbtn1 = {
                    button_selector: '#li_quantityRegister', sucessRulr: 2, msg: '未选中任何信息 '
                    , buttonCallBack: function(arrTr) {
                        var planIDsArr = [], tourIDsArr = [];
                        $(arrTr).each(function(i) {
                            var obj = $(this);
                            if (obj.find("input[name='hidePaymentType']").val() == "<%=(int)EyouSoft.Model.EnumType.PlanStructure.Payment.现付%>") {
                                obj.find("input[name='checkbox']").removeAttr("checked");
                                return true;
                            }

                            planIDsArr.push(obj.attr("data-planid"));
                            tourIDsArr.push(obj.attr("data-tourid"));
                        });
                        if (planIDsArr.length == 0) { tableToolbar._showMsg("未选中任何信息"); return false; }
                        that.ShowQuantityRegister(planIDsArr.join(','), tourIDsArr.join(','));
                    }
                };
                //批量成本确认
                var _otherbtn2 = {
                    button_selector: '#li_piliangchengbenqueren', sucessRulr: 2, msg: '未选中任何需要成本确认的项'
                    , buttonCallBack: function(_trobjs) {
                        var txtQueRenIds = [];
                        $(_trobjs).each(function(i) {
                            var _$tr = $(this);
                            if (_$tr.attr("i_chengbenquerenstatus") == "0") txtQueRenIds.push(_$tr.attr("data-planid"));
                            else _$tr.find("input[name='checkbox']").removeAttr("checked");
                        });

                        if (txtQueRenIds.length == 0) {
                            top.tableToolbar._showMsg('未选中任何需要成本确认的项');
                            return false;
                        }

                        if (!confirm("成本确认操作不可逆，你确定要确认这些成本吗？")) return;

                        var _data = { txtQueRenIds: txtQueRenIds };
                        var _posturl = "YingFu.aspx?sl=<%=SL %>&doType=PiLiangChengBenQueRen";
                        $.newAjax({
                            url: _posturl, cache: false, dataType: "json", data: _data, type: "POST"
                            , success: function(response) {
                                if (response.result == "1") {
                                    top.tableToolbar._showMsg(response.msg);
                                    Payable.reload();
                                } else {
                                    top.tableToolbar._showMsg(response.msg);
                                }
                            }
                        });
                    }
                };

                _otherbtns.push(_otherbtn1);
                _otherbtns.push(_otherbtn2);

                tableToolbar.init({ tableContainerSelector: "#liststyle", objectName: "计调项", otherButtons: _otherbtns });

                $(".a_Register").click(function() {
                    var obj = $(this).closest("tr");
                    that.ShowRegister(obj.attr("data-planid"), obj.attr("data-tourid"));
                    return false;
                });
                //成本确认弹层
                $("a[data-class='a_Confirmed']").click(function() {
                    var div_Confirmed = $("#div_Confirmed");
                    var obj = $(this);
                    that.InitPlanCostConfirmed(div_Confirmed, obj)
                    new Boxy(div_Confirmed.clone(true), { title: "成本确认", modal: true });
                    that.UnInitPlanCostConfirmed(div_Confirmed);
                });
                //成本确认查看弹层
                $("a[data-class='已确认']").click(function() {
                    var div_Confirmed = $("#div_Confirmed");
                    var obj = $(this);
                    that.InitPlanCostConfirmed(div_Confirmed, obj)
                    div_Confirmed.find("#txt_Remark").attr("disabled", "disabled");
                    var clo = div_Confirmed.clone(true);
                    clo.find("#a_PlanCostConfirmed").remove();
                    new Boxy(clo, { title: "已确认成本查看", modal: true });
                    that.UnInitPlanCostConfirmed(div_Confirmed);
                    div_Confirmed.find("#txt_Remark").removeAttr("disabled");
                });
                //成本确认提交
                $("#a_PlanCostConfirmed").click(function() {
                    that.PlanCostConfirmed.costRemark = $(this).closest("#div_Confirmed").find("#txt_Remark").val()
                    $.newAjax({
                        type: "get",
                        data: that.PlanCostConfirmed,
                        cache: false,
                        url: "YingFu.aspx?" + $.param({ sl: that.sl, doType: "PlanCostConfirmed" }),
                        dataType: "json",
                        success: function(data) {
                            if (parseInt(data.result) === 1) {
                                parent.tableToolbar._showMsg('确认成功!', function() {
                                    location.href = location.href;
                                });
                            }
                            else {
                                parent.tableToolbar._showMsg(data.msg);
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg('服务器忙!');
                            that.BindBtn();
                        }
                    });
                });

                $("#toolbar_daochu").click(function() { toXls1(); return false; });
            },
            PageInit: function() {
                var that = this;
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                });
                $("a[data-class='a_popo']").bt({
                    contentSelector: function() {
                        return $(this).next("span");
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
                //绑定按钮
                that.BindBtn();
                $(".de-ztorderform").removeClass("de-ztorderform");
                if (!Boxy.queryString("IsClean")) {
                    $("#div_skipBtn a:eq(0)").addClass("de-ztorderform");
                }
                else {
                    $("#div_skipBtn a:eq(2)").addClass("de-ztorderform");
                    //已结清数据 清除  已登待付 和 操作列
                    $("#liststyle [data-class='IsClean']").remove();
                }
                //确定状态
                $("#sel_tourType").val('<%=EyouSoft.Common.Utils.GetIntSign(EyouSoft.Common.Utils.GetQueryStringValue("tourType"), -1) %>');
                //已付金额
                var paidwuc = new wuc.caiWuShaiXuan(window['<%=Paid.ClientUniqueID %>']);
                paidwuc.setOperator(Boxy.queryString("<%=Paid.ClientUniqueIDOperator %>"));
                paidwuc.setOperatorNumber(Boxy.queryString("<%=Paid.ClientUniqueIDOperatorNumber %>"));
                //未付金额
                paidwuc = new wuc.caiWuShaiXuan(window['<%=Unpaid.ClientUniqueID  %>']);
                paidwuc.setOperator(Boxy.queryString("<%=Unpaid.ClientUniqueIDOperator %>"));
                paidwuc.setOperatorNumber(Boxy.queryString("<%=Unpaid.ClientUniqueIDOperatorNumber %>"));
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
            , reload: function() {
                window.location.href = window.location.href;
                return false;
            }
        };

        $(function() {
            Payable.PageInit();
        });
    </script>
</asp:Content>
