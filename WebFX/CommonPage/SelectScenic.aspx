<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectScenic.aspx.cs" Inherits="EyouSoft.WebFX.CommonPage.SelectScenic" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <style type="text/css">
        table, tr, td
        {
            border: 1px solid #B8C5CE;
            border-collapse: collapse;
        }
        label
        {
            cursor: pointer;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div>
        <div style="margin: 0 auto; width: 99%;">
            <div style="width: 100%" id="AjaxHotelList">
                <table id="tblList" align="center" bgcolor="#FFFFFF" cellpadding="0" cellspacing="0"
                    width="100%" class="alertboxbk1" border="0" style="border-collapse: collapse;
                    margin: 5px 0;">
                    <tr>
                        <asp:Repeater ID="rpt_ScenicList" runat="server">
                            <ItemTemplate>
                                <td align="left" height="28">
                                    &nbsp;&nbsp;
                                    <input type="hidden" name="hidpriceid" value="" data-jiagejs="" data-jiageth="" />
                                    <input id="<%#Eval("JingDianId") %>" data-isHas='<%#GetIsHavePrice(Eval("JingDianId")) %>' name="radiogroup" type="checkbox" value="<%#Eval("JingDianId") %>"
                                        data-show="<%#Eval("Name")%>" data-istuijian="<%#(bool)Eval("IsTuiJian")?"1":"0" %>" data-fujian="<%#Eval("FuJian.FilePath") %>" />
                                    <input id="<%#Eval("JingDianId") %>" data-isHas='<%#GetIsHavePrice(Eval("JingDianId")) %>' name="radiogroup" type="radio" value="<%#Eval("JingDianId") %>"
                                        data-show="<%#Eval("Name")%>" data-istuijian="<%#(bool)Eval("IsTuiJian")?"1":"0" %>" data-fujian="<%#Eval("FuJian.FilePath") %>" />
                                    <input type="hidden" id="txtJingDianDesc_<%#Eval("JingDianId") %>" value="<%#Eval("MiaoShu") %>" />
                                    <label for="<%#Eval("JingDianId") %>">
                                        <%#(Container.ItemIndex+1)+(pageIndex-1)*pageSize %>
                                        <%#Eval("Name")%>
                                    </label>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                        <tr>
                            <td align="right" class="alertboxTableT" colspan="4" height="23">
                                <div class="pages">
                                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                </div>
                            </td>
                        </tr>
                </table>
            </div>
            <div class="alertbox-btn">
                <a id="a_btn" hidefocus="true" href="javascript:void(0)"><s class="xuanzhe"></s><%=(String)GetGlobalResourceObject("string", "选择") %></a></div>
        </div>
    </div>

    <script type="text/javascript">
        var useSupplierPage = {
            //选中的景点信息 {id:"景点编号",name:"景点名称",desc:"景点描述"}
            jingDians: [],
            setXuanZhongValue: function() {
                var _self = this;
                var _$chks = $("input:checked");
                _$chks.each(function() {
                var _$chk = $(this);
                    useSupplierPage.jingDians.push({ id: _$chk.val(), name: _$chk.attr("data-show"), desc: $("#txtJingDianDesc_" + _$chk.val()).val(), jiageth: _$chk.parent().find("input[name='hidpriceid']").attr("data-jiageth"), jiagejs: _$chk.parent().find("input[name='hidpriceid']").attr("data-jiagejs"), priceid: _$chk.parent().find("input[name='hidpriceid']").val(),istuijian:_$chk.attr("data-istuijian"),fujian:_$chk.attr("data-fujian") });
                })

            },
            search_Click: function() {
                window.location = "/CommonPage/SelectScenic.aspx?" + $.param($.extend(Boxy.getUrlParams(["textfield", "cityids"]), {
                    textfield: $("#textfield").val(),
                    citys: '<%=Request.QueryString["cityids"] %>'
                }))
            },
            xuanZe_Click: function() {
                this.setXuanZhongValue();

                var _callBackFn = '<%=EyouSoft.Common.Utils.GetQueryStringValue("callback") %>';
                var _data = { jingDianXuanYongAId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("aid") %>', jingDians: this.jingDians };

                window.parent[_callBackFn](_data);

                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                return false;
            }
        };

        $(document).ready(function() {
            var scenicid = '<%=Request.QueryString["scenicids"] %>';
            var scenicpriceid = '<%=Request.QueryString["scenicpriceid"] %>';
            //初始化 单选复选
            if (parseInt('<%=Request.QueryString["IsMultiple"] %>') == 1) {
                $(":checkbox").remove();
                $(":radio[value='" + scenicid + "']").attr("checked", "checked");
                $(":radio[value='" + scenicid + "']").closest("td").find("input[name='hidpriceid']").val(scenicpriceid);
            }
            else {
                $(":radio").remove();
                //初始化选中复选框
                //                var scenicids = [];
                //                scenicids = scenicid.toString().split(',');
                //                for (var i = 0; i < scenicids.length; i++) {
                //                    $(":checkbox[value='" + scenicids[i] + "']").attr("checked", "checked");
                //                }

            }
            $("#Select").bind("click", function() { useSupplierPage.search_Click(); });
            $("#a_btn").bind("click", function() { useSupplierPage.xuanZe_Click(); });
            var data = {
                binkemode: '<%=Request.QueryString["binkemode"] %>'
            };

            $("#tblList input").click(function() {
                if (this.checked) {
                    var self = $(this);
                    if (self.attr("data-isHas") == "yes") {
                        var url = "/CommonPage/selectScenicPrice.aspx?";
                        var hidpricceid = self.parent().find("input[type='hidden'][name='hidpriceid']").val(); //价格编号
                        var hideObj = self.val(); //景点编号
                        url += $.param({ hideID: hideObj, callBack: "CallBackJDPrice", priceId: hidpricceid, parentiframeid: '<%=Request.QueryString["iframeId"] %>', binkemode: data.binkemode, LgType: '<%=Request.QueryString["LgType"] %>' })
                        top.Boxy.iframeDialog({
                            iframeUrl: url,
                            title: '<%=(String)GetGlobalResourceObject("string", "选择景点价格") %>',
                            modal: true,
                            width: "560",
                            height: "460"
                        });
                    }
                }
            })

        });

        function CallBackJDPrice(obj) {
            $("#tblList").find("input:checked").each(function() {
                $("#" + obj.scenicid).parent().find("input[type='hidden'][name='hidpriceid']").val(obj.priceid);
                $("#" + obj.scenicid).parent().find("input[type='hidden'][name='hidpriceid']").attr("data-jiageth", obj.jiageth);
                $("#" + obj.scenicid).parent().find("input[type='hidden'][name='hidpriceid']").attr("data-jiagejs", obj.jiagejs);
            })
        }
    </script>

</body>
</html>
