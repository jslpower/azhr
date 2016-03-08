<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TravelControlS.ascx.cs"
    Inherits="EyouSoft.WebFX.UserControl.TravelControlS" %>
<span class="formtableT formtableT02" style=" position:static">游客信息</span> <span class="formtableT formtableT02" style=" position:static">
    <a id="btnImportData" href="javascript:void(0);">导入游客</a></span>
<table id="TravelControlS_tbl" width="99%" border="0" style="border-collapse: collapse">
    <tbody>
        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
            <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                姓名
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                证件类型
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                证件号码
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                身份证号码
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                性别
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                类型
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                联系方式
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                有效期
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                办理
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                备注
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                签证状态
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                短信通知
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                操作
            </td>
        </tr>
        <%if (!(this.SetTravelList != null && this.SetTravelList.Count > 0))
          {%>
        <tr class="tempRow">
            <td height="28" align="center">
                <input type="hidden" name="txt_TravelControlS_TreavelID" />
                <input type="text" value="" class="inputtext formsize50" name="txt_TravelControlS_TreavelID_NameCn" /><br />
                <input type="text" value="" class="inputtext formsize50" name="txt_TravelControlS_TreavelID_NameEn" />
            </td>
            <td align="center">
                <select class="inputselect" name="slt_TravelControlS_ProveType" data-value="">
                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType)), "")%>
                </select>
            </td>
            <td align="center">
                <input type="text" id="textfield20" name="txt_TravelControlS_CardNum"
                    class="inputtext" style="width:125px" />
            </td>
            <td align="center">
                <input type="text" value="" name="slt_TravelControlS_Sfz" class="inputtext" valid="isIdCard"
                    errmsg="请输入正确的身份证号码!" style="width: 125px" />
            </td>
            <td align="center">
                <select class="inputselect" name="slt_TravelControlS_Sex" data-value="">
                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender)), "")%>
                </select>
            </td>
            <td align="center">
                <select class="inputselect" name="slt_TravelControlS_PeoType" data-value="">
                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), "")%>
                </select>
            </td>
            <td align="center">
                <input type="text" value="" class="inputtext formsize80" name="txt_TravelControlS_ContactTel"/>
            </td>
            <td align="center">
                <input type="text" value="" style="width: 25px;" name="txt_TravelControlS_ValidDate"
                    class="inputtext formsize40" />
            </td>
            <td align="center">
                <input type="hidden" name="cbx_TravelControlS_IsBan" />
                <input type="checkbox" value="3" class="fuxuank" onclick="TravelControlS.SetValue(this);" />
            </td>
            <td align="center">
                <textarea class="inputtext formsize80" style="height: 30px;" name="txt_TravelControlS_Remarks"></textarea>
            </td>
            <td align="center">
                <select class="inputselect" name="slt_TravelControlS_State" data-value="">
                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisaStatus)), "")%>
                </select>
            </td>
            <td align="center">
                <input type="hidden" name="cbx_TravelControlS_LeaveMsg" value="" />
                <label>
                    <input type="checkbox" class="fuxuank" onclick="TravelControlS.SetValue(this);" value='1' />
                    出团</label>
                <br />
                <input type="hidden" name="cbx_TravelControlS_BackMsg" value="" />
                <label>
                    <input type="checkbox" class="fuxuank" onclick="TravelControlS.SetValue(this);" value='2' />
                    回团</label>
            </td>
            <td align="center">
                <a class="addbtn" href="javascript:void(0)">
                    <img width="48" height="20" src="/images/addimg.gif" border="0" /></a> <a class="delbtn"
                        href="javascript:void(0)">
                        <img src="/images/delimg.gif" border="" /></a>
            </td>
        </tr>
        <%} %>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr class="tempRow">
                    <td height="28" align="center">
                        <input type="text" class="inputtext formsize50" name="txt_TravelControlS_TreavelID_NameCn"
                            value="<%#Eval("CnName")%>" /><br />
                        <input type="text" class="inputtext formsize50" name="txt_TravelControlS_TreavelID_NameEn"
                            value="<%#Eval("EnName") %>" />
                        <input type="hidden" name="txt_TravelControlS_TreavelID" value="<%#Eval("TravellerId")%>" />
                    </td>
                    <td align="center">
                        <select class="inputselect" name="slt_TravelControlS_ProveType" data-value="<%#(int)Eval("CardType") %>">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType)), (int)Eval("CardType"))%>
                        </select>
                    </td>
                    <td align="center">
                        <input type="text" style="width: 60px;" name="txt_TravelControlS_CardNum" class="inputtext formsize50"
                            value="<%#Eval("CardNumber") %>" />
                    </td>
                    <td align="center">
                        <input type="text" style="width: 112px;" name="slt_TravelControlS_Sfz" class="inputtext formsize120"
                            valid="isIdCard" errmsg="请输入正确的身份证号码!" value="<%#Eval("CardId") %>">
                    </td>
                    <td align="center">
                        <select class="inputselect" name="slt_TravelControlS_Sex" data-value="<%#(int)Eval("Gender") %>">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender)), (int)Eval("Gender"))%>
                        </select>
                    </td>
                    <td align="center">
                        <select class="inputselect" name="slt_TravelControlS_PeoType" data-value="<%#(int)Eval("VisitorType") %>">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), (int)Eval("VisitorType"))%>
                        </select>
                    </td>
                    <td align="center">
                        <input type="text" value="<%#Eval("Contact") %>" class="inputtext formsize80" name="txt_TravelControlS_ContactTel" />
                    </td>
                    <td align="center">
                        <input type="text" value="<%#Eval("CardValidDate") %>" style="width: 25px;" name="txt_TravelControlS_ValidDate"
                            class="inputtext formsize40" />
                    </td>
                    <td align="center">
                        <input type="hidden" name="cbx_TravelControlS_IsBan" value='<%#(bool)Eval("IsCardTransact")?"1":"" %>' />
                        <input type="checkbox" value="3" class="fuxuank" onclick="TravelControlS.SetValue(this);"
                            <%#(bool)Eval("IsCardTransact")?"checked='checked'":"" %> />
                    </td>
                    <td align="center">
                        <textarea class="inputtext formsize80" style="height: 30px;" name="txt_TravelControlS_Remarks"><%#Eval("Remark")%></textarea>
                    </td>
                    <td align="center">
                        <select class="inputselect" name="slt_TravelControlS_State" data-value="<%#(int)Eval("VisaStatus")%>">
                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisaStatus)), (int)Eval("VisaStatus"))%>
                        </select>
                    </td>
                    <td align="center">
                        <input type="hidden" name="cbx_TravelControlS_LeaveMsg" value="<%#(bool)Eval("LNotice")?"1":"" %>" />
                        <label>
                            <input type="checkbox" class="fuxuank" onclick="TravelControlS.SetValue(this);" value='1'
                                <%#(bool)Eval("LNotice")?"checked='checked'":"" %> />
                            出团</label>
                        <br />
                        <input type="hidden" name="cbx_TravelControlS_BackMsg" value="<%#(bool)Eval("RNotice")?"1":"" %>" />
                        <label>
                            <input type="checkbox" class="fuxuank" onclick="TravelControlS.SetValue(this);" value='2'
                                <%#(bool)Eval("RNotice")?"checked='checked'":"" %> />
                            回团</label>
                    </td>
                    <td align="center">
                        <a class="addbtn" href="javascript:void(0)">
                            <img width="48" height="20" src="/images/addimg.gif" border="0" /></a> <a class="delbtn"
                                href="javascript:void(0)">
                                <img src="/images/delimg.gif" border="" /></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<script type="text/javascript">
    var TravelControlS = {
        SetValue: function(o) {
            switch (o.value) {
                case "1": if (o.checked) {
                        $(o).closest("td").find("input[name='cbx_TravelControlS_LeaveMsg']").val("1");
                    } else {
                        $(o).closest("td").find("input[name='cbx_TravelControlS_LeaveMsg']").val("");
                    } break;
                case "2": if (o.checked) {
                        $(o).closest("td").find("input[name='cbx_TravelControlS_BackMsg']").val("1");
                    } else {
                        $(o).closest("td").find("input[name='cbx_TravelControlS_BackMsg']").val("");
                    } break;
                case "3": if (o.checked) {
                        $(o).closest("td").find("input[name='cbx_TravelControlS_IsBan']").val("1");
                    } else {
                        $(o).closest("td").find("input[name='cbx_TravelControlS_IsBan']").val("");
                    } break;
            }
        },
        AddCallBack: function(tr) {
            tr.find("a[data-class='tuituan']").remove();
        },
        SelectObj: null
    }
    $(function() {

        $("#TravelControlS_tbl").autoAdd({ addCallBack: TravelControlS.AddCallBack });

        $("#TravelControlS_tbl").find("select[name='slt_TravelControlS_ProveType']").each(function() {
            TravelControlS.SelectObj = $(this);
            if (TravelControlS.SelectObj.attr("data-value")) {
                TravelControlS.SelectObj.attr("value", TravelControlS.SelectObj.attr("data-value"));
            }
        })
        $("#TravelControlS_tbl").find("select[name='slt_TravelControlS_Sex']").each(function() {
            TravelControlS.SelectObj = $(this);
            if (TravelControlS.SelectObj.attr("data-value")) {
                TravelControlS.SelectObj.attr("value", TravelControlS.SelectObj.attr("data-value"));
            }
        })

        $("#TravelControlS_tbl").find("select[name='slt_TravelControlS_PeoType']").each(function() {
            TravelControlS.SelectObj = $(this);
            if (TravelControlS.SelectObj.attr("data-value")) {
                TravelControlS.SelectObj.attr("value", TravelControlS.SelectObj.attr("data-value"));
            }
        })

        $("#TravelControlS_tbl").find("select[name='slt_TravelControlS_State']").each(function() {
            TravelControlS.SelectObj = $(this);
            if (TravelControlS.SelectObj.attr("data-value")) {
                TravelControlS.SelectObj.attr("value", TravelControlS.SelectObj.attr("data-value"));
            }
        })

        $("#cbx_TravelControlS_all").click(function() {
            $("#TravelControlS_tbl").find("input[name='cbx']").attr("checked", $(this).attr("checked"));
        })


        $("#btnImportData").click(function() {
            Boxy.iframeDialog({
                iframeUrl: "/ImportSource/ImportPage.aspx?type=2&box=TravelControlS_tbl",
                title: "导入游客",
                modal: true,
                width: "900px",
                height: "500px"
            });
            return false;
        })
    })


</script>

