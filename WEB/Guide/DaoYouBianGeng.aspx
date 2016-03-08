<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouBianGeng.aspx.cs" Inherits="EyouSoft.Web.Guide.DaoYouBianGeng" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf8" />
<title>���α��</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="../js/jquery-1.4.4.js"></script>
<script type="text/javascript" src="../js/jquery.boxy.js"></script>
<script type="text/javascript" src="../js/table-toolbar.js"></script>
</head>

<body style="background:0 none;">
	<div class="alertbox-outbox">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin:0 auto;word-wrap:break-word; overflow:hidden;word-break: break-all;table-layout:fixed;">
            <tr>
              <td width="5%" height="25" align="center" class="alertboxTableT">���</td>
              <td width="10%" align="center" class="alertboxTableT">�ź�</td>
              <td width="10%" align="center" class="alertboxTableT">��������</td>
              <td width="10%" align="center" class="alertboxTableT">�����</td>
              <td width="10%" align="center" class="alertboxTableT">�������</td>
              <td width="45%" align="center" class="alertboxTableT">�������</td>
              <td width="10%" align="center" class="alertboxTableT">״̬</td>
              <%--<td width="80" align="center" class="alertboxTableT">����</td>--%>
            </tr>
            <asp:Repeater runat="server" ID="rpt_list">
            <ItemTemplate>
            <tr>
              <td height="25" align="center"><%#Container.ItemIndex+1+this.pageIndex*this.pageSize %></td>
              <td align="center"><%#Eval("tourcode") %></td>
              <td align="center"><%#Eval("guidenm") %></td>
              <td align="center"><%#Eval("operator") %></td>
              <td align="center"><%#Eval("issuetime","{0:yyyy-mm-dd}") %></td>
              <td align="center"><%#Eval("content") %></td>
              <td align="center"><%#Eval("state") %></td>
              <%--<td align="center"><a href="javascript:void(0);"><img src="../images/qren_btn.gif" width="48" height="20" /></a></td>--%>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="15">
                            ��������!
                        </td>
                    </tr>
                </asp:Panel>
            <tr><td height="30" colspan="8"><div style="position:relative; height:20px;"><div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div></div></td></tr>
            </table>
  	

       <div class="alertbox-btn"><a id="a_add" style="text-indent:3px; color: #CC0033" hidefocus="true" href="javascript:void(0);"><b>+�������</b></a></div>
  	
</div>
</body>

<script type="text/javascript">
var Page = {
    reload: function() {
                    window.location.href = window.location.href;
                },	
	ShowBoxy: function(data) { /*��ʾ����*/
		parent.Boxy.iframeDialog({
				iframeUrl: data.url,
				title: data.title,
				modal: true,
				width: data.width,
				height: data.height,
				draggable: true,
				afterHide:function() { Page.reload(); }
			});
	},
	DataBoxy: function() { /*����Ĭ�ϲ���*/
		return {
			url: '/Guide/DaoYouBianGengEdit.aspx?sl=<%=this.SL %>&pIframeId=<%=Request.QueryString["iframeId"] %>&',
			title: "���α��",
			width: "600px",
			height: "350px"
		};
	},
	Add: function() { /*����*/
		var data = Page.DataBoxy();
		data.title += EnglishToChanges.Ping("Add");
		data.url += $.param({ tourid:'<%=Request.QueryString["tourid"] %>',tourcode:'<%=Request.QueryString["tourcode"] %>' });
		Page.ShowBoxy(data);
		return false;
	},
	Upd: function(o) { /*�޸�*/
		var data = Page.DataBoxy();
		data.title += EnglishToChanges.Ping("Update");
		data.url += $.param({ id: $(o).attr("data-id") });
		Page.ShowBoxy(data);
		return false;
	},
	Del: function(arrTr) { /*ɾ��*/
		var ids = [], url = "/Guide/DaoyouBianGeng.aspx?" + $.param({
				doType: "del",
				sl: '<%=this.SL %>'
			});
		$(arrTr).each(function() {
			ids.push($(this).attr("data-id"));
		});
		$.newAjax({
				type: "post",
				data: $.param({
					Ids: ids.join(',')
				}),
				cache: false,
				url: url,
				dataType: "json",
				success: function(ret) {
					if (parseInt(ret.result) === 1) {
						parent.tableToolbar._showMsg('ɾ���ɹ�!', function() {
							location.href = location.href;
						});
					}
					else {
						parent.tableToolbar._showMsg(ret.msg);
						Page.PageInit();
					}
				},
				error: function() {
					//ajax�쳣--�㶮��
					parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
				}
			});
	},
	BindBtn: function() { /*�󶨹��ܰ�ť*/
		$("#a_add").click(function() { /*����*/
			Page.Add();
		});
		tableToolbar.init({
				tableContainerSelector: "#liststyle", //���ѡ����
				objectName: "��¼",
                    //Ĭ�ϰ�ť�¼�
				updateCallBack: function(objArr) { /*�޸�*/
					var obj = $(objArr[0]);
					Page.Upd(obj);
				},
				deleteCallBack: function(objArr) { /*ɾ��(����)*/
					Page.Del(objArr);
				}
			});
	},
	PageInit: function() {
		Page.BindBtn();
	}
};

$(function() {
	Page.PageInit();
});

</script>

</html>
