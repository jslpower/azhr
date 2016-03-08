<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouBianGengEdit.aspx.cs" Inherits="EyouSoft.Web.Guide.DaoYouBianGengEdit" %>
<%@ Register TagPrefix="uc2" TagName="SellsSelect" Src="~/UserControl/SellsSelect.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增变更</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body style="background:0 none;">
  <form runat="server">
        <div class="alertbox-outbox02">
        <table width="100%" height="79" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td width="22%" height="32" align="right">团号：</td>
                <td width="78%" height="32" align="left"><%=Request.QueryString["TourCode"] %></td>
              </tr>
              <tr>
                <td height="32" align="right">导游姓名：</td>
                <td height="32" align="left"><uc2:SellsSelect ID="txtDao" runat="server" SetTitle="导游选用" SelectFrist="false" /></td>
              </tr>
              <tr>
                <td height="32" align="right">变更人：</td>
                <td height="32" align="left"><asp:Literal runat="server" ID="ltlBian"></asp:Literal></td>
              </tr>
              <tr>
                <td height="32" align="right">变更日期：</td>
                <td height="32" align="left"><input name="txtDate" type="text" id="txtDate" class="formsize80 bk"onfocus="WdatePicker();" valid="required" errmsg="请选择变更日期！"/></td>
              </tr>
              <tr>
                <td height="32" align="right">变更标题：</td>
                <td height="32" align="left"><input name="txtTitle" type="text" id="txtTitle" class="formsize80 bk" valid="required" errmsg="请填写变更标题！"/></td>
              </tr>
              <tr>
                <td height="60" align="right">变更内容：</td>
                <td height="60" align="left"><textarea name="txtContent" cols="35" class="bk" id="txtContent" style="width:300px; height:50px;"valid="required" errmsg="请填写变更内容！"></textarea></td>
              </tr>
          </table>
          <div class="alertbox-btn"><a id="a_Save" href="javascript:void(0);" hidefocus="true"><s class="baochun"></s>确 定</a><a href="javascript:void(0);" hidefocus="true"onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s class="chongzhi"></s>关 闭</a></div>
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
        						Id: '<%=Request.QueryString["id"] %>',
        						TourId: '<%=Request.QueryString["tourid"] %>',
        						TourCode: '<%=Request.QueryString["tourcode"] %>'
        					}),
        					cache: false,
        					url: '/Guide/DaoYouBianGengEdit.aspx?sl=<%=this.SL %>',
        					dataType: "json",
        					success: function(data) {
        						if (parseInt(data.result) === 1) {
        							parent.tableToolbar._showMsg('保存成功!', function() {
        								window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
        								
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
        		$("#txtDate").val("<%=DateTime.Now.ToShortDateString() %>");
        		if (DaoYouBianGengEdit == null) return;
        		$("#<%=this.txtDao.SellsIDClient %>").val(DaoYouBianGengEdit.GuideId);
        		$("#<%=this.txtDao.SellsNameClient %>").val(DaoYouBianGengEdit.GuideNm);
        		$("#<%=this.ltlBian.ClientID %>").val(DaoYouBianGengEdit.Operator);
        		$("#txtDate").val(DaoYouBianGengEdit.IssueTime);
        		$("#txtTitle").val(DaoYouBianGengEdit.Title);
        		$("#txtContent").val(DaoYouBianGengEdit.Content);
        	}
        };
        $(function() {
        	Add.PageInit();
        });
    </script>
</html>
