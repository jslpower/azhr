<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysLxr.ascx.cs" Inherits="EyouSoft.Web.UserControl.GysLxr" %>
<div style="margin: 0 auto; width: 99%;" id="i_div_lxr">
    <span class="formtableT formtableT02">联系人</span>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd">
        <tr>
            <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                编号
            </td>
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                姓名
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                职务
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                电话
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                手机
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                生日
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                QQ
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                E-mail
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                MSN-SKYPE
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                传真
            </td>
            <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT i_lxrcaozuo">
                操作
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
        <tr class="tempRow">
            <td align="center" bgcolor="#FFFFFF" class="index">
                <%# Container.ItemIndex + 1%>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input type="hidden" name="txtLxrId" value="<%#Eval("LxrId") %>" />
                <input name="txtLxrName" type="text" class="formsize50 input-txt" value="<%#Eval("Name") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrZhiWu" type="text" class="formsize50 input-txt" value="<%#Eval("ZhiWu") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrTelephone" type="text" class="formsize80 input-txt" value="<%#Eval("Telephone") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrMobile" type="text" class="formsize80 input-txt" value="<%#Eval("Mobile") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrBirthday" type="text" class="formsize80 input-txt" value="<%#Eval("Birthday","{0:yyyy-MM-dd}") %>"
                    onfocus="WdatePicker()" />
                <input type="checkbox" name="txtLxrBitrhdayTiXing" style="border: none;" <%#IsTiXing(Eval("IsTiXing")) %> />
                提醒
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrQQ" type="text" class="formsize50 input-txt" value="<%#Eval("QQ") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrEmail" type="text" class="formsize80 input-txt" value="<%#Eval("Email") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrMSN" type="text" class="formsize80 input-txt" value="<%#Eval("MSN") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrFax" type="text" class="formsize80 input-txt" value="<%#Eval("Fax") %>"
                    maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF" class="i_lxrcaozuo">
                <a href="javascript:void(0)" class="addbtn">
                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                        class="delbtn">
                        <img src="/images/delimg.gif" width="48" height="20" /></a>
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>        
        <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
        <tr class="tempRow">
            <td align="center" bgcolor="#FFFFFF" class="index">
                1
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input type="hidden" name="txtLxrId" value="" />
                <input name="txtLxrName" type="text" class="formsize50 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrZhiWu" type="text" class="formsize50 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrTelephone" type="text" class="formsize80 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrMobile" type="text" class="formsize80 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrBirthday" type="text" class="formsize80 input-txt" onfocus="WdatePicker()" />
                <input type="checkbox" name="txtLxrBitrhdayTiXing" style="border: none;" />
                提醒
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrQQ" type="text" class="formsize50 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrEmail" type="text" class="formsize80 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrMSN" type="text" class="formsize80 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtLxrFax" type="text" class="formsize80 input-txt" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF" class="i_lxrcaozuo">
                <a href="javascript:void(0)" class="addbtn">
                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                        class="delbtn">
                        <img src="/images/delimg.gif" width="48" height="20" /></a>
            </td>
        </tr>
        </asp:PlaceHolder>
    </table>
</div>
