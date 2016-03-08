<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DistributorNotice.ascx.cs"
    Inherits="Web.UserControl.DistributorNotice" %>
<div class="pub">
    <p>
        <span>公告：</span>
        <marquee height="32" width="90%" behavior="scroll" direction="left" onmouseout="this.start();"
            onmouseover="this.stop()" scrollamount="3" scrolldelay="12">
        <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
        <a href="javascript:void(0)" onclick="BindBoxy('<%# Eval("NoticeId")%>')">·<%#Eval("Title")%></a>
        </ItemTemplate>
        </asp:Repeater>
        </marquee>
    </p>
</div>
<script type="text/javascript">
    function BindBoxy(parame) {
    	var url = "/UserCenter/Notice/NoticeInfo.aspx?id=" + parame;
    	Boxy.iframeDialog({
    			iframeUrl: url,title: "公告查看",modal: true,width: "600px",height: "370px",fixed: true
    		});
    	return false;
    }
</script>