using System;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Adpost.Common.ExporPage
{
    /// <summary>
    /// ��ҳ�Զ���ؼ�
    /// </summary>
    [DefaultProperty("Text"),
      ToolboxData("<{0}:ExporPaging runat=server></{0}:ExporPaging>")]
    public class ExporPaging : System.Web.UI.WebControls.WebControl
    {
        private int _intRecordCount = 0, _CurrencyPage = 1, _intPageSize = 10, _LinkType = 3;
        private string _PageLinkURL = "", _CurrencyPageCssClass = "current", _LinkCssClass = "", _DivPageCssClass = "defaultcss", _NextBtnText = "��һҳ", _PrevBtnText = "��һҳ";
        private NameValueCollection _UrlParams = new NameValueCollection();
        private bool _isInitBaseCssStyle = true, _isUrlRewrite = false;//�Ƿ���д

        private string _Placeholder = "#PAGEINDEX#";//��ҳռλ��

        #region model
        /// <summary>
        /// �Ƿ����css��ʽ
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(true)]
        public virtual bool IsInitBaseCssStyle
        {
            get
            {
                return _isInitBaseCssStyle;
            }

            set
            {
                _isInitBaseCssStyle = value;
            }
        }

        /// <summary>
        /// ��ҳ��С(��ÿҳ��ʾ�ļ�¼��)
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(10)]
        public virtual int intPageSize
        {
            get
            {
                return _intPageSize;
            }

            set
            {
                _intPageSize = value;
            }
        }
        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(0)]
        public virtual int intRecordCount
        {
            get
            {
                return _intRecordCount;
            }

            set
            {
                _intRecordCount = value;
            }
        }
        /// <summary>
        /// ��ǰҳ����
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(1)]
        public virtual int CurrencyPage
        {
            get
            {
                return _CurrencyPage;
            }

            set
            {
                _CurrencyPage = value;
            }
        }
        /// <summary>
        /// ��ҳ���ӵ�ַ
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("")]
        public virtual string PageLinkURL
        {
            get
            {
                if (string.IsNullOrEmpty(_PageLinkURL))
                {
                    _PageLinkURL = System.Web.HttpContext.Current.Request.FilePath;
                }

                if (!_isUrlRewrite)
                {
                    //�ж�����?
                    if (_PageLinkURL.IndexOf("?") == -1)
                        _PageLinkURL = _PageLinkURL + "?";
                }
                return _PageLinkURL;
            }
            set
            {
                _PageLinkURL = value;
            }
        }
        /// <summary>
        /// ��ҳ��ʾ��ʽ
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(3)]
        public virtual int LinkType
        {
            get
            {
                return _LinkType;
            }

            set
            {
                _LinkType = value;
            }
        }
        /// <summary>
        /// ��ǰҳ������ʾCSS
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("current")]
        public virtual string CurrencyPageCssClass
        {
            get
            {
                return _CurrencyPageCssClass;
            }

            set
            {
                _CurrencyPageCssClass = value;
            }
        }
        /// <summary>
        /// ���ְ�ť����DIV����ʾCSS
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("defaultcss")]
        public virtual string DivPageCssClass
        {
            get
            {
                return _DivPageCssClass;
            }

            set
            {
                _DivPageCssClass = value;
            }
        }

        /// <summary>
        /// ��һҳ(��)��ť������
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("��һҳ")]
        public virtual string NextBtnText
        {
            get
            {
                return _NextBtnText;
            }

            set
            {
                _NextBtnText = value;
            }
        }

        /// <summary>
        /// ��һҳ(��)��ť������
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("��һҳ")]
        public virtual string PrevBtnText
        {
            get
            {
                return _PrevBtnText;
            }

            set
            {
                _PrevBtnText = value;
            }
        }

        /// <summary>
        /// ������ʾ��CSS
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("")]
        public virtual string LinkCssClass
        {
            get
            {
                return _LinkCssClass;
            }

            set
            {
                _LinkCssClass = value;
            }
        }

        /// <summary>
        /// url��������(��ͨ��System.Web.HttpContext.Current.Request.QueryString���)
        /// </summary>
        public virtual NameValueCollection UrlParams
        {
            get
            {
                if (_UrlParams == null || _UrlParams.Count == 0)
                {
                    _UrlParams = System.Web.HttpContext.Current.Request.QueryString;
                }
                return _UrlParams;
            }

            set
            {
                _UrlParams = value;
            }
        }

        /// <summary>
        /// ��ҳռλ��
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue("#PAGEINDEX#")]
        public virtual string Placeholder
        {
            get
            {////
                return _Placeholder;
            }
            set
            {
                _Placeholder = value;
            }
        }
        /// <summary>
        /// �Ƿ����url��д
        /// </summary>
        [Bindable(true), Category("Behavior"), DefaultValue(true)]
        public virtual bool IsUrlRewrite
        {
            get
            {
                if (PageLinkURL.IndexOf("aspx") >= 0)
                {
                    return false;
                }
                else
                    return _isUrlRewrite;
            }

            set
            {

                if (PageLinkURL.IndexOf("aspx") >= 0)
                {
                    _isUrlRewrite = false;
                }
                else
                    _isUrlRewrite = value;
            }
        }

        #endregion

        private string GetCssStyle()
        {
            StringBuilder strCss = new StringBuilder();
            if (IsInitBaseCssStyle)
            {
                #region д��Ĭ�ϵ�css��ʽ
                strCss.Append("<style type=\"text/css\">");
                strCss.Append("DIV.defaultcss {PADDING-RIGHT: 3px; PADDING-LEFT: 3px; FONT-SIZE: 0.85em; PADDING-BOTTOM: 3px; MARGIN: 3px; PADDING-TOP: 3px; FONT-FAMILY: Tahoma,Helvetica,sans-serif; TEXT-ALIGN: center}");
                strCss.Append("DIV.defaultcss A {BORDER-RIGHT: #ccdbe4 1px solid; PADDING-RIGHT: 8px; BACKGROUND-POSITION: 50% bottom; BORDER-TOP: #ccdbe4 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 2px; BORDER-LEFT: #ccdbe4 1px solid; COLOR: #0061de; MARGIN-RIGHT: 3px; PADDING-TOP: 2px; BORDER-BOTTOM: #ccdbe4 1px solid; TEXT-DECORATION: none}");
                strCss.Append("DIV.defaultcss A:hover {BORDER-RIGHT: #2b55af 1px solid; BORDER-TOP: #2b55af 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #2b55af 1px solid; COLOR: #fff; BORDER-BOTTOM: #2b55af 1px solid; BACKGROUND-COLOR: #4690B9}");
                strCss.Append("DIV.defaultcss A:active {BORDER-RIGHT: #2b55af 1px solid; BORDER-TOP: #2b55af 1px solid; BACKGROUND-IMAGE: none; BORDER-LEFT: #2b55af 1px solid; COLOR: #fff; BORDER-BOTTOM: #2b55af 1px solid; BACKGROUND-COLOR: #4690B9}");
                strCss.Append("DIV.defaultcss SPAN.current {PADDING-RIGHT: 6px; PADDING-LEFT: 6px; FONT-WEIGHT: bold; PADDING-BOTTOM: 2px; COLOR: #000; MARGIN-RIGHT: 3px; PADDING-TOP: 2px}");
                strCss.Append("DIV.defaultcss SPAN.disabled {DISPLAY: none}");
                strCss.Append("DIV.defaultcss A.next {BORDER-RIGHT: #ccdbe4 2px solid; BORDER-TOP: #ccdbe4 2px solid; MARGIN: 0px 0px 0px 10px; BORDER-LEFT: #ccdbe4 2px solid; BORDER-BOTTOM: #ccdbe4 2px solid}");
                strCss.Append("DIV.defaultcss A.next:hover {BORDER-RIGHT: #2b55af 2px solid; BORDER-TOP: #2b55af 2px solid; BORDER-LEFT: #2b55af 2px solid; BORDER-BOTTOM: #2b55af 2px solid}");
                strCss.Append("DIV.defaultcss A.prev {BORDER-RIGHT: #ccdbe4 2px solid; BORDER-TOP: #ccdbe4 2px solid; MARGIN: 0px 10px 0px 0px; BORDER-LEFT: #ccdbe4 2px solid; BORDER-BOTTOM: #ccdbe4 2px solid}");
                strCss.Append("DIV.defaultcss A.prev:hover {BORDER-RIGHT: #2b55af 2px solid; BORDER-TOP: #2b55af 2px solid; BORDER-LEFT: #2b55af 2px solid; BORDER-BOTTOM: #2b55af 2px solid}");
                strCss.Append("</style>");
                #endregion
            }

            return strCss.ToString();
        }

        /// <summary> 
        /// ���˿ؼ����ָ�ָ�������������
        /// </summary>
        /// <param name="output"> Ҫд������ HTML ��д�� </param>
        protected override void Render(HtmlTextWriter output)
        {
            int index = 1;
            string retval = "", retval2 = "", retval3 = "", tmpReturnValue = "";
            int intPageCount = 0, BasePage = 0, pageNumber = 0;  //intPageCount�ܵ�ҳ��
            string NumLinkClass = " class=\"" + CurrencyPageCssClass + "\"";
            string NumDivLinkClass = " class=\"" + DivPageCssClass + "\"";
            PageLinkURL = PageLinkURL + BuildUrlString(UrlParams);
            if (intRecordCount % intPageSize == 0)
            {
                intPageCount = Convert.ToInt32(intRecordCount / intPageSize);
            }
            else
            {
                intPageCount = Convert.ToInt32(intRecordCount / intPageSize) + 1;
            }

            //������ַ�ҳ
            BasePage = Convert.ToInt32((CurrencyPage / 10) * 10);
            retval += "<div" + NumDivLinkClass + ">";
            retval2 += "<div" + NumDivLinkClass + ">";
            retval3 += "<div" + NumDivLinkClass + ">";

            retval2 += " <span " + NumLinkClass + " >��" + intPageCount + "ҳ</span>";

            if (CurrencyPage > 1)
            {
                retval += " <a href=\"" + PageLinkURL + "Page=" + Convert.ToString((CurrencyPage - 1)) + "\">" + SafeRequest(PrevBtnText) + "</a>";
                retval2 += " <a href=\"" + PageLinkURL + "Page=" + Convert.ToString((CurrencyPage - 1)) + "\">" + SafeRequest(PrevBtnText) + "</a>";
            }

            //�ں�һ�鰴ťǰ�����ǰһ�鰴ť�����������ť
            if (BasePage > 0)
            {
                index = -1;
                retval3 += " <a href=\"" + PageLinkURL + "Page=" + Convert.ToString((BasePage - 11)) + "\">" + PrevBtnText + "</a>";
            }

            for (int i = index; i <= 10; i++)
            {
                pageNumber = BasePage + i;
                if (pageNumber > intPageCount)
                {
                    i = 11;
                }
                else
                {

                    if (pageNumber == CurrencyPage)
                    {
                        retval += " <span " + NumLinkClass + " >" + pageNumber.ToString() + "</span>";
                        retval2 += " <span " + NumLinkClass + " >" + pageNumber.ToString() + "</span>";
                        retval3 += " <span " + NumLinkClass + ">" + pageNumber.ToString() + "</span>"
;
                        //retval2 += "<span class=\"current\">" + pageNumber + "</span>";
                        //retval3 += "<span class=\"current\">" + pageNumber + "</span>";
                    }
                    else
                    {
                        retval += " <a href=\"" + PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
                        retval2 += " <a href=\"" + PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
                        retval3 += " <a href=\"" + PageLinkURL + "Page=" + pageNumber.ToString() + "\">" + pageNumber.ToString() + "</a>";
                    }
                }
            }

            if (intPageCount > CurrencyPage)
            {
                retval += " <a href=\"" + PageLinkURL + "Page=" + Convert.ToString((CurrencyPage + 1)) + "\">" + SafeRequest(NextBtnText) + "</a>";
                retval2 += " <a href=\"" + PageLinkURL + "Page=" + Convert.ToString((CurrencyPage + 1)) + "\">" + SafeRequest(NextBtnText) + "</a>";
            }

            if (intPageCount > pageNumber)
            {
                retval3 += " <a href=\"" + PageLinkURL + "Page=" + Convert.ToString((BasePage + 11)) + "\">" + SafeRequest(NextBtnText) + "</a>";
            }

            retval += "</div>";
            retval2 += "</div>";
            retval3 += "</div>";

            switch (LinkType)
            {
                case 1:
                    tmpReturnValue = retval;
                    break;
                case 2:
                    tmpReturnValue = retval2;
                    break;
                case 3:
                    tmpReturnValue = retval3;
                    break;
                default:
                    tmpReturnValue = retval3;
                    break;
            }
            output.Write(GetCssStyle() + tmpReturnValue);
        }

        //����URL����
        protected string BuildUrlString(NameValueCollection urlParams)
        {
            NameValueCollection newCol = new NameValueCollection(urlParams);
            NameValueCollection col = new NameValueCollection();
            string[] newColKeys = newCol.AllKeys;
            int i;
            for (i = 0; i < newColKeys.Length; i++)
            {
                if (newColKeys[i] != null)
                {
                    newColKeys[i] = newColKeys[i];
                }
            }
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < newCol.Count; i++)
            {
                if (newColKeys[i] == null) continue;
                if (!newColKeys[i].Equals("page", StringComparison.OrdinalIgnoreCase))
                {
                    sb.Append(newColKeys[i]);
                    sb.Append("=");
                    sb.Append(Page.Server.UrlEncode(newCol[i]));
                    sb.Append("&");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// ����">"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeRequest(string str)
        {
            if (str != null && str != string.Empty)
            {
                //str = str.Replace("'", "&#39");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
            }
            else
            {
                str = "";
            }
            return str;
        }
    }
}
