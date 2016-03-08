<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="MemberTypeList.aspx.cs" Inherits="Web.Sys.MemberTypeList" %>

<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <uc1:BasciSetBar ID="BasciSetBar1" runat="server" />
        <span id="btnAction">
            <div class="tablehead">
                <ul class="fixed">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="javascript:">
                        <span>添加</span></a></li>
                    <li class="line"></li>
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:"><s class="delicon">
                    </s><span>删除</span></a></li>
                </ul>
            </div>
        </span>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" style="width: 5%">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th style="width: 43%" align="center" class="th-line">
                            类型名称
                        </th>
                        <th style="width: 5%" align="center">
                            &nbsp;
                        </th>
                        <th style="width: 43%" align="center" class="th-line">
                            类型名称
                        </th>
                    </tr>
                    <tr>
                        <asp:Repeater ID="repList" runat="server">
                            <ItemTemplate>
                                <td align="left">
                                    <input type="checkbox" id="<%#Eval("Id")%>" value="<%#Eval("Id")%>" name="chk">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%#Eval("TypeName")%>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,i,2,2) %>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->

        <script type="text/javascript">
            document.write(document.getElementById("btnAction").innerHTML);
        </script>

    </div>
    </form>

    <script type="text/javascript">
        var MemberTypeList = {
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chk']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).attr("id"));
                    }
                });
                return arrayList;
            },
            Params: { memuid: querystring(location.href, "memuid"), sl: querystring(location.href, "sl") },
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
                tableContainerSelector: "#liststyle",
                objectName: " 记录",
                updateCallBack: function(arr) {
                    var url = "MemberTypeEdit.aspx?id=" + MemberTypeList.GetSelectItemValue().toString() + "&" + $.param(MemberTypeList.Params);
                    MemberTypeList.openXLwindow(url, "修改会员类型", "500px", "99px");
                },
                deleteCallBack: function(arr) {
                var url = "MemberTypeList.aspx?ids=" + MemberTypeList.GetSelectItemValue().toString() + "&state=del" + "&" + $.param(MemberTypeList.Params);
                    window.location.href = url;
                    return false;
                }
            });
            $(".toolbar_add").click(function() {
                var url = "MemberTypeEdit.aspx?" + $.param(MemberTypeList.Params);
                MemberTypeList.openXLwindow(url, "添加会员类型", "500px", "99px");
                return false;
            });
        });
      
    </script>

</asp:Content>
