<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TuanHaoXuanYong.ascx.cs" Inherits="EyouSoft.Web.UserControl.TuanHaoXuanYong" %>

<span id="sp_<%=this.ClientID %>">
    <%=ThisTitle %><input type="text" name="<%=ClientNameTourCode %>" id="<%=ClientNameTourCode %>"
        value="<%=TourCode %>" class="<%=TxtCssClass %>" valid="required" errmsg="<%=BoxyTitle %>不能为空！" />
    <input type="hidden" name="<%=ClientNameTourId %>" id="<%=ClientNameTourId %>" value="<%=TourId %>"
        valid="required" errmsg="<%=BoxyTitle %>输入无效,请重新输入或选择<%=BoxyTitle %>！" />
    <a href="/CommonPage/TuanHaoXuanYong.aspx" class="xuanyong" title="<%=BoxyTitle %>"
        data-width="600" data-height="510"></a></span>

<script type="text/javascript">
    window['<%=this.ClientID %>'] = {
        /*
        设置参数
        data={
        TourCode:"团号",
        TourId:"团队ID",
        }
        */
        SetVal: function(data) {
            if (!data) {
                if ('<%=SelectFrist %>' == 'True') {
                    $("#<%=ClientNameTourCode %>").val("");
                }
                $("#<%=ClientNameTourId %>").val("");

                return false;
            }
            $("#<%=ClientNameTourCode %>").val($.trim(data.TourCode));

            $("#<%=ClientNameTourId %>").val($.trim(data.TourId));

        },
        /*
        返回类型
        data={
        TourCode:"团号",
        TourId:"团队ID",
        }
        */
        GetVal: function() {
            return {
                TourCode: $("#<%=ClientNameTourCode %>").val(),
                TourId: $("#<%=ClientNameTourId %>").val()
            };
        },
        /*默认回调函数,该回调仅仅对团号，团队id进行赋值 */
        BackFun: function(data) {
            this.SetVal(data);
            this.Data = data;
        },
        Data: null
    }
    $(function() {
    	window['<%=this.ClientID %>']["Data"] = { TourCode: $("#<%=ClientNameTourCode %>").val() };
    	$("#<%=ClientNameTourCode %>").autocomplete("/CommonPage/TuanHaoXuanYong.aspx?" + $.param({ doType: "GetTourCode" }), {
    		width: 100,
    		selectFirst: '<%=SelectFrist %>' == 'True',
    		blur: '<%=SelectFrist %>' == 'True'
    	}).result(function(e, data) {
    		var BackFunData = {
    			TourCode: data[0],
    			TourId: data[1]
    		};

    		if (BackFunData.TourId == "-1") {
    			BackFunData.TourCode = "";
    			window['<%=this.ClientID %>']["BackFun"](null);
    		}
    		else {
    			window['<%=this.ClientID %>']["BackFun"](BackFunData);
    			var callBackFunArr = '<%=CallBack %>'.length > 0 ? '<%=CallBack %>'.split('.') : null;
    			//存在回调函数
    			if (callBackFunArr) {
    				parents = window;
    				for (var item in callBackFunArr) {
    					if (callBackFunArr.hasOwnProperty(item)) { /*筛选掉原型链属性*/
    						parents = parents[callBackFunArr[item]];
    					}
    				}
    				parents(BackFunData);
    			}
    		}
    	});

    	/*传递参数,默认取后台配置的名称*/
    	var para = {
    		pIframeID: "<%=IframeID %>", /*父级IframeID*/
    		IsMultiple: "<%=IsMultiple %>", /*单选复选*/
    		thisClientID: '<%=this.ClientID %>'
    	};
    	newToobar.init(
    		{
    			box: "#sp_<%=this.ClientID %>",
    			className: "xuanyong",
    			para: para,
    			callBackFun: "<%=CallBack %>" /*回调函数*/
    		});
    	$("#<%=ClientNameTourCode %>").keyup(function() {
    		//团号
    		if (window['<%=this.ClientID %>']["Data"] && window['<%=this.ClientID %>']["Data"]["TourCode"]) {
    			if ($.trim($(this).val()) != window['<%=this.ClientID %>']["Data"]["TourCode"]) {
    				window['<%=this.ClientID %>']["BackFun"](null);
    			}
    		}
    	});
    });
</script>