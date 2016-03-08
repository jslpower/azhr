<%@ Page Title="" Language="C#" MasterPageFile="~/Webmaster/mpage.Master" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="Web.Webmaster.setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">

    <script type="text/javascript">
        //��ʼ��ϵͳ����
        function InitSetting() {
            if (setting == null) return;
            $("#txtTourCodeRule").val(setting.TourNoSetting);
            $("#txtShowBeforeMonth").val(setting.ShowBeforeMonth);
            $("#txtShowAfterMonth").val(setting.ShowAfterMonth);
            $("#txtSaveTime").val(setting.SaveTime);
            $("#txtCountryArea").val(setting.CountryArea);
            $("#txtProvinceArea").val(setting.ProvinceArea);
            $("#txtExitArea").val(setting.ExitArea);
            $("#txtIntegralProportion").val(setting.IntegralProportion);

            $("#ckbSkipGuide").attr("checked", setting.SkipGuide ? "checked" : "");
            $("#ckbSkipSale").attr("checked", setting.SkipSale ? "checked" : "");
            $("#ckbSkipFinalJudgment").attr("checked", setting.SkipFinalJudgment ? "checked" : "");

            $("#txtContractRemind").val(setting.ContractRemind);
            $("#txtSContractRemind").val(setting.SContractRemind);
            $("#txtComPanyContractRemind").val(setting.ComPanyContractRemind);

            $("#ckbFinancialExpensesReview").attr("checked", setting.FinancialExpensesReview ? "checked" : "");
            $("#ckbFinancialIncomeReview").attr("checked", setting.FinancialIncomeReview ? "checked" : "");
            $("#ckbArrearsRangeControl").attr("checked", setting.ArrearsRangeControl ? "checked" : "");

            $("#txtHotelControlRemind").val(setting.HotelControlRemind);
            $("#txtCarControlRemind").val(setting.CarControlRemind);
            $("#txtShipControlRemind").val(setting.ShipControlRemind);
            $("#txtSightControlRemind").val(setting.SightControlRemind);
            $("#txtOtherControlRemind").val(setting.OtherControlRemind);

            $("#chkIsEnableKis").attr("checked", setting.IsEnableKis ? "checked" : "");
            $("#chkIsEnableDuanXian").attr("checked", setting.IsEnableDuanXian ? "checked" : "");

            $("#txtMaxUserNumber").val(setting.MaxUserNumber);
            $("input[name='radUserLoginLimitType']").each(function() {
                if (this.value == setting.UserLoginLimitType) this.checked = true;
            });

            $("#chkIsZiDongShanChuSanPinJiHua").attr("checked", setting.IsZiDongShanChuSanPinJiHua ? "checked" : "");
        }

        //��ȡ��ӡ�������õ�HTML printType:��ӡ��������
        function getPrintDocumentHTML(printType) {
            var s = [];            
            var printUri = '';

            //ϵͳδ����ʱ��ȡĬ�ϵ�����
            var printSetting = printDefaultSetting;
            //ϵͳ������ʱȡϵͳ������
            if (setting != null && setting.PrintDocument != null && setting.PrintDocument.length > 0) {
                printSetting = setting.PrintDocument;
            }
            //��ӡ�������������printTypeָ���ĵ��������Ѿ����õ�ֵ 
            for (var i = 0; i < printSetting.length; i++) {
                if (printSetting[i].PrintTemplateType == printType) {
                    printUri = printSetting[i].PrintTemplate; break;
                }
            }

            s.push('<span class="unrequired">*</span>��ӡ�������ã�');
            s.push('<select disabled="disabled" name="txt_print_type_' + printType + '">');
            for (var i = 0; i < printDocumentTypes.length; i++) {
                s.push('<option value="' + printDocumentTypes[i].Value + '" ' + (printDocumentTypes[i].Value == printType ? 'selected="selected"' : '') + '>' + printDocumentTypes[i].Text + '</option>');
            }
            s.push('</select>');

            s.push('&nbsp;&nbsp;ҳ��·����');
            s.push('<input class="input_text" type="text" value="' + printUri + '" style="width: 280px" maxlength="72" name="txt_print_url_' + printType + '">');            
            
            return s.join('');            
        }

        //��ʼ����ӡ��������
        function InitPrintSetting() {
            if (printDocumentTypes.length == 0) return;

            var s = [];
            for (var i = 0; i < printDocumentTypes.length; i++) {
                s.push('<tr>');
                s.push('<td>');
                s.push(getPrintDocumentHTML(printDocumentTypes[i].Value));                
                s.push('</td>');
                s.push('</tr>');
            }

            $("#trPrintDocumentsAfter").before(s.join(""));
        }

        function WebForm_OnSubmit_Validate() {
            return true;
        }

        $(document).ready(function() {
            InitSetting();
            InitPrintSetting();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    ����ϵͳ����-<asp:Literal runat="server" ID="ltrSysName"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <table cellpadding="2" cellspacing="1" style="font-size: 12px; width: 100%;">
        <tr>
            <td>
                <span class="required">*</span>�ź����ɹ���<input type="text" id="txtTourCodeRule" name="txtTourCodeRule" class="input_text" maxlength="72" value="35" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>�б���ʾ����--ǰX�������ݣ�
                <input type="text" id="txtShowBeforeMonth" name="txtShowBeforeMonth" maxlength="3" value="12" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>�б���ʾ����--��X�������ݣ�
                <input type="text" id="txtShowAfterMonth" name="txtShowAfterMonth" maxlength="3" value="12" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>�������λʱ�䣨��λ���ӣ���
                <input type="text" id="txtSaveTime" name="txtSaveTime" maxlength="10" value="1440" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>������--��ǰX���Զ�ֹͣ�տͣ���λ�죩��
                <input type="text" id="txtCountryArea" name="txtCountryArea" maxlength="3" value="3" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>ʡ����--��ǰX���Զ�ֹͣ�տͣ���λ�죩��
                <input type="text" id="txtProvinceArea" name="txtProvinceArea" maxlength="3" value="3" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>������--��ǰX���Զ�ֹͣ�տͣ���λ�죩��
                <input type="text" id="txtExitArea" name="txtExitArea" maxlength="3" value="3" class="input_text" />                
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>���˻�Ա���ֱ�������λ%����
                <input type="text" id="txtIntegralProportion" name="txtIntegralProportion" maxlength="10" value="1" class="input_text" />
                
            </td>
        </tr>
        <tr>
            <td>
                <span class="unrequired">*</span>�Ƿ��������α��ˣ�
                <label>
                    <input type="checkbox" id="ckbSkipGuide" name="ckbSkipGuide" value="1" />����</label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="unrequired">*</span>�Ƿ��������۱��ˣ�
                <label>
                    <input type="checkbox" id="ckbSkipSale" name="ckbSkipSale" value="1" />����</label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="unrequired">*</span>�Ƿ������Ƶ�����
                <label>
                    <input type="checkbox" id="ckbSkipFinalJudgment" name="ckbSkipFinalJudgment" value="1" />����</label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>�Ͷ���ͬ������ǰX�����ѣ�
                <input type="text" id="txtContractRemind" name="txtContractRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>��Ӧ�̺�ͬ������ǰX�����ѣ�
                <input type="text" id="txtSContractRemind" name="txtSContractRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>��˾��ͬ������ǰX�����ѣ�
                <input type="text" id="txtComPanyContractRemind" name="txtComPanyContractRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="unrequired">*</span>����֧���Ǽ��Ƿ���Ҫ��ˣ�
                <label>
                    <input type="checkbox" id="ckbFinancialExpensesReview" name="ckbFinancialExpensesReview" checked="checked" value="1" />��(����֧���Ǽ���Ҫ���)</label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="unrequired">*</span>��������Ǽ��Ƿ���Ҫ��ˣ�
                <label>
                    <input type="checkbox" id="ckbFinancialIncomeReview" name="ckbFinancialIncomeReview" checked="checked" value="1" />��(��������Ǽ���Ҫ���)</label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="unrequired">*</span>�Ƿ���Ƿ���ȿ��ƣ�
                <label>
                    <input type="checkbox" id="ckbArrearsRangeControl" name="ckbArrearsRangeControl" value="1" checked="checked" />����</label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="unrequired">*</span>�Ƿ���KIS���ϣ�
                <label>
                    <input type="checkbox" id="chkIsEnableKis" name="chkIsEnableKis" value="1"/>����(�������տ���ˡ�����֧�������֧�������������Ż���ֲ�������)</label>
            </td>
        </tr>        
        <tr>   
             <td>
                <span class="unrequired">*</span>�Ƿ������ߣ�
                <label>
                    <input type="checkbox" id="chkIsEnableDuanXian" name="chkIsEnableDuanXian" value="1"/>����</label>
            </td>
        </tr>
        <tr>   
             <td>
                <span class="unrequired">*</span>�Ƿ��Զ�ɾ����Чɢƴ�ƻ���
                <label>
                    <input type="checkbox" id="chkIsZiDongShanChuSanPinJiHua" name="chkIsZiDongShanChuSanPinJiHua"
                        value="1" />�Զ�ɾ������ʱ�䳬��5�죬����δ�ɼƻ���û�ж�����ɢƴ�ƻ�</label>
            </td>
        </tr>        
        <tr>
            <td>
                <span class="required">*</span>����Ԥ�ص������������ǰN�����ѣ�
                <input type="text" id="txtHotelControlRemind" name="txtHotelControlRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>����Ԥ�ص������������ǰN�����ѣ�
                <input type="text" id="txtCarControlRemind" name="txtCarControlRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>�δ�Ԥ�ص������������ǰN�����ѣ�
                <input type="text" id="txtShipControlRemind" name="txtShipControlRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>����Ԥ�ص������������ǰN�����ѣ�
                <input type="text" id="txtSightControlRemind" name="txtSightControlRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>����Ԥ�ص������������ǰN�����ѣ�
                <input type="text" id="txtOtherControlRemind" name="txtOtherControlRemind" maxlength="3" value="15" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>����û���(����0��������)��
                <input type="text" id="txtMaxUserNumber" name="txtMaxUserNumber" maxlength="3" value="0" class="input_text" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="required">*</span>�û���¼���ƣ�
                <label><input type="radio" name="radUserLoginLimitType" value="0" />������</label>
                <label><input type="radio" name="radUserLoginLimitType" value="1" />�����¼��Ч</label>
            </td>
        </tr>
        
        <tr id="trPrintDocumentsAfter">
            <td align="right">
                <asp:Button runat="server" ID="btnSave" Text="����" OnClick="btnSave_Click" OnClientClick="return WebForm_OnSubmit_Validate()" />
                <input type="button" value="����" onclick="javascript:window.location.href = 'systems.aspx';" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageRemark" runat="server">
</asp:Content>
