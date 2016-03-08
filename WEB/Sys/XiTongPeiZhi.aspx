<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="XiTongPeiZhi.aspx.cs" Inherits="EyouSoft.Web.Sys.XiTongPeiZhi" %>

<%@ Register Src="/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="XiTongPeiZhi.aspx?sl=<%=SL %>"><span>打印配置</span></a></li>
                <%--<li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="YeWuPeiZhi.aspx?sl=<%=SL %>">
                    <span>业务配置</span></a></li>--%>
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="TuanHaoPeiZhi.aspx?sl=<%=SL %>">
                    <span>团号配置</span></a></li>
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr>
                        <th align="center" class="th-line" colspan="3">
                            打印配置
                        </th>
                    </tr>
                    <tr>
                        <td width="45%" bgcolor="#e0e9ef" align="right">
                            页眉上传(大小:695*115px):
                        </td>
                        <td width="55%" align="left" colspan="2">
                            <uc1:UploadControl ID="UploadYM" runat="server" />
                            <asp:Label ID="lblYM" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            页脚上传(大小:695*32px):
                        </td>
                        <td align="left" colspan="2">
                            <uc1:UploadControl ID="UploadYJ" runat="server" />
                            <asp:Label ID="lblYJ" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            word模板上传:
                        </td>
                        <td align="left" colspan="2">
                            <uc1:UploadControl ID="UploadWordTemp" runat="server" />
                            <asp:Label ID="lblWordTemp" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td bgcolor="#e0e9ef" align="right">
                            公司章上传:
                        </td>
                        <td align="left" colspan="2">
                            <uc1:UploadControl ID="UploadGZ" runat="server" />
                            <asp:Label ID="lblGZ" runat="server" Text=""></asp:Label>
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
                <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
    var PrintConfig={
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
            PrintConfig.Save();
            return false;
         });
      },
      //删除签证附件
            RemoveFile: function(obj) {
                $(obj).parent().remove();
            },
      //提交表单
            Save: function() {
                PrintConfig.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/XiTongPeiZhi.aspx?dotype=save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg,function(){ window.location.href='/Sys/XiTongPeiZhi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';});
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                        PrintConfig.BindBtn();
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后再试!");
                        PrintConfig.BindBtn();
                    }
                });
           }
         };
        $(function() {
           PrintConfig.BindBtn();
        })
    </script>

</asp:Content>
