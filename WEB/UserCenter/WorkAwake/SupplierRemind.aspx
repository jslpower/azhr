<%@ Page Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="SupplierRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.SupplierRemind" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <div style="float: left;">
                <ul class="fixed">
                    <li><s class="orderformicon"></s><a href="LaborRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>"
                        hidefocus="true" class="ztorderform"><span>劳动合同</span></a></li>
                    <li><s class="orderformicon"></s><a href="SupplierRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>"
                        hidefocus="true" class="ztorderform de-ztorderform"><span>供应商合同</span></a></li>
                    <li><s class="orderformicon"></s><a href="CompanyRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>"
                        hidefocus="true" class="ztorderform"><span>公司合同</span></a></li>
                </ul>
                <ul class="fixed">
                    <li>&nbsp;&nbsp;&nbsp; <b class="fontred">提醒的日期设定：</b>设定提前<input type="text" size="20"
                        class="inputtext formsize40" valid="required|RegInteger" errmsg="请填写天数|天数必须大于0的整数!"
                        name="text" id="txtDay" value="<%=GetLastTime() %>">天提醒&nbsp;&nbsp;&nbsp;</li>
                    <img src="/images/baocunimg.gif" id="btnSave" width="48" height="20" align="top" /></ul>
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
                            供应商名称
                        </th>
                        <th align="center" class="th-line">
                            合同到期时间
                        </th>
                        <th align="center" class="th-line">
                            合同编号
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" />
                                    <%# Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%# Eval("Source")%>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.Utils.GetDateTime(Eval("MaturityTime").ToString()).ToShortDateString() == "1900-1-1" ? "" : EyouSoft.Common.Utils.GetDateTime(Eval("MaturityTime").ToString()).ToShortDateString()%>
                                </td>
                                <td align="center">
                                    <%# Eval("ContractNumber")%>
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
    </form>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({});
            $("#btnSave").click(function() {
                var day = $.trim($("#txtDay").val());
                if(ValiDatorForm.validator($(this).closest("form").get(0), "parent")){
                    $.newAjax({
                        type: "post",
                        cache: false,
                        dataType:"json",
                        url: "SupplierRemind.aspx?doType=SetLastTime&argument=" + day,
                        success: function(ret) {
                                tableToolbar._showMsg(ret.msg);
                        },
                        error: function() {
                            if(arguments[1]!=null)
                                tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            else
                                tableToolbar._showMsg("服务器忙");
                        }
                    });
                }
            })
            return false;
        })
        
    </script>

</asp:Content>
