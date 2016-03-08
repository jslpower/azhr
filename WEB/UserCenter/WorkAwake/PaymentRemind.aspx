<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="PaymentRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.PaymentRemind" %>

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
                    <li>&nbsp;&nbsp;&nbsp; <span class="red">提醒的日期设定</span> 过了结算日&nbsp;<input type="text"
                        size="20" class="formsize80" name="text" id="txtDay">&nbsp;天后提醒 &nbsp;&nbsp;&nbsp;</li>
                    <li style="margin: 0px;"><a id="btnSave" class="ztorderform" style="padding-left: 2px;"
                        hidefocus="true" href="javascript:void(0);"><span>确定</span></a></li>
                </ul>
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
                            收款单位
                        </th>
                        <th align="center" class="th-line">
                            联系人
                        </th>
                        <th align="center" class="th-line">
                            电话
                        </th>
                        <th align="center" class="th-line">
                            欠款金额
                        </th>
                        <th align="center" class="th-line">
                            责任计调
                        </th>
                        <th align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <tr class="">
                        <td align="center">
                            1
                        </td>
                        <td align="center">
                            海南旅行社
                        </td>
                        <td align="center">
                            陈小红
                        </td>
                        <td align="center">
                            0571-85868487
                        </td>
                        <td align="center" class="red">
                            5680
                        </td>
                        <td align="center">
                            刘芳
                        </td>
                        <td align="center">
                            <a title="查看" class="check-btn fk_ck" href="gr-fukuantx-ck.html"></a>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="odd">
                                <td align="center">
                                    2
                                </td>
                                <td align="center">
                                    海南旅行社
                                </td>
                                <td align="center">
                                    陈小红
                                </td>
                                <td align="center">
                                    0571-85868487
                                </td>
                                <td align="center" class="red">
                                    5680
                                </td>
                                <td align="center">
                                    刘芳
                                </td>
                                <td align="center">
                                    <a title="查看" class="check-btn fk_ck" href="gr-fukuantx-ck.html"></a>
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
                
                var ret=/^-?\d+$/;//整数正则表达式
                if(!ret.test(day))
                {
                    tableToolbar._showMsg("必须输入整数！");
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
