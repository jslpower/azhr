<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QianDanGuaShiEdit.aspx.cs" Inherits="EyouSoft.Web.Fin.QianDanGuaShiEdit" %>
<%@ Register Src="/UserControl/TuanHaoXuanYong.ascx" TagName="TuanHaoXuanYong" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc2" TagName="SellsSelect" Src="~/UserControl/SellsSelect.ascx" %>

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
<form id="Form1" runat="server">
<div class="alertbox-outbox">
<table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center" style="margin:0 auto">
      <tbody><tr>
        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">团号：</td>
        <td align="left" colspan="3"><uc1:TuanHaoXuanYong ID="TuanHaoXuanYong" runat="server" SelectFrist="false"/></td>
      </tr>
      <tr>
        <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">类别：</td>
        <td width="39%" align="left">
            <asp:DropDownList ID="seltyp" runat="server" CssClass="inputselect">
                <asp:ListItem>无法获取签单类别</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td width="11%" align="right" class="alertboxTableT">签单号：</td>
        <td width="36%" align="left"><input type="text" id="txtCode" class="formsize140 input-txt" name="txtCode" runat="server"/></td>
      </tr>
      <tr>
        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">领用人：</td>
        <td align="left"><uc2:SellsSelect ID="txtL" runat="server" SetTitle="领用人" SelectFrist="false" /></td>
        <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">领用时间：</td>
        <td align="left"><input type="text" id="txtApplyTime" class="formsize80 input-txt" name="txtApplyTime" onfocus="WdatePicker();" runat="server"/></td>
      </tr>
  </tbody></table>
<div class="alertbox-btn"><a id="a_Save" hidefocus="true" href="javascript:void(0);"><s class="baochun"></s>保 存</a>
    </div>
</div>
</form>
</body>
<script type="text/javascript">
        var Add = {
        	Form: null,
        	FormCheck: function(obj) { /*提交数据验证*/
        		this.Form = $(obj).get(0);
        		FV_onBlur.initValid(this.Form);
        		return ValiDatorForm.validator(this.Form, "parent");
        	},
        	Save: function(obj) {
        		var that = this;
        		if (that.FormCheck($("form"))) {
        			var o = $(obj);
        			o.unbind("click");
        			o.css({ "background-position": "0 -57px", "text-decoration": "none" });
        			o.html("<s class=baochun></s>  提交中...");
        			$.newAjax({
        					type: "post",
        					data: $(that.Form).serialize() +
        					"&" + $.param({
        						doType: "Save",
        						Id: '<%=Request.QueryString["id"] %>'
        					}),
        					cache: false,
        					url: '/Fin/QianDanGuaShiEdit.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
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
        		$("#a_Save").unbind("click");
        		$("#a_Save").html("<s class=baochun></s>保 存");
        		$("#a_Save").click(function() {
        			Add.Save(this);
        			return false;
        		});
        		$("#a_Save").css("background-position", "0 0");
        		$("#a_Save").css("text-decoration", "none");

        	},
        	PageInit: function() {
        		Add.BindBtn();
        	}
        };
        $(function() {
        	Add.PageInit();
        });
    </script></html>
