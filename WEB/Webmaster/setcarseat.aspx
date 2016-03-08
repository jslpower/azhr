<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setcarseat.aspx.cs" Inherits="EyouSoft.Web.Webmaster.setcarseat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <asp:PlaceHolder runat="server" ID="ph_BusTypeList"></asp:PlaceHolder>
        <div class="zuoweihao_box">
            <div class="hr_10">
            </div>
            <div id="zuoweibox_list">
                <div class="subdiv">
                    <div class="zuoweibox">
                        <div class="zuowei_list">
                            <asp:Repeater runat="server" ID="rpt_busBoxList">
                                <ItemTemplate>
                                    <div class='movediv' style="left: <%#Eval("PointX")%>px; top: <%# Eval("PoinY")%>px;">
                                        <a class='graybtn'>
                                            <%#Eval("SeatNumber")%></a></div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%=strSeat%>
                        </div>
                        <input id="tempId" type="hidden" value="<%=tempID %>" />
                    </div>
                </div>
            </div>
        </div>
        <div class="alertbox-btn" style="bottom:auto";>
            <a href="javascript:void(0);" id="a_save" hidefocus="true"><s class="baochun"></s>保
                存</a>
        </div>
    </div>

    <script type="text/javascript">
        (function($) {
            $.fn.ppdrag = function(options) {
                if (typeof options == 'string') {
                    if (options == 'destroy') return this.each(function() {
                        $.ppdrag.removeEvent(this, 'mousedown', $.ppdrag.start, false);
                        $.data(this, 'pp-ppdrag', null);
                    });
                } else {
                    $.extend(this._options, options);
                }
                return this.each(function() {
                    $.data(this, 'pp-ppdrag', { options: $.extend({}, options) });
                    $.ppdrag.addEvent(this, 'mousedown', $.ppdrag.start, false);
                });

                this._options = {
                    x: true,
                    y: true
                }

            };

            $.ppdrag = {
                start: function(event) {
                    if (!$.ppdrag.current) {
                        $.ppdrag.current = {
                            el: this,
                            oleft: parseInt(this.style.left) || 0,
                            otop: parseInt(this.style.top) || 0,
                            ox: event.pageX || event.screenX,
                            oy: event.pageY || event.screenY
                        };
                        var current = $.ppdrag.current;
                        var data = $.data(current.el, 'pp-ppdrag');
                        if (data.options.zIndex) {
                            current.zIndex = current.el.style.zIndex;
                            current.el.style.zIndex = data.options.zIndex;
                        }
                        $.ppdrag.addEvent(document, 'mouseup', $.ppdrag.stop, true);
                        $.ppdrag.addEvent(document, 'mousemove', $.ppdrag.drag, true);
                    }
                    if (event.stopPropagation) event.stopPropagation();
                    if (event.preventDefault) event.preventDefault();
                    return false;
                },

                drag: function(event) {
                    if (!event) var event = window.event;
                    var current = $.ppdrag.current;
                    current.el.style.left = (current.oleft + (event.pageX || event.screenX) - current.ox) + 'px';
                    current.el.style.top = (current.otop + (event.pageY || event.screenY) - current.oy) + 'px';
                    if (event.stopPropagation) event.stopPropagation();
                    if (event.preventDefault) event.preventDefault();
                    return false;
                },

                stop: function(event) {
                    var current = $.ppdrag.current;
                    var data = $.data(current.el, 'pp-ppdrag');
                    $.ppdrag.removeEvent(document, 'mousemove', $.ppdrag.drag, true);
                    $.ppdrag.removeEvent(document, 'mouseup', $.ppdrag.stop, true);
                    if (data.options.zIndex) {
                        current.el.style.zIndex = current.zIndex;
                    }
                    if (data.options.stop) {
                        data.options.stop.apply(current.el);
                    }
                    $.ppdrag.current = null;
                    if (event.stopPropagation) event.stopPropagation();
                    if (event.preventDefault) event.preventDefault();
                    return false;
                },

                addEvent: function(obj, type, fn, mode) {
                    if (obj.addEventListener)
                        obj.addEventListener(type, fn, mode);
                    else if (obj.attachEvent) {
                        obj["e" + type + fn] = fn;
                        obj[type + fn] = function() { obj["e" + type + fn](window.event); }
                        obj.attachEvent("on" + type, obj[type + fn]);
                    }
                },

                removeEvent: function(obj, type, fn, mode) {
                    if (obj.removeEventListener)
                        obj.removeEventListener(type, fn, mode);
                    else if (obj.detachEvent) {
                        obj.detachEvent("on" + type, obj[type + fn]);
                        obj[type + fn] = null;
                        obj["e" + type + fn] = null;
                    }
                }

            };

        })(jQuery);

    </script>

    <script type="text/javascript">

        $(function() {
            $(".movediv").ppdrag();
            $("#a_save").click(function() {
                var data = { data: "", tempId: "" };
                var list = [];
                data.tempId = $("#tempId").val();
                $(".movediv").each(function() {
                    var _self = $(this);
                    list.push("{SeatNumber:" + parseInt(_self.find("a").html()) + ",PointX:" + parseInt(_self.css("left")) + ",PoinY:" + parseInt(_self.css("top")) + "}");
                })
                data.data = "[" + list.join(',') + "]";
                $.ajax({
                    url: "/Webmaster/setcarseat.aspx?type=post",
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function(result) {
                    alert("操作成功！")
                    },
                    error: function() {
                    alert("系统繁忙！")

                    }
                })
            })
        })

    </script>

</body>
</html>
