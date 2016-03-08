<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="Web.Webmaster._menu" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
    <style type="text/css">
    ul{list-style: none;margin: 0px;padding: 0px;}
    ul li{list-style: none;}
    .m2{width: 100%;}
    .m2 li{float:left; line-height: 24px;width:20%; text-align:left}
    .tblmenus{border-top:1px solid #ddd;border-left:1px solid #ddd;width: 100%;margin-bottom: 10px;}    
    .tblmenus thead{text-align:left;background: #efefef; height:35px; font-size:14px;}
    .tblmenus td{border-right:1px solid #ddd;border-bottom:1px solid #ddd; height:35px;}
    
    .tdMenu1Name {text-align:left;font-weight: bold; width:100px;}
    .tdMenu1Name span{background:url(/images/menuicon.gif) no-repeat -9999px; position:relative; left:0px; top:0px;width:19px; height:18px; display:inline-block; margin-right:4px}
    .tdMenu1Name span.zutuan{ background-position:0 0;}
    .tdMenu1Name span.diejietd{ background-position:0 -27px;}
    .tdMenu1Name span.cjtd{ background-position:-2px -57px;}
    .tdMenu1Name span.tongyefx{ background-position:-2px -80px;}
    .tdMenu1Name span.dxyw{ background-position:0 -110px;}
    .tdMenu1Name span.jidiaozx{ background-position:0 -135px;}
    .tdMenu1Name span.daoyouzx{ background-position:0 -163px;}
    .tdMenu1Name span.ziyuanyk{ background-position:0 -190px;}
    .tdMenu1Name span.zilianggl{ background-position:0 -216px;}
    .tdMenu1Name span.kehugl{ background-position:0 -243px;}
    .tdMenu1Name span.ziyuangl{ background-position:0 -276px;}
    .tdMenu1Name span.hetonggl{ background-position:0 -297px;}
    .tdMenu1Name span.caiwugl{ background-position:0 -322px;}
    .tdMenu1Name span.tongjifx{ background-position:0 -352px;}
    .tdMenu1Name span.xingzzx{ background-position:0 -376px;}
    .tdMenu1Name span.xitongsz{ background-position:0 -406px;}
    .tdMenu1Name span.xsshoukuan{ background-position:0 -560px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    子系统一二级栏目管理-<asp:Literal runat="server" ID="ltrSysName"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">   
    <asp:PlaceHolder runat="server" ID="phSetDefaultMenus">
        <table cellpadding="2" cellspacing="1" style="width: 100%;">
            <tr>
                <td>
                    暂没有为该系统设置任何一二级栏目，你可以<asp:Button ID="btnSetDefaultMenus" runat="server" Text="导入系统默认一二级栏目" OnClick="btnSetDefaultMenus_Click" />，<a href="menuedit.aspx?sysid=<%=SysId %>&cid=<%=CompanyId %>&mid=0">点击这里可以添加一级栏目信息</a>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <table cellpadding="2" cellspacing="1" style="width: 100%;" class="tblmenus">
        <asp:Repeater runat="server" ID="rptMenus" OnItemDataBound="rptMenus_ItemDataBound">
            <HeaderTemplate>
                <thead>
                    <td colspan="3">
                        以下是该子系统已经设置的一二级栏目信息，<a href="menuedit.aspx?sysid=<%=SysId %>&cid=<%=CompanyId %>&mid=0">点击这里可以添加一级栏目信息</a>
                    </td>
                </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr onmouseover="changeTrBgColor(this,'#eeeeee')" onmouseout="changeTrBgColor(this,'#ffffff')">
                    <td class="tdMenu1Name">
                        <span class="<%#Eval("ClassName") %>">&nbsp;</span><%#Eval("Name") %>
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrMenu2s"></asp:Literal>
                    </td>
                    <td style="width: 60px; text-align: center">
                        <a href="menuedit.aspx?sysid=<%=SysId %>&cid=<%=CompanyId %>&mid=<%#Eval("MenuId") %>">修改</a>&nbsp;<a href="menu.aspx?sysid=<%=SysId %>&cid=<%=CompanyId %>&mid=<%#Eval("MenuId") %>&ot=1" onclick="return confirmDelete()">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">
    <ul class="decimal">
        <li>删除操作将删除该一级栏目下二级栏目及权限信息，请慎重操作。</li>
        <li>系统使用后，不要随意更改一二级栏目信息。</li>
    </ul>
</asp:Content>
