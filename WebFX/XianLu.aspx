<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XianLu.aspx.cs" Inherits="EyouSoft.WebFX.XianLu" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title>分销商订单中心</title>
<!-- InstanceEndEditable -->
<link href="Css/fx_style.css" rel="stylesheet" type="text/css" />
<link href="Css/boxynew.css" rel="stylesheet" type="text/css" />
<!-- InstanceBeginEditable name="head" --><!-- InstanceEndEditable -->
<script type="text/javascript" src="Js/jquery-1.4.4.js"></script>
<!--[if IE]><script src="Js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->
<script type="text/javascript" src="Js/bt.min.js"></script>
<script type="text/javascript" src="Js/jquery.boxy.js"></script>
<!--[if lte IE 6]><script src="Js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

<script type="text/javascript" src="Js/jquery.blockUI.js"></script>
<script type="text/javascript" src="Js/table-toolbar.js"></script>
<script type="text/javascript" src="Js/moveScroll.js"></script>
<script type="text/javascript" src="/Js/datepicker/WdatePicker.js"> </script>

<!--[if IE 6]>
<script type="text/javascript" src="Js/PNG.js" ></script>
<script type="text/javascript">
DD_belatedPNG.fix('*,div,img,a:hover,ul,li,p');
</script>
<![endif]-->

</head>

<body style="background:0 none;">
<uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" OrderClass="default orderformicon" />
<div class="list-main">
    <div class="list-maincontent">
       <div class="linebox-menu"><a href="javascript:void(0);" onclick="AcceptPlan.PrintPage();return false;" style="visibility: <%=this.HeadDistributorControl1.IsPubLogin?"hidden":"visibility" %>"><span> <%=(String)GetGlobalResourceObject("string", "打印")%></span></a></div>    
     <div class="hr_10"></div>
       <div class="listsearch">
       <form id="frm" method="GET">
       <%=(String)GetGlobalResourceObject("string", "线路区域")%>：
       <select name="a" id="a">
         <%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("a")), SiteUserInfo.CompanyId, (EyouSoft.Model.EnumType.SysStructure.LngType)EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("LgType")))%>
       </select>
       <%=(String)GetGlobalResourceObject("string", "日期")%>：
       <input name="sd" type="text" value="<%=Request.QueryString["sd"] %>" class="searchInput size68" onfocus="WdatePicker();"/> - <input name="ed" type="text" value="<%=Request.QueryString["ed"] %>" class="searchInput size68" onfocus="WdatePicker();"/> 
       <%=(String)GetGlobalResourceObject("string", "天数")%>：
       <input name="d" type="text" value="<%=Request.QueryString["d"] %>" class="searchInput size68"/>
       <a href="javascript:void(0);" id="btnSearch"><img src="<%=GetGlobalResourceObject("string","图片搜索链接") %>" /></a>
       <input type="hidden" name="LgType" value="<%=Request.QueryString["LgType"] %>"/>
       </form>
       </div>
       <div class="listtablebox">
         <table width="100%" border="0" cellpadding="0" cellspacing="0"  id="liststyle">
           <tr>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "编号")%></th>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "线路区域")%></th>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "线路名称")%></th>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "日期")%></th>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "天数")%></th>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "抵达城市")%></th>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "离开城市")%></th>
             <th colspan="4" align="center"><%=(String)GetGlobalResourceObject("string", "价格")%></th>
             <th rowspan="2" align="center"><%=(String)GetGlobalResourceObject("string", "操作")%></th>
           </tr>
           <tr>
             <td align="center" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "成人")%></td>
             <td align="center" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "儿童")%></td>
             <td align="center" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "领队")%></td>
             <td align="center" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "单房差")%></td>
           </tr>
           <asp:Repeater runat="server" ID="rptList">
           <ItemTemplate>
           <tr class="<%#Container.ItemIndex % 2 == 0 ? "" : "odd"%>">
             <td align="center"><%#Eval("TourCode") %></td>
             <td align="center"><%#GetAreaName(Eval("AreaId"))%></td>
             <td align="center"><%#Eval("RouteName") %></td>
             <td align="center"><%#Eval("LDate","{0:yyyy-MM-dd}")%>/<%#Eval("RDate", "{0:yyyy-MM-dd}")%></td>
             <td align="center"><%#Eval("TourDays") %></td>
             <td align="center"><%#Eval("ArriveCity")%></td>
             <td align="center"><%#Eval("LeaveCity")%></td>
             <td align="right" class="fontblue"><%#Eval("AdultPrice","{0:C2}")%></td>
             <td align="right" class="font-orange"><%#Eval("ChildPrice","{0:C2}")%></td>
             <td align="right" class="fontbsize12"><%#Eval("LeaderPrice","{0:C2}")%></td>
             <td align="right"><%#Eval("SingleRoomPrice","{0:C2}")%></td>
             <td align="center"><a href="/PrintPage/XingChengDan.aspx?TourId=<%#this.Eval("TourId")%>&LgType=<%=Request.QueryString["LgType"] %>" target="_blank" style="visibility: <%=this.HeadDistributorControl1.IsPubLogin?"hidden":"visibility" %>"><%=(GetGlobalResourceObject("string", "查看"))%></a></td>
           </tr>
           </ItemTemplate>
           </asp:Repeater>
          <asp:PlaceHolder ID="PhPage" runat="server">
            <tr>
                <td colspan="14" align="center" bgcolor="#f4f4f4">
                    <div class="pages">
                        <cc1:ExporPageInfoSelect runat="server" ID="ExporPageInfoSelect1" />
                    </div>
                </td>
            </tr>
          </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="litMsg" Visible="false">
                            <tr><td align='center' colspan='14'><%=GetGlobalResourceObject("string","暂无数据") %></td></tr>
                        </asp:PlaceHolder>
         </table>
</div>
       <div class="hr_10"></div>
    </div>
  </div>
        <script type="text/javascript">
        var AcceptPlan = {
        	Submit: function() {
        		$("#frm").submit();
        	},
        	PrintPage: function() {//打印
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
        	}
        };

        $(function() {
        	AcceptPlan.PageInit();
        });

    </script></body>
</html>