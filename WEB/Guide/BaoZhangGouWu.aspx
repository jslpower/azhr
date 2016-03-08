<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoZhangGouWu.aspx.cs" Inherits="EyouSoft.Web.Guide.BaoZhangGouWu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head  runat="server">
    <title>购物收入</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
            <div class="tablelist-box" style="width:98.5%">
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <th width="10%" align="right">购物店：</th>
                  <td align="left"><label id="lblGouWuDian" runat="server"></label></td>
                  <th width="10%" align="right">联系人：</th>
                  <td align="left"><label id="lblLianXiRen" runat="server"></label></td>
                  <th width="10%" align="right">联系电话：</th>
                  <td align="left"><label id="lblLianXiTel" runat="server"></label></td>
                </tr>
                <tr>
                  <th align="right">联系传真：</th>
                  <td align="left"><label id="lblLianXiFax" runat="server"></label></td>
                  <th align="right">进店日期：</th>
                  <td colspan="3" align="left"><label id="lblJinDianDate" runat="server"></label></td>
                </tr>
                <tr>
                  <th align="right">导游需知：</th>
                  <td colspan="5" align="left"><label id="lblDaoYouXuZhi" runat="server"></label></td>
                </tr>
                <tr>
                  <th align="right">备注：</th>
                  <td colspan="5" align="left"><label id="lblRemark" runat="server"></label></td>
                </tr>
                <tr>
                  <th align="right">国籍/地区：</th>
                  <td align="left"><label id="lblGuoJi" runat="server"></label></td>
                  <th align="right">流水：</th>
                  <td align="left"><input value="0" id="txtLiuShui" name="txtLiuShui" type="text" class="formsize50 input-txt" valid="required|IsDecimalTwo" errmsg="请输入流水！|流水金额格式不对！"/></td>
                  <th align="right">保底金额：</th>
                  <td align="left"><input value="0" id="txtBaoDi" name="txtBaoDi" type="text" class="formsize50 input-txt" valid="required|IsDecimalTwo" errmsg="请输入保底金额！|保底金额格式不对！"/></td>
                </tr>
                <tr>
                  <th align="right">营业额：</th>
                  <td align="left"><input value="0" id="txtYingYe" name="txtYingYe" type="text" class="formsize50 input-txt"valid="required|IsDecimalTwo" errmsg="请输入营业额！|营业金额格式不对！"/></td>
                  <th align="right">人头：</th>
                  <td align="left">成人
                    <input value="0" id="txtAdultMoney" name="txtAdultMoney" type="text" class="formsize50 input-txt"valid="required|IsDecimalTwo" errmsg="请输入成人人头费！|成人人头费格式不对！"/>
                    儿童<input value="0" id="txtChildMoney" name="txtChildMoney" type="text" class="formsize50 input-txt"valid="required|IsDecimalTwo" errmsg="请输入儿童人头费！|儿童人头费格式不对！"/></td>
                  <th align="right">进店人数：</th>
                  <td align="left">成人
                    <input value="0" id="txtAdult" name="txtAdult" type="text" class="formsize50 input-txt"valid="required|isNumber" errmsg="请输入成人数！|成人数格式不对！"/>
儿童
<input value="0" id="txtChild" name="txtChild"  type="text" class="formsize50 input-txt"valid="required|isNumber" errmsg="请输入儿童数！|儿童数格式不对！"/></td>
                </tr>
                <tr>
                  <th align="right">产品：</th>
                  <td colspan="5" align="left"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin:5px auto;" class="autoAdd">
                    <tr>
                      <th align="center">名称</th>
                      <th align="center">数量</th>
                      <th align="center">返点金额</th>
                      <th width="120" align="center">操作</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptChanPin">
                        <ItemTemplate>
                            <tr class="tempRow">
                              <td align="center"><%#this.SelChanPinInit(Eval("ProductId").ToString()) %></td>
                              <td align="center"><input name="txtShuLiang" value="<%#Eval("BuyAmount") %>" type="text" class="formsize50 input-txt"/></td>
                              <td align="center"><input name="txtFanDian" value="<%#Eval("BackMoney","{0:F2}") %>" type="text" class="formsize50 input-txt"/></td>
                              <td align="center"><a href="javascript:void(0)" class="addbtn"><img src="../images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)" class="delbtn"><img src="../images/delimg.gif" width= "48" height="20" /></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phTemp" Visible="false">
                    <tr class="tempRow">
                      <td align="center"><%=this.SelChanPinInit("") %></td>
                      <td align="center"><input value="0" name="txtShuLiang" type="text" class="formsize50 input-txt"/></td>
                      <td align="center"><input value="0" name="txtFanDian" type="text" class="formsize50 input-txt"/></td>
                      <td align="center"><a href="javascript:void(0)" class="addbtn"><img src="../images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)" class="delbtn"><img src="../images/delimg.gif" width= "48" height="20" /></a></td>
                    </tr>
                    </asp:PlaceHolder>
                  </table></td>
                </tr>
                <tr>
                  <th align="right">交给公司：</th>
                  <td colspan="5" align="left"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin:5px auto;">
                    <tr>
                      <th align="center">购物店</th>
                      <th align="center">额外流水</th>
                      <th align="center">合计金额</th>
                    </tr>
                    <tr class="tempRow">
                      <td align="center">人头
                        <input value="0" name="txtToCompanyRenTou" id="txtToCompanyRenTou"  type="text" class="formsize40 input-txt"/>
+保底金额
<input value="0" name="txtToCompanyBaoDi" id="txtToCompanyBaoDi"  type="text" class="formsize50 input-txt"/>
*人数
<input value="0" name="txtToCompanyRenShu" id="txtToCompanyRenShu"  type="text" class="formsize30 input-txt"/>
+保底2
<input value="0" name="txtToCompanyBaoDi2" id="txtToCompanyBaoDi2"  type="text" class="formsize50 input-txt"/>
*人数
<input value="0" name="txtToCompanyRenShu2" id="txtToCompanyRenShu2"  type="text" class="formsize30 input-txt"/>
+返点金额
<input value="0" name="txtToCompanyFanDian" id="txtToCompanyFanDian"  type="text" class="formsize50 input-txt"/></td>
                      <td align="center">营业额
                        <input value="0" name="txtToCompanyYingYe" id="txtToCompanyYingYe" type="text" class="formsize50 input-txt"/>
*
<input value="0" name="txtToCompanyTiQu" id="txtToCompanyTiQu" type="text" class="formsize30 input-txt" value="15"/>
%</td>
                      <td align="center"><input value="0" name="txtToCompanyTotal" id="txtToCompanyTotal" type="text" class="formsize50 input-txt"/></td>
                    </tr>
                  </table></td>
                </tr>
                <tr>
                  <th align="right">交给导游：</th>
                  <td colspan="5" align="left"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin:5px auto;">
                    <tr>
                      <th align="center">营业额</th>
                      <th align="center">减去费用</th>
                      <th align="center">减去额外流水</th>
                      <th align="center">合计金额</th>
                    </tr>
                    <tr class="tempRow">
                      <td align="center"> 营业额
                        <input value="0" name="txtToGuideYingYe" id="txtToGuideYingYe" type="text" class="formsize50 input-txt"/>
                        提取比例
<input value="0" name="txtToGuideTiQu" id="txtToGuideTiQu" type="text" class="formsize30 input-txt"/>
%</td>
                      <td align="center">路桥费
                        <input value="0" name="txtToGuideLu" id="txtToGuideLu" type="text" class="formsize50 input-txt"/>
水费
<input value="0" name="txtToGuideShui" id="txtToGuideShui" type="text" class="formsize50 input-txt"/>
陪同床费
<input value="0" name="txtToGuidePei" id="txtToGuidePei" type="text" class="formsize50 input-txt"/>
交通费
<input value="0" name="txtToGuideJiao" id="txtToGuideJiao"type="text" class="formsize50 input-txt"/>
其他
<input value="0" name="txtToGuideOther" id="txtToGuideOther" type="text" class="formsize50 input-txt"/></td>
                      <td align="center"><input value="0" name="txtToGuideLiuShui" id="txtToGuideLiuShui" type="text" class="formsize50 input-txt"/></td>
                      <td align="center"><input value="0" name="txtToGuideTotal" id="txtToGuideTotal" type="text" class="formsize50 input-txt"/></td>
                    </tr>
                  </table></td>
                </tr>
                <tr>
                  <th align="right">交给领队：</th>
                  <td colspan="5" align="left"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin:5px auto;">
                    <tr>
                      <th align="center">营业额</th>
                      <th align="center">金额</th>
                    </tr>
                    <tr class="tempRow">
                      <td align="center">营业额
                        <input value="0" name="txtToLeaderYingYe" id="txtToLeaderYingYe" type="text" class="formsize50 input-txt"/>
                        提取比例
                        <input value="0" name="txtToLeaderTiQu" id="txtToLeaderTiQu" type="text" class="formsize30 input-txt"/>
%</td>
                      <td align="center"><input value="0" name="txtToLeaderTotal" id="txtToLeaderTotal" type="text" class="formsize50 input-txt"/></td>
                    </tr>
                  </table></td>
                </tr>
              </table>
            </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="a_Save"><s class="baochun"></s>保 存</a>
        </div>
</div>
    </form>
</body>
<script type="text/javascript">
        var Page = {
        	Form: null,
        	FormCheck: function(obj) { /*提交数据验证*/
        		this.Form = $(obj).get(0);
        		FV_onBlur.initValid(this.Form);
        		return ValiDatorForm.validator(this.Form, "parent");
        	},
        	//购物收入保存
        	Save: function(obj) {
        		var that = this;
        		if (that.FormCheck($("form"))) {
        			var obj = $(obj);
        			obj.unbind("click");
        			obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
        			obj.html("<s class=baochun></s>  提交中...");
        			$.newAjax({
        					type: "post",
        					data: $(that.Form).serialize() +
        					"&" + $.param({
        						doType: "Save",
        						PlanId: '<%=Request.QueryString["planid"] %>',
        						SourceId: '<%=Request.QueryString["sourceid"] %>'
        					}),
        					cache: false,
        					url: '/Guide/BaoZhangGouWu.aspx?sl=<%=this.SL %>',
        					dataType: "json",
        					success: function(data) {
        						if (data.result) {
        							parent.tableToolbar._showMsg(data.msg, function() {
        								window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
        								parent.location.href = parent.location.href;
        							});

        						}
        						else {
        							parent.tableToolbar._showMsg(data.msg);
        							that.BindBtn();
        						}
        					},
        					error: function() {
        						//ajax异常--你懂得
        						parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
        						that.BindBtn();
        					}
        				});
        		}
        		return false;
        	},
        	//产品选择change事件
        	selChanPin: function(o) {
        		var arr = $(o).val().split("|").reverse();
        		$(o).closest("tr").find("input[name=txtFanDian]").val(arr[1]);
        	},
        	BindBtn: function() {
        		var s = $("#a_Save");
        		s.unbind("click");
        		s.html("<s class=baochun></s>保 存");
        		s.click(function() {
        			Page.Save(this);
        			return false;
        		});
        		s.css("background-position", "0 0");
        		s.css("text-decoration", "none");
        		//input change event for autocalculate
        		$("#txtYingYe").change(function() {
        			var yingye = tableToolbar.getFloat($("#txtYingYe").val());
        			$("#txtToCompanyYingYe").val(yingye);
        			$("#txtToGuideYingYe").val(yingye);
        			$("#txtToLeaderYingYe").val(yingye);
        		});
        		$("input").change(function() {
//        			var yingye = tableToolbar.getFloat($("#txtYingYe").val());
//        			$("#txtToCompanyYingYe").val(yingye);
//        			$("#txtToGuideYingYe").val(yingye);
//        			$("#txtToLeaderYingYe").val(yingye);
        			var adult = tableToolbar.getInt($("#txtAdult").val());
        			var child = tableToolbar.getInt($("#txtChild").val());
        			$("#txtToCompanyRenTou").val(tableToolbar.calculate(
        				tableToolbar.calculate(
        					tableToolbar.getFloat($("#txtAdultMoney").val())
                	                        , adult
                	                        , "*")
                    , tableToolbar.calculate(
                    	tableToolbar.getFloat($("#txtChildMoney").val())
                	                        , child
                	                        , "*")
                    , "+"));
        			$("#txtToCompanyRenShu").val(tableToolbar.calculate(adult, child, "+"));
        			$("#txtToCompanyRenShu2").val(tableToolbar.calculate(adult, child, "+"));
        			$("#txtToCompanyBaoDi").val(tableToolbar.getFloat($("#txtBaoDi").val()));
        			var tr = $("table[class=autoAdd]").find("tr[class=tempRow]");
        			var t = 0;
        			$.each(tr, function(i) {
        				var s = $(this).find("input[name=txtShuLiang]").val();
        				var f = $(this).find("input[name=txtFanDian]").val();
        				var h = tableToolbar.calculate(tableToolbar.getFloat(f), tableToolbar.getInt(s), "*");
        				t = tableToolbar.calculate(t, h, "+");
        			});
        			$("#txtToCompanyFanDian").val(t);

        			//交给公司合计
        			$("#txtToCompanyTotal").val(tableToolbar.calculate(
        				tableToolbar.calculate(
        					tableToolbar.calculate(
        						tableToolbar.getFloat($("#txtToCompanyRenTou").val())
        						, tableToolbar.calculate(
        							tableToolbar.calculate(
        								tableToolbar.getFloat($("#txtToCompanyBaoDi").val())
        			                    , tableToolbar.getInt($("#txtToCompanyRenShu").val())
        			                    , "*"
        							)
        							, tableToolbar.calculate(
        								tableToolbar.getFloat($("#txtToCompanyBaoDi2").val())
        			                    , tableToolbar.getInt($("#txtToCompanyRenShu2").val())
        			                    , "*"
        							)
        			                , "+"
        						)
        			            , "+"
        					)
        			        , tableToolbar.getFloat($("#txtToCompanyFanDian").val())
        			        , "+"
        				)
        			    , tableToolbar.calculate(tableToolbar.getFloat($("#txtToCompanyYingYe").val()), tableToolbar.calculate(tableToolbar.getFloat($("#txtToCompanyTiQu").val()), 100, "/"), "*")
        			    , "+"
        			));
        			//交给导游合计
        			$("#txtToGuideTotal").val(tableToolbar.calculate(
        				tableToolbar.calculate($("#txtToGuideYingYe").val(), tableToolbar.calculate($("#txtToGuideTiQu").val(), 100, "/"), "*")
        			    , tableToolbar.calculate(
        			    	tableToolbar.getFloat($("#txtToGuideLu").val())
        			        , tableToolbar.calculate(
        			        	tableToolbar.getFloat($("#txtToGuideShui").val())
        			            , tableToolbar.calculate(
        			            	tableToolbar.getFloat($("#txtToGuidePei").val())
        			                , tableToolbar.calculate(
        			                	tableToolbar.getFloat($("#txtToGuideJiao").val())
        			                    , tableToolbar.calculate(
        			                    	tableToolbar.getFloat($("#txtToGuideOther").val())
        			                        , tableToolbar.getFloat($("#txtToGuideLiuShui").val())
        			                        , "+"
        			                    )
        			                	, "+"
        			                )
        			            	, "+"
        			            )
        			            , "+"
        			        )
        			    	, "+"
        			    )
        			    , "-"
        			));
        			//交给领队合计
        			$("#txtToLeaderTotal").val(tableToolbar.calculate(
        				tableToolbar.getFloat($("#txtToLeaderYingYe").val())
        			    , tableToolbar.calculate(tableToolbar.getFloat($("#txtToLeaderTiQu").val()), 100, "/")
        			    , "*"
        			));
        		});
        	},
        	ChangeDateFormat: function(val) {
        		if (val != null) {
        			var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        			//月份为0-11，所以+1，月份小于10时补个0
        			var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        			var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        			return date.getFullYear() + "-" + month + "-" + currentDate;
        		}

        		return "";
        	},
        	PageInit: function() {
        		var that = this;
        		that.BindBtn();
        		if (BaoZhangGouWu == null) return;
        		$("#<%=this.lblGouWuDian.ClientID %>").text(BaoZhangGouWu.SourceName);
        		$("#<%=this.lblLianXiRen.ClientID %>").text(BaoZhangGouWu.ContactName);
        		$("#<%=this.lblLianXiTel.ClientID %>").text(BaoZhangGouWu.ContactPhone);
        		$("#<%=this.lblLianXiFax.ClientID %>").text(BaoZhangGouWu.ContactFax);
        		$("#<%=this.lblJinDianDate.ClientID %>").text(Page.ChangeDateFormat(BaoZhangGouWu.StartDate));
        		$("#<%=this.lblDaoYouXuZhi.ClientID %>").text(BaoZhangGouWu.GuideNotes);
        		$("#<%=this.lblRemark.ClientID %>").text(BaoZhangGouWu.Remarks);
        		$("#<%=this.lblGuoJi.ClientID %>").text(BaoZhangGouWu.Country);
        		$("#txtLiuShui").val(BaoZhangGouWu.LiuShui);
        		$("#txtBaoDi").val(BaoZhangGouWu.BaoDi);
        		$("#txtYingYe").val(BaoZhangGouWu.YingYe);
        		$("#txtAdultMoney").val(BaoZhangGouWu.PeopleMoney);
        		$("#txtChildMoney").val(BaoZhangGouWu.ChildMoney);
        		$("#txtAdult").val(BaoZhangGouWu.Adult);
        		$("#txtChild").val(BaoZhangGouWu.Child);
        		$("#txtToCompanyRenTou").val(BaoZhangGouWu.ToCompanyRenTou);
        		$("#txtToCompanyBaoDi").val(BaoZhangGouWu.ToCompanyBaoDi);
        		$("#txtToCompanyRenShu").val(BaoZhangGouWu.ToCompanyRenShu);
        		$("#txtToCompanyBaoDi2").val(BaoZhangGouWu.ToCompanyBaoDi2);
        		$("#txtToCompanyRenShu2").val(BaoZhangGouWu.ToCompanyRenShu2);
        		$("#txtToCompanyFanDian").val(BaoZhangGouWu.ToCompanyFanDian);
        		$("#txtToCompanyYingYe").val(BaoZhangGouWu.ToCompanyYingYe);
        		$("#txtToCompanyTiQu").val(BaoZhangGouWu.ToCompanyTiQu*100);
        		$("#txtToCompanyTotal").val(BaoZhangGouWu.ToCompanyTotal);
        		$("#txtToGuideYingYe").val(BaoZhangGouWu.ToGuideYingYe);
        		$("#txtToGuideTiQu").val(BaoZhangGouWu.ToGuideTiQu*100);
        		$("#txtToGuideLu").val(BaoZhangGouWu.ToGuideLu);
        		$("#txtToGuideShui").val(BaoZhangGouWu.ToGuideShui);
        		$("#txtToGuidePei").val(BaoZhangGouWu.ToGuidePei);
        		$("#txtToGuideJiao").val(BaoZhangGouWu.ToGuideJiao);
        		$("#txtToGuideOther").val(BaoZhangGouWu.ToGuideOther);
        		$("#txtToGuideLiuShui").val(BaoZhangGouWu.ToGuideLiuShui);
        		$("#txtToGuideTotal").val(BaoZhangGouWu.ToGuideTotal);
        		$("#txtToLeaderYingYe").val(BaoZhangGouWu.ToLeaderYingYe);
        		$("#txtToLeaderTiQu").val(BaoZhangGouWu.ToLeaderTiQu*100);
        		$("#txtToLeaderTotal").val(BaoZhangGouWu.ToLeaderTotal);
        	}
        };
        $(function() {
        	Page.PageInit();
        });
    </script>
</html>
