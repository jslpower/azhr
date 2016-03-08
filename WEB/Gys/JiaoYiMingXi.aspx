<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiaoYiMingXi.aspx.cs" Inherits="EyouSoft.Web.Gys.JiaoYiMingXi"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox" id="alertbox" style="padding-top: 5px;">
        <div class="tanchuT" style="text-align: right">
            <a href="javascript:void(0)" class="toolbar_dayin" onclick="PrintPage('liststyle')">
                <img src="/images/dayin1-cy.gif" border="0"></a>&nbsp; <a href="javascript:void(0)"
                    class="toolbar_daochu" onclick="toXls1();return false;">
                    <img src="/images/daochu-cy.gif" border="0"></a>
        </div>
        <div style="width: 98%; margin: 0px auto;">
            <form action="" method="get">
            出团日期：
            <input type="hidden" name="sl" value="<%=SL %>" />
            <input type="hidden" name="gysid" value="<%=GysId %>" />
            <input type="hidden" name="gysname" value="<%=GysName %>" />
            <input type="text" name="txtLSDate" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLSDate") %>"
                class="inputtext" style="width: 65px;" onfocus="WdatePicker()" />
            -
            <input type="text" name="txtLEDate" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLEDate") %>"
                class="inputtext" style="width: 65px;" onfocus="WdatePicker()" />
            <input type="submit" value="搜索" class="search-btn" style="cursor: pointer; height: 24px;
                width: 64px; background: url(/images/cx.gif) no-repeat center center; border: 0 none;
                margin-left: 5px;" />
            </form>
        </div>
        <div class="tanchuT" style="margin-top: 10px;">
            【与 <b class="fontred">
                <%= GysName %></b> 的交易情况】</div>
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
            id="liststyle" style="margin: 0 auto">
            <tbody>
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;" class="odd">
                    <td height="28" bgcolor="#b7e0f3" align="center" class="alertboxTableT" style="width: 40px;">
                        序号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        团号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        线路名称
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT" style="width: 60px">
                        销售员
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT" style="width: 60px">
                        OP
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT" style="width: 60px">
                        导游
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT" style="width: 50px">
                        数量
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT" style="display:none">
                        费用明细
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 70px">
                        结算金额
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT" style="width: 70px">
                        未付金额
                    </td>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr bgcolor="F3F3F3">
                            <td align="center" style="height: 28px;">
                                <%#Container.ItemIndex + 1 %>
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#Eval("XiaoShouYuanName")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiDiaoYuanName")%>
                            </td>
                            <td align="center">
                                <%#Eval("DaoYouName")%>
                            </td>
                            <td align="center">
                                <%#Eval("ShuLiang")%>
                            </td>
                            <td align="center" style="display:none">
                                <%#Eval("FeiYongMingXi")%>
                            </td>
                            <td align="right">
                                <%#Eval("JinE","{0:C2}")%>
                            </td>
                            <td align="right">
                                <%#Eval("WeiZhiFuJinE","{0:F2}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="10" style="height: 28px;">
                            暂无交易明细
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phHeJi">
                    <tr bgcolor="#e9f4f9" class="odd">
                        <td style="height: 28px" align="right" class="alertboxTableT" colspan="6">
                            合计：
                        </td>
                        <td style="text-align: center" class="alertboxTableT">
                            <asp:Literal runat="server" ID="ltrShuLiangHeJi"></asp:Literal>
                        </td>
                        <td class="alertboxTableT" style="display:none;">
                        </td>
                        <td style="text-align: right" class="alertboxTableT">
                            <asp:Literal runat="server" ID="ltrJinEHeJi"></asp:Literal>
                        </td>
                        <td style="text-align: right" class="alertboxTableT">
                            <asp:Literal runat="server" ID="ltrWeiZhiFuJinEHeJi"></asp:Literal>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </tbody>
        </table>
        <div style="position: relative; height: 32px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
