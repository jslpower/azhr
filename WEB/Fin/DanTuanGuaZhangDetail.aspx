<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanTuanGuaZhangDetail.aspx.cs" ValidateRequest="false" Inherits="EyouSoft.Web.Fin.DanTuanGuaZhangDetail" MasterPageFile="~/MasterPage/Front.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server">
    <div class="mainbox mainbox-whiteback">
    
        <div id="divAllHtml">
        
        <div class="addContent-box">
            <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                <tr>
                    <td width="12%" class="addtableT">团号：</td>
                    <td width="24%" class="kuang2"><asp:Literal runat="server" ID="ltlTourCode"></asp:Literal></font></td>
                    <td width="12%" class="addtableT"> 线路区域：</td>
                    <td colspan="3" class="kuang2"><asp:Literal runat="server" ID="ltlAreaName"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="addtableT">线路名称：</td>
                    <td width="24%" class="kuang2"><asp:Literal runat="server" ID="ltlRouteName"></asp:Literal></td>
                    <td class="addtableT">天数：</td>
                    <td width="20%" class="kuang2"><asp:Literal runat="server" ID="ltlTourDays"></asp:Literal></td>
                    <td width="12%" align="right" class="addtableT">人数：</td>
                    <td class="kuang2"><b class="fontblue"><asp:Literal runat="server" ID="ltlAdult"></asp:Literal></b><sup class="fontred"><asp:Literal runat="server" ID="ltlChild"></asp:Literal>+<asp:Literal runat="server" ID="ltlLeader"></asp:Literal></sup></td>
                </tr>
                <tr>
                    <td class="addtableT">用房数：</td>
                    <td width="24%" class="kuang2"><asp:Literal runat="server" ID="ltlYongFangShu"></asp:Literal></td>
                    <td class="addtableT">团队国籍/地区：</td>
                    <td colspan="3" class="kuang2"><asp:Literal runat="server" ID="ltlCountry"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="addtableT">抵达日期：</td>
                    <td width="24%" class="kuang2"><asp:Literal runat="server" ID="ltlLDate"></asp:Literal></td>
                    <td class="addtableT">抵达城市：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltlArriveCity"></asp:Literal></td>
                    <td align="right" class="addtableT">航班/时间：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltlArriveCityFlight"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="addtableT">离境时间：</td>
                    <td width="24%" class="kuang2"><asp:Literal runat="server" ID="ltlRDate"></asp:Literal></td>
                    <td class="addtableT">离开城市：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltlLeaveCity"></asp:Literal></td>
                    <td align="right" class="addtableT">航班/时间：</td>
                    <td class="kuang2"><asp:Literal runat="server" ID="ltlLeaveCityFlight"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="addtableT">业务员：</td>
                    <td width="24%" class="kuang2"><asp:Literal runat="server" ID="ltlSellerName"></asp:Literal></td>
                    <td class="addtableT">OP：</td>
                    <td colspan="3" class="kuang2"><asp:Literal runat="server" ID="ltlPlaners"></asp:Literal></td>
                </tr>
            </table>
        </div>
        
        <div class="tablelist-box " style="width: 98.5%">
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th align="center">计调项</th>
                    <th align="left">单位名称</th>
                    <th align="center">状态</th>
                    <th align="center">应付金额</th>
                    <th align="center">已付金额</th>
                    <th align="center">已登待付</th>
                    <th align="center">未付金额</th>
                </tr>
                <asp:Repeater runat="server" ID="rptList">
                <ItemTemplate>
                <tr>
                    <td align="center"><%#Eval("Type") %></td>
                    <td align="left"><%#Eval("SourceName") %></td>
                    <td align="center"><%#(bool)Eval("CostStatus")?"已确认":"未确认"%></td>
                    <td align="center"><b class="fontred"><%#Eval("Confirmation","{0:C2}")%></b></td>
                    <td align="center"><b class="fontgreen"><%#Eval("Prepaid","{0:C2}")%></b></td>
                    <td align="center"><b><%#Eval("YiDengDaiFu","{0:C2}") %></b></td>
                    <td width="38%" align="center"><b class="fontblue"><%#Eval("Unpaid","{0:C2}") %></b></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server" Visible="false">
                    <tr align="center">
                        <td colspan="15">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        
        </div>
        <div class="hr_10"></div>
        <div class="mainbox cunline fixed">
            <ul>
                <li class="cun-cy"><a href="javascript:void(0);" onclick="PrintPage(this);">打印</a></li>
                <li class="cun-cy"><a href="javascript:void(0);" onclick="return ReplaceInput();" runat="server" OnServerClick="ibtnWord_Click">导出WORD</a></li>
            </ul>
         
            <div class="hr_10"></div>
        </div>
	   
	   
    </div>
    <div id="printNone" style="display: none"></div>
    <input id="hidPrintHTML" name="hidPrintHTML" type="hidden" />
    <input id="hidDocName" name="hidDocName" type="hidden" runat="server" value="" />
    <input id="hideFontSize" type="hidden" value="12" />
    <input id="hideLineHeight" type="hidden" value="26" />
    
    <script type="text/javascript">
    function ReplaceInput() {
            if (window["PrevFun"] != null) window["PrevFun"]();

            var _$printdiv = $("#printNone");
            _$printdiv.html($("#divAllHtml").html());
            _$printdiv.find("div[ref='noprint'],[type='hidden']").replaceWith("");
            _$printdiv.find("input[type='checkbox']").replaceWith("");
            _$printdiv.find("input,textarea").each(function() {
                var values = $(this).val().replace(/\n/g, "<br/>");
                $(this).before("<span class='input " + $(this).attr("class") + "'>" + values + "</span>");
                $(this).replaceWith("");
            });
            _$printdiv.find(".unprint").replaceWith("");
            _$printdiv.find(".undaochu").replaceWith("");

            $("#hidPrintHTML").val(_$printdiv.html());

            return true;
        }
    </script>
    </form>
</asp:Content>