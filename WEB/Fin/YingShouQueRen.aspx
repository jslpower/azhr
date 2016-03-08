<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingShouQueRen.aspx.cs" Inherits="EyouSoft.Web.Fin.YingShouQueRen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/ValiDatorForm.js"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

</head>
<body style="background: #e9f4f9;">
<form id="form1" runat="server">
    <div class="alertbox-outbox">
  <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9" style="margin:0 auto">
  
    <tr>
      <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">团号：</td>
      <td width="10%" align="left"><asp:Literal ID="litTourCode" runat="server"></asp:Literal></td>
      <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">团队名称：</td>
      <td width="30%" align="left"><asp:Literal ID="litRouteName" runat="server"></asp:Literal></td>
    </tr>
    <tr>
      <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">应收金额：</td>
      <td colspan="3" align="left"><b><asp:Literal ID="litAccountPrices" runat="server"></asp:Literal></b><input type="hidden" id="hidAccountPrices" runat="server" /></td>
    </tr>
    <tr>
      <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">变更情况：</td>
      <td colspan="3" align="left"><table width="98%" border="0" style="margin-top:5px; margin-bottom:5px;" class="autoAdd">
        <tr>
          <td align="center" class="td-leftbg">增加费用</td>
          <td align="center" class="td-leftbg">备注</td>
          <td align="center" class="td-leftbg">减少费用</td>
          <td align="center" class="td-leftbg">备注</td>
          <td align="center" class="td-leftbg">操作</td>
        </tr>
        <asp:Repeater runat="server" ID="rptList">
        <ItemTemplate>
            <tr class="tempRow">
              <td align="center"><input name="txtAdd" value="<%#Eval("AddFee","{0:F2}") %>" type="text" class="inputbg formsize80" /></td>
              <td align="center"><input name="txtAddRemark" value="<%#Eval("AddRemark") %>" type="text" class="inputbg formsize140" /></td>
              <td align="center"><input name="txtReduce" value="<%#Eval("RedFee","{0:F2}") %>" type="text" class="inputbg formsize80" /></td>
              <td align="center"><input name="txtReduceRemark" value="<%#Eval("RedRemark") %>" type="text" class="inputbg formsize140" /></td>
              <td align="center"><a href="javascript:void(0)" class="addbtn"><img src="../images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)" class="delbtn"><img src="../images/delimg.gif" width="48" height="20" /></a></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
            <tr class="tempRow">
              <td align="center"><input name="txtAdd" value="0" type="text" class="inputbg formsize80" /></td>
              <td align="center"><input name="txtAddRemark" type="text" class="inputbg formsize140" /></td>
              <td align="center"><input name="txtReduce" value="0" type="text" class="inputbg formsize80" /></td>
              <td align="center"><input name="txtReduceRemark" type="text" class="inputbg formsize140" /></td>
              <td align="center"><a href="javascript:void(0)" class="addbtn"><img src="../images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)" class="delbtn"><img src="../images/delimg.gif" width="48" height="20" /></a></td>
            </tr>
        </asp:PlaceHolder>
      </table></td>
    </tr>
    <tr>
      <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">变更说明：</td>
      <td colspan="3" align="left"><asp:TextBox ID="txtchangeEsplain" runat="server" TextMode="MultiLine" CssClass="inputtext formsize450"
                            Style="height: 35px;"></asp:TextBox></td>
    </tr>
	
	<tr>
      <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">确认金额：</td>
      <td colspan="3" align="left"><asp:TextBox ID="txtComfirmMoney" runat="server" CssClass="inputtext formsize40"></asp:TextBox></td>
    </tr>
  </table>
<div class="alertbox-btn">
    <asp:Panel ID="pan_Save" runat="server" Style="display: inline" Visible="false">                    
        <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>确认</a>
    </asp:Panel>
        
    <a href="javascript:" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();">
        <s class="chongzhi"></s>关 闭</a>
</div>
</div>
</form>
<script type="text/javascript">
        var StatementsPage = {
        	_Save: function() {
        		var tourId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>';
        		var OrderId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderId") %>';
        		$.newAjax({
        				type: "POST",
        				url: '/Fin/YingShouQueRen.aspx?action=save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&tourId=' + tourId + "&OrderId=" + OrderId + "&confirm=1",
        				cache: false,
        				data: $("#btnSave").closest("form").serialize(),
        				dataType: "json",
        				success: function(data) {
        					if (data.result == "1") {
        						parent.tableToolbar._showMsg(data.msg, function() {
        							window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        							parent.window.location.reload();
        						});
        					} else {
        						parent.tableToolbar._showMsg(data.msg);
        						StatementsPage._BindBtn();
        					}
        				},
        				error: function() {
        					parent.tableToolbar._showMsg(tableToolbar.errorMsg);
        					StatementsPage._BindBtn();
        				}
        			});
        	},
        	_TotalPrices: function() {
        		//合计金额 增加费用 减少费用  确认金额
        		var tr = $("table[class=autoAdd]").find("tr[class=tempRow]");
        		var total = 0;
        		$.each(tr, function(i) {
        			var a = $(this).find("input[name=txtAdd]").val();
        			var r = $(this).find("input[name=txtReduce]").val();
        			var t = tableToolbar.calculate(tableToolbar.getFloat(a), tableToolbar.getFloat(r), "-");
        			total = tableToolbar.calculate(total, t, "+");
        		});
        		$("#<%=txtComfirmMoney.ClientID %>").val(tableToolbar.calculate(tableToolbar.getFloat($("#<%=this.hidAccountPrices.ClientID %>").val()), total, "+"));
        	},
        	_BindBtn: function() {
        		$("input[type='text'][name='txtAdd']").unbind("change").change(function() {
        			StatementsPage._TotalPrices();
        		});
        		$("input[type='text'][name='txtReduce']").unbind("change").change(function() {
        			StatementsPage._TotalPrices();
        		});
        		$("#btnSave").text("确 认").unbind("click").css("background-position", "0 0px");
        		$("#btnSave").click(function() {
        			parent.tableToolbar.ShowConfirmMsg("合同金额确认后将不能修改确认金额，你确定要确认吗？", function() {
        				$("#btnSave").text("处理中...").unbind("click").css("background-position", "0 -55px");
        				StatementsPage._Save();
        			});
        			return false;
        		});

        	},
        	_PageInit: function() {
        		this._BindBtn();
        	}
        };
        $(document).ready(function() {
            StatementsPage._PageInit();
        });
    </script>
    </body>
</html>
