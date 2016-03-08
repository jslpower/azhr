<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="BaoJiaGuide.aspx.cs" Inherits="EyouSoft.Web.BaoJiaGuide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox yueli_sider">
        <!--列表开始-->
        <div class="yueli_box">
            <div class="yueli_T">
                <dl id="yueli_select" class="yl_dropdown">
                    <dt><a data-year="2014" href="javascript:;"><span>2014年</span></a></dt>
                    <dd>
                        <ul>
                            <li><a data-year="2014" href="javascript:;" class="flag">2014年</a></li>
                            <li><a data-year="2015" href="javascript:;" class="flag">2015年</a></li>
                            <li><a data-year="2016" href="javascript:;" class="flag">2016年</a></li>
                            <li><a data-year="2017" href="javascript:;" class="flag">2017年</a></li>
                            <li><a data-year="2018" href="javascript:;" class="flag">2018年</a></li>
                            <li><a data-year="2019" href="javascript:;" class="flag">2019年</a></li>
                            <li><a data-year="2020" href="javascript:;" class="flag">2020年</a></li>
                            <li><a data-year="2021" href="javascript:;" class="flag">2021年</a></li>
                        </ul>
                    </dd>
                </dl>
            </div>
            <div class="yueli_list">
                <ul>
                    <li><a data-month="1" href="javascript:;">1月</a></li>
                    <li><a data-month="2" href="javascript:;">2月</a></li>
                    <li><a data-month="3" href="javascript:;">3月</a></li>
                    <li class="marinR0"><a data-month="4" href="javascript:;">4月</a></li>
                    <li><a data-month="5" href="javascript:;">5月</a></li>
                    <li><a data-month="6" href="javascript:;">6月</a></li>
                    <li><a data-month="7" href="javascript:;">7月</a></li>
                    <li class="marinR0"><a data-month="8" href="javascript:;">8月</a></li>
                    <li><a data-month="9" href="javascript:;">9月</a></li>
                    <li><a data-month="10" href="javascript:;">10月</a></li>
                    <li><a data-month="11" href="javascript:;">11月</a></li>
                    <li class="marinR0"><a data-month="12" href="javascript:;">12月</a></li>
                </ul>
            </div>
        </div>
        <!--列表结束-->
        <input type="hidden" value="2014" id="hidyear" />
        <input type="hidden" value="1" id="hidmonth" />
    </div>

    <script type="text/javascript">
        var BaoJiaGuide = {
            sl: '<%=Request.QueryString["sl"] %>',
            BindClick: function() {
                var date = new Date();
                $(".yueli_list").find("a").each(function() {
                    var self = $(this);
                    if (self.attr("data-month") == date.getMonth() + 1) {
                        self.attr("class", "default");
                    }
                })
                $(".yueli_list").find("a").click(function() {
                    var self = $(this);
                    var month = self.attr("data-month");
                    var day = "30";
                    switch (parseInt(month)) {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            day = "31";
                            break;
                        case 2:
                            day = "28";
                            break;
                        default:
                            day = "30";
                            break;
                    }
                    var year = $("#hidyear").val();
                    if (year == "" || month == "") return;
                    var stime = year + "-" + month + "-1";
                    var etime = year + "-" + month + "-" + day;
                    var url = "";
                    if (BaoJiaGuide.sl == "1") {
                        url = "/Quote/BaoJia.aspx?sl=1&txtBeginBuyTime=" + stime + "&txtEndBuyTime=" + etime;//整团报价
                    } else if (BaoJiaGuide.sl == "2") {
                        url = "/Sales/ChanPin.aspx?sl=2&txtBeginDateF=" + stime + "&txtBeginDateS=" + etime; //团队产品
                    } else {
                        url = "/Sales/TJChanPin.aspx?sl=3&txtBeginDateF=" + stime + "&txtBeginDateS=" + etime;//自由行
                    }
                    location.href = url;
                    return false;
                })
            },
            InitYear: function() {
                $(".yl_dropdown img.flag").addClass("flagvisibility");

                $(".yl_dropdown dt a").click(function() {
                    $(".yl_dropdown dd ul").toggle();
                });

                $(".yl_dropdown dd ul li a").click(function() {
                    var text = $(this).html();
                    $(".yl_dropdown dt a span").html(text);
                    $(".yl_dropdown dd ul").hide();
                    $("#hidyear").val($(this).attr("data-year"));
                    $("#result").html("Selected value is: " + getSelectedValue("yueli_select"));
                });

                function getSelectedValue(id) {
                    return $("#" + id).find("dt a span.value").html();
                }

                $(document).bind('click', function(e) {
                    var $clicked = $(e.target);
                    if (!$clicked.parents().hasClass("yl_dropdown"))
                        $(".yl_dropdown dd ul").hide();
                });


                $("#flagSwitcher").click(function() {
                    $(".yl_dropdown a.flag").toggleClass("flagvisibility");
                });
            }
        }
        $(function() {
            BaoJiaGuide.InitYear();
            BaoJiaGuide.BindClick();
        });
    </script>

</asp:Content>
