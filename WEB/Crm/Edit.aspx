<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Web.Crm.Edit"
    EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <form id="form1" runat="server">
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">基本信息</span>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        <font color="red">*</font>国家：
                    </td>
                    <td width="23%" align="left">
                        <asp:DropDownList ID="ddlCountry" name="ddlCountry" runat="server" CssClass="inputselect"
                            valid="required|RegInteger" errmsg="请选择国家|请选择国家">
                        </asp:DropDownList>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        <font color="red">*</font>省份：
                    </td>
                    <td width="23%" align="left">
                        <asp:DropDownList ID="ddlProvince" name="ddlProvince" runat="server" CssClass="inputselect"
                            valid="required|RegInteger" errmsg="请选择省份|请选择省份">
                        </asp:DropDownList>
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">城市：</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCity" name="ddlCity" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        县区：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:DropDownList ID="ddlCounty" name="ddlCounty" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        <font color="red">*</font>姓名：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtName" name="txtName" valid="required" errmsg="请填写姓名" runat="server"
                            class="inputtext formsize120" MaxLength="10"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        简码：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtBrevityCode" name="txtBrevityCode" runat="server" class="inputtext formsize120"
                            MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        性别：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlGender" name="ddlGender" runat="server" class="inputselect">
                            <asp:ListItem Value="0">男</asp:ListItem>
                            <asp:ListItem Value="1">女</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        身份证号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtIdNumber" name="txtIdNumber" runat="server" class="inputtext formsize180"
                            MaxLength="18"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">出生日期：</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtBirthday" name="txtBirthday" onfocus="WdatePicker();" valid="isDate"
                            errmsg="请填写正确的生日" runat="server" class="inputtext formsize120"></asp:TextBox>
                        <asp:CheckBox ID="chbIsRemind" name="chbIsRemind" runat="server" Text="" />
                        提醒
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        民族：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtNational" name="txtNational" runat="server" class="inputtext formsize120"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        联系电话：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtContactPhone" name="txtContactPhone" runat="server" class="inputtext formsize120" MaxLength="20"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">手机：</span>
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtMobilePhone" name="txtMobilePhone" runat="server"  class="inputtext formsize120" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        QQ：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtQQ"  runat="server" class="inputtext formsize120"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        E-mail：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEMail" runat="server"  class="inputtext formsize180"
                            MaxLength="255"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">家庭住址：</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtHomeAddress" name="txtHomeAddress" runat="server" Style="width: 220px"
                            CssClass="inputtext formsize180" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        邮编：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtZipCode" name="txtZipCode" runat="server" 
                            class="inputtext formsize120" MaxLength="10"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        <font color="red">*</font><span class="alertboxTableT">责任销售：</span>
                    </td>
                    <td align="left" bgcolor="#e0e9ef" colspan="3">
                        <Uc1:Seller ID="Seller1" runat="server" cssclass="formsize180 inputtext" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        工作单位：
                    </td>
                    <td align="left" colspan="5">
                        <asp:TextBox ID="txtWorkUnit" name="txtWorkUnit" runat="server" class="inputtext formsize450"
                            MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        单位地址：
                    </td>
                    <td align="left" colspan="5">
                        <asp:TextBox ID="txtUnitAddress" name="txtUnitAddress" runat="server" CssClass="inputtext formsize450"
                            MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">会员信息</span>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        会员类型：
                    </td>
                    <td width="23%" align="left">
                        <asp:DropDownList ID="ddlMemberType" name="ddlMemberType" valid="PositiveIntegers"
                            errmsg="请选择会员类型" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <font color="red">*</font>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        会员卡号：
                    </td>
                    <td width="23%" align="left">
                        <asp:TextBox ID="txtMemberCardNumber" name="txtMemberCardNumber" runat="server" class="inputtext formsize180"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">会员状态：</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlMemberState" name="ddlMemberState" valid="PositiveIntegers"
                            errmsg="请选择会员状态" CssClass="inputselect" runat="server">
                        </asp:DropDownList>
                        <font color="red">*</font>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        加入时间：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtJoinTime" name="txtJoinTime" onfocus="WdatePicker();" valid="isDate"
                            errmsg="请填写正确的加入时间" runat="server" class="inputtext formsize120"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        报名类型：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtApplicationType" name="txtApplicationType" runat="server" class="inputtext formsize120"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">会费：</span>
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtMemberDues" name="txtMemberDues" runat="server" valid="isMoney"
                            errmsg="请填写正确的金额信息" class="inputtext formsize120" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        备注：
                    </td>
                    <td colspan="5" align="left">
                        <asp:TextBox ID="txtRemark" name="txtRemark" Wrap="true" runat="server" Width="600"
                            Height="80" TextMode="MultiLine" CssClass="inputtext formsize450" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="phdSave" runat="server"><a href="javascript:void(0);" id="btnSave"
                hidefocus="true"><s class="chongzhi"></s>保存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="personalEdit.closeWindow()" class="close"
                hidefocus="true"><s class="chongzhi"></s>关闭</a>
        </div>
        </form>
    </div>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        var personalEdit = {
            //查询参数
            winParams: {}
            //绑定保存按钮事件
            , bindSaveBtn: function() {
                $("#btnSave").bind("click", function() { personalEdit.submit(); });
                $("#btnSave").html("<s class='baochun'></s>保存");
            }
            //取消保存按钮事件
            , unBindSaveBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").html("<s class='baochun'></s>保存中....");
            }
            , init: function() {
                this.winParams = Boxy.getUrlParams();
                this.bindSaveBtn();

                pcToobar.init({
                    gID: "#<%=ddlCountry.ClientID %>",
                    pID: "#<%=ddlProvince.ClientID %>",
                    cID: "#<%=ddlCity.ClientID %>",
                    xID: "#<%=ddlCounty.ClientID %>",
                    gSelect: '<%=this.Country %>',
                    pSelect: '<%=this.Province %>',
                    cSelect: '<%=this.City %>',
                    xSelect: '<%=this.County %>',
                    comID: '<%=this.SiteUserInfo.CompanyId %>'
                });
            }
            //关闭窗口
            , closeWindow: function() {
                var _win = top || window;
                _win.Boxy.getIframeDialog(this.winParams["iframeId"]).hide();
                return false;
            }
            //自定义验证
            , selfValidator: function() {
                var _v = { "result": 0, msg: "" };
                var idCardCode = $.trim($("#<%=txtIdNumber.ClientID %>").val());

                if (idCardCode.length == 0) return _v;
                var params = { "doType": "isexistsidcardcode", "sl": "<%=SL %>", "idcardcode": idCardCode, "crmid": '<%=Utils.GetQueryStringValue("crmid") %>' };

                $.newAjax({
                    type: "GET"
                    , cache: false
                    , url: "Edit.aspx"
                    , dataType: "json"
                    , data: params
                    , async: false
                    , success: function(ret) {
                        if (ret.result == "0") {
                            _v.result = -1;
                            _v.msg = "身份证号码重复";
                        }
                    }
                });

                return _v;
            }
            //提交表单
            , submit: function() {
                this.unBindSaveBtn();
                var validatorResult = ValiDatorForm.validator($("#btnSave").closest("form").get(0), "parent");

                if (!validatorResult) {
                    this.bindSaveBtn();
                    return false;
                }

                var validatorResult = this.selfValidator();

                if (validatorResult.result == -1) {
                    parent.tableToolbar.ShowConfirmMsg("您输入的身份证号码在系统中已经存在，确定要保存吗？", function() {
                        this.bindSaveBtn();
                    })
                    return false;
                }

                var params = { "doType": "save", "sl": "<%=SL %>", "crmid": '<%=Utils.GetQueryStringValue("crmid") %>' };

                $.newAjax({
                    type: "post",
                    cache: false,
                    url: Boxy.createUri("Edit.aspx", params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg("操作成功!", function() {
                                if ('<%=Request.QueryString["pIframeID"] %>' != '') {
                                    var addCallBack = personalEdit.winParams["callbackfun"], addPageWin = parent.Boxy.getIframeWindow(personalEdit.winParams["pIframeID"]);
                                    if (addPageWin[addCallBack] != null && typeof addPageWin[addCallBack] == 'function') {
                                        addPageWin[addCallBack]();
                                    }
                                    personalEdit.closeWindow();
                                } else {
                                    parent.location.href = parent.location.href;
                                }
                            })


                        } else {
                            parent.tableToolbar._showMsg("操作失败!", function() {
                                this.bindSaveBtn();
                            })
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
            personalEdit.init();
        });
    </script>

</body>
</html>
