<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="XiTongXinXi.aspx.cs" Inherits="Web.Sys.XiTongXinXi" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr>
                        <th align="center" class="th-line" colspan="3">
                            系统信息
                        </th>
                    </tr>
                    <tr>
                        <td width="38%" bgcolor="#e0e9ef" align="right">
                            公司名称：
                        </td>
                        <td width="62%" align="left" colspan="2">
                            <input type="text" id="txtCompanyName" class="inputtext formsize180" name="txtCompanyName"
                                valid="required" errmsg="请填写公司名称!" runat="server">
                            <span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            外部LOGO上传(大小:458*80px)：
                        </td>
                        <td align="left" colspan="2">
                            <uc1:UploadControl ID="UploadOutLogo" runat="server" />
                            <asp:Label ID="lblUploadOutLogo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            系统内部LOGO上传(大小:420*70px)：
                        </td>
                        <td align="left" colspan="2">
                            <uc1:UploadControl ID="UploadInnerLogo" runat="server" />
                            <asp:Label ID="lblUploadInnerLogo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            分销商端LOGO上传(大小:371*64px)：
                        </td>
                        <td align="left" colspan="2">
                            <uc1:UploadControl ID="ucFXSLogo" runat="server" />
                            <asp:Label ID="lblFXSLogo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            旅行社类别：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtHotelType" class="inputtext formsize180" name="txtHotelType"
                                errmsg="请填写旅行社类别!" valid="required" runat="server">
                            <span class="fontred">*</span>
                        </td>
                    </tr>
                    <%--<tr>
                        <td bgcolor="#e0e9ef" align="right">
                            公司英文名称：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtEnCompanyName" class="inputtext formsize180" name="txtEnCompanyName"
                                runat="server" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            许可证号：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtXuKeCard" class="inputtext formsize180" name="txtXuKeCard"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            公司负责人：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtCompanyManager" class="inputtext formsize180" name="txtCompanyManager"
                                errmsg="请填写公司负责人!" valid="required" runat="server" />
                            <span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            电话：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtPhone" class="inputtext formsize180" name="txtPhone" errmsg="请填写电话!|请填写有效的电话格式!"
                                valid="required|isPhone" runat="server" />
                            <span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            手机：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtMobile" class="inputtext formsize180" name="txtMobile"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            传真：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtFax" class="inputtext formsize180" name="txtFax" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            地址：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtAddress" class="inputtext formsize180" name="txtAddress"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            邮编：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtZip" class="inputtext formsize180" name="txtZip" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            公司网站：
                        </td>
                        <td align="left" colspan="2">
                            <input type="text" id="txtSite" class="inputtext formsize250" name="txtSite" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            公司账号：
                        </td>
                        <td align="left" colspan="2">
                            <table width="600" border="0" style="margin-top: 5px; margin-bottom: 5px;" class="autoAdd">
                                <tbody>
                                    <tr>
                                        <td width="118" align="center">
                                            开户行
                                        </td>
                                        <td width="69" align="center">
                                            户名
                                        </td>
                                        <td width="168" align="center">
                                            账号
                                        </td>
                                        <td width="84" align="center">
                                            操作
                                        </td>
                                    </tr>
                                    <cc1:CustomRepeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="tempRow">
                                                <td align="center">
                                                    <input name="hidAccountId" type="hidden" value="<%#Eval("AccountId")%>" />
                                                    <input type="text" value="<%#Eval("BankName") %>" class="inputtext formsize180" name="BankName"
                                                        valid="required" errmsg="请输入开户行！" />
                                                </td>
                                                <td align="center">
                                                    <input type="text" value="<%#Eval("AccountName") %>" class="inputtext formsize180"
                                                        name="AccountName" valid="required" errmsg="请输入户名！" />
                                                </td>
                                                <td align="center">
                                                    <input type="text" value="<%#Eval("BankNo") %>" id="textfield42252" class="inputtext formsize180"
                                                        name="BankNo" valid="required" errmsg="请输入账号！">
                                                </td>
                                                <td align="center">
                                                    <a class="addbtn" href="javascript:void(0)">
                                                        <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                                            <img width="48" height="20" src="/images/delimg.gif"></a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </cc1:CustomRepeater>
                                    <asp:PlaceHolder ID="phrPanel" runat="server">
                                        <tr class="tempRow">
                                            <td align="center">
                                                <input name="hidAccountId" type="hidden" value="0" />
                                                <input type="text" class="inputtext formsize180" name="BankName" valid="required"
                                                    errmsg="请输入开户行！" />
                                            </td>
                                            <td align="center">
                                                <input type="text" class="inputtext formsize180" name="AccountName" valid="required"
                                                    errmsg="请输入户名！" />
                                            </td>
                                            <td align="center">
                                                <input type="text" id="textfield422522" class="inputtext formsize180" name="BankNo"
                                                    valid="required" errmsg="请输入账号！" />
                                            </td>
                                            <td align="center">
                                                <a class="addbtn" href="javascript:void(0)">
                                                    <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                                        <img width="48" height="20" src="/images/delimg.gif"></a>
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="mainbox cunline fixed">
            <div class="hr_10">
            </div>
            <ul>
                <li class="cun-cy"><a id="btnSave" href="javascript:">保存</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>

    <script type="text/javascript">
    var SystemConfig={
     UnBindBtn:function(){
       $("#btnSave").text("提交中...");
       $("#btnSave").unbind("click");
       $("#btnSave").css("background-position", "0 -62px");
     },
     //按钮绑定事件
      BindBtn: function() {
        $("#btnSave").bind("click");
        $("#btnSave").css("background-position", "0-31px");
        $("#btnSave").text("保存");
         $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                 if(ValiDatorForm.validator(form, "alert")){
                     SystemConfig.Save();
                    return false;
             }
         });
      },
      //删除签证附件
            RemoveFile: function(obj) {
                $(obj).parent().remove();
            },
      //提交表单
            Save: function() {
                SystemConfig.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/XiTongXinXi.aspx?dotype=save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg,function(){window.location.href='/Sys/XiTongXinXi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';});
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                        SystemConfig.BindBtn();
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后再试!");
                        SystemConfig.BindBtn();
                    }
                });
           }
         };
        $(function() {
            FV_onBlur.initValid($("#btnSave").closest("form").get(0));
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                 if(ValiDatorForm.validator(form, "alert")){
                     SystemConfig.Save();
                    return false;
                 }
            });
        })
    </script>

    </form>
</asp:Content>
