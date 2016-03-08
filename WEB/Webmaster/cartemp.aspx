<%@ Page Title="" Language="C#" MasterPageFile="~/Webmaster/mpage.Master" AutoEventWireup="true"
    CodeBehind="cartemp.aspx.cs" Inherits="EyouSoft.Web.Webmaster.cartemp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="../Js/jquery.boxy.js" type="text/javascript"></script>

    <style type="text/css">
        .tblLB
        {
            border-top: 1px solid #ddd;
            border-left: 1px solid #ddd;
            width: 100%;
            margin-bottom: 10px;
        }
        .tblLB thead
        {
            text-align: center;
            background: #efefef;
            height: 20px;
        }
        .tblLB tfoot
        {
            text-align: center;
            height: 20px;
            background: #efefef;
        }
        .tblLB td
        {
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
            height: 25px;
            text-align: center;
        }
        .white
        {
            background-color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    车型管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <table cellspacing="0" cellpadding="0" class="tblLB">
        <thead>
            <tr class="white">
                <td>
                    座位数：<asp:TextBox ID="txtSitCount" runat="server" CssClass="input_text" Style="width: 150px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnSaveSit" runat="server" Text="保存" OnClick="btnSaveSit_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    序号
                </td>
                <td>
                    座位数
                </td>
                <td>
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rpt_setNumList" runat="server">
                <ItemTemplate>
                    <tr onmouseout="changeTrBgColor(this,'#ffffff')" onmouseover="changeTrBgColor(this,'#eeeeee')"
                        style="background: none repeat scroll 0% 0% rgb(255, 255, 255);">
                        <td>
                            <%#Container.ItemIndex+1 %>
                        </td>
                        <td>
                            <%#Eval("SeatNum")%>
                        </td>
                        <td>
                            <a id="delSeatNum" onclick="javascript:return confirm('确认删除?')" href="cartemp.aspx?dotype=delSeatNum&&id=<%#Eval("id")%>">
                                删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </thead>
    </table>
    <div style="width: 100%; height: 30px">
    </div>
    <table cellspacing="0" cellpadding="0" class="tblLB">
        <thead>
            <tr class="white">
                <td>
                    车型模板：
                </td>
                <td>
                    <asp:DropDownList ID="ddlSitCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="fu_fileLoad" name="fu_fileLoad" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btnSaveCarTemp" runat="server" Text="保存" OnClick="btnSaveCarTemp_Click" />
                </td>
            </tr>
            <tr onmouseout="changeTrBgColor(this,'#ffffff')" onmouseover="changeTrBgColor(this,'#eeeeee')"
                style="background: none repeat scroll 0% 0% rgb(255, 255, 255);">
                <td>
                    序号
                </td>
                <td>
                    座位数
                </td>
                <td>
                    缩略图
                </td>
                <td colspan="2">
                    操作
                </td>
            </tr>
        </thead>
        <asp:Repeater ID="rpt_tempList" runat="server">
            <ItemTemplate>
                <tr onmouseout="changeTrBgColor(this,'#ffffff')" onmouseover="changeTrBgColor(this,'#eeeeee')"
                    style="background: none repeat scroll 0% 0% rgb(255, 255, 255);">
                    <td>
                        <%#Container.ItemIndex+1 %>
                        <input class type="hidden" id="<%#Eval("Templateid") %>" />
                    </td>
                    <td>
                        <%#Eval("SeatNum") %>
                    </td>
                    <td>
                        <img height="50px" src="<%#Eval("FilePath") %>" />
                    </td>
                    <td>
                        <a target="_blank" href="setcarseat.aspx?tempId=<%#Eval("Templateid") %>">座位设置</a>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a id="_setDefault" onclick="javascript:return confirm('确定设置为默认？')" href="cartemp.aspx?dotype=setDefault&&tempid=<%#Eval("Templateid") %>&&seatId=<%#Eval("Id") %>">
                            设为默认</a>
                    </td>
                    <td style="text-align: center;">
                        <a id="delTemp" onclick="javascript:return confirm('确认删除?')" href="cartemp.aspx?dotype=delTemp&&tempid=<%#Eval("Templateid") %>">
                            删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </thead>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageRemark" runat="server">
1、新增座位数不可重复。
2、同一座位数下可增加多个车辆模版。
</asp:Content>
