<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiRunFenPei.aspx.cs" Inherits="EyouSoft.Web.Fin.LiRunFenPei" %>

<%@ Register Src="/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>利润分配</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="Form1" runat="server">
    <div class="alertbox-outbox">
        <asp:Panel ID="pan_tourTye" runat="server" Visible="false">
            <div style="width: 99%; margin: auto; text-align: center;">
                <label>
                    <input name="radio" type="radio" value="tour" id="rad_tour" checked="checked" />
                    团队</label>
                <label>
                    <input type="radio" name="radio" id="dingdan" value="order" />
                    订单</label><span id="sp_oId" style="display: none;">：
                        <asp:TextBox ID="txt_OrderList" runat="server" ReadOnly="true"></asp:TextBox><a class="xuanyong"
                            href="javascript:void(0);" id="a_OrderSelect"></a></span></div>
        </asp:Panel>
        <div class="hr_10">
        </div>
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    分配类别 ：
                </td>
                <td width="35%" align="left">
                    <asp:TextBox ID="txt_distributeType" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                </td>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    人员：
                </td>
                <td width="35%" align="left">
                    <asp:Label ID="lbl_uName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    毛利：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txt_Price" CssClass="inputtext formsize80" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    分配金额：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txt_amount" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    净利：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <input type="text" id="txt_cleanAmount" style="width: 65px; padding-left: 2px;" value="0"
                        disabled="disabled" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox ID="txt_remark" runat="server" CssClass="inputtext formsize350" TextMode="MultiLine"
                        Style="height: 40px; margin: 5px 0;"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="baochun"></s>保
                存</a><a href="javascript:void(0);" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var DistributeProfit = {
            DataInit: function() {
                $("#txt_Price").val('<%=EyouSoft.Common.Utils.GetQueryStringValue("Price") %>');
            },
            Save: function(obj) {
                var that = this, url = "LiRunFenPei.aspx?", obj = $(obj);
                obj.unbind("click")
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" })
                obj.html("<s class=baochun></s>  提交中...");
                url += $.param({
                    doType: "Save",
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                })
                if ($(":radio:checked").val() == "order" && that.OrderList.Ids.length <= 0) {
                    parent.tableToolbar._showMsg('请选择订单!');
                    that.BindBtn();
                    return false;
                }

                $.newAjax({
                    type: "post",
                    data: $(":text,:radio,textarea").serialize() + "&" + $.param($.extend({
                        Id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>',
                        TourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                        TourCode: '<%=EyouSoft.Common.Utils.GetQueryStringValue("TourCode") %>',
                        OrderList: $("#<%=txt_OrderList.ClientID%>").val(),
                        Price: '<%=EyouSoft.Common.Utils.GetQueryStringValue("Price") %>'
                    }, that.OrderList)),
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(data) {
                        if (parseInt(data.result) === 1) {
                            parent.tableToolbar._showMsg('提交成功!');
                            setTimeout(function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            }, 1000)
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg);
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                        that.BindBtn();
                    }
                });
                return false;
            },
            BindBtn: function() {
                var that = this;
                var obj = $("#a_Save");
                obj.css({ "background-position": "0 0", "text-decoration": "none" })
                obj.html("<s class=baochun></s>  保 存");
                obj.unbind("click").click(function() {
                    that.Save(this);
                    return false;
                })
            },
            PageInit: function() {
                this.DataInit();
                this.OrderList.Staffs = $.trim($("#<%=lbl_uName.ClientID %>").html());
                $("#dingdan").click(function() {
                    $("#sp_oId").css("display", "")
                })
                $("#rad_tour").click(function() {
                    $("#sp_oId").css("display", "none")
                })

                if ($("#txt_OrderList").val() && $("#txt_OrderList").val().length) {
                    $("#dingdan").click();
                }

                $("#a_OrderSelect").click(function() {
                    parent.Boxy.iframeDialog({
                        iframeUrl: "/Fin/LiRunFenPeiOrderSel.aspx?" +
                        $.param({
                            tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                            callBack: "DistributeProfit.OrderCallBack",
                            pIframeID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>'
                        }),
                        title: "利润分配-订单号选择-",
                        modal: true,
                        width: "820px",
                        height: "420px"
                    });
                })
                $("#<%=txt_amount.ClientID %>").keyup(function() {
                    var thisVal = parseFloat($(this).val()) || 0;
                    var priceVal = parseFloat($("#<%=txt_Price.ClientID %>").val()) || 0;
                    $("#txt_cleanAmount").val(tableToolbar.calculate(priceVal, thisVal, "-"))
                })
                $("#<%=txt_amount.ClientID %>").keyup();
                this.BindBtn();


            },
            OrderList: {
                Ids: "",
                Staffs: ""
            },
            OrderCallBack: function(data) {
                if (data && data.Ids && data.Staffs) {
                    DistributeProfit.OrderList.Ids = data.Ids;
                    $("#<%=txt_OrderList.ClientID%>").val(data.OrderCodes);
                    DistributeProfit.OrderList.Staffs = data.Staffs;
                    $("#<%=lbl_uName.ClientID %>").html(data.Staffs);
                    $("#<%=txt_Price.ClientID %>").val(data.MaoProfit);
                    $("#<%=txt_amount.ClientID %>").keyup();
                }
            }
        }
        $(function() {
            DistributeProfit.PageInit();
        })
    </script>

</body>
</html>

