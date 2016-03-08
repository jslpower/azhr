<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMemorandum.aspx.cs"
    Inherits="Web.UserCenter.Memorandum.AjaxMemorandum" %>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<form id="form1" runat="server">
<asp:calendar id="CalendarDate" runat="server" cssclass="jsh_table" firstdayofweek="Monday"
    border="0" ondayrender="CalendarDate_DayRender" cellspacing="1" bgcolor="#c7c7c7"
    style="border-collapse: collapse" title="">
            </asp:calendar>
</form>

<script type="text/javascript">

    var AjaxMemorandum = {
            getAjaxData: function(params) {
                var resultmsg = "";
                var obj = $(params.obj);
                //是否第一次浏览该条数据
                var Ispostback = $.trim(obj.attr("data_Ispostback"));
                if (params.objresult == null)//ajax请求数据
                {
                    if (Ispostback == "false") {
                        $.newAjax({
                            type: params.ajaxt,
                            cache: false,
                            url: params.url,
                            dataType: params.ajaxdt,
                            async: false,
                            success: function(ret) {
                                if (ret.result != null && ret.result == "true") {
                                    obj.attr("data_Ispostback", "true");
                                    obj.next().html(ret.msg);
                                }
                                resultmsg = ret.msg;
                            },
                            error: function() {
                                resultmsg = "服务器忙";
                            }
                        });
                    }
                    else
                        resultmsg = $.trim(obj.next().html());
                }
                else
                    resultmsg = $(params.objresult).html();
                return resultmsg;
            },
            BindBT: function(params, newoptions) {
                var options = {};
                var jqueryobj = $(params.obj);
                options = {
                    contentSelector: function() {
                        var strResult = "";
                        strResult = AjaxMemorandum.getAjaxData(params);
                        return strResult;
                    },
                    positions: ['top','left','bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 5,
                    spikeGirth: 15,
                    width: 220,
                    overlap: 0,
                    centerPointY: 4,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '200%' }
                }
                options = $.extend(options, newoptions);
                jqueryobj.bt(options);
            },
            Data: {
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                ajaxtype: 'AjaxLoadData'
            }
     }   

        //自定义参数
        function params() {
            this.obj = null;
            this.ajaxt = "";
            this.ajaxdt = "";
            this.url = "";
            this.objresult = null; //为null是直接获取对象html，不为null为ajax异步请求数据
        }

</script>

<script type="text/javascript">
    $(function() {
        $("#<%=CalendarDate.ClientID %>").find("tr").eq(0).css("display", "none");
        $("#<%=CalendarDate.ClientID %>").find("td").eq(0).css("width", "99%");
        
        $("#<%=CalendarDate.ClientID %>").find("a").attr("href", "javascript:void(0);");
        $("#<%=CalendarDate.ClientID %>").find("a").attr("data_Ispostback", "false");
        $("#<%=CalendarDate.ClientID %>").find("a").after("<div style='display:none'></div>");
        
        var staDate=$("#<%=CalendarDate.ClientID %>").find("a").eq(2).attr("title");
        var len=$("#<%=CalendarDate.ClientID %>").find("a").length;
        var endDate=$("#<%=CalendarDate.ClientID %>").find("a").eq(len-1).attr("title");
        
        $("#<%=CalendarDate.ClientID %>").find("a").removeAttr("style");
        $(".today").find("a").css("color", "red");
        var lastVal = "";
        $("#<%=CalendarDate.ClientID %> tr:last").find("td").each(function() {
            lastVal += $.trim($(this).html());
        })
        if (lastVal == "") {
            $("#<%=CalendarDate.ClientID %> tr:last").remove();
        }

        var fristVal = "";
        $("#<%=CalendarDate.ClientID %> tr").eq(3).find("td").each(function() {
            fristVal += $.trim($(this).html());
        })
        if (fristVal == "") {
            $("#<%=CalendarDate.ClientID %> tr").eq(3).remove();
        }
        if ('<%=Request.QueryString["date"]%>' == '<%=DateTime.Now.ToString("yyyy-M-01")%>')
            switch ("<%=DateTime.Now.DayOfWeek %>") {
            case "Monday":
                $("#<%=CalendarDate.ClientID %> tr").eq(2).find("th").eq(0).css("color", "red");
                break;
            case "Tuesday":
                $("#<%=CalendarDate.ClientID %> tr").eq(2).find("th").eq(1).css("color", "red");
                break;
            case "Wednesday":
                $("#<%=CalendarDate.ClientID %> tr").eq(2).find("th").eq(2).css("color", "red");
                break;
            case "Thursday":
                $("#<%=CalendarDate.ClientID %> tr").eq(2).find("th").eq(3).css("color", "red");
                break;
            case "Friday":
                $("#<%=CalendarDate.ClientID %> tr").eq(2).find("th").eq(4).css("color", "red");
                break;
            case "Saturday":
                $("#<%=CalendarDate.ClientID %> tr").eq(2).find("th").eq(5).css("color", "red");
                break;
            case "Sunday":
                $("#<%=CalendarDate.ClientID %> tr").eq(2).find("th").eq(6).css("color", "red");
                break;

        }
        var ajaxcomplete=false;
        $.newAjax({
            type: 'GET',
            cache: false,
            url: '../UserCenter/Memorandum/AjaxMemorandum.aspx?ajaxtype=GetMemoAll&argument='+escape(staDate+'$'+endDate),
            dataType: 'json',
            async:false,
            success: function(ret) {
                if (ret.result != null) {
                    var listmemo=ret.result.split('$');
                    var listmemoContent=ret.msg.split('$');
                    if(listmemo!=null&&listmemo.length>0)
                    {
                        $("#<%=CalendarDate.ClientID %>").find("a").each(function(j){
                            if(j>1){
                                var obj=$(this);
                                var datecal=$.trim(obj.attr('title'));//$.trim(obj.attr('bt-xtitle'));
                                for(var i=0;i<listmemo.length;i++)
                                {
                                    if(datecal==$.trim(listmemo[i]))
                                    {
                                         //分隔设置日期颜色和加粗
                                        obj.attr("style");
                                        if($.trim(listmemo[i])!='<%=DateTime.Now.ToString("M")%>')
                                        {
                                            obj.css("color","Green");
                                        }
                                        obj.css("font-weight","bold");
                                        //分隔设置日期颜色和加粗
                                        
                                        
                                        //分隔设置日期气泡内容
                                        obj.next().eq(0).html(listmemoContent[i]);
                                        //分隔设置日期气泡内容
                                    }
                                } 
                            }
                        })
                        ajaxcomplete=true;
                    }
                }
            },
            error: function() {
                 tableToolbar._showMsg("初始化本月记事数据失败!");
            }
        });
        
        
        $("#<%=CalendarDate.ClientID %>").find("a").unbind("click").bind("click", function(){
            var stardate="";
            if($.trim($(this).attr("title"))=="")
                stardate=$.trim($(this).attr("bt-xtitle"));
            else
                stardate=$.trim($(this).attr("title"));
            Boxy.iframeDialog({
                iframeUrl: '/UserCenter/Memo/AddDataMemo.aspx?stardate='+escape(stardate)+'&sl=0',
                title: '添加记事',
                modal: true,
                width: '673px',
                height: '370px'
            });
            return false;
        });
        if(ajaxcomplete==true)
        {
            $("#<%=CalendarDate.ClientID %>").find("a").each(function(){
                    var $obj=$(this);
                    if($obj.css("font-weight")=="700"&&
                       $.trim($obj.attr("title"))!='<%=DateTime.Now.ToString("M")%>'){
                        var pa=new params();
                        pa.obj = $obj;
                        pa.objresult=$obj.next();
                        AjaxMemorandum.BindBT(pa);
                    }
            });
        }

        
    })
</script>

