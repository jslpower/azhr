using System;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Collections;

namespace Adpost.Common.ExporPage
{
    /// <summary>
    /// ��ҳ�Զ���ؼ�
    /// </summary>
    [DefaultProperty("Text"),
    ToolboxData("<{0}:ExporPageInfoSelect runat=server></{0}:ExporPageInfoSelect>")]
    public class ExporPageInfoSelect : ExporPaging
    {
        private string _AllPageCssClass = "hong18";
        private Hashtable _EventSelectHt = new Hashtable();  //select�ؼ��ϵ��¼�
        private Hashtable _EventLinkHt = new Hashtable();  //��ҳ�����ϵ��¼�
        private HrefTypeEnum _HrefType = HrefTypeEnum.UrlHref;   //��ת������
        private PageStyleTypeEnum _pageStyleType = PageStyleTypeEnum.Select;
        private int _pageLinkCount = 10;
        private bool _isInitCssStyle = true;
        private bool _isInitJs = true;
        private int _startEndPageCount = 2;
        private ButtonColor _buttoncolorstyle = ButtonColor.Blue;

        #region ��������
        /// <summary>
        /// ��ť����ɫ(������PageStyleTypeEnum!=Select����ʽ) Ĭ��ΪBlue
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(ButtonColor.Blue)]
        public virtual ButtonColor ButtonColorStyle
        {
            get { return this._buttoncolorstyle; }
            set { this._buttoncolorstyle = value; }
        }
        /// <summary>
        /// �ж��Ƿ����js,Ĭ��Ϊ���
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(true)]
        public virtual bool IsInitJs
        {
            get
            {
                return _isInitJs;
            }

            set
            {
                _isInitJs = value;
            }
        }

        /// <summary>
        /// �ж��Ƿ����css��ʽ,Ĭ��Ϊ���
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(true)]
        public virtual bool IsInitCssStyle
        {
            get
            {
                return _isInitCssStyle;
            }

            set
            {
                _isInitCssStyle = value;
            }
        }

        /// <summary>
        /// ��β�ķ�ҳ���ֵĸ���(��ʱֻ��PageStyleType=PageStyleTypeEnum.NewButton���͵�����������) Ĭ��Ϊ2
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(2)]
        public virtual int PageLinkStartEndCount
        {
            get
            {
                return _startEndPageCount;
            }

            set
            {
                _startEndPageCount = value;
            }
        }

        /// <summary>
        /// ���÷�ҳ��ʾ�����Ӹ���(��ʱֻ��PageStyleType=PageStyleTypeEnum.NewButton���͵�����������) Ĭ��Ϊ10
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(10)]
        public virtual int PageLinkCount
        {
            get
            {
                return _pageLinkCount;
            }

            set
            {
                _pageLinkCount = value;
            }
        }

        /// <summary>
        /// ��ҳ�ؼ�����ʽ,Ĭ��Ϊselect��ʽ��
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(PageStyleTypeEnum.Select)]
        public virtual PageStyleTypeEnum PageStyleType
        {
            get
            {
                return _pageStyleType;
            }

            set
            {
                _pageStyleType = value;
            }
        }

        /// <summary>
        /// "�ܵ�ҳ��" ��ʾ��CSS��ʽ  Ĭ��Ϊ��ɫ��hong18��ʽ��
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("hong18")]
        public virtual string AllPageCssClass
        {
            get
            {
                return _AllPageCssClass;
            }

            set
            {
                _AllPageCssClass = value;
            }
        }

        /// <summary>
        /// ������ת������(0:urlҳ����ת 1:ͨ��js��ajax��ҳ����ת) Ĭ��Ϊ0
        /// ��Ϊ1��ʱ����Ի�õ�ǰ�����ϵ�valueֵ,value��Ϊ��ǰ�����ϵ�page��ֵ
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(HrefTypeEnum.UrlHref)]
        public virtual HrefTypeEnum HrefType
        {
            get
            {
                return _HrefType;
            }

            set
            {
                _HrefType = value;
            }
        }
        #endregion

        /// <summary>
        /// ���ÿؼ����¼����ͺ��¼�����
        /// </summary>
        /// <param name="keyEventName">�¼�����(��:onclick, onchange)</param>
        /// <param name="valueFunctionName">�¼����õĺ���</param>
        /// <param name="ControlType">Ҫ����¼��Ŀؼ�����(Type=0:select�ؼ� 1:����������¼�)</param>
        public void AttributesEventAdd(string keyEventName, string valueFunctionName, int ControlType)
        {
            switch (ControlType)
            {
                case 0:
                    if (!_EventSelectHt.ContainsKey(keyEventName))
                        _EventSelectHt.Add(keyEventName, valueFunctionName);
                    break;
                case 1:
                    if (!_EventLinkHt.ContainsKey(keyEventName))
                        _EventLinkHt.Add(keyEventName, valueFunctionName);
                    break;
            }
        }


        /// <summary>
        /// ע��Ҫ���ɵ�js
        /// </summary>
        private string RegisterJs()
        {
            //���Ĭ�ϵ�js����
            StringBuilder strJs = new StringBuilder();
            if (IsInitJs && HrefType == HrefTypeEnum.UrlHref && PageStyleType == PageStyleTypeEnum.Select)  //Ҫ��ʼ��js����ҳͨ��urlhref��ת,������select��ʽ�ķ�ҳ
            {   //URL��ʽ��ת
                strJs.Append("<script lanuage='javascript' type='text/javascript'>");
                strJs.Append("function ExporPageInfoSelect_Change(obj){");
                //string url = PageLinkURL + BuildUrlString(UrlParams);
                string url = "";
                if (PageLinkURL == "?")
                    url = "?" + BuildUrlString(UrlParams);
                else
                    url = PageLinkURL;

                strJs.AppendFormat("window.location.href='{0}Page=' + obj.value;", url);
                strJs.Append("}</script>");

                //��ʼ��Ĭ�ϵ��¼�
                //AttributesEventAdd("onchange", "ExporPageInfoSelect_Change(this)", 0);
            }
            else if (IsInitJs && HrefType == HrefTypeEnum.JsHref)
            {
                strJs.Append("<script lanuage='javascript' type='text/javascript'>");
                strJs.Append("var exporpage = { getgotopage : function(obj){var gotopage = $(obj).attr('gotoPage'); if(gotopage==undefined)gotopage = obj.value; return gotopage;} }");
                strJs.Append("</script>");
            }
            return strJs.ToString();
        }

        /// <summary>
        /// ע��Ҫ���ɵ�css��ʽ
        /// </summary>
        private string RegisterCssStyle()
        {
            //���Ĭ�ϵ�css��ʽ
            StringBuilder strCss = new StringBuilder();
            if (IsInitCssStyle)
            {
                strCss.Append("<style>");
                strCss.Append(".hong18 {color: #bc2931;}");
                strCss.Append("</style>");
                //if (PageStyleType != PageStyleTypeEnum.Select)  //Ϊ�µ���ʽ
                //{
                //    switch (this._buttoncolorstyle)
                //    {
                //        case ButtonColor.Blue:
                //            cssPath =  "/ExporPageResources/Css/BlueButton.css";
                //            break;
                //        case ButtonColor.Green:
                //            cssPath = "/ExporPageResources/Css/GreenButton.css";
                //            break;
                //    }

                //}
                //strCss.AppendFormat("<link href=\"{0}\" type=\"text/css\" rel=\"stylesheet\" />", cssPath);
            }
            return strCss.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            base.IsInitBaseCssStyle = false;
            base.OnInit(e);
        }

        private bool _selfVisible = true;

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = true;
                _selfVisible = value;
            }
        }

        /// <summary> 
        /// ���˿ؼ����ָ�ָ�������������
        /// </summary>
        /// <param name="output"> Ҫд������ HTML ��д�� </param>
        protected override void Render(HtmlTextWriter output)
        {
            string recordCountScript = string.Format(@"<script type=""text/javascript"">
//<![CDATA[
var pagingRecordCount={0};//]]>
</script>
", intRecordCount.ToString());

            if (!_selfVisible)
            {
                output.Write(recordCountScript);
                return;
            }

            //InitCssStyle();
            #region ���������ʼ��
            StringBuilder tmpReturnValue = new StringBuilder();
            int intPageCount = 0;   //�ܵ�ҳ��            
            StringBuilder strJs = new StringBuilder(); //�����ϵ��¼�
            if (HrefType == HrefTypeEnum.JsHref)
            {
                //���������ϵ��¼�
                if (_EventLinkHt != null && _EventLinkHt.Count > 0)
                {
                    foreach (string key in _EventLinkHt.Keys)
                    {
                        strJs.AppendFormat(" {0}='{1}' ", key, _EventLinkHt[key]);
                    }
                }
            }

            #region �޸ĺ�Ĵ���
            //if (intRecordCount == 0)
            //    intRecordCount = 1;
            if (!IsUrlRewrite)
            {
                PageLinkURL = PageLinkURL + BuildUrlString(UrlParams);
            }
            if (intRecordCount > 0)
            {
                if (intRecordCount % intPageSize == 0)
                {
                    intPageCount = Convert.ToInt32(intRecordCount / intPageSize);
                }
                else
                {
                    intPageCount = Convert.ToInt32(intRecordCount / intPageSize) + 1;
                }
            }
            #endregion

            if (intPageCount == 0)
                intPageCount = 1;

            if (CurrencyPage <= 0)  //����ǰҳ��Ϊ0��С��0,��Ĭ��Ϊ�ڵ�ǰҳ
                CurrencyPage = 1;
            else if (CurrencyPage > intPageCount)
                CurrencyPage = intPageCount;
            #endregion

            //Ϊѡ�����������ʽ
            if (PageStyleType == PageStyleTypeEnum.Select)
            {
                //tmpReturnValue.AppendFormat("<span>ÿҳ��ʾ{0}�� ��{1}����Ϣ��</span>", intPageSize, intRecordCount);
                tmpReturnValue.AppendFormat("��<span class='{0}'><strong>{1}</strong></span>/<span class='{2}'><strong>{3}</strong></span>ҳ&nbsp;&nbsp;|&nbsp;&nbsp;", CurrencyPageCssClass, CurrencyPage, AllPageCssClass, intPageCount);
                #region ��������ҳ��
                tmpReturnValue.Append("<select class='inputselect' onchange='ExporPageInfoSelect_Change(this)'");

                //����select���¼�
                if (_EventSelectHt != null && _EventSelectHt.Count > 0)
                {
                    foreach (string key in _EventSelectHt.Keys)
                    {
                        tmpReturnValue.AppendFormat(" {0}='{1}' ", key, _EventSelectHt[key]);
                    }
                }

                tmpReturnValue.Append(">");
                for (int i = 1; i <= intPageCount; i++)
                {
                    if (CurrencyPage == i)
                        tmpReturnValue.AppendFormat("<option value='{0}' selected='selected'>{0}</option>", i);
                    else
                        tmpReturnValue.AppendFormat("<option value='{0}'>{0}</option>", i);
                }
                tmpReturnValue.Append("</select>&nbsp;&nbsp;");
                #endregion

                #region ��������
                if (intPageCount == 1)
                {
                    tmpReturnValue.Append("��ҳ ��һҳ ��һҳ βҳ");
                }
                else
                {
                    switch (Convert.ToInt32(HrefType))
                    {
                        case 0: //url��href��ת
                            //��ҳ
                            tmpReturnValue.AppendFormat("<span class='{0}'><a href='{1}Page=1'>��ҳ</a></span>", LinkCssClass, PageLinkURL);

                            //��һҳ
                            if (CurrencyPage == 1)  //��ǰҳΪ��1ҳ
                            {
                                if (intPageCount > 1)
                                    tmpReturnValue.AppendFormat("<span class='{0}'> ��һҳ <a href='{1}Page=2'>��һҳ</a> </span>", LinkCssClass, PageLinkURL);
                                else if (intPageCount == 1)
                                    tmpReturnValue.AppendFormat("<span class='{0}'> ��һҳ ��һҳ </span>", LinkCssClass);
                            }
                            else if (CurrencyPage == intPageCount)  //��ǰҳΪ���1ҳ
                                tmpReturnValue.AppendFormat("<span class='{0}'> <a href='{1}Page={2}'>��һҳ</a> ��һҳ </span>", LinkCssClass, PageLinkURL, intPageCount - 1);
                            else
                                tmpReturnValue.AppendFormat(" <span class='{0}'><a href='{1}Page={2}'>��һҳ</a> <a href='{1}Page={3}'>��һҳ</a></span> ", LinkCssClass, PageLinkURL, CurrencyPage - 1, CurrencyPage + 1);

                            tmpReturnValue.AppendFormat("<span class='{0}'><a href='{1}Page={2}'>βҳ</a></span>", LinkCssClass, PageLinkURL, intPageCount);
                            break;
                        case 1:   //ͨ��js�е�ajax��ת      
                            //��ҳ
                            tmpReturnValue.AppendFormat("<span class='{0}' gotoPage='1' {1}><a href='javascript:void(0);'>��ҳ</a></span>", LinkCssClass, strJs);

                            //��һҳ
                            if (CurrencyPage == 1)  //��ǰҳΪ��1ҳ
                                tmpReturnValue.AppendFormat("<span class='{0}'> ��һҳ <span gotoPage='2' {1}><a href='javascript:void(0);'>��һҳ</a></span> </span>", LinkCssClass, strJs);
                            else if (CurrencyPage == intPageCount)  //��ǰҳΪ���1ҳ
                                tmpReturnValue.AppendFormat("<span class='{0}'> <span gotoPage='{2}' {1}><a href='javascript:void(0);'>��һҳ</a></span> ��һҳ </span>", LinkCssClass, strJs, intPageCount - 1);
                            else
                                tmpReturnValue.AppendFormat(" <span class='{0}' gotoPage='{2}' {1}><a href='javascript:void(0);'>��һҳ</a></span> <span gotoPage={3} {1}><a href='javascript:void(0);'>��һҳ</a></span> ", LinkCssClass, strJs, CurrencyPage - 1, CurrencyPage + 1);

                            tmpReturnValue.AppendFormat("<span class='{0}' gotoPage='{2}' {1}><a href='javascript:void(0);'>βҳ</a></span>", LinkCssClass, strJs, intPageCount);
                            break;
                    }
                }
                #endregion
            }
            else  //Ϊ�µ���ʽ
            {
                tmpReturnValue.Append("<div class='diggPage'>");

                //Ϊ��򵥵ķ�ҳ��ʽ
                if (PageStyleType != PageStyleTypeEnum.MostEasyNewButtonStyle)
                {
                    //����ҳ��ǰ���ͳ��
                    tmpReturnValue.AppendFormat("<span>ÿҳ��ʾ{0}�� ��{1}����Ϣ����{2}ҳ</span>", intPageSize, intRecordCount, intPageCount);
                }

                #region ����ÿҳ������
                //��һҳ
                if (CurrencyPage == 1)  //��ǰҳΪ��1ҳ
                {
                    if (PageStyleType != PageStyleTypeEnum.MostEasyNewButtonStyle)
                    {

                        tmpReturnValue.Append("<span class='disabled'> ��һҳ </span>"); //��һҳ
                    }


                    //�м�ҳ��������---------begin------                    
                    tmpReturnValue.Append(GetPageNumLink(intPageCount, strJs.ToString()));
                    //�м�ҳ��������----------end-----

                    if (PageStyleType != PageStyleTypeEnum.MostEasyNewButtonStyle)
                    {
                        if (intPageCount > 1)
                        {
                            if (HrefType == HrefTypeEnum.UrlHref)
                                tmpReturnValue.AppendFormat("<a href='{0}'> ��һҳ </a>", BuildUrlStr(PageLinkURL, "2")); //��һҳ
                            else if (HrefType == HrefTypeEnum.JsHref)
                                tmpReturnValue.AppendFormat("<span gotoPage='2' {0}><a href='javascript:void(0);'> ��һҳ </a></span>", strJs); //��һҳ
                        }
                        else if (intPageCount == 1)
                            tmpReturnValue.Append("<span class='disabled'> ��һҳ </span>"); //��һҳ
                    }
                }
                else if (CurrencyPage == intPageCount)  //��ǰҳΪ���1ҳ
                {
                    if (PageStyleType != PageStyleTypeEnum.MostEasyNewButtonStyle)
                    {
                        if (HrefType == HrefTypeEnum.UrlHref)
                            tmpReturnValue.AppendFormat("<a href='{0}'> ��һҳ </a>", BuildUrlStr(PageLinkURL, Convert.ToString(CurrencyPage - 1))); //��һҳ
                        else if (HrefType == HrefTypeEnum.JsHref)
                            tmpReturnValue.AppendFormat("<span gotoPage='{0}' {1}><a href='javascript:void(0);'> ��һҳ </a></span>", CurrencyPage - 1, strJs);

                    }

                    //�м�ҳ��������---------begin------                    
                    tmpReturnValue.Append(GetPageNumLink(intPageCount, strJs.ToString()));
                    //�м�ҳ��������----------end-----    

                    if (PageStyleType != PageStyleTypeEnum.MostEasyNewButtonStyle)
                    {
                        tmpReturnValue.Append("<span class='disabled'> ��һҳ </span>"); //��һҳ
                    }
                }
                else  //��ǰҳΪ�м�ҳ
                {
                    if (PageStyleType != PageStyleTypeEnum.MostEasyNewButtonStyle)
                    {
                        if (HrefType == HrefTypeEnum.UrlHref)
                            tmpReturnValue.AppendFormat("<a href='{0}'> ��һҳ </a>", BuildUrlStr(PageLinkURL, Convert.ToString(CurrencyPage - 1))); //��һҳ
                        else if (HrefType == HrefTypeEnum.JsHref)
                            tmpReturnValue.AppendFormat("<span gotoPage='{0}' {1}><a href='javascript:void(0);'> ��һҳ </a></span>", CurrencyPage - 1, strJs);

                    }

                    //�м�ҳ��������---------begin------                    
                    tmpReturnValue.Append(GetPageNumLink(intPageCount, strJs.ToString()));
                    //�м�ҳ��������----------end-----      

                    if (PageStyleType != PageStyleTypeEnum.MostEasyNewButtonStyle)
                    {
                        if (HrefType == HrefTypeEnum.UrlHref)
                            tmpReturnValue.AppendFormat("<a href='{0}'> ��һҳ </a>", BuildUrlStr(PageLinkURL, Convert.ToString(CurrencyPage + 1))); //��һҳ
                        else if (HrefType == HrefTypeEnum.JsHref)
                            tmpReturnValue.AppendFormat("<span gotoPage='{0}' {1}><a href='javascript:void(0);'> ��һҳ </a></span>", CurrencyPage + 1, strJs);
                    }
                }
                #endregion

                tmpReturnValue.Append("</div>");
            }

            string strJsOut = RegisterJs();
            string strCss = RegisterCssStyle();

            output.Write(recordCountScript + strJsOut + strCss + tmpReturnValue.ToString());
        }

        #region ����м�ķ�ҳ����
        /// <summary>
        /// ���ÿһ����ҳ������
        /// </summary>
        /// <param name="pageIndex">ҳ������</param>
        /// <param name="currentPage">��ǰҳ</param>
        /// <param name="hrefType">������ת������</param>
        /// <param name="strJs">ͨ��js��ҳ��ʱ���js�¼�</param>
        /// <param name="pageLinkURL">��ҳurl����</param>
        /// <returns></returns>
        private string GetOnePageNumLink(int pageIndex, int currentPage, HrefTypeEnum hrefType, string strJs, string pageLinkURL)
        {
            StringBuilder tmpReturnValue = new StringBuilder();
            if (pageIndex == currentPage)
            {
                tmpReturnValue.AppendFormat("<span class='current'>{0}</span>", pageIndex);   //��ǰҳ
            }
            else
            {
                //����м��ÿһ������
                if (hrefType == HrefTypeEnum.UrlHref)
                    tmpReturnValue.AppendFormat("<a href='{0}'>{1}</a>", BuildUrlStr(pageLinkURL, Convert.ToString(pageIndex)), pageIndex);
                else if (hrefType == HrefTypeEnum.JsHref)
                    tmpReturnValue.AppendFormat("<span gotoPage='{0}' {1}><a href='javascript:void(0);'>{0}</a></span>", pageIndex, strJs);
            }
            return tmpReturnValue.ToString();
        }
        /// <summary>
        /// txb
        /// </summary>
        /// <param name="url"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private string BuildUrlStr(string url, string page)
        {
            if (IsUrlRewrite)
            {
                return url.Replace(Placeholder, page);
            }
            else
            {
                return PageLinkURL + "Page=" + Convert.ToString((int.Parse(page)));
            }
        }

        /// <summary>
        /// ����м�ķ�ҳ����
        /// </summary>
        /// <param name="intPageCount">�ܵķ�ҳ��</param>
        /// <param name="strJs">ͨ��js��ҳ��ʱ���js�¼�</param>
        /// <returns></returns>
        private string GetPageNumLink(int intPageCount, string strJs)
        {
            StringBuilder tmpReturnValue = new StringBuilder();
            int pageIndex = 1;   //ҳ����
            int startEndPageCount = PageLinkStartEndCount;   //��β�ķ�ҳ���ֵĸ���
            int middlePageCount = PageLinkCount - PageLinkStartEndCount * 2;  //�м�ķ�ҳ���ֵĸ���(����С��3)
            if (middlePageCount < 3)
                return "PageLinkCount���� - PageLinkStartEndCount*2���Բ���С��3��";
            if (intPageCount <= PageLinkCount)   //ֱ�����ÿҳ
                for (pageIndex = 1; pageIndex <= intPageCount; pageIndex++)
                    tmpReturnValue.AppendFormat(GetOnePageNumLink(pageIndex, CurrencyPage, HrefType, strJs, PageLinkURL));
            else if (intPageCount > PageLinkCount)
            {
                //�ж��Ƿ�Ҫ���ǰ���...
                bool isHasStartPoint = false;
                //�ж��Ƿ�Ҫ��������...
                bool isHasEndPoint = false;
                if (CurrencyPage >= startEndPageCount + middlePageCount)
                    isHasStartPoint = true;
                if (CurrencyPage <= intPageCount - (startEndPageCount + middlePageCount) + 1)
                    isHasEndPoint = true;
                if (!isHasStartPoint && !isHasEndPoint && intPageCount > PageLinkCount)
                    isHasEndPoint = true;

                //�����ʼ��ҳ��
                for (pageIndex = 1; pageIndex <= startEndPageCount; pageIndex++)
                    tmpReturnValue.AppendFormat(GetOnePageNumLink(pageIndex, CurrencyPage, HrefType, strJs, PageLinkURL));

                //�����ʼ��...
                if (isHasStartPoint)   //�����ʼ��...
                {
                    tmpReturnValue.AppendFormat("...");

                    //�ж��Ƿ�Ҫ���������...,������м�ķ�ҳҳ��
                    if (isHasEndPoint)  //���������...      
                    {
                        pageIndex = CurrencyPage - middlePageCount / 2;
                        if (middlePageCount % 2 == 0)
                            pageIndex += 1;
                        int end = CurrencyPage + middlePageCount / 2;
                        for (; pageIndex <= end; pageIndex++)
                            tmpReturnValue.AppendFormat(GetOnePageNumLink(pageIndex, CurrencyPage, HrefType, strJs, PageLinkURL));
                    }
                    else  //�����������...
                    {
                        pageIndex = intPageCount - (middlePageCount + startEndPageCount - 1);
                        int end = intPageCount - startEndPageCount;
                        for (; pageIndex <= end; pageIndex++)
                            tmpReturnValue.AppendFormat(GetOnePageNumLink(pageIndex, CurrencyPage, HrefType, strJs, PageLinkURL));
                    }
                }
                else  //�������ʼ��...
                {
                    //����м�ķ�ҳҳ��
                    int end = startEndPageCount + middlePageCount;
                    for (; pageIndex <= end; pageIndex++)
                    {
                        tmpReturnValue.AppendFormat(GetOnePageNumLink(pageIndex, CurrencyPage, HrefType, strJs, PageLinkURL));
                    }
                }

                //���������...
                if (isHasEndPoint)
                {
                    tmpReturnValue.AppendFormat("...");
                }

                //�������ҳ��
                for (pageIndex = intPageCount - (startEndPageCount - 1); pageIndex <= intPageCount; pageIndex++)
                    tmpReturnValue.AppendFormat(GetOnePageNumLink(pageIndex, CurrencyPage, HrefType, strJs, PageLinkURL));
            }

            return tmpReturnValue.ToString();
        }
        #endregion

        #region ������д���ֲ���ת��input

        /// <summary>
        /// ������д���ֲ���ת��input
        /// </summary>
        /// <param name="intPageCount">��ҳ��</param>
        /// <returns></returns>
        private string GetInput(int intPageCount)
        {
            StringBuilder strInput = new StringBuilder();
            StringBuilder strJs = new StringBuilder();

            #region ����js

            strJs.Append("<script type=\"text/javascript\" >");
            strJs.Append("function GoToPageNum(){");
            strJs.Append("var obj = document.getElementById('txtIndex');");
            strJs.Append("var num = 0;");
            strJs.Append("if(obj != null) num = obj.value;");
            strJs.Append("var r=/^[0-9]*[1-9][0-9]*$/;");
            strJs.Append("if(!r.test(num) || obj.value <= 0) return;");
            strJs.AppendFormat("var pageNumber = {0};", intPageCount);
            strJs.AppendFormat("if(num<=0||num>pageNumber||num=={0})return;", CurrencyPage);
            strJs.AppendFormat("window.location.href=\"{0}Page=\" + num;", PageLinkURL);
            strJs.Append("}");
            strJs.Append("function CheckKeyDown(){if(event.keyCode==13){GoToPageNum();event.returnValue=false;return false;}}</script>");
            strJs.Append("</script>");

            #endregion

            strInput.Append(strJs.ToString());
            strInput.Append("<span>��ת��<input type=\"text\" id=\"txtIndex\" size=\"3\"");
            strInput.Append(" onkeydown='CheckKeyDown();' onchange='GoToPageNum();' style='height:20px; width:35px;'");
            strInput.AppendFormat(" value=\"{0}\" />ҳ</span>", CurrencyPage);

            return strInput.ToString();
        }

        #endregion
    }

    #region ����ö����
    /// <summary>
    /// ������ת������(0:urlҳ����ת 1:ͨ��js��ajax��ҳ����ת) Ĭ��Ϊ0
    /// ��Ϊ1��ʱ����Ի�õ�ǰ�����ϵ�valueֵ,value��Ϊ��ǰ�����ϵ�page��ֵ
    /// </summary>
    public enum HrefTypeEnum
    {
        /// <summary>
        /// 0:urlҳ����ת
        /// </summary>
        UrlHref = 0,

        /// <summary>
        /// 1:ͨ��js��ajax��ҳ����ת,Ϊ1��ʱ����Ի�õ�ǰ�����ϵ�valueֵ,value��Ϊ��ǰ�����ϵ�page��ֵ
        /// </summary>
        JsHref = 1
    }

    /// <summary>
    /// ��ҳ��ʽ����ö��
    /// </summary>
    public enum PageStyleTypeEnum
    {
        /// <summary>
        /// �����������ʽ
        /// </summary>
        Select,

        /// <summary>
        /// �µİ�ť��ʽ
        /// </summary>
        NewButton,

        /// <summary>
        /// ��򵥵��°�ť��ʽ��ҳ�����ϡ���ҳ����ÿҳ���ٵ�������Ϣ��
        /// </summary>
        MostEasyNewButtonStyle,

        /// <summary>
        /// ��ԭʼ�ķ�ҳ��ʽ
        /// </summary>
        OldStyle,

        /// <summary>
        /// ��ԭʼ�ķ�ҳ����ÿ�����ַ�ҳ����ʽ
        /// </summary>
        OldNoEveryPageStyle,

        /// <summary>
        /// MQ���Һ�����ʽ
        /// </summary>
        MQSeachFriendStyle
    }

    /// <summary>
    /// ��ť����ɫö��(������PageStyleTypeEnum!=Select����ʽ)
    /// </summary>
    public enum ButtonColor
    {
        /// <summary>
        /// ��ɫ
        /// </summary>
        Blue,

        /// <summary>
        /// ��ɫ
        /// </summary>
        Green
    }
    #endregion
}
