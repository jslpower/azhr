<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerUnitSelect.ascx.cs"
    Inherits="Web.UserControl.CustomerUnitSelect" %>
<span id="sp_<%=this.ClientID %>">
    <%=ThisTitle %><input type="text" readonly="readonly" style="background-color: #dadada"
        name="<%=ClientNameKHMC %>" id="<%=ClientNameKHMC %>" value="<%=CustomerUnitName %>"
        class="<%=TxtCssClass %> " valid="required" errmsg="<%=BoxyTitle %>不能为空！" />
    <input type="hidden" name="<%=ClientNameKHBH %>" id="<%=ClientNameKHBH %>" value="<%=CustomerUnitId %>"
        valid="required" errmsg="<%=BoxyTitle %>输入无效,请重新输入或选择<%=BoxyTitle %>！" class="vartext" />
    <input type="hidden" name="<%=ClientNameKHLX %>" id="<%=ClientNameKHLX %>" value="<%=(int)CustomerUnitType %>" />
    <a href="/CommonPage/CustomerUnitSelect.aspx" class="xuanyong" title="<%=BoxyTitle %>"
        data-width="920" data-height="<%=IsUniqueness?410:510 %>"></a></span>

<script type="text/javascript">
    window['<%=this.ClientID %>'] = {
        /*
        设置参数
        data={
        CustomerUnitName:"客户单位名称",
        CustomerUnitId:"客户单位ID",
        CustomerUnitType"客户单位类型枚举int"
        }
        */
        SetVal: function(data) {
            if (!data) {
                if ('<%=SelectFrist %>' == 'True') {
                    $("#<%=ClientNameKHMC %>").val("");
                }
                $("#<%=ClientNameKHBH %>").val("");

                $("#<%=ClientNameKHLX %>").val("");
                return false;
            }
            $("#<%=ClientNameKHMC %>").val($.trim(data.CustomerUnitName || data.name));

            $("#<%=ClientNameKHBH %>").val($.trim(data.CustomerUnitId || data.id));

            $("#<%=ClientNameKHLX %>").val($.trim(data.CustomerUnitType));
        },
        /*
        返回类型
        data={
        CustomerUnitName:"客户单位名称",
        CustomerUnitId:"客户单位ID",
        CustomerUnitType"客户单位类型枚举int"
        }
        */
        GetVal: function() {
            return {
                CustomerUnitName: $("#<%=ClientNameKHMC %>").val(),
                CustomerUnitId: $("#<%=ClientNameKHBH %>").val(),
                CustomerUnitType: $("#<%=ClientNameKHLX %>").val(),
                type: window['<%=this.ClientID %>']["Data"] && window['<%=this.ClientID %>']["Data"]["type"] ? window['<%=this.ClientID %>']["Data"]["type"] : ""
            };
        },
        /*默认回调函数,该回调仅仅对客户单位名称，客户单位id，客户单位类型进行赋值 */
        BackFun: function(data) {
            this.SetVal(data);
            if (!data) {
                if ('<%=DefaultTab !=null%>' == "True") {
                    window['<%=this.ClientID %>']["Data"] = { type: '<%=(int?)DefaultTab %>' };
                    data = { type: '<%=(int?)DefaultTab %>' };
                }
            }
            this.Data = data;
        },
        Data: null
    }
    $(function() {
        if ('<%=DefaultTab !=null%>' == "True") {
            window['<%=this.ClientID %>']["Data"] = { type: '<%=(int?)DefaultTab %>' };
        } else {
            window['<%=this.ClientID %>']["Data"] = { CustomerUnitName: $("#<%=ClientNameKHMC %>").val() };
        }
        /* 自动匹配开始*/
        //        if ("<%=IsUniqueness %>" == "True") {
        //            $("#<%=ClientNameKHMC %>").autocomplete("/CommonPage/CustomerUnitSelect.aspx?" + $.param({ doType: "GetCustomerUnit" }), {
        //                width: 120,
        //                selectFirst: '<%=SelectFrist %>' == 'True',
        //                blur: '<%=SelectFrist %>' == 'True'
        //            }).result(function(e, data) {
        //                var BackFunData = {
        //                    CustomerUnitName: data[0],
        //                    CustomerUnitId: data[1],
        //                    CustomerUnitType: data[2],
        //                    CustomerUnitLV: data[3],
        //                    CustomerUnitContactId: data[4],
        //                    CustomerUnitContactName: data[5],
        //                    CustomerUnitMobilePhone: data[6],
        //                    CustomerUnitContactPhone: data[7],
        //                    CustomerUnitContactFax:data[8]
        //                }

        //                if (BackFunData.CustomerUnitId == "-1") {
        //                    BackFunData.CustomerUnitName = "";
        //                    window['<%=this.ClientID %>']["BackFun"](null);
        //                }
        //                else {
        //                    window['<%=this.ClientID %>']["BackFun"](BackFunData)
        //                    var callBackFunArr = '<%=CallBack %>'.length > 0 ? '<%=CallBack %>'.split('.') : null;
        //                    //存在回调函数
        //                    if (callBackFunArr) {
        //                        parents = window;
        //                        for (var item in callBackFunArr) {
        //                            if (callBackFunArr.hasOwnProperty(item)) {/*筛选掉原型链属性*/
        //                                parents = parents[callBackFunArr[item]];
        //                            }
        //                        }
        //                        parents(BackFunData);
        //                    }
        //                }
        //            });
        //        }
        /* 自动匹配结束*/

        /*传递参数,默认取后台配置的名称*/
        var para = {
            pIframeID: "<%=IframeID %>", /*父级IframeID*/
            IsUniqueness: "<%=IsUniqueness?1:-1 %>", /*是否只显示客户单位*/
            IsMultiple: "<%=IsMultiple %>", /*单选复选*/
            IsApply: "<%=IsApply?1:-1 %>", /*是否报名是选用*/
            thisClientID: '<%=this.ClientID %>'
        }
        newToobar.init(
        {
            box: "#sp_<%=this.ClientID %>",
            className: "xuanyong",
            para: para,
            callBackFun: "<%=CallBack %>" /*回调函数*/
        });
        $("#<%=ClientNameKHMC %>").keyup(function() {
            //客户单位数据
            if (window['<%=this.ClientID %>']["Data"] && window['<%=this.ClientID %>']["Data"]["CustomerUnitName"]) {
                if ($.trim($(this).val()) != window['<%=this.ClientID %>']["Data"]["CustomerUnitName"]) {
                    window['<%=this.ClientID %>']["BackFun"](null);
                }
            }
            //供应商
            if (window['<%=this.ClientID %>']["Data"] && window['<%=this.ClientID %>']["Data"]["name"]) {
                if ($.trim($(this).val()) != window['<%=this.ClientID %>']["Data"]["name"]) {
                    window['<%=this.ClientID %>']["BackFun"](null);
                }
            }
        })



    }) 
</script>

