<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanTuanGuaZhang.aspx.cs" Inherits="EyouSoft.Web.Fin.DanTuanGuaZhang" MasterPageFile="~/MasterPage/Front.Master"%>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="mainbox">
    <div class="searchbox border-bot fixed">
    	    <form method="GET">
    	    <span class="searchT">
    	    <p>团号：<input value="<%=Request.QueryString["th"] %>" name="th" type="text" class="formsize80 input-txt" />
                <input type="submit" class="search-btn"/></p>
                <input type="hidden" value="<%=this.SL %>" name="sl"/>
            </span>   	 
            </form>  
        <div class="pages" id="pages">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </div>
     </div>
     <!--列表表格-->
         <div class="tablelist-box">
           	  <table width="100%" id="liststyle">
				<tr>
                  <th class="thinputbg"><input type="checkbox" name="checkbox" id="checkbox" /></th>
                  <th align="center" >团号</th>
                  <th align="center" >线路区域</th>
                  <th align="left" >线路名称</th>
                  <th align="center" >抵达时间</th>
                  <th align="center" >天数</th>
                  <th align="left" >客户单位</th>
                  <th align="center" >人数</th>
                  <th align="center" >OP</th>
                  <th align="center" >业务员</th>
                  <th align="center" >团队状态</th>
                  <th align="center" >操作</th>
				</tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                      <tr>
                        <td align="center"><input type="checkbox" name="checkbox" id="checkbox" /></td>
                        <td align="center"><a href="javascript:void(0);" target="_blank"><%#Eval("TourCode") %></a></td>
                        <td align="center"><%#Eval("AreaName") %></td>
                        <td align="left"><a href="#"><%#Eval("RouteName")%></a></td>
                        <td align="center"><%#Eval("LDate","{0:yyyy-MM-dd}") %></td>
                        <td align="center"><%#Eval("TourDays")%></td>
                        <td align="left"><%#Eval("BuyCompanyName")%></td>
                        <td align="center"><b class="fontblue"><%#Eval("Adults")%></b><sup class="fontred"><%#Eval("Childs")%>+<%#Eval("Leaders")%></sup></td>
                        <td align="center"><%#Eval("Planers")%></td>
                        <td align="center"><%#Eval("SellerName")%></td>
                        <td align="center"><%#Eval("TourStatus")%></td>
                        <td align="center"><a href="DanTuanGuaZhangDetail.aspx?sl=<%=this.SL %>&tourid=<%#Eval("tourid") %>"><img src="../images/baozhang-cy.gif" /> 查看挂账</a></td>
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
            </table>
</div>
          <!--列表结束-->
          	<div id="tablehead_clone">
        </div>
     </div><!-- InstanceEndEditable -->
     
<script type="text/javascript">
var Page = {
	BindBtn: function() { /*绑定功能按钮*/
		tableToolbar.init({
				tableContainerSelector: "#liststyle", //表格选择器
				objectName: "记录"
			});
	},
	PageInit: function() {
		Page.BindBtn();
		$("#tablehead_clone").html($("#pages").clone(true).css("border-top", "0 none"));
	}
};

$(function() {
	Page.PageInit();
});

</script>     
</asp:Content>
