<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleServerEdit.aspx.cs"
    Inherits="EyouSoft.WEB.Sales.SingleServerEdit" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SupplierControl.ascx" TagName="SupplierControl"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>单项业务-新增</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/swfupload/swfupload.js"></script>

    <script src="/js/datepicker/wdatepicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <input type="hidden" runat="server" id="Agreement" />
    <input type="hidden" id="HDCustomType" runat="server" />
    <input type="hidden" id="HDCertificatesType" runat="server" />
    <input type="hidden" id="HDSexType" runat="server" />
    <input type="hidden" id="HDServerType" runat="server" />
    <div class="alertbox-outbox" style="padding-top: 5px;">
        <table id="tablelist" width="99%" align="center" cellpadding="0" cellspacing="0"
            bgcolor="#e9f4f9" style="margin: 0 auto;">
            <%--            <asp:PlaceHolder runat="server" ID="phOrderCode" Visible="false">
                <tr>
                    <td class="alertboxTableT" style="text-align: right; height: 28px; background: #b7e0f3;">
                        订单号：
                    </td>
                    <td colspan="5">
                        <asp:Literal runat="server" ID="ltrOrderCode"></asp:Literal>
                    </td>
                </tr>
            </asp:PlaceHolder>--%>
            <tr>
                                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font color="#FF0000">*</font>订单日期：
                </td>
                <td width="26%" align="left">
                    <asp:TextBox ID="txtWeiTuoRiQi" runat="server" class="inputtext formsize100" onfocus="WdatePicker()"
                        valid="required" errmsg="请输入委托日期"></asp:TextBox>
                </td>
                <td width="12%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <font color="#FF0000">*</font>销售员：
                </td>
                <td width="20%">
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SMode="false" readonly="true" />
                </td>
                <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    OP：
                </td>
                <td width="20%">
                    <uc4:SellsSelect ID="SellsSelect2" runat="server" SMode="true" IsMust="false" SetTitle="选择OP" />
                </td>
            </tr>
            <tr>
                <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    操作员：
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtOpeator" runat="server" class="inputtext formsize80" Enabled="false"></asp:TextBox>
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font color="#FF0000">*</font>客户单位：
                </td>
                <td width="30%" align="left" bgcolor="#e0e9ef">
                    <uc1:CustomerUnitSelect runat="server" ID="CustomerUnitSelect1" IsApply="false" />
                    <input type="hidden" value="" id="hdContactdepartid" runat="server" />
                </td>
                <td width="10%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    联系人：
                </td>
                <td width="20%" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtContactName" runat="server" class="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" align="right" height="28px" bgcolor="#B7E0F3" class="alertboxTableT">
                    联系电话：
                </td>
                <td width="20%" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtContactTel" runat="server" class="inputtext formsize140" name="ContactTel"></asp:TextBox>
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font color="#FF0000"></font>人数：
                </td>
                <td bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtAdultCount" runat="server" class="inputtext formsize80"></asp:TextBox>
                </td>
                    <td class="alertboxTableT" style="text-align: right; height: 28px; background: #b7e0f3;">
                        订单号：
                    </td>
                    <td width="20%" align="left">
                        <asp:Label ID="txtOrderCode" runat="server" Text=""></asp:Label>
                    </td>
            </tr>
            <tr>
                <td rowspan="2" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    客人详情：
                </td>
                <td height="28" align="left">
                    <p>
                        <span class="redanniu"><a href="javascript:void(0);" id="link3">
                            <img src="/images/daorumd-cy.gif" alt="" title="导入名单" />导入名单</a></span></p>
                </td>
                <td height="28" align="center">
                    上传附件：
                </td>
                <td colspan="3">
                    <uc5:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true" Setimgwidth="92" SetImgheight="24" SizeLimit="4" />
                    <asp:Label ID="lbFiles" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="5" align="left" style="padding: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" id="Customlist">
                        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                姓名
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                类型
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                证件名称
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                证件号码
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                签发日期
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                有效期
                            </td>                            
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" rowspan="2">
                                操作
                            </td>
                        </tr>
                        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                出生日期
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                签发地
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                性别
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                联系电话
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                里程积分
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                备注
                            </td>
                        </tr>
                        <%if (!(this.CustomListCount > 0))
                          {%>
                        <tr class="showlist">
                            <td height="26" align="center">
                                <input type="hidden" name="hidTravellerId" value="" />
                                <input name="CustomName" type="text" class="inputtext formsize50" id="text1" />
                            </td>
                            <td height="26" align="center">
                                <select name="CustomType" class="inputselect">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), "0", false)%>
                                </select>
                            </td>
                            <td height="26" align="center">
                                <select name="Certificates" class="inputselect" onchange="CustomListControl.ProveTypeOnChange(this)">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType)), "0", false)%>
                                </select>
                            </td>
                            <td height="26" align="center">
                                <input name="CertificatesNum" type="text" id="text2" class="inputtext formsize120" />
                            </td>
                            <td height="26" align="center">
                                <input name="QianFaDate" type="text" id="text15" class="inputtext formsize80" onfocus="WdatePicker()"/>
                            </td>
                            <td height="26" align="center">
                                <input name="CardValidDate" type="text" id="text16" class="inputtext formsize80" />
                            </td>
                            <td height="26" align="center" rowspan="2">
                                <a href="javascript:void(0)" class="addlist">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="dellist">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                        <tr class="showlist">
                            <td height="26" align="center">
                                <input name="Birthday" type="text" id="text27" class="inputtext formsize80" onfocus="WdatePicker()" />
                            </td>
                            <td height="26" align="center">
                                <input name="QianFaDi" type="text" id="text28" class="inputtext formsize80" />
                            </td>
                            <td height="26" align="center">
                                <select name="Sex" class="inputselect">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender)), "0", false)%>
                                </select>
                            </td>
                            <td height="26" align="center">
                                <input name="ContactTel" type="text" class="inputtext formsize80" id="text29" />
                            </td>
                            <td height="26" align="center">
                                <input name="LiCheng" type="text" class="inputtext formsize80" id="text30" valid="isNumber" errmsg="必须是数字!"/>
                            </td>
                            <td height="26" align="center">
                                <input name="Remark" type="text" class="inputtext formsize80" id="text31" />
                            </td>
                        </tr>
                        <%} %>
                        <tr style="display: none" id="Templist">
                            <td height="26" align="center">
                                <input type="hidden" name="hidTravellerId" value="" />
                                <input name="CustomName" type="text" class="inputtext formsize50" id="textfield15" />
                            </td>
                            <td height="26" align="center">
                                <select name="CustomType" class="inputselect">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), "0", false)%>
                                </select>
                            </td>
                            <td height="26" align="center">
                                <select name="Certificates" class="inputselect" onchange="CustomListControl.ProveTypeOnChange(this)">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType)), "0", false)%>
                                </select>
                            </td>
                            <td height="26" align="center">
                                <input name="CertificatesNum" type="text" id="textfield20" class="inputtext formsize120" />
                            </td>
                            <td height="26" align="center">
                                <input name="QianFaDate" type="text" id="text19" class="inputtext formsize80" onfocus="WdatePicker()"/>
                            </td>
                            <td height="26" align="center">
                                <input name="CardValidDate" type="text" id="text20" class="inputtext formsize80" />
                            </td>
                            <td height="26" align="center" rowspan="2">
                                <a href="javascript:void(0)" class="addlist">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="dellist">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                        <tr style="display: none" id="Templist1">
                            <td height="26" align="center">
                                <input name="Birthday" type="text" id="text24" class="inputtext formsize80" onfocus="WdatePicker()" />
                            </td>
                            <td height="26" align="center">
                                <input name="QianFaDi" type="text" id="text25" class="inputtext formsize80" />
                            </td>
                            <td height="26" align="center">
                                <select name="Sex" class="inputselect">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender)), "0", false)%>
                                </select>
                            </td>
                            <td height="26" align="center">
                                <input name="ContactTel" type="text" class="inputtext formsize80" id="text26" />
                            </td>
                            <td height="26" align="center">
                                <input name="LiCheng" type="text" class="inputtext formsize80" id="text32" valid="isNumber" errmsg="必须是数字!"/>
                            </td>
                            <td height="26" align="center">
                                <input name="Remark" type="text" class="inputtext formsize80" id="text33" />
                            </td>
                        </tr>
                        <tr id="Loading">
                            <td colspan="7" style="height: 30px">
                                正在加载...
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptCustomList">
                            <ItemTemplate>
                                <tr class="showlist" style="display: none">
                                    <td height="26" align="center">
                                        <input type="hidden" name="hidTravellerId" value="<%#Eval("TravellerId") %>" />
                                        <input name="CustomName" type="text" class="inputtext formsize50" id="textfield15"
                                            value="<%#Eval("CnName") %>" />
                                    </td>
                                    <td height="26" align="center">
                                        <select name="CustomType" class="inputselect">
                                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), ((int)Eval("VisitorType")).ToString(), false)%>
                                        </select>
                                    </td>
                                    <td height="26" align="center">
                                        <select name="Certificates" class="inputselect" onchange="CustomListControl.ProveTypeOnChange(this)"
                                            data-value="<%#(int)Eval("CardType") %>">
                                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType)), ((int)Eval("CardType")).ToString(), false)%>
                                        </select>
                                    </td>
                                    <td height="26" align="center">
                                        <input name="CertificatesNum" type="text" id="textfield20" class="inputtext formsize120"
                                            value="<%#Eval("CardNumber") %>" />
                                    </td>
                            <td height="26" align="center">
                                <input name="QianFaDate" type="text" id="text15" class="inputtext formsize80" onfocus="WdatePicker()" value="<%#Eval("QianFaDate","{0:yyyy-MM-dd}") %>"/>
                            </td>
                            <td height="26" align="center">
                                <input name="CardValidDate" type="text" id="text16" class="inputtext formsize80"  value="<%#Eval("CardValidDate") %>"/>
                            </td>
                                    <td height="26" align="center" rowspan="2">
                                        <a href="javascript:void(0)" class="addlist">
                                            <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                                class="dellist">
                                                <img src="/images/delimg.gif" width="48" height="20" /></a>
                                    </td>
                                </tr>
                                <tr class="showlist" style="display: none">
                            <td height="26" align="center">
                                <input name="Birthday" type="text" id="text12" class="inputtext formsize80" onfocus="WdatePicker()"  value="<%#Eval("Birthday","{0:yyyy-MM-dd}") %>"/>
                            </td>
                            <td height="26" align="center">
                                <input name="QianFaDi" type="text" id="text21" class="inputtext formsize80"  value="<%#Eval("QianFaDi") %>"/>
                            </td>
                                    <td height="26" align="center">
                                        <select name="Sex" class="inputselect">
                                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender)), ((int)Eval("Gender")).ToString(), false)%>
                                        </select>
                                    </td>
                                    <td height="26" align="center">
                                        <input name="ContactTel" type="text" class="inputtext formsize80" id="text22"
                                            value="<%#Eval("Contact") %>" />
                                    </td>
                                    <td height="26" align="center">
                                        <input name="LiCheng" type="text" class="inputtext formsize80" id="text23" valid="isNumber" errmsg="必须是数字!"
                                            value="<%#Eval("LiCheng") %>" />
                                    </td>
                                    <td height="26" align="center">
                                        <input name="Remark" type="text" class="inputtext formsize80" id="text34"
                                            value="<%#Eval("Remark") %>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    业务内容：
                </td>
                <td colspan="5" align="left" style="padding: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" id="Customrequire">
                        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <font color="#FF0000">*</font>服务类别
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                具体要求
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                单价
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                数量
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <font color="#FF0000">*</font>小计
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                发票内容
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                GST
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                操作
                            </td>
                        </tr>
                        <%if (!(this.CustomRequireCount > 0))
                          {%>
                        <tr class="showrequire">
                            <td height="50" align="center">
                                <select class="inputselect" name="ServerType" valid="required" errmsg="请选择服务类别" onchange="SingleEditPage.initFuWuLeiBie(this)">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.ContainProjectType)) , "", "","请选择")%>
                                </select>
                            </td>
                            <td height="50" align="center">
                                <textarea name="ServiceStandard" class=" inputtext" style="height: 85px; width: 350px;"
                                    cols="ds"></textarea>
                            </td>
                            <td height="50" align="center">
                                <input valid="isNumber" errmsg="必须是数字!" name="DanJia" type="text" id="text5"
                                    class="inputtext formsize40 price" />
                            </td>
                            <td height="50" align="center">
                                <input valid="isNumber" errmsg="必须是数字!" name="ShuLiang" type="text" id="text6"
                                    class="inputtext formsize40 price" />
                            </td>
                            <td height="50" align="center">
                                <input valid="isNumber" errmsg="必须是数字!" name="Price" type="text" id="textfield82"
                                    class="inputtext formsize40 price" />
                            </td>
                            <td height="50" align="center">
                                <textarea name="remarkCustom" class="inputtext formsize180" style="height: 85px;
                                    width: 190px;"></textarea>
                            </td>
                            <td height="50" align="center">
                                <input type="hidden" name="hidIsTax" value="false"/>
                                <input type="checkbox" onclick='$(this).closest("td").find(":hidden").val(this.checked)'/>
                            </td>
                            <td height="50" align="center">
                                <a href="javascript:void(0)" class="addrequire">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="delrequire">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                        <%} %>
                        <tr id="Temprequire" style="display: none">
                            <td height="50" align="center">
                                <select class="inputselect" name="ServerType" onchange="SingleEditPage.initFuWuLeiBie(this)">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.ContainProjectType)), "", "","请选择")%>
                                </select>
                            </td>
                            <td height="50" align="center">
                                <textarea name="ServiceStandard" class="inputtext" id="textarea1" style="height: 85px;
                                    width: 350px;"></textarea>
                            </td>
                            <td height="50" align="center">
                                <input valid="isNumber" errmsg="必须是数字!" name="DanJia" type="text" id="text7"value="0"
                                    class="inputtext formsize40 price" />
                            </td>
                            <td height="50" align="center">
                                <input valid="isNumber" errmsg="必须是数字!" name="ShuLiang" type="text" id="text8"value="0"
                                    class="inputtext formsize40 price" />
                            </td>
                            <td height="50" align="center">
                                <input valid="isNumber" errmsg="必须是数字!" name="Price" type="text" id="text4" class="inputtext formsize40 price"
                                    value="0" />
                            </td>
                            <td height="50" align="center">
                                <textarea name="remarkCustom" class="bk pand4 inputtext formsize180" id="textarea2"
                                    style="height: 85px"></textarea>
                            </td>
                            <td height="50" align="center">
                                <input type="hidden" name="hidIsTax" value="false"/>
                                <input type="checkbox" onclick='$(this).closest("td").find(":hidden").val(this.checked)'/>
                            </td>
                            <td height="50" align="center">
                                <a href="javascript:void(0)" class="addrequire">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="delrequire">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                        <asp:Repeater ID="rptRequire" runat="server">
                            <ItemTemplate>
                                <tr class="showrequire">
                                    <td height="50" align="center">
                                        <select class="inputselect" name="ServerType" valid="required" errmsg="请选择服务类别" onchange="SingleEditPage.initFuWuLeiBie(this)">
                                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.ContainProjectType)), ((int)Eval("ServiceType")).ToString(), "","请选择")%>
                                        </select>
                                    </td>
                                    <td height="50" align="center">
                                        <textarea name="ServiceStandard" class="bk pand4 inputtext" id="textarea1" style="height: 85px;
                                            width: 350px;"><%#Eval("ServiceStandard") %></textarea>
                                    </td>
                                    <td height="50" align="center">
                                        <input valid="isNumber" errmsg="必须是数字!" name="DanJia" type="text" id="text5"
                                            class="inputtext formsize40 price" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("DanJia"))) %>" />
                                    </td>
                                    <td height="50" align="center">
                                        <input valid="isNumber" errmsg="必须是数字!" name="ShuLiang" type="text" id="text6"
                                            class="inputtext formsize40 price" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("ShuLiang"))) %>" />
                                    </td>
                                    <td height="50" align="center">
                                        <input valid="isNumber" errmsg="必须是数字!" name="Price" type="text" id="text4" class="inputtext formsize40 price"
                                            value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("Quote"))) %>" />
                                    </td>
                                    <td height="50" align="center">
                                        <textarea name="remarkCustom" class="bk pand4 inputtext formsize180" style="height: 85px"><%#Eval("Remark")%></textarea>
                                    </td>
                                    <td height="50" align="center">
                                <input type="hidden" name="hidIsTax" value="<%#Eval("IsTax") %>"/>
                                <input type="checkbox" onclick='$(this).closest("td").find(":hidden").val(this.checked);' <%#(bool)Eval("IsTax")?"checked":"" %>/>
                            </td>
                                    <td height="50" align="center">
                                        <a href="javascript:void(0)" class="addrequire">
                                            <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                                class="delrequire">
                                                <img src="/images/delimg.gif" width="48" height="20" /></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="true">
                <tr>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        供应商：
                    </td>
                    <td colspan="5" align="left" style="padding: 5px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd i_gysap"
                            id="sourceplan">
                            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                    <font color="#FF0000">*</font>供应商类别
                                </td>
                                <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                    <font color="#FF0000">*</font>供应商名称
                                </td>
                                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                    具体安排
                                </td>
                                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                    数量
                                </td>
                                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                    <font color="#FF0000">*</font>结算费用
                                </td>
                                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                    支付方式
                                </td>
                                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                    费用明细
                                </td>
                                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT i_gysap_cz_0">
                                    操作
                                </td>
                            </tr>
                            <%if (!(this.SourcePlanCount > 0))
                              {%>
                            <tr class="showsourceplan">
                                <td align="center">
                                    <input type="hidden" name="PLanId" />
                                    <select class="inputselect" name="Sourcetype">
                                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EnumSource,"1", false)%>
                                    </select>
                                </td>
                                <td height="50" align="center">
                                    <input type="hidden" name="ShowID" value="" />
                                    <input name="SourceName" readonly="readonly" type="text" class="inputtext formsize80"
                                        value="<%#Eval("SourceName") %>" />
                                    <input type="hidden" name="ContactName" value="" />
                                    <input type="hidden" name="ContactPhone" value="" />
                                    <input type="hidden" name="ContactFax" value="" />
                                    <a href="javascript:void(0);" class="xuanyong Offers"></a>
                                </td>
                                <td height="50" align="center">
                                    <textarea name="GuideNotes" class="bk pand4 inputtext" style="height: 55px; width: 140px;"></textarea>
                                </td>
                                <td height="50" align="center">
                                    <input valid="isNumber" onafterpaste="this.value=this.value.replace(/\D/g,'')" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                        errmsg="必须是数字!" name="Count" type="text" class="inputtext formsize20" />
                                </td>
                                <td height="50" align="center">
                                    <input valid="isNumber" errmsg="必须是数字!" name="PlanCost" type="text" class="inputtext formsize50 plancost" />
                                </td>
                                <td height="50" align="center">
                                    <select name="PayType" class="inputselect">
                                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "1", false)%>
                                    </select>
                                </td>
                                <td height="50" align="center">
                                    <textarea name="remarkSource" class="bk pand4 inputtext" style="height: 55px; width: 160px;"></textarea>
                                </td>
                                <td height="50" align="center" class="i_gysap_cz_1">
                                    <a href="javascript:void(0)" class="addsourceplan">
                                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                            class="delsourceplan">
                                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                                </td>
                            </tr>
                            <%} %>
                            <tr id="Tempsourceplan" style="display: none">
                                <td align="center">
                                    <input type="hidden" name="PLanId" />
                                    <select class="inputselect" name="Sourcetype">
                                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EnumSource,"1", false)%>
                                    </select>
                                </td>
                                <td height="50" align="center">
                                    <input type="hidden" name="ShowID" value="" />
                                    <input name="SourceName" readonly="readonly" type="text" class="inputtext formsize80"
                                        value="<%#Eval("SourceName") %>" />
                                    <input type="hidden" name="ContactName" value="" />
                                    <input type="hidden" name="ContactPhone" value="" />
                                    <input type="hidden" name="ContactFax" value="" /><a href="javascript:void(0);" class="xuanyong Offers"></a>
                                </td>
                                <td height="50" align="center">
                                    <textarea name="GuideNotes" class="bk pand4 inputtext" style="height: 55px; width: 160px;"></textarea>
                                </td>
                                <td height="50" align="center">
                                    <input valid="isNumber" onafterpaste="this.value=this.value.replace(/\D/g,'')" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                        errmsg="必须是数字!" name="Count" type="text" class="inputtext formsize20" />
                                </td>
                                <td height="50" align="center">
                                    <input valid="isNumber" errmsg="必须是数字!" name="PlanCost" type="text" class="inputtext formsize50 plancost" />
                                </td>
                                <td height="50" align="center">
                                    <select name="PayType" class="inputselect">
                                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "1", false)%>
                                    </select>
                                </td>
                                <td height="50" align="center">
                                    <textarea name="remarkSource" class="bk pand4 inputtext" style="height: 55px; width: 160px;"></textarea>
                                </td>
                                <td height="50" align="center" class="i_gysap_cz_1">
                                    <a href="javascript:void(0)" class="addsourceplan">
                                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                            class="delsourceplan">
                                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptsourceplan" runat="server">
                                <ItemTemplate>
                                    <tr class="showsourceplan">
                                        <td align="center">
                                            <input type="hidden" name="PLanId" value="<%#Eval("PlanId") %>" />
                                            <select class="inputselect" name="Sourcetype">
                                                <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EnumSource,((int)Eval("Type")).ToString(), false)%>
                                            </select>
                                        </td>
                                        <td height="50" align="center">
                                            <input type="hidden" name="ShowID" value='<%#Eval("SourceId") %>' />
                                            <input name="SourceName" readonly="readonly" type="text" class="inputtext formsize80"
                                                value="<%#Eval("SourceName") %>" />
                                            <input type="hidden" name="ContactName" value='<%#Eval("ContactName") %>' />
                                            <input type="hidden" name="ContactPhone" value='<%#Eval("ContactPhone") %>' />
                                            <input type="hidden" name="ContactFax" value='<%#Eval("ContactFax") %>' />
                                            <a href="javascript:void(0);" class="xuanyong Offers"></a>
                                        </td>
                                        <td height="50" align="center">
                                            <textarea name="GuideNotes" class="bk pand4 inputtext" style="height: 55px; width: 160px;"><%#Eval("GuideNotes") %></textarea>
                                        </td>
                                        <td height="50" align="center">
                                            <input valid="isNumber" onafterpaste="this.value=this.value.replace(/\D/g,'')" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                                errmsg="必须是数字!" value='<%#Eval("Num") %>' name="Count" type="text" class="inputtext formsize20" />
                                        </td>
                                        <td height="50" align="center">
                                            <input valid="isNumber" errmsg="必须是数字!" name="PlanCost" type="text" class="inputtext formsize50 plancost"
                                                value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("Confirmation"))) %>" />
                                        </td>
                                        <td height="50" align="center">
                                            <select name="PayType" class="inputselect">
                                                <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)Eval("PaymentType")).ToString(), false)%>
                                            </select>
                                        </td>
                                        <td height="50" align="center">
                                            <textarea name="remarkSource" class="bk pand4 inputtext" style="height: 55px; width: 160px;"><%#Eval("Remarks")%></textarea>
                                        </td>
                                        <td height="50" align="center" class="i_gysap_cz_1">
                                            <a href="javascript:void(0)" class="addsourceplan">
                                                <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                                    class="delsourceplan">
                                                    <img src="/images/delimg.gif" width="48" height="20" /></a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font color="#FF0000">*</font>合计收入：
                </td>
                <td align="left" style="padding: 5px;" colspan="2">
                    <asp:TextBox ID="txtTotalIn" runat="server" class="inputtext formsize80" Text="0"
                        ReadOnly="true" BackColor="#dadada"></asp:TextBox>
                </td>
                <td width="10%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    操作状态：
                </td>
                <td width="20%" bgcolor="#e0e9ef" colspan="2">
                    <asp:DropDownList ID="ddlopeaterStatus" runat="server" class="inputselect">
                        <asp:ListItem Value="0">操作中</asp:ListItem>
                        <asp:ListItem Value="2">已落实</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <div class="alertbox-btn">
            <asp:Literal runat="server" ID="ltrCaoZuoTiShi"></asp:Literal>&nbsp;&nbsp;
            <asp:PlaceHolder runat="server" ID="phCaoZuo"><a id="btnSave" href="javascript:void(0);">
                <s class="baochun"></s>保 存</a> <a style="text-indent: 0px;" hidefocus="true" href="javascript:void(0);"
                    id="SubmitPlan">提交财务</a> </asp:PlaceHolder>
        </div>
    </div>
    <input type="hidden" runat="server" id="status" value="" />
    <asp:HiddenField ID="hideDeptID" runat="server" />
    <asp:HiddenField ID="hideDeptName" runat="server" />
    <asp:HiddenField ID="hidePlanerDeptID" runat="server" />
    <asp:HiddenField ID="hidePlanerDeptName" runat="server" />
    </form>
</body>
</html>

<script type="text/javascript">

    var SingleEditPage = {
        ajaxurl: "/Sales/SingleServerEdit.aspx",
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
        CheckForm: function() {
            if (!CustomListControl.CheckInfo()) {
                return false;
            }
            $(".showrequire").find("select[name='ServerType']").each(function() { var _$obj = $(this); if (!_$obj.attr("valid")) _$obj.attr("valid", "required").attr("errmsg", "请选择服务类别"); });
            var form = $("#btnSave").closest("form").get(0);
            $("#Agreement").val($("#lbFiles").find("a").attr("href"));
            return ValiDatorForm.validator(form, "parent");
        },
        SourceID: '<%=Request.QueryString["id"] %>',
        PageInit: function() {
            $(".showlist").find("select[name='Certificates']").each(function() {
                if ($(this).attr("data-value")) {
                    $(this).attr("value", $(this).attr("data-value"));
                }
                CustomListControl.ProveTypeOnChange(this);
            })
            $("#Loading").hide();
            $(".showlist").show();
            $("#link3").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/ImportSource/ImportPage.aspx?callback=CallBack&type=5&box=Customlist",
                    title: "导入客户",
                    modal: true,
                    width: "900px",
                    height: "500px"
                });
                return false;
            })

            var _tourStatus = $("#status").val();
            if (_tourStatus == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置 %>") {
                $("#btnSave").remove();
            }

            if (SingleEditPage.SourceID == "") {
                $("#SubmitPlan").hide();
            }

            if (_tourStatus == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.财务待审 %>"
                || _tourStatus == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.单团核算 %>"
                || _tourStatus == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 %>"
                || _tourStatus == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消 %>") {
                $("#btnSave").remove();
                $("#SubmitPlan").remove();
            }

            $("#tablelist").find("select[name='Certificates']").each(function() {
                CustomListControl.ProveTypeOnChange(this);
            })
            $(".Offers").live("click", function() {
                $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
                var url = "/CommonPage/Supplier.aspx?aid=" + $(this).attr("id") + "&";
                var hideObj = $(this).parent().find("input[name='ShowID']");
                var showObj = $(this).parent().find("input[name='SourceName']");
                if (!hideObj.attr("id")) {
                    hideObj.attr("id", "hideID_" + parseInt(Math.random() * 10000000));
                }
                if (!showObj.attr("id")) {
                    showObj.attr("id", "ShowID_" + parseInt(Math.random() * 10000000));
                }
                var sourceID = $(this).parent().prev().find("select").val();
                url += $.param({ Sourcetype: sourceID, hideID: hideObj.attr("id"), callBack: "CallBackSource", ShowID: showObj.attr("id") })
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "选择供应商",
                    modal: true,
                    width: "880",
                    height: "350"
                });
            });
            //    客人详情----------------start
            $(".addlist").live("click", function() {
                var tr = "<tr class='showlist'>" + $("#Templist").html() + "</tr>"+"<tr class='showlist'>" + $("#Templist1").html() + "</tr>";
                $(this).closest("tr").next().after(tr);
            })
            $("select[name='Sourcetype']").change(function() {
                $(this).closest("tr").find("input[name='ShowID']").val("");
                $(this).closest("tr").find("input[name='SourceName']").val("");
                $(this).closest("tr").find("input[name='ShowID']").attr("id", "");
                $(this).closest("tr").find("input[name='SourceName']").attr("id", "");
            })
        	$(".price").live("keyup",function () {
        		if ($(this).attr("name")=="Price") {
        			return;
        		}
        		var tr = $(this).closest("tr");
        		var dj = tableToolbar.getFloat($(tr).find("input[name='DanJia']").eq(0).val());
        		var sl = tableToolbar.getFloat($(tr).find("input[name='ShuLiang']").eq(0).val());
        		$(tr).find("input[name='Price']").eq(0).val(tableToolbar.getFloat(tableToolbar.calculate(dj, sl, "*")));
        		$("input[name='Price']").trigger("change");
        	})
            //showrequire
            $("input[name='Price']").live("change", function() {
                var price = 0;
                $("input[name='Price']").each(function() {
                    price = tableToolbar.calculate(tableToolbar.getFloat($(this).val()),price,"+");
                })
                $("#<%=txtTotalIn.ClientID %>").val(price);
            })
            $(".plancost").live("keyup", function() {

                var price = 0;
                $(".plancost").each(function() {
                    price += parseFloat($(this).val() == "" ? 0 : $(this).val());
                })
            })


            $(".dellist").live("click", function() {
                if ($("#Customlist .showlist").length == 2) {
                    tableToolbar._showMsg("必须保留一行!");
                    return;
                }
                if ($("#Customlist .showlist").length > 2) {
                    $(this).parent().parent().closest("tr").next().remove();
                    $(this).parent().parent().closest("tr").remove();
                }
                else {
                    tableToolbar._showMsg("请选择要删除的行!");
                }
            })
            //    客人详情------------end

            //        业务内容-----start
            $(".addrequire").live("click", function() {
                var tr = "<tr class='showrequire'>" + $("#Temprequire").html() + "</tr>";
                $(this).closest("tr").after(tr);
            })

            $(".delrequire").live("click", function() {
                if ($("#Customrequire .showrequire").length == 1) {
                    tableToolbar._showMsg("必须保留一行!");
                    return;
                }
                if ($("#Customrequire .showrequire").length > 1) {
                    $(this).parent().parent().closest("tr").remove();
                    var price = 0;
                    $("input[name='Price']").each(function() {
                        price = tableToolbar.calculate(tableToolbar.getFloat($(this).val()),price,"+");
                    })
                    $("#<%=txtTotalIn.ClientID %>").val(price);
                }
                else {
                    tableToolbar._showMsg("请选择要删除的行!");
                }
            })
            //        业务内容----------end
            //供应商安排-------------------start
            $(".addsourceplan").live("click", function() {
                var tr = "<tr class='showsourceplan'>" + $("#Tempsourceplan").html() + "</tr>";
                $(this).closest("tr").after(tr);
            })

            $(".delsourceplan").live("click", function() {
                if ($("#sourceplan .showsourceplan").length == 1) {
                    tableToolbar._showMsg("必须保留一行!");
                    return;
                }
                if ($("#sourceplan .showsourceplan").length > 1) {
                    $(this).parent().parent().closest("tr").remove();
                    var price = 0;
                    $(".plancost").each(function() {
                        price += parseFloat($(this).val() == "" ? 0 : $(this).val());
                    })
                }
                else {
                    tableToolbar._showMsg("请选择要删除的行!");
                }
            })
            $("#<%=ddlopeaterStatus.ClientID %>").change(function() {
                $("#<%=status.ClientID %>").val($(this).val());
            })
            SingleEditPage.BindBtn();
        },
        GoAjax: function(url, type) {
            if (type == "save") {
                $("#btnSave").html("提交中...");
                $("#btnSave").css("background-position", "0 -56px");
                $("#btnSave").unbind("click");
            }
            if (type == "submit") {
                $("#SubmitPlan").html("提交中...");
                $("#SubmitPlan").css("background-position", "0 -56px");
                $("#SubmitPlan").unbind("click");
            }
            $.newAjax({
                type: "post",
                cache: false,
                url: url,
                dataType: "json",
                data: $("#btnSave").closest("form").serialize(),
                success: function(ret) {
                    if (ret.result == "1") {
                        tableToolbar._showMsg(ret.msg, function() { parent.location.href = parent.location.href; });
                    }
                    else {
                        tableToolbar._showMsg(ret.msg, function() { SingleEditPage.BindBtn() });

                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { SingleEditPage.BindBtn() });
                }
            });
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        BindBtn: function() {
            $("#btnSave").click(function() {
                if ($("#status").val() == "9") {
                    tableToolbar._showMsg("待终审数据无法修改!");
                    return false;
                }
                if (!SingleEditPage.CheckForm()) {
                    return false;
                }
                SingleEditPage.GoAjax(SingleEditPage.ajaxurl + "?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.单项业务_单项业务 %>&id=" + SingleEditPage.SourceID, "save");
                return false;
            })
            $("#SubmitPlan").click(function() {
                if ($("#<%=ddlopeaterStatus.ClientID %>").val() != "2") {
                    tableToolbar._showMsg("此计调未落实!");
                    return false;
                }
                else {
                    if (!SingleEditPage.CheckForm()) {
                        return false;
                    }
                    SingleEditPage.GoAjax(SingleEditPage.ajaxurl + "?doType=update&type=save&submitplan=submit&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.单项业务_单项业务 %>&id=" + SingleEditPage.SourceID, "submit");
                    return false;
                }
            })
            $("#btnSave").html("<s class='baochun'></s>保存");
            $("#btnSave").attr("class", "");
            $("#SubmitPlan").html("提交财务");
            $("#SubmitPlan").attr("class", "");
        },
        //服务类别onchange事件，初始化相关服务模板
        initFuWuLeiBie: function(obj) {
            var _$obj = $(obj);
            var _v = _$obj.val();
            var _$tr = _$obj.closest("tr");
            var _$juTiYaoQiu = _$tr.find("textarea[name='ServiceStandard']");

            var _txt = {
                't<%=(int)EyouSoft.Model.EnumType.ComStructure.ContainProjectType.机票 %>': "机票、火车票、汽车票\n出发地：\t目的地：\n出发日期：\t返回日期：\n去程航班号：\t回程航班号：\n价格：\t元/张，共\t张\n合计金额：\t元"
                , 't<%=(int)EyouSoft.Model.EnumType.ComStructure.ContainProjectType.用车 %>': "车辆租用起始时间：\t年\t月\t日\t时\n停止时间：\t年\t月\t日\t时\n车型：\t数量：\n简要行程：\n合计金额：\t元"
                , 't<%=(int)EyouSoft.Model.EnumType.ComStructure.ContainProjectType.酒店 %>': "酒店所在城市：\t标准：\t\n数量：\t间\n入住时间：\t年\t月\t日\n退房时间：\t年\t月\t日\n共\t晚\n客房单价：\t元/间，合计金额：\t元"
                , 't<%=(int)EyouSoft.Model.EnumType.ComStructure.ContainProjectType.餐 %>': ""
                , 't<%=(int)EyouSoft.Model.EnumType.ComStructure.ContainProjectType.景点 %>': ""
                , 't<%=(int)EyouSoft.Model.EnumType.ComStructure.ContainProjectType.其它 %>': ""
            };

//            _$juTiYaoQiu.val(_txt["t" + _v]);
        },
        initJiDiaoAnPaiPrivs: function() {
            var _privs = '<%=Privs_JiDiaoAnPai?"1":"0" %>';
            if (_privs == "1") return;

            $(".i_gysap_cz_1").html("<span style='color:#666'>暂无权限</span>");
            $(".i_gysap").find("input[type='text']").attr("readonly", "readonly").css({ "background-color": "#dadada" });
            $(".i_gysap").find(".xuanyong").hide();
            $(".i_gysap").find("textarea").attr("readonly", "readonly").css({ "background-color": "#dadada" });
            $(".i_gysap").find("select").css({ "background-color": "#dadada" }).find("option").each(function() { if ($(this).attr("selected")) { return; } $(this).remove(); });
        }
    }
    function delFileByItemid(obj) {
        tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
            $(obj).siblings("#lbFiles").hide()
            $(obj).hide();
            $("#Agreement").val("");
        });
    }
    //回调函数 给供应商赋值(hideid,showid)
    function CallBackSource(obj) {
        if (obj) {
            $("#" + obj.aid).closest("td").find("input[name='ContactName']").val(obj.contactname);
            $("#" + obj.aid).closest("td").find("input[name='ContactPhone']").val(obj.contacttel);
            $("#" + obj.aid).closest("td").find("input[name='ContactFax']").val(obj.contactfax);
            $("#" + obj.showid).val(obj.name);
            $("#" + obj.hideid).val(obj.id);
            $("#hideID").val(obj.id);
            $("#txtShowID").val(obj.name);
            $("#flag").val(obj.flag);
        }
    }
    function sellCallBack(data) {
        $("#<%=SellsSelect1.SellsIDClient%>").val(data.value);
        $("#<%=SellsSelect1.SellsNameClient%>").val(data.text);
        $("#<%=hideDeptID.ClientID%>").val(data.deptID);
        $("#<%=hideDeptName.ClientID%>").val(data.deptName);
    }
    function planerCallBack(data) {
        $("#<%=SellsSelect2.SellsIDClient%>").val(data.value);
        $("#<%=SellsSelect2.SellsNameClient%>").val(data.text);
        $("#<%=hidePlanerDeptID.ClientID%>").val(data.deptID);
        $("#<%=hidePlanerDeptName.ClientID%>").val(data.deptName);
    }

    function CallBackFun(data) {
        if (data) {
            $("#<%=txtContactName.ClientID %>").val(data.CustomerUnitContactName);
            $("#<%=txtContactTel.ClientID %>").val(data.CustomerUnitContactPhone);
            $("#<%=hdContactdepartid.ClientID %>").val(data.CustomerUnitContactId);
        }
    }
    function CallBack() {
        $(".showlist").each(function() {
            var msg = "";
            $(this).each(function() {
                if ($.trim($(this).find("input[type='text']").val()) != "") {
                    msg = "noEmpty";
                }
            })
            if (msg == "") {
                $(this).remove();
            }
        })
    }

    var CustomListControl = {
        CheckInfo: function() {
            var msgcar = this.isCheckingCar("True", "CustomName", "ContactTel", "SourceName");
            if (msgcar.isYescar) {
                return true;
            } else {
                tableToolbar._showMsg(msgcar.msgcar);
                return false;
            }
        }
        ,
        isCheckingCar: function(ischecking, CustomName, ContactTel, SourceName) {
            var isYescar = false;
            var msgcar = "";
            var msgscar = "";
            if (ischecking.toString() == "True") {
                msgcar = checkingCar(CustomName, ContactTel, SourceName);
                if (msgcar.length > 0) {
                    isYescar = false;
                }
                else {
                    isYescar = true;
                }
                msgscar = msgcar;
            }
            else {
                isYescar = true;
            }
            msgcar = { isYescar: isYescar, msgcar: msgscar }
            return msgcar;
        },
        ProveTypeOnChange: function(o) {
//            var _self = $(o);
//            if (_self.val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.CardType.身份证%>") {
//                _self.closest("tr").find("input[name='CertificatesNum']").attr("valid", "isIdCard").attr("errmsg", "请输入正确的身份证号码!");

//            } else {
//                _self.closest("tr").find("input[name='CertificatesNum']").removeAttr("valid", "isIdCard").removeAttr("errmsg", "请输入正确的身份证号码!");
//            }
        }
    }


    //信息验证
    function checkingCar(CustomName, ContactTel, SourceName) {
        var msgcar = "";
        //验证客户名称不能为空
        $("[name=" + CustomName + "]").each(function() {
            if ($(this).parent().parent().hasClass("showlist") && $(this).val() == "") {
                msgcar += "*客人姓名不能为空! <br />";
                $(this).focus();
                return false;
            }
        })

        //验证供应商名称不能为空
        $("[name=" + SourceName + "]").each(function() {
            if ($(this).closest("tr").hasClass("showsourceplan") && $(this).val() == "" && $(this).closest("tr").find("input[name='PlanCost']").val() != "") {
                msgcar += "*供应商不能为空! <br />";
                $(this).focus();
                return false;
            }
        })
        //验证结算费用不能为空
        $("[name='PlanCost']").each(function() {
            if ($(this).closest("tr").hasClass("showsourceplan") && $(this).val() == "" && $(this).closest("tr").find("input[name='SourceName']").val() != "") {
                msgcar += "*结算费用不能为空! <br />";
                $(this).focus();
                return false;
            }
        })
        //验证电话格式
//        $("[name=" + ContactTel + "]").each(function() {
//            var isPhone = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$|^(13|15|18|14)\d{9}$/;
//            if ($(this).closest("tr").hasClass("showlist") && $(this).val() != "" && !(isPhone.exec($(this).val()))) {
//                msgcar += "*联系电话格式错误! <br />";
//                $(this).focus();
//                return false;
//            }
//        })
        return msgcar;
    }

    $(function() {
        SingleEditPage.PageInit();

        SingleEditPage.initJiDiaoAnPaiPrivs();
    })
</script>

