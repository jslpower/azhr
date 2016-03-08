<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="DocList.aspx.cs" Inherits="Web.UserCenter.DocManage.DocList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="searchbox_01 fixed">
            <span class="searchT">
                <p>
                    文档名称：
                    <input type="text" class="formsize120">
                    上传人：
                    <input type="text" class="formsize80">
                    <button class="search-btn" type="button">
                        搜索</button></p>
            </span>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="gr-wendanggl-add.html">
                    <span>新增</span></a></li>
                <li class="line"></li>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="gr-wendanggl-update.html">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><s class="delicon"></s><a class="toolbar_delete" hidefocus="true" href="#"><span>
                    删除</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellspacing="0" cellpadding="0" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" class="th-line">
                            文档名称
                        </th>
                        <th align="center" class="th-line">
                            上传人
                        </th>
                        <th align="center" class="th-line">
                            上传时间
                        </th>
                        <th align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <tr>
                        <td align="center">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </td>
                        <td align="center">
                            文档管理标题
                        </td>
                        <td align="center">
                            刘芳
                        </td>
                        <td align="center">
                            2010-08-01
                        </td>
                        <td align="center">
                            <a href="#">下载</a>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="odd">
                                <td align="center">
                                    <input type="checkbox" id="checkbox2" name="checkbox2">
                                </td>
                                <td align="center">
                                    文档管理标题
                                </td>
                                <td align="center">
                                    刘芳
                                </td>
                                <td align="center">
                                    2010-08-01
                                </td>
                                <td align="center">
                                    <a href="#">下载</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0;" class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="gr-wendanggl-add.html">
                    <span>新增</span></a></li>
                <li class="line"></li>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="gr-wendanggl-update.html">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><s class="delicon"></s><a class="toolbar_delete" hidefocus="true" href="#"><span>
                    删除</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        //页面初始化必须存在方法
        $(function() {
            //绑定功能按钮
            BindBtn();
            //当列表页面出现横向滚动条时使用以下方法
            //需要左右滚动调用格式：$("需要滚动最外层选择器").moveScroll();
            //$('.tablelist-box').moveScroll();

            $(".ck_jl").click(function() {
                var data = dataBoxy();
                data.title = "查看";
                data.url = "/UserCenter/DocManage/DocEdit.aspx";
                data.width = "650px";
                data.height = "370px";
                ShowBoxy(data);
                return false;
            })
        })

        //绑定功能按钮
        function BindBtn() {
            //绑定Add事件
            $(".toolbar_add").click(function() {
                Add();
            })
            tableToolbar.init({
                tableContainerSelector: "#liststyle", //表格选择器
                objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                updateCallBack: function(objsArr) {
                    //修改
                    Update(objsArr);
                },
                deleteCallBack: function(objsArr) {
                    //删除(批量)
                    DelAll(objsArr);
                },
                copyCallBack: function(objsArr) {
                    //复制
                    Copy(objsArr)
                }
            })
        }
        
       
    </script>

    <script type="text/javascript">
        //使用弹窗方式添加，修改
        //添加(弹窗)
        function Add() {
            var data = dataBoxy();
            data.title = "新增";
            data.url = "/UserCenter/DocManage/DocEdit.aspx";
            data.width = "650px";
            data.height = "370px";
            ShowBoxy(data);
        }



        //修改(弹窗)---objsArr选中的TR对象
        function Update(objsArr) {
            var data = dataBoxy();
            data.title = "新增";
            data.url = "/UserCenter/DocManage/DocEdit.aspx?doType=update&id=" + objsArr[0].find("input[type='checkbox']").val();
            data.width = "650px";
            data.height = "370px";
            ShowBoxy(data);
        }

        //复制(弹窗)---objsArr选中的TR对象
        function Copy(objsArr) {
            alert("执行了复制")
        }

        //删除(批量)
        function DelAll(objArr) {
            //ajax执行文件路径,默认为本页面
            var ajaxUrl = "/UserCenter/DocManage/DocList.aspx";
            //定义数组对象
            var list = new Array();
            //遍历按钮返回数组对象
            for (var i = 0; i < objArr.length; i++) {
                //从数组对象中找到数据所在，并保存到数组对象中
                if (objArr[i].find("input[type='checkbox']").val() != "on") {
                    list.push(objArr[i].find("input[type='checkbox']").val());
                }
            }
            //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
            ajaxUrl += "?doType=delete&idList=" + list.join(',');
            //执行ajax
            GoAjax(ajaxUrl, EnglishToChanges.Ping(type))
        }

        //弹窗参数
        //弹窗默认参数
        function dataBoxy() {
            return {
                url: "",
                title: "",
                width: "",
                height: ""
            }
        };
        //显示弹窗
        function ShowBoxy(data) {
            Boxy.iframeDialog({
                iframeUrl: data.url,
                title: data.title,
                modal: true,
                width: data.width,
                height: data.height
            });
        }
       
    </script>

    <script type="text/javascript">
        //ajax请求
        function GoAjax(url, msg) {
            $.newAjax({
                type: "post",
                cache: false,
                url: url,
                dataType: "html",
                success: function(ret) {
                    //ajax回发提示
                    if (ret.toString() == "1") {
                        alert(msg + "成功！")
                        location.reload();
                    }
                    else {
                        alert(msg + ret)
                    }
                },
                error: function() {
                    //ajax异常--你懂得
                    alert("服务器忙！");
                }
            });
        }
    </script>

</asp:Content>
