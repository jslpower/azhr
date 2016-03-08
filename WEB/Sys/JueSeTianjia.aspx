<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="JueSeTianjia.aspx.cs"
    Inherits="Web.Sys.JueSeTianjia" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <title>系统设置-权限管理-添加</title>
</head>
<body style="background: #fff; ">
    <form id="form1" runat="server">
    <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="40" align="center" style="font-size: 14px">
                    <strong>权限组名称：</strong>
                    <input type="text" id="txtRoleName" name="txtRoleName" runat="server" class="inputtext formsize140" />
                </td>
            </tr>
            <tr>
                <td>
                    <ul style="padding-left: 18px; color: #666; line-height: 20px">
                        <li>权限类别-栏目：无栏目权限的用户看不到该二级栏目。</li>
                        <li>权限类别-本部浏览：可以查看自己所在部门内用户的数据（不含下级部门）。</li>
                        <li>权限类别-部门浏览：可以查看自己所在部门及下级部门内用户的数据（含下级部门）。</li>
                        <li>权限类别-内部浏览：可以查看自己所在部门的第一层级部门及下级部门内用户的数据（包含第一级部门及下级部门）。</li>
                        <li>权限类别-查看全部：可以查看所有用户的数据（包含所有第一级部门及下级部门）。</li>
                    </ul>
                </td>
            </tr>
        </tbody>
    </table>
    <div style="text-align: center; width: 97%; margin: 5px auto; background-color=#ffffff">
        <%=PowerStr %>
    </div>
    <div class="mainbox cunline fixed">
        <ul>
            <li class="cun-cy">
                <% if (IsAddOrUpdatePrivs)
                   { %>
                <a id="btnSave" href="javascript:">保存</a></li>
            <%} %>
            <li class="quxiao-cy"><a href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();">
                取 消</a></li>
        </ul>
        <div class="hr_10">
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var RolePowerConfig = {
            DoType:"<%=EyouSoft.Common.Utils.GetQueryStringValue("state")==""?"save":"copy" %>",
            UnBindBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").css("background-position", "0 -62px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click");
                $("#btnSave").text("保存");
                $("#btnSave").css("background-position", "0-31px");
                $("#btnSave").click(function() {
                    RolePowerConfig.Save();
                    return false;
                });
            },
            //提交表单
            Save: function() {
                $("#btnSave").text("提交中...");
                RolePowerConfig.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/JueSeTianjia.aspx?dotype="+RolePowerConfig.DoType+"&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&id=<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>",
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { window.parent.location.reload(); });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            RolePowerConfig.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后再试!");
                        RolePowerConfig.BindBtn();
                    }
                });
            }
        };
        $(function() {
            $("[name='chkAll']").click(function() {
                var MenuId = $(this).attr("value");
                var Checked = $(this).attr("checked");
                $("[MenuId='" + MenuId + "']").each(function() {
                    $(this).attr("checked", Checked);
                    var Menu2Id = $(this).attr("Menu2Id");
                    $("[Menu2Id=" + Menu2Id + "]").attr("checked", Checked);
                });
            });
            $("[name='chkMenu']").click(function() {
                var Menu2Id = $(this).attr("Menu2Id");
                $("[Menu2Id=" + Menu2Id + "]").attr("checked", $(this).attr("checked"));
            });

            $("#btnSave").click(function() {
                var msg = "";
                var IsSelected = false;
                var chkPower = document.getElementsByName("chkPower");
                if ($("#<%=txtRoleName.ClientID%>").val() == "") {
                    msg = "权限组名称不能为空!<br/>";
                }
                for (var i = 0; i < chkPower.length; i++) {
                    if (chkPower[i].checked == true) {
                        IsSelected = true;
                        break;
                    }
                }
                if (!IsSelected) {
                    msg = msg + "请选择权限!<br/>";
                }
                if (msg != "") {
                    tableToolbar._showMsg(msg);
                    return false;
                }
                RolePowerConfig.Save();
            });
        });
        
    </script>

</body>
</html>
