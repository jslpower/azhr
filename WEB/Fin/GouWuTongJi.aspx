<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GouWuTongJi.aspx.cs" Inherits="EyouSoft.Web.Fin.GouWuTongJi" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="mainbox">
            <div class="searchbox border-bot fixed">
            <form method="GET">
                <span class="searchT">
                    <p>
                        进店日期：<input value="<%=Request.QueryString["sd"] %>" name="sd" type="text" class="formsize80 input-txt" onfocus="WdatePicker();" /> - <input value="<%=Request.QueryString["ed"] %>" name="ed" type="text" class="formsize80 input-txt" onfocus="WdatePicker();"/>
                        国籍：<input value="<%=Request.QueryString["gj"] %>" name="gj" type="text" class="formsize80 input-txt" />
                        购物店：<input value="<%=Request.QueryString["nm"] %>" name="nm" type="text" class="formsize140 input-txt" />
                        <input type="submit" class="search-btn"/>
                        <input type="hidden" name="sl" value="<%=this.SL %>"/>
                    </p>
                </span>
            </form>
            <div id="pages" class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
            </div>
          <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                        <th align="center" >
                            购物店
                    </th>
                        <th align="center" >
                            保底额
                    </th>
                        <th align="center" >
                            流水百分比
                      </th>
                        <th align="center" > 保底合计数
                      </th>
                        <th align="center" >
                            流水合计数
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" />
                                </td>
                                <td align="center"><%#Eval("GysName")%></td>
                                <td align="center"><b class="fontgreen"><%#Eval("BaoDiJinE", "{0:C2}")%></b></td>
                                <td align="center"><%#Eval("LiuShui","{0:C2}") %></td>
                                <td align="center"><a href="javascript:void(0);" class="link1" data-id="<%#Eval("GysId") %>"><%#Eval("BaoDiHeJiShu") %></a></td>
                                <td align="center"><a href="javascript:void(0);" class="link2" data-id="<%#Eval("GysId") %>"><%#Eval("LiuShuiHeJiShu") %></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="15">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                </table>
</div>
            <!--列表结束-->

                <div id="tablehead_clone">
                </div>
        </div>
<script type="text/javascript">
var Page = {
	ShowBoxy: function(data) { /*显示弹窗*/
		Boxy.iframeDialog({
				iframeUrl: data.url,
				title: data.title,
				modal: true,
				width: data.width,
				height: data.height,
				draggable: true
			});
	},
	DataBoxy: function() { /*弹窗默认参数*/
		return {
			url: "/Fin/GouWuTongJiDetail.aspx?",
			title: "",
			width: "600px",
			height: "350px"
		};
	},
	BindBtn: function() { /*绑定功能按钮*/
		$(".link1").click(function() { /*保底合计数明细*/
			var data = Page.DataBoxy();
			data.title = "保底合计数";
		    data.url += $.param({ sl: '<%=this.SL %>', sourceId: $(this).attr("data-id") });
			Page.ShowBoxy(data);
			return;
		});
		$(".link2").click(function() { /*流水合计数明细*/
			var data = Page.DataBoxy();
			data.title = "流水合计数";
		    data.url += $.param({ sl: '<%=this.SL %>', sourceId: $(this).attr("data-id") });
			Page.ShowBoxy(data);
			return;
		});
		tableToolbar.init({
				tableContainerSelector: "#liststyle", //表格选择器
				objectName: "记录"
			});
	},
	PageInit: function() {
		Page.BindBtn();
		$("#tablehead_clone").html($("#pages").clone(true).css("border-top", "0 none"));
	}
};

$(function() {
	Page.PageInit();
});

</script>
</asp:Content>
