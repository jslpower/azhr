<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiWuDengJiEdit.aspx.cs" Inherits="EyouSoft.Web.Fin.CaiWuDengJiEdit" %>

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
        <td width="13%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">财务类型：</td>
        <td align="left">
            <asp:DropDownList ID="seltyp" runat="server" CssClass="inputselect">
                <asp:ListItem>无法获取财务类型</asp:ListItem>
            </asp:DropDownList>
            </td>
      </tr>
      <tr>
        <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">发生时间：</td>
        <td align="left"><input type="text" id="txtApplyDate" class="inputtext formsize80" name="txtApplyDate" runat="server" onfocus="WdatePicker();" valid="required" errmsg="请选择发生时间！"/></td>
      </tr>
      <tr>
        <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">财务标题：</td>
        <td align="left"><input type="text" class="inputtext formsize200" id="txtTitle" name="txtTitle" runat="server" valid="required" errmsg="请填写财务标题！" maxlength="255"/></td>
      </tr>
      <tr>
        <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">单位名称：</td>
        <td align="left"><input type="text" class="inputtext formsize200" id="txtDanWeiNm" name="txtDanWeiNm" runat="server" maxlength="255"/></td>
      </tr>
	  <tr>
        <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">金额：</td>
        <td align="left"><input type="text" id="txtFeeAmount" class="inputtext formsize80" name="txtFeeAmount" runat="server"  valid="required|IsDecimalTwo" errmsg="请输入金额！|请输入正确金额！"/></td>
      </tr>
	  <tr>
        <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">备注：</td>
        <td align="left">
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="inputtext formsize450" Style="height: 50px;"
                            TextMode="MultiLine"></asp:TextBox>
        </td>
      </tr>
  </tbody></table>
  <div class="hr_10"></div>
  <div class="alertbox-btn">
  <a hidefocus="true" href="javascript:void(0);" id="a_Save"><s class="baochun"></s>保 存</a>
  <a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s class="chongzhi"></s>关 闭</a>
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
        			var obj = $(obj);
        			obj.unbind("click");
        			obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
        			obj.html("<s class=baochun></s>  提交中...");
        			$.newAjax({
        					type: "post",
        					data: $(that.Form).serialize() +
        					"&" + $.param({
        						doType: "Save",
        						Id: '<%=Request.QueryString["id"] %>'
        					}) ,
        					cache: false,
        					url: '/Fin/CaiWuDengJiEdit.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
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
        		var s = $("#a_Save");
        		s.unbind("click");
        		s.html("<s class=baochun></s>保 存");
        		s.click(function() {
        			Add.Save(this);
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
        	Add.PageInit();
        });
    </script>

</html>
