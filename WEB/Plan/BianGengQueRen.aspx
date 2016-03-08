<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BianGengQueRen.aspx.cs" Inherits="EyouSoft.Web.Plan.BianGengQueRen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
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
<body style="background:0 none;">
<div class="alertbox-outbox">
    <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9" style="margin:0 auto">
      <tr>
        <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">团号：</td>
        <td colspan="3" align="left"><asp:Literal runat="server" ID="ltlTourCode"></asp:Literal></td>
      </tr>
      <tr>
        <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">线路区域：</td>
        <td align="left"><asp:Literal runat="server" ID="ltlAreaName"></asp:Literal></td>
        <td align="right" class="alertboxTableT">线路名称：</td>
        <td align="left"><asp:Literal runat="server" ID="ltlRouteName"></asp:Literal></td>
      </tr>
      <tr>
        <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">变更标题：</td>
        <td colspan="3" align="left"><asp:Literal runat="server" ID="ltlTitle"></asp:Literal></td>
      </tr>
      <tr>
        <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">变更人：</td>
        <td width="35%" align="left"><asp:Literal runat="server" ID="ltlOperator"></asp:Literal></td>
        <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">变更时间：</td>
        <td width="35%" height="28"><asp:Literal runat="server" ID="ltlIssueTime"></asp:Literal></td>
      </tr>
       <tr>
        <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">变更内容：</td>
        <td colspan="3" style="padding:6px 4px;"><asp:Literal runat="server" ID="ltlContent"></asp:Literal></td>
      </tr>
  </table>
  <div class="alertbox-btn"><a href="javascript:void(0);" hidefocus="true" id="a_Save">确认变更</a><a href="javascript:void(0);" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s class="chongzhi"></s>关闭</a>
    </div>
</div>
</body>
<script type="text/javascript">
        var Page = {
        	Confirm: function(obj) {
        		var that = this;
        		var obj = $(obj);
        		obj.unbind("click");
        		obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
        		obj.html("<s class=baochun></s>  提交中...");
        		$.newAjax({
        				type: "post",
        				cache: false,
        				url: '/Plan/BianGengQueRen.aspx?doType=Save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&id=<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>&tourid=<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>',
        				dataType: "json",
        				success: function(data) {
        					if (parseInt(data.result) === 1) {
        						parent.tableToolbar._showMsg('确认成功!', function() {
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
        		return false;
        	},
        	BindBtn: function() {
        		var s = $("#a_Save");
        		s.unbind("click");
        		s.html("<s class=baochun></s>确认变更");
        		s.click(function() {
        			Page.Confirm(this);
        			return false;
        		});
        		s.css("background-position", "0 0");
        		s.css("text-decoration", "none");
        	},
        	PageInit: function() {
        		var that = this;
        		that.BindBtn();
        	}
        };
        $(function() {
        	Page.PageInit();
        });
    </script>
</html>
