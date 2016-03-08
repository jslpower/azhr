<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingFuDeng.aspx.cs" Inherits="EyouSoft.Web.Fin.YingFuDeng" %>
<%@ Import Namespace="EyouSoft.Common" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>付款审核</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <form id="form1" runat="server">
            <div style="margin: auto; text-align: center; font-size: 12px; color: #FF0000; font-weight: bold;
                padding-bottom: 5px;">
                <asp:Label ID="lbl_listTitle" runat="server" Text=""></asp:Label>
            </div>
            </form>
            <div class="hr_10">
            </div>
            <span class="formtableT formtableT02">登记信息</span>
            <table id="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        付款日期
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        请款人
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        请款金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支付方式
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        最晚付款日期
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 120px;">
                        操作
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpt_list">
                    <ItemTemplate>
                        <tr data-registerid="<%#Eval("RegisterId") %>">
                            <td height="28" align="center">
                                <input type="text" class="inputtext formsize80" onfocus="WdatePicker();" name="txt_paymentDate"
                                    value="<%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("PaymentDate"), ProviderToDate)%>"
                                    valid="required" errmsg="请选择付款日期!" />
                            </td>
                            <td align="center" data-dealer="<%#Eval("Dealer")%>" data-dealerid="<%#Eval("DealerId")%>">
                                <span data-class="spanSeller">
                                    <uc1:SellsSelect ID="txt_Sells" runat="server" SetTitle="请款人" />
                                </span>
                            </td>
                            <td align="right">
                                <input type="text" class="inputtext formsize60" name="txt_paymentAmount" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString( (Eval("PaymentAmount")??"0").ToString())%>"
                                    valid="required|isMoney|range" errmsg="请输入请款金额!|请款金额格式不正确!|请款金额必须大于0!" min="0" />
                            </td>
                            <td align="center">
                                <select class="inputselect" name="sel_paymentType" data-paymenttype="<%#Eval("PaymentType") %>">
                                    <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.支出) %>
                                </select>
                            </td>
                            <td align="center">
                                <input type="text" class="inputtext formsize80" onfocus="WdatePicker();" name="txt_deadline"
                                    value="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("Deadline"), ProviderToDate)%>" />
                            </td>
                            <td align="left">
                                <textarea cols="45" rows="5" name="txt_remark" class="inputtext formsize100" style="height: 35px"><%#Eval("Remark")%></textarea>
                            </td>
                            <td align="center" data-status="<%#(int)Eval("Status") %>">
                                <a href="javascript:void(0);" class="a_Updata">修改</a>&nbsp<a class="a_Delete" href="javascript:void(0);">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder ID="phdAdd" runat="server">
                    <tr>
                        <td height="28" align="center">
                            <input type="text" name="txt_paymentDate" class="inputtext formsize80" onfocus="WdatePicker();"
                                valid="required" errmsg="请选择付款日期!" value="<%=EyouSoft.Common.UtilsCommons.GetDateString(DateTime.Now,ProviderToDate) %>" />
                        </td>
                        <td align="center" data-dealer="<%=SiteUserInfo.Name%>" data-dealerid="<%=SiteUserInfo.UserId%>">
                            <span data-class="spanSeller">
                                <uc1:SellsSelect ID="txt_Sells" runat="server" SetTitle="请款人" />
                            </span>
                        </td>
                        <td align="right">
                            <input type="text" class="inputtext formsize60" name="txt_paymentAmount" valid="required|isMoney|range"
                                errmsg="请输入请款金额!|请款金额格式不正确!|请款金额必须大于0!" min="0" />
                        </td>
                        <td align="center">
                            <select class="inputselect" name="sel_paymentType">
                                <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.支出) %>
                            </select>
                        </td>
                        <td align="center">
                            <input type="text" class="inputtext formsize80" onfocus="WdatePicker();" name="txt_deadline"
                                 />
                        </td>
                        <td align="left">
                            <textarea cols="45" rows="5" class="inputtext formsize100" name="txt_remark" style="height: 35px"></textarea>
                        </td>
                        <td align="center">
                            <a href="javascript:void(0);" id="a_Add">添加</a>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var Register = {
            Form: null,
            Save: function(obj, type) {
                var that = this;
                var url = "/Fin/YingFuDeng.aspx?" + $.param({ doType: type, sl: '<%=Request.QueryString["sl"] %>' });
                var obj = $(obj), trobj = obj.closest("tr"), aobjs = obj.closest("td").find("a");
                trobj.find("span[data-class='spanSeller']").attr("valid", "required").attr("errmsg", "请选择请款人!");
                if (!ValiDatorForm.validator(trobj, "parent")) {
                    return false;
                }
                var txt = "";
                aobjs.unbind("click");
                aobjs.css("color", "#A9A9A9");
                switch (type) {
                    case "Delete":
                        that.Form = { registerId: trobj.attr("data-registerid")}/*登记编号*/
                        txt = "删除";
                        obj.text(txt + "中...");
                        break;
                    case "Add":
                        that.Form = trobj.find("input,select,textarea").serialize() + "&" +
                        $.param({
                            planID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("planID") %>',
                            tourID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourID") %>'
                        }); /*普通登记数据*/
                        txt = "添加";
                        obj.text(txt + "中...");
                        break;
                    case "Updata":
                        var sellsFormKey = trobj.find("td:eq(1) input:eq(0)").attr("id").split('_');
                        that.Form = trobj.find("input,select,textarea").serialize() + "&" + $.param(
                        {
                            planID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("planID") %>',
                            tourID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourID") %>',
                            registerId: trobj.attr("data-registerid"),
                            sellsFormKey: sellsFormKey[0] + "_" + sellsFormKey[1] + "_" + sellsFormKey[2] + "_"
                        })

                        /*登记编号 + 普通登记数据*/
                        txt = "修改";
                        obj.text(txt + "中...");
                        break;
                }
                obj.css("color", "#A9A9A9");
                $.newAjax({
                    type: "post",
                    data: that.Form,
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg(txt + '成功!');
                            setTimeout(function() {
                                location.href = location.href;
                            }, 1000)
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg);
                            that.BindBtn();
                        }
                    },
                    error: function() {
                        //ajax异常
                        parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                        that.BindBtn();
                    }
                });

                return false;
            },
            BindBtn: function() {
                var that = this;
                var obj = $("#a_Add");
                obj.css("color", "#1B4F86");
                obj.unbind("click");
                obj.html("添加")
                obj.click(function() {
                    that.Save(this, "Add");
                    return false;
                })
                obj = $(".a_Updata");
                obj.css("color", "#1B4F86");
                obj.unbind("click");
                obj.html("修改")
                obj.click(function() {
                    that.Save(this, "Updata");
                    return false;
                })
                obj = $(".a_Delete")
                obj.css("color", "#1B4F86");
                obj.unbind("click")
                obj.html("删除")
                obj.click(function() {
                    that.Save(this, "Delete");

                    return false;
                })
            },
            PageInit: function() {
                this.BindBtn();
                /*初始化 列表修改 删除按钮*/
                $("#tab_list td[data-status]").each(function() {
                    var intstatus = parseInt($(this).attr("data-status"))
                    if (intstatus == 1 || intstatus == 2) {
                        $(this).children("a").remove();
                    }
                })
                $("select[data-paymenttype]").each(function() {
                    var obj = $(this);
                    obj.val(obj.attr("data-paymenttype"));

                })
                //对收退款人赋值
                $("td[data-dealerid][data-dealer]").each(function() {
                    var obj = $(this);
                    obj.find(":text").val(obj.attr("data-dealer"))

                    obj.find(":hidden").val(obj.attr("data-dealerid"))
                })
            }

        }
        $(function() {
            Register.PageInit();
        })
    </script>

</body>
</html>
