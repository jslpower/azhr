<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanAyencyList.aspx.cs"
    Inherits="EyouSoft.Web.Plan.PlanAyencyList" %>

<%@ Register Src="/UserControl/SupplierControl.ascx" TagName="supplierControl" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

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
    <form id="formAyency" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_1">
        <div action="divfortoggle">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>地接社名称：
                    </th>
                    <td width="40%">
                            <uc1:supplierControl ID="supplierControl1" runat="server" CallBack="AyencyPage._AjaxContectInfo" IsMust="true" flag="0" SupplierType="地接社" />
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT">联系人：</span>
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtContentName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="15%" align="right" class="border-l">
                        联系电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContentPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT">联系传真：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtContentFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="15%" align="right" class="border-l">
                        联系手机：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtContactMobile" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        接待行程：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtTravel" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800" style="height: 60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        服务标准：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtServerStand" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800" style="height: 60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        用房数：
                    </th>
                    <td colspan="3">
                        <%=GetRoomList(TourId) %>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="addtableT"><span class="fontred">*</span> 接团日期：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="inputtext formsize100" valid="required"
                            errmsg="*请输入接团时间!"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>送团日期：
                    </th>
                    <td>
                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="inputtext formsize100" valid="required"
                            errmsg="*请输入送团时间!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>结算费用：
                    </th>
                    <td>
                        <asp:TextBox ID="txtCostAccount" runat="server" CssClass="inputtext formsize40" valid="required|isMoney"
                            errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:TextBox>
                        元
                    </td>
                    <th align="right">
                        <span class="border-l"><span class="fontred">*</span>支付方式：</span>
                    </th>
                    <td>
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
                        人数：
                    </th>
                    <td>
                        成人
                        <asp:TextBox ID="txtAdultNumber" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        儿童
                        <asp:TextBox ID="txtChildNumber" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        领队
                        <asp:TextBox ID="txtLeaderNumber" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span><span class="border-l">状态</span>：
                    </th>
                    <td align="left">
                        <select class="inputselect" name="states">
                            <asp:Literal ID="litOperaterStatus" runat="server"></asp:Literal>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        导游需知：
                    </th>
                    <td colspan="3">
                        <textarea name="txtguidNotes" id="txtguidNotes" runat="server" class="inputtext formsize800" style="height: 60px">
地接社：
接团时间：
送团时间：
地接导游：
服务电话：
                           </textarea>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        备注：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="inputtext formsize800" style="height: 60px"></asp:TextBox>
                    </td>
                </tr>
            </table>
                <div class="hr_5">
                </div>
        </div>
            <asp:PlaceHolder ID="panView" runat="server">
                <div class="mainbox cunline fixed" action="divfortoggle">
                    <ul id="ul_AyencyBtn">
                        <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a></li>
                    </ul>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排地接</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="actionType">
                    <tr>
                        <th align="center" class="border-l">
                            地接社名称
                        </th>
                        <th align="center">
                            接团时间
                        </th>
                        <th align="center">
                            送团时间
                        </th>
                        <th align="center">
                            人数
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
                    <asp:Repeater ID="repAycentylist" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("StartDate") != null ? Convert.ToDateTime(Eval("StartDate")).ToString("yyyy-MM-dd") : ""%>
                                </td>
                                <td align="center">
                                    <%# Eval("EndDate") != null ? Convert.ToDateTime(Eval("EndDate")).ToString("yyyy-MM-dd") : ""%>
                                </td>
                                <td align="center">
                                    <%# Eval("Num")%>
                                </td>
                                <td align="right">
                                    <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                    <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>" data-soucesid="<%# Eval("SourceId")%>">
                                        <img src="/images/yufu.gif" alt="" /></a>
                                </td>
                                <td align="center">
                                    <b class="fontred"><%#Eval("Prepaid", "{0:C2}")%><%--<%#GetYFJE(0,Eval("PlanId").ToString())%>--%></b>
                                </td>
                                <td align="center">
                                    <b class="fontblue"><%#(Convert.ToDecimal(Eval("Confirmation")) - Convert.ToDecimal(Eval("Prepaid"))).ToString("C2")%><%--<%#GetYFJE(Convert.ToDecimal(Eval("Confirmation")),Eval("PlanId").ToString())%>--%></b>
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
                                        <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:void(0);" data-class="updateAyency" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                    <a href="javascript:void(0);" data-class="delAyency">
                                        <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:void(0);" data-class="ShowAyency" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                    <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:PlaceHolder>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var AyencyPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            //删除
            _DeleteAyency: function(_ObjID) {
                var _Url = '/Plan/PlanAyencyList.aspx?type=' + AyencyPage.type + '&sl=' + AyencyPage.sl + '&tourId=' + AyencyPage.tourId + '&iframeId=' + AyencyPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanAyencyList.aspx?sl=' + AyencyPage.sl + '&action=delete&PlanId=' + _ObjID + '&tourid=' + AyencyPage.tourId,
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
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _SaveAyency: function(planID, TourId) {
                var _Url = '/Plan/PlanAyencyList.aspx?type=' + AyencyPage.type + '&sl=' + AyencyPage.sl + '&tourId=' + AyencyPage.tourId + '&iframeId=' + AyencyPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/Plan/PlanAyencyList.aspx?action=save&sl=' + AyencyPage.sl + '&PlanID=' + planID + "&tourId=" + TourId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    async: false,
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave").css("background-position", "0 0px").text("保存").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).css("background-position", "0 -62px").text("保存中...").unbind("click");
                                    var _planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                                    AyencyPage._SaveAyency(_planId, AyencyPage.tourId);
                                });
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                            window.location.href = _Url;
                        });
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                //预付申请
                $("#actionType").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    var soucesId= $(this).attr("data-soucesid");
                    AyencyPage._OpenBoxy("预付申请", '/Plan/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + AyencyPage.type + '&sl=' + AyencyPage.sl + '&tourId=' + AyencyPage.tourId + '&soucesId=' + escape(soucesId) + '&souceName=' + escape(soucesName), "850px", "600px", true);
                    return false;
                });

                //删除
                $("#actionType").find("a[data-class='delAyency']").unbind("click");
                $("#actionType").find("a[data-class='delAyency']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var tourId = newThis.find("img").attr("data-ID");
                        if (tourId) {
                            AyencyPage._DeleteAyency(tourId);
                        }
                    });
                    return false;
                });

                //修改
                $("#actionType").find("a[data-class='updateAyency']").unbind("click");
                $("#actionType").find("a[data-class='updateAyency']").click(function() {
                    var PlanId = $(this).find("img").attr("data-ID");
                    if (PlanId) {
                        window.location.href = '/Plan/PlanAyencyList.aspx?type=' + AyencyPage.type + '&sl=' + AyencyPage.sl + '&tourId=' + AyencyPage.tourId + '&action=update&PlanId=' + PlanId + '&iframeId=' + AyencyPage.iframeId;
                    }
                    return false;
                });

                //查看
                $("#actionType").find("a[data-class='ShowAyency']").unbind("click");
                $("#actionType").find("a[data-class='ShowAyency']").click(function() {
                    var PlanId = $(this).find("img").attr("data-ID");
                    if (PlanId) {
                        window.location.href = '/Plan/PlanAyencyList.aspx?type=' + AyencyPage.type + '&sl=' + AyencyPage.sl + '&tourId=' + AyencyPage.tourId + '&action=update&PlanId=' + PlanId + '&iframeId=' + AyencyPage.iframeId + "&show=1";
                    }
                    return false;
                });

                //保存 取消绑定事件
                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=formAyency.ClientID %>").get(0), "parent")) {
                        return;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        //计调项id 团号
                        var planID = '<%=Utils.GetQueryStringValue("PlanId") %>';
                        AyencyPage._SaveAyency(planID, AyencyPage.tourId);
                    }
                });

                $("#<%=txtStartTime.ClientID %>").focus(function() { WdatePicker(); });
                $("#<%=txtEndTime.ClientID %>").focus(function() { WdatePicker(); });
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

            },
            _PageInit: function() {
                this._BindBtn();

                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_AyencyBtn").parent("div").hide();
                }
            },
            _AjaxContectInfo: function(obj) { //联系人 电话 传真 手机
                if (obj) {
                    $("#<%=supplierControl1.ClientText %>").val(obj.name);
                    $("#<%=supplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=txtContentName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContentPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContentFax.ClientID %>").val(obj.contactfax);
                    $("#<%=txtContactMobile.ClientID %>").val(obj.contactmobile);
                }
            }
        }

        $(function() {
            AyencyPage._PageInit();
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
