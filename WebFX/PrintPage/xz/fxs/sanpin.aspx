<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../../MasterPage/Print.Master" ValidateRequest="false"
    CodeBehind="sanpin.aspx.cs" Inherits="EyouSoft.WebFX.PrintPage.xz.fxs.sanpin" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="PrintC1">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
        <tr>
            <td height="30" colspan="3" align="left">
                <span class="font14">团队编号：<asp:Label ID="lbTourCode" runat="server" Text=""></asp:Label></span>
            </td>
        </tr>
        <tr>
            <td width="50%" height="30" align="left">
                <span class="font14">敬呈：<asp:TextBox ID="txtunitname" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox
                    ID="txtunitContactname" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
            <td width="25%" align="left">
                <span class="font14">电话：<asp:TextBox ID="txtunittel" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
            <td width="25%" align="right">
                <span class="font14">传真：<asp:TextBox ID="txtunitfax" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
        </tr>
        <tr>
            <td width="50%" height="30" align="left">
                <span class="font14">发自：<asp:TextBox ID="txtsourcename" runat="server" CssClass="input120"></asp:TextBox>/<asp:TextBox
                    ID="txtname" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
            <td width="25%" align="left">
                <span class="font14">电话：<asp:TextBox ID="txttel" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
            <td width="25%" align="right">
                <span class="font14">传真：<asp:TextBox ID="txtfax" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="center">
                <b class="font24">
                    <asp:Label ID="lbRouteName" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
        id="TPlanFeature" class="borderline_2">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" checked="checked" name="checkbox" id="checkbox" />
                    行程特色</b>
            </td>
        </tr>
        <tr>
            <td class="td_text">
                <asp:Literal ID="lbPlanFeature" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:Literal ID="lbtourplan" runat="server"></asp:Literal>
    <asp:Literal ID="lbPriceStand" runat="server"></asp:Literal>
    <div runat="server" id="TPlanService">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
            id="TService" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" checked="checked" name="checkbox" id="checkbox2" />
                        服务标准</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbService" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
            id="TNoService" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" checked="checked" name="checkbox" id="checkbox3" />
                        服务不含</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbnoService" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TShopping"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" checked="checked" name="checkbox" id="checkbox4" />
                        购物安排</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbshopping" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TChildren"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" checked="checked" name="checkbox" id="checkbox5" />
                        儿童安排</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbchildren" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TSelfProject"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" checked="checked" name="checkbox" id="checkbox6" />
                        自费项目</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbselfproject" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TNeedAttention"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" checked="checked" name="checkbox" id="checkbox7" />
                        注意事项</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbneedattention" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TWarmRemind"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" checked="checked" name="checkbox" id="checkbox8" />
                        温馨提示</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbwarmremind" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function PrevFun() {
            $("input[type='checkbox'][name='checkbox']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                var checked = self.attr("checked");
                self.after("<label data-id='" + id + "' data-type='checkbox' data-checked='" + checked + "'></label>");
                if (!self.attr("checked")) {
                    self.closest("tr").hide();
                }
                self.remove();
            })

            setTimeout(function() {
                $("label[data-type='checkbox']").each(function() {
                    var id = $(this).attr("data-id");
                    var checded = $(this).attr("data-checked");
                    if (checded == 'false') {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' />")
                    }
                    else {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' checked='checked' />")
                    }
                    $(this).remove();
                })
                $("input[type='checkbox'][name='checkbox']").click(function() { showorhide(this) });
                $("input[type='checkbox'][name='checkbox']").closest("tr").show();
            }, 1000);
        }

        function showorhide(obj) {
            var self = $(obj);
            var id = self.attr("id");
            if (!self.attr("checked")) {
                self.closest("tr").next("tr").hide();
            }
            else {
                self.closest("tr").next("tr").show();
            }
        }
        $(function() {
            $("input[type='checkbox'][name='checkbox']").attr("checked", "checked")
            $("input[type='checkbox'][name='checkbox']").click(function() { showorhide(this) });
        })
    </script>

</asp:Content>
