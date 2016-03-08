<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectScenicPrice.aspx.cs" Inherits="EyouSoft.WebFX.CommonPage.selectScenicPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
                id="tblList" style="margin: 0 auto">
                <tr class="odd">
                    <td>
                    </td>
                    <td>
                        <%=(String)GetGlobalResourceObject("string", "团型") %>
                    </td>
                    <td>
                        <%=(String)GetGlobalResourceObject("string", "宾客类型") %>
                    </td>
                    <td>
                        <%=(String)GetGlobalResourceObject("string", "开始时间") %>
                    </td>
                    <td>
                        <%=(String)GetGlobalResourceObject("string", "截止时间") %>
                    </td>
                    <td>
                        <%=(String)GetGlobalResourceObject("string", "同行价") %>
                    </td>
                    <td>
                        <%=(String)GetGlobalResourceObject("string", "结算价") %>
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="RepList">
                    <ItemTemplate>
                        <tr>
                            <td align="left" height="30px">
                                <input name="1" type="radio" value="<%#Eval("JiaGeId") %>" data-jiagejs="<%#Convert.ToDecimal(Eval("JiaGeJS")).ToString("f2")%>"
                                    data-jiageth="<%#Convert.ToDecimal(Eval("JiaGeTH")).ToString("f2")%>" <%#Eval("JiaGeId")==Request.QueryString["priceid"]?"checked=checked":"" %> />
                            </td>
                            <td align="left" height="30px">
                                <%#Eval("TuanXing").ToString()%>
                            </td>
                            <td align="left" height="30px">
                                <%#Eval("BinKeLeiXing").ToString()%>
                            </td>
                            <td align="left" height="30px">
                                <%#GetDateTime(Eval("STime"))%>
                            </td>
                            <td align="left" height="30px">
                                <%#GetDateTime(Eval("ETime"))%>
                            </td>
                            <td align="left" height="30px">
                                <%#Convert.ToDecimal(Eval("JiaGeTH")).ToString("f2") %>
                            </td>
                            <td align="left" height="30px">
                                <%#Convert.ToDecimal(Eval("JiaGeJS")).ToString("f2") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal runat="server" ID="lbemptymsg"></asp:Literal>
            </table>
            <div class="alertbox-btn">
                <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s><%=(String)GetGlobalResourceObject("string", "选择") %></a></div>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function() {
        //CaidanId
        var data = {
            priceId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("priceId") %>'
        }
        //选中单选钮
        $(":radio[value='" + data.priceId + "']").attr("checked", "checked");
        $("#a_btn").click(function() {
            SelectPage.SetValue();
            SelectPage.SelectValue();
        })
    })
    var SelectPage = {
        priceid: "",
        jiageth: "",
        jiagejs: '',
        SetValue: function() {
            $("#tblList").find("input[type='radio']:checked").each(function() {
                SelectPage.jiagejs = $(this).attr("data-jiagejs");
                SelectPage.priceid = $(this).val();
                SelectPage.jiageth = $(this).attr("data-jiageth");
            })
        },
        SelectValue: function() {
            var data = {
                callBack: Boxy.queryString("callBack"),
                hideID: Boxy.queryString("hideID"),
                iframeID: Boxy.queryString("iframeId"),
                pIframeID: '<%=Request.QueryString["parentiframeid"] %>'
            }

            var args = {
                scenicid: '<%=Request.QueryString["hideID"] %>',
                priceid: SelectPage.priceid,
                jiageth: SelectPage.jiageth,
                jiagejs: SelectPage.jiagejs
            }

            var boxyParent = window.parent.Boxy.getIframeWindow(data.pIframeID) || window.parent.Boxy.getIframeWindowByID(data.pIframeID);
            //根据父级是否为弹窗传值
            if (data.pIframeID != "" && data.pIframeID.length > 0) {
                //定义父级弹窗
                var boxyParent = window.parent.Boxy.getIframeWindow(data.pIframeID) || window.parent.Boxy.getIframeWindowByID(data.pIframeID);
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    if (data.callBack.indexOf('.') == -1) {
                        boxyParent[data.callBack](args);
                    }
                    else {
                        boxyParent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                    }
                }
                //定义回调
            }
            else {
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    if (data.callBack.indexOf('.') == -1) {
                        boxyParent[data.callBack](args);
                    }
                    else {
                        boxyParent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                    }
                }
                //定义回调
            }
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }
    }
</script>
