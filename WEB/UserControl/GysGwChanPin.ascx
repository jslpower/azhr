<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysGwChanPin.ascx.cs" Inherits="EyouSoft.Web.UserControl.GysGwChanPin" %>
<div style="margin: 0 auto; width: 99%;">
    <span class="formtableT formtableT02">产品信息</span>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="ChanPinAutoAdd">
        <tr>
            <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                编号
            </td>
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                产品名称
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                销售金额
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                返点金额
            </td>
            <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
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
                        <input type="hidden" name="txtChanPinId" value="<%#Eval("ChanPinId") %>" />
                        <input name="txtChanPinName" type="text" class="formsize80 input-txt" value="<%#Eval("Name") %>"
                            maxlength="50" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="txtChanPinXSJE" type="text" class="formsize80 input-txt" value="<%#Eval("XiaoShouJinE","{0:F2}") %>"
                            maxlength="10" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="txtChanPinFDJE" type="text" class="formsize80 input-txt" value="<%#Eval("FanDianJinE","{0:F2}") %>"
                            valid="isNumber" errmsg="请输入正确的流水" maxlength="10" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
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
                    <input type="hidden" name="txtChanPinId" value="" />
                    <input name="txtChanPinName" type="text" class="formsize80 input-txt" value="" maxlength="50" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtChanPinXSJE" type="text" class="formsize80 input-txt" value="" valid="isNumber"
                        errmsg="请输入正确的销售金额" maxlength="10" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtChanPinFDJE" type="text" class="formsize80 input-txt" value="" valid="isNumber"
                        errmsg="请输入正确的返点金额" maxlength="10" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <a href="javascript:void(0)" class="addbtn">
                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                            class="delbtn">
                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</div>
<style type="text/css">
    #heTongAutoAdd .progressWrapper
    {
        width: 200px;
        overflow: hidden;
    }
    #heTongAutoAdd .progressName
    {
        overflow: hidden;
        width: 150px; height:20px;
    }
</style>

<script type="text/javascript">
    $(document).ready(function() {
        $("#ChanPinAutoAdd").autoAdd({});
    });
</script>

