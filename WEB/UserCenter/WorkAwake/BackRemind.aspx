<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="BackRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.BackRemind" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
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
                <ul class="fixed">
                    <li>&nbsp;&nbsp;&nbsp; <span class="red">设定提前3天提醒</span> 设定提醒的天数：<input type="text"
                        size="20" class="formsize80" name="text" id="txtDay" />&nbsp;&nbsp;&nbsp;</li>
                    <li style="margin: 0px;"><a id="btnSave" class="ztorderform" style="padding-left: 2px;"
                        hidefocus="true" href="javascript:void(0);"><span>确定</span></a></li></ul>
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
                        <th align="center" class="th-line">
                            团号
                        </th>
                        <th align="center" class="th-line">
                            线路名称
                        </th>
                        <th align="center" class="th-line">
                            出团日期
                        </th>
                        <th align="center" class="th-line">
                            人数
                        </th>
                        <th align="center" class="th-line">
                            责任计调
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class='<%# Container.ItemIndex % 2 == 0 ? "highlight" : ""%>'>
                            </tr>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#Convert.ToDateTime( Eval("TourDate")).ToString("yyyy-MM-dd")%>
                            </td>
                            <td align="center">
                                <a href="gr-chutuantx-print.html">
                                    <%#Eval("RealNum")%></a>
                            </td>
                            <td align="center">
                                <%#Eval("Planers")%>
                            </td>
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
            $("#btnSave").click(function() {
                var day = $.trim($("#txtDay").val());
                if (day == "") {
                    tableToolbar._showMsg("请输入天数！");
                    return false;
                }
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "<%=this.Request.Url.ToString() %>?doType=update&day=" + day,
                    success: function(ret) {
                        if (ret == "OK") {
                            tableToolbar._showMsg("修改成功！");
                        }
                        else {
                            tableToolbar._showMsg("服务器忙！");
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙！");
                    }
                });
            })
            return false;
        })
        
    </script>

</asp:Content>
