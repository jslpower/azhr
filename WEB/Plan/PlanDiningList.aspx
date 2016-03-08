<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanDiningList.aspx.cs"
    Inherits="EyouSoft.Web.Plan.PlanDiningList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/UserControl/SupplierControl.ascx" TagName="SupplierControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

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
<form id="dform" runat="server">
<div class="jidiao-r">
    <div id="con_faq_4">
    <div action="divfortoggle">
        <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
            <tr>
                <th width="15%" align="right" class="border-l">
                    预定方式：
                </th>
                <td colspan="3">
                    <select class="inputselect" name="dueToway">
                        <asp:Literal id="litDueToway" runat="server"></asp:Literal>
                    </select>
                </td>
            </tr>
            <tr>
                <th width="15%" align="right" class="border-l">
                    <span class="fontred">*</span><span class="addtableT">餐厅名称</span>：
                </th>
                <td width="40%">
                    <uc1:SupplierControl id="SupplierControl1" runat="server" ismust="true" callback="ObjPage._AjaxContectInfo"
                        suppliertype="餐馆" />
                </td>
                <th width="15%" align="right">
                    <span class="addtableT">联系人：</span>
                </th>
                <td width="30%">
                    <asp:Textbox id="txtContectName" runat="server" cssclass="inputtext formsize140"></asp:Textbox>
                </td>
            </tr>
            <tr>
                <th align="right" class="border-l">
                    联系电话：
                </th>
                <td>
                    <asp:Textbox id="txtContectPhone" runat="server" cssclass="inputtext formsize140"></asp:Textbox>
                </td>
                <th align="right">
                    <span class="addtableT">联系传真：</span>
                </th>
                <td>
                    <asp:Textbox id="txtContectFax" runat="server" cssclass="inputtext formsize140"></asp:Textbox>
                </td>
            </tr>
            <tr>
                <th align="right" class="border-l">
                    <span class="fontred">*</span> 用餐时间：
                </th>
                <td colspan="3">
                    <asp:textbox id="txtStartTime" runat="server" cssclass="inputtext formsize80" valid="required"
                        errmsg="*请输入用餐时间!" onfocus="WdatePicker();"></asp:textbox>
                </td>
            </tr>
            <tr>
                <th align="right" class="border-l" rowspan="2">
                    <span class="fontred">*</span> <a id="a_openInfoDiv" href="javascript:void(0);">价格组成</a>：
                </th>
                <td colspan="3">
                    <asp:radiobutton id="r1" runat="server" groupname="radshipType" value="1" checked="true"
                        onclick="PriceItemType(1)" />
                    <label for="r1">
                        人</label>
                    <asp:radiobutton id="r2" runat="server" groupname="radshipType" value="2" onclick="PriceItemType(2)" />
                    <label for="r2">
                        桌</label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div id="divGroup" style="display: none;">
                        <table width="100%" border="0" id="tabPricesCG" cellspacing="0" cellpadding="0" class="noborder bottom_bot">
                            <asp:placeholder id="tabViewCG" runat="server">
                                    <tr class="tempRow">
                                        <td>
                                            菜单<select class="inputselect" name="selRoomTypeListR" valid="required" errmsg="*请选择菜单!">
                                                <option value=",">-请选择-</option> 
                                                <%=GetCarTypeList("")%>
                                            </select>
                                            成人数<input type="text" name="txtAdultNumberR" class="inputtext formsize50" valid="required|RegInteger"
                                                errmsg="*请输入成人数!|请填写正确的成人数!" />
                                            儿童数<input type="text" name="txtChildNumberR" class="inputtext formsize50" valid="required|RegInteger"
                                                errmsg="*请输入儿童数!|请填写正确的儿童数!" />
                                            领队数<input type="text" name="txtLeaderNumberR" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的领队数!" />
                                            导游数<input type="text" name="txtGuideNumberR" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的导游数!" />
                                            司机数<input type="text" name="txtDriverNumberR" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的司机数!" />
                                            <br />成人价<input type="text" name="txtAdultUnitPriceR" date-price="txtPrice" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入成人!|成人格式不正确!" />
                                            儿童价<input type="text" name="txtChildPriceR" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入儿童价!|儿童价格式不正确!" />
                                            时间<%=DiningTypeHtml("","R")%>
                                            桌数<input type="text" name="txtTableNumberR" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的桌数!" />
                                            减免人数<input type="text" name="txtFreeNumber" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的减免人数!" />
                                            减免金额<input type="text" name="txtFreePrice" class="inputtext formsize50" valid="isMoney"
                                                errmsg="减免金额式不正确!" />
                                            金额<input type="text" name="txtTotalPriceR" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入金额!|金额格式不正确!" />
                                        </td>
                                        <td width="110" align="right">
                                            <a href="javascript:void(0);" class="addbtn">
                                                <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                    class="delbtn">
                                                    <img src="/images/delimg.gif" width="48" height="20" /></a>
                                        </td>
                                    </tr>
                                </asp:placeholder>
                            <asp:repeater id="repPricesListCG" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td>
                                                菜单：<select class="inputselect" name="selRoomTypeListR" valid="required" errmsg="*请选择菜单!">
                                                    <option value=",">-请选择-</option> 
                                                    <%#GetCarTypeList(Eval("MenuId").ToString())%>
                                                </select>
                                                成人数<input type="text" name="txtAdultNumberR" value="<%#Eval("AdultNumber") %>" class="inputtext formsize50" valid="required|RegInteger"
                                                    errmsg="*请输入成人数!|请填写正确的成人数!" />
                                                儿童数<input type="text" name="txtChildNumberR" value="<%#Eval("ChildNumber") %>" class="inputtext formsize50" valid="required|RegInteger"
                                                    errmsg="*请输入儿童数!|请填写正确的儿童数!" />
                                                领队数<input type="text" name="txtLeaderNumberR" value="<%#Eval("LeaderNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的领队数!" />
                                                导游数<input type="text" name="txtGuideNumberR" value="<%#Eval("GuideNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的导游数!" />
                                                司机数<input type="text" name="txtDriverNumberR" value="<%#Eval("DriverNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的司机数!" />
                                                <br />成人价<input type="text" name="txtAdultUnitPriceR" date-price="txtPrice" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("AdultUnitPrice").ToString())) %>" class="inputtext formsize50" valid="required|isMoney"
                                                    errmsg="*请输入成人!|成人格式不正确!" />
                                                儿童价<input type="text" name="txtChildPriceR" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("ChildPrice").ToString())) %>" class="inputtext formsize50" valid="required|isMoney"
                                                    errmsg="*请输入儿童价!|儿童价格式不正确!" />
                                                时间<%#DiningTypeHtml(((int)Eval("DiningType")).ToString(),"R")%>
                                                桌数<input type="text" name="txtTableNumberR" value="<%#Eval("TableNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的桌数!" />
                                                减免人数<input type="text" name="txtFreeNumber" value="<%#Eval("FreeNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的减免人数!" />
                                                减免金额<input type="text" name="txtFreePrice" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("FreePrice").ToString())) %>" class="inputtext formsize50" valid="isMoney"
                                                    errmsg="减免金额式不正确!" />
                                                金额：
                                                <input type="text" name="txtTotalPriceR" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("SumPrice").ToString())) %>"
                                                    valid="required|isMoney" min="1" errmsg="*请输入金额!|金额格式不正确!" />
                                            </td>
                                            <td width="110" align="right">
                                                <a href="javascript:void(0);" class="addbtn">
                                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                        class="delbtn">
                                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:repeater>
                        </table>
                    </div>
                    <div id="divItems">
                        <table width="100%" border="0" cellspacing="0" id="tabPricesTS" cellpadding="0" class="noborder bottom_bot">
                            <asp:placeholder id="tabViewTS" runat="server">
                                    <tr class="tempRow">
                                        <td>
                                            菜单<select class="inputselect" name="selRoomTypeListZ" valid="required" errmsg="*请选择菜单!">
                                                <option value=",">-请选择-</option> 
                                                <%=GetCarTypeList("")%>
                                            </select>
                                            成人数<input type="text" name="txtAdultNumberZ" class="inputtext formsize50" valid="required|RegInteger"
                                                errmsg="*请输入成人数!|请填写正确的成人数!" />
                                            儿童数<input type="text" name="txtChildNumberZ" class="inputtext formsize50" valid="required|RegInteger"
                                                errmsg="*请输入儿童数!|请填写正确的儿童数!" />
                                            领队数<input type="text" name="txtLeaderNumberZ" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的领队数!" />
                                            导游数<input type="text" name="txtGuideNumberZ" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的导游数!" />
                                            司机数<input type="text" name="txtDriverNumberZ" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的司机数!" />
                                            时间<%=DiningTypeHtml("","Z")%>
                                            单价<input type="text" name="txtAdultUnitPriceZ" date-price="txtPrice" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入成人!|成人格式不正确!" />
                                            桌数<input type="text" name="txtTableNumberZ" class="inputtext formsize50" valid="RegInteger"
                                                errmsg="请填写正确的桌数!" />
                                            金额<input type="text" name="txtTotalPriceZ" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入金额!|金额格式不正确!" />
                                        </td>
                                        <td width="110" align="right">
                                            <a href="javascript:void(0)" class="addbtn">
                                                <img height="20" width="48" src="../images/addimg.gif"></a>&nbsp;<a href="javascript:void(0)"
                                                    class="delbtn">
                                                    <img height="20" width="48" src="../images/delimg.gif"></a>
                                        </td>
                                    </tr>
                                </asp:placeholder>
                            <asp:repeater id="repPricesListTS" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td>
                                                菜单：<select class="inputselect" name="selRoomTypeListZ" valid="required" errmsg="*请选择菜单!">
                                                    <option value=",">-请选择-</option> 
                                                    <%#GetCarTypeList(Eval("MenuId").ToString())%>
                                                </select>
                                                成人数<input type="text" name="txtAdultNumberZ" value="<%#Eval("AdultNumber") %>" class="inputtext formsize50" valid="required|RegInteger"
                                                    errmsg="*请输入成人数!|请填写正确的成人数!" />
                                                儿童数<input type="text" name="txtChildNumberZ" value="<%#Eval("ChildNumber") %>" class="inputtext formsize50" valid="required|RegInteger"
                                                    errmsg="*请输入儿童数!|请填写正确的儿童数!" />
                                                领队数<input type="text" name="txtLeaderNumberZ" value="<%#Eval("LeaderNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的领队数!" />
                                                导游数<input type="text" name="txtGuideNumberZ" value="<%#Eval("GuideNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的导游数!" />
                                                司机数<input type="text" name="txtDriverNumberZ" value="<%#Eval("DriverNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的司机数!" />
                                                时间<%#DiningTypeHtml(((int)Eval("DiningType")).ToString(), "Z")%>
                                                单价<input type="text" name="txtAdultUnitPriceZ" date-price="txtPrice" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("AdultUnitPrice").ToString())) %>" class="inputtext formsize50" valid="required|isMoney"
                                                    errmsg="*请输入成人!|成人格式不正确!" />
                                                桌数<input type="text" name="txtTableNumberZ" value="<%#Eval("TableNumber") %>" class="inputtext formsize50" valid="RegInteger"
                                                    errmsg="请填写正确的桌数!" />
                                                金额：
                                                <input type="text" name="txtTotalPriceZ" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("SumPrice").ToString())) %>"
                                                    valid="required|isMoney" min="1" errmsg="*请输入金额!|金额格式不正确!" />
                                            </td>
                                            <td width="110" align="right">
                                                <a href="javascript:void(0)" class="addbtn">
                                                    <img height="20" width="48" src="../images/addimg.gif"></a>&nbsp;<a href="javascript:void(0)"
                                                        class="delbtn">
                                                        <img height="20" width="48" src="../images/delimg.gif"></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:repeater>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <th align="right" class="border-l">
                    <span class="addtableT"><span class="fontred">*</span> </span>结算费用
                </th>
                <td align="left">
                    <asp:textbox id="txttotalMoney" runat="server" cssclass="inputtext formsize50" valid="required|isMoney"
                        errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:textbox>
                    元
                </td>
                <th align="right">
                    <span class="addtableT"><span class="fontred">*</span> 支付方式：</span>
                </th>
                <td align="left">
                    <select class="inputselect" name="panyMent" id="isSigning">
                        <asp:literal id="litpanyMent" runat="server"></asp:literal>
                    </select>
                    <span id="spanQDS" style="display: none;">签单数<asp:textbox id="txtSigningCount" Text="1" runat="server"
                        cssclass="inputtext formsize40"></asp:textbox>
                    </span>
                </td>
            </tr>
            <tr>
                <th align="right" class="border-l">
                    导游需知：
                </th>
                <td colspan="3">
                    <asp:textbox id="txtGuidNotes" runat="server" textmode="MultiLine" cssclass="inputtext formsize800" style="height: 60px">

                        </asp:textbox>
                </td>
            </tr>
            <tr>
                <th align="right" class="border-l">
                    备注：
                </th>
                <td colspan="3">
                    <asp:textbox id="txtOtherRemark" textmode="MultiLine" runat="server" cssclass="inputtext formsize800" style="height: 60px"></asp:textbox>
                </td>
            </tr>
            <tr>
                <th align="right" class="border-l">
                    <span class="addtableT"><span class="fontred">*</span> 状态：</span>
                </th>
                <td colspan="3">
                    <select class="inputselect" name="states">
                        <asp:literal id="litOperaterStatus" runat="server"></asp:literal>
                    </select>
                </td>
            </tr>
        </table>
        <div class="hr_5">
        </div>
    </div>
        <asp:placeholder id="panView" runat="server">
                <div class="mainbox cunline fixed" action="divfortoggle">
                    <ul id="ul_btn_list">
                        <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
                    </ul>
                </div>
            </asp:placeholder>
        <asp:placeholder id="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排用餐</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="tblcarlist">
                    <tr>
                        <th align="center">
                            餐厅名称
                        </th>
                        <th align="center">
                            用餐时间
                        </th>
                        <th align="center">
                            用餐明细
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
                    <asp:Repeater ID="repCarList" runat="server">
                        <ItemTemplate>
                            <tr style="background-color: <%#(EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status")==EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"#dadada":""%>">
                                <td align="center">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetDateString(Eval("StartDate"), "yyyy-MM-dd")%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetAPMX(Eval("PlanDiningList"), EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐.ToString())%>
                                </td>
                                <td align="right">
                                    <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                    <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>">
                                        <img src="/images/yufu.gif" /></a>
                                </td>
                                <td align="center">
                                    <b class="fontred">
                                        <%#Eval("Prepaid", "{0:C2}")%></b>
                                </td>
                                <td align="center">
                                    <b class="fontblue">
                                        <%#(Convert.ToDecimal(Eval("Confirmation")) - Convert.ToDecimal(Eval("Prepaid"))).ToString("C2")%></b>
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
                                    <a href='<%# querenUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" border="0" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:void(0);" data-class="updateCar" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" data-type="<%# ((int)Eval("PriceType")).ToString() %>"
                                            data-sid="<%# Eval("SourceId") %>" />
                                        修改</a> <a href="javascript:void(0);" data-class="deleteCar">
                                            <img src="/images/y-delicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />
                                            删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:void(0);" data-class="showCar" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" data-sid="<%# Eval("SourceId") %>"/>查看</a>
                                    <%} %>
                                </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:placeholder>
    </div>
    <div id="divShowList" class="floatbox" style="position: absolute;">
        <div class="tlist">
            <a id="a_closeDiv" class="closeimg" href="javascript:void(0);">关闭</a>
            <table width="99%" border="0" id="tblItemInfoList">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
</form>

<script type="text/javascript" src="/js/jquery.easydrag.handler.beta2.js"></script>

<script type="text/javascript">
    var ObjPage = {
        sl: '<%=SL %>',
        type: '<%=Utils.GetQueryStringValue("Type") %>',
        tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
        iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
        _tourMode: '<%=Utils.GetQueryStringValue("TourMode") %>',
        _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
            parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
        },
        _DeleteCar: function(objID) {
            var _Url = '/Plan/PlanDiningList.aspx?sl=' + ObjPage.sl + '&type=' + ObjPage.type + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + "&iframeId=" + ObjPage.iframeId + "&m=" + new Date().getTime();
            $.newAjax({
                type: "get",
                url: '/Plan/PlanDiningList.aspx?sl=' + ObjPage.sl + '&action=delete&PlanId=' + objID + '&TourMode=' + ObjPage._tourMode + '&tourid=' + ObjPage.tourId,
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
            var _url = '/Plan/PlanDiningList.aspx?sl=' + ObjPage.sl + '&type=' + ObjPage.type + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId + "&m=" + new Date().getTime();
            $.newAjax({
                type: "POST",
                url: '/Plan/PlanDiningList.aspx?sl=' + ObjPage.sl + '&action=save&planId=' + planId + "&TourMode=' + ObjPage._tourMode + '&tourId=" + tourId,
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
                                ObjPage._SaveCar(_planId, ObjPage.tourId);
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
            var tp = $('input:radio[name="radshipType"]:checked').val();
            if (tp == "1") {
                $("input[type=text][name='txtTotalPriceR']").each(function() {
                    var trClass = $(this).closest("tr");
                    var txtAdultNumber = trClass.find("input[type=text][name='txtAdultNumberR']").val();
                    var txtChildNumber = trClass.find("input[type=text][name='txtChildNumberR']").val();
                    var txtAdultUnitPrice = trClass.find("input[type=text][name='txtAdultUnitPriceR']").val();
                    var txtChildPrice = trClass.find("input[type=text][name='txtChildPriceR']").val();
                    var txtFreePrice = trClass.find("input[type=text][name='txtFreePrice']").val();
                    var TotalPrices = tableToolbar.calculate(tableToolbar.calculate(tableToolbar.calculate(txtAdultNumber, txtAdultUnitPrice, "*"), tableToolbar.calculate(txtChildNumber, txtChildPrice, "*"), "+"), txtFreePrice, "-");
                    trClass.find("input[type=text][name='txtTotalPriceR']").val(TotalPrices);
                    total = tableToolbar.calculate(total, TotalPrices, "+");
                });
            }
            if (tp == "2") {
                $("input[type=text][name='txtTotalPriceZ']").each(function() {
                    var trClass = $(this).closest("tr");
                    var txtTableNumber = trClass.find("input[type=text][name='txtTableNumberZ']").val();
                    var txtAdultUnitPrice = trClass.find("input[type=text][name='txtAdultUnitPriceZ']").val();
                    var TotalPrices = tableToolbar.calculate(txtTableNumber, txtAdultUnitPrice, "*");
                    trClass.find("input[type=text][name='txtTotalPriceZ']").val(TotalPrices);
                    total = tableToolbar.calculate(total, TotalPrices, "+");
                });
            }
            //结算费用
            $("#<%=txttotalMoney.ClientID %>").val(total);
        },
        _BIndBtn: function() {
            //修改
            $("#tblcarlist").find("[data-class='updateCar']").unbind("click");
            $("#tblcarlist").find("[data-class='updateCar']").click(function() {
                var PlanId = $(this).find("img").attr("data-Id");
                var PriceType = $(this).find("img").attr("data-type");
                var suppId = $(this).find("img").attr("data-sid");
                if (PlanId != "") {
                    window.location.href = '/Plan/PlanDiningList.aspx?sl=' + ObjPage.sl + '&action=update&PlanId=' + PlanId + '&PriceType=' + PriceType + '&suppId=' + suppId + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId;
                }
                return false;
            });

            //查看 
            $("#tblcarlist").find("[data-class='showCar']").unbind("click");
            $("#tblcarlist").find("[data-class='showCar']").click(function() {
                var PlanId = $(this).find("img").attr("data-Id");
                var suppId = $(this).find("img").attr("data-sid");
                if (PlanId != "") {
                    window.location.href = '/Plan/PlanDiningList.aspx?sl=' + ObjPage.sl + '&action=update&PlanId=' + PlanId + '&suppId=' + suppId + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId + "&show=1";
                }
                return false;
            });

            //删除
            $("#tblcarlist").find("[data-class='deleteCar']").unbind("click");
            $("#tblcarlist").find("[data-class='deleteCar']").click(function() {
                var newThis = $(this);
                parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                    var planId = newThis.find("img").attr("data-ID");
                    if (planId) {
                        ObjPage._DeleteCar(planId);
                    }
                });

                return false;
            });

            //失去焦点计算价格
            $("input[type='text'][name='txtAdultUnitPriceZ'],input[type='text'][name='txtTableNumberZ']").blur(function() {
                ObjPage._TotalPrices();
            });
            $("input[type='text'][name='txtAdultNumberR'],input[type='text'][name='txtChildNumberR'],input[type='text'][name='txtAdultUnitPriceR'],input[type='text'][name='txtChildPriceR'],input[type='text'][name='txtFreePrice']").blur(function() {
                ObjPage._TotalPrices();
            });

            //预付申请
            $("#tblcarlist").find("a[data-class='Prepaid']").unbind("click");
            $("#tblcarlist").find("a[data-class='Prepaid']").click(function() {
                var planId = $(this).attr("data-ID");
                var soucesName = $(this).attr("data-soucesname");
                ObjPage._OpenBoxy("预付申请", '/Plan/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + ObjPage.type + '&sl=' + ObjPage.sl + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&souceName=' + escape(soucesName), "850px", "600px", true);
                return false;
            });

            $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
            $("#btnSave").click(function() {
                var tp = $('input:radio[name="radshipType"]:checked').val();
                if (tp == "2") {
                    ObjPage._RemoveAttr("#divGroup");
                }
                if (tp == "1") {
                    ObjPage._RemoveAttr("#divItems");
                }
                if (!ValiDatorForm.validator($("#<%=dform.ClientID %>").get(0), "parent")) {
                    return false;
                } else {
                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                    var planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                    ObjPage._SaveCar(planId, ObjPage.tourId);
                }
            });
        },
        _RemoveAttr: function(div) {
            var box = $(div);
            box.find("input,select").each(function() {
                if ($(this).attr("valid")) {
                    $(this).attr("data-valid", $(this).attr("valid")).removeAttr("valid");
                }
            })
        },
        _AddAttr: function(div) {
            var box = $(b);
            box.find("input,select").each(function() {
                $(this).attr("valid", $(this).attr("data-valid"));
            })
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

            var PriceType = '<%=EyouSoft.Common.Utils.GetQueryStringValue("PriceType") %>';
            if (PriceType == "2") {
                $("#divGroup").hide();
                $("#divItems").fadeIn("slow");
                $("input[type='radio'][value='2']").attr("checked", 'checked');
            }
            else {
                $("#divGroup").fadeIn("slow");
                $("#divItems").hide();
                $("input[type='radio'][value='1']").attr("checked", 'checked');
            }

            var supid = $("#<%=SupplierControl1.ClientValue %>").val();
            if (supid != "") {
                ObjPage._DdlChangeEvent(supid);
            }

            ObjPage._BIndBtn();
            $("#tabPricesCG").autoAdd({ addCallBack: parent.ConfigPage.SetWinHeight, delCallBack: parent.ConfigPage.SetWinHeight });
            $("#tabPricesTS").autoAdd({ addCallBack: parent.ConfigPage.SetWinHeight, delCallBack: parent.ConfigPage.SetWinHeight });

            if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                $("#ul_btn_list").parent("div").hide();
            }
        },
        _AjaxReqModel: function(id) {
            if (id) {
                $.newAjax({
                    type: "POST",
                    url: "/Plan/PlanDiningList.aspx?suppId=" + id + "&action=getdata&sl=" + ObjPage.sl + "&TourMode=' + ObjPage._tourMode + '&tourId=" + ObjPage.tourId + "&m=" + new Date().getTime(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.tolist != "") {
                            var info = "";
                            if (ret.tolist.length > 0) {
                                for (var i = 0; i < ret.tolist.length; i++) {
                                    $("select[name='selRoomTypeListR']").append("<option value='" + ret.tolist[i].id + "," + ret.tolist[i].text + "'>" + ret.tolist[i].text + "</option>");
                                    $("select[name='selRoomTypeListZ']").append("<option value='" + ret.tolist[i].id + "," + ret.tolist[i].text + "'>" + ret.tolist[i].text + "</option>");
                                }
                            }
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            }
        },
        _AjaxContectInfo: function(obj) {
            var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 %>';
            var companyId = "<%=this.SiteUserInfo.CompanyId %>";
            if (obj) {
                $("#<%=SupplierControl1.ClientText %>").val(obj.name);
                $("#<%=SupplierControl1.ClientValue %>").val(obj.id);
                $("#<%=txtContectName.ClientID %>").val(obj.contactname);
                $("#<%=txtContectPhone.ClientID %>").val(obj.contacttel);
                $("#<%=txtContectFax.ClientID %>").val(obj.contactfax);

                $("select[name='selRoomTypeListR'] option").each(function() {
                    if ($(this).val() != "" && $(this).val() != ",") {
                        $(this).remove();
                    }
                });
                $("select[name='selRoomTypeListZ'] option").each(function() {
                    if ($(this).val() != "" && $(this).val() != ",") {
                        $(this).remove();
                    }
                });
                ObjPage._AjaxReqModel(obj.id);
                ObjPage._DdlChangeEvent(obj.id, obj.name);
            }
        },
        _DdlChangeEvent: function(id) {
            //下拉事件
            $("#tabPricesCG").find("select[name='selRoomTypeListR']").unbind("change");
            $("#tabPricesCG").find("select[name='selRoomTypeListR']").change(function() {
                var seleId = $(this).find("option:selected").val().split(',')[0];
                var seleName = $(this).find("option:selected").val().split(',')[1];
                if (seleId != "" && seleName != "") {
                    ObjPage._AjaxReqPrice($(this).closest("tr"), seleId, id, seleName, "ren");
                    ObjPage._TotalPrices();
                }
            });
            $("#tabPricesTS").find("select[name='selRoomTypeListZ']").unbind("change");
            $("#tabPricesTS").find("select[name='selRoomTypeListZ']").change(function() {
                var seleId = $(this).find("option:selected").val().split(',')[0];
                var seleName = $(this).find("option:selected").val().split(',')[1];
                if (seleId != "" && seleName != "") {
                    ObjPage._AjaxReqPrice($(this).closest("tr"), seleId, id, seleName, "zuo");
                    ObjPage._TotalPrices();
                }
            });
        },
        _AjaxReqPrice: function(tr, seleId, id, seleName, type) {
            $.newAjax({
                type: "POST",
                url: "/Plan/PlanDiningList.aspx?suppId=" + id + "&rid=" + seleId + "&action=getdata&sl=" + ObjPage._sl + "&type=" + type + "&tourId=" + ObjPage._tourID + "&m=" + new Date().getTime(),
                dataType: "json",
                success: function(ret) {
                    if (ret.tolist != "") {
                        if (ret.tolist.length > 0) {
                            var html = [], mTp = '<%=ProviderMoneyStr %>';
                            html.push('<tr><th width="40px">选择</th><th width="75px">菜单名称</th><th width="75px">菜单内容</th><th width="75px">结算价</th></tr>');
                            for (var i = 0; i < ret.tolist.length; i++) {
                                html.push('<tr><td><input type="radio" value="' + ret.tolist[i].js + '" name="radPrice" /></td><td>' + seleName + '</td><td>' + ret.tolist[i].con + '</td><td>' + ret.tolist[i].js0 + '</td></tr>')
                            }
                        }
                        //从供应商选用显示参考价格
                        if (id != "" && seleId != "") {
                            ObjPage.ShowListDiv(html.join(''));
                            $("#a_openInfoDiv").click(function() {
                                ObjPage.ShowListDiv("");
                            });
                        }
                        else {
                            $("#a_openInfoDiv").unbind("click");
                        }
                        $("input[type='radio'][name='radPrice']").click(function() {
                            $.trim(tr.find("input[type='text'][date-price='txtPrice']").val($(this).val()));
                            $("#divShowList").fadeOut("fast");
                            ObjPage._TotalPrices();
                        });
                    }
                    else {
                        tableToolbar._showMsg("无此景点价格信息！");
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg);
                    return false;
                }
            });
        },
        ShowListDiv: function(html) {
            if (html != "") {
                $("#tblItemInfoList").html(html);
                $("#divShowList").fadeIn("fast");
            } else {
                if ($("#tblItemInfoList").find("tr").length > 0) {
                    $("#divShowList").fadeIn("fast");
                }
            }

            $("#tblItemInfoList").find("tr").eq(0).attr("id", "i_tr_EasydragHandler");
            $("#divShowList").setHandler("i_tr_EasydragHandler"); //set easydrag handler
        }
    }
    $(function() {
    	ObjPage._InitPage();
    	parent.ConfigPage.SetWinHeight();

    	$("#a_closeDiv").click(function() { $("#divShowList").fadeOut("fast"); return false; });
    	$("#divShowList").easydrag();
    	$(".untoggle").click(function() {
    		parent.ConfigPage._toggle = false;
    	});
    	if (!parent.ConfigPage._toggle) {
    		parent.ConfigPage._ForUnToggle();
    	}
    	else {
    		parent.ConfigPage._ForToggle();
    	}

//        var PriceType = '<%=EyouSoft.Common.Utils.GetQueryStringValue("PriceType") %>';
//        if (PriceType == "2") {
//            $("#r2").click();
//        }
//        else {
//            $("#r1").click();
//        }
    });

    /*
    @用餐安排价格组成
    1 人
    2 桌
    */
    function PriceItemType(ItemType) {
        ObjPage._TotalPrices();
        switch (ItemType) {
            case 1:
                $("#divGroup").fadeIn("slow");
                $("#divItems").hide();
                break;
            case 2:
                $("#divGroup").hide();
                $("#divItems").fadeIn("slow");
                break;
        }
    }

</script>

</body>
</html>
