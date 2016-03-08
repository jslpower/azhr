<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaiTuan.aspx.cs" Inherits="EyouSoft.Web.Sales.PaiTuan" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>派团给计调</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;"
            class="juzh-cy">
            <tr>
                <td width="15%" align="right" valign="middle">
                    <font class="fontbsize12">* </font>OP：
                </td>
                <td width="31%" align="left" valign="middle">
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="选择OP" CallBackFun="TeamPlanForAdjustment.CallBackFun" />
                    <asp:HiddenField ID="hideDeptId" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" rowspan="2" align="right">
                    <font class="fontbsize12">* </font>需安排项目：
                </td>
                <td align="left">
                <label>
                        <input type="checkbox" name="chk_item" id="chk_all"/>全选
                    </label>
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接 %>" />
                        <img src="../images/jie.gif" />接
                    </label>
                    &nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 %>" />
                        <img src="../images/fang.gif" />房
                    </label>
                    &nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐 %>" />
                        <img src="../images/can.gif"/>餐</label>
                    &nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 %>" />
                        <img src="../images/jing.gif"  />景</label>&nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车 %>" />
                        <img src="../images/che.gif" />车
                    </label>
<%--                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 %>" />
                        船(内)</label>&nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮 %>" />
                        船(外)</label>&nbsp;
--%>                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游 %>" />
                        <img src="../images/dao.gif" />导</label>&nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物 %>" />
                        购</label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车 %>" />
                        <img src="../images/che.gif" />票(汽)</label>&nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车 %>" />
                        <img src="../images/car.gif" />票(火)</label>&nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机 %>" />
                        <img src="../images/feij.gif" />票(飞)</label>&nbsp;
                    <label>
                        <input type="checkbox" name="chk_item" value="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船 %>" />
                        <img src="../images/chuan.gif" />票(船)</label>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">是否分车：</td>
                <td>
                    <input type="checkbox" name="chk_isfenche" id="chk_isfenche" runat="server"/>
                    几车<input type="text" name="txtChe" id="txtChe" runat="server"/>
                    <img src="../images/bei.jpg" /><input type="text" name="txtCheRemark" id="txtCheRemark" runat="server"/>
                </td>
            </tr>
            <tr>
                <td align="right">是否分桌：</td>
                <td>
                    <input type="checkbox" name="chk_isfenzuo" id="chk_isfenzuo" runat="server"/>
                    几桌<input type="text" name="txtZuo" id="txtZuo" runat="server"/>
                    <img src="../images/bei.jpg" /><input type="text" name="txtZuoRemark" id="txtZuoRemark" runat="server"/>
                </td>
            </tr>
            <tr>
                <td width="11%" valign="middle" align="right">
                    内部信息：
                </td>
                <td width="31%" valign="middle" align="left">
                    <asp:TextBox ID="txtInsiderInfor" TextMode="MultiLine" runat="server" CssClass="inputtext formsize450"
                        Height="80"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="phdSave" runat="server"><a href="javascript:void(0);" id="btnSave">
                <s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" id="btnClose" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <asp:HiddenField ID="hidePlanItem" runat="server" />
    </form>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        var TeamPlanForAdjustment = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                act: '<%=Request.QueryString["act"] %>',
                tourID: '<%=Request.QueryString["id"] %>'
            },
            Submit: function() {
                var sellerId = $("#<%=this.SellsSelect1.SellsIDClient%>").val();
                if (!sellerId) {
                    parent.tableToolbar._showMsg("请选择OP!");
                    return false;
                }
                $("#btnSave").unbind("click");
                $("#btnSave").html('<s class="baochun"></s>正在保存..');
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sales/PaiTuan.aspx?dotype=save&" + $.param(TeamPlanForAdjustment.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            $("#btnSave").html('<s class="baochun"></s>保 存');
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                $("#btnClose").click();
                                parent.window.location.reload();
                            })
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                            $("#btnSave").html('<s class="baochun"></s>保 存');
                            $("#btnSave").click(function() {
                                TeamPlanForAdjustment.Submit();
                            })
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        $("#btnSave").html('<s class="baochun"></s>保 存');
                        $("#btnSave").click(function() {
                            TeamPlanForAdjustment.Submit();
                        })
                    }
                });
            },
        	QuanXuan:function () {
        		$("input[name=chk_item]").attr("checked","checked");
        	},
        	QuXiao:function () {
        		$("input[name=chk_item]").attr("checked","");
        	},
            CallBackFun: function(data) {
                $("#<%=SellsSelect1.SellsIDClient%>").val(data.value);
                $("#<%=SellsSelect1.SellsNameClient%>").val(data.text);
                $("#<%=hideDeptId.ClientID%>").val(data.deptID);
            }
            //初始化需要安排的计调项目
            , initXuYaoAnPaiXiangMu: function() {
                var _s = $.trim($("#<%=hidePlanItem.ClientID%>").val());
                if (_s.length == 0) return;
                var _arr = _s.split(",");
                var _length = _arr.length;
                if (_length == 0) return;
                for (var i = 0; i < _length; i++) {
                    if (_arr[i].length == 0) continue;
                    $("input[name='chk_item'][value='" + _arr[i] + "']").attr("checked", "checked");
                }
            }
        }
        $(function() {
            $("#btnSave").click(function() { TeamPlanForAdjustment.Submit(); });
            $("#<%=this.SellsSelect1.SellsNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada").css("width", "350px");

            TeamPlanForAdjustment.initXuYaoAnPaiXiangMu();
        	$("#chk_all").bind("click",function() {
        		if (!$(this).attr("checked")) {
        			TeamPlanForAdjustment.QuXiao();
        		}
        		else {
        			TeamPlanForAdjustment.QuanXuan();
        		}
        	});
        });
    </script>

</body>
</html>
