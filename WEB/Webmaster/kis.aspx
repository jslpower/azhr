<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kis.aspx.cs" Inherits="Web.Webmaster._kis" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
    <script type="text/javascript">
        function WebForm_OnSubmit_Validate() {
            return true;
        }

        //获取科目下拉菜单HTML
        function getKeMuHtml() {            
            var s = [];
            s.push('<option value="">请选择金蝶科目</option>');
            
            if (kisAccountGroups.length == 0) return s.join('')
            
            for (var i = 0; i < kisAccountGroups.length; i++) {
                s.push('<option value="' + kisAccountGroups[i].Code + '">' + kisAccountGroups[i].Name + '</option>');
            }
            
            return s.join('')
        }

        //初始化单个科目下拉菜单，selectName:下拉菜单名称，v:选中值。
        function initKeMu(selectName, v) {
            var obj$ = $("select[name='" + selectName + "']");
            obj$.append(getKeMuHtml());
            obj$.attr("value", v);
        }
        
        //初始化科目
        function init() {
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预付账款_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预付账款_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预收账款_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预收账款_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_团队借款_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_团队借款_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_现金_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_现金_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务收入_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务收入_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队借款_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队借款_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队预支_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队预支_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队支出_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队支出_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应付账款_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应付账款_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_贷 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_贷 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务成本_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务成本_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_预收账款_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_预收账款_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_银行存款_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_银行存款_借 %>"]);
            initKeMu("txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应收帐款_借 %>", kisConfig["Kis<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应收帐款_借 %>"]);
        }

        $(document).ready(function() {
            init();
        });
        
    </script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    金蝶默认科目配置-<asp:Literal runat="server" ID="ltrSysName"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">
    <table cellpadding="2" cellspacing="1" style="font-size: 12px; width: 100%;">
        <tr>
            <td style="width:200px;">
                订单收款_贷：</td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                订单收款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                计调预付款_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                计调预付款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                导游备用金_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                导游备用金_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                团未完导游先报账_预付账款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预付账款_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                团未完导游先报账_预收账款_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预收账款_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                团未完导游先报账_团队借款_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_团队借款_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                团未完导游先报账_现金_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_现金_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_主营业务收入_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务收入_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_团队借款_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队借款_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_团队预支_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队预支_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_团队支出_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队支出_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_应付账款_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应付账款_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_现金_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_主营业务成本_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务成本_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_预收账款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_预收账款_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_现金_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_银行存款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_银行存款_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                单团核算_应收帐款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应收帐款_借 %>">
                </select>
            </td>
        </tr>
        <%--<tr>
            <td>
                后期收款_主营业务收入_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务收入_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                后期收款_团队预支_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队预支_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                后期收款_应付账款_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应付账款_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                后期收款_团队支出_贷：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队支出_贷 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                后期收款_预收账款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_预收账款_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                后期收款_应收账款_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应收账款_借 %>">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                后期收款_主营业务成本_借：
            </td>
            <td>
                <select name="txt<%=(int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务成本_借 %>">
                </select>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2" style="height:30px;">
                <asp:Button ID="btnSubmit" runat="server" Text="保存金蝶默认科目配置" OnClick="btnSubmit_Click" OnClientClick="return WebForm_OnSubmit_Validate()" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">
</asp:Content>
