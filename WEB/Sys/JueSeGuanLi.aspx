<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="JueSeGuanLi.aspx.cs" Inherits="EyouSoft.Web.Sys.JueSeGuanLi" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <div class="tablehead" id="btnHead">
            <ul class="fixed">
                <% if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_新增))
                   { %>
                <li><s class="addicon"></s><a class="add_xq" hidefocus="true" href="javascript:"><span>
                    添加</span></a></li>
                <li class="line"></li>
                <li><s class="updateicon"></s><a class="toolbar_copy" hidefocus="true" href="javascript:">
                    <span>复制</span></a></li>
                <%} %>
                <% if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_删除))
                   { %>
                <li class="line"></li>
                <li><s class="delicon"></s><a class="toolbar_delete" hidefocus="true" href="javascript:">
                    <span>删除</span></a></li><li class="line"></li>
                <%} %>
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr>
                        <th style="width: 35px" class="thinputbg">
                            <input type="checkbox" id="checkbox1" name="checkbox" disabled="disabled" />
                        </th>
                        <th width="264" align="center" class="th-line">
                            权限组名称
                        </th>
                        <th width="234" align="center" class="th-line">
                            操作
                        </th>
                        <th width="30" align="center">
                            &nbsp;
                        </th>
                        <th width="342" align="center" class="th-line">
                            权限组名称
                        </th>
                        <th width="234" align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <tr>
                        <cc1:CustomRepeater ID="repList" runat="server">
                            <ItemTemplate>
                                <td align="left">
                                    <input type="checkbox" value="<%#Eval("Id")%>" <%#Convert.ToBoolean(Eval("IsSystem")) ? "disabled=\"disabled\"" : ""%>
                                        name="chkRole" />
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%#Eval("RoleName")%>
                                </td>
                                <td align="center">
                                    <a class="update_xq" href="javascript:" roleid='<%#Eval("id")%>'>修改</a>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,i,2,3) %>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none;" class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("btnHead").innerHTML);
            </script>

        </div>
    </div>
    </form>

    <script type="text/javascript">
        var RoleList = {
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chkRole']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).attr("value"));
                    }
                });
                return arrayList;
            },

            SL: querystring(location.href, "sl"),

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

            Oper: function(type) {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Sys/JueSeGuanLi.aspx?dotype=" + type + "&ids=" + RoleList.GetSelectItemValue().toString() + "&sl=" + RoleList.SL,
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
            },

            Init: function() {
                tableToolbar.IsHandleElse = "true";
                tableToolbar.init({
                    tableContainerSelector: "#liststyle",
                    objectName: " 记录",
                    //默认按钮
                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(objsArr) {
                        //修改
                        var url = "JueSeTianjia.aspx?id=" + RoleList.GetSelectItemValue().toString() + "&sl=" + RoleList.SL;
                        RoleList.openXLwindow(url, "修改权限", "780px", "680px");
                        return false;
                    },
                    copyCallBack: function(objsArr) {
                        //复制
                        var url = "JueSeTianjia.aspx?state=copy&id=" + RoleList.GetSelectItemValue().toString() + "&sl=" + RoleList.SL;
                        RoleList.openXLwindow(url, "复制权限", "780px", "680px");
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        //删除
                        RoleList.Oper("del");
                        return false;
                    }
                })

                $(".add_xq").click(function() {
                    var url = 'JueSeTianjia.aspx?&sl=' + RoleList.SL;
                    RoleList.openXLwindow(url, "添加权限", "780px", "680px");
                    return false;
                });

                $(".update_xq").click(function() {
                    var url = "JueSeTianjia.aspx?&id=" + $(this).attr("RoleId") + "&sl=" + RoleList.SL;
                    RoleList.openXLwindow(url, "修改权限", "780px", "680px");
                    return false;
                });
            }
        };

        $(function() {
            RoleList.Init();
        });
    </script>

</asp:Content>
