<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="privs.aspx.cs" Inherits="Web.Webmaster._privs" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
    <style type="text/css">
    ul{list-style: none;margin: 0px;padding: 0px;}
    ul li{list-style: none;}
    .tblprivs{border-top:1px solid #ddd;border-left:1px solid #ddd;width: 100%;margin-bottom: 10px;}    
    .tblprivs .thead{text-align:left;background: #efefef; height:35px; font-size:14px;}
    .tblprivs td{border-right:1px solid #ddd;border-bottom:1px solid #ddd; height:35px;}
    .p1{font-weight:bold;line-height:30px;font-size:14px; clear:both; margin-top:10px; background:#eee}
    .p2{float:left;width:24%;}
    .p2 li{line-height:22px}
    .p2 li.p2title{font-weight:bold;line-height:24px;}
    .p2space{clear:both;width:100%; height:10px;}
    .p3{}
    .pcode{color:#ff0000; font-weight:normal;}
    </style>
    
    <script type="text/javascript">
        //初始化已经设置的权限信息
        function initPrivs() {
            if (comMenus == null || comMenus.length == 0) return;
            for (var i = 0; i < comMenus.length; i++) {
                if (comMenus[i].Menu2s == null || comMenus[i].Menu2s.length < 1) continue;
                for (var j = 0; j < comMenus[i].Menu2s.length; j++) {
                    if (comMenus[i].Menu2s[j].Privs == null || comMenus[i].Menu2s[j].Privs.length < 1) continue;
                    $("#chk_p_2_" + comMenus[i].Menu2s[j].MenuId).attr("checked", true);
                    $("#chk_p_1_" + comMenus[i].MenuId).attr("checked", true);
                    for (var k = 0; k < comMenus[i].Menu2s[j].Privs.length; k++) {
                        $("#chk_p_3_" + comMenus[i].Menu2s[j].Privs[k].PrivsId).attr("checked", true);
                    }
                }
            }
        }

        $(document).ready(function() {
            initPrivs();

            //一级栏目(1)checkbox添加事件，全选或取消子栏目及所有权限
            $(".p1 input[type='checkbox']").bind("click", function() {
                $(this).parent().next().find("input[type='checkbox']").attr("checked", this.checked);
            });

            //二级栏目(2)checkbox添加事件，全选或取消全选所有权限，选中后并选中栏目
            $(".p2title input[type='checkbox']").bind("click", function() {
                $(this).parent().parent().find("input[type='checkbox']").attr("checked", this.checked);
                if (this.checked) {
                    $(this).parent().parent().parent().prev().find("input[type='checkbox']").attr("checked", true);
                }
            });

            //权限(3)checkbox添加事件，选中后选中子栏目及栏目
            $(".p3 input[type='checkbox']").bind("click", function() {
                if (!this.checked) return;
                $(this).parent().parent().find("li:eq(0)").find("input[type='checkbox']").attr("checked", true);
                $(this).parent().parent().parent().prev().find("input[type='checkbox']").attr("checked", true);
            });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    子系统权限管理-<asp:Literal runat="server" ID="ltrSysName"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">
    <asp:PlaceHolder runat="server" ID="phUnsetMenuMsg">
        <table cellpadding="2" cellspacing="1" style="width: 100%;">
            <tr>
                <td>
                    没有为该子系统设置过任何一二级栏目信息，不能进行权限设置操作，请先设置好子系统一二级栏目。
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phPrivs">
    <table cellpadding="2" cellspacing="1" style="width: 100%;" class="tblprivs">
        <tr class="thead">
            <td colspan="2">
                <asp:Button ID="btnSetSysPrivs" runat="server" Text="设置子系统权限" OnClick="btnSetSysPrivs_Click" />&nbsp;
                <asp:Button ID="btnSetAdminRoleBySys" runat="server" Text="设置管理员角色权限为子系统权限" OnClick="btnSetAdminRoleBySys_Click" />&nbsp;
                <%--<asp:Button ID="btnSetAdminPrivsBySys" runat="server" Text="设置管理员账号权限为子系统权限" OnClick="btnSetAdminPrivsBySys_Click" />--%>
                <asp:Button ID="btnSetAdminRoleByAdminRole" runat="server" Text="设置管理员为管理员角色" OnClick="btnSetAdminRoleByAdminRole_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal runat="server" ID="ltrPrivs"></asp:Literal>
            </td>
        </tr>
    </table>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">
    <ul class="decimal">
        <li>权限类别-栏目：无栏目权限的用户看不到该二级栏目。</li>
        <li>权限类别-本部浏览：可以查看自己所在部门内用户的数据（不含下级部门）。</li>
        <li>权限类别-部门浏览：可以查看自己所在部门及下级部门内用户的数据（含下级部门）。</li>
        <li>权限类别-内部浏览：可以查看自己所在部门的第一层级部门及下级部门内用户的数据（包含第一级部门及下级部门）。</li>
        <li>权限类别-查看全部：可以查看所有用户的数据（包含所有第一级部门及下级部门）。</li>
    </ul>
</asp:Content>
