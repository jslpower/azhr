using System;

namespace EyouSoft.WebFX
{
    using System.Globalization;
    using System.Threading;

    using EyouSoft.Common;

    public partial class Login : System.Web.UI.Page
    {
        protected string _lgtype = "ZH-CN";//语言类型（1为中文,2为英文,3为泰文）
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch ((EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetQueryStringValue("l")))
                {
                    case EyouSoft.Model.EnumType.SysStructure.LngType.英文:
                        this._lgtype = "EN-US";
                        break;
                    //case EyouSoft.Model.EnumType.SysStructure.LngType.泰文:
                    //    this._lgtype = "TH-TH";
                    //    break;
                    default:
                        this._lgtype = "ZH-CN";
                        break;
                }
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(this._lgtype);
                this.YuYan.Text = (string)GetLocalResourceObject("LitLanguage.text");
                this.YongHuMing.Text = (string)GetLocalResourceObject("Label1Resource1.text");
                this.MiMa.Text = (string)GetLocalResourceObject("LitPWD.text");
                //this.GongGongZhangHao.Text = (string)GetLocalResourceObject("Public.text");
                //this.GongGongMiMa.Text = (string)GetLocalResourceObject("LitPWD.text");
                this.XiTong.Text = (string)GetLocalResourceObject("System.text");
                this.BanQuan.Text = (string)GetLocalResourceObject("Reserved.text");
            }
        }
    }
}
