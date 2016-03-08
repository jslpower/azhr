<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FenPeiZhangHao.aspx.cs"
    Inherits="Web.CrmCenter.FenPeiZhangHao" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户关系账号管理</title>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server" method="post">
    <div class="alertbox-outbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="stop"></s><a href="javascript:void(0)" onclick="javascript:crmUser.setStatus('stop')"
                    class="toolbar_cancel"><span>停用</span></a> </li>
                </li><li class="line"></li>
                <li><s class="qyicon"></s><a href="javascript:void(0)" onclick="javascript:crmUser.setStatus('enable')"
                    class="toolbar_cancel"><span>启用</span></a> </li>
                </li><li class="line"></li>
                <li><s class="hmdicon"></s><a href="javascript:void(0)" onclick="javascript:crmUser.setStatus('block')"
                    class="toolbar_cancel"><span>黑名单</span></a></li>
            </ul>
        </div>
        <div class="tablelist-box">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="liststyle">
                <tr>
                    <th align="center" bgcolor="#B7E0F3" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" style="border: none;" />
                    </th>
                    <th height="23" align="center" bgcolor="#B7E0F3" class="th-line">
                        部门
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        地址
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        联系人
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        电话
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        传真
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        手机
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        生日
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        QQ
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        账号
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        密码
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        状态
                    </th>
                    <th align="center" bgcolor="#B7E0F3" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rtpLxrs" runat="server" OnItemDataBound="rptLxrs_ItemDataBound">
                    <ItemTemplate>
                        <tr class="selector_lxr">
                            <td style="text-align: center">
                                <input type="checkbox" name="chkUserId" value='<%# Eval("UserId") %>' />
                                <input type="hidden" name="txtLxrId" value="<%#Eval("LinkManId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("Department") %>
                            </td>
                            <td align="center">
                                <%# Eval("Address") %>
                            </td>
                            <td align="center">
                                <%# Eval("Name") %>
                            </td>
                            <td align="center">
                                <%# Eval("Telephone") %>
                            </td>
                            <td align="center">
                                <%# Eval("Fax") %>
                            </td>
                            <td align="center">
                                <%# Eval("Mobile")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("BirthDay"), this.ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%# Eval("QQ") %>
                            </td>
                            <td align="center">
                                <asp:Literal runat="server" ID="ltrUserName"></asp:Literal>
                            </td>
                            <td align="center">
                                <input type="password" name="txtPwd" class="formsize80 inputtext" style="width: 65px;"
                                    maxlength="20" />
                            </td>
                            <td align="center">
                                <asp:Literal runat="server" ID="ltrStatus"></asp:Literal>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="selector_save">保存</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" onclick="crmUser.closeWindow();" class="close" hidefocus="true">
                <s></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var crmUser = {
            //查询参数
            winParams: {}
            //关闭窗口
            , closeWindow: function() {
                var _win = top || window;
                _win.Boxy.getIframeDialog('<%=Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            }
            //用户名验证，返回false可用
            , isexists: function(_username) {
                var _retCode = true;
                $.newAjax({ type: "post"
                    , cache: false
                    , url: '/ashx/isexistsusername.ashx'
                    , data: { "companyid": "<%=CurrentUserCompanyID %>", "username": _username }
                    , dataType: "json"
                    , async: false
                    , success: function(ret) {
                        if (ret.result == "1") {
                            _retCode = false;
                        }
                    }
                });

                return _retCode;
            }
            //设置用户状态
            , setStatus: function(_status) {
                var userids = [];

                $("input[name='chkUserId']:checked").each(function() {
                    var userid = $(this).val();
                    if (userid.length > 0) userids.push(userid);
                });

                if (userids.length == 0) {
                    parent.tableToolbar._showMsg("请选择已经开启过的账号进行状态设置！");
                    return false;
                }

                parent.tableToolbar.ShowConfirmMsg("你确定要做状态设置吗?", function() {
                    var params = { "sl": crmUser.winParams["sl"], "doType": "updateuserstatus" };

                    $.newAjax({ type: "post"
                    , cache: false
                    , url: Boxy.createUri("FenPeiZhangHao.aspx", params)
                    , data: { "userids": userids.join(","), "status": _status, "crmid": '<%=Utils.GetQueryStringValue("crmid") %>' }
                    , dataType: "json"
                    , async: false
                    , success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg("状态设置成功!", function() {
                                window.location.href = window.location.href;
                            });
                        } else if (ret.result == "0") {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                    }
                    });
                });
            }
            , bindSaveBtn: function(obj) {
                var _self = this;
                $(obj).text("保存");
                $(obj).bind("click", function() { _self.save(obj); });
            }
            , unbindSaveBtn: function(obj) {
                $(obj).unbind("click");
                $(obj).text("...");
            }
            //保存信息
            , save: function(obj) {
                this.unbindSaveBtn(obj);
                var parenttr = $(obj).closest("tr");
                var userid = parenttr.find("input[name='chkUserId']").val();
                var lxrid = parenttr.find("input[name='txtLxrId']").val();
                var username = $.trim(parenttr.find("input[name='txtUsername']").val());
                var pwd = $.trim(parenttr.find("input[name='txtPwd']").val());

                if (userid.length == 0 && username.length == 0) {
                    parent.tableToolbar._showMsg("请输入用户名！");
                    this.bindSaveBtn(obj);
                    return false;
                }

                if (pwd.length == 0) {
                    parent.tableToolbar._showMsg("请输入密码！");
                    this.bindSaveBtn(obj);
                    return false;
                }

                if (userid.length == 0 && this.isexists(username)) {
                    parent.tableToolbar._showMsg("用户名重复");
                    this.bindSaveBtn(obj);
                    return false;
                }

                var params = { "doType": "setcrmuser", "sl": this.winParams["sl"] };
                var data = { "crmid": '<%=Utils.GetQueryStringValue("crmid") %>', "lxrid": lxrid, "username": username, "pwd": pwd };

                $.newAjax({ type: "post"
                    , cache: false
                    , url: Boxy.createUri("FenPeiZhangHao.aspx", params)
                    , data: data
                    , dataType: "json"
                    , async: false
                    , success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg("操作成功", function() {
                                window.location.href = window.location.href;
                            });
                        } else if (ret.result == "-1") {
                            parent.tableToolbar._showMsg("用户名重复", function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            parent.tableToolbar._showMsg("操作失败", function() {
                                window.location.href = window.location.href;
                            });
                        }

                    }
                });
            }
            //初始化
            , init: function() {
                var _self = this;
                this.winParams = Boxy.getUrlParams();

                tableToolbar.init({
                    objectName: "信息",
                    otherButtons: []
                });

                $(".selector_lxr").each(function() {
                    if ($(this).find("input[name = 'chkUserId']").val().length == 0) {
                        $(this).find("input[name='txtUsername']").val('');
                    }
                    $(this).find("input[name='txtPwd']").val('');
                });

                $(".selector_save").each(function() {
                    _self.bindSaveBtn(this);
                });
            }
        };

        $(document).ready(function() {
            crmUser.init();
        });
    </script>

</body>
</html>
