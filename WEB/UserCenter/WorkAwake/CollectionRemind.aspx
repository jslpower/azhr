<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="CollectionRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.CollectionRemind" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <div style="float: left; padding-top: 5px;">
            </div>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="th-line">
                            编号
                        </th>
                        <th align="center" class="th-line">
                            订单号
                        </th>
                        <th align="center" class="th-line">
                            欠款单位
                        </th>
                        <th align="center" class="th-line">
                            联系人
                        </th>
                        <th align="center" class="th-line">
                            电话
                        </th>
                        <th align="center" class="th-line">
                            应收金额
                        </th>
                        <th align="center" class="th-line">
                            欠款金额
                        </th>
                        <th align="center" class="th-line">
                            责任销售
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>"> 
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" />
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDingDanChaKan(Eval("tourid"), Eval("orderid"), Eval("ordertype"), Eval("OrderCode"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Customer")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Contact")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Phone")%>
                                </td>
                                <td align="center" class="blue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("confirmmoney"), this.ProviderToMoney)%>
                                </td>
                                <td align="center" class="red">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Arrear"), this.ProviderToMoney)%>
                                </td>
                                <td align="center">
                                    <%#Eval("SellerName")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="border: 0 none;" class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
    
    <script>
    $(function(){
    
        tableToolbar.init({});
    })
    
    </script>
    
    </form>
    
</asp:Content>
