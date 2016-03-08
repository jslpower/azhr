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
				     <li><i>�û�����</i>
			         <input type="text" class="inputtext formsize140" style="width: 165px;" name="u" id="u" tabindex="1"></li>
					 <li><i>��<span style="padding-left:14px;"></span>�룺</i>
				   <input type="password" class="inputtext formsize140" style="width: 165px;" name="p" id="p" tabindex="2"></li>
					 <li><i>��֤�룺</i>
				   <input onfocus="this.select();" class="inputtext formsize100" tabindex="3" type="text" name="vc" id="vc" />
				   <img style="cursor: pointer; margin-top: 4px" title="���������֤��" onclick="this.src='/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&id='+Math.random();return false;" align="middle" width="60" height="20" id="imgCheckCode" src="/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&t=<%=DateTime.Now.ToString("HHmmssffff") %>" /></li>
              </ul>
              <div class=""><a href="javascript:void(0);" id="linkLogin" tabindex="4" class="loginbtn"><img src="images/loginbtn.png" /></a><a href="http://jjtravelly.gocn.cn"><img src="images/fxs-btn.png"></a></div>
          </div>
        </div>
       
       <div class="login_b">ϵͳʹ�û�����1024*768���Ϸֱ��ʡ�IE 7�����ϰ汾�����<br />��Ȩ���У����޻��������� ����֧�֣�������ŵ�Ƽ����޹�˾</div> 
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
        		tableToolbar._showMsg("�������û���!");
        		$("#u").focus();
        		return;
        	}
        	if (p == "") {
        		tableToolbar._showMsg("����������");
        		return;
        	}
        	if (ckcode == "" || ckcode != getCheckCode()) {
        		tableToolbar._showMsg("��������ȷ����֤��");
        		return;
        	}

        	//��ʾ��¼״̬
        	tableToolbar._showMsg("���ڵ�¼��....");
        	//��ֹ�ظ���½
        	$("#linkLogin").unbind().css("cursor", "default");

        	blogin5($("form").get(0), function(h) { //login success callback
        		tableToolbar._showMsg("��¼�ɹ���������ϵͳ....");
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