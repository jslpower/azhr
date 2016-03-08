<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetSeat.aspx.cs" Inherits="EyouSoft.WebFX.CommonPage.SetSeat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Js/jquery-1.4.4.js"></script>

    <script src="../Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="../Js/table-toolbar.js" type="text/javascript"></script>

    <style type="text/css">
        #zuoweibox_list
        {
            width: 937px;
            overflow: hidden;
            position: relative;
            height: 320px;
        }
        .subdiv
        {
            width: 10000px;
            position: absolute;
            display: inherit;
            height: 320px;
        }
        .zuoweibox
        {
            float: left;
            display: inline;
            margin: 0px 10px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <asp:PlaceHolder runat="server" ID="ph_BusTypeList">
            <div class="zuoweifp_box" id="zuoweifp_box">
                <ul>
                    <asp:Repeater runat="server" ID="rpt_BusTypeList">
                        <ItemTemplate>
                            <li><a href="javascript:void(0);" class='<%#Container.ItemIndex==0?"select":"" %>'
                                data-class="busType" data-id='<%#Container.ItemIndex %>' data-busid='<%#Eval("TourCarTypeId") %>'
                                title='<%#Eval("Desc") %>'><span><strong>
                                    <%#Eval("CarTypeName")%></strong>(<%#Eval("SeatNum")%>座)</span></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="clear">
                </div>
            </div>
        </asp:PlaceHolder>
        <div class="zuoweihao_box">
            <asp:PlaceHolder runat="server" ID="ph_Auto">
                <div class="massage fixed">
                    <span style="float: left;"><a class="btn" id="btn_Auto" href="javascript:void(0);"
                        data-orderid='<%=Request.QueryString["orderId"] %>'>自动排位</a></span> <span style="padding-left: 10px;
                            color: Red; float: left;">未安排座位人数:
                            <label id="lb_msg">
                                <%=Request.QueryString["peoNum"] %>
                            </label>
                            人</span>
                    <div class="rightmsg">
                        <ul>
                            <li><s class="yixuan"></s>已选座位</li>
                            <li><s class="dangqian"></s>当前选坐</li>
                            <li><s class="weixuan"></s>可选座位</li>
                        </ul>
                    </div>
                </div>
            </asp:PlaceHolder>
            <div class="hr_10">
            </div>
            <div id="zuoweibox_list">
                <div class="subdiv">
                    <asp:Repeater runat="server" ID="rpt_busBoxList">
                        <ItemTemplate>
                            <div class="zuoweibox" data-id='<%#Container.ItemIndex %>' data-busid='<%#Eval("TourCarTypeId") %>'>
                                <div class="zuowei_list">
                                </div>
                                <input type="hidden" name="hid_Seat" value='<%#GetJsonSeat(Eval("SysCarTypeSeatList")) %>' />
                                <input type="hidden" name="hid_Seated" value='<%#GetJsonSeated(Eval("TourOrderCarTypeSeatList"))%>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_SaveBtn"><a href="javascript:void(0);" id="a_save"
                hidefocus="true"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();"
                hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var SetSeatObj = {
            oldPeoNum: '<%=Request.QueryString["oldPeoNum"]%>',
            peoNum: '<%=Request.QueryString["peoNum"]%>',
            seatDatastr: "", //座位数据
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetSeatData: function() {//设置座位数据的值
                //获取所有当前选中的座位信息
                ///{ chexing1: [{ "SeatNumber": "1", "PointX": 53, "PoinY": 30}], chexing2: [{ "SeatNumber": "1", "PointX": 53, "PoinY": 30}]}///
                //然后就是把A变成一个字符串保存到隐藏于里边
                var jsonstr = "", busId = '', i = 0, seatArr = [], orderId = $("#btn_Auto").attr("data-orderid"); //seatArr用于跟底层对接
                jsonstr += "{";
                $("#zuoweibox_list .zuoweibox").each(function() {
                    var self = $(this), arr = [];
                    busId = self.attr("data-busid");
                    jsonstr += "\"busId" + i + "\":\"" + busId + "\",";
                    jsonstr += "\"chexing" + i + "\":[";
                    i++;
                    self.find('a[class="yellowbtn"]').each(function() {
                        var temp = $(this);
                        //var movediv = temp.closest("div[class='movediv']");
                        var NXY = "{ \"SeatNumber\": \"" + temp.text() + "\"}";
                        //  , \"PointX\": " + movediv.css("left").replace("px", "") + ", \"PoinY\": " + movediv.css("top").replace("px", "") + " 已选座位不需要XY坐标
                        arr.push(NXY);
                        seatArr.push("{ \"SeatNumber\": \"" + temp.text() + "\" , \"TourCarTypeId\": \"" + busId + "\", \"OrderId\":\"" + orderId + "\"}");
                    })
                    jsonstr += (arr.join(','));
                    jsonstr += "],";
                });
                jsonstr += "\"data\":[]"; //已被占用座位编号
                jsonstr += "}";
                this.seatDatastr = "[" + seatArr.join(',') + "]";
                SetSeatObj.parentWindow["seatData"] = $.parseJSON(jsonstr);
                //alert(jsonstr);
            },
            InitSeatData: function() {//加载全部车型列表机座位分布状况
                $("#zuoweibox_list .zuoweibox input[type='hidden'][name='hid_Seat']").each(function() {            //车型列表容器div
                    if ($.trim($(this).val()) != "") {
                        var jsonData = $.parseJSON("{\"data\":" + $(this).val() + "}"); //需要加载座位分布的数据
                        if (jsonData) {
                            var str = '';
                            for (var i = 0; i < jsonData.data.length; i++) {
                                str += '<div class="movediv" style="left:' + jsonData.data[i].PointX + 'px;top:' +
                         jsonData.data[i].PoinY + 'px"><a href="javascript:void(0);" class="graybtn" >' + jsonData.data[i].SeatNumber + '</a></div>';
                            }
                            $(this).closest("div[class='zuoweibox']").find("div[class='zuowei_list']").html(str);
                        }
                        jsonData = null;
                        str = null;
                    }
                })
            },
            InitSeatedData: function() {
                $("#zuoweibox_list .zuoweibox").each(function() {
                    var self = $(this), //当前选中车型
                    id = self.attr("data-id"),                //当前选中车型的index
                    parentdiv = $("#zuoweibox_list"),
                    orderId = $("#btn_Auto").attr("data-orderid");
                    var jsonData = SetSeatObj.parentWindow["seatData"]; //父页面已选座位数据

                    var tempval = parentdiv.find(".zuoweibox[data-id=" + id + "] input[type='hidden'][name='hid_Seated']");
                    if (tempval.val()) {
                        var jsonDataSeated = $.parseJSON("{\"data\":" + tempval.val() + "}"); //已选座位分布
                        if (jsonDataSeated) {
                            var busId = jsonDataSeated.data[0].TourCarTypeId; //当前json数据集合里的车型编号都是一样的
                            for (var j = 0; j < jsonDataSeated.data.length; j++) {
                                parentdiv.find(".zuoweibox[data-busid=" + busId + "] a").each(function() {
                                    if (jsonDataSeated.data[j].OrderId == orderId && $(this).text() == jsonDataSeated.data[j].SeatNumber) {
                                        if (tableToolbar.getInt(SetSeatObj.oldPeoNum) <= tableToolbar.getInt(SetSeatObj.peoNum)) {
                                            $(this).removeClass().addClass("yellowbtn");
                                            //$("#lb_msg").text((tableToolbar.getInt($("#lb_msg").text()) - 1));
                                        }
                                    }
                                    else if (jsonDataSeated.data[j].OrderId != orderId && $(this).text() &&
                                         $(this).text() == jsonDataSeated.data[j].SeatNumber) {
                                        $(this).removeClass().addClass("bluebtn");
                                    }

                                })
                            }

                        }
                        //这边需要把底层给的数据跟父页面的json对比，然后去掉父页面没有的当前选坐
                        var arrjsondata = [], arrjsonseated = [];
                        if (jsonData && jsonDataSeated) {
                            for (var i = 0; i < jsonDataSeated.data.length; i++) {
                                if (jsonDataSeated.data[i].OrderId == orderId) {
                                    arrjsonseated.push(jsonDataSeated.data[i].SeatNumber);
                                }
                            }
                            for (var j = 0; j < jsonData["chexing" + id].length; j++) {
                                arrjsondata.push(jsonData["chexing" + id][j].SeatNumber);
                            }
                            for (var k = 0; k < arrjsonseated.length; k++) {
                                for (var l = 0; l < arrjsondata.length; l++) {
                                    if (arrjsonseated[k] == arrjsondata[l]) {
                                        arrjsonseated.splice(k, 1);
                                    }
                                }
                            }
                            for (var m = 0; m < arrjsonseated.length; m++) {
                                parentdiv.find(".zuoweibox[data-id=" + id + "] .movediv a").each(function() {
                                    if ($(this).text() == arrjsonseated[m]) {
                                        $(this).removeClass().addClass("graybtn");
                                    }
                                })
                            }
                        }
                    }

                    if (jsonData && jsonData["chexing" + id]) {//判断父页面是否有数据,然后加载出来黄色按钮的数据
                        var jsonobj = jsonData["chexing" + id];
                        for (var i = 0; i < jsonobj.length; i++) {
                            $(".zuoweibox[data-id='" + id + "'] .zuowei_list .movediv a").each(function() {
                                if ($(this).text() == jsonobj[i].SeatNumber) {
                                    if (tableToolbar.getInt(SetSeatObj.oldPeoNum) <= tableToolbar.getInt(SetSeatObj.peoNum)) {
                                        $(this).removeClass().addClass("yellowbtn");
                                        //$("#lb_msg").text((tableToolbar.getInt($("#lb_msg").text()) - 1));
                                    }
                                }
                            })
                        }
                        //$("#lb_msg").text((tableToolbar.getInt($("#lb_msg").text()) - jsonobj.length));
                    }
                })
                //加载红色座位
                var redSeat = '<%=Request.QueryString["seatedData"]%>';
                if (redSeat) {
                    var redSeatJson = $.parseJSON(redSeat);
                    if (redSeatJson) {
                        for (var i = 0; i < redSeatJson.selectedSeat.length; i++) {
                            $(".zuoweibox[data-busid='" + redSeatJson.selectedSeat[i].TourCarTypeId + "'] .zuowei_list .movediv a").each(function() {
                                if ($(this).text() == redSeatJson.selectedSeat[i].SeatNumber) {
                                    $(this).removeClass().addClass("redbtn");
                                }
                            })
                        }
                    }
                }
                $("#lb_msg").text(tableToolbar.getInt(SetSeatObj.peoNum) - $("#zuoweibox_list .zuoweibox").find("a[class='yellowbtn']").length);
            },
            bindFun: function() {
                setTimeout(function() {
                    $("#zuoweifp_box").find("ul li a[data-class='busType']").each(function() {
                        $(this).bind("cilck");
                    })
                }, 500);
            },
            unbindFun: function() {
                $("#zuoweifp_box").find("ul li a[data-class='busType']").each(function() {
                    $(this).unbind("cilck");
                })
            },
            scollFun: function(obj) {
                //加载数据
                SetSeatObj.unbindFun();
                $("#zuoweifp_box").find("ul li a[data-class='busType']").removeClass();
                var self = $(obj);
                self.addClass("select");
                var temp = self.attr("data-id");
                $(".subdiv").animate({ left: -947 * temp + "px" }, 500, SetSeatObj.bindFun);
            },
            bindBtn: function() {
                $("#zuoweifp_box").find("ul li a[data-class='busType']").click(function() {
                    SetSeatObj.scollFun(this);
                    return false;
                });
                $("#a_save").click(function() {
                    if (tableToolbar.getInt($("#lb_msg").text()) > 0) {
                        parent.tableToolbar.ShowConfirmMsg("还有" + $("#lb_msg").text() + "人没有安排座位,确定要保存吗?", function() {
                            SetSeatObj.SetSeatData();
                            var data = { seatData: SetSeatObj.seatDatastr };
                            var func = '<%=Request.QueryString["callBackFun"] %>';
                            if (func.indexOf('.') == -1) {
                                SetSeatObj.parentWindow[func](data);
                            } else {
                                SetSeatObj.parentWindow[func.split('.')[0]][func.split('.')[1]](data);
                            }
                            parent.Boxy.getIframeDialog(SetSeatObj.iframeID).hide();
                        });
                    }
                    else if(tableToolbar.getInt($("#lb_msg").text()) < 0){
                        parent.tableToolbar._showMsg("选取的座位数不能大于预定人数!");
                        return false;
                    }
                    else {
                        SetSeatObj.SetSeatData();
                        var data = { seatData: SetSeatObj.seatDatastr };
                        var func = '<%=Request.QueryString["callBackFun"] %>';
                        if (func.indexOf('.') == -1) {
                            SetSeatObj.parentWindow[func](data);
                        } else {
                            SetSeatObj.parentWindow[func.split('.')[0]][func.split('.')[1]](data);
                        }
                        parent.Boxy.getIframeDialog(SetSeatObj.iframeID).hide();
                    }
                })
            }
        }

        $(function() {
            //获得需要赋值页面的window 对象
            if (SetSeatObj.pIframeID) {
                SetSeatObj.parentWindow = window.parent.Boxy.getIframeWindow(SetSeatObj.pIframeID) || window.parent.Boxy.getIframeWindowByID(SetSeatObj.pIframeID);
            }
            else {
                SetSeatObj.parentWindow = parent.window;
            }
            SetSeatObj.bindBtn();
            SetSeatObj.InitSeatData();
            SetSeatObj.InitSeatedData();
            //点选座位
            var tourCarTypeId = '<%=Request.QueryString["tourCarTypeId"] %>';
            if ($.trim(tourCarTypeId) == "") {
                $("#zuoweibox_list .movediv a").click(function() {
                    var self = $(this);
                    var temp = $("#lb_msg");
                    if (tableToolbar.getInt(temp.text()) > 0) {
                        if (self.attr("class") == "graybtn") {
                            self.removeClass().addClass("yellowbtn");
                            temp.text((tableToolbar.getInt(temp.text()) - 1));
                            return false;
                        }
                    }
                    if (self.attr("class") == "yellowbtn") {
                        self.removeClass().addClass("graybtn");
                        var temp = $("#lb_msg");
                        temp.text((tableToolbar.getInt(temp.text()) + 1));
                        return false;
                    }

                })
            }
            //自动跳转到有空位的车型
            $("#zuoweifp_box a[data-class='busType']").each(function() {
                if ($(this).attr("data-id") == $(".yellowbtn").first().closest("div[class='zuoweibox']").attr("data-id")) {
                    $(this).click();
                }
                else {
                    if ($(this).attr("data-id") == $(".graybtn").first().closest("div[class='zuoweibox']").attr("data-id")) {
                        $(this).click();
                    }
                }
            })

            //自动排座
            $("#btn_Auto").click(function() {
                var nullSeat = $(".zuowei_list .movediv").find("a[class='graybtn']");
                if (nullSeat.length == 0) {
                    tableToolbar._showMsg("座位已排满，请继续添加车型！");
                    return false
                }
                var peonum = tableToolbar.getInt($("#lb_msg").text()); //要分配座位的人数
                if (tableToolbar.getInt(peonum) && tableToolbar.getInt(peonum) == 0) {
                    tableToolbar._showMsg("自动排座已完成！");
                    return false;
                }
                var selectObj = $("#zuoweifp_box ul li .select").first(), //当前选中的车型
                id = selectObj.attr("data-id");
                if (nullSeat.length >= peonum) {
                    for (var i = 0; i < peonum; i++) {
                        var obj = $(nullSeat[i]);
                        var tempid = obj.closest("div[class='zuoweibox']").attr("data-id");
                        if (tempid != id) {
                            $("#zuoweifp_box").find("ul li a[data-class='busType']").each(function() {
                                if ($(this).attr("data-id") == tempid) {
                                    $(this).click()
                                }
                            })
                        }
                        obj.removeClass().addClass("yellowbtn");
                        if (i == (peonum - 1)) {
                            //alert(peonum);
                            $("#lb_msg").text("0");
                            tableToolbar._showMsg("自动排座已完成！")
                        }
                    }
                }
                else {
                    for (var i = 0; i < nullSeat.length; i++) {
                        var obj = $(nullSeat[i]);
                        var tempid = obj.closest("div[class='zuoweibox']").attr("data-id");
                        if (tempid != id) {
                            $("#zuoweifp_box").find("ul li a[data-class='busType']").each(function() {
                                if ($(this).attr("data-id") == tempid) {
                                    $(this).click()
                                }
                            })
                        }
                        obj.removeClass().addClass("yellowbtn");
                        if (i == (nullSeat.length - 1)) {
                            $("#lb_msg").text(peonum - (nullSeat.length));
                            tableToolbar._showMsg("自动排座已完成,还有" + (peonum - nullSeat.length) + "个人没有安排座位！")
                        }
                    }
                }
            })


        })
    </script>

</body>
</html>
