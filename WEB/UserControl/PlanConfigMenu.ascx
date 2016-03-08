<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlanConfigMenu.ascx.cs" Inherits="EyouSoft.Web.UserControl.PlanConfigMenu" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.PrivsStructure" %>
<div class="jdcz-menu">
    <ul>
        <li class="jdczmenu-t jdczmenu-first"><s></s>计调操作</li>
        <li><a id="configpage" href="/Plan/PlanConfigPage.aspx?&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&tourId=<%=TourId %>">
            计调配置</a></li>
        <li><a id="globalPage" href="/Plan/PlanLobal.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&tourId=<%=TourId %>">
            全局计调</a></li>
        <%--<li><a id="A1" href="/Sales/ChanPinEdit.aspx?sl=<%=(int)Menu2.团队中心_团队产品 %>&Id=<%=TourId %>&act=updat" target="_blank">
            行程修改</a></li>--%>
        <li class="jdczmenu-t jdczmenu-Second"><s></s>导游相关</li>
        <li><a id="pattyCashPage" href="/Plan/PettyCashList.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&tourId=<%=TourId %>">
            备用金申请</a></li>
        <li><a id="baozhangPage" href="/Guide/BaoZhangCaoZuo.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调报账 %>&tourId=<%=TourId %>">
            计调报账 </a></li>
        <li class="jdczmenu-t jdczmenu-Third"><s></s>表单打印</li>
        <li><a href='<%=Print_XingChengDan %>' target="_blank">确认导游行程单</a></li>
        <li><a href='<%=_original %>' target="_blank">行程原始单</a></li>
        <li><a href='<%=Print_DaoYouRenWuDan %>' target="_blank">行程计划单</a></li>
        <li><a href='<%=Print_YouKeMingDan %>' target="_blank">游客名单</a></li>
        <li><a id="a_duanxintongzhi" href='/Plan/DuanXinTongZhi.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&tourId=<%=TourId %>'>短信通知</a></li>
    </ul>
</div>

<script type="text/javascript">
    $(document).ready(function() {
    	switch ('<%=this.IndexClass %>') {
    	case "1":
    		$("#configpage").addClass("jdapdefault");
    		break;
    	case "2":
    		$("#globalPage").addClass("jdapdefault");
    		break;
    	case "3":
    		$("#pattyCashPage").addClass("jdapdefault");
    		break;
    	case "4":
    		$("#baozhangPage").addClass("baozhangPage");
    		break;
    	case "5":
    		$("#a_duanxintongzhi").addClass("jdapdefault");
    		break;
    	default:
    		$("#configpage").addClass("jdapdefault");
    		break;
    	}
    });
</script>