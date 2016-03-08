<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="UserInfo.aspx.cs" Inherits="Web.UserCenter.SelfInfo.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <!--列表表格-->
        <div class="add-grzxbox">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td width="40%" height="35" align="right">
                            所属公司：
                        </td>
                        <td width="60%" align="left">
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="gr-addInput" ReadOnly="true"
                                BackColor="#dadada"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            所属部门：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="gr-addInput" ReadOnly="true"
                                BackColor="#dadada"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            用户名：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserLogin" runat="server" CssClass="gr-addInput" ReadOnly="true"
                                BackColor="#dadada"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            密码：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPwd" TextMode="Password" runat="server" CssClass="gr-addInput"
                                errmsg="请输入密码!" valid="required"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            姓名：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="gr-addInput gr-addInput140"
                                errmsg="请输入姓名!" valid="required"></asp:TextBox>
                            <span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            性 别：
                        </td>
                        <td align="left">
                            <asp:RadioButton ID="rdoBoy" runat="server" Text="男" Checked="true" />
                            <asp:RadioButton ID="rdoGirl" runat="server" Text="女" />
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            职 位：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPosition" runat="server" CssClass="gr-addInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            联系电话：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="gr-addInput gr-addInput140" errmsg="请输入联系电话!"
                                valid="required"></asp:TextBox>
                            <span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            传 真：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFax" runat="server" CssClass="gr-addInput gr-addInput140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            手 机：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="gr-addInput gr-addInput140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            Q Q：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtQQ" runat="server" CssClass="gr-addInput gr-addInput140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            MSN：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMsn" runat="server" CssClass="gr-addInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" align="right">
                            E-mail：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="gr-addInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="54" align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <div style="margin-left: 0;" class="mainbox cunline fixed">
                                <div class="hr_10">
                                </div>
                                <ul>
                                    <li class="cun-cy">
                                        <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return UserInfo.FormCheck();"
                                            OnClick="btnSave_Click">保存</asp:LinkButton>
                                    </li>
                                    <li class="quxiao-cy"><a href="javascript:void(0);">取消</a></li>
                                </ul>
                                <div class="hr_10">
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
    </div>
    </form>

    <script type="text/javascript">
        $(function() {
            FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
        })
        var UserInfo = {
            FormCheck: function() {
                var form = FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
                return ValiDatorForm.validator(form, "alert");
            }
        }    
    </script>

</asp:Content>
