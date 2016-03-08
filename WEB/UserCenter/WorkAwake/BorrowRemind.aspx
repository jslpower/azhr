<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="BorrowRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.BorrowRemind" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
            <div style="float: left; padding-top: 5px;">
                <ul class="fixed">
                    <li>&nbsp;&nbsp;&nbsp; <span class="red">设置提前<input type="text" size="20" class="formsize80"
                        name="text" id="txtDay">
                        天提醒</span>&nbsp;&nbsp;&nbsp;</li>
                    <li style="margin: 0px;"><a id="btnSave" class="ztorderform" style="padding-left: 2px;"
                        hidefocus="true" href="javascript:void(0);"><span>确定</span></a></li></ul>
            </div>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="th-line">
                            编号
                        </th>
                        <th align="center" class="th-line">
                            物品名称
                        </th>
                        <th align="center" class="th-line">
                            借阅时间
                        </th>
                        <th align="center" class="th-line">
                            归还时间
                        </th>
                        <th align="center" class="th-line">
                            借阅部门
                        </th>
                        <th align="center" class="th-line">
                            借阅人
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    1
                                </td>
                                <td align="center">
                                    《一个企业的信念》
                                </td>
                                <td align="center">
                                    2010-10-13
                                </td>
                                <td align="center">
                                    2011-05-06
                                </td>
                                <td align="center">
                                    市场部
                                </td>
                                <td align="center">
                                    张娜
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none;" class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
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
