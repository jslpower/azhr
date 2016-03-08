<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.PrivsStructure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="/Css/style.css" rel="stylesheet" type="text/css" />
        <!--[if IE 6]>
            <script type="text/javascript" src="js/PNG.js" > </script>
            <script type="text/javascript">
    DD_belatedPNG.fix('*,div,img,a:hover,ul,li,p');
    </script>
        <![endif]-->
        <script src="/Js/jquery-1.4.4.js" type="text/javascript"> </script>
        <script src="js/foucs.js" type=text/javascript> </script>
    </head>
    <body style="background:url(images/login_bodybg.jpg) #00a8e2 repeat-x;">
   <form id="form1" runat="server">
     <div class="login-wrap">
     <div class="loginbar">
     
        <div class="login_logo"><asp:Literal ID="litLogo" runat="server"></asp:Literal></div>
        
        <div class="loginbox fixed">
          <div class="leftimg"><img src="images/login_l.png" /></div>
          <div class="right-form">
              <ul>
				     <li><i>用户名：</i>
			         <input type="text" class="inputtext formsize140" style="width: 165px;" name="u" id="u" tabindex="1"></li>
					 <li><i>密<span style="padding-left:14px;"></span>码：</i>
				   <input type="password" class="inputtext formsize140" style="width: 165px;" name="p" id="p" tabindex="2"></li>
					 <li><i>验证码：</i>
				   <input onfocus="this.select();" class="inputtext formsize100" tabindex="3" type="text" name="vc" id="vc" />
				   <img style="cursor: pointer; margin-top: 4px" title="点击更换验证码" onclick="this.src='/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&id='+Math.random();return false;" align="middle" width="60" height="20" id="imgCheckCode" src="/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&t=<%=DateTime.Now.ToString("HHmmssffff") %>" /></li>
              </ul>
              <div class=""><a href="javascript:void(0);" id="linkLogin" tabindex="4" class="loginbtn"><img src="images/loginbtn.png" /></a><a href="http://jjtravelly.gocn.cn"><img src="images/fxs-btn.png"></a></div>
          </div>
        </div>
       
       <div class="login_b">系统使用环境：1024*768以上分辨率、IE 7及以上版本浏览器<br />版权所有：澳洲华人旅游网 技术支持：杭州易诺科技有限公司</div> 
     </div>
     
     <div class="login-foot"></div>
     
  </div>

        </form>

        <script src="/Js/slogin.js" type="text/javascript"> </script>

        <script src="/Js/jquery.blockUI.js" type="text/javascript"> </script>

        <script src="/Js/table-toolbar.js" type="text/javascript"> </script>

        <script type="text/javascript">
        function getCheckCode() {
        	var c = document.cookie, ckcode = "", tenName = "";
        	for (var i = 0; i < c.split(";").length; i++) {
        		tenName = c.split(";")[i].split("=")[0];
        		ckcode = c.split(";")[i].split("=")[1];
        		if ($.trim(tenName) == "SYS_VC") {
        			break;
        		} else {
        			ckcode = "";
        		}
        	}
        	return $.trim(ckcode);
        }

        ;


        function reset() {
        	$("#u").val("");
        	$("#p").val("");
        	$("#vc").val("");
        }

        function login() {
        	var u = $.trim($("#u").val()), p = $.trim($("#p").val()), ckcode = $.trim($("#vc").val());
        	if (u == "") {
        		tableToolbar._showMsg("请输入用户名!");
        		$("#u").focus();
        		return;
        	}
        	if (p == "") {
        		tableToolbar._showMsg("请输入密码");
        		return;
        	}
        	if (ckcode == "" || ckcode != getCheckCode()) {
        		tableToolbar._showMsg("请输入正确的验证码");
        		return;
        	}

        	//显示登录状态
        	tableToolbar._showMsg("正在登录中....");
        	//防止重复登陆
        	$("#linkLogin").unbind().css("cursor", "default");

        	blogin5($("form").get(0), function(h) { //login success callback
        		tableToolbar._showMsg("登录成功，正进入系统....");
        		var s = '<%=this.Request.QueryString["returnurl"]%>';
        		if (s == "") {
        			if (h == "1") {
        				s = "/GroupEnd/Distribution/AcceptPlan.aspx?sl=<%=(int)Menu2.None%>";
        			} else if (h == "2") {
        				s = "/Default.aspx?sl=<%=(int)Menu2.None%>";
        			} else if (h == "3") {
        				s = "/GroupEnd/Suppliers/ProductList.aspx?sl=<%=(int)Menu2.None%>";
        			}
        		}
        		window.location.href = s;
        	}, function(m) { //login error callback
        		tableToolbar._showMsg(m);
        		$("#linkLogin").click(function() {
        			login();
        			return false;
        		}).css("cursor", "pointer");
        	});
        }

        $(function() {
        	$("#u").focus();
        	$("#linkLogin").click(function() {
        		login();
        		return false;
        	});
        	$("#u,#p,#vc").keypress(function(e) {
        		if (e.keyCode == 13) {
        			login();
        			return false;
        		}
        	});
        });

    </script>

    </body>
</html>