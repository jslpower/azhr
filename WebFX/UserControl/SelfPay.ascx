<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelfPay.ascx.cs" Inherits="EyouSoft.WebFX.UserControl.SelfPay" %>
<table width="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin: 5px auto;"
    class="autoAdd" id="Tab_SelfPay">
    <tbody>
        <tr>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "城市")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "景点名称")%>
            </th>
            <th align="center">
                <%=(String)GetGlobalResourceObject("string", "备注")%>
            </th>
            <th width="120" align="center">
                <%=(String)GetGlobalResourceObject("string", "操作")%>
            </th>
        </tr>
        <%if (!(this.SetSelfPayList != null && this.SetSelfPayList.Count > 0))
          {%>
        <tr class="tempRowself">
            <td align="center">
                <input type="hidden" name="hidselfcityid" value="" />
                <input type="text" class="formsize120 searchInput" name="txtselfcity" value="" />
                <a class="xuanyong citybox" data-mode="0" href="javascript:;">
                    <img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'></a>
            </td>
            <td align="center">
                <input type="hidden" name="hidselfscenicid" value="" />
                <input type="hidden" name="hidselfpricejs" value="" />
                <input type="hidden" name="txt_SelfPayPrice" value="" />
                <input type="hidden" name="hidselfscenicpriceid" value="" />
                <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 searchInput"
                    name="txtselfscenicname" value="" />
                <a class="xuanyongscenic xuanyong" href="javascript:;">
                    <img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'></a>
            </td>
            <td align="center">
                <input type="text" class="searchInput formsize140" name="txt_SelfPayRemark" value="" />
            </td>
            <td align="center">
                <a href="javascript:void(0)" class="addbtnself">
                    <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>'></a> <a href="javascript:void(0)"
                        class="delbtnself">
                        <img src='<%=(String)GetGlobalResourceObject("string", "图片删除链接")%>'></a>
            </td>
        </tr>
        <%} %>
        <asp:Repeater runat="server" ID="rpSelfPay">
            <ItemTemplate>
                <tr class="tempRowself">
                    <td align="center">
                        <input type="hidden" name="hidselfcityid" value="<%#Eval("CityId") %>" />
                        <input type="text" class="formsize120 searchInput" name="txtselfcity" value='<%#Eval("CityName") %>' />
                        <a class="xuanyong citybox" data-mode="0" href="javascript:;">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'></a>
                    </td>
                    <td align="center">
                        <input type="hidden" name="hidselfscenicid" value="<%#Eval("ScenicSpotId") %>" />
                        <input type="hidden" name="txt_SelfPayPrice" value="<%#Convert.ToDecimal(Eval("Price")).ToString("f2") %>" />
                        <input type="hidden" name="hidselfpricejs" value="<%#Convert.ToDecimal(Eval("SettlementPrice")).ToString("f2") %>" />
                        <input type="hidden" name="hidselfscenicpriceid" value="" />
                        <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 searchInput"
                            name="txtselfscenicname" value="<%#Eval("ScenicSpotName") %>" />
                        <a class="xuanyong xuanyongscenic" href="javascript:;">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片选用链接")%>'></a>
                    </td>
                    <td align="center">
                        <input type="text" class="searchInput formsize140" name="txt_SelfPayRemark" value="<%#Eval("Remark") %>" />
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="addbtnself">
                            <img src='<%=(String)GetGlobalResourceObject("string", "图片添加链接")%>'></a> <a href="javascript:void(0)"
                                class="delbtnself">
                                <img src='<%=(String)GetGlobalResourceObject("string", "图片删除链接")%>'></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<script type="text/javascript">
    var SelfPage = {
        DelRowCallBack: function() {
            Journey.SumPriceJingDian();
            AddPrice.SetSumPrice();
        },
        AddRowCallBack: function(tr) {
            tr.find("input[name='txtselfcity']").unbind();
            tr.find("input[name='txtselfcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID+"&LgType=<%=Request.QueryString["LgType"] %>", {
                width: 130,
                selectFirst: 'true',
                blur: 'true'
            }).result(function(e, data) {
                $(this).prev("input[type='hidden']").val(data[1]);
                $(this).attr("data-old", data[0]);
                $(this).attr("data-oldvalue", data[1]);
            });
        }
    };
    $(function() {
        $("#Tab_SelfPay").autoAdd({ tempRowClass: "tempRowself", addButtonClass: "addbtnself", delButtonClass: "delbtnself", delCallBack: SelfPage.DelRowCallBack, addCallBack: SelfPage.AddRowCallBack });

        $("#Tab_SelfPay .xuanyongscenic").live("click", function() {
            $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
            var citys = $(this).closest("tr").find("input[name='hidselfcityid']").val();
            if (citys == "") { var msg = '<%=(String)GetGlobalResourceObject("string", "请先选择城市")%>'; alert(msg); return false; }
            var scenicd = $(this).closest("td").find("input[name='hidselfscenicid']").val();
            var scenicpriceid = $(this).closest("td").find("input[name='hidselfscenicpriceid']").val();
            var binkemode = $("#ddlCountry").val();
            if (binkemode == "") {
                var msg = '<%=(String)GetGlobalResourceObject("string", "请先选择国家")%>';
                alert(msg);
                return false;
            }
            var url = "/CommonPage/SelectScenic.aspx?aid=" + $(this).attr("id") + "&";
            url += $.param({ callBack: "CallBackJingDian", IsMultiple: 1, cityids: citys, scenicids: scenicd, scenicpriceid: scenicpriceid, binkemode: binkemode, LgType: '<%=Request.QueryString["LgType"] %>' });
            top.Boxy.iframeDialog({
                iframeUrl: url,
                title: '<%=(String)GetGlobalResourceObject("string", "选择景点")%>',
                modal: true,
                width: "948",
                height: "412"
            });
        })
    })

    $("input[name='txtselfcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID+"&LgType=<%=Request.QueryString["LgType"] %>", {
        width: 130,
        selectFirst: 'true',
        blur: 'true'
    }).result(function(e, data) {
        $(this).prev("input[type='hidden']").val(data[1]);
        $(this).attr("data-old", data[0]);
        $(this).attr("data-oldvalue", data[1]);
    });

    $("#Tab_SelfPay").delegate("input[name='txtselfcity']", "keyup", function() {
        var v = $(this);
        if ($.trim(v.val()) == "") {
            v.prev("input[type='hidden']").val("");
        }
        if ($.trim(v.val()) != $.trim(v.attr("data-old"))) {
            v.prev("input[type='hidden']").val("");
        }

    });

</script>

