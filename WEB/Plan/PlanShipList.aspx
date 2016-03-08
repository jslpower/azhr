<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanShipList.aspx.cs" Inherits="EyouSoft.Web.Plan.PlanShipList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/UserControl/SupplierControl.ascx" TagName="supplierControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <style type="text/css">
        /*样式重写*/body, html
        {
            overflow: visible;
            width: 100%;
        }
        .jidiao-r
        {
            border-right: 0px;
            border-top: 0px;
        }
    </style>
</head>
<body>
    <form id="largeform" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_5">
            <div>
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01" style="border-bottom: 1px solid #A9D7EC;
                    margin-bottom: 1px;">
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            大交通类型：
                        </th>
                        <td width="85%" colspan="3">
                            <label for="r1">
                                <input name="radioLartype" id="r1" type="radio" value="1" />
                                飞机</label>
                            <input name="radioLartype" id="r2" type="radio" value="2" />
                            <label for="r2">
                                火车</label>
                            <input name="radioLartype" id="r3" type="radio" value="3" />
                            <label for="r3">
                                汽车</label>
                            <input name="radioLartype" id="r4" type="radio" value="4" checked="checked" />
                            <label for="r4">
                                轮船</label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="g" style="display: block">
            <div action="divfortoggle">
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            <span class="fontred">*</span>出票点：
                        </th>
                        <td width="40%">
                            <uc1:suppliercontrol id="SupplierControl1" ismust="true" runat="server" callback="LargePage._AjaxContectInfo"
                                suppliertype="区间交通" />
                        </td>
                        <th width="15%" align="right">
                            联系人：
                        </th>
                        <td width="30%">
                            <asp:TextBox ID="txtContentName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            联系电话：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentPhone" runat="server" CssClass=" inputtext formsize140"></asp:TextBox>
                        </td>
                        <th align="right">
                            联系传真：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContentFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>安排信息：
                        </th>
                        <td colspan="3">
                            <table id="tabLargeFilght" width="100%" border="0" cellspacing="0" cellpadding="0"
                                class="noborder bottom_bot">
                                <asp:PlaceHolder ID="tabHolderView" runat="server">
                                    <tr class="tempRow">
                                        <td>
                                            出发日期<input type="text" name="txtstartTime" id="txtstartTime" class="inputtext" onfocus="WdatePicker();"
                                                valid="required" errmsg="*请选择出发日期!" data-title="出发日期" data-class="show" style="width: 65px" />
                                            时间<input type="text" class="inputtext formsize40" name="txtStartHours" data-title="时间"
                                                data-class="show" />
                                            出发地<input type="text" name="txtstartPlace" class="inputtext formsize40" data-title="出发地"
                                                data-class="show" />
                                            目的地<input type="text" class="inputtext formsize40" name="txtendPlace" data-title="目的地"
                                                data-class="show" />
                                            航班号<input type="text" class="inputtext formsize50" name="txtFilghtnumbers" data-title="航班号"
                                                data-class="show" />
                                            舱位类型<%=seleSpaceTypeHtml("")%>
                                            乘客类型<%=PassengerstypeHtml("") %>
                                            <br />人数<input type="text" class="inputtext formsize40" data-class="show" data-title="人数"
                                                name="txtpeopleNums" valid="required|RegInteger" errmsg="*请输入人数!|*人数必须是正整数!" />&nbsp;
                                            价格<input type="text" class="inputtext formsize40" data-class="show" data-title="单价"
                                                name="txtprices" valid="required|isMoney" errmsg="*请输入单价!|*单价有误!" />元
                                            保险<input type="text" class="inputtext formsize40" data-class="show" data-title="保险"
                                                name="txtBaoxian" valid="required|isMoney" errmsg="*请输入保险!|*保险有误!" />元&nbsp;
                                            机建费<input type="text" class="inputtext formsize40" data-class="show" data-title="机建费"
                                                name="txtJijian" valid="required|isMoney" errmsg="*请输入机建费!|*机建费有误!" />元&nbsp;
                                            附加费<input type="text" class="inputtext formsize40" data-class="show" data-title="附加费"
                                                name="txtFujia" valid="required|isMoney" errmsg="*请输入附加费!|*附加费有误!" />元&nbsp;
                                            折扣<input type="text" class="inputtext formsize40" data-class="show" data-title="折扣"
                                                name="txtZhekou" valid="required|IsDecimalTwo" errmsg="*请输入折扣!|*折扣有误!" />&nbsp;
                                            小计：<input type="text" class="inputtext formsize40" data-class="show" data-title="小计"
                                                name="txtXiaoJi" valid="required|isMoney" errmsg="*请输入小计费用!|*小计费有误!" />元
                                        </td>
                                        <td width="110" align="right">
                                            <a href="javascript:void(0)" class="addbtn">
                                                <img height="20" width="48" src="/images/addimg.gif" alt=""></a>&nbsp; <a href="javascript:void(0)"
                                                    class="delbtn">
                                                    <img height="20" width="48" src="/images/delimg.gif" alt=""></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:Repeater ID="repFilght" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td>
                                                出发时间<input type="text" name="txtstartTime" id="txtstartTime" class="inputtext" valid="required"
                                                    errmsg="*请输入出发日期!" value="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("DepartureTime"),ProviderToDate) %>"
                                                    onfocus="WdatePicker();" data-title="出发日期" data-class="show" style="width: 65px" />
                                                时间<input type="text" class="inputtext formsize40" name="txtStartHours" value="<%# Eval("Time") %>"
                                                    data-title="时间" data-class="show" />
                                                出发地<input type="text" name="txtstartPlace" class="inputtext formsize40" value="<%# Eval("Departure") %>"
                                                    data-title="出发地" data-class="show" />
                                                目的地<input type="text" class="inputtext formsize40" name="txtendPlace" value="<%# Eval("Destination") %>"
                                                    data-title="目的地" data-class="show" />
                                                航班号<input type="text" class="inputtext formsize50" name="txtFilghtnumbers" value="<%# Eval("Numbers") %>"
                                                    data-title="航班号" data-class="show" />
                                                舱位类型<%#seleSpaceTypeHtml(((int)Eval("SeatType")).ToString())%>
                                                乘客类型<%#PassengerstypeHtml(((int)Eval("AdultsType")).ToString())%>
                                                <br />人数<input type="text" class="inputtext formsize40" name="txtpeopleNums" valid="required|RegInteger"
                                                    errmsg="*请输入人数!|*人数必须是正整数!" min="1" data-class="show" data-title="人数" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("PepolePayNum"))) %>" />
                                                价格<input type="text" class="inputtext formsize40" name="txtprices" valid="required|isMoney"
                                                    errmsg="*请输入单价!|*单价有误!" data-class="show" data-title="单价" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("FarePrice"))) %>" />元&nbsp;
                                                保险<input type="text" class="inputtext formsize40" data-class="show" data-title="保险"
                                                    name="txtBaoxian" valid="required|isMoney" errmsg="*请输入保险!|*保险有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("InsuranceHandlFee"))) %>" />元&nbsp;
                                                机建费<input type="text" class="inputtext formsize40" data-class="show" data-title="机建费"
                                                    name="txtJijian" valid="required|isMoney" errmsg="*请输入机建费!|*机建费有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("Fee"))) %>" />元&nbsp;
                                                附加费<input type="text" class="inputtext formsize40" data-class="show" data-title="附加费"
                                                    name="txtFujia" valid="required|isMoney" errmsg="*请输入附加费!|*附加费有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("Surcharge"))) %>" />元&nbsp;
                                                折扣<input type="text" class="inputtext formsize40" data-class="show" data-title="折扣"
                                                    name="txtZhekou" valid="required|IsDecimalTwo" errmsg="*请输入折扣!|*折扣有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("Discount"))) %>" />&nbsp;
                                                小计：<input type="text" class="inputtext formsize40" name="txtXiaoJi" valid="required|isMoney"
                                                    errmsg="*请输入小计费用!|*小计费有误!" data-class="show" data-title="小计" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("SumPrice"))) %>" />
                                                元
                                            </td>
                                            <td align="right">
                                                <a href="javascript:void(0)" class="addbtn">
                                                    <img height="20" width="48" alt="" src="/images/addimg.gif"></a>&nbsp; <a href="javascript:void(0)"
                                                        class="delbtn">
                                                        <img height="20" alt="" width="48" src="/images/delimg.gif"></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>结算费用：
                        </th>
                        <td align="left">
                            <asp:TextBox ID="txtCostAccount" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                                errmsg="*请输入结算费用!|*结算费用输入有误！" isvalid="1"></asp:TextBox>
                            元
                        </td>
                        <th align="right">
                            <span class="addtableT"><span class="fontred">*</span> 支付方式：</span>
                        </th>
                        <td align="left">
                            <select class="inputselect" name="panyMent" id="isSigning">
                                <asp:Literal ID="litpanyMent" runat="server"></asp:Literal>
                            </select>
                            <span id="spanQDS" style="display: none;">签单数<asp:TextBox ID="txtSigningCount" Text="1" runat="server"
                                CssClass="inputtext formsize40"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            导游需知：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtGuidNotes" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800" style="height: 60px">
出发时间:
航班号:
出发地:
目的地:
请提前90分钟抵达码头
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtOtherMark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800" style="height: 60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>状态：
                        </th>
                        <td align="left" colspan="3">
                            <select class="inputselect" name="states">
                                <asp:Literal ID="litOperaterStatus" runat="server"></asp:Literal>
                            </select>
                        </td>
                    </tr>
                </table>
                    <div class="hr_5">
                    </div>
            </div>
                <asp:PlaceHolder ID="panView" runat="server">
                    <div class="mainbox cunline fixed" action="divfortoggle">
                        <ul id="ul_Btn_listAir">
                            <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phdShowList" runat="server">
                    <h2>
                        <p>
                            已安排机票</p>
                    </h2>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                        style="border-bottom: 1px solid #A9D7EC;" id="Largelist">
                        <tr>
                            <th align="" class="border-l">
                                出票点
                            </th>
                            <th align="center" class="addtableT">
                                出票数
                            </th>
                            <th align="right">
                                结算费用
                            </th>
                            <th align="center">
                                已付金额
                            </th>
                            <th align="center">
                                未付金额
                            </th>
                            <th align="center">
                                支付方式
                            </th>
                            <th align="center">
                                状态
                            </th>
                            <th align="center">
                                确认单
                            </th>
                            <th align="center">
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="repLargeList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="border-l">
                                        <%# Eval("SourceName")%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("Num")%>
                                    </td>
                                    <td align="right" class="red">
                                        <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%><a
                                            href="javascript:" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                            data-soucesname="<%# Eval("SourceName")%>">
                                            <img src="/images/yufu.gif" /></a>
                                    </td>
                                    <td align="center">
                                        <b class="fontred"><b class="fontred"><%#Eval("Prepaid", "{0:C2}")%></b>
                                    </td>
                                    <td align="center">
                                        <b class="fontblue"><%#(Convert.ToDecimal(Eval("Confirmation")) - Convert.ToDecimal(Eval("Prepaid"))).ToString("C2")%></b>
                                    </td>
                                    <td align="center">
                                        <%# Utils.GetEnumText(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment),Eval("PaymentType"))%>
                                    </td>
                                    <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <select onchange="parent.ConfigPage.ChangePlanStatus('<%#Eval("PlanId") %>',$(this).val());">
                                    <%#UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { ((int)EyouSoft.Model.EnumType.PlanStructure.PlanState.无计调任务).ToString(), ((int)EyouSoft.Model.EnumType.PlanStructure.PlanState.未安排).ToString() }), ((int)Eval("Status")).ToString(), false)%>
                                    </select>
                                    <%}
                                      else
                                      { %>
                                    <%#Eval("Status") %>
                                    <%} %>
                                    </td>
                                    <td align="center">
                                        <a href='<%# querenUrl %>?planId=<%#Eval("PlanId")%>&t=<%#(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船 %>' target="_blank">
                                            <img src="/images/y-kehuqueding.gif" /></a>
                                    </td>
                                    <td align="center">
                                        <%if (ListPower)
                                          { %>
                                        <a href="javascript:" data-class="updateLarge" class="untoggle">
                                            <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />修改</a>
                                        <a href="javascript:" data-class="deleteLarge">
                                            <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />删除</a>
                                        <%}
                                          else
                                          { %>
                                        <a href="javascript:" data-class="showLarge" class="untoggle">
                                            <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />查看</a>
                                        <%} %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var LargePage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _DeleteCar: function(objID) {
                var _Url = '/Plan/PlanShipList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + "&iframeId=" + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanShipList.aspx?sl=' + LargePage.sl + '&action=delete&PlanId=' + objID + '&tourid=' + LargePage.tourId,
                    cache: false,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _SaveCar: function(planId, tourId) {
                var _url = '/Plan/PlanShipList.aspx?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + '&iframeId=' + LargePage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/Plan/PlanShipList.aspx?sl=' + LargePage.sl + '&action=save&planId=' + planId + "&tourId=" + tourId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    async: false,
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                                    LargePage._SaveCar(_planId, LargePage.tourId);
                                });
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _TotalPrices: function() {
                //价格组成
                var total = 0;
                $("input[type=text][name='txtXiaoJi']").each(function() {
                    var trClass = $(this).closest("tr");
                    var peopleNums = trClass.find("input[type=text][name='txtpeopleNums']").val();
                    var prices = trClass.find("input[type=text][name='txtprices']").val();
                    var baoxian = trClass.find("input[type=text][name='txtBaoxian']").val();
                    var jijian = trClass.find("input[type=text][name='txtJijian']").val();
                    var fujia = trClass.find("input[type=text][name='txtFujia']").val();
                    //var zhekou = trClass.find("input[type=text][name='txtZhekou']").val();
                    var p = tableToolbar.calculate(fujia, tableToolbar.calculate(baoxian, jijian, "+"), "+");
                    var TotalPrices = tableToolbar.calculate(p, tableToolbar.calculate(peopleNums, prices, "*"), "+");
                    trClass.find("input[type=text][name='txtXiaoJi']").val(TotalPrices);
                    total = tableToolbar.calculate(total, TotalPrices, "+");
                });
                //结算费用
                $("#<%=txtCostAccount.ClientID %>").val(total);
            },
            _BIndBtn: function() {
                //修改
                $("#Largelist").find("[data-class='updateLarge']").unbind("click");
                $("#Largelist").find("[data-class='updateLarge']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    if (PlanId != "") {
                        window.location.href = '/Plan/PlanShipList.aspx?sl=' + LargePage.sl + '&action=update&PlanId=' + PlanId + '&tourId=' + LargePage.tourId + '&iframeId=' + LargePage.iframeId;
                    }
                    return false;
                });

                //查看
                $("#Largelist").find("[data-class='showLarge']").unbind("click");
                $("#Largelist").find("[data-class='showLarge']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    if (PlanId != "") {
                        window.location.href = '/Plan/PlanShipList.aspx?sl=' + LargePage.sl + '&action=update&PlanId=' + PlanId + '&tourId=' + LargePage.tourId + '&iframeId=' + LargePage.iframeId + "&show=1";
                    }
                    return false;
                });

                //删除
                $("#Largelist").find("[data-class='deleteLarge']").unbind("click");
                $("#Largelist").find("[data-class='deleteLarge']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            LargePage._DeleteCar(planId);
                        }
                    });

                    return false;
                });

                //失去焦点计算价格
                //                $("input[type='text'][name='txtpeopleNums'],input[type='text'][name='txtprices'],input[type='text'][name='txtBaoxian'],input[type='text'][name='txtJijian'],input[type='text'][name='txtFujia'],input[type='text'][name='txtXiaoJi']").blur(function() {
                //                    LargePage._TotalPrices();
                //                });
                $("input[type='text'][name='txtpeopleNums'],input[type='text'][name='txtprices'],input[type='text'][name='txtBaoxian'],input[type='text'][name='txtJijian'],input[type='text'][name='txtFujia'],input[type='text'][name='txtXiaoJi']").live("blur", function() {
                    LargePage._TotalPrices();
                });

                $("input[type='text'][name='txtpeopleNums'],input[type='text'][name='txtprices'],input[type='text'][name='txtBaoxian'],input[type='text'][name='txtJijian'],input[type='text'][name='txtFujia'],input[type='text'][name='txtXiaoJi']").live("change", function() {
                    LargePage._TotalPrices();
                });

                //预付申请
                $("#Largelist").find("a[data-class='Prepaid']").unbind("click");
                $("#Largelist").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    LargePage._OpenBoxy("预付申请", '/Plan/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + LargePage.type + '&sl=' + LargePage.sl + '&tourId=' + LargePage.tourId + '&souceName=' + escape(soucesName), "850px", "600px", true);
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=largeform.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                        LargePage._SaveCar(planId, LargePage.tourId);
                    }
                });

                var _url_par = '?sl=' + LargePage.sl + '&type=' + LargePage.type + '&tourId=' + LargePage.tourId + '&iframeId=' + LargePage.iframeId + "&m=" + new Date().getTime();
                //飞机
                $("#r1").click(function() {
                    window.location.href = '/Plan/PlanPlaneList.aspx' + _url_par;
                });
                //火车
                $("#r2").click(function() {
                    window.location.href = '/Plan/PlanTrainList.aspx' + _url_par;
                });
                //汽车
                $("#r3").click(function() {
                    window.location.href = '/Plan/PlanQiCheList.aspx' + _url_par;
                });
                //轮船
//                $("#r4").click(function() {
//                    window.location.href = '/Plan/PlanShipList.aspx' + _url_par;
//                });
            },

            _delCallBackTotalPrices: function() {
                parent.ConfigPage.SetWinHeight();
                LargePage._TotalPrices();
            },

            _InitPage: function() {
                //是否导游签单
                if ($("#isSigning").val() == '3') {
                    $("#spanQDS").css("display", "");
                }
                $("#isSigning").change(function() {
                    var val = $(this).val();
                    if (val == '3') {
                        $("#spanQDS").css("display", "");
                    } else {
                        $("#spanQDS").css("display", "none");
                    }
                });

                LargePage._BIndBtn();

                $("#tabLargeFilght").autoAdd({ addCallBack: function(tr) { parent.ConfigPage.SetWinHeight(); }, delCallBack: LargePage._delCallBackTotalPrices });

                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }
            },
            _AjaxContectInfo: function(obj) {
                $("#<%=SupplierControl1.ClientText %>").val(obj.name);
                $("#<%=SupplierControl1.ClientValue %>").val(obj.id);
                $("#<%=txtContentName.ClientID %>").val(obj.contactname);
                $("#<%=txtContentPhone.ClientID %>").val(obj.contacttel);
                $("#<%=txtContentFax.ClientID %>").val(obj.contactfax);
            }
        }
        $(function() {
            LargePage._InitPage();
            parent.ConfigPage.SetWinHeight();
        	$(".untoggle").click(function() {
        		parent.ConfigPage._toggle = false;
        	});
        	if (!parent.ConfigPage._toggle) {
        		parent.ConfigPage._ForUnToggle();
        	}
        	else {
        		parent.ConfigPage._ForToggle();
        	}
        });

    </script>

</body>
</html>
