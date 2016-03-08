<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingFuShenPi.aspx.cs" Inherits="EyouSoft.Web.Fin.YingFuShenPi" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" method="get">
        <div class="tablehead border-bot">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="YingFu.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>应付账款</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:void(0);" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>付款审批</span></a></li>
                <li><s class="orderformicon"></s><a href="YingFu.aspx?sl=<%=Request.QueryString["sl"] %>&IsClean=1"
                    hidefocus="true" class="ztorderform"><span>已结清账款</span></a></li>
            </ul>
        </div>
        <div class="searchbox border-bot fixed">
            <span class="searchT">
                <p>
                    计调项目：
                    <select id="txt_sustainType" class="inputselect" name="sustainType">
                        <option>-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanProject), new string[] { ((int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物).ToString() }), Request.QueryString["sustainType"] ?? "-1")%>
                    </select>
                    团号：
                    <input type="text" name="txt_teamNumber" value="<%= Request.QueryString["txt_teamNumber"]%>"
                        class="inputtext formsize140" />
                    供应商单位：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" IsUniqueness="false"  selectfrist="false"/>
                    请款人：
                    <uc1:SellsSelect ID="SellsSelect1" runat="server"  selectfrist="false"/>
                    付款日期：
                    <input type="text" name="txt_paymentDateS" onclick="WdatePicker()" class="inputtext formsize80"
                        value="<%=Request.QueryString["txt_paymentDateS"] %>" />
                    -
                    <input type="text" name="txt_paymentDateE" onclick="WdatePicker()" class="inputtext formsize80"
                        value="<%=Request.QueryString["txt_paymentDateE"] %>" />
                    <br />
                    审批状态：
                    <select name="Status" class="inputselect" id="sel_Status">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.FinStructure.FinStatus)), Request.QueryString["Status"] ?? "-1", true)%>
                    </select>
                    最晚付款日期：
                    <input type="text" name="txt_DeadlineS" onclick="WdatePicker()" value="<%=Request.QueryString["txt_DeadlineS"] %>"
                        class="inputtext formsize80" />
                    -
                    <input type="text" name="txt_DeadlineE" onclick="WdatePicker()" value="<%=Request.QueryString["txt_DeadlineE"] %>"
                        class="inputtext formsize80" />
                    <input type="submit" id="submit_Select" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <asp:PlaceHolder ID="pan_quantityExamineA" runat="server">
                    <li id="li_quantityExamineA"><s class="dengji"></s><a href="javascript:void(0);"
                        hidefocus="true" class="toolbar_plshenpi"><span>批量审批</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_quantityPayMoney" runat="server">
                    <li id="li_quantityPayMoney"><s class="duizhang"></s><a href="javascript:void(0);"
                        hidefocus="true" class="toolbar_plzhifu"><span>批量支付</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="dayin"></s><a id="a_print" href="javascript:void();" hidefocus="true"
                    class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" id="toolbar_daochu" hidefocus="true"
                    class="toolbar_daochu"><span>导出列表</span></a></li>
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
                        <input type="checkbox" name="checkbox" id="checkbox1" />
                    </th>
                    <th align="center" class="th-line">
                        计调项
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="center" class="th-line">
                        出团时间
                    </th>
                    <th align="center" class="th-line">
                        供应商单位
                    </th>
                    <th align="center" class="th-line">
                        请款人
                    </th>
                    <th align="right" class="th-line">
                        付款金额
                    </th>
                    <th align="center" class="th-line">
                        最晚付款时间
                    </th>
                    <th align="center" class="th-line" >
                        备注
                    </th>
                    <th align="center" class="th-line" style="width:80px">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-registerid="<%#Eval("RegisterId") %>" data-status="<%#(int)Eval("Status") %>"
                            data-tourid="<%#Eval("TourId") %>" data_IsDaoYouXianFu="<%#(bool)Eval("IsDaoYouXianFu")?"1":"0" %>" >
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("PlanTyp")%>
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Supplier")%>
                            </td>
                            <td align="center">
                                <%#Eval("Dealer")%>
                            </td>
                            <td align="right" class="fontred">
                                <span class="PayAmount">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PayAmount"), ProviderToMoney)%></span>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("PayExpire"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Remark")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" data-class="a_examineA">审批</a> <a href="javascript:void(0);"
                                    data-class="a_payMoney">支付</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="10">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server" Visible="false">
                    <tr>
                        <td colspan="6" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <asp:Label ID="lbl_sum" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td colspan="3" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var ExamineAListObj = {
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
                    width: "950px",
                    height: "350px"
                }
            },
            ExamineA: function(registerIds) {/*审批,批量审批*/
                var that = this;
                var data = that.DataBoxy();
                data.title += ((registerIds.valueOf(",") > 0 ? "批量" : " ") + EnglishToChanges.Ping("ExamineA"));
                data.url += "/YingFuBatchShen.aspx?" + $.param({ sl: that.sl, registerIds: registerIds });
                that.ShowBoxy(data);
                return false;
            },
            PayMoney: function(registerIds, isInAccount, tourID) {/*支付,批量支付*/
                var that = this;
                var data = that.DataBoxy();
                data.title += ((registerIds.valueOf(",") > 0 ? "批量" : " ") + EnglishToChanges.Ping("Pay"));
                data.url += "/YingFuBatchFu.aspx?" + $.param({ sl: that.sl, registerIds: registerIds, isInAccount: isInAccount || "", tourID: tourID || "" });
                that.ShowBoxy(data);
                return false;
            },
            BindBtn: function() {
                var that = this;
                var registeridArr = [], errRegisteridArr = [];
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "计划",
                    otherButtons: [
                    {/*批量审批*/
                        button_selector: '#li_quantityExamineA', //按钮选择器
                        sucessRulr: 2, //验证选择记录---1表示单选，2表示多选
                        msg: '未选中任何 计划 ', //验证未通过提示文本
                        buttonCallBack: function(arrTr) {//验证通过执行函数
                            $(arrTr).each(function() {
                                var obj = $(this);
                                var intstatus = parseInt(obj.attr("data-status"))
                                if (intstatus == 0) {
                                    registeridArr.push(obj.attr("data-registerid"));
                                }
                                else {
                                    errRegisteridArr.push(obj.attr("data-registerid"));
                                }
                            })

                            if (errRegisteridArr.length == arrTr.length) {
                                parent.tableToolbar._showMsg('无可审批记录!')
                            }
                            else {
                                that.ExamineA(registeridArr.join(','));
                            }
                            registeridArr = [];
                            errRegisteridArr = [];
                            return false;
                        }
                    },
                    {/*批量支付*/
                        button_selector: '#li_quantityPayMoney', //按钮选择器
                        sucessRulr: 2, //验证选择记录---1表示单选，2表示多选
                        msg: '未选中任何 计划 ', //验证未通过提示文本
                        buttonCallBack: function(arrTr) {//验证通过执行函数
                            $(arrTr).each(function() {
                                var obj = $(this);
                                var intstatus = parseInt(obj.attr("data-status"))
                                if (intstatus == 1) {
                                    registeridArr.push(obj.attr("data-registerid"));
                                }
                                else {
                                    errRegisteridArr.push(obj.attr("data-registerid"));
                                }
                            })

                            if (errRegisteridArr.length == arrTr.length) {
                                parent.tableToolbar._showMsg('无可支付记录!')
                            }
                            else {
                                that.PayMoney(registeridArr.join(','));
                            }
                            registeridArr = [];
                            errRegisteridArr = [];
                            return false;
                        }
                    }
                    ]
                })
                //审批
                $("#liststyle a[data-class='a_examineA']").click(function() {
                    that.ExamineA($(this).closest("tr").attr("data-registerid"));
                    return false;
                })
                //支付
                $("#liststyle a[data-class='a_payMoney']").click(function() {
                    that.PayMoney($(this).closest("tr").attr("data-registerid"));
                    return false;
                })
                //财务入账
                //                $("#liststyle a[data-class='a_InMoney']").click(function() {
                //                    var tr = $(this).closest("tr");
                //                    that.PayMoney(tr.attr("data-registerid"), 1, tr.attr("data-tourid"));
                //                    return false;
                //                })
                $("#toolbar_daochu").click(function() {
                    toXls1();
                    return false;
                })
            },
            PageInit: function() {
                var that = this;
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                })
                $("#sel_Status option[value='<%=(int)EyouSoft.Model.EnumType.FinStructure.FinStatus.销售待确认 %>']").remove();

                if ('<%=IsEnableKis %>' == 'False') {
                    $("a[data-class='a_InMoney']").html("查看");
                }
                //确定状态
                $("#sel_Status").val('<%=EyouSoft.Common.Utils.GetIntSign(EyouSoft.Common.Utils.GetQueryStringValue("Status"), -1) %>');
                $("#liststyle tr[data-status]").each(function() {
                    var obj = $(this);
                    var intstatus = parseInt(obj.attr("data-status"))
                    switch (intstatus) {
                        case 0: /*财务待审批,移除支付,财务入账功能*/
                            obj.find("a[data-class='a_payMoney']").remove();
                            obj.find("a[data-class='a_InMoney']").remove();
                            break;
                        case 1: /*财务待支付,移除审批,财务入账功能*/
                            obj.find("a[data-class='a_examineA']").remove();
                            obj.find("a[data-class='a_InMoney']").remove();
                            break;
                        default: /*其他状态,全部移除*/
                            obj.find("a[data-class='a_payMoney'],a[data-class='a_examineA']").remove();
                            break;
                    }
                });

                $("#liststyle tr[data_IsDaoYouXianFu]").each(function() {
                    var _trobj = $(this);
                    var isDaoYouXianFu = _trobj.attr("data_IsDaoYouXianFu");
                    if (isDaoYouXianFu == "1") {
                        _trobj.find("a[data-class='a_InMoney']").html("查看").click(function() {
                            var tr = $(this).closest("tr");
                            that.PayMoney(tr.attr("data-registerid"), "chakan", tr.attr("data-tourid"));
                            return false;
                        });
                    } else {
                        _trobj.find("a[data-class='a_InMoney']").click(function() {
                            var tr = $(this).closest("tr");
                            that.PayMoney(tr.attr("data-registerid"), 1, tr.attr("data-tourid"));
                            return false;
                        });
                    }
                });

                that.BindBtn();
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            ExamineAListObj.PageInit();

        })
       
    </script>

</asp:Content>
