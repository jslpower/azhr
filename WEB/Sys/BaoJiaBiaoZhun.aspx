<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="BaoJiaBiaoZhun.aspx.cs" Inherits="EyouSoft.Web.Sys.BaoJiaBiaoZhun" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <uc1:BaseBar ID="BaseBar1" runat="server" />
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="add_xlqy" hidefocus="true" href="javascript:"><span>
                    添加</span></a></li><li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                </s><span>删除</span></a></li>
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" style="width: 30px">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th width="264" align="center" class="th-line">
                            报价标准名称
                        </th>
                        <th width="234" align="center" class="th-line">
                            操作
                        </th>
                        <th width="30" align="center">
                            &nbsp;
                        </th>
                        <th width="342" align="center" class="th-line">
                            报价标准名称
                        </th>
                        <th width="241" align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <tr>
                        <cc1:CustomRepeater ID="repList" runat="server">
                            <ItemTemplate>
                                <td align="left">
                                    <input type="checkbox" id="<%#Eval("Id")%>" value="<%#Eval("Id")%>" name="chk" />&nbsp;<%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#Eval("Name")%>
                                </td>
                                <td align="center">
                                    <a class="update_bjbz" target="_blank" href="BaoJiaBiaoZhunBJ.aspx?id=<%#Eval("Id")%>&Name=<%#Server.UrlEncode(Eval("Name").ToString())%>">
                                        修改</a>&nbsp; |&nbsp;<a class="del_xq" href="javascript:" onclick="return QuoteStandardList.Del('<%#Eval("Id")%>')">
                                            删除</a>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,i,2,3) %>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none;" class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add add_xlqy" hidefocus="true" href="javascript:">
                    <span>添加</span></a></li>
                <li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                </s><span>删除</span></a></li>
            </ul>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var QuoteStandardList = {

            Params: { memuid: querystring(location.href, "memuid"), sl: querystring(location.href, "sl") },

            GetSelectCount: function() {
                var count = 0;
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        count++;
                    }
                });
                return count;
            },
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).attr("id"));
                    }
                });
                return arrayList;
            },

            Del: function(id) {
                //删除
                var url = "BaoJiaBiaoZhun.aspx?ids=" + id + "&state=del&" + $.param(QuoteStandardList.Params);
                window.location.href = url;
                return false;
            },

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
        tableToolbar.IsHandleElse = "true";
        tableToolbar.init({
            tableContainerSelector: "#liststyle",
            objectName: " 记录",
            //默认按钮
            //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
            deleteCallBack: function(objsArr) {
                //删除
                var url = "BaoJiaBiaoZhun.aspx?ids=" + QuoteStandardList.GetSelectItemValue().toString() + "&state=del&" + $.param(QuoteStandardList.Params);
                window.location.href = url;
                return false;
            }
        });
        $(".add_xlqy").click(function() {
            var url = "BaoJiaBiaoZhunBJ.aspx?" + $.param(QuoteStandardList.Params);
            QuoteStandardList.openXLwindow(url, "添加报价标准", "500px", "99px");
            return false;
        });
        $(".update_bjbz").click(function() {
            var url = $(this).attr("href") + "&" + $.param(QuoteStandardList.Params); ;
            QuoteStandardList.openXLwindow(url, "修改报价标准", "500px", "99px");
            return false;
        });
       
    </script>

</asp:Content>
