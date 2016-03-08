<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BianGeng.aspx.cs" Inherits="EyouSoft.Web.Plan.BianGeng" MasterPageFile="~/MasterPage/Front.Master"%>
<%@ Import Namespace="EyouSoft.Model.EnumType.PrivsStructure" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.TourStructure" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="mainbox">
     <form method="GET">
       <div class="searchbox fixed"><span class="searchT">
       <p>
            团号：<input name="th" value="<%=Request.QueryString["th"] %>" type="text" class="formsize120 input-txt" />
            <% if (this._sl == Menu2.计调中心_业务变更)%>
            <%{%>
            线路区域：<input name="qy" value="<%=Request.QueryString["qy"]%>" type="text" class="formsize120 input-txt" />
            业务员：<uc1:SellsSelect ID="yw" SetTitle="业务员选用" runat="server" selectfrist="false" />
            OP：<uc1:SellsSelect ID="jd" SetTitle="OP选用" runat="server" selectfrist="false" />
            <%}%>
            <%else %>
            <%{%>
            导游：<uc1:SellsSelect ID="dy" SetTitle="导游选用" runat="server" selectfrist="false" />
            变更状态：<select name="sel">
              <option value="<%=(int)ChangeStatus.销售未确认 %>" <%=Request.QueryString["sel"]==((int)ChangeStatus.销售未确认).ToString()?"selected":"" %> ><%=ChangeStatus.销售未确认 %></option>
              <option value="<%=(int)ChangeStatus.计调未确认 %>" <%=Request.QueryString["sel"]==((int)ChangeStatus.计调未确认).ToString()?"selected":"" %>><%=ChangeStatus.计调未确认 %></option>
              <option value="<%=(int)ChangeStatus.销售暂不处理 %>" <%=Request.QueryString["sel"]==((int)ChangeStatus.销售暂不处理).ToString()?"selected":"" %>><%=ChangeStatus.销售暂不处理 %></option>
            </select>
            <%}%>
            变更时间：
            <input name="sd" value="<%=Request.QueryString["sd"] %>" type="text" class="formsize80 input-txt" onfocus="WdatePicker();"/>
            -
            <input name="ed" value="<%=Request.QueryString["ed"] %>" type="text" class="formsize80 input-txt" onfocus="WdatePicker();"/>
            <input class="search-btn" type="submit" id="btnSearch"/>
        </p>
        </span>  
            <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
          </div>
</form>
     
<!--列表表格-->
         <div class="tablelist-box">
           	  <table width="100%" id="liststyle">
				<tr>
                  <th class="thinputbg"><input type="checkbox" name="checkbox" id="checkbox" /></th>
                  <th align="center" >团号</th>
            <% if (this._sl == Menu2.计调中心_业务变更)%>
            <%{%>
                  <th align="center" >线路区域</th>
                  <th align="center" >线路名称</th>
                  <th align="center" >业务员</th>
                  <th align="center" >OP</th>
            <%}%>
            <%else %>
            <%{%>
                  <th align="center" >导游姓名</th>
            <%}%>
                  <th align="center" >变更时间</th>
            <% if (this._sl == Menu2.计调中心_业务变更)%>
            <%{%>
                  <th align="center" >变更计调项</th>
                  <th align="center" >变更人</th>
                  <th align="center" >变更标题</th>
            <%}%>
            <%else %>
            <%{%>
                  <th align="center" >变更内容</th>
            <%}%>
                  <th align="center" >状态</th>
            <% if (this._sl == Menu2.销售中心_导游变更)%>
            <%{%>
                  <th align="center" >操作</th>
            <%}%>
				</tr>
				<asp:Repeater runat="server" ID="rptList">
				<ItemTemplate>
                  <tr>
                    <td align="center"><input type="checkbox" name="checkbox" id="checkbox" /></td>
                    <td align="left"><a href="javascript:void(0);" id="fabu"><%#Eval("TourCode") %></a>
                        <div style="display: none">
                            <%# GetOperaterInfo(Eval("TourId").ToString())%>
                        </div>
                        <span><a target="_blank" <%# (ChangeStatus)Eval("State") == ChangeStatus.计调已确认 ? "class=fontgreen" : "class=fontred"%> href='<%#Eval("TourCode")==""?"javascript:void(0)":PrintUrl %>?tourid=<%# Eval("TourId") %>&type=1'>
                            <%#Eval("TourCode") == "" ? "" : "(变)"%></a></span>
                    </td>
            <% if (this._sl == Menu2.计调中心_业务变更)%>
            <%{%>
                    <td align="left"><%#Eval("AreaName") %></td>
                    <td align="left"><a href="javascript:void(0);"><%#Eval("RouteName") %></a></td>
                    <td align="left"><%#Eval("SellerName") %></td>
                    <td align="left"><%#Eval("Planer") %></td>
            <%}%>
            <%else %>
            <%{%>
                    <td align="left"><%#Eval("GuideNm") %></td>
            <%}%>
                    <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd}") %></td>
            <% if (this._sl == Menu2.计调中心_业务变更)%>
            <%{%>
                    <td align="center"><%#EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.HTourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%></td>
                    <td align="left"><%#Eval("Operator") %></td>
                    <td align="left"><a href="javascript:void(0);" name="a_confirm" data-id="<%#Eval("Id") %>" data-tourid="<%#Eval("tourid") %>"><%#Eval("Title") %></a></td>
            <%}%>
            <%else %>
            <%{%>
                    <td align="left"><%#Eval("Content") %></td>
            <%}%>
                    <td align="center"><b class="fontred"><%#Eval("State") %></b></td>
            <% if (this._sl == Menu2.销售中心_导游变更)%>
            <%{%>
                    <td align="center">
                        <a href="javascript:void(0);" data-class="xiaoshouqueren" data-id="<%#Eval("Id") %>" data-tourid="<%#Eval("tourid") %>" onclick="Page.SaleConfirm(this);"><img src="../images/qren_btn.gif" width="48" height="20" /></a>
                        <a href="javascript:void(0);" data-class="xiaoshouzanbuchuli" data-id="<%#Eval("Id") %>" data-tourid="<%#Eval("tourid") %>" onclick="Page.SaleConfirm(this);"><img src="../images/buchl_btn.gif" width="71" height="20" /></a>
                    </td>
            <%}%>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server" Visible="false"> 
                    <tr align="center">
                        <td colspan="15">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
       </div>
       <div class="tablehead border-bot">
        	<div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>             
        </div>
        </div>
        <script type="text/javascript">
var Page = {
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
			url: "/Plan/BianGengQueRen.aspx?",
			title: "查看变更内容",
			width: "600px",
			height: "350px"
		};
	},
	SaleConfirm: function(o) { /*销售确认*/
		var url = "/Plan/BianGeng.aspx?" + $.param({
				doType: $(o).attr("data-class"),
				sl: '<%=this.SL %>',
				TourId: $(o).attr("data-tourid"),
				Id: $(o).attr("data-id")
			});
		$.newAjax({
				type: "post",
				cache: false,
				url: url,
				dataType: "json",
				success: function(ret) {
					if (parseInt(ret.result) === 1) {
						parent.tableToolbar._showMsg('操作成功!', function() {
							location.href = location.href;
						});
					}
					else {
						parent.tableToolbar._showMsg(ret.msg);
						Page.PageInit();
					}
				},
				error: function() {
					//ajax异常--你懂得
					parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
				}
			});
	},
	PlanerConfirm: function(o) { /*计调确认*/
		var data = Page.DataBoxy();
		data.url += $.param({ sl: '<%=this.SL %>',id:$(o).attr("data-id"),tourid:$(o).attr("data-tourid") });
		Page.ShowBoxy(data);
		return false;
	},

	BindBtn: function() { /*绑定功能按钮*/
		//计调确认弹窗
		$("a[name=a_confirm]").click(function() {
			Page.PlanerConfirm(this);
		});
		tableToolbar.init({
				tableContainerSelector: "#liststyle", //表格选择器
				objectName: "记录"
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
</asp:Content>
