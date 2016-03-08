<%@ Page Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="UpdatePassword.aspx.cs" Inherits="Web.UserCenter.SelfInfo.UpdatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <!--列表表格-->
        <div class="add-grzxbox">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="40%" height="35" align="right">
                        原始密码：
                    </td>
                    <td width="60%" align="left">
                        <asp:TextBox ID="txtPwd" TextMode="Password" runat="server" CssClass="inputtext formsize140"
                            errmsg="请输入密码!" valid="required"></asp:TextBox>
                        <span class="fontred">(必填)</span>
                    </td>
                </tr>
                <tr>
                    <td height="35" align="right">
                        新密码：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtNewPwd" runat="server" CssClass="inputtext formsize140" errmsg="请输入新密码!"
                            valid="required"></asp:TextBox>
                        <span class="fontred">(必填)</span>
                    </td>
                </tr>
                <tr>
                    <td height="35" align="right">
                        确认密码：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSurePwd" runat="server" CssClass="inputtext formsize140" errmsg="请输入确认密码!"
                            valid="required"></asp:TextBox>
                        <span class="fontred">(必填)</span>
                    </td>
                </tr>
                <tr>
                    <td height="54" align="right">
                        &nbsp;
                    </td>
                    <td align="left">
                        <div class="mainbox cunline fixed" style="margin-left: 0; text-align:left">
                            <div class="hr_10">
                            </div>
                            <ul>
                                <li class="cun-cy"><a href="javascript:void(0)" id="btnsave">保存</a></li>
                                <li class="quxiao-cy"><a href="../../Default.aspx?sl=0"id="btncancel">取消</a></li>
                            </ul>
                            <div class="hr_10">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--列表结束-->
    </div>
    </form>

    <script type="text/javascript">
        $(function() {
            UserInfo.BindBtn();
        })
        var UserInfo = {
            FormCheck: function() {
                var form  = $("#btnsave").closest("form").get(0);
                FV_onBlur.initValid(form);
                return ValiDatorForm.validator(form, "alert");
            },
            Save: function() {
                if (UserInfo.FormCheck()) {
                    var newpwd= $("#<%=txtNewPwd.ClientID %>").val();
                    var surepwd= $("#<%=txtSurePwd.ClientID %>").val();
                    if(newpwd!=surepwd)
                    {
                        tableToolbar._showMsg("新密码和确认密码不相同");
                        $("#<%=txtSurePwd.ClientID %>").focus();
                        return;
                    }
                    $("#btnsave").attr("class", "cun-cyactive")
                    $("#btnsave").html("修改中...");
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "UpdatePassword.aspx?doType=AjaxPwd",
                        data: $("#btnsave").closest("form").serialize(),
                        dataType:"json",
                        success: function(ret) {
                            tableToolbar._showMsg(ret.msg);
                            UserInfo.BindBtn();
                        },
                        error: function() {
                            tableToolbar._showMsg("服务器忙");
                            UserInfo.BindBtn();
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnsave").click(function() {
                    UserInfo.Save();
                    return false;
                })
                $("#btnsave").attr("class", "");
                $("#btnsave").html("保存");
            }
        }
    </script>

</asp:Content>
