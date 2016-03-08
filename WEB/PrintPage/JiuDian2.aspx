<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiuDian2.aspx.cs" Inherits="EyouSoft.Web.PrintPage.JiuDian2" MasterPageFile="~/MasterPage/Print.Master" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <link href="/Css/print1.css" rel="stylesheet" type="text/css" />
<table width="696" border="0" cellpadding="0" cellspacing="0" class="Basic_Table" style="margin-top:5px;">
  <tr>
    <td height="40" align="left" class="BTitle">订房委托单</td>
    <td align="right" valign="middle" class="zhtai">委托状态：<select name="select" class="selectbk" id="select">
        <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanBGType)))%>
    </select></td>
  </tr>
</table>
<div id="i_div_to">
<div class="Basic_Table Tablebk_top bg01">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_00f font16">
    <td width="34%" height="40">TO：<asp:Literal ID="txtCompanyName" runat="server"></asp:Literal>/<asp:Literal ID="txtCompanyContactName" runat="server"></asp:Literal></td>
    <td width="22%">TEL：<asp:Literal ID="txtContact" runat="server"></asp:Literal></td>
    <td width="22%">FAX：<asp:Literal ID="txtFax" runat="server"></asp:Literal></td>
    <td width="22%">手机：<asp:Literal runat="server" ID="txtCompanyMob"></asp:Literal></td>
  </tr>
</table>
</div>

<div class="Basic_Table Tablebk_bot bg02">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_00f font16">
    <td width="34%" height="40">FROM：<asp:Literal ID="txtSelfName" runat="server"></asp:Literal>/<asp:Literal ID="txtSelfContactName" runat="server"></asp:Literal></td>
    <td width="22%">TEL：<asp:Literal ID="txtSelfContact" runat="server"></asp:Literal></td>
    <td width="22%">FAX：<asp:Literal ID="txtSelfFax" runat="server"></asp:Literal> </td>
    <td width="22%">手机：<asp:Literal runat="server" ID="txtSelfMob"></asp:Literal></td>
  </tr>
</table>
</div>
</div>
    <div class="Basic_Table Tablebk_top bg03" style="margin-top:5px;">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right" class="borderR">公司团号</th>
    <td width="150" class="borderR"><asp:Literal id="txtTuanHao" runat="server" /></td>
    <th width="90" align="right" class="borderR">用房数</th>
    <th align="left">
    <asp:Repeater runat="server" ID="rpt_PlanHotelRoomList">
        <ItemTemplate>
          <%#Eval("RoomType")%>(<%#Eval("Quantity") %>间)
          </ItemTemplate>
         </asp:Repeater>
</th>
    </tr>
  </table>
</div>

<div class="Basic_Table Tablebk_mid" style="margin-top:-8px;">
<table width="698" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right">人数</th>
    <td width="40" align="right">成人</td>
    <td width="24"><asp:Literal runat="server" ID="txtAdult"></asp:Literal></td>
    <td width="40" align="right">陪同</td>
    <td width="25"><asp:Literal runat="server" ID="txtPei"></asp:Literal></td>
    <th width="90" align="right">入住日期</th>
    <td><asp:Literal id="txtRuZhuShiJian" runat="server" /></td>
    </tr>
  <tr class="font_000 font16">
    <th align="right">国籍</th>
    <td colspan="4"><asp:Literal id="txtGuoJi" runat="server" /></td>
    <th align="right">离店日期</th>
    <td><asp:Literal id="txtLiDianShiJian" runat="server" /></td>
  </tr>
  <tr class="font_000 font16">
    <th align="right">付款方式</th>
    <td colspan="4"><asp:Literal id="txtFuKuanFangShi" runat="server"/></td>
    <th align="right">房价</th>
    <td><asp:Literal id="txtJieSuanJinE" runat="server"/></td>
  </tr>
  <tr class="font_000 font16">
    <th align="right">导游姓名</th>
    <td colspan="4"><asp:Literal id="txtGuideName" runat="server"/></td>
    <th align="right">手机号码</th>
    <td><asp:Literal id="txtGuideMobile" runat="server"/></td>
  </tr>
  </table>
</div>

<div class="Basic_Table Tablebk_bot bg04">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right" class="borderR">备注</th>
    <td><asp:Literal id="txtBeiZhu" runat="server"></asp:Literal></td>
    </tr>
</table>
</div>

<table width="696" border="0" cellpadding="0" cellspacing="0" class="Basic_Table">
  <tr>
    <td height="40" align="right" class="font16 font_00f"><%=DateTime.Now.Year %>&nbsp;年&nbsp;<%=DateTime.Now.Month %>&nbsp;月&nbsp;<%=DateTime.Now.Day %>&nbsp;日</td>
  </tr>
</table>
</asp:Content>