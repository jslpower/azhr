<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EyouSoft.WebFX.Login" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title><asp:Localize runat="server" Text="<%$Resources:PageResource1.Title%>" /></title>
        <link href="Css/fx_style.css" rel="stylesheet" type="text/css" />
        <!--[if IE 6]>
<script type="text/javascript" src="js/PNG.js" ></script>
<script type="text/javascript">
DD_belatedPNG.fix('div,img,a:hover,ul,li,p');
</script>
<![endif]-->
        <script type="text/javascript" src="/Js/jquery-1.4.1-vsdoc.js"> </script>
        <script src="/Js/jquery.blockUI.js" type="text/javascript"> </script>
        <script src="/Js/table-toolbar.js" type="text/javascript"> </script>
        <script src="/Js/slogin.js" type="text/javascript"> </script>
        <script src="/Js/ValiDatorForm.js" type="text/javascript"> </script>
    </head>

    <body style="background: url(../images/fx-images/login_bodybg.jpg) #00a8e2 repeat-x;">
        <div class="login-wrap">
            <div class="loginbar">
                <div class="login_logo"><img src="/images/fx-images/logo.png" /></div>
                <div class="loginbox fixed">
                    <div class="leftimg"><img src="/images/fx-images/login_l.png" /></div>
                    <div class="right-form">
                    <form>
                        <ul>
                            <li><i><asp:Localize runat="server" ID="YuYan" meta:resourcekey="LitLanguage"></asp:Localize>：</i>
                                <select name="l" id="l" onchange="javascript:form.submit();">
                                    <option value="<%=(int)EyouSoft.Model.EnumType.SysStructure.LngType.中文 %>" <%=Request.QueryString["l"]==((int)EyouSoft.Model.EnumType.SysStructure.LngType.中文).ToString()?"selected":"" %>>中文</option>
                                    <option value="<%=(int)EyouSoft.Model.EnumType.SysStructure.LngType.英文 %>" <%=Request.QueryString["l"]==((int)EyouSoft.Model.EnumType.SysStructure.LngType.英文).ToString()?"selected":"" %>>English</option>
                                    <%--<option value="<%=(int)EyouSoft.Model.EnumType.SysStructure.LngType.泰文 %>" <%=Request.QueryString["l"]==((int)EyouSoft.Model.EnumType.SysStructure.LngType.泰文).ToString()?"selected":"" %>>ภาษาไทย</option>--%>
                                </select>
                            </li>
                            <li><i><asp:Localize runat="server" ID="YongHuMing" meta:resourcekey="Label1Resource1"></asp:Localize>：</i>
                                <input type="text" id="u" style="width: 165px;"/></li>
                            <li><i><asp:Localize runat="server" ID="MiMa" meta:resourcekey="LitPWD"></asp:Localize>：</i>
                                <input type="password" id="p" style="width: 165px;"/></li>
                        </ul>
                     </form>
                        <div class="loginbtn"><a href="javascript:void(0);" id="linkLogin"><img src="/images/fx-images/loginbtn.png" /></a></div>
                        <%--<div class="login_txt"><asp:Localize runat="server" ID="GongGongZhangHao" meta:resourcekey="Public"></asp:Localize>：tongye <asp:Localize runat="server" ID="GongGongMiMa" meta:resourcekey="LitPWD"></asp:Localize>：123456</div>--%>
                    </div>
                </div>
                <div class="login_b"><asp:Localize runat="server" ID="XiTong" meta:resourcekey="System"></asp:Localize><br /><asp:Localize runat="server" ID="BanQuan" meta:resourcekey="Reserved"></asp:Localize></div> 
            </div>
            <div class="login-foot"></div>
        </div>
    </body>

    <script type="text/javascript">
var Page = {
	FormCheck: function(obj) { /*提交数据验证*/
		this.Form = $(obj).get(0);
		FV_onBlur.initValid(this.Form);
		return ValiDatorForm.validator(this.Form, "parent");
	},
	Login: function() {
		var u = $.trim($("#u").val()), p = $.trim($("#p").val()), ckcode = $.trim($("#vc").val());
		if (u == "") {
			tableToolbar._showMsg('<%=GetLocalResourceObject("Msg1.errmsg") %>');
			$("#u").focus();
			return;
		}
		else if (p == "") {
			tableToolbar._showMsg('<%=GetLocalResourceObject("Msg2.errmsg") %>');
			$("#p").focus();
			return;
		}
//		else if (ckcode == "") {
//			tableToolbar._showMsg("请输入验证码");
//			return;
//		}
//		else if (ckcode != Page.GetCodeByCookie()) {
//			tableToolbar._showMsg("请输入正确的验证码");
//			return;
//		}
		if (Page.FormCheck($("form"))) {
			tableToolbar._showMsg('<%=GetLocalResourceObject("Login.text") %>');
			//防止重复登陆
			$("#linkLogin").unbind().css("cursor", "default");
			blogin5($("form").get(0), function(status) {
				var url = "<%=this.Request.QueryString["returnurl"]%>";
				if (url == "") {
					if (status == "1") {
						url = "AcceptPlan.aspx?LgType=" + $("#l").val()+"&sl=0";
					}
				}
				window.location.href = url;
			}, function(error) {
				tableToolbar._showMsg('<%=GetLocalResourceObject("Msg5.errmsg") %>');
				$("#linkLogin").css("cursor", "pointer").click(function() {
					Page.Login();
					return false;
				});
			});
		}
	},
    //Cookie 中获取当前验证码
	GetCodeByCookie: function() {
		var c = document.cookie;
		var dic = c.split(';');
		for (var i = 0; i < dic.length; i++) {
			if ($.trim(dic[i].split('=')[0]) == "SYS_VC") {
				return $.trim(dic[i].split('=')[1]);
			}
		}
		return "";
	}
};

$(function() {
	$("#u").focus();
	$("#linkLogin").click(function() {
		Page.Login();
		return false;
	});
	$("#u,#p,#vc").keypress(function(e) {
		if (e.keyCode == 13) {
			Page.Login();
			return false;
		}
	});
});
</script>
 
</html>