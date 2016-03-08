<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menuedit.aspx.cs" Inherits="Web.Webmaster._menuedit" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
    <style type="text/css">
    ul{list-style: none;margin: 0px;padding: 0px;}
    ul li{list-style: none;}
    .m2{width: 100%;}
    .m2 li{float:left; line-height: 24px;width:20%; text-align:left}
    .tblmenus{border-top:1px solid #ddd;border-left:1px solid #ddd;width: 100%;margin-bottom: 10px;}    
    .tblmenus .thead{text-align:left;background: #efefef; height:35px; font-size:14px;}
    .tblmenus td{border-right:1px solid #ddd;border-bottom:1px solid #ddd; height:35px;}
    .m1classname li { float:left;}
    .m1classname li span{background:url(/images/menuicon.gif) no-repeat -9999px; position:relative; left:0px; top:0px;width:19px; height:18px; display:inline-block; margin-right:2px}
    .m1classname li span.zutuan{ background-position:0 0;}
    .m1classname li span.diejietd{ background-position:0 -27px;}
    .m1classname li span.cjtd{ background-position:-2px -57px;}
    .m1classname li span.tongyefx{ background-position:-2px -80px;}
    .m1classname li span.dxyw{ background-position:0 -110px;}
    .m1classname li span.jidiaozx{ background-position:0 -135px;}
    .m1classname li span.daoyouzx{ background-position:0 -163px;}
    .m1classname li span.ziyuanyk{ background-position:0 -190px;}
    .m1classname li span.zilianggl{ background-position:0 -216px;}
    .m1classname li span.kehugl{ background-position:0 -243px;}
    .m1classname li span.ziyuangl{ background-position:0 -276px;}
    .m1classname li span.hetonggl{ background-position:0 -297px;}
    .m1classname li span.caiwugl{ background-position:0 -322px;}
    .m1classname li span.tongjifx{ background-position:0 -352px;}
    .m1classname li span.xingzzx{ background-position:0 -376px;}
    .m1classname li span.xitongsz{ background-position:0 -406px;}
    .m1classname li span.xsshoukuan{ background-position:0 -560px;}
    .m1classname li span.kehuzx{ background-position:0 -267px;}
    </style>

    <script type="text/javascript">
        //初始化已经设置过的一二级栏目
        function initComMenus() {
            if (comMenus.length < 1) return;

            for (var i = 0; i < comMenus.length; i++) {
                if (comMenus[i].MenuId == comMenu1Id) {
                    $("#txtMenu1Name").val(comMenus[i].Name);
                    if (comMenus[i].IsDisplay) $("#chkIsDisplay").attr("checked", "checked");
                    $("input[name='radClassName'][value='" + comMenus[i].ClassName + "']").attr("checked", true);
                }
                
                for (var j = 0; j < comMenus[i].Menu2s.length; j++) {
                    var chkObj$ = $("#chkSysMenu2Id_" + comMenus[i].Menu2s[j].DefaultMenu2Id);
                    
                    chkObj$.attr("checked", true);
                    chkObj$.next().val(comMenus[i].Menu2s[j].MenuId);
                    chkObj$.next().next().val(comMenus[i].Menu2s[j].Name);

                    if (comMenus[i].MenuId != comMenu1Id) {
                        chkObj$.attr("disabled", "disabled");
                        chkObj$.next().next().attr("disabled", "disabled");
                    }
                }
            }
        }

        function WebForm_OnSubmit_Validate() {
            if ($.trim($("#txtMenu1Name").val()) == "") {
                $("#txtMenu1Name").focus();
                alert("请填写一级栏目名称。");
                return false;
            }
                    
            if ($(".tdMenus input[type='checkbox']:enabled:checked").length < 1) {
                alert("至少要选择一个二级栏目。")
                return false;
            }
            $(".tdMenus input[type='text']:enabled").each(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).focus();
                    alert("二级栏目名称不能为空。");
                    return false;
                }
            });
            var _mClassName = $("input[name = 'radClassName']:checked").val();
            if (_mClassName == "" || _mClassName == "undefined" || _mClassName == undefined) {
                alert("请选择一级栏目小图标。");
                return false;
            }
            
            return true;
        }

        $(document).ready(function() {
            initComMenus();
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    子系统一二级栏目管理-<asp:Literal runat="server" ID="ltrSysName"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">
    <table cellpadding="2" cellspacing="1" style="width: 100%;" class="tblmenus">
        <tr class="thead">
            <td colspan="2">
                一级栏目名称：<input id="txtMenu1Name" class="input_text" name="txtMenu1Name" type="text" maxlength="20" style="width: 200px" />
                <label><input type="checkbox" name="chkIsDisplay" id="chkIsDisplay" value="1" />在菜单中显示</label>
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick="return WebForm_OnSubmit_Validate();" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <ul class="m1classname">
                    <li><label><input type="radio" name="radClassName" value="zutuan" /><span class="zutuan">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="diejietd" /><span class="diejietd">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="cjtd" /><span class="cjtd">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="tongyefx" /><span class="tongyefx">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="dxyw" /><span class="dxyw">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="jidiaozx" /><span class="jidiaozx">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="daoyouzx" /><span class="daoyouzx">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="ziyuanyk" /><span class="ziyuanyk">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="zilianggl" /><span class="zilianggl">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="hetonggl" /><span class="hetonggl">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="caiwugl" /><span class="caiwugl">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="tongjifx" /><span class="tongjifx">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="xingzzx" /><span class="xingzzx">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="xitongsz" /><span class="xitongsz">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="ziyuangl" /><span class="ziyuangl">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="kehugl" /><span class="kehugl">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="xsshoukuan" /><span class="xsshoukuan">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="kehuzx" /><span class="kehuzx">&nbsp;</span></label></li>
                    <li><label><input type="radio" name="radClassName" value="none" />none</label></li>
                </ul>
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptMenus" OnItemDataBound="rptMenus_ItemDataBound">            
            <ItemTemplate>
                <tr>
                    <td style="font-weight: bold; width: 70px; text-align: center">
                          <%#Eval("Name") %>
                    </td>
                    <td class="tdMenus">
                        <asp:Literal runat="server" ID="ltrMenu2s"></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">
    
</asp:Content>
