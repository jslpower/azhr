<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysJiuDianBaoJia.ascx.cs" Inherits="EyouSoft.Web.UserControl.GysJiuDianBaoJia" %>
<div style="margin: 0 auto; width: 99%;" id="i_div_jiudianbaojia">
    <span class="formtableT formtableT02">报价信息</span>
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" style="margin: 0 auto;">
        <tr>
            <td width="25" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                单人
            </td>
            <td height="23" align="center" bgcolor="#FFFFFF">
                合同价
            </td>
            <td align="center" bgcolor="#FFFFFF">
                平：价格
                <input name="txtJDJGNTP" type="text" class="formsize50 input-txt" id="txtJDJGNTP"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGNTPBZ" type="text" class="formsize80 input-txt" id="txtJDJGNTPBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                淡：价格
                <input name="txtJDJGNTD" type="text" class="formsize50 input-txt" id="txtJDJGNTD"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGNTDBZ" type="text" class="formsize80 input-txt" id="txtJDJGNTDBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                旺：价格
                <input name="txtJDJGNTW" type="text" class="formsize50 input-txt" id="txtJDJGNTW"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGTNWBZ" type="text" class="formsize80 input-txt" id="txtJDJGNTWBZ"
                    runat="server" maxlength="50" />
            </td>
        </tr>
        <tr class="tempRow">
            <td align="center" bgcolor="#FFFFFF">
                零售价
            </td>
            <td align="center" bgcolor="#FFFFFF">
                平：价格
                <input name="txtJDJGNSP" type="text" class="formsize50 input-txt" id="txtJDJGNSP"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGNSPBZ" type="text" class="formsize80 input-txt" id="txtJDJGNSPBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                淡：价格
                <input name="txtJDJGNSD" type="text" class="formsize50 input-txt" id="txtJDJGNSD"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGNSDBZ" type="text" class="formsize80 input-txt" id="txtJDJGNSDBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                旺：价格
                <input name="txtJDJGNSW" type="text" class="formsize50 input-txt" id="txtJDJGNSW"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGNSWBZ" type="text" class="formsize80 input-txt" id="txtJDJGNSWBZ"
                    runat="server" maxlength="50" />
            </td>
        </tr>
        <tr>
            <td width="25" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                双人
            </td>
            <td height="23" align="center" bgcolor="#FFFFFF">
                合同价
            </td>
            <td align="center" bgcolor="#FFFFFF">
                平：价格
                <input name="txtJDJGWTP" type="text" class="formsize50 input-txt" id="txtJDJGWTP"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGWTPBZ" type="text" class="formsize80 input-txt" id="txtJDJGWTPBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                淡：价格
                <input name="txtJDJGWTD" type="text" class="formsize50 input-txt" id="txtJDJGWTD"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额"" />
                备注
                <input name="txtJDJGWTDBZ" type="text" class="formsize80 input-txt" id="txtJDJGWTDBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                旺：价格
                <input name="txtJDJGWTW" type="text" class="formsize50 input-txt" id="txtJDJGWTW"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGWTWBZ" type="text" class="formsize80 input-txt" id="txtJDJGWTWBZ"
                    runat="server" maxlength="50" />
            </td>
        </tr>
        <tr class="tempRow">
            <td align="center" bgcolor="#FFFFFF">
                零售价
            </td>
            <td align="center" bgcolor="#FFFFFF">
                平：价格
                <input name="txtJDJGWSP" type="text" class="formsize50 input-txt" id="txtJDJGWSP"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGWSPBZ" type="text" class="formsize80 input-txt" id="txtJDJGWSPBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                淡：价格
                <input name="txtJDJGWSD" type="text" class="formsize50 input-txt" id="txtJDJGWSD"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGWSDBZ" type="text" class="formsize80 input-txt" id="txtJDJGWSDBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                旺：价格
                <input name="txtJDJGWSW" type="text" class="formsize50 input-txt" id="txtJDJGWSW"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGWSWBZ" type="text" class="formsize80 input-txt" id="txtJDJGWSWBZ"
                    runat="server" maxlength="50" />
            </td>
        </tr>
        <tr>
            <td width="25" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                三人
            </td>
            <td height="23" align="center" bgcolor="#FFFFFF">
                合同价
            </td>
            <td align="center" bgcolor="#FFFFFF">
                平：价格
                <input name="txtJDJGSHP" type="text" class="formsize50 input-txt" id="txtJDJGSHP"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGSHPBZ" type="text" class="formsize80 input-txt" id="txtJDJGSHPBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                淡：价格
                <input name="txtJDJGSHD" type="text" class="formsize50 input-txt" id="txtJDJGSHD"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额"" />
                备注
                <input name="txtJDJGSHDBZ" type="text" class="formsize80 input-txt" id="txtJDJGSHDBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                旺：价格
                <input name="txtJDJGSHW" type="text" class="formsize50 input-txt" id="txtJDJGSHW"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGSHWBZ" type="text" class="formsize80 input-txt" id="txtJDJGSHWBZ"
                    runat="server" maxlength="50" />
            </td>
        </tr>
        <tr class="tempRow">
            <td align="center" bgcolor="#FFFFFF">
                零售价
            </td>
            <td align="center" bgcolor="#FFFFFF">
                平：价格
                <input name="txtJDJGSLP" type="text" class="formsize50 input-txt" id="txtJDJGSLP"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGSLPBZ" type="text" class="formsize80 input-txt" id="txtJDJGSLPBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                淡：价格
                <input name="txtJDJGSLD" type="text" class="formsize50 input-txt" id="txtJDJGSLD"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGSLDBZ" type="text" class="formsize80 input-txt" id="txtJDJGSLDBZ"
                    runat="server" maxlength="50" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                旺：价格
                <input name="txtJDJGSLW" type="text" class="formsize50 input-txt" id="txtJDJGSLW"
                    maxlength="10" runat="server" valid="isNumber" errmsg="请输入正确的金额" />
                备注
                <input name="txtJDJGSLWBZ" type="text" class="formsize80 input-txt" id="txtJDJGSLWBZ"
                    runat="server" maxlength="50" />
            </td>
        </tr>
    </table>
</div>
