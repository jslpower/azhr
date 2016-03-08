<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="CaoZuoRiZhi.aspx.cs" Inherits="EyouSoft.Web.Sys.CaoZuoRiZhi" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div class="mainbox">
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="DengLuRiZhi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    <span>登录日志</span> </a></li>
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="CaoZuoRiZhi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    <span>操作日志</span></a></li>
            </ul>
        </div>
        <div class="searchbox fixed">
            <span class="searchT">部门：<asp:DropDownList ID="ddlDepart" runat="server" CssClass=" inputselect">
            </asp:DropDownList>
                操作员：<input type="text" id="txtOperator" name="operator" class=" inputtext formsize80"
                    runat="server" />
                操作时间：<input type="text" class="inputtext formsize80" onfocus="WdatePicker()" id="txtBeginDate"
                    runat="server" />
                至
                <input type="text" class="inputtext formsize80" onfocus="WdatePicker()" id="txtEndDate"
                    runat="server" />
                <button class="search-btn" type="button">
                    搜索</button><p>
                    </p>
            </span>
        </div>
        <div class="tablehead" id="page">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr>
                        <th align="center" class="th-line">
                            操作员
                        </th>
                        <th align="center" class="th-line">
                            部门
                        </th>
                        <th align="center" class="th-line">
                            操作模块
                        </th>
                        <th align="center" class="th-line">
                            操作时间
                        </th>
                        <th align="center" class="th-line">
                            IP
                        </th>
                        <th align="center" class="th-line">
                            操作内容
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                           <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <%#Eval("Operator")%>
                                </td>
                                <td align="center">
                                    <%#Eval("DeptName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Menu1Name")%>-<%#Eval("Menu2Name")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime")%>
                                </td>
                                <td align="center">
                                    <%#Eval("RemoteIp")%>
                                </td>
                                <td align="left">
                                    <%#Eval("Content")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </cc2:CustomRepeater>
                </tbody>
            </table>
        </div>
        <div class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("page").innerHTML);
            </script>

        </div>
        <!--列表结束-->
    </div>
    </form>

    <script type="text/javascript">
        $(".search-btn").click(function() {
            var data = {
                sl: querystring(location.href, "sl"),
                departId: $("#<%=ddlDepart.ClientID %>").val(),
                operator: $("#<%=txtOperator.ClientID %>").val(),
                txtBeginDate: $("#<%=txtBeginDate.ClientID %>").val(),
                txtEndDate: $("#<%=txtEndDate.ClientID %>").val()
            };
            window.location.href = "CaoZuoRiZhi.aspx?" + $.param(data);
        });
        //回车事件
        $("input[type='text']").bind("keypress", function(e) {
            if (e.keyCode == 13) {
                $(".search-btn").click();
                return false;
            }
        });
    </script>

</asp:Content>
