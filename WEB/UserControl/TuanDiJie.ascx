<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TuanDiJie.ascx.cs" Inherits="EyouSoft.Web.UserControl.TuanDiJie" %>
<div style="width: 98.5%" class="tablelist-box" id="Tab_DiJie">
    <span class="formtableT">地接社</span>
    <table width="100%" cellspacing="0" cellpadding="0" class="autoAdd" id="tblDiJie">
        <tbody>
            <tr>
                <th width="20%" valign="middle">
                    城市
                </th>
                <th width="25%" valign="middle">
                    地接社名称
                </th>
                <th width="10%" valign="middle">
                    联系人
                </th>
                <th width="10%" valign="middle">
                    联系电话
                </th>
                <th width="20%" valign="middle">
                    备注
                </th>
                <th width="15%" valign="middle">
                    操作
                </th>
            </tr>
            <asp:PlaceHolder runat="server" ID="PHDefault">
                <tr class="tempDiJieRow">
                    <td align="center">
                        <input type="hidden" name="hiddijiecityid" value="" />
                        <input type="text" class="formsize80 inputtext"
                            name="txtdijiecity" value="" />
                        <a class="xuanyong citybox" data-mode="0" href="javascript:;"></a>
                    </td>
                    <td align="center">
                        <input type="hidden" name="hiddijieid" value="" />
                        <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                            name="txtdijiename" value="" />
                        <a class="xuanyongdijie xuanyong" href="javascript:;">&nbsp;</a>
                    </td>
                    <td align="center">
                        <input type="text" class="formsize80 inputtext" name="dijie_contact" value="" />
                    </td>
                    <td align="center">
                        <input type="text" class="formsize80 inputtext" name="dijie_tel" value="" />
                    </td>
                    <td align="center">
                        <input type="text" class="formsize120 inputtext" name="dijie_remark" value="" />
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="addDiJie">
                            <img width="48" height="20" src="../images/addimg.gif"></a> <a href="javascript:void(0)"
                                class="delDiJie">
                                <img width="48" height="20" src="../images/delimg.gif"></a>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:Repeater runat="server" ID="rptList">
                <ItemTemplate>
                    <tr class="tempDiJieRow">
                        <td align="center">
                            <input type="hidden" name="hiddijiecityid" value="<%#Eval("CityId") %>" />
                            <input type="text" class="formsize80 inputtext"
                                name="txtdijiecity" value="<%#Eval("CityName") %>" />
                            <a class="xuanyong citybox" data-mode="0" href="javascript:;"></a>
                        </td>
                        <td align="center">
                            <input type="hidden" name="hiddijieid" value="<%#Eval("DiJieId") %>" />
                            <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize80 inputtext"
                                name="txtdijiename" value="<%#Eval("DiJieName") %>">
                            <a class="xuanyongdijie xuanyong" href="javascript:;">&nbsp;</a>
                        </td>
                        <td align="center">
                            <input type="text" class="formsize80 inputtext" name="dijie_contact" value="<%#Eval("Contact") %>" />
                        </td>
                        <td align="center">
                            <input type="text" class="formsize80 inputtext" name="dijie_tel" value="<%#Eval("Phone") %>" />
                        </td>
                        <td align="center">
                            <input type="text" class="inputtext" style="width: 80%" name="dijie_remark" value="<%#Eval("Remark") %>" />
                        </td>
                        <td align="center">
                            <a href="javascript:void(0)" class="addDiJie">
                                <img width="48" height="20" src="../images/addimg.gif"></a> <a href="javascript:void(0)"
                                    class="delDiJie">
                                    <img width="48" height="20" src="../images/delimg.gif"></a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>

<script type="text/javascript">
var TuanDiJie = {
	//地接城市自动匹配
	AutoMatch: function() {
		$("input[name='txtdijiecity']").unbind();
		$("input[name='txtdijiecity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID, {
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
    $("#tblDiJie").autoAdd({ tempRowClass: "tempDiJieRow", addButtonClass: "addDiJie", delButtonClass: "delDiJie", addCallBack: TuanDiJie.AutoMatch, delCallBack: TuanDiJie.AutoMatch });
    $("#Tab_DiJie").delegate(".xuanyongdijie", "click", function() {
        $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
        var areas = $(this).closest("tr").find("input[name='hiddijiecityid']").val();
        if (areas == "") { alert("请先选择城市!"); return false; }
        var dijieid = $(this).closest("td").find("input[name='hiddijieid']").val();
        var url = "/CommonPage/UserSupplier.aspx?aid=" + $(this).attr("id") + "&";
        url += $.param({ callBack: "CallBackDijie", IsMultiple: 1, ddlCity: areas, suppliertype: 0, hideID: dijieid });
        top.Boxy.iframeDialog({
            iframeUrl: url,
            title: "选择地接社",
            modal: true,
            width: "948",
            height: "412"
        });
    })

    TuanDiJie.AutoMatch();

    $("#Tab_DiJie").delegate("input[name='txtdijiecity']", "keyup", function() {
        var v = $(this);
        if ($.trim(v.val()) == "") {
            v.prev("input[type='hidden']").val("");
        }
        if ($.trim(v.val()) != $.trim(v.attr("data-old"))) {
            v.prev("input[type='hidden']").val("");
        }

    });
})
    function CallBackDijie(obj) {
        if (obj) {
            $("#" + obj.aid).closest("td").find("input[type='hidden']").val(obj.id);
            $("#" + obj.aid).closest("td").find("input[type='text']").val(obj.name);
            $("#" + obj.aid).closest("tr").find("input[name='dijie_contact']").val(obj.contactname);
            $("#" + obj.aid).closest("tr").find("input[name='dijie_tel']").val(obj.contactmobile);

        }
    }

</script>

