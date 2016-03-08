<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="YongHuGuanLi.aspx.cs" Inherits="EyouSoft.Web.Sys.YongHuGuanLi" %>

<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="form1" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                    部门：
                    <uc1:SelectSection ID="BelongDepart" runat="server" SetTitle="所属部门" SModel="1" />
                    姓名：<input type="text" class=" inputtext formsize120" name="name" id="txtName" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("name") %>" />
                    用户名：<input type="text" class=" inputtext  formsize120" name="username" id="txtUserName"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("username") %>" />
                    <button class="search-btn" type="submit">
                        搜索</button></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="temptablehead">
            <ul class="fixed">
                <% if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_新增))
                   { %>
                <li><s class="addicon"></s><a class="toolbar_add add_gg" hidefocus="true" href="javascript:">
                    <span>添加</span></a></li>
                <li class="line"></li>
                <%}%>
                <% if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改))
                   { %>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <%}
                %>
                <% if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_删除))
                   { %>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                </s><span>删除</span></a></li>
                <li class="line"></li>
                <%}
                    
                %>
                <% if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改))
                   { %>
                <li><s class="qyicon"></s><a class="toolbar_Start" hidefocus="true" href="javascript:">
                    <span>启用</span></a></li>
                <li class="line"></li>
                <%} %>
                <% if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改))
                   { %>
                <li><s class="cancelicon"></s><a class="toolbar_cancel" hidefocus="true" href="javascript:">
                    <span>停用</span></a></li>
                <li class="line"></li>
                <%} %>
                <%--                <li><s class="cancelicon"></s><a class="toolbar_logout" hidefocus="true" href="javascript:"
                    onclick="UserList.UserLogout()"><span>下线</span></a></li>
                <li class="line"></li>--%>
            </ul>
            <div class="pages">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg">
                            <input type="checkbox" id="checkbox1" name="checkbox" />
                        </th>
                        <th align="center" class="th-line">
                            部门名称
                        </th>
                        <th align="center" class="th-line">
                            姓名
                        </th>
                        <th align="center" class="th-line">
                            用户名
                        </th>
                        <th align="center" class="th-line">
                            职位
                        </th>
                        <th align="center" class="th-line">
                            角色
                        </th>
                        <th align="center" class="th-line">
                            电话
                        </th>
                        <th align="center" class="th-line">
                            手机
                        </th>
                        <th align="center" class="th-line">
                            传真
                        </th>
                        <th align="center" class="th-line">
                            QQ
                        </th>
                        <th align="center" class="th-line">
                            用户状态
                        </th>
                        <th align="center" class="th-line">
                            登录状态
                        </th>
                    </tr>
                    <cc1:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" id="<%#Eval("UserId")%>" value="<%#Eval("UserId")%>" name="chk">
                                </td>
                                <td align="center">
                                    <%#Eval("DeptName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("UserName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("DeptName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("RoleName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactTel")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactMobile")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactFax")%>
                                </td>
                                <td align="center">
                                    <span class="th-line">
                                        <%#Eval("QQ")%></span>
                                </td>
                                <td align="center" class="">
                                    <%# Eval("UserStatus")%>
                                </td>
                                <td align="center">
                                    <%#GetOnlineStatus(Eval("OnlineStatus").ToString())%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </cc1:CustomRepeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none;" class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("temptablehead").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var UserList = {
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).attr("id"));
                    }
                });
                return arrayList;
            },
            sl: querystring(location.href, "sl"),
            openXLwindow: function(url, title, width, height) {
                url = url;
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: width,
                    height: height
                });
            },
            //            //强制用户下线
            //            UserLogout: function() {
            //                var count = tableToolbar.getSelectedColCount();
            //                if (count > 0) {
            //                    tableToolbar.ShowConfirmMsg("确定强制下线选中的用户吗？", function(obj) {
            //                        UserList.Oper("logout");
            //                        return false;
            //                    })
            //                } else {
            //                    tableToolbar._showMsg("未选中任何用户")
            //                }

            //            },
            Init: function() {
                tableToolbar.IsHandleElse = "true";
                tableToolbar.init({
                    objectName: "用户",
                    otherButtons: [{
                        button_selector: '.toolbar_Start',
                        sucessRulr: 2,
                        msg: '未选中任何用户 ',
                        buttonCallBack: function(obj) {
                            UserList.Oper("start");
                            return false;
                        }
}],
                        updateCallBack: function(obj) {
                            var url = "YongHuGuanLiBJ.aspx?sl=" + UserList.sl + "&id=" + UserList.GetSelectItemValue().toString();
                            UserList.openXLwindow(url, "修改用户", "850px", "410px");
                            return false;
                        },
                        deleteCallBack: function(objsArr) {
                            if (UserList.GetSelectItemValue().length > 1) {
                                tableToolbar._showMsg("一次只能删除一个用户数据！");
                                return false;
                            }

                            UserList.Oper("del");
                            return false;
                        },
                        cancelCallBack: function(objsArr) {
                            UserList.Oper("stop");
                            return false;
                        }
                    });
                    $(".add_gg").click(function() {
                        var url = "YongHuGuanLiBJ.aspx?sl=" + UserList.sl;
                        UserList.openXLwindow(url, "添加用户", "850px", "410px");
                        return false;
                    });
                    $(".add_sq").click(function() {
                        var UserId = $(this).attr("UserId");
                        var RoleId = $(this).attr("roleId");
                        var url = "UserAuthorize.aspx?id=" + UserId + "&sl=" + UserList.sl + "&roleId=" + RoleId;
                        UserList.openXLwindow(url, "授权用户", "780px", "680px");
                        return false;
                    })
                },
                //执行启用，停用，删除操作
                Oper: function(type) {
                    $.newAjax({
                        type: "get",
                        cache: false,
                        url: "/Sys/YongHuGuanLi.aspx?dotype=" + type + "&ids=" + UserList.GetSelectItemValue().toString() + "&sl=" + UserList.sl,
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                tableToolbar._showMsg(ret.msg, function() {
                                    window.location.href = window.location.href;
                                });
                            } else {
                                tableToolbar._showMsg(ret.msg);
                            }
                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    });
                }
            }
            $(function() {
                UserList.Init();
            });
    </script>

</asp:Content>
