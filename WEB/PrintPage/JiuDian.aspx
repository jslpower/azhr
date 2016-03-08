<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiuDian.aspx.cs" Inherits="EyouSoft.Web.PrintPage.JiuDian" MasterPageFile="~/MasterPage/Print.Master" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
<div id="i_div_to">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
      <tr>
        <td align="right" width="6%">To:</td>
        <td align="left" width="25%"><asp:TextBox ID="txtCompanyName" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox ID="txtCompanyContactName" runat="server" CssClass="input100"></asp:TextBox></td>
        <td align="right" width="10%">Supplier Tel:</td>
        <td align="left" width="10%"><asp:TextBox ID="txtContact" runat="server" CssClass="input100"></asp:TextBox></td>
        <td align="right" width="5%">Fax:</td>
        <td align="left" width="10%"><asp:TextBox ID="txtFax" runat="server" CssClass="input100"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="right">From:</td>
        <td align="left"><asp:TextBox ID="txtSelfName" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox ID="txtSelfContactName" runat="server" CssClass="input100"></asp:TextBox></td>
        <td align="right">Tel:</td>
        <td align="left"><asp:TextBox ID="txtSelfContact" runat="server" CssClass="input100"></asp:TextBox></td>
        <td align="right">Fax:</td>
        <td align="left"><asp:TextBox ID="txtSelfFax" runat="server" CssClass="input100"></asp:TextBox> </td>
      </tr>
</table>
</div>
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0" >
  <tr>
    <th height="40" colspan="2" align="center"><b class="font24">Voucher</b></th>
  </tr>
    <tr>
    <th height="30" colspan="2" align="right" style="padding-right:30px;">Date:<%=DateTime.Now.ToShortDateString() %></th>
  </tr>
</table>
<table width="696" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="#000000" class="list_2" style="margin-top:10px;">
      <tr>
        <th align="right" bgcolor="#EAEAEA" style="width:10%">File Number</th>
        <td align="left" colspan="3"><asp:Literal id="txtTuanHao" runat="server" /></td>
        <%if (1 == 0) %>
        <%{ %>
          <th align="right" bgcolor="#EAEAEA">国籍 </th><td align="left"><asp:Literal id="txtGuoJi" runat="server" /></td>
          <%} %>
        </tr>
        <tr>
        <th  align="right" valign="middle" nowrap="nowrap" bgcolor="#EAEAEA" >Check in</th>
        <td align="left">
        <asp:Literal id="txtRuZhuShiJian" runat="server" />
        </td>
        <th  align="right" valign="middle" nowrap="nowrap" bgcolor="#EAEAEA" >Check out</th>
        <td align="left">
        <asp:Literal id="txtLiDianShiJian" runat="server" />
        </td>
        </tr>
      <tr>
        <th  align="right" valign="middle" nowrap="nowrap" bgcolor="#EAEAEA" >Room Type</th>
        <td align="left" colspan="3">
        <asp:Repeater runat="server" ID="rpt_PlanHotelRoomList">
        <ItemTemplate>
          <%#Eval("RoomType") %>：
          <%--单价：<%#Eval("UnitPrice","{0:C2}") %>--%>
          <%#Eval("Quantity") %>
          <%--金额：<%#Eval("Total","{0:C2}") %>--%><br/>
          </ItemTemplate>
         </asp:Repeater>
        </td>
      </tr>
      <%if (1 == 0) %>
      <%{ %>
      <tr>
        <th  align="right" valign="middle" nowrap="nowrap" bgcolor="#EAEAEA" >免房数量</th>
        <td align="left"><asp:Literal id="txtMianFangShuLiang" runat="server" /></td>
        <th align="right" bgcolor="#EAEAEA">免房金额</th>
        <td align="left"><asp:Literal id="txtMianFangJinE" runat="server"/></td>
      </tr>
      <%} %>
      <tr>
        <th  align="right" valign="middle" nowrap="nowrap" bgcolor="#EAEAEA" >Breakfast including</th>
        <td colspan="3" align="left"><asp:Literal id="txtShiFouHanZao" runat="server"/></td>
      </tr>
      <tr>
        <th align="right" bgcolor="#EAEAEA" >Note</th>
        <td colspan="3" align="left"><asp:Literal id="txtBeiZhu" runat="server"></asp:Literal></td>
      </tr>
      <tr>
      <%if (1 == 0) %>
      <%{ %>
        <th align="right" bgcolor="#EAEAEA" >结算金额 </th>
        <td align="left"><asp:Literal id="txtJieSuanJinE" runat="server"/></td>
        <%} %>
          <th align="right" bgcolor="#EAEAEA">Payment</th>
          <td align="left" colspan="3"><asp:Literal id="txtFuKuanFangShi" runat="server"/></td>
      </tr>
      <tr>
        <th align="right" bgcolor="#EAEAEA" >Name</th>
        <td align="left"><asp:Literal id="txtGuideName" runat="server"/></td>
        <th align="right" bgcolor="#EAEAEA">Mobile</th>
        <td align="left"><asp:Literal id="txtGuideMobile" runat="server"/></td>
      </tr>
    </table>
<table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td height="120" align="center" style="width: 50%">
                <div id="div1">
                    </div>
            </td>
            <td colspan="3" align="center">
                Sign by
            </td>
        </tr>
    </table>
</asp:Content>