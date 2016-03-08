<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueRen.aspx.cs" Inherits="EyouSoft.Web.Sales.QueRen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�Ŷ�ȷ��</title>
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
<div class="alertbox-outbox02">
    <form id="form1" runat="server">
	<table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin:0 auto;" class="juzh-cy">
      <tr>
        <td width="11%" align="right" valign="middle">������</td>
        <td width="31%" align="left" valign="middle">����
          <input name="txtAdult" id="txtAdult" type="text" class="formsize40 input-txt" valid="required|isNumber" errmsg="��¼���������|��¼����ȷ�ĳ�������" runat="server"/>
        ��ͯ
        <input name="txtChild" id="txtChild" type="text" class="formsize40 input-txt" valid="required|isNumber" errmsg="��¼���ͯ����|��¼����ȷ�Ķ�ͯ����" runat="server"/>
        ���
        <input name="txtLeader" ID="txtLeader" type="text" class="formsize40 input-txt" valid="required|isNumber" errmsg="��¼���������|��¼����ȷ���������" runat="server"/></td>
      </tr>
      <tr>
        <td width="11%" align="right" valign="middle">Ӧ�ս�</td>
        <td width="31%" align="left" valign="middle"><input name="txtAmout" id="txtAmout" type="text" class="formsize80 bk" valid="required|IsDecimalTwo" errmsg="��¼��Ӧ�ս�|��¼����ȷ��Ӧ�ս�" runat="server"/></td>
      </tr>
	  <tr>
        <td width="11%" align="right" valign="middle">�ڲ���Ϣ��</td>
        <td width="31%" align="left" valign="middle"><textarea name="txtNeiBu" id="txtNeiBu" class="bk" style="height:45px; width:320px;" runat="server"></textarea></td>
      </tr>
  </table>
  </form>
  	<div class="alertbox-btn"><%--<a href="javascript:void(0);" hidefocus="true">�����տ</a>--%><a href="javascript:void(0);" id="a_Save" hidefocus="true"><s class="baochun"></s>�� ��</a>
    </div>
</div>
</body>

<script type="text/javascript">
        var Page = {
        	Form: null,
        	FormCheck: function(obj) { /*�ύ������֤*/
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
        			obj.html("<s class=baochun></s>  �ύ��...");
        			$.newAjax({
        					type: "post",
        					data: $(that.Form).serialize() +
        					"&" + $.param({
        						doType: "Save",
        						TourId: '<%=Request.QueryString["tourid"] %>'
        					}) ,
        					cache: false,
        					url: '/Sales/QueRen.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
        					dataType: "json",
        					success: function(data) {
        						if (data.result) {
        							parent.tableToolbar._showMsg(data.msg, function() {
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
        						//ajax�쳣--�㶮��
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
        		s.html("<s class=baochun></s>�� ��");
        		s.click(function() {
        			Page.Save(this);
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
