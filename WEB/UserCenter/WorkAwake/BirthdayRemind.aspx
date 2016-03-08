<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="BirthdayRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.BirthdayRemind" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <div style="line-height: 25px; float: left;" class="red">
                今天生日</div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" style="margin-bottom: 1px;" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="th-line">
                            序号
                        </th>
                        <th align="center" class="th-line">
                            类型
                        </th>
                        <th align="center" class="th-line">
                            姓名
                        </th>
                        <th align="center" class="th-line">
                            生日
                        </th>
                        <th align="center" class="th-line">
                            手机
                        </th>
                        <th align="center" class="th-line">
                            单位名称
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    1
                                </td>
                                <td align="center">
                                    内部员工
                                </td>
                                <td align="center">
                                    张媛媛
                                </td>
                                <td align="center">
                                    1988-10-12
                                </td>
                                <td align="center">
                                    18656258768
                                </td>
                                <td align="center">
                                    杭州易诺旅行社(单位客户)
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <script type="text/javascript">
                        $('#example2').bt('杭州西湖地接社', { width: 100, fill: '#fff2b5', cornerRadius: 4, strokeWidth: 1, strokeStyle: '#d59228', trigger: ['mouseover', 'click'] });
                    </script>

                </tbody>
            </table>
        </div>
        <div class="tablehead">
            <div style="float: left;">  <ul class="fixed"><li>
                <span class="red">提前3天提醒</span> 设定提醒的天数：
                <input type="text" size="20" class="formsize80" name="text" id="txtDay"></li>
                   <li style="margin: 0px;"><a id="btnSave" class="ztorderform" style="padding-left: 2px;" hidefocus="true"
                        href="javascript:void(0);"><span>确定</span></a></li>
                        </ul>
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr>
                        <th width="30" class="th-line">
                            序号
                        </th>
                        <th align="center" class="th-line">
                            类型
                        </th>
                        <th align="center" class="th-line">
                            姓名
                        </th>
                        <th align="center" class="th-line">
                            生日
                        </th>
                        <th align="center" class="th-line">
                            手机
                        </th>
                        <th align="center" class="th-line">
                            单位
                        </th>
                    </tr>
                    <asp:Repeater ID="rptListSecond" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    1
                                </td>
                                <td align="center">
                                    内部员工
                                </td>
                                <td align="center">
                                    张媛媛
                                </td>
                                <td align="center">
                                    1988-10-12
                                </td>
                                <td align="center">
                                    18656258768
                                </td>
                                <td align="center">
                                    杭州易诺旅行社(单位客户)
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
    </div>
    </form>
         <script type="text/javascript">
             $(function() {
                 $("#btnSave").click(function() {
                     var day = $.trim($("#txtDay").val());
                     if (day == "") {
                         tableToolbar._showMsg("请输入天数！");
                         return false;
                     }
                     $.newAjax({
                         type: "post",
                         cache: false,
                         url: "<%=this.Request.Url.ToString() %>?doType=update&day=" + day,
                         success: function(ret) {
                             if (ret == "OK") {
                                 tableToolbar._showMsg("修改成功！");
                             }
                             else {
                                 tableToolbar._showMsg("服务器忙！");
                             }
                         },
                         error: function() {
                             tableToolbar._showMsg("服务器忙！");
                         }
                     });
                 })
                 return false;
             })
        
    </script>
</asp:Content>
