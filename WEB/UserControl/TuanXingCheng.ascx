﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TuanXingCheng.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.TuanXingCheng" %>

<script type="text/javascript">
    var Journey = {
        //创建编辑器
        CreateEdit: function(obj) {
            var _self = $(obj);
            if ($.trim(_self.attr("id")).length == 0) _self.attr("id", "txtRemark" + parseInt(Math.random() * 1000));
            KEditer.remove(_self.attr("id"));
            KEditer.init(_self.attr("id"), { resizeMode: 0, items: keSimple, height: "200px", width: "680px" });
        },
        //单选 用餐
        SelectOneForEat: function(obj) {
            var a = true;
            var td = $(obj).closest("td");
            switch (obj.value) {
                case "1":
                    td.find("input[name='eatFrist']").val(obj.checked ? "1" : "");
                    td.find("input[name='txtbreakprice']").attr("eatfrist", obj.checked ? "yes" : "no");
                    break;
                case "2":
                    td.find("input[name='eatSecond']").val(obj.checked ? "1" : "");
                    td.find("input[name='txtsecondprice']").attr("eatsecond", obj.checked ? "yes" : "no");
                    break;
                case "3":
                    td.find("input[name='eatThird']").val(obj.checked ? "1" : "");
                    td.find("input[name='txtthirdprice']").attr("eatthird", obj.checked ? "yes" : "no");
                    break;
            }
            td.find("input[type='checkbox']").each(function() {
                if (this.value != "0" && !this.checked) {
                    a = false;
                }
            })
            $(obj).closest("td").find("input[type='checkbox'][value='0']").attr("checked", a ? "checked" : "");
            $(obj).closest("td").find("input[class='pricejs']").attr("Eatpricejs", a ? "yes" : "no");
            AddPrice.SumMenuPrice();
            AddPrice.SetTotalPrice();
        },
        //选用景点
        SelectSince: function(obj) {
            $(obj).attr("id", "a_Journey_" + parseInt(Math.random() * 10000));
            var cityids = [];
        	$(obj).closest("tbody").find("input[name='hidcityid']").each(function() {
        		var self = $(this).val();
        		if (self!="") {
        			cityids.push(self);
        		}
        	});
        	if (cityids.length==0) { alert("请先选择城市!"); return false; }
            var binkemode = $("#hidbinkemode").val();
            var tourmode = $("#hidtourmode").val();
            var sdate = $("#hidsdate").val();
            var edate = $("#hidedate").val();
            var scenicIds = $(obj).closest("td").find("input[name='hd_scenery_spot']").val();
            var lgtype = $("#hidLngType").val();
            var param = { binkemode: binkemode, tourmode: tourmode, sdate: sdate, edate: edate, cityids: cityids.join(','), scenicids: scenicIds, LgType: lgtype };

            Boxy.iframeDialog({
                iframeUrl: $(obj).attr("href") + "&aid=" + $(obj).attr("id") + "&" + $.param(param),
                title: $(obj).attr("title"),
                modal: true,
                width: "948px",
                height: "412px"
            });
        },
        CreatePlanShop: function(that, Shop) {//选择城市后创建购物点
            if (that != null) {
                if (Shop != null) {
                    var temp = '<span id="' + Shop.GysId + '" class="planshopspan"><input type="checkbox" onclick="Journey.SetShopValue(this)" name="ckgouwuid" data-name="' + Shop.GysName + '" value="' + Shop.GysId + '" />' + Shop.GysName + '</span>';
                    that.closest("tbody[class='tempRow']").find("td[class='gouwutd']").append(temp);
                }
            }
            else {
                if (Shop != null) {
                    var xcgouwuids = new Array(); //行程中勾选的购物店
                    var checked = "";
                    $(".gouwutd").find("input[type='checkbox']:checked").each(function() {
                        xcgouwuids.push($(this).val());
                    });
                    if ($.inArray(Shop.GysId, xcgouwuids) != -1) {
                        checked = "checked";
                    }
                    var temp = '<span id="' + Shop.GysId + '" class="planshopspan"><input type="checkbox" ' + checked + ' onclick="Journey.SetShopValue(this)" data-liushui="'+Shop.LiuShui+'" data-peoplemoney="'+Shop.PeopleMoney+'" data-childmoney="'+Shop.ChildMoney+'" data-name="' + Shop.GysName + '" value="' + Shop.GysId + '" />' + Shop.GysName + '(' + tableToolbar.calculate(Shop.PeopleMoney,Shop.LiuShui,'+') + ')</span>';
                    var shopCount = $(".tdshop").find("span[class='planshopspan']").length;
                    if (shopCount % 5 == 0 && shopCount > 0) {
                        temp += "<br />";
                    }
                    $(".tdshop").append(temp);
                }
            }
        },
        //选择景点以后创建
        CreateSince: function(jingDianXuanYongId, jingDian, num) {
            var temp = '<span id="' + jingDian.id + '" data-istuijian="' + jingDian.istuijian + '" data-fujian="' + jingDian.fujian + '" class="upload_filename">' +
            '<a  data-class="a_Journey_Since" data-pricejs="' + jingDian.jiagejs + '" data-priceth="' + jingDian.jiageth + '" data-path="' + jingDian.filepath + '" data-for="' + jingDian.id + '">' + jingDian.name + '</a>' +
            '<a data-for="' + jingDian.id + '" href="javascript:void(0);" onclick="Journey.RemoveSince(this)"><img src="/images/cha.gif"></a></span>';

            $("#" + jingDianXuanYongId).parent().find("span[data-class='fontblue']").append(temp);
            this.SetSinveValue($("#" + jingDianXuanYongId).closest("td"));

            //将景点名称写入行程内容 【A景点】：描述
            var _$tr = $("#" + jingDianXuanYongId).closest("tr").next();
            var $td = $("#" + jingDianXuanYongId).closest("td");
            var strpricejs = "";
            var strpriceth = "";
            $("#" + jingDian.id).find("a[data-class='a_Journey_Since']").attr("data-pricejs", jingDian.jiagejs);
            $("#" + jingDian.id).find("a[data-class='a_Journey_Since']").attr("data-priceth", jingDian.jiageth);
            Journey.SumPriceJingDian();
            AddPrice.SetTotalPrice();
            AddPrice.SumHeJiPrice();
            var _$textarea = _$tr.find("textarea[name='txtContent']");
            var _textareaid = _$textarea.attr("id");
            if (KEditer.isInit(_textareaid)) KEditer.sync(_textareaid);
            else this.CreateEdit(_$textarea);
            var _html = _$textarea.val();
            _html += '<b style="color:#ff0000">【' + jingDian.name + '】</b>：' + jingDian.desc + "<br/>";
            if (num == 0) {
                if ($(".imgtd").find("img").length > 0) {
                    $(".imgtd").find("img").attr("src", jingDian.filepath);
                } else {
                    $(".imgtd").append("<img width='150' height='120' src='" + jingDian.filepath + "' />");
                }
            }
            $(".imgtd").find("input[type='hidden']").val(jingDian.filepath);
            _$textarea.val(_html);
            KEditer.html(_$textarea.attr("id"), _html);
        },
        //删除景点
        RemoveSince: function(args) {
            var td = $(args).closest("td");
            var aid = $(args).closest("td").find(".xuanyong").attr("id");
            $(args).closest("span[class='upload_filename']").remove();
            this.SetSinveValue(td);
            Journey.SumPriceJingDian();
            AddPrice.SetTotalPrice();
            Journey.SetScenicPic(aid);
        },
        //删除和添加购物后修改表单(选中/反选)
        SetShopValue: function(obj) {
            var shopidArr = new Array();
            var shopnameArr = new Array();
        	var totalliushui = 0.00;//购物店流水
        	var totalpeoplemoney = 0.00;//购物店成人人头
        	var totalchildmoney = 0.00;//购物店儿童人头
            $(obj).closest("td").find("input[type='checkbox']:checked").each(function() {
                shopidArr.push($(this).val());
                shopnameArr.push($(this).attr("data-name"));
        		totalliushui = tableToolbar.calculate(tableToolbar.getFloat(totalliushui), tableToolbar.getFloat($(this).attr("data-liushui")), "+");
        		totalpeoplemoney = tableToolbar.calculate(tableToolbar.getFloat(totalpeoplemoney), tableToolbar.getFloat($(this).attr("data-peoplemoney")), "+");
        		totalchildmoney = tableToolbar.calculate(tableToolbar.getFloat(totalchildmoney), tableToolbar.getFloat($(this).attr("data-childmoney")), "+");
            })
        	//成人价-购物流水-成人人头
        	$("#Tab_Item_Price,#Tab_Price_Price,#Tab_Group_Price").find("input[name='txt_Item_AdultPrice'],input[name='txt_Price_AdultPrice'],input[name='txt_zengtuan_AdultPrice']").each(function() {
        		$(this).val(tableToolbar.calculate(tableToolbar.getFloat($(this).attr("data-ordinal")), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"));
        	});
        	//儿童价-购物流水-儿童人头
//        	$("#Tab_Item_Price,#Tab_Price_Price,#Tab_Group_Price").find("input[name='txt_Item_ChildPrice'],input[name='txt_Price_ChildPrice'],input[name='txt_zengtuan_ChildPrice']").each(function() {
//        		$(this).val(tableToolbar.calculate(tableToolbar.getFloat($(this).attr("data-ordinal")), tableToolbar.calculate(totalliushui, totalchildmoney, "+"), "-"));
//        	});
        	//合计金额
        	AddPrice.SumHeJiPrice();
            //$(obj).closest("td").find("input[class='shopid']").val(shopidArr.join(','));
            //$(obj).closest("td").find("input[class='shopname']").val(shopnameArr.join(','));

        },
        //删除和添加 景点后修改表单
        SetSinveValue: function(td) {
            var hideArray = new Array();
            var showArray = new Array();
            var filepath = new Array();
            var pricejs = new Array();
            var priceth = new Array();
            td.closest("td").find("a[data-class='a_Journey_Since']").each(function() {
                hideArray.push($(this).attr("data-for"));
                showArray.push(encodeURIComponent($.trim($(this).html())));
                filepath.push($(this).attr("data-path"));
                pricejs.push($(this).attr("data-pricejs"));
                priceth.push($(this).attr("data-priceth"));
            })
            td.find("input[name='hidpriceth']").val(priceth.join(','));
            td.find("input[name='hidpricejs']").val(pricejs.join(','));
            td.find("input[name='hd_scenery_spot']").val(hideArray.join(','));
            td.find("input[name='show_scenery_spot']").val(showArray.join(','));
            td.find("input[name='filepath']").val(filepath.join(','));

        },
        AddRowCallBack: function(tr) {
            var $tr = tr;
            $tr.find("span[data-class='fontblue']").html("");
            tr.find("textarea[name='txtContent']").show();
            tr.find("input[name='txtcity']").unbind();
            var lgtype = '<%=Request.QueryString["LgType"] %>';
            tr.find("input[name='txtcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID+"&LgType=" + lgtype, {
                width: 130,
                selectFirst: 'true',
                blur: 'true'
            }).result(function(e, data) {
                $(this).prev("input[type='hidden']").val(data[1]);
                $(this).attr("data-old", data[0]);
                $(this).attr("data-oldvalue", data[1]);
                Journey.AjaxGetShop($(this));
            });
        },
        DelRowCallBack: function(tr) {
            Journey.SumPriceJingDian();
            AddPrice.SetHotelPrice();
            AddPrice.SetTrafficPrice();
            AddPrice.SumMenuPrice();
            AddPrice.SetTotalPrice();
        },
        StartFun: function(tr) {//删除行程 要把行程中存在的购物店,风味餐 移除
            $(tr).find("input[type='checkbox'][name='ckgouwuid']").each(function() {//购物店
                var self = $(this);
                $("#TabList_Box .tdshop").find("input[type='checkbox']").each(function() {
                    if ($.trim($(this).val()) == $.trim(self.val())) {
                        $(this).removeAttr("checked");
                        Journey.SetShopValue(this);
                        $(this).closest("span[class='planshopspan'],br").remove();
                    }
                })
            })
            $(tr).find("input[class='footid']").each(function() {//风味餐
                var self = $(this);
                if (self.val() != "") {
                    $("#TabFengWeiCan").find("input[name='hidfootid']").each(function() {
                        if ($.trim(self.val()) == $.trim($(this).val())) {
                            $(this).closest("tr").remove();
                        }
                    })
                }
            })
        },
        MoveRowCallBack: function(tr) {
            var txt = tr.find("textarea[name='txtContent']");
            KEditer.sync(txt.attr("id"));
            Journey.CreateEdit(txt);
        }, //行程中选择风味餐后自动添加一条风味餐信息
        AutoFengWeiCan: function(caidanid, id, caidanname, isdel, pricejs, priceth, ramNum, type) {
            var isHas = false; //是新增还是修改(true 是修改，false 新增)
            if (type == "0") {
                $("#TabFengWeiCan").find("input[name='hidfootid']").each(function() {
                    var self = $(this);
                    if (self.val() == ramNum) {
                        self.closest("tr").find("input[name='hid_fpricejs']").val(pricejs);
                        self.closest("tr").find("input[name='hid_fcaidanid']").val(caidanid);
                        self.closest("tr").find("input[name='hid_fcanguanid']").val(id);
                        self.closest("tr").find("input[name='txtfcaidanname']").val(caidanname);
                        self.closest("td").next("td").find("input").val(priceth);
                        isHas = true;
                    }
                })
            }
            if (!isHas) {
                var _$html = "";
                _$html = "<tr class='tempRowfwei'>";
                if (isdel) {
                    _$html += "<td align='center'><input type='hidden' name='hidfootid' value='' /><input type='hidden' class='pricejs' value='" + pricejs + "' name='hid_fpricejs' /><input type='hidden' name='hid_fcaidanid' class='canid' value='" + caidanid + "' /><input type='hidden' name='hid_fcanguanid' value='" + id + "' class='menuid' /><input type='text' class='formsize120 inputtext' name='txtfcaidanname' readonly='readonly' style='background-color: #dadada' value='" + caidanname + "' /><a href='javascript:;' class='xuanyong fengweican'></a></td>";
                }
                else {
                    _$html += "<td align='center'><input type='hidden' name='hidfootid' value='" + ramNum + "' /><input type='hidden' value='" + pricejs + "' name='hid_fpricejs' /><input type='hidden' name='hid_fcaidanid' class='canid' value='" + caidanid + "' /><input type='hidden' name='hid_fcanguanid' value='" + id + "' class='menuid' /><input type='text' class='formsize120 inputtext' name='txtfcaidanname' readonly='readonly' style='background-color: #dadada' value='" + caidanname + "' /><a href='javascript:;' class='xuanyong'></a></td>";
                }
                if (isdel)
                    _$html += "<td align='center'><input type='text' class='formsize50 inputtext' data-name='txtfweiprice' name='txtfprice' value='" + priceth + "'></td>";
                else
                    _$html += "<td align='center'><input type='text' class='formsize50 inputtext' readonly='readonly' style='background-color:#dadada' name='txtfprice' value='" + priceth + "'></td>";
                _$html += "<td align='center'><input type='text' class='formsize140 inputtext' name='txtfremark' value=''></td>";
                _$html += "<td align='center'><a href='javascript:void(0)' class='addbtnfwei'><img width='48' height='20' src='../images/addimg.gif'></a>";
                if (isdel)
                    _$html += "<a href='javascript:void(0)' class='delbtnfwei'><img width='48' height='20' src='../images/delimg.gif'></a>";
                _$html += "</td>";
                _$html += "</tr>";
                $("#TabFengWeiCan tbody").append(_$html);
            }
        },
        SumPriceJingDian: function() {//计算景点价格合计
            var sumpricejs = 0; var sumpriceth = 0; var selfpricejs = 0.00; var selfpriceth = 0.00; var selfcost = 0.00;
            $("#tbl_Journey_AutoAdd").find("a[data-class='a_Journey_Since']").each(function() {
                sumpricejs += tableToolbar.getFloat($(this).attr("data-pricejs"));
                sumpriceth += tableToolbar.getFloat($(this).attr("data-priceth"));
            })

            $("#Tab_SelfPay").find("input[name='hidselfpricejs']").each(function() {
                selfpricejs += tableToolbar.getFloat($(this).val());
            })
            $("#Tab_SelfPay").find("input[name='txt_SelfPayPrice']").each(function() {
                selfpriceth += tableToolbar.getFloat($(this).val());
            })
            //
            $("#Tab_SelfPay").find("input[name='txt_SelfPayCost']").each(function() {
                selfcost += tableToolbar.getFloat($(this).val());
            })


            $(".tdScenicCost").find("input").first().val(sumpricejs + selfpricejs - selfcost);
            $(".tdScenicPrice").find("input").first().val(sumpriceth + selfpriceth - selfcost);
        },
        SetScenicPic: function(aid) { //设置行程中推荐景点图片
            var path = "";
            var isResult = true;
            $("#" + aid).closest("tbody").find("span[class='upload_filename']").each(function() {
                if (isResult) {
                    var self = $(this);
                    if (self.attr("data-istuijian") == "1") {
                        if ($.trim(self.attr("data-fujian")) != "") {
                            path = $.trim(self.attr("data-fujian"));
                            isResult = false;
                        }
                    }
                }
            })
            if (isResult) {//有推荐图片下面就不执行了。
                $("#" + aid).closest("tbody").find("span[class='upload_filename']").each(function() {
                    if (isResult) {
                        var self = $(this);
                        if ($.trim(self.attr("data-fujian") != "")) {
                            path = self.attr("data-fujian");
                            isResult = false;
                        }
                    }
                })
            }
            $("#" + aid).closest("td").find("input[name='filepath']").val(path);
            $("#" + aid).closest("tbody").find("td[class='imgtd']").html("<img alt='' width='150' height='120' src='" + path + "' />");
        },
        gouwu: "",
        AjaxGetShop: function(that) { //重新获取城市
            var thiscity = "";
            var allcity = "";
            that.closest("tbody[class='tempRow']").find("input[name='hidcityid']").each(function() {
                var self = $(this);
                thiscity += self.val() + ",";
            })
            if (thiscity.length > 0) {
                Journey.AjaxShop(that, thiscity, "oneday");
            }
            $("input[name='hidcityid']").each(function() {
                var self = $(this);
                allcity += self.val() + ",";
            })
            if (allcity.length > 0) {
                Journey.AjaxShop(that, allcity, "all");
            }
        },
        AjaxShop: function(that, cityids, type) {//文本筛选完城市后，获取城市下面的购物点
            var url = "/CommonPage/selectCity.aspx?citysid=" + cityids;
            $.newAjax({
                type: "post",
                cache: false,
                async: false,
                url: url,
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {
                        Journey.gouwu = ret.obj;
                        var arrayshop = new Array();
                        arrayshop = Journey.gouwu;

                        if (type == "oneday")
                            that.closest("tbody[class='tempRow']").find("td[class='gouwutd']").find("span").remove();
                        else
                            $(".tdshop").find("span,br").remove();
                        if (Journey.gouwu.length > 0) {
                            for (var i = 0; i < arrayshop.length; i++) {
                                if (type == "oneday") {
                                    //单天的购物点
                                    Journey.CreatePlanShop(that, arrayshop[i]);
                                }
                                if (type == "all") {
                                    //所有的购物点
                                    Journey.CreatePlanShop(null, arrayshop[i]);
                                }
                            }
                        }
                        else {
                            if (type == "oneday") {
                                //单天的购物点
                                Journey.CreatePlanShop(that, null);
                            }
                            if (type == "all") {
                                //所有的购物点
                                Journey.CreatePlanShop(null, null);
                            }
                        }
                    }
                }
            });
        }
    }

    //(行程)景点选择打开窗口选择景点后回调方法
    function jingDianXuanZe_callBack(data) {
        if (!data || $.trim(data.jingDianXuanYongAId).length == 0) return;
        for (var i = 0; i < data.jingDians.length; i++) {
            Journey.CreateSince(data.jingDianXuanYongAId, data.jingDians[i], i);
            Journey.SetScenicPic(data.jingDianXuanYongAId);
        }
    }
    //选择酒店回调方法
    function CallBackHotel(obj) {
        $("#" + obj.aid).parent().find("input[type='hidden']").val(obj.id);
        $("#" + obj.aid).prev("input[type='text']").val(obj.name);
        $("#" + obj.aid).next("input[type='text']").val(obj.wprice);
        AddPrice.SetHotelPrice();
        AddPrice.SetTotalPrice();
    }
    //选择餐馆回调方法
    function CallBackCanGuan(obj) {
        var ramNum = "";
        if ($.trim($("#" + obj.aid).parent().find("input[class='footid']").val()) == "" && obj.id != "") {
            var date = new Date();
            ramNum = Math.random() * 100000 + date.getSeconds();
            $("#" + obj.aid).parent().find("input[class='footid']").val(ramNum);
        }
        else {
            ramNum = $("#" + obj.aid).parent().find("input[class='footid']").val();
        }
        $("#" + obj.aid).parent().find("input[type='hidden'][class='canid']").val(obj.id);
        $("#" + obj.aid).parent().find("input[type='hidden'][class='menuid']").val(obj.caidanid);
        $("#" + obj.aid).parent().find("input[type='hidden'][class='pricejs']").val(obj.pricejs);
        $("#" + obj.aid).parent().find("input[type='text']").eq(0).val(obj.pricesell);
        $("#" + obj.aid).prev("input[type='text']").val(obj.caidanname);
        if (obj.caidanid != "" && obj.caidanname != "" && obj.id != "") {
            Journey.AutoFengWeiCan(obj.caidanid, obj.id, obj.caidanname, false, obj.pricejs, obj.pricesell, ramNum, "0");
            AddPrice.SumMenuPrice();
            AddPrice.SetTotalPrice();
            AddPrice.SumHeJiPrice();
        }
        if (obj.id == "" && ramNum != "") {//删除风味餐(移除掉下面追加的风味餐)
            $("#TabFengWeiCan").find("input[name='hidfootid']").each(function() {
                if ($(this).val() == ramNum) {
                    $(this).closest("tr").remove();
                    if ($("#TabFengWeiCan").find("tr[class='tempRowfwei']").length == 0) {
                        Journey.AutoFengWeiCan("", "", "", true, "", "", "", "1");
                    }
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                }
            })
            $("#" + obj.aid).closest("td").find("input[class='footid']").val("");
        }
    }
    //选择城市回调方法
    function CallBackCity(ret) {
        $("#" + ret.aid).parent().find("input[type='text']").val(ret.name);
        $("#" + ret.aid).parent().find("input[type='hidden']").val(ret.id);
        Journey.AjaxGetShop($("#" + ret.aid));
    }
    //(自费)景点选择打开窗口选择景点后回调方法
    function CallBackJingDian(data) {
        var aid = data.jingDianXuanYongAId;
        var jingdian = [];
        jingdian = data.jingDians;
        if (jingdian.length > 0) {
            $("#" + aid).closest("td").find("input[name='hidselfscenicid']").val(jingdian[0].id);
            $("#" + aid).closest("td").find("input[name='hidselfscenicpriceid']").val(jingdian[0].priceid);
            $("#" + aid).closest("td").find("input[name='hidselfpricejs']").val(jingdian[0].jiagejs);
            $("#" + aid).closest("td").next("td").find("input[name='txt_SelfPayPrice']").val(jingdian[0].jiageth);
            $("#" + aid).closest("td").find("input[type='text']").val(jingdian[0].name);
        }
        Journey.SumPriceJingDian();
        AddPrice.SetTotalPrice();
        AddPrice.SumHeJiPrice();
    }




    $(function() {
        $("#tbl_Journey_AutoAdd").find("textarea[name='txtContent']").each(function() {
            var _self = $(this);
            _self.unbind().click(function() {
                Journey.CreateEdit(this);
            })
            if ($.trim(_self.val()) != "") {
                $(this).click();
            }
        })
        //选择购物点事件(行程中购物)
        $(".gouwutd").delegate("input[type='checkbox']", "click", function() {
            var shopid = ""; var shopname = "";
            if (this.checked) {
                var thisVal = $(this).val();
                shopid += thisVal + ",";
                shopname += $(this).attr("data-name") + ",";
                $(this).parent().find("input[name='gouwuname']").attr("checked", "checked");
                $(".tdshop").find("input[type='checkbox']").each(function() {
                    if ($(this).val() == thisVal) {
                        $(this).attr("checked", "checked");
                    }
                    Journey.SetShopValue(this);

                })
            } else {
                var thisVal = $(this).val();
                $(this).parent().find("input[name='gouwuname']").removeAttr("checked");
                $(".tdshop").find("input[type='checkbox']").each(function() {
                    if ($(this).val() == thisVal) {
                        $(this).removeAttr("checked");
                    }
                    Journey.SetShopValue(this);
                })
            }
        })
        // 选择购物点事件(所有购物)
        $(".tdshop").delegate("input[type='checkbox']", "click", function() {
            var self = $(this);
            if (!this.checked) {
                $(".gouwutd").find("input[type='checkbox']:checked").each(function() {
                    if (self.val() == $(this).val()) {
                        self.attr("checked", "checked");
                    }
                })
                Journey.SetShopValue(this);
            }
        });
        $("#Tab_SelfPay").delegate("input[name='txt_SelfPayCost']", "blur", function() {
            Journey.SumPriceJingDian();
            AddPrice.SetTotalPrice();
            AddPrice.SumHeJiPrice();
        })


        $("#TabFengWeiCan").delegate(".addbtnfwei", "click", function() {
            Journey.AutoFengWeiCan("", "", "", true, "", "", "", "1");
        })
        $("#TabFengWeiCan").delegate(".delbtnfwei", "click", function() {
            if ($("#TabFengWeiCan").find("tr[class='tempRowfwei']").length == 1) {
                tableToolbar._showMsg("最少保留一行!");
                return false;
            }
            $(this).closest("tr").remove();
            AddPrice.SumMenuPrice();
            setTimeout(AddPrice.SetTotalPrice, 200);
            AddPrice.SumHeJiPrice();
        })


        $(".xuanyonghotel").live("click", function() {
            $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
            var url = "/CommonPage/UserSupplier.aspx?aid=" + $(this).attr("id") + "&";
            var hideObj = $(this).parent().find("input[type='hidden']");
            var showObj = $(this).parent().find("input[type='text']");
            if (!hideObj.attr("id")) {
                hideObj.attr("id", "hideID_" + parseInt(Math.random() * 10000000));
            }
            if (!showObj.attr("id")) {
                showObj.attr("id", "ShowID_" + parseInt(Math.random() * 10000000));
            }
            var binkemode = $("#hidbinkemode").val();
            var tourmode = $("#hidtourmode").val();
            var lgtype = $("#hidLngType").val();
            url += $.param({ suppliertype: 1, hideID: $(hideObj).val(), callBack: "CallBackHotel", ShowID: showObj.attr("id"), binkemode: binkemode, tourmode: tourmode, LgType: lgtype });
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择酒店",
                modal: true,
                width: "1024",
                height: "406"
            });
        });

        $(".canguanbox").live("click", function() {
            $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
            var url = "/CommonPage/UserSupplier.aspx?aid=" + $(this).attr("id") + "&";
            var hideObj = $(this).parent().find("input[class='canid']"); //餐厅编号
            var hidemenu = $(this).parent().find("input[class='menuid']"); //菜单编号
            var showObj = $(this).prev("input[type='text']"); //菜单名称
            if (!hideObj.attr("id")) {
                hideObj.attr("id", "hideID_" + parseInt(Math.random() * 10000000));
            }
            if (!showObj.attr("id")) {
                showObj.attr("id", "ShowID_" + parseInt(Math.random() * 10000000));
            }
            if (!hidemenu.attr("id")) {
                hidemenu.attr("id", "hidemenu_" + parseInt(Math.random() * 10000000));
            }
            var lgtype = $("#hidLngType").val();
            url += $.param({ suppliertype: 2, hideID: $(hideObj).val(), callBack: "CallBackCanGuan", ShowID: $(showObj).val(), hidcaidanid: $(hidemenu).val(), LgType: lgtype })
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择餐馆",
                modal: true,
                width: "948",
                height: "406"
            });
        })
        $(".citybox").live("click", function() {
            $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
            var isMore = $(this).attr("data-mode");
            var url = "/CommonPage/selectCity.aspx?aid=" + $(this).attr("id") + "&";
            var hideObj = $(this).parent().find("input[type='hidden']");
            var showObj = $(this).parent().find("input[type='text']");
            if (!hideObj.attr("id")) {
                hideObj.attr("id", "hideID_" + parseInt(Math.random() * 10000000));
            }
            if (!showObj.attr("id")) {
                showObj.attr("id", "ShowID_" + parseInt(Math.random() * 10000000));
            }
            url += $.param({ hideID: $(hideObj).val(), CityName: $(showObj).val(), callBack: "CallBackCity", ShowID: showObj.attr("id"), isMore: isMore, isxuanyong: 1 })
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择城市",
                modal: true,
                width: "450",
                height: "300"
            });
        })
        var lgtype = '<%=Request.QueryString["LgType"] %>';
        $("input[name='txtcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID + "&LgType=" + lgtype, {
            width: 130,
            selectFirst: 'true',
            blur: 'true'
        }).result(function(e, data) {
            $(this).prev("input[type='hidden']").val(data[1]);
            $(this).attr("data-old", data[0]);
            $(this).attr("data-oldvalue", data[1]);
            Journey.AjaxGetShop($(this));
        });

        $(".TabCity").delegate("input[name='txtcity']", "keyup", function() {
            var v = $(this);
            if ($.trim(v.val()) == "") {

                v.prev("input[type='hidden']").val("");

                //单天的购物点
                Journey.CreatePlanShop(v, null);

                //所有的购物点
                Journey.CreatePlanShop(null, null);

            }
            if ($.trim(v.val()) != $.trim(v.attr("data-old"))) {
                v.prev("input[type='hidden']").val("");
                //单天的购物点
                Journey.CreatePlanShop(v, null);
                //所有的购物点
                Journey.CreatePlanShop(null, null);
            }

        });


        $(".dropdown img.flag").addClass("flagvisibility");

        $(".TabCity").delegate(".dropdown dt a", "click", function() {
            $(this).closest("tr").find(".dropdown dd ul").toggle();
        })


        $(".TabCity").delegate(".dropdown dd ul li a", "click", function() {
            var text = $(this).html();
            $(this).closest("tr").find(".dropdown dt a span").html(text);
            $(".dropdown dd ul").hide();
            var selectvalue = $(this).attr("data-value");
            $(this).closest("td").find("input[name='hidtraffictype']").val(selectvalue);
        })
        $(".showtrafficprice").live("click", function() {
            $(this).next("input").toggleClass("showprice");
        })

        $(document).bind('click', function(e) {
            var $clicked = $(e.target);
            if (!$clicked.parents().hasClass("dropdown"))
                $(".dropdown dd ul").hide();
        });

        $("#tbl_Journey_AutoAdd").delegate("a[class='addcity']", "click", function() {
            var Tr = $(this).closest("tr[class='Trcity']");
            $(Tr).closest("table[class='TabCity']").append($(Tr).clone());
            var lgtype = '<%=Request.QueryString["LgType"] %>';
            $("input[name='txtcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID + "&LgType=" + lgtype, {
                width: 130,
                selectFirst: 'true',
                blur: 'true'
            }).result(function(e, data) {
                $(this).prev("input[type='hidden']").val(data[1]);
                $(this).attr("data-old", data[0]);
                $(this).attr("data-oldvalue", data[1]);
                Journey.AjaxGetShop($(this));
            });
        })
        $("#tbl_Journey_AutoAdd").delegate("a[class='delcity']", "click", function() {
            var Tr = $(this).closest("tr[class='Trcity']");
            var TrCount = $(this).closest("table[class='TabCity']").find("tr[class='Trcity']").length;
            if (TrCount == 1) {
                tableToolbar._showMsg("最少保留一行!");
                return false;
            }
            $(Tr).remove();
            Journey.AjaxGetShop($(this));
            AddPrice.SetTrafficPrice();
            AddPrice.SetTotalPrice();
        })

    })
</script>

<div style="width: 98.5%" class="tablelist-box ">
    <span class="formtableT"><s></s>行程安排</span>
    <table width="100%" cellspacing="0" cellpadding="0" class="content2" id="tbl_Journey_AutoAdd">
        <tbody>
            <tr class="addcontentT">
                <th width="5%" valign="middle">
                    日期
                </th>
                <th valign="middle">
                    城市
                </th>
                <th valign="middle">
                    酒店
                </th>
                <th valign="middle" colspan="2">
                    用餐
                </th>
                <th valign="middle">
                    操作
                </th>
            </tr>
        </tbody>
        <%if (SetPlanList == null || (SetPlanList != null && SetPlanList.Count == 0))
          { %>
        <tbody class="tempRow">
            <tr>
                <td align="center" rowspan="4">
                    D<span class="index">1</span><br>
                </td>
                <td align="center" rowspan="2">
                    <input type="hidden" name="hidcityids" value="" />
                    <input type="hidden" name="hidcitys" value="" />
                    <input type="hidden" name="hidtraffics" value="" />
                    <input type="hidden" name="hidtrafficprices" value="" />
                    <table class="TabCity">
                        <tr class="Trcity">
                            <td>
                                <input type="hidden" name="hidcityid" value="" />
                                <input type="text" class="formsize120 inputtext" name="txtcity" valid="required"
                                    errmsg="行程中城市不能为空！" value="">
                                <a class="xuanyong citybox" data-mode="0" href="javascript:;"></a>
                            </td>
                            <td>
                                <input type="hidden" value="" name="hidtraffictype" />
                                <dl class="dropdown">
                                    <dt><a href="javascript:;" data-value="-1"><span>请选择</span></a></dt>
                                    <dd>
                                        <ul>
                                            <li><a href="javascript:;" data-value="9">
                                                <img alt="" src="../images/jt_feiji.png" class="flag flagvisibility"></a></li>
                                            <li><a href="javascript:;" data-value="10">
                                                <img alt="" src="../images/jt_huoche.png" class="flag flagvisibility"></a></li>
                                            <li><a href="javascript:;" data-value="11">
                                                <img alt="" src="../images/jt_qiche.png" class="flag flagvisibility"></a></li>
                                            <li><a href="javascript:;" data-value="12">
                                                <img alt="" src="../images/jt_youlun.png" class="flag flagvisibility"></a></li>
                                        </ul>
                                    </dd>
                                </dl>
                                <div style="display: inline-block; float: left;">
                                    <img src="../images/price.jpg" alt="" class="showtrafficprice" />
                                    <input type="text" class="formsize40 inputtext" name="txttrafficprice" value="" />
                                </div>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="addcity">
                                    <img width="48" height="20" src="../images/addimg.gif"></a> <a href="javascript:void(0)"
                                        class="delcity">
                                        <img width="48" height="20" src="../images/delimg.gif"></a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" rowspan="2" class="tdHotel">
                    &nbsp; <span>
                        <input type="hidden" name="hidhotel1id" value="" />
                        <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                            name="txthotel1">
                        <a class="xuanyonghotel xuanyong" data-hotel="hotel1" href="javascript:;">&nbsp;</a>
                        价格
                        <input type="text" class="formsize40 inputtext" name="txthotel1price"></span>
                </td>
                <td align="left" colspan="2">
                    <input type="hidden" name="eatFrist" value="0" />
                    <label>
                        <input type="checkbox" value="1" onclick="Journey.SelectOneForEat(this)" />早</label>&nbsp;
                    <input type="hidden" value="" name="hidfastfootid" class="footid" />
                    <input type="hidden" value="" name="hidfastprice" class="pricejs" />
                    <input type="text" value="" class="formsize40 inputtext menuprice" name="txtbreakprice" />
                    <input type="hidden" name="hidbreakmenuid" class="menuid" />
                    <input type="hidden" name="hidbreak" value="" class="canid" />
                    <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                        name="txtbreakname" />
                    <a class="xuanyong canguanbox" href="javascript:;"></a>
                </td>
                <td valign="middle" align="center" rowspan="5">
                    <a href="javascript:void(0)">
                        <img width="48" height="20" class="moveupbtn" src="../images/shangyiimg.gif" style="display: none;"></a><br>
                    <br>
                    <a href="javascript:void(0)">
                        <img width="48" height="20" class="insertbtn" src="../images/charuimg.gif"></a>&nbsp;
                    <a href="javascript:void(0)">
                        <img width="48" height="20" class="delbtn" src="../images/delimg.gif"></a><br>
                    <br>
                    <a href="javascript:void(0)">
                        <img width="48" height="20" class="movedownbtn" src="../images/xiayiimg.gif" style="display: none;"></a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="hidden" name="eatSecond" value="0" />
                    <label>
                        <input type="checkbox" value="2" onclick="Journey.SelectOneForEat(this)" />中</label>&nbsp;
                    <input type="hidden" value="" name="hidsecondfootid" class="footid" />
                    <input type="hidden" value="" name="hidsecondprice" class="pricejs" />
                    <input type="text" value="" class="formsize40 inputtext menuprice" name="txtsecondprice">
                    <input type="hidden" name="hidsecondmenuid" value="" class="menuid" />
                    <input type="hidden" name="hidsecond" value="" class="canid" />
                    <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                        name="txtsecondname">
                    <a class="xuanyong canguanbox" href="javascript:;"></a>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" colspan="3">
                    <b>选择景点</b>：&nbsp;<input type="hidden" name="hd_scenery_spot" value="" />
                    <input type="hidden" name="show_scenery_spot" value="" />
                    <input type="hidden" name="filepath" value="" />
                    <input type="hidden" name="hidpriceth" value="" />
                    <input type="hidden" name="hidpricejs" value="" />
                    <a class="xuanyong" title="选择景点" onclick="Journey.SelectSince(this);return false;"
                        href="/CommonPage/SelectScenic.aspx?callback=jingDianXuanZe_callBack&IsMultiple=0">
                        &nbsp;</a> <span data-class="fontblue" class="fontblue"></span>
                </td>
                <td valign="middle" align="left">
                    <input type="hidden" name="eatThird" value="0" />
                    <label>
                        <input type="checkbox" value="3" onclick="Journey.SelectOneForEat(this)" />晚</label>&nbsp;
                    <input type="hidden" value="" name="hidthirdfootid" class="footid" />
                    <input type="hidden" value="" name="hidthirdprice" class="pricejs" />
                    <input type="text" value="" class="formsize40 inputtext menuprice" name="txtthirdprice" />
                    <input type="hidden" value="" name="hidthirdmenuid" class="menuid" />
                    <input type="hidden" value="" name="hidthird" class="canid" />
                    <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                        name="txtthirdname" />
                    <a class="xuanyong canguanbox" href="javascript:;"></a>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding: 4px;" colspan="3">
                    <textarea name="txtContent" class="richText" style="width: 99%; height: 200px;"></textarea>
                </td>
                <td align="center" style="padding: 4px;" class="imgtd">
                </td>
            </tr>
        </tbody>
        <%} %>
        <asp:Repeater ID="rptJourney" runat="server" OnItemDataBound="rptXingCheng_ItemDataBound">
            <ItemTemplate>
                <tbody class="tempRow">
                    <tr>
                        <td align="center" rowspan="4">
                            D<span class="index"><%#Eval("Days")%></span><br>
                        </td>
                        <td align="center" rowspan="2">
                            <input type="hidden" name="hidcityids" value="" />
                            <input type="hidden" name="hidcitys" value="" />
                            <input type="hidden" name="hidtraffics" value="" />
                            <input type="hidden" name="hidtrafficprices" value="" />
                            <table class="TabCity">
                                <asp:Repeater runat="server" ID="rptCityAndTraffic">
                                    <ItemTemplate>
                                        <tr class="Trcity">
                                            <td>
                                                <input type="hidden" name="hidcityid" value='<%#Eval("CityId") %>' />
                                                <input type="text" class="formsize120 inputtext" name="txtcity" valid="required"
                                                    errmsg="行程中城市不能为空！" value='<%#Eval("CityName") %>'>
                                                <a class="xuanyong citybox" data-mode="0" href="javascript:;"></a>
                                            </td>
                                            <td>
                                                <input type="hidden" value='<%#(int)Eval("JiaoTong") %>' name="hidtraffictype" />
                                                <dl class="dropdown">
                                                    <dt>
                                                        <%#GetTraffic(Eval("JiaoTong"))%></dt>
                                                    <dd>
                                                        <ul>
                                                            <li><a href="javascript:;" data-value="9">
                                                                <img alt="" src="../images/jt_feiji.png" class="flag flagvisibility"></a></li>
                                                            <li><a href="javascript:;" data-value="10">
                                                                <img alt="" src="../images/jt_huoche.png" class="flag flagvisibility"></a></li>
                                                            <li><a href="javascript:;" data-value="11">
                                                                <img alt="" src="../images/jt_qiche.png" class="flag flagvisibility"></a></li>
                                                            <li><a href="javascript:;" data-value="12">
                                                                <img alt="" src="../images/jt_youlun.png" class="flag flagvisibility"></a></li>
                                                        </ul>
                                                    </dd>
                                                </dl>
                                                <div style="display: inline-block; float: left;">
                                                    <img src="../images/price.jpg" alt="" class="showtrafficprice" />
                                                    <input type="text" class="formsize40 inputtext" name="txttrafficprice" value='<%#Eval("JiaoTongJiaGe","{0:F2}")%>' />
                                                </div>
                                            </td>
                                            <td align="center">
                                                <a href="javascript:void(0)" class="addcity">
                                                    <img width="48" height="20" src="../images/addimg.gif"></a> <a href="javascript:void(0)"
                                                        class="delcity">
                                                        <img width="48" height="20" src="../images/delimg.gif"></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                        <td align="center" rowspan="2" class="tdHotel">
                            &nbsp; <span>
                                <input type="hidden" name="hidhotel1id" value='<%#Eval("HotelId1") %>' />
                                <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                                    name="txthotel1" value='<%#Eval("HotelName1") %>'>
                                <a class="xuanyonghotel xuanyong" data-hotel="hotel1" href="javascript:;">&nbsp;</a>
                                价格
                                <input type="text" class="formsize40 inputtext" name="txthotel1price" value='<%#Convert.ToDecimal(Eval("HotelPrice1")).ToString("f2") %>'>
                            </span>
                        </td>
                        <td align="left" colspan="2">
                            <input type="hidden" name="eatFrist" value="<%#((bool)Eval("IsBreakfast"))?"1":"0"%>" />
                            <label>
                                <input type="checkbox" value="1" onclick="Journey.SelectOneForEat(this)" <%#(bool)Eval("IsBreakfast")?"checked=checked":""%> />早</label>&nbsp;
                            <input type="hidden" value="<%#Eval("BreakfastId") %>" name="hidfastfootid" class="footid" />
                            <input type="hidden" value="<%#Convert.ToDecimal(Eval("BreakfastSettlementPrice")).ToString("f2") %>"
                                name="hidfastprice" class="pricejs" eatpricejs='<%#(bool)Eval("IsBreakfast")?"yes":"no"%>' />
                            <input type="text" class="formsize40 inputtext menuprice" name="txtbreakprice" value='<%#Convert.ToDecimal(Eval("BreakfastPrice")).ToString("f2") %>'
                                eatfrist='<%#(bool)Eval("IsBreakfast")?"yes":"no"%>' />
                            <input type="hidden" name="hidbreakmenuid" class="menuid" value='<%#Eval("BreakfastMenuId") %>' />
                            <input type="hidden" name="hidbreak" class="canid" value='<%#Eval("BreakfastRestaurantId") %>' />
                            <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                                name="txtbreakname" value='<%#Eval("BreakfastMenu") %>'>
                            <a class="canguanbox xuanyong" href="javascript:;"></a>
                        </td>
                        <td valign="middle" align="center" rowspan="5">
                            <a href="javascript:void(0)">
                                <img width="48" height="20" class="moveupbtn" src="../images/shangyiimg.gif" style="display: none;"></a><br>
                            <br>
                            <a href="javascript:void(0)">
                                <img width="48" height="20" class="insertbtn" src="../images/charuimg.gif"></a>&nbsp;
                            <a href="javascript:void(0)">
                                <img width="48" height="20" class="delbtn" src="../images/delimg.gif"></a><br>
                            <br>
                            <a href="javascript:void(0)">
                                <img width="48" height="20" class="movedownbtn" src="../images/xiayiimg.gif" style="display: none;"></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input type="hidden" name="eatSecond" value="<%#(bool)Eval("IsLunch")?"1":"0"%>" />
                            <label>
                                <input type="checkbox" value="2" onclick="Journey.SelectOneForEat(this)" <%#(bool)Eval("IsLunch")?"checked=checked":""%> />中</label>&nbsp;
                            <input type="hidden" value="<%#Eval("LunchId") %>" name="hidsecondfootid" class="footid" />
                            <input type="hidden" class="pricejs" value="<%#Convert.ToDecimal(Eval("LunchSettlementPrice")).ToString("f2") %>"
                                name="hidsecondprice" eatpricejs='<%#(bool)Eval("IsLunch")?"yes":"no"%>' />
                            <input type="text" class="formsize40 inputtext menuprice" name="txtsecondprice" value='<%#Convert.ToDecimal(Eval("LunchPrice")).ToString("f2") %>'
                                eatsecond='<%#(bool)Eval("IsLunch")?"yes":"no"%>' />
                            <input type="hidden" name="hidsecondmenuid" class="menuid" value='<%#Eval("LunchMenuId") %>' />
                            <input type="hidden" name="hidsecond" class="canid" value='<%#Eval("LunchRestaurantId") %>' />
                            <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                                name="txtsecondname" value='<%#Eval("LunchMenu") %>'>
                            <a class="xuanyong canguanbox" href="javascript:;"></a>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" colspan="3">
                            <b>选择景点</b>：&nbsp;
                            <input type="hidden" name="filepath" value='<%#Eval("FilePath") %>' />
                            <a class="xuanyong" id="<%# Container.ItemIndex %>" title="选择景点" onclick="Journey.SelectSince(this);return false;"
                                href="/CommonPage/SelectScenic.aspx?callback=jingDianXuanZe_callBack">&nbsp;</a>
                            <%#GetSinceByList(Eval("TourPlanSpotList"))%>
                        </td>
                        <td valign="middle" align="left">
                            <input type="hidden" name="eatThird" value="<%#(bool)Eval("IsSupper")?"1":"0"%>" />
                            <label>
                                <input type="checkbox" value="3" onclick="Journey.SelectOneForEat(this)" <%#(bool)Eval("IsSupper")?"checked=checked":""%> />晚</label>&nbsp;
                            <input type="hidden" value="<%#Eval("SupperId") %>" name="hidthirdfootid" class="footid" />
                            <input type="hidden" value="<%#Convert.ToDecimal(Eval("SupperSettlementPrice")).ToString("f2") %>"
                                name="hidthirdprice" class="pricejs" eatpricejs='<%#(bool)Eval("IsSupper")?"yes":"no"%>' />
                            <input type="text" class="formsize40 inputtext menuprice" name="txtthirdprice" value='<%#Convert.ToDecimal(Eval("SupperPrice")).ToString("f2") %>'
                                eatthird='<%#(bool)Eval("IsSupper")?"yes":"no"%>' />
                            <input type="hidden" name="hidthirdmenuid" class="menuid" value='<%#Eval("SupperMenuId") %>' />
                            <input type="hidden" name="hidthird" class="canid" value='<%#Eval("SupperRestaurantId") %>' />
                            <input type="text" readonly="readonly" style="background-color: #dadada" class="formsize120 inputtext"
                                name="txtthirdname" value='<%#Eval("SupperMenu") %>' />
                            <a class="xuanyong canguanbox" href="javascript:;"></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding: 4px;" colspan="3">
                            <textarea name="txtContent" class="richText" style="width: 99%; height: 200px;"><%#Eval("Content")%></textarea>
                        </td>
                        <td align="center" style="padding: 4px;" class="imgtd">
                            <%#Eval("FilePath").ToString().Trim() != "" ? "<img width='150' height='120' src='" + Eval("FilePath").ToString() + "' />" : ""%>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>
