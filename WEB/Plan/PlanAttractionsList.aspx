<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanAttractionsList.aspx.cs"
    Inherits="EyouSoft.Web.Plan.PlanAttractionsList" %>

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
    <form id="aform" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_5">
        <div action="divfortoggle">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>景点公司：
                    </th>
                    <td width="40%">
                        <uc1:suppliercontrol id="SupplierControl1" IsMust="true" runat="server" callback="ObjPage._AjaxContectInfo"
                            suppliertype="景点" />
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
                        <span class="fontred">*</span> <a id="a_openInfoDiv" href="javascript:void(0);">安排信息</a>：
                    </th>
                    <td colspan="3">
                        <table id="tabAnPaiInfo" width="100%" border="0" cellspacing="0" cellpadding="0"
                            class="noborder bottom_bot">
                            <asp:PlaceHolder ID="tabHolderView" runat="server">
                                <tr class="tempRow">
                                    <td>
                                        景点<秀>名称<select class="inputselect" name="selList" valid="required" errmsg="*请选择景点<秀>名称!">
                                                <option value=",">-请选择-</option> 
                                                <%=GetList("")%>
                                            </select>
                                        游览日期<input type="text" name="txtvisitTime" id="txtvisitTime" class="inputtext" onfocus="WdatePicker();"
                                            valid="required" errmsg="*请选择游览日期!"  style="width: 65px" />
                                        成人数<input type="text" class="inputtext formsize40" name="txtadultNums" valid="required|RegInteger" errmsg="*请输入成人数!|*成人数必须是正整数!" />
                                        儿童数<input type="text" class="inputtext formsize40" name="txtchildNums" valid="required|RegInteger" errmsg="*请输入儿童数!|*儿童数必须是正整数!" /><br />
                                        成人价<input type="text" class="inputtext formsize40" name="txtadultPrices" valid="required|isMoney" errmsg="*请输入成人价!|*成人价有误!" />
                                        儿童价<input type="text" class="inputtext formsize40" name="txtchildPrices" valid="required|isMoney" errmsg="*请输入儿童价!|*儿童价有误!" />
                                        家庭价<input type="text" class="inputtext formsize40" name="txtfamilyPrices" valid="required|isMoney" errmsg="*请输入家庭价!|*家庭价有误!" />
                                        席位<input type="text" class="inputtext formsize40" name="txtseats" />
                                        备注<input type="text" class="inputtext formsize40" name="txtbeiZhu" />
                                        小计：<input type="text" class="inputtext formsize40" name="txtXiaoJi" valid="required|isMoney" errmsg="*请输入小计费用!|*小计费有误!" />
                                    </td>
                                    <td width="110" align="right">
                                        <a href="javascript:void(0)" class="addbtn">
                                            <img height="20" width="48" src="/images/addimg.gif" alt=""></a>&nbsp; <a href="javascript:void(0)"
                                                class="delbtn">
                                                <img height="20" width="48" src="/images/delimg.gif" alt=""></a>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:Repeater ID="repAPList" runat="server">
                                <ItemTemplate>
                                    <tr class="tempRow">
                                        <td>
                                            景点<秀>名称<select class="inputselect" name="selList" valid="required" errmsg="*请选择用车类型!">
                                                    <option value=",">-请选择-</option> 
                                                    <%# GetList(Eval("AttractionsId").ToString())%>
                                                </select>
                                           
                                            游览日期<input type="text" name="txtvisitTime" id="txtvisitTime" class="inputtext" onfocus="WdatePicker();"
                                                valid="required" errmsg="*请选择游览日期!"  style="width: 65px" value="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("VisitTime"),ProviderToDate) %>"/>
                                            成人数<input type="text" class="inputtext formsize40" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("AdultNumber"))) %>" name="txtadultNums" valid="required|RegInteger" errmsg="*请输入成人数!|*成人数必须是正整数!" />
                                            儿童数<input type="text" class="inputtext formsize40" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("ChildNumber"))) %>" name="txtchildNums" valid="required|RegInteger" errmsg="*请输入儿童数!|*儿童数必须是正整数!" /><br />
                                            成人价<input type="text" class="inputtext formsize40" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("AdultPrice"))) %>" name="txtadultPrices" valid="required|isMoney" errmsg="*请输入成人价!|*成人价有误!" />
                                            儿童价<input type="text" class="inputtext formsize40" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("ChildPrice"))) %>" name="txtchildPrices" valid="required|isMoney" errmsg="*请输入儿童价!|*儿童价有误!" />
                                            家庭价<input type="text" class="inputtext formsize40" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("JTprice"))) %>" name="txtfamilyPrices" valid="required|isMoney" errmsg="*请输入家庭价!|*家庭价有误!" />
                                            席位<input type="text" class="inputtext formsize40" value="<%# Eval("Seats") %>" name="txtseats" />
                                            备注<input type="text" class="inputtext formsize40" value="<%# Eval("BeiZhu") %>" name="txtbeiZhu" />
                                            小计：<input type="text" class="inputtext formsize40" name="txtXiaoJi" valid="required|isMoney"
                                                errmsg="*请输入小计费用!|*小计费有误!" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("SumPrice"))) %>" />
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
                        <asp:TextBox ID="txtGuidNotes" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800" style="height: 60px">

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
                        已安排景点
                        <a href="javascript:void(0);" onclick="ObjPage.QuanBuLuoShi();">全部落实</a>
                    </p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="Objlist">
                    <tr>
                        <th align="" class="border-l">
                            景点公司
                        </th>
                        <th align="center">
                            安排信息
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
                    <asp:Repeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr style="background-color: <%#(EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status")==EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"#dadada":""%>">
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# GetAPMX(Eval("PlanAttractionsList"))%>
                                </td>
                                <td align="right" class="red">
                                    <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%><a
                                        href="javascript:" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
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
                                    <a href='<%# querenUrl %>?planId=<%#Eval("PlanId")%>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:" data-class="updateObj" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" data-sid="<%# Eval("SourceId") %>"
                                            alt="" />修改</a> <a href="javascript:" data-class="deleteObj">
                                                <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:" data-class="showObj" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" data-sid="<%# Eval("SourceId") %>" alt="" />查看</a>
                                    <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="hr_5">
                </div>
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
                var _Url = '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&type=' + ObjPage.type + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + "&iframeId=" + ObjPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&action=delete&PlanId=' + objID + '&TourMode=' + ObjPage._tourMode + '&tourid=' + ObjPage.tourId,
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
            _SaveAttractions: function(planId, tourId) {
                var _url = '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&type=' + ObjPage.type + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&action=save&planId=' + planId + "&TourMode=' + ObjPage._tourMode + '&tourId=" + tourId,
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
                                    ObjPage._SaveAttractions(_planId, ObjPage.tourId);
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
                    var txtadultNums = trClass.find("input[type=text][name='txtadultNums']").val();
                    var txtchildNums = trClass.find("input[type=text][name='txtchildNums']").val();
                    var txtadultPrices = trClass.find("input[type=text][name='txtadultPrices']").val();
                    var txtchildPrices = trClass.find("input[type=text][name='txtchildPrices']").val();
                    var txtfamilyPrices = trClass.find("input[type=text][name='txtfamilyPrices']").val();
                    var TotalPrices = tableToolbar.calculate(tableToolbar.calculate(tableToolbar.calculate(txtadultNums, txtadultPrices, "*"), tableToolbar.calculate(txtchildNums, txtchildPrices, "*"), "+"), txtfamilyPrices, "+");
                    trClass.find("input[type=text][name='txtXiaoJi']").val(TotalPrices);
                    total = tableToolbar.calculate(total, TotalPrices, "+");
                });
                //结算费用
                $("#<%=txtCostAccount.ClientID %>").val(total);
            },
            _BIndBtn: function() {
                //修改
                $("#Objlist").find("[data-class='updateObj']").unbind("click");
                $("#Objlist").find("[data-class='updateObj']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    var suppId = $(this).find("img").attr("data-sid");
                    if (PlanId != "") {
                        window.location.href = '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&action=update&PlanId=' + PlanId + '&suppId=' + suppId + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId;
                    }
                    return false;
                });

                //查看
                $("#Objlist").find("[data-class='showObj']").unbind("click");
                $("#Objlist").find("[data-class='showObj']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    var suppId = $(this).find("img").attr("data-sid");
                    if (PlanId != "") {
                        window.location.href = '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&action=update&PlanId=' + PlanId + '&suppId=' + suppId + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId + "&show=1";
                    }
                    return false;
                });

                //删除
                $("#Objlist").find("[data-class='deleteObj']").unbind("click");
                $("#Objlist").find("[data-class='deleteObj']").click(function() {
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
                $("input[type='text'][name='txtadultNums'],input[type='text'][name='txtchildNums'],input[type='text'][name='txtadultPrices'],input[type='text'][name='txtchildPrices'],input[type='text'][name='txtfamilyPrices'],input[type='text'][name='txtXiaoJi']").blur(function() {
                    ObjPage._TotalPrices();
                });

                //预付申请
                $("#Objlist").find("a[data-class='Prepaid']").unbind("click");
                $("#Objlist").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    ObjPage._OpenBoxy("预付申请", '/Plan/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + ObjPage.type + '&sl=' + ObjPage.sl + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + '&souceName=' + escape(soucesName), "850px", "600px", true);
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=aform.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                        ObjPage._SaveAttractions(planId, ObjPage.tourId);
                    }
                });

            },

            _delCallBackTotalPrices: function() {
                parent.ConfigPage.SetWinHeight();
                ObjPage._TotalPrices();
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

                var supid = $("#<%=SupplierControl1.ClientValue %>").val();
                if (supid != "") {
                    ObjPage._DdlChangeEvent(supid);
                }

                ObjPage._BIndBtn();

                $("#tabAnPaiInfo").autoAdd({ addCallBack: function(tr) { parent.ConfigPage.SetWinHeight(); }, delCallBack: ObjPage._delCallBackTotalPrices });

                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }
            },
            _AjaxReqModel: function(id) {
                if (id) {
                    $.newAjax({
                        type: "POST",
                        url: "/Plan/PlanAttractionsList.aspx?suppId=" + id + "&action=getdata&sl=" + ObjPage.sl + "&TourMode=' + ObjPage._tourMode + '&tourId=" + ObjPage.tourId + "&m=" + new Date().getTime(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.tolist != "") {
                                var info = "";
                                if (ret.tolist.length > 0) {
                                    for (var i = 0; i < ret.tolist.length; i++) {
                                        $("select[name='selList']").append("<option value='" + ret.tolist[i].id + "," + ret.tolist[i].text + "'>" + ret.tolist[i].text + "</option>");
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
                    $("#<%=txtContentName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContentPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContentFax.ClientID %>").val(obj.contactfax);

                    $("select[name='selList'] option").each(function() {
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
                $("#tabAnPaiInfo").find("select[name='selList']").unbind("change");
                $("#tabAnPaiInfo").find("select[name='selList']").change(function() {
                    var seleId = $(this).find("option:selected").val().split(',')[0];
                    var seleName = $(this).find("option:selected").val().split(',')[1];
                    if (seleId != "" && seleName != "") {
                        ObjPage._AjaxReqPrice($(this).closest("tr"), seleId, id, seleName);
                        ObjPage._TotalPrices();
                    }
                });
            },
            _AjaxReqPrice: function(tr, seleId, id, seleName) {
                $.newAjax({
                    type: "POST",
                    url: "/Plan/PlanAttractionsList.aspx?suppId=" + id + "&rid=" + seleId + "&action=getprice&sl=" + ObjPage._sl + "&tourMode=" + ObjPage._tourMode + "&tourId=" + ObjPage._tourID + "&m=" + new Date().getTime(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.tolist != "") {
                            if (ret.tolist.length > 0) {
                                var html = [], mTp = '<%=ProviderMoneyStr %>';
                                html.push('<tr><th width="40px">选择</th><th width="75px">有效时间</th><th width="75px">景点名称</th><th width="75px">结算价</th><th width="75px">儿童价</th><th width="75px">家庭价</th><th width="75px">类型</th></tr>');
                                for (var i = 0; i < ret.tolist.length; i++) {
                                    html.push('<tr><td><input type="radio" value="' + ret.tolist[i].JiaGeJS+"|"+ret.tolist[i].JiaGeET+"|"+ret.tolist[i].JiaGeJT + '" name="radPrice" /></td><td>' + ChangeDateFormat(ret.tolist[i].STime) + '/' + ChangeDateFormat(ret.tolist[i].ETime) + '</td><td>' + seleName + '</td><td>' + mTp + ret.tolist[i].JiaGeJS + '</td><td>' + mTp + ret.tolist[i].JiaGeET + '</td><td>' + mTp + ret.tolist[i].JiaGeJT + '</td><td>' + (ret.tolist[i].BinKeLeiXing == 1 ? "内宾" : "外宾") + '</td></tr>')
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
                                var s=$(this).val().split('|');
                                $.trim(tr.find("input[type='text'][name='txtadultPrices']").val(s[0]));
                                $.trim(tr.find("input[type='text'][name='txtchildPrices']").val(s[1]));
                                $.trim(tr.find("input[type='text'][name='txtfamilyPrices']").val(s[2]));
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
            },
        	//全部落实
        	QuanBuLuoShi:function () {
                var _Url = '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&type=' + ObjPage.type + '&TourMode=' + ObjPage._tourMode + '&tourId=' + ObjPage.tourId + "&iframeId=" + ObjPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanAttractionsList.aspx?sl=' + ObjPage.sl + '&action=quanbuluoshi&type=<%=EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 %>&tourid=' + ObjPage.tourId,
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
