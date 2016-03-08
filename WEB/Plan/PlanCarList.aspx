<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanCarList.aspx.cs" Inherits="EyouSoft.Web.Plan.PlanCarList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/UserControl/SupplierControl.ascx" TagName="supplierControl" TagPrefix="uc1" %>
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
<body style="background-color: #fff">
    <form id="formCar" runat="server">
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
                            <asp:Literal ID="litDueToway" runat="server"></asp:Literal>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span><span class="addtableT">车队</span>：
                    </th>
                    <td width="40%">
                        <uc1:suppliercontrol id="SupplierControl1" runat="server" ismust="true" callback="CarPage._AjaxContectInfo"
                            suppliertype="车队" />
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT">联系人：</span>
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtContectName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        联系电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContectPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="addtableT">联系传真：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtContectFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l" rowspan="2">
                        <span class="fontred">*</span> <a id="a_openInfoDiv" href="javascript:void(0);">价格组成</a>：
                    </th>
                    <td colspan="3">
                        <asp:RadioButton ID="r1" runat="server" GroupName="radshipType" value="1" Checked="true"
                            onclick="PriceItemType(1)" />
                        <label for="r1">
                            常规线路</label>
                        <asp:RadioButton ID="r2" runat="server" GroupName="radshipType" value="2" onclick="PriceItemType(2)" />
                        <label for="r2">
                            特殊线路</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div id="divGroup" style="display: none;">
                            <table width="100%" border="0" id="tabPricesCG" cellspacing="0" cellpadding="0" class="noborder bottom_bot">
                                <asp:PlaceHolder ID="tabViewCG" runat="server">
                                    <tr class="tempRow">
                                        <td>
                                            车型：<select class="inputselect" name="selRoomTypeList" valid="required" errmsg="*请选择用车类型!">
                                                <option value=",">-请选择-</option>
                                                <%=GetCarTypeList("")%>
                                            </select>
                                            车牌号：
                                            <input type="text" name="txtCarNumber" class="inputtext formsize50"/>
                                            司机：<input type="text" name="txtDirverName" class="inputtext formsize50"/>
                                            司机电话：
                                            <input type="text" name="txDirverPhone" class="inputtext formsize50"/>
                                            车价：<input type="text" name="txtCarPriceCG" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入车价!|车价格式不正确!" />
                                            天数：
                                            <input type="text" name="txtDaysCG" class="inputtext formsize50" valid="required|RegInteger"
                                                errmsg="*请输入天数!|请填写正确的天数!" />
                                            <br />
                                            金额：
                                            <input type="text" name="txtTotalPriceCG" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入金额!|金额格式不正确!" />
                                            备注：<input type="text" name="txtSheWaiBeiZhu" class="inputtext" maxlength="255" />
                                        </td>
                                        <td width="110" align="right">
                                            <a href="javascript:void(0);" class="addbtn">
                                                <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                    class="delbtn">
                                                    <img src="/images/delimg.gif" width="48" height="20" /></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:Repeater ID="repPricesListCG" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td>
                                                车型：<select class="inputselect" name="selRoomTypeList" valid="required" errmsg="*请选择用车类型!">
                                                    <option value=",">-请选择-</option>
                                                    <%# GetCarTypeList(Eval("CarId").ToString())%>
                                                </select>
                                                车牌号：
                                                <input type="text" name="txtCarNumber" value="<%#Eval("CarNumber") %>" class="inputtext formsize50"
                                                    valid="required" errmsg="*请输入车牌号!" />
                                                司机：<input type="text" name="txtDirverName" value="<%#Eval("Driver") %>" class="inputtext formsize50"
                                                    valid="required" errmsg="*请输入司机姓名!" />
                                                司机电话：
                                                <input type="text" name="txDirverPhone" value="<%#Eval("DriverPhone") %>" class="inputtext formsize50"
                                                    valid="required" errmsg="*请输入司机电话!" />
                                                车价：<input type="text" name="txtCarPriceCG" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("CarPrice").ToString())) %>"
                                                    class="inputtext formsize50" valid="required|isMoney" errmsg="*请输入车价!|车价格式不正确!" />
                                                天数：
                                                <input type="text" name="txtDaysCG" value="<%#Eval("Days") %>" class="inputtext formsize50"
                                                    valid="required|RegInteger" errmsg="*请输入天数!|请填写正确的天数!" />
                                                <br />
                                                金额：
                                                <input type="text" name="txtTotalPriceCG" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("SumPrice").ToString())) %>"
                                                    valid="required|isMoney" min="1" errmsg="*请输入金额!|金额格式不正确!" />
                                                备注：<input type="text" name="txtSheWaiBeiZhu" value="<%#Eval("Remark") %>" class="inputtext"
                                                    maxlength="255" />
                                            </td>
                                            <td width="110" align="right">
                                                <a href="javascript:void(0);" class="addbtn">
                                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0);"
                                                        class="delbtn">
                                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div id="divItems">
                            <table width="100%" border="0" cellspacing="0" id="tabPricesTS" cellpadding="0" class="noborder bottom_bot">
                                <asp:PlaceHolder ID="tabViewTS" runat="server">
                                    <tr class="tempRow">
                                        <td>
                                            车型：<select class="inputselect" name="selRoomTypeListTS" valid="required" errmsg="*请选择用车类型!">
                                                <option value=",">-请选择-</option>
                                                <%=GetCarTypeList("")%>
                                            </select>
                                            车牌号：
                                            <input type="text" name="txtCarNumberTS" class="inputtext formsize50" valid="required"
                                                errmsg="*请输入车牌号!" />
                                            司机：<input type="text" name="txtDirverNameTS" class="inputtext formsize50" valid="required"
                                                errmsg="*请输入司机姓名!" />
                                            司机电话：
                                            <input type="text" name="txDirverPhoneTS" class="inputtext formsize50" valid="required"
                                                errmsg="*请输入司机电话!" />
                                            车价：<input type="text" name="txtCarPriceTS" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入车价!|车价格式不正确!" />
                                            天数：
                                            <input type="text" name="txtDaysTS" class="inputtext formsize50" valid="required|RegInteger"
                                                errmsg="*请输入天数!|请填写正确的天数!" />
                                            过路过桥费：
                                            <input type="text" name="txtBridgePrice" class="inputtext formsize50" valid="isMoney"
                                                errmsg="过路过桥费格式不正确!" />
                                            司机小费：
                                            <input type="text" name="txtDriverPrice" class="inputtext formsize50" valid="isMoney"
                                                errmsg="司机小费格式不正确!" />
                                            司机房费：
                                            <input type="text" name="txtDriverRoomPrice" class="inputtext formsize50" valid="isMoney"
                                                errmsg="司机房费格式不正确!" />
                                            司机餐费：
                                            <input type="text" name="txtDriverDiningPrice" class="inputtext formsize50" valid="isMoney"
                                                errmsg="司机餐费格式不正确!" />
                                            空驶费：
                                            <input type="text" name="txtEmptyDrivingPrice" class="inputtext formsize50" valid="isMoney"
                                                errmsg="空驶费格式不正确!" />
                                            其他：
                                            <input type="text" name="txtOtherPrice" class="inputtext formsize50" valid="isMoney"
                                                errmsg="其他费格式不正确!" />
                                            金额：
                                            <input type="text" name="txtTotalPriceTS" class="inputtext formsize50" valid="required|isMoney"
                                                errmsg="*请输入金额!|金额格式不正确!" />
                                        </td>
                                        <td width="110" align="right">
                                            <a href="javascript:void(0)" class="addbtn">
                                                <img height="20" width="48" src="../images/addimg.gif"></a>&nbsp;<a href="javascript:void(0)"
                                                    class="delbtn">
                                                    <img height="20" width="48" src="../images/delimg.gif"></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:Repeater ID="repPricesListTS" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td>
                                                车型：<select class="inputselect" name="selRoomTypeListTS" valid="required" errmsg="*请选择用车类型!">
                                                    <option value=",">-请选择-</option>
                                                    <%# GetCarTypeList(Eval("CarId").ToString())%>
                                                </select>
                                                车牌号：
                                                <input type="text" name="txtCarNumberTS" value="<%#Eval("CarNumber") %>" class="inputtext formsize50"
                                                    valid="required" errmsg="*请输入车牌号!" />
                                                司机：<input type="text" name="txtDirverNameTS" value="<%#Eval("Driver") %>" class="inputtext formsize50"
                                                    valid="required" errmsg="*请输入司机姓名!" />
                                                司机电话：
                                                <input type="text" name="txDirverPhoneTS" value="<%#Eval("DriverPhone") %>" class="inputtext formsize50"
                                                    valid="required" errmsg="*请输入司机电话!" />
                                                车价：<input type="text" name="txtCarPriceTS" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("CarPrice").ToString())) %>"
                                                    class="inputtext formsize50" valid="required|isMoney" errmsg="*请输入车价!|车价格式不正确!" />
                                                天数：
                                                <input type="text" name="txtDaysTS" value="<%#Eval("Days") %>" class="inputtext formsize50"
                                                    valid="required|RegInteger" errmsg="*请输入天数!|请填写正确的天数!" />
                                                过路过桥费：
                                                <input type="text" name="txtBridgePrice" class="inputtext formsize50" valid="isMoney"
                                                    value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("BridgePrice").ToString())) %>"
                                                    errmsg="过路过桥费格式不正确!" />
                                                司机小费：
                                                <input type="text" name="txtDriverPrice" class="inputtext formsize50" valid="isMoney"
                                                    value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("DriverPrice").ToString())) %>"
                                                    errmsg="司机小费格式不正确!" />
                                                司机房费：
                                                <input type="text" name="txtDriverRoomPrice" class="inputtext formsize50" valid="isMoney"
                                                    value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("DriverRoomPrice").ToString())) %>"
                                                    errmsg="司机房费格式不正确!" />
                                                司机餐费：
                                                <input type="text" name="txtDriverDiningPrice" class="inputtext formsize50" valid="isMoney"
                                                    value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("DriverDiningPrice").ToString())) %>"
                                                    errmsg="司机餐费格式不正确!" />
                                                空驶费：
                                                <input type="text" name="txtEmptyDrivingPrice" class="inputtext formsize50" valid="isMoney"
                                                    value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("EmptyDrivingPrice").ToString())) %>"
                                                    errmsg="空驶费格式不正确!" />
                                                其他：
                                                <input type="text" name="txtOtherPrice" class="inputtext formsize50" valid="isMoney"
                                                    value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("OtherPrice").ToString())) %>"
                                                    errmsg="其他费格式不正确!" />
                                                金额：
                                                <input type="text" name="txtTotalPriceTS" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("SumPrice").ToString())) %>"
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
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span> </span>结算费用
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txttotalMoney" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                            errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:TextBox>
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
车队名称：
司机：
车牌号：
司机电话:
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        备注：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOtherRemark" TextMode="MultiLine" runat="server" CssClass="inputtext formsize800" style="height: 60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span> 状态：</span>
                    </th>
                    <td colspan="3">
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
                    <ul id="ul_btn_list">
                        <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排车队</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="tblcarlist">
                    <tr>
                        <th align="center">
                            车队名称
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
                            <tr>
                                <td align="center">
                                    <%# Eval("SourceName")%>
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
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" data-type="<%#Eval("PriceType").ToString() %>"
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
            </asp:PlaceHolder>
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
        var CarPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _DeleteCar: function(objID) {
                var _Url = '/Plan/PlanCarList.aspx?sl=' + CarPage.sl + '&type=' + CarPage.type + '&tourId=' + CarPage.tourId + "&iframeId=" + CarPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanCarList.aspx?sl=' + CarPage.sl + '&action=delete&PlanId=' + objID + '&tourid=' + CarPage.tourId,
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
                var _url = '/Plan/PlanCarList.aspx?sl=' + CarPage.sl + '&type=' + CarPage.type + '&tourId=' + CarPage.tourId + '&iframeId=' + CarPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/Plan/PlanCarList.aspx?sl=' + CarPage.sl + '&action=save&planId=' + planId + "&tourId=" + tourId,
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
                                    CarPage._SaveCar(_planId, CarPage.tourId);
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
                    $("input[type=text][name='txtTotalPriceCG']").each(function() {
                        var trClass = $(this).closest("tr");
                        var daysCG = trClass.find("input[type=text][name='txtDaysCG']").val();
                        var carPriceCG = trClass.find("input[type=text][name='txtCarPriceCG']").val();
                        var TotalPrices = tableToolbar.calculate(daysCG, carPriceCG, "*");
                        trClass.find("input[type=text][name='txtTotalPriceCG']").val(TotalPrices);
                        total = tableToolbar.calculate(total, TotalPrices, "+");
                    });
                }
                if (tp == "2") {
                    $("input[type=text][name='txtTotalPriceTS']").each(function() {
                        var trClass = $(this).closest("tr");
                        var daysCG = trClass.find("input[type=text][name='txtDaysTS']").val();
                        var carPriceCG = trClass.find("input[type=text][name='txtCarPriceTS']").val();
                        var bridgePrice = trClass.find("input[type=text][name='txtBridgePrice']").val();
                        var driverPrice = trClass.find("input[type=text][name='txtDriverPrice']").val();
                        var driverRoomPrice = trClass.find("input[type=text][name='txtDriverRoomPrice']").val();
                        var driverDiningPrice = trClass.find("input[type=text][name='txtDriverDiningPrice']").val();
                        var emptyDrivingPrice = trClass.find("input[type=text][name='txtEmptyDrivingPrice']").val();
                        var otherPrice = trClass.find("input[type=text][name='txtOtherPrice']").val();
                        var p = tableToolbar.calculate(otherPrice, tableToolbar.calculate(emptyDrivingPrice, tableToolbar.calculate(driverDiningPrice, tableToolbar.calculate(driverRoomPrice, tableToolbar.calculate(bridgePrice, driverPrice, "+"), "+"), "+"), "+"), "+");
                        var TotalPrices = tableToolbar.calculate(p, tableToolbar.calculate(daysCG, carPriceCG, "*"), "+");
                        trClass.find("input[type=text][name='txtTotalPriceTS']").val(TotalPrices);
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
                        window.location.href = '/Plan/PlanCarList.aspx?sl=' + CarPage.sl + '&action=update&PlanId=' + PlanId + '&PriceType=' + PriceType + '&suppId=' + suppId + '&tourId=' + CarPage.tourId + '&iframeId=' + CarPage.iframeId;
                    }
                    return false;
                });

                //查看 
                $("#tblcarlist").find("[data-class='showCar']").unbind("click");
                $("#tblcarlist").find("[data-class='showCar']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    var suppId = $(this).find("img").attr("data-sid");
                    if (PlanId != "") {
                        window.location.href = '/Plan/PlanCarList.aspx?sl=' + CarPage.sl + '&action=update&PlanId=' + PlanId + '&suppId=' + suppId + '&tourId=' + CarPage.tourId + '&iframeId=' + CarPage.iframeId + "&show=1";
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
                            CarPage._DeleteCar(planId);
                        }
                    });

                    return false;
                });

                //失去焦点计算价格
                $("input[type='text'][name='txtCarPriceCG'],input[type='text'][name='txtDaysCG'],input[type='text'][name='txtTotalPriceCG']").blur(function() {
                    CarPage._TotalPrices();
                });
                $("input[type='text'][name='txtCarPriceTS'],input[type='text'][name='txtDaysTS'],input[type='text'][name='txtBridgePrice'],input[type='text'][name='txtDriverPrice'],input[type='text'][name='txtDriverRoomPrice'],input[type='text'][name='txtDriverDiningPrice'],input[type='text'][name='txtEmptyDrivingPrice'],input[type='text'][name='txtOtherPrice'],input[type='text'][name='txtTotalPriceTS']").blur(function() {
                    CarPage._TotalPrices();
                });

                //预付申请
                $("#tblcarlist").find("a[data-class='Prepaid']").unbind("click");
                $("#tblcarlist").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    CarPage._OpenBoxy("预付申请", '/Plan/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + CarPage.type + '&sl=' + CarPage.sl + '&tourId=' + CarPage.tourId + '&souceName=' + escape(soucesName), "850px", "600px", true);
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    var tp = $('input:radio[name="radshipType"]:checked').val();
                    if (tp == "2") {
                        CarPage._RemoveAttr("#divGroup");
                    }
                    if (tp == "1") {
                        CarPage._RemoveAttr("#divItems");
                    }
                    if (!ValiDatorForm.validator($("#<%=formCar.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                        CarPage._SaveCar(planId, CarPage.tourId);
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
                    CarPage._DdlChangeEvent(supid);
                }

                CarPage._BIndBtn();
                $("#tabPricesCG").autoAdd({ addCallBack: parent.ConfigPage.SetWinHeight, delCallBack: parent.ConfigPage.SetWinHeight });
                $("#tabPricesTS").autoAdd({ addCallBack: parent.ConfigPage.SetWinHeight, delCallBack: parent.ConfigPage.SetWinHeight });

                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }
            },
            _AjaxReqCarModel: function(id) {
                if (id) {
                    $.newAjax({
                        type: "POST",
                        url: "/Plan/PlanCarList.aspx?suppId=" + id + "&action=getdata&sl=" + CarPage.sl + "&tourId=" + CarPage.tourId + "&m=" + new Date().getTime(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.tolist != "") {
                                var info = "";
                                if (ret.tolist.length > 0) {
                                    for (var i = 0; i < ret.tolist.length; i++) {
                                        $("select[name='selRoomTypeList']").append("<option value='" + ret.tolist[i].id + "," + ret.tolist[i].text + "'>" + ret.tolist[i].text + "</option>");
                                    }
                                }
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg("暂无车型");
                            return false;
                        }
                    });
                }
            },
            _AjaxContectInfo: function(obj) {
                if (obj) {
                    $("#<%=SupplierControl1.ClientText %>").val(obj.name);
                    $("#<%=SupplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=txtContectName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContectPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContectFax.ClientID %>").val(obj.contactfax);

                    $("select[name='selRoomTypeList'] option").each(function() {
                        if ($(this).val() != "" && $(this).val() != ",") {
                            $(this).remove();
                        }
                    });
                    CarPage._AjaxReqCarModel(obj.id);
                    CarPage._DdlChangeEvent(obj.id, obj.name);
                }
            },
            _DdlChangeEvent: function(id) {
                //下拉事件
                $("#tabPricesCG").find("select[name='selRoomTypeList']").unbind("change");
                $("#tabPricesCG").find("select[name='selRoomTypeList']").change(function() {
                    var seleId = $(this).find("option:selected").val().split(',')[0];
                    var seleName = $(this).find("option:selected").val().split(',')[1];
                    if (seleId != "" && seleName != "") {
                        CarPage._AjaxReqPrice($(this).closest("tr"), seleId, id, seleName);
                        CarPage._TotalPrices();
                    }
                });
            },
            _AjaxReqPrice: function(tr, seleId, id, seleName) {
                $.newAjax({
                    type: "POST",
                    url: "/Plan/PlanCarList.aspx?suppId=" + id + "&rid=" + seleId + "&action=getprice&sl=" + CarPage._sl + "&tourId=" + CarPage._tourID + "&m=" + new Date().getTime(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.tolist != "") {
                            if (ret.tolist.length > 0) {
                                var html = [], mTp = '<%=ProviderMoneyStr %>';
                                html.push('<tr><th width="40px">选择</th><th width="75px">有效时间</th><th width="75px">车型</th><th width="75px">结算价</th><th width="75px">类型</th></tr>');
                                for (var i = 0; i < ret.tolist.length; i++) {
                                    html.push('<tr><td><input type="radio" value="' + ret.tolist[i].JiaGeJS + '" name="radPrice" /></td><td>' + ChangeDateFormat(ret.tolist[i].STime) + '/' + ChangeDateFormat(ret.tolist[i].ETime) + '</td><td>' + seleName + '</td><td>' + ret.tolist[i].JiaGeJS + '</td><td>' + (ret.tolist[i].BinKeLeiXing == 1 ? "内宾" : "外宾") + '</td></tr>')
                                }
                            }
                            //从供应商选用显示参考价格
                            if (id != "" && seleId != "") {
                                CarPage.ShowListDiv(html.join(''));
                                $("#a_openInfoDiv").click(function() {
                                    CarPage.ShowListDiv("");
                                });
                            }
                            else {
                                $("#a_openInfoDiv").unbind("click");
                            }
                            $("input[type='radio'][name='radPrice']").click(function() {
                                $.trim(tr.find("input[type='text'][name='txtCarPriceCG']").val($(this).val()));
                                $("#divShowList").fadeOut("fast");
                                CarPage._TotalPrices();
                            });
                        }
                        else {
                            tableToolbar._showMsg("无此车型价格信息！");
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
            CarPage._InitPage();
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

        });

        /*
        @用车安排价格组成
        1 常规线路
        2 特殊线路
        */
        function PriceItemType(ItemType) {
            CarPage._TotalPrices();
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

        function ChangeDateFormat(jsondate) {
            if (jsondate != null) {
                jsondate = jsondate.replace("/Date(", "").replace(")/", "");
                if (jsondate.indexOf("+") > 0) {
                    jsondate = jsondate.substring(0, jsondate.indexOf("+"));
                }
                else if (jsondate.indexOf("-") > 0) {
                    jsondate = jsondate.substring(0, jsondate.indexOf("-"));
                }
                var date = new Date(parseInt(jsondate, 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                return date.getFullYear() + "-" + month + "-" + currentDate;
            }
            else {
                return "";
            }
        }

    </script>

</body>
</html>
