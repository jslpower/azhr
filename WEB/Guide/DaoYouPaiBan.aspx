<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoYouPaiBan.aspx.cs" Inherits="EyouSoft.Web.Guide.DaoYouPaiBan" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <iframe src="PaiBanTongJi.aspx?sl=<%=this.SL %>" style="width: 100%;height:300px" frameborder="0" onload="$(this).height($(this).contents().find('body').attr('scrollHeight'));"></iframe>

    <div class="mainbox">
        <form id="formSearch" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=Request.QueryString["sl"]%>" />
                    <select name="seleYear" id="seleYear" class="inputselect">
                        <%=this.GetYearHtml()%>
                    </select>
                    年
                    <select name="seleMonth" id="seleMonth" class="inputselect">
                        <option value="0">-请选择-</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                    </select>
                    月 导游姓名：
                    <input type="text" class="inputtext" size="20" name="txtguidname" value="<%=Request.QueryString["txtguidname"] %>" />
                    下团时间：<input class="inputtext" type="text" size="20" name="txtStarTime" id="txtStarTime"
                        onfocus="WdatePicker();" value="<%=Request.QueryString["txtStarTime"] %>" />
                    -<input class="inputtext" type="text" size="20" name="txtEndTime" id="txtEndTime"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtStarTime\',{M:1})}'});" value="<%=Request.QueryString["txtEndTime"] %>" />
                    下团地点：<input class="inputtext" type="text" size="20" name="txtLocation" id="txtLocation"
                        value="<%=Request.QueryString["txtLocation"] %>" />
                    <input type="submit" id="search" class="search-btn" value="" /></p>
            </span>
        </div>

        <script type="text/javascript">
            function setValue(obj, v) {
                for (var i = 0; i < obj.options.length; i++) {
                    if (obj.options[i].value == v) {
                        obj.options[i].selected = true;
                    }
                }
            }
            //setValue($("#seleYear")[0], '<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("seleYear")) > 0 ? EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("seleYear")) : DateTime.Now.Year %>');
            //setValue($("#seleMonth")[0], '<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("seleMonth")) > 0 ? EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("seleMonth")) : DateTime.Now.Month %>');
            setValue($("#seleYear")[0], '<%=Year %>');
            setValue($("#seleMonth")[0], '<%=Month %>');
        </script>

        </form>
        <div class="tablehead">
            <table width="60%" border="0" align="left" cellpadding="0" cellspacing="0" class="kuang"
                style="margin-top: 2px">
                <tr>
                    <td width="51%" height="26">
                        &nbsp;<strong class="unnamed1">注解：</strong>&nbsp;
                        <img src="/images/dy-center/daoshang.gif" width="17" height="17" border="0" alt="" />上团&nbsp;
                        <img src="/images/dy-center/daoqi.gif" border="0" alt="" />行程中&nbsp;
                        <img src="/images/dy-center/daoxia.gif" border="0" alt="" />下团&nbsp;
                        <img src="/images/dy-center/daoshangxia.gif" border="0" alt="" />同天上下团&nbsp;
                        <img src="/images/dy-center/daoq.gif" width="17" height="17" border="0" alt="" />套团&nbsp;&nbsp;—&nbsp;无任务
<%--                        <img src="/images/dy-center/jia.gif" border="0" alt="" />假期&nbsp;
                        <img src="/images/dy-center/ting.gif" border="0" alt="" />停职&nbsp;
--%>                    </td>
                </tr>
            </table>
            <%--<div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>--%>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="50" align="center" class="th-line">
                        姓名
                    </th>
                    <th align="center" class="th-line">
                        28
                    </th>
                    <th align="center" class="th-line">
                        29
                    </th>
                    <th align="center" class="th-line">
                        30
                    </th>
                    <th align="center" class="th-line">
                        31
                    </th>
                    <th align="center" class="th-line">
                        1
                    </th>
                    <th align="center" class="th-line">
                        2
                    </th>
                    <th align="center" class="th-line">
                        3
                    </th>
                    <th align="center" class="th-line">
                        4
                    </th>
                    <th align="center" class="th-line">
                        5
                    </th>
                    <th align="center" class="th-line">
                        6
                    </th>
                    <th align="center" class="th-line">
                        7
                    </th>
                    <th align="center" class="th-line">
                        8
                    </th>
                    <th align="center" class="th-line">
                        9
                    </th>
                    <th align="center" class="th-line">
                        10
                    </th>
                    <th align="center" class="th-line">
                        11
                    </th>
                    <th align="center" class="th-line">
                        12
                    </th>
                    <th align="center" class="th-line">
                        13
                    </th>
                    <th align="center" class="th-line">
                        14
                    </th>
                    <th align="center" class="th-line">
                        15
                    </th>
                    <th align="center" class="th-line">
                        16
                    </th>
                    <th align="center" class="th-line">
                        17
                    </th>
                    <th align="center" class="th-line">
                        18
                    </th>
                    <th align="center" class="th-line">
                        19
                    </th>
                    <th align="center" class="th-line">
                        20
                    </th>
                    <th align="center" class="th-line">
                        21
                    </th>
                    <th align="center" class="th-line">
                        22
                    </th>
                    <th align="center" class="th-line">
                        23
                    </th>
                    <th align="center" class="th-line">
                        24
                    </th>
                    <th align="center" class="th-line">
                        25
                    </th>
                    <th align="center" class="th-line">
                        26
                    </th>
                    <th align="center" class="th-line">
                        27
                    </th>
                    <th align="center" class="th-line">
                        28
                    </th>
                    <th align="center" class="th-line">
                        29
                    </th>
                    <th align="center" class="th-line">
                        30
                    </th>
                    <th align="center" class="th-line">
                        31
                    </th>
                    <th align="center" class="th-line">
                        1
                    </th>
                    <th align="center" class="th-line">
                        2
                    </th>
                    <th align="center" class="th-line">
                        3
                    </th>
                    <th align="center" class="th-line">
                        4
                    </th>
                    <th align="center" class="th-line">
                        5
                    </th>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr <%#Eval("StaffStatus").ToString()=="离职"?"bgcolor='#EEEEEE'":""%> data-status="<%#Eval("StaffStatus").ToString()%>"
                            data-guideid="<%#Eval("GuideId")%>" data-name="<%#Eval("Name")%>">
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 1 ? (Year - 1) : Year).ToString(), (Month == 1 ? 12 : (Month - 1)).ToString(), 28, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 1 ? (Year - 1) : Year).ToString(), (Month == 1 ? 12 : (Month - 1)).ToString(), 29, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 1 ? (Year - 1) : Year).ToString(), (Month == 1 ? 12 : (Month - 1)).ToString(), 30, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 1 ? (Year - 1) : Year).ToString(), (Month == 1 ? 12 : (Month - 1)).ToString(), 31, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 1, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 2, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 3, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 4, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), (Month).ToString(), 5, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 6, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 7, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 8, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 9, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 10, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 11, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 12, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 13, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 14, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 15, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 16, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 17, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 18, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 19, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 20, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 21, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 22, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 23, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 24, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 25, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 26, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 27, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 28, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 29, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 30, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), Year.ToString(), Month.ToString(), 31, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 12 ? (Year + 1) : Year).ToString(), (Month == 12 ? 1 : (Month + 1)).ToString(), 1, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 12 ? (Year + 1) : Year).ToString(), (Month == 12 ? 1 : (Month + 1)).ToString(), 2, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 12 ? (Year + 1) : Year).ToString(), (Month == 12 ? 1 : (Month + 1)).ToString(), 3, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 12 ? (Year + 1) : Year).ToString(), (Month == 12 ? 1 : (Month + 1)).ToString(), 4, Eval("Name"), Eval("GuideId"))%>
                            </td>
                            <td align="center">
                                <%#GetState(Eval("TypeList"), (Month == 12 ? (Year + 1) : Year).ToString(), (Month == 12 ? 1 : (Month + 1)).ToString(), 5, Eval("Name"), Eval("GuideId"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;">
            <table width="60%" border="0" align="left" cellpadding="0" cellspacing="0" class="kuang"
                style="margin-top: 2px">
                <tr>
                    <td width="51%" height="26">
                        &nbsp;<strong class="unnamed1">注解：</strong>&nbsp;
                        <img src="/images/dy-center/daoshang.gif" width="17" height="17" border="0" />上团&nbsp;
                        <img src="/images/dy-center/daoqi.gif" border="0" />行程中&nbsp;
                        <img src="/images/dy-center/daoxia.gif" border="0" />下团&nbsp;
                        <img src="/images/dy-center/daoshangxia.gif" border="0" />同天上下团&nbsp;
                        <img src="/images/dy-center/daoq.gif" width="17" height="17" border="0" />套团&nbsp;&nbsp;—&nbsp;无任务
<%--                        <img src="/images/dy-center/jia.gif" border="0" alt="" />假期&nbsp;
                        <img src="/images/dy-center/ting.gif" border="0" alt="" />停职&nbsp;
--%>                    </td>
                </tr>
            </table>
            <%--<div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>--%>
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            GuidePage.PageInit();
            var dt = new Date();
            GuidePage.DTime = dt.getFullYear() + "-" + (parseInt(dt.getMonth()) + 1) + "-" + dt.getDate();
        });
        var GuidePage = {
            DTime: null,
            PageInit: function() {
                $("a.linkaa").click(function() {
                    if ($(this).parents("tr").attr("data-status") == "离职" && $(this).attr("data-type") == "无任务") {
                        tableToolbar._showMsg("导游已离职,无法安排! 请重新选择!");
                        return false;
                    }
                    Boxy.iframeDialog({
                        iframeUrl: "/Guide/DaoYouPaiBanEdit.aspx?name=" + encodeURIComponent($(this).parents("tr").attr("data-name")) + "&time=" + $(this).attr("data-time") + "&type=" + $(this).attr("data-type") + "&guideid=" + $(this).parents("tr").attr("data-guideid") + '&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        title: "选择任务",
                        modal: true,
                        width: "1000px",
                        height: "425px"
                    });
                    return false;
                });

                $("a.linkbb").click(function() {
                    if ($(this).parents("tr").attr("data-status") == "离职" && $(this).attr("data-type") == "无任务") {
                        tableToolbar._showMsg("导游已离职,无法安排! 请重新选择!");
                        return false;
                    }
                    Boxy.iframeDialog({
                        iframeUrl: "/Guide/DaoYouPaiBanEdit.aspx?name=" + encodeURIComponent($(this).parents("tr").attr("data-name")) + "&time=" + $(this).attr("data-time") + "&type=" + $(this).attr("data-type") + "&guideid=" + $(this).parents("tr").attr("data-guideid") + '&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        title: "选择任务",
                        modal: true,
                        width: "1000px",
                        height: "425px"
                    });
                    return false;
                });

                $("a.linkcc").click(function() {
                    if ($(this).parents("tr").attr("data-status") == "离职") {
                        tableToolbar._showMsg("导游已离职,无法安排! 请重新选择!");
                        return false;
                    }
                    tableToolbar._showMsg($(this).attr("data-type") + "期间,无法安排! 请重新选择!");
                    return false;
                });

            }

        }
    </script>

</asp:Content>
