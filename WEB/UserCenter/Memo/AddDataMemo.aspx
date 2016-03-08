<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDataMemo.aspx.cs" Inherits="EyouSoft.Web.UserCenter.Memo.AddDataMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../Css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="../../Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="../../Js/table-toolbar.js" type="text/javascript"></script>

    <script src="../../Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="../../Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    时间
                </td>
                <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    事件紧急程度
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    完成状态
                </td>
                <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                    事件标题
                </td>
                <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                    事件详情
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%#EyouSoft.Common.Utils.GetDateTime(Eval("MemoTime").ToString()).ToLongDateString()%>
                        </td>
                        <td height="28" align="center">
                            <%#Eval("UrgentType")%>
                        </td>
                        <td align="center">
                            <%#Eval("MemoState")%>
                        </td>
                        <td align="left" title="<%#Eval("MemoTitle") %>">
                            <%# EyouSoft.Common.Utils.GetText2(Eval("MemoTitle").ToString(),3,true)%>
                        </td>
                        <td align="center" title="<%#Eval("MemoText") %>">
                            <%# EyouSoft.Common.Utils.GetText2(Eval("MemoText").ToString(), 3, true)%>
                        </td>
                        <td align="center">
                            <a href="AddDataMemo.aspx?Id=<%#Eval("Id") %>&iframeId=<%=Request.QueryString["iframeId"]%>&stardate=<%=stardate %>">
                                <img src="../../images/y-delupdateicon.gif" border="0" />
                                修改</a> <a href="javascript:void(0);" onclick="Memo.deleteMemo('<%#Eval("Id") %>')">
                                    <img src="../../images/y-delicon.gif" />
                                    删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="hr_10">
        </div>
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="18%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontred">*</font>时间：
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox ID="txtMemoTime" runat="server" name="txtMemoTime" class="inputtext formsize80"
                        onfocus="WdatePicker()" errmsg="请输入汇报时间!" valid="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontred">*</font>事件紧急程度：
                </td>
                <td width="32%" align="left" bgcolor="#e0e9ef">
                    <asp:DropDownList ID="dropMemoUrgent" runat="server"  CssClass="inputselect">
                    </asp:DropDownList>
                    <%-- <select id="dropMemoUrgent" name="dropMemoUrgent" class="inputselect" runat="server">
                        <%=GetSelect("-1", "MemoUrgent") %>--%>
                </td>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontred">*</font>事件状态：
                </td>
                <td width="35%" align="left" bgcolor="#e0e9ef">
                    <%--<select id="dropMemoState" name="dropMemoState" class="inputselect" runat="server">
                        <%=GetSelect("-1", "MemoState") %>
                    </select>--%>
                    <asp:DropDownList ID="dropMemoState" runat="server" CssClass="inputselect">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="inputtext formsize180">
                    <font class="fontred">*</font>事件标题：
                </td>
                <td colspan="3" align="left">
                    <%--
                    <input name="txtMemoTitle" id="txtMemoTitle" type="text" runat="server" id="textfield3"
                        class="inputtext formsize80" errmsg="请输入事件标题!" valid="required" />--%>
                    <asp:TextBox ID="txtMemoTitle" runat="server" name="txtMemoTitle" class="inputtext formsize180"
                        errmsg="请输入事件标题!" valid="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    事件详细：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <%--
                    <textarea name="txtMemoText" id="txtMemoText" cols="6=" runat="server" class="inputtext formsize450"
                        style="height: 35px; padding: 3px;"></textarea>--%>
                    <asp:TextBox ID="txtMemoText" style="height:50px;" cols="20" rows="2" name="txtMemoText" CssClass="inputtext formsize450" runat="server"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="btnAddMemo"><s class="baochun"></s>
                保 存</a><a href="javascript:void(0)" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <asp:HiddenField ID="hidstart" runat="server" />
    <asp:HiddenField ID="hidid" runat="server" />

    <script type="text/javascript">
    var Memo={
        data:{sl:'<%=Request.QueryString["sl"] %>'},
        CheckForm: function() {
            var form = $("#btnAddMemo").closest("form").get(0);
            return ValiDatorForm.validator(form, "parent");
        },
        deleteMemo:function(id){
            $.newAjax({
                type: "post",
                cache: false,
                url: "AddDataMemo.aspx?AjaxType=DeleteMemo&Id="+id+"&sl=0",
                dataType:"json",
                success: function(ret) {
                    tableToolbar._showMsg(ret.msg);
                    if(ret.result=="true")
                       window.parent.frames.location.reload();
                },
                error: function() {
                        if(arguments[1]!=null)
                            tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                        else
                            tableToolbar._showMsg("服务器忙");
                }
            });
        
        },
        SaveMemo:function(){
            if (Memo.CheckForm()) {
                var dropMemoUrgent=$("#dropMemoUrgent").val();
                var dropMemoState=$("#dropMemoState").val();
                var MemoId=$("<%=hidid.ClientID %>");
                var memotitle= $("#<%=txtMemoTitle.ClientID %>").val();
                if(memotitle.indexOf('$')!=-1)
                {
                     tableToolbar._showMsg("标题包含敏感字符");
                     return false;
                }
                if(dropMemoUrgent=="-1")
                {
                     tableToolbar._showMsg("请选择事件紧急程度");
                     return false;
                }
                
                if(dropMemoState=="-1")
                {
                     tableToolbar._showMsg("请选择事件状态");
                     return false;
                }                
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "AddDataMemo.aspx?AjaxType=ajaxAddMemo&Id="+MemoId+"&sl=0",
                    data: $("#btnAddMemo").closest("form").serialize(),
                    dataType:"json",
                    success: function(ret) {
                        tableToolbar._showMsg(ret.msg);
                        if(ret.result=="true")
                           window.parent.frames.location.reload();
                    },
                    error: function() {
                        if(arguments[1]!=null)
                            tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                        else
                            tableToolbar._showMsg("服务器忙");
                    }
                });
            }
        },
        BindMemo:function(){
            $("#btnAddMemo").click(function() {
                Memo.SaveMemo();
                return false;
            })
        }
    }
    
    
    $(function(){
        Memo.BindMemo();    
    })
    
    </script>

    </form>
</body>
</html>
