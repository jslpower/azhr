<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaiWuShaiXuan.ascx.cs"
    Inherits="Web.UserControl.CaiWuShaiXuan" %>
<%--财务筛选用户控件 下拉选择大于等于、等于、小于等于+输入框模式--%>
<span id="<%=ClientUniqueID %>">
    <%=ShaiXuanBiaoTi%>
    <select name="<%=ClientUniqueIDOperator %>" class="inputselect" id="<%=ClientUniqueIDOperator %>">
        <option value="-1">请选择</option>
        <option value="<%=(int)EyouSoft.Model.EnumType.FinStructure.EqualSign.大于等于 %>">≥</option>
        <option value="<%=(int)EyouSoft.Model.EnumType.FinStructure.EqualSign.等于 %>">＝</option>
        <option value="<%=(int)EyouSoft.Model.EnumType.FinStructure.EqualSign.小于等于 %>">≤</option>
    </select>
    <input type="text" name="<%=ClientUniqueIDOperatorNumber %>" id="<%=ClientUniqueIDOperatorNumber %>"
        class="inputtext formsize50" />
</span>
<asp:Literal runat="server" ID="ltrScripts"></asp:Literal>

<script type="text/javascript">
    window["<%=ClientUniqueID %>"] = { "ClientUniqueIDOperator": "<%=ClientUniqueIDOperator %>", "ClientUniqueIDOperatorNumber": "<%=ClientUniqueIDOperatorNumber %>" };
    /*//财务筛选 params.uniqueID：window.wucClientUniqueID obj。
    function caiWuShaiXuan(uniqueID) {
        //获取操作符
        this.getOperator = function() {
            return $("#" + uniqueID["ClientUniqueIDOperator"]).val();
        };
        //获取操作数
        this.getOperatorNumber = function() {
            return $("#" + uniqueID["ClientUniqueIDOperatorNumber"]).val();
        }
        //设置操作符
        this.setOperator = function(_v) {
            $("#" + uniqueID["ClientUniqueIDOperator"]).val(_v);
        }
        //设置操作数
        this.setOperatorNumber = function(_v) {
            $("#" + uniqueID["ClientUniqueIDOperatorNumber"]).val(_v);
        }
        
        return this;
    }*/
</script>

