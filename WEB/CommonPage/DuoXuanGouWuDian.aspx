<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuoXuanGouWuDian.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.DuoXuanGouWuDian" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox" id="alertbox" style="padding-top: 5px;">
        <form id="form1" method="get">
        <table width="99%" id="TabSearch" align="center" cellpadding="0" cellspacing="0"
            bgcolor="#e9f4f9" style="margin: 0 auto">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="SourceName"></span>查询：
                </td>
                <td width="90%" align="left">
                    国家：
                    <select id="txtCountryId" name="txtCountryId">
                    </select>
                    省份：
                    <select id="txtProvinceId" name="txtProvinceId">
                    </select>
                    城市：
                    <select id="txtCityId" name="txtCityId">
                    </select>
                    名称：
                    <input type="text" id="txtName" name="txtName" class="input-txt" maxlength="50" value='<%=Request.QueryString["txtName"]%>' />
                    <input type="hidden" name="iframeid" id="iframeid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeid") %>" />
                    <input type="submit" value="搜索" class="search-btn" style="cursor: pointer; height: 24px;
                        width: 64px; background: url(/images/cx.gif) no-repeat center center; border: 0 none;
                        margin-left: 5px;" />
                </td>
            </tr>
        </table>
        </form>
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center" style="margin: 0 auto">
            <tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <td align="left" height="30px" width="25%">                            
                            <input name="chk" id="chk_<%#Eval("GysId") %>" type="checkbox" value="<%#Eval("GysId") %>" />
                            <label for="chk_<%#Eval("GysId") %>"><%#Eval("GysName")%></label>
                            <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,recordCount,4) %>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <td>暂无购物店信息</td></tr>
                </asp:PlaceHolder>
        </table> 
        
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="i_a_xuanze"><s class="baochun"></s>
                选择</a>
        </div>       
    </div>
    
    <script type="text/javascript">
        var iPage = {
            close: function() {
                top.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            },
            init: function() {
                var _v = parent.window["dxgwd"].getValue();
                if (_v.id.length == 0) return;
                var _arr = _v.id.split(",");
                for (var i = 0; i < _arr.length; i++) {
                    $("#chk_" + _arr[i]).attr("checked", "checked");
                }
            },
            xuanZe: function() {
                var _id = [];
                var _name = [];
                $("input:checked").each(function() {
                    var _$obj = $(this);
                    _id.push(_$obj.val());
                    _name.push(_$obj.next().text());
                });                
                parent.window["dxgwd"].setValue({ id: _id.join(","), name: _name.join(",") });                
                this.close();
            }
        };

        $(document).ready(function() {
            pcToobar.init({ gID: "#txtCountryId", pID: "#txtProvinceId", cID: "#txtCityId", gSelect: '<%=Utils.GetQueryStringValue("txtCountryId") %>', pSelect: '<%=Utils.GetQueryStringValue("txtProvinceId") %>', cSelect: '<%=Utils.GetQueryStringValue("txtCityId") %>' });
            iPage.init();
            $("#i_a_xuanze").click(function() { iPage.xuanZe(); });
        });
    </script>
</asp:Content>
