<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Front.Master"
    CodeBehind="SmsList.aspx.cs" Inherits="Web.SmsCenter.SmsList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/SysTop.ascx" TagName="SysTop" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:SysTop ID="SmsTop" runat="Server"></uc1:SysTop>
        <form id="form1" method="get">
        <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
        <div class="searchbox fixed" style="border-bottom: none;">
            <span class="searchT">
                <p>
                    关键字：
                    <input type="text" name="iptKeyWord" class="inputtext formsize120" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iptKeyWord") %>" />
                    类型：
                    <select id="type" name="type" class=" inputselect">
                        <%= GetTypeList() %>
                    </select>
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="PageBtn">
            <ul class="fixed">
                <li><s class="addicon"></s><a href="javascript:" hidefocus="true" class="toolbar_add">
                    <span>新增</span></a></li>
                <li class="line"></li>
                <li><s class="updateicon"></s><a href="javascript:" hidefocus="true" class="toolbar_update">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><s class="delicon"></s><a href="javascript:" hidefocus="true" class="toolbar_delete">
                    <span>删除</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellpadding="0" cellspacing="0" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        类型
                    </th>
                    <th align="center" class="th-line">
                        发送内容
                    </th>
                </tr>
                <cc2:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="chk" value="<%#Eval("PhraseId") %>" id="<%#Eval("PhraseId") %>" />
                            </td>
                            <td align="center">
                                <%#((EyouSoft.Model.SmsStructure.MSmsPhraseTypeBase)Eval("SmsPhraseType")).TypeName%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.Utils.GetText2(Eval("Content").ToString(), 50, true)%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc2:CustomRepeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" id="PageBtn">

            <script type="text/javascript">
                document.write(document.getElementById("PageBtn").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript">

        var SmsList = {
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).val());
                    }
                });
                return arrayList;
            },
            sl: querystring(location.href, "sl"),
            openXLwindow: function(url, title, width, height) {
                url = url;
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: width,
                    height: height
                });
            }
        }
        $(function() {
            tableToolbar.IsHandleElse = "true";
            tableToolbar.init({
                updateCallBack: function(arr) {
                    SmsList.openXLwindow("/SmsCenter/SMSEdit.aspx?dotype=update&id=" + arr[0].find(":checkbox").val() + "&sl=" + SmsList.sl, "常用短信修改", "700px", "185px");
                    return false;
                },
                deleteCallBack: function(arr) {
                    var url = "/SmsCenter/SmsList.aspx?dotype=del&ids=" + SmsList.GetSelectItemValue().toString() + "&sl=" + SmsList.sl
                    window.location.href = url;
                    return false;
                }
            });
            $(".toolbar_add").click(function() {
                var url = "/SmsCenter/SMSEdit.aspx?sl=" + SmsList.sl;
                SmsList.openXLwindow(url, "常用短信添加", "700px", "185px");
                return false;
            });
        });
    </script>

    <!-- InstanceEndEditable -->
</asp:Content>
