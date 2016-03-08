<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanShoppingList.aspx.cs"
    Inherits="EyouSoft.Web.Plan.PlanShoppingList" %>

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
    <form id="form1" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_5">
        <div action="divfortoggle">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>购物店：
                    </th>
                    <td width="40%">
                        <uc1:suppliercontrol id="SupplierControl1" ismust="true" runat="server" callback="ObjPage._AjaxContectInfo"
                            suppliertype="购物" />
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
                        进店日期：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="inputtext formsize80" onfocus="WdatePicker();"></asp:TextBox>
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
                        已安排购物
                    </p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="Objlist">
                    <tr>
                        <th align="" class="border-l">
                            购物店名称
                        </th>
                        <th align="center">
                            联系人
                        </th>
                        <th align="center">
                            联系电话
                        </th>
                        <th align="center">
                            联系传真
                        </th>
                        <th align="center">
                            进店日期
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
                            <tr>
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%# string.IsNullOrEmpty(Eval("ContactMobile").ToString())? Eval("ContactPhone") :Eval("ContactMobile") %>
                                </td>
                                <td align="center">
                                    <%# Eval("ContactFax")%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetDateString(Eval("StartDate"), "yyyy-MM-dd")%>
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
                                    <a href="javascript:" data-class="updateObj" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" data-sid="<%# Eval("SourceId") %>"
                                            alt="" />修改</a> <a href="javascript:" data-class="deleteObj">
                                                <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:" data-class="showObj" class="untoggle">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />查看</a>
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
    </div>
    </form>

    <script type="text/javascript">
        var ObjPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _DeleteCar: function(objID) {
                var _Url = '/Plan/PlanShoppingList.aspx?sl=' + ObjPage.sl + '&type=' + ObjPage.type + '&tourId=' + ObjPage.tourId + "&iframeId=" + ObjPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/Plan/PlanShoppingList.aspx?sl=' + ObjPage.sl + '&action=delete&PlanId=' + objID + '&tourid=' + ObjPage.tourId,
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
                var _url = '/Plan/PlanShoppingList.aspx?sl=' + ObjPage.sl + '&type=' + ObjPage.type + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/Plan/PlanShoppingList.aspx?sl=' + ObjPage.sl + '&action=save&planId=' + planId + "&tourId=" + tourId,
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
            _BIndBtn: function() {
                //修改
                $("#Objlist").find("[data-class='updateObj']").unbind("click");
                $("#Objlist").find("[data-class='updateObj']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    var suppId = $(this).find("img").attr("data-sid");
                    if (PlanId != "") {
                        window.location.href = '/Plan/PlanShoppingList.aspx?sl=' + ObjPage.sl + '&action=update&PlanId=' + PlanId + '&suppId=' + suppId + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId;
                    }
                    return false;
                });

                //查看
                $("#Objlist").find("[data-class='showObj']").unbind("click");
                $("#Objlist").find("[data-class='showObj']").click(function() {
                    var PlanId = $(this).find("img").attr("data-Id");
                    if (PlanId != "") {
                        window.location.href = '/Plan/PlanShoppingList.aspx?sl=' + ObjPage.sl + '&action=update&PlanId=' + PlanId + '&tourId=' + ObjPage.tourId + '&iframeId=' + ObjPage.iframeId + "&show=1";
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

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=form1.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("PlanId") %>';
                        ObjPage._SaveCar(planId, ObjPage.tourId);
                    }
                });

            },

            _delCallBackTotalPrices: function() {
                parent.ConfigPage.SetWinHeight();
                ObjPage._TotalPrices();
            },

            _InitPage: function() {

                ObjPage._BIndBtn();
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
            ObjPage._InitPage();
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
