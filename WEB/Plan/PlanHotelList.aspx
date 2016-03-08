<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanHotelList.aspx.cs"
    Inherits="EyouSoft.Web.Plan.PlanHotelList" %>

<%@ Register Src="/UserControl/SupplierControl.ascx" TagName="supplierControl" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

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
    <form id="Fromhotel" runat="server">
    <asp:HiddenField runat="server" ID="hidroomTypePrices" />
    <input type="hidden" id="hotelStar" runat="server" />
    <div id="con_faq_3">
        <div class="jidiao-r">
            <div action="divfortoggle">
                <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b" id="roomtypelist">
                    <tr>
                        <th width="15%" align="right" class="border-l">
                            预定方式：
                        </th>
                        <td width="40%">
                            <select class="inputselect" name="dueToway">
                                <asp:Literal ID="litDueToway" runat="server"></asp:Literal>
                            </select>
                        </td>
                        <th width="15%" align="right">
                            <span class="fontred">*</span> 酒店名称：
                        </th>
                        <td width="30%">
                            <uc1:supplierControl ID="supplierControl1" runat="server" CallBack="HotelPage._AjaxContectInfo"
                                SupplierType="酒店" IsMust="true" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            联系人：
                        </th>
                        <td>
                            <asp:TextBox ID="txtContectName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                        <th width="15%" align="right">
                            <span class="border-l">星级：</span>
                        </th>
                        <td width="30%">
                            <select class="inputselect" name="hotelStart" id="hotelStart">
                                <asp:Literal ID="litHotelStart" runat="server"></asp:Literal>
                            </select>
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
                            <p>
                                联系传真：</p>
                        </th>
                        <td>
                            <asp:TextBox ID="txtContectFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span> 入住时间：
                        </th>
                        <td>
                            <asp:TextBox ID="txtStartTime" runat="server" CssClass="inputtext formsize80" valid="required"
                                errmsg="*请输入入住时间!"></asp:TextBox>
                        </td>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span> 是否含早：
                        </th>
                        <td align="left" colspan="3">
                            <select class="inputselect" name="containsEarly" valid="required" errmsg="*请选择是否含有早餐!">
                                <asp:Literal ID="litContainsEarly" runat="server"></asp:Literal>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span> 离店时间：
                        </th>
                        <td>
                            <asp:TextBox ID="txtEndTime" runat="server" CssClass="inputtext formsize80" valid="required"
                                errmsg="*请输入离店时间!"></asp:TextBox>
                        </td>
                        <th align="right">
                            <span class="border-l"><span class="fontred">*</span>天数：</span>
                        </th>
                        <td>
                            <asp:TextBox ID="txtroomDays" runat="server" CssClass="inputtext formsize50" valid="required|RegInteger|range"
                                errmsg="*请输入天数!|*天数输入有误!|*天数必须大于0!" min="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span> <a id="a_openInfoDiv" href="javascript:void(0);">房型</a>：
                        </th>
                        <td colspan="3">
                            <table id="tblHolderView" width="100%" border="0" cellspacing="0" cellpadding="0"
                                class="noborder">
                                <asp:Repeater ID="reproomtypelist" runat="server">
                                    <ItemTemplate>
                                        <tr class="tempRow">
                                            <td style="padding: 0px;" width="75%">
                                                <select class="inputselect" name="ddlRoomType" valid="required" errmsg="*请选择房型!">
                                                    <option value="" selected="selected">--请选择--</option>
                                                    <%# GetRoomType(Eval("RoomId").ToString())%>
                                                </select>
                                                <input type="hidden" name="hidRoomTypeId" value="<%# Eval("RoomId") %>,<%# Eval("RoomType") %>" />
                                                单价
                                                <input type="text" name="txtunitPrice" class="inputtext formsize40" valid="required|isMoney"
                                                    errmsg="*请输入单价!|*单价输入有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("UnitPrice").ToString()) %>" />
                                                <select name="select" class="inputselect">
                                                    <%# Getcalculate(((int)Eval("PriceType")).ToString())%>
                                                </select>
                                                间数
                                                <input type="text" name="txtRoomNumber" class="inputtext formsize40" valid="required|RegInteger"
                                                    errmsg="*请输入数量!|*数量必须是正整数!" value="<%# Eval("Quantity") %>" />
                                                金额：
                                                <input type="text" class="inputtext formsize40" name="txtTotalMoney" valid="required|isMoney"
                                                    errmsg="*请输入小计!|*小计输入有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("Total").ToString()) %>" />
                                                元
                                            </td>
                                            <td style="padding: 0px;" width="15%" align="right">
                                                <a class="addbtn" href="javascript:void(0)">
                                                    <img height="20" width="48" src="/images/addimg.gif" /></a>&nbsp; <a class="delbtn"
                                                        href="javascript:void(0)">
                                                        <img height="20" width="48" src="/images/delimg.gif" />
                                                    </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:PlaceHolder ID="holderView" runat="server">
                                    <tr class="tempRow">
                                        <td style="padding: 0px;" width="75%">
                                            <select class="inputselect" style="width: 100px;" name="ddlRoomType" valid="required"
                                                errmsg="*请选择房型!">
                                                <option value="">--请选择--</option>
                                                <%= GetRoomType("")%>
                                            </select>
                                            <input type="hidden" name="hidRoomTypeId" value="" />
                                            单价
                                            <input type="text" name="txtunitPrice" class="inputtext formsize40" valid="required|isMoney"
                                                errmsg="*请输入单价!|*单价输入有误!" />
                                            <select name="select" class="inputselect">
                                                <%=Getcalculate("") %>
                                            </select>
                                            间数
                                            <input type="text" name="txtRoomNumber" class="inputtext formsize40" valid="required|RegInteger"
                                                errmsg="*请输入数量!|*数量必须是正整数!" />
                                            金额：
                                            <input type="text" class="inputtext formsize40" name="txtTotalMoney" valid="required|isMoney"
                                                errmsg="*请输入小计!|*小计输入有误!" />
                                            元
                                        </td>
                                        <td style="padding: 0px;" width="15%" align="right">
                                            <a class="addbtn" href="javascript:void(0)">
                                                <img height="20" width="48" src="/images/addimg.gif" /></a>&nbsp; <a class="delbtn"
                                                    href="javascript:void(0)">
                                                    <img height="20" width="48" src="/images/delimg.gif" />
                                                </a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            免房数量：
                        </th>
                        <td>
                            <asp:TextBox ID="txtFreRoomNumbers" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                            间
                        </td>
                        <th align="right">
                            免房金额
                        </th>
                        <td>
                            <asp:TextBox ID="txtFreRoomMoney" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            备注：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="inputtext formsize800" Style="height: 60px"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span>结算费用：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTotalPrices" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                                errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:TextBox>
                            元
                        </td>
                        <th align="right">
                            <span class="fontred">*</span><span class="border-l">支付方式</span>：
                        </th>
                        <td>
                            <select class="inputselect" name="panyMent" id="isSigning">
                                <asp:Literal ID="litpanyMent" runat="server"></asp:Literal>
                            </select>
                            <span id="spanQDS" style="display: none;">签单数<asp:TextBox ID="txtSigningCount" runat="server"
                                CssClass="inputtext formsize40" Text="1"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            导游需知：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txtGuidNotes" TextMode="MultiLine" runat="server" CssClass="inputtext formsize800"
                                Style="height: 60px">
酒店名称：
入住-离店时间：
前台电话：
请提前联络。
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" class="border-l">
                            <span class="fontred">*</span><span class="border-l">状态</span>：
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
                        <li class="cun-cy"><a id="btnSave" href="javascript:">保存</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排酒店 <a href="javascript:void(0);" onclick="HotelPage.QuanBuLuoShi();">全部落实</a>
                    </p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="actionType">
                    <tr>
                        <th align="center">
                            酒店名称
                        </th>
                        <th align="center">
                            入住/离店日期
                        </th>
                        <th align="center">
                            安排明细
                        </th>
                        <th align="center">
                            免房数量
                        </th>
                        <th align="center">
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
                    <asp:Repeater ID="repHotellist" runat="server">
                        <ItemTemplate>
                            <tr style="background-color: <%#(EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status")==EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"#dadada":""%>">
                                <td align="center">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetDateString(Eval("StartDate"), "yyyy/MM/dd")%>&nbsp;/
                                    <%# UtilsCommons.GetDateString(Eval("EndDate"), "yyyy/MM/dd")%>
                                </td>
                                <td align="left">
                                    <%# GetAPMX(Eval("PlanHotelRoomList")) %>
                                </td>
                                <td align="center">
                                    <%# Eval("FreeNumber")%>
                                </td>
                                <td align="center">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%></b>
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
                                    <a href='<%#querenUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" border="0" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:void(0);" data-class="updateHotel" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                    <a href="javascript:void(0);" data-class="deleteHotel">
                                        <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:void(0);" data-class="showHotel" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                    <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:PlaceHolder>
        </div>
        <input type="hidden" id="hidUserNum" runat="server" />
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
        var HotelPage = {
            _sl: '<%=SL%>',
            _type: '<%=Utils.GetQueryStringValue("type") %>',
            _tourID: '<%=Utils.GetQueryStringValue("tourId") %>',
            _iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _tourMode: '<%=Utils.GetQueryStringValue("TourMode") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _daysDiff: function() {
                var daysStr = $("#roomtypelist").children().find("tr");
                var startTime = new Date(Date.parse(daysStr.find("#<%=txtStartTime.ClientID %>").val().replace(/-/g,
"/"))).getTime();
                var endTime = new Date(Date.parse(daysStr.find("#<%=txtEndTime.ClientID %>").val().replace(/-/g,
"/"))).getTime();

                var diffDays = 0;
                if (isNaN(startTime) && isNaN(endTime)) {
                    diffDays = 0;
                }
                else if (isNaN(startTime) || isNaN(endTime)) {
                    diffDays = 1;
                }
                else {
                    diffDays = Math.abs((startTime - endTime)) / (1000 * 60 * 60 * 24);
                }
                if (diffDays == 0) {
                    diffDays = 1;
                }
                $("#<%=txtroomDays.ClientID %>").val(diffDays);

            },
            _DeleteHotel: function(objID) {
                var _url = '/Plan/PlanHotelList.aspx?sl=' + HotelPage._sl + '&type=' + HotelPage._type + '&TourMode=' + HotelPage._tourMode + '&tourId=' + HotelPage._tourID + "&iframeId=" + HotelPage._iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanHotelList.aspx?sl=' + HotelPage._sl + '&action=delete&planId=' + objID + '&TourMode=' + HotelPage._tourMode + '&tourid=' + HotelPage._tourID,
                    cache: false,
                    dataType: 'json',
                    success: function(ret) {
                        tableToolbar._showMsg(ret.msg, function() {
                            window.location.href = _url;
                        });
                        return false;
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _SaveHotel: function(planId, tourId) {
                var _Url = '/Plan/PlanHotelList.aspx?sl=' + HotelPage._sl + '&type=' + HotelPage._type + '&TourMode=' + HotelPage._tourMode + '&tourId=' + HotelPage._tourID + '&iframeId=' + HotelPage._iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/Plan/PlanHotelList.aspx?action=save&sl=' + HotelPage._sl + '&planId=' + planId + '&TourMode=' + HotelPage._tourMode + '&tourId=' + tourId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: 'json',
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _planId = '<%=Utils.GetQueryStringValue("planId") %>';
                                    HotelPage._SaveHotel(_planId, HotelPage._tourID);
                                });
                            });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                //入住时间计算               
                $("#<%=txtStartTime.ClientID %>").focus(function() {
                    WdatePicker({ onpicked: HotelPage._daysDiff });
                });
                $("#<%=txtEndTime.ClientID %>").focus(function() {
                    WdatePicker({ onpicked: HotelPage._daysDiff });
                });

                $("#<%=txtStartTime.ClientID %>,#<%=txtEndTime.ClientID %>").blur(function() {
                    HotelPage._daysDiff();
                });

                $("#actionType").find("a[data-class='updateHotel']").unbind("click");
                $("#actionType").find("a[data-class='updateHotel']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/Plan/PlanHotelList.aspx?sl=' + HotelPage._sl + '&action=update&planId=' + planId + '&TourMode=' + HotelPage._tourMode + '&tourId=' + HotelPage._tourID + '&type=' + HotelPage._type + '&iframeId=' + HotelPage._iframeId;
                    }
                    return false;
                });

                //查看
                $("#actionType").find("a[data-class='showHotel']").unbind("click");
                $("#actionType").find("a[data-class='showHotel']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/Plan/PlanHotelList.aspx?sl=' + HotelPage._sl + '&action=update&planId=' + planId + '&TourMode=' + HotelPage._tourMode + '&tourId=' + HotelPage._tourID + '&type=' + HotelPage._type + '&iframeId=' + HotelPage._iframeId + "&show=1";
                    }
                    return false;
                });

                $("#actionType").find("a[data-class='deleteHotel']").unbind("click");
                $("#actionType").find("a[data-class='deleteHotel']").click(function() {
                    var newThis = $(this);
                    tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            HotelPage._DeleteHotel(planId);
                        }
                    });
                    return false;
                });

                $("input[name='txtRoomNumber'],input[name='txtunitPrice'],input[name='txtStartTime'],input[name='txtEndTime'],input[name='txtFreRoomMoney'],input[name='txtroomDays']").blur(function() {
                    HotelPage._InitTotalIncome();
                    HotelPage._RoomCount();
                });

                $("#actionType").find("a[data-class='Prepaid']").unbind("click");
                $("#actionType").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    HotelPage._OpenBoxy("预付申请", '/Plan/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + HotelPage._type + '&sl=' + HotelPage._sl + '&tourId=' + HotelPage._tourID + '&souceName=' + escape(soucesName), "850px", "600px", true);
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=Fromhotel.ClientID %>").get(0), "parent")) {
                        return;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planId") %>';
                        HotelPage._SaveHotel(planId, HotelPage._tourID);
                    }
                });
            },
            _InitTotalIncome: function() {
                //房型小计计算
                var roomToalPrice = 0;
                $("input[type='text'][name='txtTotalMoney']").each(function() {
                    var p = $(this).closest("tr");
                    var _unitPrices = $.trim(p.find("input[type='text'][name='txtunitPrice']").val());
                    var _roomNums = $.trim(p.find("input[type='text'][name='txtRoomNumber']").val());
                    var _days = $("#<%=txtroomDays.ClientID %>").val();
                    var TotalMoney = tableToolbar.calculate(_unitPrices, _roomNums, "*");
                    p.find("input[name='txtTotalMoney']").val(TotalMoney);
                    roomToalPrice = tableToolbar.calculate(roomToalPrice, tableToolbar.calculate(TotalMoney, _days, "*"), "+");
                });
                var fre = $("#<%=txtFreRoomMoney.ClientID %>").val();
                $("#<%=txtTotalPrices.ClientID %>").val(tableToolbar.calculate(roomToalPrice, fre, "-"));
            },
            _AjaxContectInfo: function(obj) {
                var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 %>';
                var companyId = "<%=this.SiteUserInfo.CompanyId %>";
                if (obj) {
                    $("#<%=supplierControl1.ClientText %>").val(obj.name);
                    $("#<%=supplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=txtContectName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContectPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContectFax.ClientID %>").val(obj.contactfax);
                    if (obj.jiesuanfangshi == "<%=EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi.挂账 %>") {
                        $("#isSigning").val("<%=(int)EyouSoft.Model.EnumType.PlanStructure.Payment.签单 %>");
                        $("#spanQDS").css("display", "");
                    }
                    HotelPage._DdlChangeEvent(obj.id);
                    HotelPage._AjaxReqHotelStar(obj.id);
                }
            },
            _PageInit: function() {
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

                var supid = $("#<%=supplierControl1.ClientValue %>").val();
                if (supid != "") {
                    HotelPage._DdlChangeEvent(supid);
                    $("#tblHolderView").find("select[name='ddlRoomType']").each(function() {
                        $(this).val($.trim($(this).closest("tr").find("input[type='hidden'][name='hidRoomTypeId']").val()));
                    });
                }

                HotelPage._BindBtn();


                $("#tblHolderView").autoAdd({ addCallBack: parent.ConfigPage.SetWinHeight, delCallBack: parent.ConfigPage.SetWinHeight });
                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }

            },
            _RoomCount: function() {
                var _totalNums = 0;
                $("input[name='txtRoomNumber']").each(function() {
                    var tr = $(this).closest("tr");
                    var thisNums = tr.find("input[name='txtRoomNumber']").val();
                    _totalNums = tableToolbar.calculate(_totalNums, thisNums, "+");
                });
            },
            _DdlChangeEvent: function(id) {
                //房型下拉事件
                $("#tblHolderView").find("select[name='ddlRoomType']").unbind("change");
                $("#tblHolderView").find("select[name='ddlRoomType']").change(function() {
                    //                    if ($("#<%=txtStartTime.ClientID %>").val() != "" && $("#<%=txtEndTime.ClientID %>").val() != "") {
                    //                        var seleId = $(this).find("option:selected").val().split(',')[0];
                    //                        HotelPage._AjaxReqHotelRoomPrice($(this).closest("tr"), seleId, id);
                    //                        HotelPage._InitTotalIncome();
                    //                    }
                    //                    else {
                    //                        tableToolbar._showMsg("请先选择入住-离店时间！");
                    //                    }
                    var seleId = $(this).find("option:selected").val().split(',')[0];
                    HotelPage._AjaxReqHotelRoomPrice($(this).closest("tr"), seleId, id);
                    HotelPage._InitTotalIncome();
                });
            },
            _AjaxReqHotelStar: function(id) {
                $.newAjax({
                    type: "POST",
                    url: "/Plan/PlanHotelList.aspx?suppId=" + id + "&star=1&action=getdata&sl=" + HotelPage._sl + "&tourId=" + HotelPage._tourID + "&m=" + new Date().getTime(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.star != "") {
                            $("#hotelStart").val(ret.star);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },

            _AjaxReqHotelRoomPrice: function(tr, seleId, id) {
                var d1 = ""//$("#<%=txtStartTime.ClientID %>").val();
                var d2 = ""//$("#<%=txtEndTime.ClientID %>").val();
                $.newAjax({
                    type: "POST",
                    url: "/Plan/PlanHotelList.aspx?suppId=" + id + "&rid=" + seleId + "&d1=" + d1 + "&d2=" + d2 + "&action=getdata&sl=" + HotelPage._sl + "&TourMode=" + HotelPage._tourMode + "&tourId=" + HotelPage._tourID + "&m=" + new Date().getTime(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.tolist != "") {
                            if (ret.tolist.length > 0) {
                                var html = [], mTp = '<%=ProviderMoneyStr %>';
                                if (HotelPage._tourMode == "2") {
                                    html.push('<tr><th width="40px">选择</th><th width="75px">有效时间</th><th width="75px">房型</th><th width="75px">散客价</th><th width="75px">服务费</th><th width="75px">类型</th><th width="75px">含早</th></tr>');
                                    for (var i = 0; i < ret.tolist.length; i++) {
                                        html.push('<tr><td><input type="radio" value="' + ret.tolist[i].JiaGeSJS + '" name="radPrice" /></td><td>' + ChangeDateFormat(ret.tolist[i].STime) + '/' + ChangeDateFormat(ret.tolist[i].ETime) + '</td><td>' + ret.tolist[i].FangXingName + '</td><td>' + mTp + ret.tolist[i].JiaGeSJS + '</td><td>' + mTp + ret.tolist[i].JiaGeSFW + '</td><td>' + (ret.tolist[i].BinKeLeiXing == 1 ? "内宾" : "外宾") + '</td><td>' + (ret.tolist[i].IsHanZao ? "含" : "不含") + '</td></tr>')
                                    }
                                }
                                else {
                                    html.push('<tr><th width="40px">选择</th><th width="75px">有效时间</th><th width="75px">房型</th><th width="75px">团队价</th><th width="75px">服务费</th><th width="75px">类型</th><th width="75px">含早</th></tr>');
                                    for (var i = 0; i < ret.tolist.length; i++) {
                                        html.push('<tr><td><input type="radio" value="' + ret.tolist[i].JiaGeTJS + '" name="radPrice" /></td><td>' + ChangeDateFormat(ret.tolist[i].STime) + '/' + ChangeDateFormat(ret.tolist[i].ETime) + '</td><td>' + ret.tolist[i].FangXingName + '</td><td>' + mTp + ret.tolist[i].JiaGeTJS + '</td><td>' + mTp + ret.tolist[i].JiaGeTFW + '</td><td>' + (ret.tolist[i].BinKeLeiXing == 1 ? "内宾" : "外宾") + '</td><td>' + (ret.tolist[i].IsHanZao ? "含" : "不含") + '</td></tr>')
                                    }
                                }
                            }
                            //从供应商选用显示参考价格
                            if (id != "" && seleId != "") {
                                HotelPage.ShowListDiv(html.join(''));
                                $("#a_openInfoDiv").click(function() {
                                    HotelPage.ShowListDiv("");
                                });
                            }
                            else {
                                $("#a_openInfoDiv").unbind("click");
                            }
                            $("input[type='radio'][name='radPrice']").click(function() {
                                $.trim(tr.find("input[type='text'][name='txtunitPrice']").val($(this).val()));
                                $("#divShowList").fadeOut("fast");
                                HotelPage._InitTotalIncome();
                            });
                        }
                        else {
                            tableToolbar._showMsg("该酒店无此房型！");
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
            },
            //全部落实
            QuanBuLuoShi: function() {
                var _Url = '/Plan/PlanHotelList.aspx?sl=' + HotelPage._sl + '&type=' + HotelPage._type + '&TourMode=' + HotelPage._tourMode + '&tourId=' + HotelPage._tourID + "&iframeId=" + HotelPage._iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanHotelList.aspx?sl=' + HotelPage._sl + '&action=quanbuluoshi&type=<%=EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 %>&tourid=' + HotelPage._tourID,
                    cache: false,
                    dataType: "text",
                    success: function(msg) {
                        parent.tableToolbar._showMsg(msg, function() {
                            window.location.href = _Url;
                        });
                        return false;
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            }
        }


        $(function() {
            HotelPage._PageInit();
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
