<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="smsbank.aspx.cs" Inherits="Web.Webmaster.smsbank" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
    <style type="text/css">
    .tblLB{border-top:1px solid #ddd;border-left:1px solid #ddd;width: 100%;margin-bottom: 10px;}
    .tblLB thead{text-align:center;background: #efefef; height:35px;}
    .tblLB tfoot{text-align: center; height: 35px; background: #efefef;}
    .tblLB td{border-right:1px solid #ddd;border-bottom:1px solid #ddd; height:35px; text-align:center;}
    
    .cashierbox{display: none ;  position: absolute;  width: 300px;  height: 160px; border: 3px solid #bbb;  background-color: white;  z-index:1002; overflow: auto;/*FILTER: progid:dXImageTransform.Microsoft.Shadow(color:black,direction:145,strength:3);-webkit-box-shadow: 4px 4px 8px 5px #999;-moz-box-shadow: 4px 4px 8px 5px #999}'*/}
    .cashierbox p{line-height:normal;border:0px; line-height:20px;margin:5px;}
    
    </style>

    <script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>

    <script type="text/javascript">
        //分页配置
        var pConfig = {
            pageSize: 15,
            pageIndex: 1,
            recordCount: 0,
            showPrev: true,
            showNext: true,
            showDisplayText: false,
            cssClassName: 'page_change'
        }
        //分页初始化
        $(document).ready(function() {
            if (pConfig.recordCount > 0) {
                AjaxPageControls.replace("page_change", pConfig);
            }
        });

        //打开审核对话窗口
        function openCashierBox(aobj) {
            var obj = $(aobj);

            closeCashierBox();
            
            var chongZhiBianHao = obj.attr("data_chargeid");
            var chongZhiJinE = obj.attr("data_jine");

            $("#txtChongZhiBianHao").val(chongZhiBianHao);
            $("#txtChongZhiJinE").val(chongZhiJinE);
            $("#txtShiJiChongZhiJinE").val(chongZhiJinE);

            var offset = obj.offset();
            
            if ((offset.left + 300) > $(window).width()) offset.left = offset.left + obj.width() - 325;
            offset.top = offset.top - 25;
            $('#divCashierBox').css({ "top": offset.top + "px", "left": offset.left + "px" });
            
            $('#divCashierBox').show();
        }
        
        //关闭审核窗口
        function closeCashierBox() {
            
            $("#txtChongZhiBianHao").val('');
            $("#txtShiJiChongZhiJinE").val('0');
            $("#txtShenHeRen").val('');
            $("#txtShenHeBeiZhu").val('');

            $('#divCashierBox').hide();
        }

        //审核 status=T 通过 status=F不通过
        function cashier(status) {
            var _status = status ? 1 : 2;
            var params = { "chongZhiBianHao": $.trim($("#txtChongZhiBianHao").val())
                , "shiJiChongZhiJinE": $.trim($("#txtShiJiChongZhiJinE").val())
                , "shenHeRen": $.trim($("#txtShenHeRen").val())
                , "shenHeBeiZhu": $.trim($("#txtShenHeBeiZhu").val())
                , "status": _status
            };
            if (params.shenHeRen.length == 0) {
                alert("请填写审核人!");
                return false;
            }
            if (params.shiJiChongZhiJinE.length == 0 || parseFloat(params.shiJiChongZhiJinE) <= 0) {
                alert("请填写实际充值金额!");
                return false;
            }

            if (!confirm("此操作不可逆，你确定要操作吗？")) return false;

            $("#btnTongGuo").attr("disabled", "disabled");
            $("#btnBuTongGuo").attr("disabled", "disabled");

            $.ajax({
                url: "smsbank.aspx?isshenhe=400",
                cache: false,
                data: params,
                async: false,
                dataType: "text",
                type: "POST",
                success: function(response) {
                    if (response == "1") {
                        alert("操作成功！");
                        window.location.href = window.location.href;
                    } else {
                        alert("操作失败!");
                        $("#btnTongGuo").removeAttr("disabled");
                        $("#btnBuTongGuo").removeAttr("disabled");
                    }
                }
            });

            return false;
        }
    </script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    短信中心管理-账户充值审核
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">
    <asp:Repeater runat="server" ID="rptCharges" OnItemDataBound="rptCharges_ItemDataBound">
        <HeaderTemplate>
            <table class="tblLB" cellpadding="0" cellspacing="0">
                <thead>
                    <td>
                        序号
                    </td>
                    <td>
                        系统类型
                    </td>
                    <td>
                        充值公司
                    </td>
                    <td>
                        充值联系人
                    </td>
                    <td>
                        充值人电话
                    </td>
                    <td>
                        充值金额
                    </td>
                    <td>
                        充值状态
                    </td>
                    <td style="">
                        操作
                    </td>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tr onmouseover="changeTrBgColor(this,'#eeeeee')" onmouseout="changeTrBgColor(this,'#ffffff')">
                <td>
                    <%#(PageIndex-1)*PageSize+Container.ItemIndex + 1%>
                </td>
                <td>
                    <%#Eval("SysTypeName")%>
                </td>
                <td title="短信账户：<%#Eval("AccountId") %>">
                    <%#Eval("ChargeComName")%>
                </td>
                <td>
                    <%#Eval("ChargeName")%>
                </td>
                <td>
                    <%#Eval("ChargeTelephone")%>
                </td>
                <td>
                    <%#Eval("ChargeAmount","{0:C2}")%>
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrStatus"></asp:Literal>
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltrHandler"></asp:Literal>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            <div id="page_change" style="width: 100%; text-align: center; margin: 5px auto 0px; margin: 30px 0; clear: both">
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:PlaceHolder runat="server" ID="phNotFound" Visible="false">
        <div style="line-height: 60px;">
            <span class="required">暂任何充值记录。</span>
        </div>
    </asp:PlaceHolder>
    
    <!--审核窗口 起-->
    <div class='cashierbox' id='divCashierBox'>
        <p>
            &nbsp;充值金额：<input value='0' type='text' id='txtChongZhiJinE' disabled='disabled' class="input_text" style="width: 220px" maxlength="11" /></p>
        <p>
            &nbsp;实际充值：<input type='text' value='0' id='txtShiJiChongZhiJinE' class="input_text" style="width: 220px" maxlength="11" /></p>
        <p>
            &nbsp;审&nbsp;核&nbsp;人&nbsp;：<input value='' type='text' id="txtShenHeRen" class="input_text" style="width: 220px" maxlength="49" />
        </p>
        <p>
            &nbsp;审核时间：<input value='<%=DateTime.Now %>' type='text' disabled='disabled' class="input_text" style="width: 220px" />
        </p>
        <p>
            &nbsp;审核备注：<input value='' type='text' class="input_text" id="txtShenHeBeiZhu" style="width: 220px" maxlength="254" />
        </p>
        <p>
            <input type="hidden" id="txtChongZhiBianHao" />
            <input value='审核通过' type='button' onclick="cashier(true)" id="btnTongGuo" />
            &nbsp;&nbsp;<input value='审核不通过' type='button' onclick="cashier(false)" id="btnBuTongGuo" />
            &nbsp;&nbsp;<input value="关闭" type="button" onclick=" closeCashierBox(); return false;" /></p>
    </div>
    <!--审核窗口 止-->
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">
</asp:Content>
