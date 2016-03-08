<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YongHu.aspx.cs" Inherits="EyouSoft.WebFX.YongHu" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title>分销商系统设置</title>
<!-- InstanceEndEditable -->
<link href="Css/fx_style.css" rel="stylesheet" type="text/css" />
<link href="Css/boxynew.css" rel="stylesheet" type="text/css" />
<!-- InstanceBeginEditable name="head" --><!-- InstanceEndEditable -->
<script type="text/javascript" src="Js/jquery-1.4.4.js"></script>
<!--[if IE]><script src="Js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->
<script type="text/javascript" src="Js/bt.min.js"></script>
<script type="text/javascript" src="Js/jquery.boxy.js"></script>
<!--[if lte IE 6]><script src="Js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

<script type="text/javascript" src="Js/jquery.blockUI.js"></script>
<script type="text/javascript" src="Js/table-toolbar.js"></script>
<script type="text/javascript" src="Js/moveScroll.js"></script>
<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

<!--[if IE 6]>
<script type="text/javascript" src="Js/PNG.js" ></script>
<script type="text/javascript">
DD_belatedPNG.fix('*,div,img,a:hover,ul,li,p');
</script>
<![endif]-->

</head>

<body style="background:0 none;">
<uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" SystemClass="default xtglicon" />
<div class="list-main">
    <div class="list-maincontent">
    	<div class="linebox-menu">
            <a href="YongHu.aspx?LgType=<%=Request.QueryString["LgType"] %>" class="linebox-menudefault"><span> <%=(String)GetGlobalResourceObject("string", "用户信息")%></span></a><a href="DaYin.aspx?LgType=<%=Request.QueryString["LgType"] %>"><span> <%=(String)GetGlobalResourceObject("string", "打印设置")%></span></a>
       </div>
       <div class="hr_10"></div>
       <div class="listtablebox">
       <form id="frmpass">
       		<table width="100%" border="0" cellpadding="0" cellspacing="0" >
              <tr>
                <td align="right" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "公司名称")%>：</td>
                <td align="left"><input name="input" type="text" class="searchInput size170" disabled="disabled" value="<%=this.SiteUserInfo.CompanyName %>"/></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "用户名")%>：</td>
                <td align="left"><input name="txtUserName" id="txtUserName" type="text" class="searchInput size170" value="<%=this.SiteUserInfo.Username %>" valid="required" errmsg="<%=(String)GetGlobalResourceObject("string", "用户名不能为空")%>"/>
                <span class="fontbsize12">*</span></td>
              </tr>
              <tr>
                <td width="20%" align="right" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "密码")%>：</td>
                <td width="80%" align="left"><input name="txtNewPwd" id="txtNewPwd" type="password" class="searchInput size170" valid="required" errmsg="<%=(String)GetGlobalResourceObject("string", "密码不能为空")%>"/>
                <span class="fontbsize12">*</span></td>
              </tr>
              <tr>
                <td align="right" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "重复密码")%>：</td>
                <td align="left"><input name="txtSurePwd" id="txtSurePwd" type="password" class="searchInput size170" valid="required|equal" eqaulname="txtNewPwd" errmsg="<%=(String)GetGlobalResourceObject("string", "密码不能为空")%>|<%=(String)GetGlobalResourceObject("string", "两次输入的密码不一致")%>"/> <span class="fontbsize12">*</span></td>
              </tr>
              <tr>
                <td colspan="2" align="right">
                <div class="mainbox cunline fixed">
                 <ul>
                   <li class="cun-cy"><a href="javascript:void(0);" id="btnUpdate"><%=(String)GetGlobalResourceObject("string", "保存")%></a></li>
                   <li class="quxiao-cy"><a href="javascript:void(0);" id="btnCancle"><%=(String)GetGlobalResourceObject("string", "取消")%></a></li>
                 </ul>
               </div>
               </td>
              </tr>
            </table>
        </form>
</div>
       <div class="hr_10"></div>
    </div>
  </div>
<script type="text/javascript">
        $(function() {
        	//表单验证初始化
        	FV_onBlur.initValid($("#frmpass").get(0));

        	$("#txtUserName,#txtNewPwd,#txtSurePwd").keypress(function(e) {
        		if (e.keyCode == 13) {
        			PasswordSettings.Update();
        			return false;
        		}
        	});
        	//取消
        	$("#btnCancle").click(function() {
        		PasswordSettings.Cancle();
        	});

        	//提交
        	$("#btnUpdate").click(function() {
        		if (ValiDatorForm.validator($("#frmpass").get(0), "alert")) {
        			PasswordSettings.Update();
        			return false;
        		}
        	});

        });

        var PasswordSettings = {
        	Data: {
        		Type: 'Update'
        	},
        	Cancle: function() {
        		location.reload();
        	},
        	Update: function() {
        		//防止重复提交
        		$("#btnUpdate").html('<%=GetGlobalResourceObject("string","提交中") %>');
        		PasswordSettings.UnBind();
        		$.newAjax({
        				type: "post",
        				url: "YongHu.aspx?" + $.param(PasswordSettings.Data),
        				data: $("#frmpass").serialize(),
        				dataType: "json",
        				success: function(data) {
        					if (data.result == "1") {
        						tableToolbar._showMsg(data.msg, function() {
        							window.location.href = "/logout.aspx";
        						});
        					}
        					else {
        						tableToolbar._showMsg(data.msg);
        						PasswordSettings.Bind();
        					}
        				},
        				error: function() {
        					tableToolbar._showMsg("服务器忙！");
        					PasswordSettings.Bind();
        				}
        			});
        	},
        	Bind: function() {
        		var that = $("#btnUpdate");
        		that.html('<%=GetGlobalResourceObject("string","保存") %>');
        		that.css("cursor", "pointer")
        		that.click(function() {
        			if (ValiDatorForm.validator($("#frmpass").get(0), "alert")) {
        				PasswordSettings.Update();
        				return false;
        			}
        		});

        		$("#btnCancle").click(function() {
        			PasswordSettings.Cancle();
        			return false;
        		});
        	},
        	UnBind: function() {
        		$("#btnUpdate").unbind("click").css("cursor", "default");
        		$("#btnCancle").unbind("click");
        	}
        };
    </script>
    </body>

</html>
