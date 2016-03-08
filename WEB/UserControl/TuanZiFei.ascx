<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TuanZiFei.ascx.cs" Inherits="EyouSoft.Web.UserControl.TuanZiFei" %>
<table width="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin: 5px auto;"
    class="autoAdd" id="Tab_SelfPay">
    <tbody>
        <tr>
            <th align="center">
                城市
            </th>
            <th align="center">
                景点名称
            </th>
            <th align="center">
                对外收费金额
            </th>
            <th align="center">
                减少成本金额
            </th>
            <th align="center">
                备注
            </th>
            <th width="120" align="center">
                操作
            </th>
        </tr>
        <%if (!(this.SetZiFei != null && this.SetZiFei.Count > 0))
          {%>
        <tr class="tempRowself">
            <td align="center">
                <input type="hidden" name="hidselfcityid" value="" />
                <input type="text" class="formsize120 inputtext"
                    name="txtselfcity" value="" />
                <a class="xuanyong citybox" data-mode="0" href="javascript:;"></a>
            </td>
            <td align="center">
                <input type="hidden" name="hidselfscenicid" value="" />
                <input type="hidden" name="hidselfpricejs" value="" />
                <input type="hidden" name="hidselfscenicpriceid" value="" />
                <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                    name="txtselfscenicname" value="" />
                <a class="xuanyongscenic xuanyong" href="javascript:;">&nbsp;</a>
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize50" name="txt_SelfPayPrice" id="txt_SelfPayPrice"
                    valid="isMoney" errmsg="请输入正确的对外收费金额" value="" />
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize50" name="txt_SelfPayCost" valid="isMoney"
                    errmsg="请输入正确的减少成本金额" value="" />
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize140" name="txt_SelfPayRemark" value="" />
            </td>
            <td align="center">
                <a href="javascript:void(0)" class="addbtnself">
                    <img width="48" height="20" src="../images/addimg.gif"></a> <a href="javascript:void(0)"
                        class="delbtnself">
                        <img width="48" height="20" src="../images/delimg.gif"></a>
            </td>
        </tr>
        <%} %>
        <asp:Repeater runat="server" ID="rpSelfPay">
            <ItemTemplate>
                <tr class="tempRowself">
                    <td align="center">
                        <input type="hidden" name="hidselfcityid" value="<%#Eval("CityId") %>" />
                        <input type="text" class="formsize120 inputtext"
                            name="txtselfcity" value='<%#Eval("CityName") %>' />
                        <a class="xuanyong citybox" data-mode="0" href="javascript:;"></a>
                    </td>
                    <td align="center">
                        <input type="hidden" name="hidselfscenicid" value="<%#Eval("ScenicSpotId") %>" />
                        <input type="hidden" name="hidselfpricejs" value="<%#Convert.ToDecimal(Eval("SettlementPrice")).ToString("f2") %>" />
                        <input type="hidden" name="hidselfscenicpriceid" value="" />
                        <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                            name="txtselfscenicname" value="<%#Eval("ScenicSpotName") %>" />
                        <a class="xuanyongscenic xuanyong" href="javascript:;">&nbsp;</a>
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize50" name="txt_SelfPayPrice" valid="isMoney"
                            errmsg="请输入正确的对外收费金额" value="<%#Convert.ToDecimal(Eval("Price")).ToString("f2") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize50" name="txt_SelfPayCost" valid="isMoney"
                            errmsg="请输入正确的减少成本金额" value="<%#Convert.ToDecimal(Eval("Cost")).ToString("f2") %>" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize140" name="txt_SelfPayRemark" value="<%#Eval("Remark") %>" />
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="addbtnself">
                            <img width="48" height="20" src="../images/addimg.gif"></a> <a href="javascript:void(0)"
                                class="delbtnself">
                                <img width="48" height="20" src="../images/delimg.gif"></a>
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
            tr.find("input[name='txtselfcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID, {
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
            if (citys == "") { var msg =  "请先选择城市"; alert(msg); return false; }
            var scenicd = $(this).closest("td").find("input[name='hidselfscenicid']").val();
            var scenicpriceid = $(this).closest("td").find("input[name='hidselfscenicpriceid']").val();
            var binkemode = $("#hidbinkemode").val();
            var tourmode = $("#hidtourmode").val();
            var sdate = $("#hidsdate").val();
            var edate = $("#hidedate").val();
            var url = "/CommonPage/SelectScenic.aspx?aid=" + $(this).attr("id") + "&";
            var lgtype = $("#hidLngType").val();
            url += $.param({ callBack: "CallBackJingDian", IsMultiple: 1, cityids: citys, scenicids: scenicd, scenicpriceid: scenicpriceid, binkemode: binkemode, tourmode: tourmode, sdate: sdate, edate: edate, LgType: lgtype });
            top.Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择景点",
                modal: true,
                width: "948",
                height: "412"
            });
        })
    })

    $("input[name='txtselfcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID, {
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

