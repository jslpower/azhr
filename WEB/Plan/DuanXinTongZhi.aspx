<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuanXinTongZhi.aspx.cs" Inherits="EyouSoft.Web.Plan.DuanXinTongZhi" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="PlanConfigMenu" TagPrefix="uc1" Src="/UserControl/PlanConfigMenu.ascx" %>
<%@ Register tagName="JieShouRen" tagPrefix="uc2" src="~/UserControl/SellsSelect.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="hr_10">
        </div>
        <div class="jd-mainbox fixed">
            <uc1:PlanConfigMenu id="PlanConfigMenu1" runat="server" />
            <form id="form" runat="server">
            <div class="jdcz-main">
				<div class="addContent-box">
                  <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                    <tr>
                      <td width="14%" class="addtableT">短信接收人：</td>
                      <td class="kuang2"><uc2:JieShouRen ID="JieShouRen" runat="server" SetTitle="短信接受人" SMode="true" IsMust="true" /></td>
                    </tr>
                    <tr>
                      <td class="addtableT">短信内容：</td>
                      <td class="kuang2"><textarea id="txtSendContent" name="txtSendContent" class="inputtext formsize600"
                            style="height: 80px;" onkeyup="SendSms.fontNum(this);" valid="required" errmsg="请填写短信内容！"></textarea></td>
                    </tr>
                    </table>
   	             </div>
			   <div class="hr_10"></div>
               
               <div class="mainbox cunline fixed">
                 <ul>
                                <asp:PlaceHolder ID="PSendBtn" runat="server">
                                    <li class="cun-cy"><a href="javascript:" id="btnSave">发 送</a></li></asp:PlaceHolder>
                   <li class="quxiao-cy"><a href="javascript:void(0);" onclick="SendSms.Cancel();">取消</a></li>
                 </ul>
               </div>

            </div>
            </form>
            <div class="hr_10">
            </div>
        </div>
    </div>
<script type="text/javascript">
        var SendSms = {
        	FormCheck: function(obj) { /*提交数据验证*/
        		this.Form = $(obj).get(0);
        		FV_onBlur.initValid(this.Form);
        		return ValiDatorForm.validator(this.Form, "parent");
        	},
        	Cancel:function () {
        		$("input,textarea").val("");
        	},
        	BtnBind: function() {
        		$("#btnSave").text("发送");
        		$("#btnSave").bind("click");
        		$("#btnSave").css("background-position", "0 0");
        		$("#btnSave").click(function() {
        			SendSms.sendMess();
        		});
        	},
        	UnBtnBind: function() {
        		$("#btnSave").css("background-position", "0 -62px");
        		$("#btnSave").unbind("click");
        		$("#btnSave").text("正在发送中...");
        	},
            //发送短信
        	sendMess: function() {
        		if (SendSms.FormCheck($("form"))) {
        			SendSms.UnBtnBind();
        			$.newAjax({
        					type: "post",
        					dataType: "json",
        					url: "/Plan/DuanXinTongZhi.aspx?dotype=save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",
        					data: $("#<%=form.ClientID%>").serialize(),
        					cache: false,
        					dataType: "json",
        					success: function(ret) {
        						//ajax回发提示
        						if (ret.result == "1") {
        							tableToolbar._showMsg(ret.msg);
        							SendSms.BtnBind();
        						} else {
        							SendSms.BtnBind();
        							tableToolbar._showMsg(ret.msg);
        						}
        					},
        					error: function() {
        						SendSms.BtnBind();
        						tableToolbar._showMsg("服务器忙，请稍后再试！");
        					}
        				});
        		}
        	}
        };
        $(function() {
            $("#btnSave").click(function() {
                SendSms.sendMess();
            });
        });
    </script></asp:Content>
