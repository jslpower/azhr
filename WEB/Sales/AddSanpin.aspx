<%@ Page Title="����ɢƴ" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="AddSanpin.aspx.cs" Inherits="EyouSoft.Web.Sales.AddSanpin" ValidateRequest="false" %>

<%@ Register Src="../UserControl/PriceStand.ascx" TagName="PriceStand" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc2" %>
<%--<%@ Register Src="../UserControl/CostAccounting.ascx" TagName="CostAccounting" TagPrefix="uc3" %>--%>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc8" %>
<%@ Register Src="~/UserControl/SelectJourneySpot.ascx" TagName="SelectJourneySpot"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/TuanFengWeiCan.ascx" TagName="TuanFengWeiCan" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ register src="~/UserControl/TuanZiFei.ascx" tagname="TuanZiFei" tagprefix="uc1" %>
    <%@ register src="~/UserControl/TuanZengSong.ascx" tagname="TuanZengSong" tagprefix="uc1" %>
    <%@ register src="~/UserControl/TuanXiaoFei.ascx" tagname="TuanXiaoFei" tagprefix="uc1" %>
    <%@ register src="~/UserControl/SelectJourney.ascx" tagname="SelectJourney" tagprefix="uc1" %>
    <%@ register src="~/UserControl/TuanDiJie.ascx" tagname="TuanDiJie" tagprefix="uc1" %>
    <%@ register src="~/UserControl/TuanXingCheng.ascx" tagname="TuanXingCheng" tagprefix="uc2" %>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style>
        td.noboder td
        {
            border: 0px;
            margin: 0;
            padding: 0;
            white-space: normal;
            height: auto;
        }
        #spanPlanContent td
        {
            border: 0px;
            margin: 0;
            padding: 0;
            white-space: normal;
            height: auto;
        }
        #spanPlanContent tr
        {
            height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="wrap">
        <!--����-->
        <div class="mainbox mainbox-whiteback">
            <div class="addContent-box">
                <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                    <tbody>
                        <tr>
                            <td width="12%" class="addtableT">
                                �Ƿ�ͬ�з�����
                            </td>
                            <td width="23%" class="kuang2">
                                <asp:CheckBox ID="cbxDistribution" runat="server" />
                            </td>
                            <td width="10%" class="addtableT">
                                <font class="fontbsize12">* </font>�Ŷ����ͣ�
                            </td>
                            <td width="15%" class="kuang2">
                                <input type="hidden" name="hidtourmode" value="0" id="hidtourmode" />
                                <input type="hidden" name="hidTourModeValue" runat="server" id="hidTourModeValue" />
                                <select name="txtTuanXing" id="txtTuanXing" runat="server">
                                    <option value="0">����</option>
                                    <option value="1">����</option>
                                </select>
                            </td>
                            <td width="10%" class="addtableT">
                            <font class="fontbsize12">*</font> ������
                            </td>
                            <td width="30%" class="kuang2">
                             <asp:TextBox ID="txt_Days" runat="server" CssClass="inputtext formsize40" errmsg="����������!|��������ȷ������!|�����������0!"
                                    valid="required|isInt|range" min="1"></asp:TextBox>
                                <button class="addtimebtn" type="button" id="btnAddDays">
                                    �����ճ�</button>
                            </td>
                        </tr>
                        <tr>
                            <td width="12%" class="addtableT">
                                ��·����
                            </td>
                            <td width="23%" class="kuang2">
                                <select name="txtAreaId" id="txtAreaId" valid="required" errmsg="��ѡ����·����!">
                                    <asp:Literal runat="server" ID="ltrArea"></asp:Literal>
                                </select>
                            </td>
                            <td width="10%" class="addtableT">
                                <font class="fontbsize12">* </font>��·���ƣ�
                            </td>
                            <td width="15%" class="kuang2">
                                <input type="text" class="formsize180 input-txt" id="txt_RouteName" runat="server"
                                    valid="required" errmsg="��������·����!" />
                                <asp:HiddenField ID="hideRouteID" runat="server" />
                            </td>
                            <td class="addtableT">
                                �Ŷӹ���/������
                            </td>
                            <td class="kuang2">
                                <select id="txtCountryId" name="txtCountryId" valid="required" errmsg="��ѡ�����">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                �ִ���У�
                            </td>
                            <td class="kuang2">
                                <input type="text" id="txtGetCity" name="txtGetCity" runat="server" class="formsize80 input-txt" />
                            </td>
                            <td class="addtableT">
                                ����/ʱ�䣺
                            </td>
                            <td colspan="3" class="kuang2">
                                <input type="text" id="txtGetCityTime" runat="server" class="formsize180 input-txt" />
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                �뿪���У�
                            </td>
                            <td class="kuang2">
                                <input type="text" id="txtOutCity" runat="server" class="formsize80 input-txt" />
                            </td>
                            <td class="addtableT">
                                ����/ʱ�䣺
                            </td>
                            <td colspan="3" class="kuang2">
                                <input type="text" id="txtOutCityTime" runat="server" class="formsize180 input-txt" />
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                OP ��
                            </td>
                            <td class="kuang2">
                                <uc4:SellsSelect ID="txtJiDiaoYuan" runat="server" SetTitle="OP" SMode="true" ReadOnly="true" />
                            </td>
                            <td class="addtableT">
                                <font class="fontbsize12">* </font>Ԥ��������
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txtPeopleCount" runat="server" CssClass="inputtext formsize40" errmsg="������Ԥ������!|��������ȷ��Ԥ������!|Ԥ������������1-999֮��!"
                                    valid="required|isInt|range" min="1" max="999"></asp:TextBox>
                            </td>
                            <td class="addtableT">
                                �������ڣ�
                            </td>
                            <td class="kuang2">
                                <asp:PlaceHolder ID="phdSelectDate" runat="server">
                                    <asp:Label ID="lblLeaveDate" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hideLeaveDate" runat="server" />
                                    <a style="font-weight: bold; color: #fff; background-color: #ff6600; display: block;
                                        width: 80px; height: 17px; text-align: center; padding-top: 5px;" id="a_SelectData"
                                        href="javascript:void(0);">��ѡ������</a></asp:PlaceHolder>
                                <asp:PlaceHolder Visible="false" ID="phdSelectDate1" runat="server">
                                    <input type="text" class="formsize80 input-txt" id="txtLDate" runat="server" onfocus="WdatePicker()"
                                        valid="required" errmsg="�����뷢������!" />
                                </asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                ����Ա��
                            </td>
                            <td colspan="5" class="kuang2 pand4">
                                <uc4:SellsSelect ID="SellsSelect1" runat="server" SetTitle="����Ա" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="addtableT">
                                �ļ��ϴ���
                            </td>
                            <td colspan="5" class="kuang2 pand4">
                                <uc8:UploadControl ID="txtFuJian1" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                                    IsUploadMore="true" IsUploadSelf="true" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="addtableT">
                                �Ŷ�ȷ���ϴ���
                            </td>
                            <td colspan="5" class="kuang2 pand4">
                                <uc8:UploadControl ID="txtFuJian2" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                                    IsUploadMore="true" IsUploadSelf="true" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="addtableT">
                                ������ͨ��
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccesssStraffBegin" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <%--<tr>
                            <td class="addtableT">
                                ���̽�ͨ��
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccesssStraffEnd" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <%--<tr>
                            <td class="addtableT">
                                ���Ϸ�ʽ��
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSuccessGather" runat="server" CssClass="inputtext formsize600"
                                    TextMode="MultiLine" Height="50"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="addtableT">
                                �����ϴ���
                            </td>
                            <td class="kuang2" colspan="5">
                                <uc8:UploadControl ID="UploadControl2" runat="server" IsUploadMore="true" IsUploadSelf="true" />
                                <asp:Label ID="lblFiles" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                �г���ɫ��<uc3:SelectJourneySpot runat="server" ID="SelectJourneySpot1" />
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <span id="spanPlanContent" style="display: inline-block;">
                                    <asp:TextBox ID="txtPlanContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"
                                        Height="55"></asp:TextBox></span>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="addtableT">
                                �ؼ��֣�
                            </td>
                            <td class="kuang2 pand4" colspan="5">
                                <asp:TextBox ID="txtSearchKey" runat="server" CssClass="inputtext formsize600" Height="17"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <%--<asp:PlaceHolder ID="phdVisaFile" runat="server">
                            <tr>
                                <td class="addtableT">
                                    ǩ֤���ϣ�
                                </td>
                                <td class="kuang2" colspan="5">
                                    <uc8:UploadControl ID="UploadControl1" runat="server" />
                                    <asp:Label ID="lblVisaFiles" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </asp:PlaceHolder>--%>
                    </tbody>
                </table>
            </div>
            <div class="hr_5">
            </div>
            <div style="width: 98.5%" class="tablelist-box ">
                <uc1:PriceStand ID="PriceStand1" runat="server" />
                <%-- <uc1:PriceStand ID="PriceStand1" runat="server" />--%>
            </div>
            <div class="hr_5">
            </div>
            <uc2:TuanXingCheng ID="TuanXingCheng1" runat="server" />
            <div class="hr_5">
            </div>
            <div style="width: 98.5%" class="tablelist-box" id="TabList_Box">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <th align="right">
                                ��ζ�ͣ�
                            </th>
                            <td align="left">
                                <uc3:TuanFengWeiCan ID="TuanFengWeiCan1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th align="right">
                                �Է���Ŀ��
                            </th>
                            <td align="left">
                                <uc1:TuanZiFei runat="server" ID="TuanZiFei1" />
                            </td>
                        </tr>
                        <tr>
                            <th align="right">
                                ���ͣ�
                            </th>
                            <td align="left">
                                <uc1:TuanZengSong runat="server" ID="TuanZengSong1" />
                            </td>
                        </tr>
                        <tr>
                            <th align="right">
                                С�ѣ�
                            </th>
                            <td align="left">
                                <uc1:TuanXiaoFei runat="server" ID="TuanXiaoFei1" />
                            </td>
                        </tr>
                        <%-- <tr>
                        <th align="right">
                            ���Է���Ҫ��
                        </th>
                        <td align="left">
                            <span id="spanSpecificRequire" style="display: inline-block;">
                                <asp:TextBox ID="txtSpecificRequire" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                            </span>
                        </td>
                    </tr>--%>
                        <tr>
                            <th align="right">
                                �г̱�ע��<uc1:SelectJourney runat="server" ID="SelectJourney1" />
                            </th>
                            <td align="left">
                                <span id="spanJourney" style="display: inline-block;">
                                    <asp:TextBox ID="txtJourney" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="hr_5">
            </div>
            <uc1:TuanDiJie runat="server" ID="TuanDiJie1" />
            <%--<uc3:CostAccounting ID="CostAccounting1" runat="server" />--%>
            <div class="mainbox cunline">
                <ul id="ul_AddSanPlan_Btn">
                    <asp:PlaceHolder ID="phdSave" runat="server">
                        <li class="cun-cy"><a href="javascript:void(0);" data-name="����" id="btnSave">����</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdCanel" runat="server">
                        <li class="cun-cy"><a id="btnCanel" href="javascript:void(0);" data-name="�����б�">�����б�</a></li></asp:PlaceHolder>
                </ul>
                <div class="hr_10">
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <div class="alertbox-outbox03" id="div_Change" style="display: none">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        ������⣺
                    </td>
                    <td bgcolor="#E9F4F9" align="left">
                        <input type="text" id="txt_ChangeTitle" class="inputtext formsize600" style="height: 17px;"
                            name="txt_ChangeTitle">
                    </td>
                </tr>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        �����ϸ��
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <textarea id="txt_ChangeRemark" style="height: 100px;" class="inputtext formsize600"
                            name="txt_ChangeRemark"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a hidefocus="true" href="javascript:void(0);" id="btnChangeSave"><s class="baochun">
            </s>�� ��</a><a hidefocus="true" href="javascript:void(0);" onclick="AddSanPlan.ChangeBox.hide();return false;"><s
                class="chongzhi"></s>�ر�</a>
        </div>
    </div>
    <asp:HiddenField ID="hideOverrunState" runat="server" Value="0" />
    <asp:HiddenField ID="hideSysStopCount" runat="server" />
    <asp:HiddenField ID="hideIsChangeInput" runat="server" />
    <input type="hidden" name="hidbinkemode" id="hidbinkemode" value="1" />
    </form>

    <script type="text/javascript">

        var AddSanPlan = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                act: '<%=Request.QueryString["act"] %>',
                id: '<%=Request.QueryString["id"] %>',
                isparent:'<%=Request.QueryString["isparent"] %>'
            },
            ChangeBox: null,
            CheckForm: function() {
                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");
            },
            //�ύ��
            Save: function() {
                PriceStand.Init();
                KEditer.sync();
                $("#btnSave").text("�ύ��...");
                AddSanPlan.UnBindBtn();
                AddPrice.SetCityAndTraffic();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sales/AddSanpin.aspx?dotype=save&" + $.param(AddSanPlan.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax�ط���ʾ
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = "/Sales/SanpinList.aspx?sl=" + AddSanPlan.Data.sl + "&type=" + AddSanPlan.Data.type;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            AddSanPlan.BindBtn();
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        AddSanPlan.BindBtn();
                    }
                });
            },
            //��ť���¼�
            BindBtn: function() {
                //���
                $("#btnChangeSave").unbind("click").click(function() {
                    if ($.trim($("#txt_ChangeTitle").val()) == "") {
                        tableToolbar._showMsg("������ⲻ��Ϊ��!");
                        return false;
                    }
                    if ($.trim($("#txt_ChangeRemark").val()) == "") {
                        tableToolbar._showMsg("�����ϸ����Ϊ��!");
                        return false;
                    }
                    if (AddSanPlan.CheckForm()) {
                        AddSanPlan.ChangeBox.hide();
                        AddSanPlan.Save();
                    }

                })

                $("#ul_AddSanPlan_Btn").find("a").css("background-position", "0 0");
                $("#ul_AddSanPlan_Btn").find("a").each(function() {
                    $(this).html($(this).attr("data-name"));
                })

                $("#btnSave").unbind("click").click(function() {
                    //var result = ValiDatorForm.validator($("#form1").get(0), "alert");
                    if (AddSanPlan.CheckForm()) {
                        if ($("#<%=hideLeaveDate.ClientID %>").val() == "") {
                            tableToolbar._showMsg("��ѡ���������!");
                            return false;
                        }
                        if ($("#<%=hideIsChangeInput.ClientID %>").val() == "true" && AddSanPlan.Data.act == "update") {
                            AddSanPlan.ChangeBox = new Boxy($("#div_Change"), { modal: true, fixed: false, title: "�����ϸ", width: "725px", height: "245px", display: "none" });
                        } else {
                            AddSanPlan.SaveType = 1;
                            AddSanPlan.Save();
                        }

                    }
                    return false;
                });

                $("#btnCanel").unbind("click").click(function() {
                    window.history.go(-1);
                    return false;
                })
            },
            //��ť�����
            UnBindBtn: function() {
                $("#ul_AddSanPlan_Btn").find("a").unbind("click");
                $("#ul_AddSanPlan_Btn").find("a").css("background-position", "0 -62px");
            }, //ɾ��ǩ֤����
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            },
            //����
            SysSettingList: null,
            CreatePlanEdit: function() {
                //�����г̱༭��
                KEditer.init('<%=txtJourney.ClientID %>');
                KEditer.init('<%=txtPlanContent.ClientID %>');
            },
            PageInit: function() {
                this.CreatePlanEdit();

                $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txt_Days.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack });

                $("#a_SelectData").click(function() {
                    Boxy.iframeDialog({
                        iframeUrl: "/Sales/SelectChildTourDate.aspx?hide=<%=hideLeaveDate.ClientID %>&show=<%=lblLeaveDate.ClientID %>&old=" + $("#<%=hideLeaveDate.ClientID %>").val(),
                        title: "ѡ�񷢰�����",
                        modal: true,
                        width: "925px",
                        height: "315px"
                    });
                });

                $("#btnAddDays").click(function() {
                    var day = tableToolbar.getInt($("#<%=txt_Days.ClientID %>").val());
                    day++;
                    $("#<%=txt_Days.ClientID %>").val(day);
                    $("#<%=txt_Days.ClientID %>").change();
                });

                $("#sltArea").change(function() {
                    //�����͸��Ƶ�ʱ�� ������·�����޸�ͣ������
                    if (AddSanPlan.Data.act == "add" || AddSanPlan.Data.act == "copy") {
                        if (AddSanPlan.SysSettingList.length > 2) {

                        }
                    }
                });

                $("#<%=txt_RouteName.ClientID %>").unbind("blur").blur(function() {
                    var _$routename = $(this);
                    var _$routeid = $("#<%=hideRouteID.ClientID %>");
                    if ($.trim(_$routename.val()) != _$routename.attr("data-value")) _$routeid.val("");
                    else _$routeid.val(_$routeid.attr("data-value"));
                });
            }
        }

        $(function() {
            //����֤��ʼ��
            FV_onBlur.initValid($("#<%=txt_RouteName.ClientID %>").closest("form").get(0));
            AddSanPlan.BindBtn();
            AddSanPlan.SysSettingList = $("#<%=hideSysStopCount.ClientID %>").val().split(',');
            AddSanPlan.PageInit();

            pcToobar.init({ gID: "#txtCountryId", gSelect: '<%=CountryId %>' });
            $("#txtCountryId").change(function() {
                $("#hidbinkemode").val($(this).val())
            });
            $("#txtAreaId").val("<%=AreaId %>");
        });
    </script>

    <script type="text/javascript">

        var AddPrice = {
            SetTrafficPrice: function() {//�����ͨ�۸�

            },
            SetHotelPrice: function() {//����Ƶ�۸�

            },
            SetTotalPrice: function() {//���ܼ۸�

            },
            SetSumPrice: function() {//�����ܼ�

            },
            SumHeJiPrice: function() {

            },
            SumTipPrice: function() {//С�Ѻϼ�

            },
            SumZongFeiPrice: function() {//�����۷�

            },
            SumMenuPrice: function() {//����͹ݲ˵��ļ۸�

            },
            SetCityAndTraffic: function() {
                $(".TabCity").each(function() {
                    var self = $(this);
                    var cityid = [];
                    var cityname = [];
                    var traffic = [];
                    var trafficprice = [];
                    self.find("input[name='hidcityid']").each(function() { cityid.push($(this).val()); });
                    self.find("input[name='txtcity']").each(function() { cityname.push($(this).val()); });
                    self.find("input[name='hidtraffictype']").each(function() { traffic.push($(this).val()); });
                    self.find("input[name='txttrafficprice']").each(function() { trafficprice.push($(this).val()); });

                    self.closest(".TabCity").parent().find("input[name='hidcityids']").val(cityid.join(','));
                    self.closest(".TabCity").parent().find("input[name='hidcitys']").val(cityname.join(','));
                    self.closest(".TabCity").parent().find("input[name='hidtraffics']").val(traffic.join(','));
                    self.closest(".TabCity").parent().find("input[name='hidtrafficprices']").val(trafficprice.join(','));
                })
            }
        };
    
    
    </script>

</asp:Content>
