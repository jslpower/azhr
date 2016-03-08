<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WuPinFaFangXX.aspx.cs"
    Inherits="EyouSoft.Web.Gys.WuPinFaFangXX" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <div class="alertbox-outbox">
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr>
                <td width="5%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td width="10%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    发放时间
                </td>
                <td width="9%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    签收人
                </td>
                <td width="8%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    数量
                </td>
                <td width="9%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    单价
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    总价
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    用途说明
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td align="center">
                            <%#Eval("ShiJian","{0:yyyy-MM-dd}")%>
                        </td>
                        <td align="center">
                            <%#Eval("LingYongRenName")%>
                        </td>
                        <td align="center">
                            <%#Eval("ShuLiang")%>
                        </td>
                        <td align="right">
                            <%#Eval("DanJia","{0:F2}")%>
                        </td>
                        <td height="28" align="right">
                            <%#Eval("ZongJia","{0:F2}")%>
                        </td>
                        <td align="left">
                            <%#Eval("YongTu")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                    <td colspan="20">
                        暂无发放信息
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </div>
</asp:Content>
