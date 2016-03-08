<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuanHaoXuanYong.aspx.cs" Inherits="EyouSoft.Web.CommonPage.TuanHaoXuanYong" %>
<%@ Register TagPrefix="cc1" Namespace="Adpost.Common.ExporPage" Assembly="ControlLibrary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD><TITLE>无标题文档</TITLE>
<META content="text/html; charset=gb2312" http-equiv="Content-Type">
<LINK rel="stylesheet" type="text/css" href="/css/style.css"/>
<LINK rel="stylesheet" type="text/css" href="/css/boxynew.css"/>

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <!--[if IE]><script src="/Js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/bt.min.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>
    <script type="text/javascript" src="/Js/utilsUri.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

</HEAD>

<BODY style="BACKGROUND: 0px 50%"><DIV class=alertbox-outbox>
<form method="GET">
    <TABLE style="MARGIN: 0px auto" cellSpacing=0 cellPadding=0 width="99%" bgColor=#e9f4f9 align=center>
        <TBODY>
            <TR>
                <TD bgColor=#b7e0f3 height=28 align=left>
                    团号： <INPUT id="th" class="formsize140 input-txt" type="text" name="th" value="<%=Request.QueryString["th"] %>"/> 
                    抵达日期： <INPUT id="sd" class="formsize80 input-txt" type="text" name="sd" value="<%=Request.QueryString["sd"] %>" onfocus="WdatePicker();"/> 
                    - <INPUT id='ed' class="formsize80 input-txt" type="text" name="ed" value="<%=Request.QueryString["ed"] %>" onfocus="WdatePicker();"/> 
                    <A id="submit_select" class="box_searchbtn" href="javascript:void(0);">查询</A>
                </TD>
            </TR>
        </TBODY>
    </TABLE>
</form>
<DIV class=hr_10></DIV>
<DIV style="MARGIN: 0px auto; WIDTH: 99%" id="div_tablist">
<TABLE border=0 cellSpacing=0 cellPadding=0 width="100%" align=center>
<TBODY>
<TR>
<TD class="alertboxTableT" height=25 width=40 align="middle">编号</TD>
<TD class="alertboxTableT" align="middle">团号</TD>
<TD class="alertboxTableT" align="middle">线路名称</TD></TR>
<TR>
<asp:Repeater runat="server" ID="rpt">
<ItemTemplate>
<tr>
<TD height=25 align="middle">
<%if (EyouSoft.Common.Utils.GetQueryStringValue("IsMultiple") == "1")
  { %>
<input type="radio" name="id" value="<%#Eval("TourId")%>" data-tourcode="<%#Eval("TourCode")%>"/>
<%}
  else
  { %>
<input type="checkbox" name="id" value="<%#Eval("TourId")%>"  data-tourcode="<%#Eval("TourCode")%>"/>
<%} %>
<%#Container.ItemIndex+1+(this.PageIndex-1)*this.PageSize %></TD>
<TD align="middle"><%#Eval("TourCode") %></TD>
<TD align="middle"><%#Eval("RouteName") %></TD>
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
</TR>
<TR>
<TD height=30 colSpan=3>
<DIV style="POSITION: relative; HEIGHT: 20px">
                    <div id="div_page" style="position: relative; height: 20px;">
                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                    </div>
</DIV></TD></TR></TBODY></TABLE>
</DIV>
<DIV class=alertbox-btn><A id="a_btn" hideFocus="false" href="javascript:void(0);"/><S class=xuanzhe></S>选 择</A></DIV></DIV>
    
<script type="text/javascript">
        var CommPageJsObj = {
        	_parentWindow: null, /*父级页面Window*/
        	_dataObj: { }, //交互数据集合
        	_queryString: { }, //URL传递参数集合
        	_setdataObj: function(d) {
        		var that = this;
        		that._dataObj.TourCode = d.TourCode.join(',');
        		that._dataObj.TourId = d.TourId.join(',');
        	},
        	_getdata: function(d, o) {
        		d.TourId.push($.trim($(o).val()));
        		d.TourCode.push($.trim($(o).attr("data-tourcode")));
        	},
        	SetReturn: function() { /*执行默认回调和自定义回调*/
        		var that = this;
        		var parents = that._parentWindow;
        		//判断是否是用用户控件调用该页面
        		if (parents[that._queryString["thisClientID"]] && parents[that._queryString["thisClientID"]]["BackFun"]) {
        			parents[that._queryString["thisClientID"]]["BackFun"](this._dataObj);
        		}
        		var callBackFun = this._queryString["callBackFun"];
        		//判断自定义回调,若没有则返回null
        		var callBackFunArr = callBackFun ? callBackFun.split('.') : null;
        		//存在回调函数
        		if (callBackFunArr) {
        			for (var item in callBackFunArr) {
        				if (callBackFunArr.hasOwnProperty(item)) { /*筛选掉原型链属性*/
        					parents = parents[callBackFunArr[item]];
        				}
        			}
        			parents(this._dataObj);
        		}
        	},
        	GetData: function() {
        		var that = this;
        		var data = { TourId: [], TourCode: [] }
        		if ($("#div_tablist :checkbox:checked").length > 0) {
        			$("#div_tablist :checkbox:checked").each(function() {
        				that._getdata(data, $(this));
        			})
        		}
        		else if ($("#div_tablist :radio:checked").length > 0) {
        			that._getdata(data, $("#div_tablist :radio:checked"));
        		}
        		that._setdataObj(data);

        	},
        	Select: function() { /*选择功能*/

        		this.GetData();
        		this.SetReturn();
        		parent.Boxy.getIframeDialog(this._queryString["iframeId"]).hide();
        		return false;
        	},
            Search: function() {
                this._queryString.th = $("#th").val();
                this._queryString.sd = $("#sd").val();
                this._queryString.ed = $("#ed").val();
            	window.location.href = "/CommonPage/TuanHaoXuanYong.aspx?" + $.param(this._queryString);
            },
        	InitData: function() {
        		var TourInfo =
                    this._parentWindow[this._queryString["thisClientID"]] &&
	                    this._parentWindow[this._queryString["thisClientID"]]["GetVal"] ?
                    	this._parentWindow[this._queryString["thisClientID"]]["GetVal"]() : { };
        		this._dataObj.TourCode = TourInfo.TourCode || "";
        		this._dataObj.TourId = TourInfo.TourId || Boxy.queryString("TourId") || "";
        	},
        	InitBtn: function() {
        		var that = this;
        		$("#submit_select").click(function() { /*查询按钮*/
        			that.Search();
        			return false;
        		});
        		$("#a_btn").click(function() { /*初始化 选择按钮*/
        			that.Select();
        			return false;
        		});
        	},
        	Init: function() { /*初始化*/
        		this._queryString = utilsUri.getUrlParams();
        		this._parentWindow = this._queryString["pIframeID"] ? window.parent.Boxy.getIframeWindow(this._queryString["pIframeID"]) : parent;
        		this.InitData();
        		this.InitBtn();
        	}
        };

        $(function() {
        	CommPageJsObj.Init();
        });
    </script>    
</BODY>
</html>
