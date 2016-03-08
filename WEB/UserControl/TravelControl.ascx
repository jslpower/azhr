<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TravelControl.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.TravelControl" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<span class="formtableT formtableT02" style="position: static">游客信息</span><%--<span class="formtableT formtableT02"
    style="position: static"><a href="javascript:void(0);" id="btnImportData">导入游客</a></span>--%>
<table width="100%" border="0" style="border-collapse: collapse" id="TravelControl_tbl">
    <tbody>
        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
            <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                姓名
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                性别
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                类型
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                证件类型
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                证件号码
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                有效期
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                身份证
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                联系方式
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                出生日期
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                备注
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                短信通知
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                操作
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
        <tr class="tempRow">
            <td height="28" align="center">
                <input type="hidden" value="" name="txt_TravelControl_TreavelID">
                <input type="text" value="" class="inputtext formsize50" name="txt_TravelControl_Name">
            </td>
            <td align="center">
                <select class="inputselect" name="slt_TravelControl_UserSex" data-value="">
                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender),new string[]{"2"}), "")%>
                </select>
            </td>
            <td align="center">
                <select class="inputselect" name="slt_TravelControl_PeopleType" data-value="">
                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), "")%>
                </select>
            </td>
            <td align="center">
                <select class="inputselect" name="slt_TravelControl_ProveType" data-value="" onchange="TravelControl.ProveTypeOnChange(this)">
                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType),new string[]{"0"}), "")%>
                </select>
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize100" value="" name="txt_TravelControl_CardNum">
            </td>
            <td align="center">
                <input type="text" value="" class="inputtext formsize80" name="txt_TravelControl_EffectTime">
            </td>
            <td align="center">
                <input type="text" value="" class="inputtext formsize80" name="txt_TravelControl_IDCard">
            </td>
            <td align="center">
                <input type="text" value="" class="inputtext formsize80" name="txt_TravelControl_Phone">
            </td>
            <td align="center">
                <input type="text" value="" class="inputtext formsize80" name="txt_TravelControl_Brithday"
                    onfocus="WdatePicker()">
            </td>
            <td align="center">
                <input type="text" class="inputtext formsize120" name="txt_TravelControl_Remarks">
            </td>            
            <td align="center">
                <input type="hidden" name="cbx_TravelControl_LeaveMsg" value="" />
                <label>
                    <input type="checkbox" class="fuxuank" onclick="TravelControl.SetValue(this);" value='1' />
                    出团</label>
                <input type="hidden" name="cbx_TravelControl_BackMsg" value="" />
                <label>
                    <input type="checkbox" class="fuxuank" onclick="TravelControl.SetValue(this);" value='2' />
                    回团</label>
            </td>
            <td align="center">
                <a class="addbtn" href="javascript:void(0)">
                    <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                        <img src="/images/delimg.gif"></a>
            </td>
        </tr>
        </asp:PlaceHolder>
        <cc1:CustomRepeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr class="tempRow">
                    <td height="28" align="center">
                        <input type="text" class="inputtext formsize50" value="<%#Eval("CnName")%>" name="txt_TravelControl_Name">
                        <input type="hidden" name="txt_TravelControl_TreavelID" value="<%#Eval("TravellerId")%>" />
                    </td>
                    <td align="center">
                        <select class="inputselect" name="slt_TravelControl_UserSex" data-value="<%#(int)Eval("Gender")%>">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender),new string[]{"2"}), (int)Eval("Gender"))%>
                        </select>
                    </td>
                    <td align="center">
                        <select class="inputselect" name="slt_TravelControl_PeopleType" data-value="<%#(int)Eval("VisitorType") %>">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)),(int)Eval("VisitorType"))%>
                        </select>
                    </td>
                    <td align="center">
                        <select class="inputselect" name="slt_TravelControl_ProveType" data-value="<%#(int)Eval("CardType") %>"
                            onchange="TravelControl.ProveTypeOnChange(this)">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType),new string[]{"0"}), (int)Eval("CardType"))%>
                        </select>
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize100" value="<%#Eval("CardNumber")%>"
                            name="txt_TravelControl_CardNum">
                    </td>
                    <td align="center">
                        <input type="text"  class="inputtext formsize80" name="txt_TravelControl_EffectTime"
                            value="<%#Eval("CardValidDate")%>" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize80" name="txt_TravelControl_IDCard"
                            value="<%#Eval("CardId")%>" />
                    </td>
                    <td align="center">
                        <input type="text" value="<%#Eval("Contact")%>" class="inputtext formsize80" name="txt_TravelControl_Phone">
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize80" name="txt_TravelControl_Brithday"
                            value="<%#Eval("Birthday")%>" onfocus="WdatePicker()" />
                    </td>
                    <td align="center">
                        <input type="text" class="inputtext formsize120" name="txt_TravelControl_Remarks"
                            value="<%#Eval("Remark")%>">
                    </td>
                    <td align="center">
                        <input type="hidden" name="cbx_TravelControl_LeaveMsg" value="<%#(bool)Eval("LNotice") ? "1" : ""%>" />
                        <label>
                            <input type="checkbox" class="fuxuank" onclick="TravelControl.SetValue(this);" value='1'
                                <%#(bool)Eval("LNotice") == true ? " checked='checked'" : ""%> />
                            出团</label>
                        <input type="hidden" name="cbx_TravelControl_BackMsg" value="<%#(bool)Eval("RNotice") ? "1" : ""%>" />
                        <label>
                            <input type="checkbox" class="fuxuank" onclick="TravelControl.SetValue(this);" value='2'
                                <%#(bool)Eval("RNotice") == true ? " checked='checked'" : ""%> />
                            回团</label>
                    </td>
                    <td align="center">
                        <a class="addbtn" href="javascript:void(0)">
                            <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                <img src="/images/delimg.gif"></a>
                    </td>
                </tr>
            </ItemTemplate>
        </cc1:CustomRepeater>
    </tbody>
</table>

<script type="text/javascript">
    var TravelControl = {
        SetHideValue: function(a_id, id, name, price, count) {
            var a = $("#" + a_id);
            a.parent().find("input[name='hideInsuranceID']").val(id);
            a.parent().find("input[name='hideInsuranceName']").val(name);
            a.parent().find("input[name='hideInsurancePrice']").val(price);
            a.parent().find("input[name='hideInsuranceCount']").val(count);
            if (id != "") {
                a.html("<img  border=\"0\" src=\"/images/y-duihao.gif\">");
            } else {
                $("#" + a_id).html("<img  border=\"0\" src=\"/images/y-cuohao.gif\" />");
            }

        },
        SetAllValue: function(data) {
            if (data && data.id.length > 0) {
                $("#TravelControl_tbl").find("a[class='baoxian']").each(function() {
                    var _s = $(this);
                    var four = { id: _s.parent().find("input[name='hideInsuranceID']"), name: _s.parent().find("input[name='hideInsuranceName']"), price: _s.parent().find("input[name='hideInsurancePrice']"), count: _s.parent().find("input[name='hideInsuranceCount']") };
                    if (four.id.val() == "") {
                        four.id.val(data.id.join(','));
                        four.name.val(data.name.join(','));
                        four.price.val(data.price.join(','));
                        four.count.val(data.count.join(','));
                    } else {
                        var fourArray = { idlist: four.id.val().split(','), namelist: four.name.val().split(','), pricelist: four.price.val().split(','), coutlist: four.count.val().split(',') };

                        for (var m = 0; m < data.id.length; m++) {
                            if (!ArrayUnique(fourArray.idlist, data.id[m])) {
                                fourArray.idlist.push(data.id[m]);
                                fourArray.namelist.push(data.name[m]);
                                fourArray.pricelist.push(data.price[m]);
                                fourArray.coutlist.push(data.count[m]);
                            }
                        }

                        four.id.val(fourArray.idlist.join(','));
                        four.name.val(fourArray.namelist.join(','));
                        four.price.val(fourArray.pricelist.join(','));
                        four.count.val(fourArray.coutlist.join(','));
                    }
                    _s.html("<img border=\"0\" src=\"/images/y-duihao.gif\" />");
                })
            }
        },
        Open: function(o) {
            var data = { id: "", name: "", price: "", count: "", a_id: "" }
            var obj = $(o);
            data.id = obj.parent().find("input[name='hideInsuranceID']").val();
            data.name = obj.parent().find("input[name='hideInsuranceName']").val();
            data.price = obj.parent().find("input[name='hideInsurancePrice']").val();
            data.count = obj.parent().find("input[name='hideInsuranceCount']").val();

            var newID = "a_" + parseInt(Math.random() * 9999);
            obj.attr("id", newID);
            data.a_id = newID;

            var url = "/SellCenter/Order/OrderInsurance.aspx?" + $.param(data);
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "游客保险",
                modal: true,
                width: "600px",
                height: "350px"
            });
            return false;
        },
        SetValue: function(o) {
            if (o.value == "1") {
                if (o.checked) {
                    $(o).closest("td").find("input[name='cbx_TravelControl_LeaveMsg']").val("1");
                } else {
                    $(o).closest("td").find("input[name='cbx_TravelControl_LeaveMsg']").val("");
                }
            } else {
                if (o.checked) {
                    $(o).closest("td").find("input[name='cbx_TravelControl_BackMsg']").val("1");
                } else {
                    $(o).closest("td").find("input[name='cbx_TravelControl_BackMsg']").val("");
                }
            }
        },
        AddCallBack: function(tr) {
            tr.find("a[class='baoxian']").html("<img  border=\"0\" src=\"/images/y-cuohao.gif\">");
            tr.find("a[class='baoxian']").parent().find("input[type='hidden']").val("");
            tr.find("a[data-class='tuituan']").remove();
        },
        ProveTypeOnChange: function(o) {
            var _self = $(o);
            if (_self.val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.CardType.身份证%>") {
                _self.closest("tr").find("input[name='txt_TravelControl_Prove']").attr("valid", "isIdCard").attr("errmsg", "请输入正确的身份证号码!");
            } else {
                _self.closest("tr").find("input[name='txt_TravelControl_Prove']").removeAttr("valid", "isIdCard").removeAttr("errmsg", "请输入正确的身份证号码!");
            }
        },
        SelectObj: null
    }
    //存在判断
    function ArrayUnique(array, item) {
        var r = false;
        for (var i = 0; i < array.length; i++) {
            if (array[i] && array[i] == item) {
                r = true;
            }
        }
        return r;
    }

    $(function() {

        $("#TravelControl_tbl").autoAdd({ addCallBack: TravelControl.AddCallBack });
        $("#TravelControl_tbl").find("select[name='slt_TravelControl_ProveType']").each(function() {
            TravelControl.SelectObj = $(this);
            if (TravelControl.SelectObj.attr("data-value")) {
                TravelControl.SelectObj.attr("value", TravelControl.SelectObj.attr("data-value"));
            }
            TravelControl.ProveTypeOnChange(this);
        })
        $("#TravelControl_tbl").find("select[name='slt_TravelControl_UserSex']").each(function() {
            TravelControl.SelectObj = $(this);
            if (TravelControl.SelectObj.attr("data-value")) {
                TravelControl.SelectObj.attr("value", TravelControl.SelectObj.attr("data-value"));
            }
        })

        $("#TravelControl_tbl").find("select[name='slt_TravelControl_PeopleType']").each(function() {
            TravelControl.SelectObj = $(this);
            if (TravelControl.SelectObj.attr("data-value")) {
                TravelControl.SelectObj.attr("value", TravelControl.SelectObj.attr("data-value"));
            }
        })

        $("#cbxTravelControlAll").click(function() {
            $("#TravelControl_tbl").find("input[name='cbx']").attr("checked", $(this).attr("checked"));
        })

        $("#btnImportData").click(function() {
            Boxy.iframeDialog({
                iframeUrl: "/ImportSource/ImportPage.aspx?type=1&box=TravelControl_tbl",
                title: "导入游客",
                modal: true,
                width: "900px",
                height: "500px"
            });
            return false;
        })


    })

   
    
</script>

