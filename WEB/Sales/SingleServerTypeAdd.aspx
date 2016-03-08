<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleServerTypeAdd.aspx.cs"
    Inherits="EyouSoft.Web.Sales.SingleServerTypeAdd" %>

<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script type="text/javascript" src="/js/datepicker/wdatepicker.js"></script>

    <script type="text/javascript" src="/js/validatorform.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/js/jquery.blockuI.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table id="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd">
            <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    <font color="#FF0000">*</font>供应商类别
                </td>
                <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width160px">
                    <font color="#FF0000">*</font>供应商名称
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    具体安排
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    数量
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width80px">
                    <font color="#FF0000">*</font>结算费用
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    支付方式
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    备注
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    计调状态
                </td>
                <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="repAycentylist" runat="server">
                <ItemTemplate>
                    <tr data-registerid="<%#Eval("PlanId") %>">
                        <td align="center">
                            <input type="hidden" name="PLanId" value="<%#Eval("PlanId") %>" />
                            <select name="ddlType">
                                <option selected="selected" value="<%=Utils.GetQueryStringValue("tp") %>">
                                    <%=(EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt( Utils.GetQueryStringValue("tp"))%></option>
                            </select>
                        </td>
                        <td height="28" align="center" data-dealer="<%#Eval("SourceName")%>" data-dealerid="<%#Eval("SourceId")%>">
                            <uc1:CustomerUnitSelect runat="server" ID="CustomerUnitSelect1" IsApply="false" IsUniqueness="false"
                                SelectFrist="false" />
                        </td>
                        <td align="center">
                            <textarea name="txtinfo" class="bk pand4" id="txtinfo" style="height: 35px; width: 130px;"><%# EyouSoft.Common.Utils.GetText(Eval("GuideNotes").ToString(),30) %> </textarea>
                        </td>
                        <td align="center">
                            <input name="txtnum" type="text" id="txtnum" style="width: 20px;" class="input-txt  vartext"
                                 onpaste="return false" value="<%# Eval("Num")%>" />
                        </td>
                        <td align="center">
                            <input name="txtcost" type="text" id="textfield82" class="formsize80  input-txt  vartext"
                                 onpaste="return false" value=" <%#  Convert.ToDecimal(Eval("Confirmation")).ToString("0.00")%>" />
                        </td>
                        <td align="center">
                            <select name="payStateDDL">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)Eval("PaymentType")).ToString(), false)%>
                            </select>
                        </td>
                        <td align="center">
                            <span>
                                <textarea name="txtremark" class="bk pand4" id="txtremark" style="height: 30px; width: 130px;"><%#  Eval("Remarks").ToString() %></textarea>
                            </span>
                        </td>
                        <td align="center">
                            <select name="planStateDDL">
                                <%#   getPlanState((EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status"))%>
                            </select>
                        </td>
                        <td align="center">
                            <%if (ListPower)
                              { %>
                            <%# GetOPThtml( Eval("PlanId").ToString())%>
                            <%}%>
                            <a target="_blank" href="<%# GetPrintURL(Eval("PlanId").ToString())%>">确认单</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder ID="placeTemlTR" runat="server">
                <tr class="tempRow">
                    <td align="center">
                        <select name="ddlType">
                            <option selected="selected" value="<%=Utils.GetQueryStringValue("tp") %>">
                                <%=(EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt( Utils.GetQueryStringValue("tp"))%></option>
                        </select>
                    </td>
                    <td height="50" align="center">
                        <span data-class="spanSeller">
                            <uc1:CustomerUnitSelect runat="server" ID="CustomerUnitSelect1" IsApply="false" IsUniqueness="false"
                                SelectFrist="false" />
                        </span>
                        <input type="hidden" value="" id="hdContactdepartid" runat="server" />
                    </td>
                    <td height="50" align="center">
                        <textarea name="txtinfo" class="bk pand4" id="txtinfo" style="height: 35px; width: 130px;"></textarea>
                    </td>
                    <td align="center">
                        <input name="txtnum" type="text" id="txtnum" style="width: 20px;" class="input-txt  vartext"
                             onpaste="return false" />
                    </td>
                    <td height="50" align="center">
                        <input name="txtcost" type="text" id="textfield82" class="formsize140  input-txt  vartext"
                             onpaste="return false" />
                    </td>
                    <td align="center">
                        <select name="payStateDDL">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "1", false)%>
                        </select>
                    </td>
                    <td height="50" align="center">
                        <textarea name="txtremark" class="bk pand4" id="txtremark" style="height: 30px; width: 130px;"></textarea>
                    </td>
                    <td height="50" align="center">
                        <select name="planStateDDL">
                            <option value="3">未落实</option>
                            <option value="4">已落实</option>
                        </select>
                    </td>
                    <td align="center">
                        <%if (ListPower)
                          { %>
                        <a href="javascript:void(0);" id="a_Add" data-id="">添加</a>
                        <%} %>
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:;" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var Register = {
            tp: '<%=Utils.GetQueryStringValue("tp") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            Save: function(obj, type, pid) {
                var that = this;
                var url = "/Sales/SingleServerTypeAdd.aspx?" + $.param({ doType: type, sl: '<%=Request.QueryString["sl"] %>', tourId: Register.tourId, planid: pid, tp: Register.tp });
                var obj = $(obj), trobj = obj.closest("tr"), aobjs = obj.closest("td").find("a");
                var msg = "";
                trobj.find(".vartext").each(function() {
                    if ($(this).val() == "") {
                        msg += "1";
                    }
                });
                if (msg != "") {
                    parent.tableToolbar._showMsg("请核查信息是否完整！");
                    return false;
                }
                var txt = "";
                aobjs.unbind("click");
                aobjs.css("color", "#A9A9A9");
                switch (type) {
                    case "Delete":
                        that.Form = { planid: trobj.attr("data-registerid")}/*登记编号*/
                        txt = "删除";
                        obj.text(txt + "中...");
                        break;
                    case "Add":
                        that.Form = trobj.find("input,select,textarea").serialize() + "&" +
                        $.param({

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
                            sellsFormKey: sellsFormKey[1] + "_" + sellsFormKey[2] + "_"
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
                        if (parseInt(ret.result) == 1) {
                            parent.tableToolbar._showMsg(ret.msg);
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
                    that.Save(this, "Add", $(this).attr("data-id"));
                    return false;
                })
                obj = $(".a_Updata");
                obj.css("color", "#1B4F86");
                obj.unbind("click");
                obj.html("修改")
                obj.click(function() {
                    that.Save(this, "Updata", $(this).attr("data-id"));
                    return false;
                })
                obj = $(".a_Delete")
                obj.css("color", "#1B4F86");
                obj.unbind("click")
                obj.html("删除")
                obj.click(function() {
                    that.Save(this, "Delete", $(this).attr("data-id"));

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

    </form>
</body>
</html>
