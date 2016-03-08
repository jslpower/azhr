<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaYin.aspx.cs" Inherits="EyouSoft.WebFX.DaYin" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc3" %>

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
<script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

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
            <a href="YongHu.aspx?LgType=<%=Request.QueryString["LgType"] %>"><span> <%=(String)GetGlobalResourceObject("string", "用户信息")%></span></a><a href="DaYin.aspx?LgType=<%=Request.QueryString["LgType"] %>" class="linebox-menudefault"><span> <%=(String)GetGlobalResourceObject("string", "打印设置")%></span></a>
       </div>
       <div class="hr_10"></div>
       <div class="listtablebox">
       <form id="frmupload">
         <table width="100%" border="0" cellpadding="0" cellspacing="0" >
           <tr>
             <td width="20%" align="right" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "页眉图片")%>：</td>
             <td width="80%" align="left">
                                <uc3:UploadControl runat="server" ID="UploadControl1" />
                                <br />
                                <asp:Label runat="server" ID="lblPrintHeader"></asp:Label>
             </td>
           </tr>
           <tr>
             <td align="right" bgcolor="#DCEFF3"><%=(String)GetGlobalResourceObject("string", "页脚图片")%>：</td>
             <td align="left">
                                <uc3:UploadControl runat="server" ID="UploadControl2" />
                                <br />
                                <asp:Label runat="server" ID="lblPrintFooter"></asp:Label>
             </td>
           </tr>
           <tr>
             <td colspan="2" align="right"><div class="mainbox cunline fixed">
               <ul>
                 <li class="cun-cy"><a href="javascript:void(0);" id="btnSave"><%=(String)GetGlobalResourceObject("string", "保存")%></a></li>
                 <li class="quxiao-cy"><a href="javascript:void(0);" id="btnCancle"><%=(String)GetGlobalResourceObject("string", "取消")%></a></li>
               </ul>
             </div></td>
           </tr>
         </table>
         </form>
       </div>
       <div class="hr_10"></div>
    </div>
  </div>
  <script type="text/javascript">
    var ConfigSettings = {
        Data: {
            Type: "Save"
        },
        Save: function() {
            $.newAjax({
                url: "DaYin.aspx?" + $.param(ConfigSettings.Data),
                type: "post",
                data: $("#frmupload").serialize(),
                dataType: "json",
                success: function(back) {
                    if (back.result == "1") {
                        tableToolbar._showMsg(back.msg, function() {
                            window.location.reload();
                        });

                    }
                    else {
                        tableToolbar._showMsg(back.msg);
                    }
                },
                error: function() {
                    tableToolbar._showMsg("服务器忙！");
                }
            });
        },
        Cancle: function() {
            window.location.reload();
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        PageInit: function() {
            $("#btnSave").click(function() {
                ConfigSettings.Save();
            });
            $("#btnCancle").click(function() {
                ConfigSettings.Cancle();
            });

        }
    };


    $(function() {
        ConfigSettings.PageInit();
    });
</script>
</body>

</html>
