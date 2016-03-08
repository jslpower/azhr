<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="DengLuRiZhi.aspx.cs" Inherits="EyouSoft.Web.Sys.DengLuRiZhi" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="DengLuRiZhi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"><span>
                        登录日志</span> </a></li>
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="CaoZuoRiZhi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    <span>操作日志</span></a></li>
            </ul>
        </div>
        <div class="searchbox fixed" style="height: 33px;">
            <form method="get">
            <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    姓名：<input type="text" name="txtName" class=" inputtext formsize80" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtName") %>" />
                    登录时间：<input type="text" name="txtLoginTimeS" onfocus="WdatePicker()" class="inputtext formsize80"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLoginTimeS") %>" />
                    -
                    <input type="text" name="txtLoginTimeE" onfocus="WdatePicker()" class="inputtext formsize80"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLoginTimeE") %>" />
                    <button class="search-btn" type="submit">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="page">
            <div class="pages">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr>
                        <th width="6%" align="center" class="th-line">
                            姓名
                        </th>
                        <th width="6%" align="center" class="th-line">
                            账号
                        </th>
                        <th width="12%" align="center" class="th-line">
                            登录时间
                        </th>
                        <th width="10%" align="center" class="th-line">
                            登录IP
                        </th>
                    </tr>
                    <cc1:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <%#Eval("Name") %>
                                </td>
                                <td align="center">
                                    <%#Eval("Username")%>
                                </td>
                                <td align="center">
                                    <%#Eval("LoginTime")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IP")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </cc1:CustomRepeater>
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
</asp:Content>
