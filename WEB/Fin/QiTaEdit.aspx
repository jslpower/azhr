<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiTaEdit.aspx.cs" Inherits="EyouSoft.Web.Fin.QiTaEdit" %>

<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc3" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>杂费</title>
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
    <form id="Form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span id="sp_dealTime"></span>：
                    </td>
                    <td width="35%" align="left">
                        <asp:TextBox ID="txt_dealTime" onfocus="WdatePicker();" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span id="sp_feeItem"></span>：
                    </td>
                    <td width="35%" align="left">
                        <asp:TextBox ID="txt_feeItem" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        客户单位：
                    </td>
                    <td width="25%" align="left" bgcolor="#e0e9ef">
                        <uc2:customerunitselect id="CustomerUnitSelect1" isuniqueness="false" runat="server" />
                    </td>
                    <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        销售员：
                    </td>
                    <td width="45%" align="left" bgcolor="#e0e9ef">
                        <uc3:sellsselect id="txt_seller" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span id="sp_feeAmount"></span>：
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txt_feeAmount" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span id="sp_PayType"></span>：
                    </td>
                    <td width="45%" align="left">
                        <asp:DropDownList ID="ddl_PayType" runat="server" CssClass="inputselect">
                            <asp:ListItem>无法获取支付方式</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="tr_dealer"  align="right" height="28">
                    <td class="alertboxTableT" bgcolor="#b7e0f3">
                        请款人：
                    </td>
                    <td align="left" colspan="3">
                        <uc3:sellsselect id="txt_dealer" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" align="right">
                        GST：
                    </td>
                    <td align="left" colspan="3">
                        <asp:CheckBox ID="chkIsTax" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        备注：
                    </td>
                    <td colspan="3" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txt_remark" runat="server" CssClass="inputtext formsize450" Style="height: 50px;"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" id="a_Save" hidefocus="true"><s class="baochun"></s>保
                存</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    </form>

    <script type="text/javascript">
        var Add = {
            privateDataStr: {
                收入: ["收款时间", "收入项目", "收款金额", "支付方式"],
                支出: ["付款时间", "支出项目", "付款金额", "收款方式"]
            },
            Form: null,
            FormCheck: function(obj) {/*提交数据验证*/
                this.Form = $(obj).get(0);
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            CustomerUnitSelectData: {
                ContactName: "<%= ContactName%>",
                ContactPhone: "<%= ContactPhone%>"
            }, /*客户单位数据*/
            CustomerUnitSelectCallBack: function(data) {
                if (data) {
                    Add.CustomerUnitSelectData.ContactName = data.CustomerUnitContactName || "暂无";
                    Add.CustomerUnitSelectData.ContactPhone = data.CustomerUnitContactPhone || "暂无";
                }
            },
            Save: function(obj) {
                var that = this;
                if (that.FormCheck($("form"))) {
                    var obj = $(obj);
                    obj.unbind("click");
                    obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
                    obj.html("<s class=baochun></s>  提交中...");
                    $.newAjax({
                        type: "post",
                        data: $(that.Form).serialize() +
                        "&" + $.param({
                            doType: "Save",
                            OtherFeeID: "<%=OtherFeeID %>",
                            parent: '<%=(int)parent %>',
                            PayTypeName: $("#<%=ddl_PayType.ClientID %> option:selected").text()
                        }) +
                        "&" + $.param(that.CustomerUnitSelectData),
                        cache: false,
                        url: '/Fin/QiTaEdit.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        dataType: "json",
                        success: function(data) {
                            if (parseInt(data.result) === 1) {
                                parent.tableToolbar._showMsg('保存成功!', function() {
                                    window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                    parent.location.href = parent.location.href;
                                });

                            }
                            else {
                                parent.tableToolbar._showMsg(data.msg);
                                that.BindBtn();
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            that.BindBtn();
                        }
                    });
                }
                return false;
            },
            BindBtn: function() {
                var that = this;
                var a_ExamineA = $("#a_Save");
                a_ExamineA.unbind("click");
                a_ExamineA.html("<s class=baochun></s>保 存");
                a_ExamineA.click(function() {
                    that.Save(this);
                    return false;
                })
                a_ExamineA.css("background-position", "0 0")
                a_ExamineA.css("text-decoration", "none")

            },
            InitTitle: function() {/*初始化各种标题*/
                var strArr = this.privateDataStr['<%=parent %>'];
                $("#sp_dealTime").html(strArr[0]);
                $("#sp_feeItem").html(strArr[1]);
                $("#sp_feeAmount").html(strArr[2]);
                $("#sp_PayType").html(strArr[3]);
                if ('<%=parent %>' == "收入") {
                    $("#tr_dealer").remove();
                }
            },
            PageInit: function() {
                var that = this;
                that.InitTitle();
                that.BindBtn();
            }
        }
        $(function() {
            Add.PageInit();
        })
    </script>

</body>
</html>
