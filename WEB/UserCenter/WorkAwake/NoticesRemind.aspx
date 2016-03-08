<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="NoticesRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.NoticesRemind" %>

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
                <ul class="fixed">
                    <li>&nbsp;&nbsp;&nbsp; <span class="red">设置提前<input type="text" size="20" class="formsize80"
                        name="text" id="txtDay">
                        天提醒</span>&nbsp;&nbsp;&nbsp;</li>
                    <li style="margin: 0px;"><a id="btnSave" class="ztorderform" style="padding-left: 2px;"
                        hidefocus="true" href="javascript:void(0);"><span>确定</span></a></li>
                </ul>
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
                            公告标题
                        </th>
                        <th align="center" class="th-line">
                            发布人
                        </th>
                        <th align="center" class="th-line">
                            发布时间
                        </th>
                        <th align="center" class="th-line">
                            阅读数
                        </th>
                        <th align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="">
                            <input type="hidden" name="ItemUserID" />
                                <td align="center">
                                    <%#Eval("NoticeId")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Title") %>
                                </td>
                                <td align="center">
                                    <%#Eval("Operator")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Views")%>
                                </td>
                                <td align="center">
                                    <a title="查看" class="check-btn sk_ck" href="gr-gonggtx-ck.html"></a>
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
</asp:Content>
