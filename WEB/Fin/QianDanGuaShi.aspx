<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QianDanGuaShi.aspx.cs" Inherits="EyouSoft.Web.Fin.QianDanGuaShi" MasterPageFile="~/MasterPage/Front.Master"%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="warp">
     <div class="mainbox">
     <form method="GET">
        	<div class="searchbox border-bot fixed"><span class="searchT">
       	    <p>签单号：
                  <input type="text" class="formsize120 input-txt" name="cd" value="<%=this.Request.QueryString["cd"]%>"/>
领单人：
<uc1:SellsSelect ID="ld" runat="server" selectfrist="false" />
<input class="search-btn" type="submit"/></p></span>            </div>
                <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />

	</form>		
	
          <div id="tablehead" class="tablehead">
             <ul class="fixed">
				<asp:PlaceHolder ID="pan_Add" runat="server">
				<li><s class="addicon"></s><a id="a_add" class="link1" hidefocus="true" href="javascript:void(0)"><span>新增</span></a></li>
                <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_Upd" runat="server">
			    <li><s class="updateicon"></s><a id="a_upd" class="toolbar_update" hidefocus="true" href="javascript:void(0)"><span>修改</span></a></li>
			    <li class="line"></li>
			    </asp:PlaceHolder>
			    <asp:PlaceHolder ID="pan_Del" runat="server">
				<li><s class="delicon"></s><a id="a_del" class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><span>删除</span></a></li>
				</asp:PlaceHolder>
			 </ul>
             <div class="pages">                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
</div>
          </div>
          
 <!--列表表格-->
         <div class="tablelist-box">
           	  <table width="100%" cellspacing="0" border="0" id="liststyle">
				<tbody><tr class="odd">
                  <th width="30" class="thinputbg"><input type="checkbox" id="checkbox" name="checkbox"></th>
                  <th valign="middle" align="center">团号</th>
                  <th valign="middle" align="center">签单类别</th>
                  <th valign="middle" align="center">签单号</th>
                  <th valign="middle" align="center">领用人</th>
                  <th valign="middle" align="center">领用时间</th>
                </tr>
                    <asp:Repeater ID="rpt_list" runat="server">
                        <ItemTemplate>
				<tr data-id="<%#Eval("Id") %>">
                  <td align="center"><input type="checkbox" id="checkbox" name="checkbox"></td>
                  <td align="center"><%#Eval("TourCode") %></td>
                  <td align="center"><%#Eval("Typ") %></td>
                  <td align="center"><%#Eval("SignCode")%></td>
                  <td align="center"><%#Eval("Applier")%></td>
                  <td align="center"><%#Eval("ApplyTime","{0:yyyy-MM-dd}")%></td>
                </tr> 
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="15">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
           </tbody></table>
</div>
          <!--列表结束-->
                    <div id="tablehead_clone">
        </div>
        </div><!-- InstanceEndEditable -->  
</div>

    <script type="text/javascript">
var QianDanGuaShi = {
	ShowBoxy: function(data) { /*显示弹窗*/
		Boxy.iframeDialog({
				iframeUrl: data.url,
				title: data.title,
				modal: true,
				width: data.width,
				height: data.height,
				draggable: true
			});
	},
	DataBoxy: function() { /*弹窗默认参数*/
		return {
			url: "/Fin/QianDanGuaShiEdit.aspx?",
			title: "签单挂失",
			width: "600px",
			height: "350px"
		};
	},
	Add: function() { /*新增*/
		var data = QianDanGuaShi.DataBoxy();
		data.title += EnglishToChanges.Ping("Add");
		data.url += $.param({ sl: '<%=Utils.GetQueryStringValue("sl")%>' });
		QianDanGuaShi.ShowBoxy(data);
		return false;
	},
	Upd: function(o) { /*修改*/
		var data = QianDanGuaShi.DataBoxy();
		data.title += EnglishToChanges.Ping("Update");
		data.url += $.param({ sl: '<%=Utils.GetQueryStringValue("sl")%>', id: $(o).attr("data-id") });
		QianDanGuaShi.ShowBoxy(data);
		return false;
	},
	Del: function(arrTr) { /*删除*/
		var ids = [], url = "/Fin/QianDanGuaShi.aspx?"+$.param({
					doType: "del",
					sl: '<%=Utils.GetQueryStringValue("sl")%>'
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
						parent.tableToolbar._showMsg('删除成功!', function() {
							location.href = location.href;
						});
					} else {
						parent.tableToolbar._showMsg(ret.msg);
						QianDanGuaShi.PageInit();
					}
				},
				error: function() {
					//ajax异常--你懂得
					parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
				}
			});
	},
	BindBtn: function() { /*绑定功能按钮*/
		$("#a_add").click(function() { /*新增*/
			QianDanGuaShi.Add();
		}),
		tableToolbar.init({
				tableContainerSelector: "#liststyle", //表格选择器
				objectName: "记录",
                    //默认按钮事件
				updateCallBack: function(objArr) { /*修改*/
					var obj = $(objArr[0]);
					QianDanGuaShi.Upd(obj);
				},
				deleteCallBack: function(objArr) { /*删除(批量)*/
					QianDanGuaShi.Del(objArr);
				}
			});
	},
	PageInit: function() {
		QianDanGuaShi.BindBtn();
		$("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
	}
};

$(function() {
	QianDanGuaShi.PageInit();
});

</script>

</asp:Content>