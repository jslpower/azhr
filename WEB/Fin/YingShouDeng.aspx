<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingShouDeng.aspx.cs" Inherits="EyouSoft.Web.Fin.YingShouDeng" %>
<%@ Import Namespace="EyouSoft.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收款</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
    body,html{background:#e9f4f9;}
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: auto; text-align: center; font-size: 12px; font-weight: bold;
            padding-bottom: 5px;">
            <asp:Label ID="lbl_listTitle" runat="server" Text=""></asp:Label>
        </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 0 auto">
            <tr>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">序号</span>
                </td>
                <td align="center" bgcolor="#B7E0F3">是否委托代收</td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT"><span data-class="sp_title"></span>日期</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT"><span data-class="sp_title"></span>人</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT"><span data-class="sp_title"></span>金额</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT"><span data-class="sp_title"></span>方式</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    备注
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3" style="width: 150px">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <tr data-id="<%#Eval("Id") %>">
                        <td height="28" align="center">
                            <%#Container.ItemIndex+1 %>
                        </td>
                        <td align="center"><input name="chkDaiShou" type="checkbox" <%#(bool)Eval("IsDaiShou")? "checked='checked'":""%>  />代收人<input name="txtDaiShouRen" type="text" class="formsize80 input-txt" value="<%#Eval("DaiShouRen") %>"/></td>
                        <td height="28" align="center">
                            <input type="text" class="inputtext formsize80" onfocus="WdatePicker();" name="txt_collectionRefundDate"
                                value="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("CollectionRefundDate"),ProviderToDate) %>" />
                        </td>
                        <td height="28" align="center" data-sellsid="<%#Eval("CollectionRefundOperatorID") %>"
                            data-sellsnameclient="<%#Eval("CollectionRefundOperator") %>">
                            <uc1:SellsSelect ID="txt_Sells" runat="server" SetTitle="收款人" />
                        </td>
                        <td height="28" align="center">
                            <input type="text" size="10" class="inputselect" name="txt_collectionRefundAmount"
                                value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("CollectionRefundAmount")) %>" />
                        </td>
                        <td height="28" align="center">
                            <select name="sel_collectionRefundMode" class="inputselect" data-value="<%#Eval("CollectionRefundMode") %>">
                                <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, ShouTuiType =="1" ? EyouSoft.Model.EnumType.ComStructure.ItemType.收入 : EyouSoft.Model.EnumType.ComStructure.ItemType.支出)%>
                            </select>
                        </td>
                        <td height="28" align="center">
                            <textarea name="txt_Memo" class="inputtext formsize150" style="height: 25px;"><%#Eval("Memo")%></textarea>
                        </td>
                        <td height="28" align="center" data-isguiderealincome="<%#Eval("IsGuideRealIncome").ToString().ToLower() %>">
                            <%--<a class="a_Updata" href="javascript:void(0);">修改</a>&nbsp;<a data-ischeck="<%#<%#Eval("IsCheck").ToString().ToLower() %>"
                                class="a_ExamineV" href="javascript:void(0);">审核 </a>&nbsp;<a class="a_Delete" href="javascript:void(0);">
                                    删除</a><a class="a_InAccount" href="javascript:void(0);">财务入帐</a>--%>
                                    <%#GetCaoZuoHTML((bool)Eval("IsCheck"), (bool)Eval("IsGuideRealIncome"), Eval("Id"),Eval("ShouKuanType"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td height="28" align="center">
                    -
                </td>
                <td align="center"><input name="chkDaiShou" type="checkbox"/>代收人<input name="txtDaiShouRen" type="text" class="formsize80 input-txt"/></td>
                <td height="28" align="center">
                    <input type="text" class="inputtext formsize80" onfocus="WdatePicker();" name="txt_collectionRefundDate"
                        value="<%=EyouSoft.Common.UtilsCommons.GetDateString( DateTime.Now,ProviderToDate) %>" />
                </td>
                <td height="28" align="center">
                    <uc1:SellsSelect ID="txt_Sells" runat="server" SetTitle="收付款人" />
                </td>
                <td height="28" align="center">
                    <input type="text" size="10" name="txt_collectionRefundAmount" />
                </td>
                <td height="28" align="center">
                    <select name="sel_collectionRefundMode" class="inputselect">
                        <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, ShouTuiType=="1" ? EyouSoft.Model.EnumType.ComStructure.ItemType.收入 : EyouSoft.Model.EnumType.ComStructure.ItemType.支出)%>
                    </select>
                </td>
                <td height="28" align="center">
                    <textarea name="txt_Memo" id="textarea" class="inputtext formsize150" style="height: 25px;"></textarea>
                </td>
                <td height="28" align="center">
                    <%=GetTianJiaHTML()%>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" onclick="window.parent.location.reload();" hidefocus="true">
                <s class="chongzhi"></s>关 闭</a></div>
    </div>
</body>

<script type="text/javascript">
    var PageJsDataObj = {
        //提交
        Submit: function(obj, type) {
            var that = this;
            var _params = { 'sl': '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                , 'ParentType': '<%=RequestType %>'
                , 'ReturnOrSet': '<%=ShouTuiType %>'
                , "doType": type
            };
            var url = Boxy.createUri('/Fin/YingShouDeng.aspx', _params);
            var obj = $(obj);
            var trobj = obj.closest("tr");
            var txt = obj.text()
            obj.unbind("click");
            obj.css("color", "#A9A9A9");
            obj.text(txt + "中...");
            var sellsFormKey = trobj.find("td:eq(3) input:eq(0)").attr("id").split('_');
            var data = {
                OrderId: Boxy.queryString("OrderId"),
                TourOrderSalesID: trobj.attr("data-id"),
                sellsFormKey: sellsFormKey[0] + "_" + sellsFormKey[1] + "_" + sellsFormKey[2] + "_"
            }
            var msg = "";
            if ($.trim(trobj.find("input[name='txt_collectionRefundDate']").val()).length == 0) {
                msg += that._title[parseInt('<%=ShouTuiType %>')] + "日期不能为空<br/>";
            }
            if ($.trim(trobj.find("input:hidden").val()).length <= 0) {
                msg += that._title[parseInt('<%=ShouTuiType %>')] + "人不能为空<br/>";
            }
            if ($.trim(trobj.find("input[name='txt_collectionRefundAmount']").val()).length <= 0) {
                msg += that._title[parseInt('<%=ShouTuiType %>')] + "金额不能为空<br/>";
            }
            if (msg.length > 0) {
                parent.tableToolbar._showMsg(msg);
                that.InitListBtn();
                return false;
            }
            $.newAjax({
                type: "post",
                data: $.param(data) + "&" + trobj.find("input,textarea,select").serialize(),
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
                        that.InitListBtn();
                    }
                },
                error: function() {
                    parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                    that.InitListBtn();
                }
            });

            return false;
        },
        InitListBtn: function() {/*初始化列表按钮*/
            var that = this;
            var obj = $("#AddSave");
            obj.css("color", "");
            obj.text("添加");
            obj.unbind("click").click(function() {
                that.Submit(this, "Add")
                return false;
            })
            obj = $(".a_Updata");
            obj.css("color", "");
            obj.text("修改");
            obj.unbind("click").click(function() {
                that.Submit(this, "Updata");
                return false;
            })
            obj = $(".a_Delete");
            obj.css("color", "");
            obj.text("删除");
            obj.unbind("click").click(function() {
                that.Submit(this, "Delete");
                return false;
            })
            obj = $(".a_ExamineV");
            obj.css("color", "");
            obj.text("审核");
            obj.unbind("click").click(function() {
                //parent.tableToolbar.ShowConfirmMsg("审核确认?审核成功后将无法进行修改!", function() {
                that.Submit(this, "ExamineV");
                //})
                return false;
            })
            obj = $(".a_InAccount");
            obj.css("color", "");
//            obj.text("财务入账");
            obj.unbind("click").click(function() {
                parent.Boxy.iframeDialog({
                    iframeUrl: "/FinanceManage/Common/InAccount.aspx?" + $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        KeyId: $(this).closest("tr").attr("data-id"),
                        DefaultProofType: Boxy.queryString("DefaultProofType"),
                        tourId: Boxy.queryString("tourId")
                    }),
                    title: "财务入账",
                    modal: true,
                    width: "900px",
                    height: "500px",
                    draggable: true
                });
            })
        },
        _title: ["", "收款", "退款"],
        PageInit: function() {
            var that = this;
            //设置标题
            $("span[data-class='sp_title']").html(that._title[parseInt('<%=ShouTuiType %>')]);
            this.InitListBtn();
            //对收退款人赋值
            $("td[data-sellsid][data-sellsnameclient]").each(function() {
                var obj = $(this);
                obj.find("[type='text']").val(obj.attr("data-sellsnameclient"))
                obj.find("[type='hidden']").val(obj.attr("data-sellsid"))
            });

            $("select[data-value]").each(function() {
                var obj = $(this);
                obj.val(obj.attr("data-value"));
            });
        }

    }
    $(function() {
        PageJsDataObj.PageInit();
    })
</script>

</html>
