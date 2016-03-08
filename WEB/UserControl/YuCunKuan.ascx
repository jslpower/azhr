<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YuCunKuan.ascx.cs" Inherits="EyouSoft.Web.UserControl.YuCunKuan" %>
<div style="margin: 0 auto; width: 99%;">
    <span class="formtableT formtableT02">预存款信息</span>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="YuCunKuan">
        <tr>
            <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                编号
            </td>
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                存款时间
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                存款金额
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                备注
            </td>
            <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT i_hetongcaozuo">
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
                <input type="hidden" name="txtYuCunId" value="<%#Eval("YuCunId") %>" />
                <input name="txtTime" type="text" class="formsize80 input-txt" value="<%#Eval("Time","{0:yyyy-MM-dd}") %>"
                    onfocus="WdatePicker()" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtJinE" type="text" class="formsize80 input-txt" value="<%#Eval("JinE","{0:F2}") %>"
                    valid="isMoney" errmsg="请输入正确的存款金额" maxlength="10" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtBeiZhu" type="text" class="formsize240 input-txt" value="<%#Eval("BeiZhu") %>"
                    maxlength="255" />
            </td>
            <td align="center" bgcolor="#FFFFFF" class="i_hetongcaozuo">
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
                <input type="hidden" name="txtYuCunId" value="" />
                <input name="txtTime" type="text" class="formsize80 input-txt" onfocus="WdatePicker()"/>
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtJinE" type="text" class="formsize80 input-txt" valid="isMoney"
                    errmsg="请输入正确的存款金额" maxlength="10" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtBeiZhu" type="text" class="formsize240 input-txt" maxlength="255" />
            </td>
            <td align="center" bgcolor="#FFFFFF" class="i_hetongcaozuo">
                <a href="javascript:void(0)" class="addbtn">
                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                        class="delbtn">
                        <img src="/images/delimg.gif" width="48" height="20" /></a>
            </td>
        </tr>
        </asp:PlaceHolder>
    </table>
</div>

<script type="text/javascript">

    $(document).ready(function() {
        $("#YuCunKuan").autoAdd();
    });

</script>