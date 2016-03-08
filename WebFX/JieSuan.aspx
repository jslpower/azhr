<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JieSuan.aspx.cs" Inherits="EyouSoft.WebFX.JieSuan" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title>分销商财务管理</title>
<!-- InstanceEndEditable -->
<link href="Css/fx_style.css" rel="stylesheet" type="text/css" />
<link href="Css/boxynew.css" rel="stylesheet" type="text/css" />
<!-- InstanceBeginEditable name="head" --><!-- InstanceEndEditable -->
<script type="text/javascript" src="Js/jquery-1.4.4.js"></script>
<!--[if IE]><script src="Js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->
<script type="text/javascript" src="Js/bt.min.js"></script>
<script type="text/javascript" src="Js/jquery.boxy.js"></script>
<!--[if lte IE 6]><script src="Js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
<script type="text/javascript" src="/js/utilsuri.js"></script>
<script type="text/javascript" src="Js/jquery.blockUI.js"></script>
<script type="text/javascript" src="Js/table-toolbar.js"></script>
<script type="text/javascript" src="Js/moveScroll.js"></script>

<!--[if IE 6]>
<script type="text/javascript" src="Js/PNG.js" ></script>
<script type="text/javascript">
DD_belatedPNG.fix('*,div,img,a:hover,ul,li,p');
</script>
<![endif]-->

</head>

<body style="background:0 none;">
<uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" FinanceClass="default cawuglicon" />
<div class="list-main">
    <div class="list-maincontent">
       <div class="linebox-menu"><a href="javascript:void(0);" onclick="AcceptPlan.PrintPage();return false;"><span> <%=(String)GetGlobalResourceObject("string", "打印")%></span></a>
       <a href="javascript:void(0);" id="i_a_toxls"><span> <%=(String)GetGlobalResourceObject("string", "导出")%></span></a></div>    
       <div class="hr_10"></div>
       <div class="listsearch">
       <form method="GET" id="frm">
       <%=(String)GetGlobalResourceObject("string", "团号")%>：<input name="th" type="text" value="<%=Request.QueryString["th"] %>" class="searchInput"/> 
       <%=(String)GetGlobalResourceObject("string", "状态")%>：
         <select name="s" id="s">
           <%=GetFinancialStatus(EyouSoft.Common.Utils.GetQueryStringValue("s"))%>
         </select>
         <a href="javascript:void(0);" id="btnSearch"><img src="<%=GetGlobalResourceObject("string","图片搜索链接") %>" /></a>
         <input type="hidden" name="LgType" value="<%=Request.QueryString["LgType"] %>"/>
         </form>
       </div>
       <div class="listtablebox">
       		<table width="100%" border="0" cellpadding="0" cellspacing="0"  id="liststyle">
              <tr>
                <th><%=(String)GetGlobalResourceObject("string", "编号")%></th>
                <th><%=(String)GetGlobalResourceObject("string", "团号")%></th>
                <th><%=(String)GetGlobalResourceObject("string", "抵达日期")%></th>
                <th><%=(String)GetGlobalResourceObject("string", "应付金额")%></th>
                <th><%=(String)GetGlobalResourceObject("string", "已付金额")%></th>
                <th><%=(String)GetGlobalResourceObject("string", "未付金额")%></th>
              </tr>
              <asp:Repeater runat="server" ID="rptList">
              <ItemTemplate>
              <tr class='<%#Container.ItemIndex%2==0?"odd":"" %>'>
                <td align="center"><%#Container.ItemIndex + 1 + (pageIndex - 1) * pageSize%></td>
                <td align="center"><%#Eval("TourCode")%></td>
                <td align="center"><%#Eval("LDate", "{0:yyyy-MM-dd}")%></td>
                <td align="right"><strong class="fontbsize12"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Receivable"), this.ProviderToMoney)%></strong></td>
                <td align="right"><strong class="font-green"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Received"), this.ProviderToMoney)%></strong></td>
                <td align="right"><strong class="fontblue"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Receivable")) - Convert.ToDecimal(Eval("Received")), this.ProviderToMoney)%></strong></td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
          <asp:PlaceHolder ID="PhPage" runat="server">
          <tr>
                                <td align="right" colspan="3">
                                    <strong><%=(String)GetGlobalResourceObject("string", "合计金额")%>：</strong>
                                </td>
                                <td align="right">
                                    <strong class="fontbsize12">
                                        <asp:Literal ID="LtTotalSumPrice" runat="server"></asp:Literal></strong>
                                </td>
                                <td align="right">
                                    <strong class="font-green">
                                        <asp:Literal ID="LtTotalReceived" runat="server"></asp:Literal></strong>
                                </td>
                                <td align="right">
                                    <strong class="fontblue">
                                        <asp:Literal ID="LtTotalUnReceived" runat="server"></asp:Literal></strong>
                                </td>
                            </tr>
            <tr>
                <td colspan="14" align="center" bgcolor="#f4f4f4">
                    <div class="pages">
                        <cc1:ExporPageInfoSelect runat="server" ID="ExporPageInfoSelect1" />
                    </div>
                </td>
            </tr>
          </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="litMsg" Visible="false">
                            <tr><td align='center' colspan='14'><%=GetGlobalResourceObject("string", "暂无数据")%></td></tr>
                        </asp:PlaceHolder>
         </table>
</div>
       <div class="hr_10"></div>
    </div>
  </div>
    <div id="printNone" style="display: none">
    </div>
    <input id="hidPrintHTML" name="hidPrintHTML" type="hidden" />
    <input id="hidDocName" name="hidDocName" type="hidden" runat="server" value="" />
  <script type="text/javascript">
        var AcceptPlan = {
        	Submit: function() {
        		$("#frm").submit();
        	},
        	PrintPage: function() { //打印
        		if (window.print != null) {
        			if (window["PrevFun"] != null) window["PrevFun"]();

        			$(".top,.pub,.linebox-menu,.listsearch").hide();
        			window.print();

        			//还原页面内容
        			window.setTimeout(function() {
        				$(".top,.pub,.linebox-menu,.listsearch").show();
        			}, 1000);

        		} else {
        			alert("没有安装打印机");
        		}
        	},
        	PageInit: function() {

        		//查询列表
        		$("#btnSearch").click(function() {
        			AcceptPlan.Submit();
        		});

        		//Enter搜索
        		$("#frm").find(":text").keypress(function(e) {
        			if (e.keyCode == 13) {
        				AcceptPlan.Submit();
        				return false;
        			}
        		});
        		tableToolbar.init();toXls.init({ "selector": "#i_a_toxls" });
        	}
        };

        $(function() {
        	AcceptPlan.PageInit();
        });
    </script>
  </body>

</html>
