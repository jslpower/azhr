<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiWuDengJi.aspx.cs" Inherits="EyouSoft.Web.Fin.CaiWuDengJi" MasterPageFile="~/MasterPage/Front.Master"%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="warp">
        <div class="mainbox">
        <form method="GET">
          <div class="searchbox border-bot fixed">
              <span class="searchT">
              <p>
                        财务类型：
                          <select name="selTyp" class="inputselect">
                          <%foreach (var item in EnumObj.GetList(typeof(EyouSoft.Model.EnumType.FinStructure.CaiWuDengJi))) %>
                          <%{ %>
                            <option value="<%= item.Value%>" <%=item.Value==Request.QueryString["selTyp"]?"selected":"" %>><% =item.Text%></option>
                          <%} %>
                          </select>
                          发生时间：
                          <input value="<%= Request.QueryString["sDate"] %>" name="sDate" type="text" class="inputtext formsize80" onfocus="WdatePicker();"/> 
                          - <input value="<%= Request.QueryString["eDate"] %>" name="eDate" type="text" class="inputtext formsize80" onfocus="WdatePicker();"/>
                        财务标题：<input value="<%=Request.QueryString["txtTitle"] %>" type="text" name="txtTitle" class="inputtext formsize120"/>
                        <input class="search-btn" type="submit" id="btnSearch"/></p>
                </span>
                <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </div>
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
                          <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>

            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle" >
                    <tbody><tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox"/>
                        </th>
                        <th align="center">
                            财务类型
                        </th>
                        <th align="center">
                            发生时间
                        </th>
                        <th align="center">
                            财务标题
                        </th>
                        <th align="center">
                            金额
                        </th>
                        <th align="center">
                            备注
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr data-id="<%#Eval("Id") %>">
                                <td align="center"><input type="checkbox" id="checkbox" name="checkbox"></td>
                                <td align="center"><%#Eval("Typ") %></td>
                                <td align="center"><%#Eval("ApplyDate","{0:yyyy-MM-dd}") %></td>
                                <td align="left"><%#Eval("Title") %></td>
                                <td align="right"><b class="fontbsize12"><%#Eval("FeeAmount","{0:C2}") %></b></td>
                                <td align="left"><%#Eval("Remark") %></td>
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

        </div>
        <!-- InstanceEndEditable -->
  
 
</div>  
        
<script type="text/javascript">
var CaiWuDengJi = {
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
			url: "/Fin/CaiWuDengJiEdit.aspx?",
			title: "财务情况登记",
			width: "600px",
			height: "350px"
		};
	},
	Add: function() { /*新增*/
		var data = CaiWuDengJi.DataBoxy();
		data.title += EnglishToChanges.Ping("Add");
		data.url += $.param({ sl: '<%=Utils.GetQueryStringValue("sl") %>' });
		CaiWuDengJi.ShowBoxy(data);
		return false;
	},
	Upd: function(o) { /*修改*/
		var data = CaiWuDengJi.DataBoxy();
		data.title += EnglishToChanges.Ping("Update");
		data.url += $.param({ sl: '<%=Utils.GetQueryStringValue("sl") %>', id: $(o).attr("data-id") });
		CaiWuDengJi.ShowBoxy(data);
		return false;
	},
	Del: function(arrTr) { /*删除*/
		var ids = [], url = "/Fin/CaiWuDengJi.aspx?" + $.param({
				doType: "del",
				sl: '<%=Utils.GetQueryStringValue("sl") %>'
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
					}
					else {
						parent.tableToolbar._showMsg(ret.msg);
						CaiWuDengJi.PageInit();
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
			CaiWuDengJi.Add();
		});
		tableToolbar.init({
				tableContainerSelector: "#liststyle", //表格选择器
				objectName: "记录",
                    //默认按钮事件
				updateCallBack: function(objArr) { /*修改*/
					var obj = $(objArr[0]);
					CaiWuDengJi.Upd(obj);
				},
				deleteCallBack: function(objArr) { /*删除(批量)*/
					CaiWuDengJi.Del(objArr);
				}
			});
	},
	PageInit: function() {
		CaiWuDengJi.BindBtn();
		$("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
	}
};

$(function() {
	CaiWuDengJi.PageInit();
});

</script>

</asp:Content>