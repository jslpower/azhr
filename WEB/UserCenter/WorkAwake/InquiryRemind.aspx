<%@ Page Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="InquiryRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.InquiryRemind" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="th-line">
                        序号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="left" class="th-line">
                        询价单位
                    </th>
                    <th align="center" class="th-line">
                        天数
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        报价员
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                            <td align="center">
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <td align="left">
                                <%# Eval("RouteName")%>
                            </td>
                            <td align="left">
                                <%# Eval("BuyCompanyName")%>
                            </td>
                            <td align="center">
                                <%# Eval("Days")%>
                            </td>
                            <td align="center">
                                <%# Eval("PersonNum")%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%# Eval("Operator")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" onclick="GotoAddPrice('<%# Eval("QuoteId")%>','<%# Eval("QuoteType")%>')">
                                    报价</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="border: 0 none;" class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
    
    function GotoAddPrice(id,t)
    {
        var sl='<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
        var type="";
        switch(t)
        {
            case'<%=EyouSoft.Model.EnumType.TourStructure.ModuleType.组团 %>':
            type="1";
            break;
            case'<%=EyouSoft.Model.EnumType.TourStructure.ModuleType.地接 %>':
            type="2";
            break;
            case'<%=EyouSoft.Model.EnumType.TourStructure.ModuleType.出境 %>':
            type="3";
            break;
        }
        var strUrl="/TeamCenter/AddPrice.aspx?id="+id+"&act=forOper&type="+type+"&sl="+sl;
        window.location.href=strUrl;
    }
    $(function(){
        tableToolbar.init({});
    })
    </script>

</asp:Content>
