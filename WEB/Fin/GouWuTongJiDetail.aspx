<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GouWuTongJiDetail.aspx.cs" Inherits="EyouSoft.Web.Fin.GouWuTongJiDetail" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.4.js"></script>
    <!--[if IE]><script src="../js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->
    <script type="text/javascript" src="../js/table-toolbar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
<div class="alertbox-outbox">
  <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" style="margin:0 auto" id="liststyle">
    <tr>
        <td height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">序号</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">团号</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">出团日期</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">进店日期</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">业务员</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">人数</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">营业额</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">流水百分比</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">保底额</td>
        <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">流水额</td>
    </tr>
    <asp:Repeater ID="rpt_list" runat="server">
        <ItemTemplate>
              <tr>
                <td  align="center"><%#Container.ItemIndex+1+(this.PageIndex-1)*this.PageSize %></td>
                <td height="28" align="center"><%#Eval("TourCode") %></td>
                <td  align="center"><%#Eval("LDate", "{0:yyyy-MM-dd}")%></td>
                <td  align="center"><%#Eval("JinDianRiQi","{0:yyyy-MM-dd}") %></td>
                <td  align="center"><%#Eval("SellerName") %></td>
                <td  align="center"><font class="bfontred"><%#Eval("Adult") %><sup>+<%#Eval("Child") %></sup></font></td>
                <td  align="right"><b class="fontred"><%#Eval("YingYeE","{0:C2}") %></b></td>
                <td  align="right"><%#Eval("LiuShui", "{0:C2}")%></td>
                <td  align="right"><b class="fontgreen"><%#Eval("BaoDiE","{0:C2}")%></b></td>
                <td height="28"  align="right"><b class="fontred"><%#Eval("LiuShuiE","{0:C2}")%></b></td>
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
	  <tr>
	    <td height="23" colspan="10" align="right" class="alertboxTableT">
	        <div style="position:relative; height:32px;">
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                </div>
            </div>
        </td>
      </tr>
  </table> 
</div>
    </form>
</body>
<script type="text/javascript">
	$(function(){
		tableToolbar.init({
				tableContainerSelector: "#liststyle", //表格选择器
				objectName: "记录"
		});
	})
</script>
</html>
