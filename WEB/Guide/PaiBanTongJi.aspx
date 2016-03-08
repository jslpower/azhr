<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaiBanTongJi.aspx.cs" Inherits="EyouSoft.Web.Guide.PaiBanTongJi"%>
<%@ Import Namespace="EyouSoft.Common" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script type="text/javascript" src="/Js/bt.min.js"></script>

    <script type="text/javascript" src="/js/datepicker/wdatepicker.js"></script>
</head>
<body style="background-color: #fff">
<div class="mainbox">
        <form id="formSearch" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=Request.QueryString["sl"]%>" />
                    <select name="seleYear" id="seleYear" class="inputselect">
                        <%=this.GetYearHtml()%>
                    </select>
                    年
                    <select name="seleMonth" id="seleMonth" class="inputselect">
                        <option value="0">-请选择-</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                    </select>
                    月 团&nbsp;&nbsp;&nbsp;&nbsp;号：
                    <input type="text" class="inputtext" size="20" name="th" value="<%=Request.QueryString["th"] %>" />
                    出团时间：<input class="inputtext" type="text" size="20" name="sd" id="sd"
                        onfocus="WdatePicker();" value="<%=Request.QueryString["sd"] %>" />
                    -<input class="inputtext" type="text" size="20" name="ed" id="ed"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'sd\',{M:1})}'});" value="<%=Request.QueryString["ed"] %>" />
                    团态标识：<input class="inputtext" type="text" size="20" name="tt" id="tt"
                        value="<%=Request.QueryString["tt"] %>" />
                    <input type="submit" id="search" class="search-btn" value="" /></p>
            </span>
        </div>
        <script type="text/javascript">
            function setValue(obj, v) {
                for (var i = 0; i < obj.options.length; i++) {
                    if (obj.options[i].value == v) {
                        obj.options[i].selected = true;
                    }
                }
            }
            setValue($("#seleYear")[0], '<%=Year %>');
            setValue($("#seleMonth")[0], '<%=Month %>');
        </script>
        </form>
        <%--<div class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>--%>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <%=this.GetHead() %>
                </tr>
                <%--<asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <%#GetTable(Utils.GetDateTime(Eval("ldate").ToString()), Utils.GetDateTime(Eval("rdate").ToString()), Eval("tourcode"), Eval("tourmark"), Eval("salemark"))%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>--%>
                <asp:Literal runat="server" ID="ltlList"></asp:Literal>
                <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            </table>
        </div>
        <!--列表结束-->
        <%--<div class="tablehead" style="border: 0 none;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>--%>
    </div>
</body>
<script type="text/javascript">
$(function() {
	$(".a_bt").bt({
			contentSelector: function() {
				return $(this).next().html();
			},
			positions: ['left', 'right', 'bottom'],
			fill: '#FFF2B5',
			strokeStyle: '#D59228',
			noShadowOpts: { strokeStyle: "#D59228" },
			spikeLength: 10,
			spikeGirth: 15,
			width: 200,
			overlap: 0,
			centerPointY: 1,
			cornerRadius: 4,
			shadow: true,
			shadowColor: 'rgba(0,0,0,.5)',
			cssStyles: { color: '#00387E', 'line-height': '180%' }
		});
});
</script>
</html>