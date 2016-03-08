<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="JiaoTong.aspx.cs" Inherits="EyouSoft.Web.PrintPage.JiaoTong"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
            <link href="/Css/print1.css" rel="stylesheet" type="text/css" />
<table width="696" border="0" cellpadding="0" cellspacing="0" class="Basic_Table" style="margin-top:5px;">
  <tr>
    <td height="40" align="left" class="BTitle">订交通工具委托单</td>
    <td align="right" valign="middle" class="zhtai">委托状态：<select name="select" class="selectbk" id="select">
        <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanBGType)))%>
    </select></td>
  </tr>
</table>
<div id="i_div_to">
        <div class="Basic_Table Tablebk_top bg01">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_00f font16">
    <td width="34%" height="40">TO：<asp:Literal ID="txtunitname" runat="server"></asp:Literal>/<asp:Literal ID="txtunitContactname" runat="server"></asp:Literal></td>
    <td width="22%">TEL：<asp:Literal ID="txtunittel" runat="server"></asp:Literal></td>
    <td width="22%">FAX：<asp:Literal ID="txtunitfax" runat="server"></asp:Literal></td>
    <td width="22%">手机：<asp:Literal runat="server" ID="txtCompanyMob"></asp:Literal></td>
  </tr>
</table>
</div>

<div class="Basic_Table Tablebk_bot bg02">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_00f font16">
    <td width="34%" height="40">FROM：<asp:Literal ID="txtsourcename" runat="server"></asp:Literal>/<asp:Literal ID="txtname" runat="server"></asp:Literal></td>
    <td width="22%">TEL：<asp:Literal ID="txttel" runat="server"></asp:Literal></td>
    <td width="22%">FAX：<asp:Literal ID="txtfax" runat="server"></asp:Literal> </td>
    <td width="22%">手机：<asp:Literal runat="server" ID="txtSelfMob"></asp:Literal></td>
  </tr>
</table>
</div>

    </div>
   
    <div class="Basic_Table Tablebk_top bg03" style="margin-top:5px;">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right" class="borderR">公司团号</th>
    <td width="150" class="borderR"><asp:Label ID="lbTourID" runat="server" Text=""></asp:Label></td>
    <th width="90" align="right" class="borderR">国籍</th>
    <td class="borderR"><asp:Label runat="server" ID="lblGuoJi"></asp:Label></td>
    <th width="100" align="right" class="borderR">人数</th>
    <td width="80"><asp:Literal runat="server" ID="txtPeople"></asp:Literal></td>
  </tr>
  </table>
</div>

<div class="Basic_Table Tablebk_mid" style="margin-top:-8px;">
<table width="698" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right">交通工具</th>
    <td width="150" class="borderR"><asp:Literal runat="server" ID="ltlJiao"></asp:Literal></td>
    <th width="90" align="right">单价</th>
    <td class="borderR"><asp:Literal runat="server" ID="ltlPrice"></asp:Literal></td>
    <th width="100" align="right" class="borderR">手续费/保险</th>
    <td width="80" class="borderR"><asp:Literal runat="server" ID="ltlShou"></asp:Literal></td>
    </tr>
  <tr class="font_000 font16">
    <th align="right">出发日期</th>
    <td class="borderR"><asp:Literal runat="server" ID="ltlChuDate"></asp:Literal></td>
    <th align="right">抵达城市</th>
    <td colspan="3" class="borderR"><asp:Literal runat="server" ID="ltlDiCheng"></asp:Literal></td>
  </tr>
  <tr class="font_000 font16">
    <th align="right">出发城市</th>
    <td class="borderR"><asp:Literal runat="server" ID="ltlChu"></asp:Literal></td>
    <th align="right">席位等级</th>
    <td colspan="3" class="borderR"><asp:Literal runat="server" ID="ltlXi"></asp:Literal></td>
  </tr>
  <tr class="font_000 font16">
    <th align="right">付款方式</th>
    <td class="borderR"><asp:Label runat="server" ID="lbPaymentType"></asp:Label></td>
    <th align="right">手机号码</th>
    <td colspan="3" class="borderR"><asp:Label runat="server" ID="lblMobile"></asp:Label></td>
  </tr>
  <tr class="font_000 font16">
    <th align="right">导游姓名</th>
    <td colspan="5" class="borderR"><asp:Label runat="server" ID="lblDaoYou"></asp:Label></td>
    </tr>
  </table>
</div>


<div class="Basic_Table Tablebk_bot bg04">
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr class="font_000 font16">
    <th width="90" align="right" class="borderR">备注</th>
    <td><asp:Label ID="LbRemark" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
</div>

<table width="696" border="0" cellpadding="0" cellspacing="0" class="Basic_Table">
  <tr>
    <td height="40" align="right" class="font16 font_00f"><%=DateTime.Now.Year %>&nbsp;年&nbsp;<%=DateTime.Now.Month %>&nbsp;月&nbsp;<%=DateTime.Now.Day %>&nbsp;日</td>
  </tr>
</table>

</asp:Content>
