<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouPaiBanEdit.aspx.cs" Inherits="EyouSoft.Web.Guide.DaoYouPaiBanEdit" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.PlanStructure" %>

<%@ Register Src="../UserControl/LineSelect.ascx" TagName="LineSelect" TagPrefix="uc1" %>
<%@ Register TagName="Guid" TagPrefix="uc4" Src="~/UserControl/SellsSelect.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" src="/Js/Newjquery.autocomplete.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/ValiDatorForm.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <asp:PlaceHolder ID="phdFrist" runat="server">
            <div style="margin: 0 auto; width: 99%;">
                <div style="padding-left: 20px; font-size: 14px; font-weight: bold; padding-bottom: 5px;
                    height: 20px;">
                    <asp:Label ID="labinfo" runat="server" Text=""></asp:Label>的出团安排明细</div>
                <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center" id="tbList">
                    <tbody>
                        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">团态标识</td>
                            <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                团号
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                线路名称
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                出团时间
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                天数
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                上团时间
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                上团地点
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                下团时间
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                下团地点
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                安排时间
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                结算费用
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                状态
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                操作
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptList">
                            <ItemTemplate>
                                <tr class="<%#Eval("SaleMark") %>">
                                    <td align="center">
                                        <%#Eval("TourMark") %>
                                    </td>
                                    <td height="28" align="center">
                                        <%#Eval("TourCode")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("RouteName")%>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(EyouSoft.Common.Utils.GetDateTime(Eval("LDate").ToString()), ProviderToDate)%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("TourDays")%>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(EyouSoft.Common.Utils.GetDateTime(Eval("OnTime").ToString()), ProviderToDate)%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("OnLocation")%>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(EyouSoft.Common.Utils.GetDateTime(Eval("NextTime").ToString()), ProviderToDate)%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("NextLocation")%>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(EyouSoft.Common.Utils.GetDateTime(Eval("IssueTime").ToString()),ProviderToDate)%>
                                    </td>
                                    <td align="right">
                                        <strong class="fontred">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(EyouSoft.Common.Utils.GetDecimal(Eval("PlanCost").ToString()), ProviderToMoney)%></strong>
                                    </td>
                                    <td align="center">
                                        <%#Eval("PlanState")%>
                                    </td>
                                    <td align="center">
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%#(PlanState)Eval("PlanState")!= PlanState.已落实 %>'>
                                            <a href="/Guide/DaoYouPaiBanEdit.aspx?dotype=update&guideid=<%=EyouSoft.Common.Utils.GetQueryStringValue("guideid") %>&planid=<%#Eval("PlanId") %>&name=<%=EyouSoft.Common.Utils.GetQueryStringValue("name") %>&time=<%=EyouSoft.Common.Utils.GetQueryStringValue("time") %>&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&iframeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>">
                                                修改</a> | <a href="javascript:void(0);" data-class="delGuidPlan" data-delguid="deleteGuid"
                                                    data-planid="<%#Eval("planid") %>">删除</a></asp:PlaceHolder>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </asp:PlaceHolder>
        <div style="padding-left: 20px; font-size: 14px; font-weight: bold; padding-bottom: 5px;
            height: 20px;">
            【<asp:Label ID="lbguidename" runat="server" Text="Label"></asp:Label>】的出团安排</div>
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
              <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">团态标识：</td>
                <td align="left">
                  <%--<input type="radio" name="radio" id="radio" value="radio" style="border:none;"/>
                  <img src="../images/zht1.gif" width="17" height="16" align="middle" />
                  <input type="radio" name="radio" id="radio2" value="radio" style="border:none;"/>
                  <img src="../images/zht2.gif" width="17" height="16" align="middle" />
                  <input type="radio" name="radio" id="radio3" value="radio" style="border:none;"/>
                  <img src="../images/zht3.gif" width="17" height="16" align="middle" />
                  <input type="radio" name="radio" id="radio4" value="radio" style="border:none;"/>
                  <img src="../images/zht4.gif" width="17" height="16" align="middle" />
                  <input type="radio" name="radio" id="radio5" value="radio" style="border:none;"/>
                  <img src="../images/zht5.gif" width="17" height="16" align="middle" />--%>
                   <asp:TextBox ID="txtTourMark" runat="server" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#B7E0F3">销售标识：</td>
                <td align="left"><select name="selSaleMark" id="selSaleMark" runat="server">
                  <option value=""> 请选择 </option>
                  <option value="select_1" class="select_1">红</option>
                  <option value="select_2" class="select_2">橙</option>
                  <option value="select_3" class="select_3">黄</option>
                  <option value="select_4" class="select_4">绿</option>
                  <option value="select_5" class="select_5">青</option>
                  <option value="select_6" class="select_6">蓝</option>
                  <option value="select_7" class="select_7">紫</option>
                </select></td>
              </tr>
            <tr>
                <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    <span class="fontred">*</span>线路名称：
                </td>
                <td align="left">
                    <uc1:LineSelect ID="LineSelect1" runat="server" CallBackFun="GuidInfo.callBackFun"
                        SetTitle="选择线路"  ReadOnly="true"/>
                    <input type="hidden" name="tourID" id="tourID" runat="server" />
                </td>
                <td bgcolor="#B7E0F3" align="right">
                    <span class="alertboxTableT">团号：</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtTourCode" runat="server" class="inputtext formsize120" name="txtTourCode"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    <span class="fontred">*</span>导游姓名：
                </td>
                <td width="44%" bgcolor="#E0E9EF" align="left">
                    <uc4:Guid ID="GuidControl1" runat="server" CallBackFun="GuidInfo._AjaxContectInfo"
                        errmsg="请选择导游!" valid="required" />
                </td>
                <td width="11%" height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                    导游电话：
                </td>
                <td width="31%" height="28" bgcolor="#E0E9EF">
                    <asp:TextBox ID="txttel" runat="server" class="inputtext formsize120"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    <span class="fontred">*</span>上团地点：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStartAddress" runat="server" class="inputtext formsize120" errmsg="请输入上团地点!"
                        valid="required"></asp:TextBox>
                </td>
                <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                    <span class="addtableT"><span class="fontred">*</span> 上团时间：</span>
                </td>
                <td height="28">
                    <asp:TextBox ID="txtDate_Start" name="txtDate_Start" runat="server" onfocus="WdatePicker({onpicked:function(){$('#txtDate_Start').focus();},minDate:'%y-%M-#{%d}'})"
                        class="inputtext formsize120" errmsg="请输入上团时间!" valid="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    <span class="fontred">*</span>下团地点：
                </td>
                <td bgcolor="#E0E9EF" align="left">
                    <asp:TextBox ID="txtEndAddress" runat="server" class="inputtext formsize120" errmsg="请输入下团地点!"
                        valid="required"></asp:TextBox>
                </td>
                <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                    <span class="fontred">*</span>下团时间：
                </td>
                <td height="28" bgcolor="#E0E9EF">
                    <asp:TextBox ID="txtDate_End" name="txtDate_End" runat="server" class="inputtext formsize120"
                        onfocus="WdatePicker({onpicked:function(){$('#txtDate_End').focus();},minDate:'%y-%M-#{%d}'})"
                        errmsg="请输入下团时间!" valid="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    <span class="fontred">*</span>任务类型：
                </td>
                <td height="28" align="left" colspan="3">
                    <asp:DropDownList ID="ddltype" runat="server" CssClass="inputselect">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    行程接待：
                </td>
                <td height="28" bgcolor="#E0E9EF" align="left" colspan="5">
                    <asp:TextBox TextMode="MultiLine" ID="txttravel" name="txttravel" Style="height: 35px;"
                        class="inputtext formsize600" Rows="6" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    服务标准：
                </td>
                <td height="28" align="left" colspan="5">
                    <asp:TextBox TextMode="MultiLine" ID="txtService" name="txtService" Style="height: 35px;"
                        class="inputtext formsize600" Rows="6" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    其他备注：
                </td>
                <td height="28" bgcolor="#E0E9EF" align="left" colspan="5">
                    <asp:TextBox TextMode="MultiLine" ID="txtrmark" name="txtrmark" Style="height: 35px;"
                        Rows="6" runat="server" class="inputtext formsize600"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    费用明细：
                </td>
                <td height="28" align="left" colspan="5">
                    <asp:TextBox TextMode="MultiLine" ID="txtCostDesc" name="txtCostDesc" Style="height: 35px;"
                        Rows="6" runat="server" class="inputtext formsize600"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    <span class="fontred">*</span>结算费用：
                </td>
                <td height="28" bgcolor="#E0E9EF" align="left">
                    <asp:TextBox ID="txtPaidCost" name="txtPaidCost" class="inputtext formsize120" runat="server"
                        errmsg="请输入结算费用!|结算费用有误!" valid="required|isMoney"></asp:TextBox>
                </td>
                <td height="28" bgcolor="#B7E0F3" align="right">
                    <span class="fontred">*</span>状态：
                </td>
                <td height="28" bgcolor="#E0E9EF" align="left">
                    <asp:DropDownList ID="ddlState" name="selectState" runat="server" CssClass="inputselect">
                        <asp:ListItem Value="3">未落实</asp:ListItem>
                        <asp:ListItem Value="4">已落实</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <a hidefocus="true" href="javascript:void(0);" id="BtnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <input type="hidden" id="hdplantype" runat="server" />
    <input type="hidden" runat="server" id="hdguideid" />
    </form>

    <script type="text/javascript">
        var GuidInfo = {
            callBackFun: function(obj) {
                $("#<%=txtTourCode.ClientID %>").val(obj.tourcode);
                $("#<%=txtDate_Start.ClientID %>").val(obj.ldate);
                $("#<%=txtDate_End.ClientID %>").val(obj.rdate);
                $("#<%=LineSelect1.LineNameClient %>").val(obj.text);
                $("input[type='hidden'][name='tourID']").val(obj.tourID);
            },
            _AjaxContectInfo: function(obj) {
                $('#<%=GuidControl1.SellsIDClient %>').val(obj.value);
                $('#<%=GuidControl1.SellsNameClient %>').val(obj.text);
                $("#<%=txttel.ClientID %>").val(obj.phone);
            },
            PageInit: function() {
                if ($("#<%=hdplantype.ClientID %>").val() == "停职") {
                    $("#BtnSave").html("已停职");
                    $("#BtnSave").unbind("click");
                }
                if ($("#<%=hdplantype.ClientID %>").val() == "假期") {
                    $("#BtnSave").html("请假中");
                    $("#BtnSave").unbind("click");
                }
                this.BindBtn();
            },
            BindBtn: function() {
                $("#BtnSave").html('<s class="baochun"></s>保存').bind("click").css("background-position", "0 0px");
                $("#BtnSave").click(function() {
                    if (ValiDatorForm.validator($("#BtnSave").closest("form").get(0), "alert")) {
                        $("#BtnSave").html("提交中...").unbind("click").css("background-position", "0 -55px");
                        GuidInfo.GoAjax('/Guide/DaoYouPaiBanEdit.aspx?guideid=<%=EyouSoft.Common.Utils.GetQueryStringValue("guideid") %>&dotype=save&name=<%=EyouSoft.Common.Utils.GetQueryStringValue("name") %>&planid=<%=EyouSoft.Common.Utils.GetQueryStringValue("planid") %>&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>');
                    }
                    return false;
                });
            },
            DeleteGuid: function(planid) {
                var _Url = '/Guide/DaoYouPaiBanEdit.aspx?type=<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>&time=<%=EyouSoft.Common.Utils.GetQueryStringValue("time") %>&name=<%=EyouSoft.Common.Utils.GetQueryStringValue("name") %>&guideid=<%=EyouSoft.Common.Utils.GetQueryStringValue("guideid") %>&&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                $.newAjax({
                    type: "get",
                    url: "/Guide/DaoYouPaiBanEdit.aspx?dotype=delete&planid=" + planid + '&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    cache: false,
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                        }
                        else {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
                return false;
            },
            GoAjax: function(url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    data: $("#BtnSave").closest("form").serialize(),
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                parent.window.location.href = parent.location.href;
                            });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg, function() {
                                GuidInfo.BindBtn();
                            });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                            GuidInfo.BindBtn();
                        });
                    }
                });
            }
        }

        $(function() {
            GuidInfo.PageInit();
            //删除
            $("#tbList").find("a[data-Class='delGuidPlan']").click(function() {
                var _Id = $(this).attr("data-planid");
                if (_Id) {
                    GuidInfo.DeleteGuid(_Id);
                }
                return false;
            })
        });
       
        
    </script>

</body>
</html>

