<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcceptPlanApply.aspx.cs"
    Inherits="EyouSoft.WebFX.AcceptPlanApply" %>

<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/PriceStand.ascx" TagName="PriceStand" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/TravelControl.ascx" TagName="TravelControl" TagPrefix="uc4" %>
<%@ Register Src="~/UserControl/TravelControlS.ascx" TagName="TravelControlS" TagPrefix="uc7" %>
<%@ Register Src="~/UserControl/SetSeat.ascx" TagName="SetSeat" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>分销商收客计划-报名</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" ProcductClass="default skjhicon" />
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div class="hr_10">
    </div>
    <div class="list-maincontent">
        <div class="listtablebox">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td width="77" bgcolor="#DCEFF3" align="right" class="addtableT">
                            订单号：
                        </td>
                        <td width="308" class="kuang2">
                            <label id="lblOrderCode">
                            </label>
                        </td>
<%--                        <td width="104" bgcolor="#DCEFF3" align="right" class="addtableT">
                            <font class="fontbsize12">*</font> 客源单位：
                        </td>
                        <td width="270" class="kuang2">
                            <input type="text" id="txtDCompanyName" name="txtDCompanyName" class="searchInput size170"
                                valid="required" errmsg="请填写客源单位" />
                        </td>
--%>                        <td width="100" bgcolor="#DCEFF3" align="right" class="addtableT">
                            <font class="fontbsize12">*</font> 联系人：
                        </td>
                        <td width="135" class="kuang2" colspan="3">
                            <input type="text" id="txtDContactName" name="txtDContactName" class="searchInput size68"
                                valid="required" errmsg="请填写客源单位联系人" />
                        </td>
                    </tr>
                    <tr>
                        <td width="77" bgcolor="#DCEFF3" align="right" class="addtableT">
                            <font class="fontbsize12">*</font> 联系电话：
                        </td>
                        <td class="kuang2">
                            <input type="text" id="txtDContactTel" name="txtDContactTel" class="searchInput size70"
                                valid="required" errmsg="请填写客源单位联系电话" />
                        </td>
                        <td width="104" bgcolor="#DCEFF3" align="right" class="addtableT">
                            <span class="kuang2"><font class="fontbsize12">*</font> 订单销售员：</span>
                        </td>
                        <td class="kuang2" colspan="3">
                            <%=SiteUserInfo.Name %>
                        </td>
                    </tr>
                    <tr>
                        <td width="77" bgcolor="#DCEFF3" align="right" class="addtableT">
                            下单人：
                        </td>
                        <td class="kuang2">
                            <%=SiteUserInfo.Name%>
                        </td>
                        <td width="104" bgcolor="#DCEFF3" align="right" class="addtableT">
                            人数：
                        </td>
                        <td colspan="3" class="kuang2">
                            <img width="16" height="15" style="vertical-align: middle" src="/Images/chengren.gif" />成人
                            <input type="text" id="txtAdults" name="txtAdults" class="searchInput size40" valid="required|RegInteger"
                                errmsg="请填写订单成人数|人数必须是正整数" />
                            &nbsp;
                            <img style="vertical-align: middle" src="/Images/child.gif" />
                            儿童
                            <input type="text" id="txtChilds" name="txtChilds" class="searchInput size40" valid="RegInteger"
                                errmsg="人数必须是正整数" />
                        </td>
                    </tr>
                    <tr>
                        <td width="77" bgcolor="#DCEFF3" align="right" class="addtableT">
                            <font class="fontbsize12">*</font> 价格组成：
                        </td>
                        <td class="kuang2" colspan="5">
                            <uc3:PriceStand ID="PriceStand1" runat="server" />
                            <input id="_hstandard" type="hidden" name="_hstandard" value="" />
                        </td>
                    </tr>
                    <tr>
                        <td width="77" bgcolor="#DCEFF3" align="right" class="addtableT">
                            增加费用：
                        </td>
                        <td class="kuang2">
                            <input type="text" name="txtSaleAddCost" id="txtSaleAddCost" class="searchInput size68"
                                valid="isMoney" errmsg="请输入正确的金额" />
                        </td>
                        <td width="104" bgcolor="#DCEFF3" align="right" class="addtableT">
                            备注：
                        </td>
                        <td class="kuang2" colspan="3">
                            <input type="text" name="txtSaleAddCostRemark" id="txtSaleAddCostRemark" class="searchInput"
                                style="width: 300px;" />
                        </td>
                    </tr>
                    <tr>
                        <td width="77" bgcolor="#DCEFF3" align="right" class="addtableT">
                            减少费用：
                        </td>
                        <td class="kuang2">
                            <input type="text" name="txtSaleReduceCost" id="txtSaleReduceCost" class="searchInput size68"
                                valid="isMoney" errmsg="请输入正确的金额" />
                        </td>
                        <td width="104" bgcolor="#DCEFF3" align="right" class="addtableT">
                            备注：
                        </td>
                        <td class="kuang2" colspan="3">
                            <input type="text" name="txtSaleReduceCostRemark" id="txtSaleReduceCostRemark" class="searchInput"
                                style="width: 300px;" />
                        </td>
                    </tr>
                    <tr>
                        <td width="77" bgcolor="#DCEFF3" align="right" class="addtableT">
                            <font class="fontbsize12">*</font> 合计金额：
                        </td>
                        <td class="kuang2">
                            <input type="text" id="txtSumPrice" name="txtSumPrice" class="searchInput size68" />
                        </td>
                        <td width="104" bgcolor="#DCEFF3" align="right" class="addtableT">
                            预留截止时间：
                        </td>
                        <td class="kuang2" colspan="3">
                            <input type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%=this.MinDateTime %>'})"
                                id="txtSaveSeatDate" name="txtSaveSeatDate" class="searchInput size68" style="width: 150px;" />
                            <a onclick="WdatePicker({el:'txtSaveSeatDate',dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%=this.MinDateTime %>'});"
                                href="javascript:void(0);" class="timesicon">预留截止时间</a>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            订单备注：
                        </td>
                        <td class="kuang2 pand4">
                            <textarea class="searchInput" style="width: 800px; height: 55px;" rows="" cols=""
                                name="textarea"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
            <asp:PlaceHolder ID="phdTravelControlS" runat="server">
                <uc4:TravelControl ID="TravelControlS1" runat="server" />
            </asp:PlaceHolder>
            <div class="hr_10">
            </div>
            <div class="mainbox cunline">
                <ul>
                    <li class="cun-cy"><a href="javascript:void(0)" id="btnSave">报名</a></li>
                    <li class="quxiao-cy"><a href="javascript:void(0)" id="btnCancle">取消</a></li>
                </ul>
            </div>
            <asp:HiddenField runat="server" ID="hfTourType" />
        </div>
        <div class="hr_10">
        </div>
    </div>
    <!-- InstanceEndEditable -->
    </form>
</body>
</html>

<script type="text/javascript">
    var orderApply = {
        Data: {
            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
            TourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("TourId") %>',
            IsShort:'<%=EyouSoft.Common.Utils.GetQueryStringValue("IsShort") %>'
        },
        Bind: function() {
            $("#btnSave").css("background-position", "0 0").unbind("click").text("报名");
            $("#btnSave").click(function() {
                if (ValiDatorForm.validator("<%=form1.ClientID %>", "alert")) {
                    orderApply.Save();
                }
                return false;
            });

            $("#btnCancle").css("background-position", "0 0").unbind("click").text("取消");
            $("#btnCancle").click(function() {
                window.location.href = "AcceptPlan.aspx?sl=" + orderApply.Data.sl;
                return false;
            });
        },
        UnBind: function() {
            $("#btnSave").unbind("click");
            $("#btnCancle").unbind("click");
        },
        Save: function() {
            $("#btnSave").text("提交中...").css("background-position", "0 -62px");
            orderApply.UnBind();
            $.newAjax({
                url: "AcceptPlanApply.aspx?Type=Save&" + $.param(orderApply.Data),
                type: "post",
                data: $("#form1").serialize(),
                dataType: "json",
                success: function(data) {
                    if (data.result == "1") {
                        tableToolbar._showMsg(data.msg, function() {
                            window.location.href = "AcceptPlan.aspx?sl=" + orderApply.Data.sl;
                        });
                    }
                    else {
                        tableToolbar._showMsg(data.msg);
                    }
                    orderApply.Bind();
                },
                error: function() {
                    tableToolbar._showMsg("服务器忙！");
                    orderApply.Bind();
                }
            });
        },
        Cancle: function() {
            window.location.href = "AcceptPlan.aspx?sl=" + orderApply.Data.sl;
        },
        SelectPrice: function() {
            $("input[type='radio']").click(function() {
                if ($(this).attr("checked") == true) {
                    var adults = $("#txtAdults").val()||0;
                    var childs = $("#txtChilds").val()||0;
                    var jie=$.trim($("#lbOnPrice").text())||0;
                    var song=$.trim($("#lbOffPrice").text())||0;
                    var value = $(this).val().split('|');
                    var adultsPrice=0,childsPrice=0,jiePrice = 0;

                    var RegInteger = /^[0-9]+$/;
                    var RegMoney=/^\d+(\.\d+)?$/;
                    if(RegMoney.test(jie) && RegMoney.test(song)){
                       jiePrice=(parseInt(adults)+parseInt(childs))*(parseFloat(jie)+parseFloat(song));
                    }
                    if (RegInteger.test(adults)) {
                        adultsPrice = adults * value[1];
                    }
                    if (RegInteger.test(childs)) {
                        childsPrice = childs * value[2];
                    }

                    var saleAddCost = $("#txtSaleAddCost").val();
                    var saleReduceCost = $("#txtSaleReduceCost").val();
                    var addPrice = tableToolbar.getFloat(saleAddCost),reducePrice = tableToolbar.getFloat(saleReduceCost);
                    $("#txtSumPrice").val(tableToolbar.getFloat(adultsPrice + childsPrice + addPrice - reducePrice+jiePrice));

                    //standard用于取结算价
                    var span = $(this).closest("tr").find("span");

                    if (span.attr("data-class") == "standard") {
                        //alert(span.attr("data-id"));
                        $("#_hstandard").val(span.attr("data-id"));
                    }
                }
            });
        },
        ChangePrice: function(obj) {
                var adults = $("#txtAdults").val()||0;
                var childs = $("#txtChilds").val()||0;
                var jie=$.trim($("#lbOnPrice").text())||0;
                var song=$.trim($("#lbOffPrice").text())||0;
                var value = "0|0|0";
                $("input[type='radio']").each(function() {
                    if ($(this).is(':checked')) {
                        value = $(this).val();
                    }
                });
                var adultsPrice=0,childsPrice=0,jiePrice=0;
                var values = value.split('|');
                var RegInteger = /^[0-9]+$/;
                var RegMoney=/^\d+(\.\d+)?$/;
                if(RegMoney.test(jie) && RegMoney.test(song)){
                     jiePrice=(parseInt(adults)+parseInt(childs))*(parseFloat(jie)+parseFloat(song));
                }
                if (RegInteger.test(adults)) {
                    adultsPrice = parseInt(adults) * parseFloat(values[1]);
                }
                if (RegInteger.test(childs)) {
                    childsPrice = parseInt(childs) * parseFloat(values[2]);
                }

                var saleAddCost = $("#txtSaleAddCost").val();
                var saleReduceCost = $("#txtSaleReduceCost").val();
                var addPrice = tableToolbar.getFloat(saleAddCost),reducePrice = tableToolbar.getFloat(saleReduceCost);
                $("#txtSumPrice").val(tableToolbar.getFloat(adultsPrice + childsPrice + addPrice - reducePrice+jiePrice));
        },
        PageInit: function() {
            
            $("#txtAdults,#txtChilds,#txtSaleAddCost,#txtSaleReduceCost").blur(function() {
                orderApply.ChangePrice();
            })
            $("#selPickUpPosition").change(function(){
                 orderApply.ChangePrice();
            })
            orderApply.SelectPrice();
            $("#btnSave").click(function() {
                if($("#PriceStand_table").find("input[name='txt_PriceStand_radio_price']:checked").length<1){
                    tableToolbar._showMsg("请选择价格组成!");
                    return false;
                 }
                if (ValiDatorForm.validator("<%=form1.ClientID %>","alert")) {
                    orderApply.Save();
                }
            });

            $("#btnCancle").click(function() {
                orderApply.Cancle();
                return false;
            });


            //游客信息的控件样式
            var that = $("#TravelControl_tbl");
            if (that) {
                that.attr("width", "100%");
            }

            var _that = $("#TravelControlS_tbl");
            if (_that) {
                _that.attr("width", "100%");
            }
            //            that.attr("style", "");
            //            that.attr("bordercolor", "#CCCCCC");
            //            that.attr("border", "1");

            //            that.find("input[type]")


            //            var tr = $("#TravelControl_tbl tr:eq(0)");
            //            tr.removeAttr("style");
            //            var tds = tr.find("td")
            //            tds.attr("bgcolor", "#DCEFF3");
            //            tds.removeAttr("class");
            //报价控件的样式
            var priceStand = $("#PriceStand_table");
            priceStand.removeAttr("class", "autoAdd");
            priceStand.removeAttr("bgcolor");
            priceStand.attr("bordercolor", "#cccccc");
            priceStand.attr("style", "border-collapse:collapse; margin:5px 0px;");


        }

    };


    $(function() {
        orderApply.PageInit();
    });
</script>

