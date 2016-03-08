using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.PlanStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.PlanStructure;
using EyouSoft.Common;

namespace Web.UserControl
{
    /// <summary>
    /// 财务报账
    /// 计调报账
    /// 导游报账
    /// 销售报账
    /// 
    /// 团队支出 用户控件
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-4-1
    /// 用户控件备注:
    /// 必须配置的属性
    /// ParentType(调用页面类型)
    /// 
    /// 当财务调用时  计调项无数据 则整个计调栏目不显示
    /// 财务进入 无法进行 修改,删除,添加操作
    /// 
    /// 计调,销售,导游 调用始即使计调项无数据 也保留栏目以及添加功能
    /// 
    /// TourID(团队编号)
    /// CurrentUserCompanyID(公司编号)
    public partial class TourMoneyOut : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        private bool _isplanchangechange = true;

        /// <summary>
        /// 获取或设置调用页面类型
        ///     财务传null
        ///     导游报账
        ///     销售报账
        ///     计调报账
        /// </summary>
        public PlanChangeChangeClass? ParentType
        {
            set;
            get;
        }
        /// <summary>
        /// 是否允许计调变更
        /// </summary>
        public bool IsPlanChangeChange
        {
            set { _isplanchangechange = value; }
            get { return _isplanchangechange; }
        }

        private bool _isChangeDaoYou;
        /// <summary>
        /// 是否可以修改 支付方式为导游现付的数据
        /// </summary>
        public bool IsChangeDaoYou
        {
            get { return _isChangeDaoYou; }
            set { _isChangeDaoYou = value; }
        }
        /// <summary>
        /// 获取添加类型
        /// </summary>
        public PlanAddStatus? AddStatusPlan
        {
            get
            {
                switch (ParentType)
                {
                    //case PlanChangeChangeClass.导游报账:
                    //    return PlanAddStatus.导游报账时添加;
                    case PlanChangeChangeClass.计调报账:
                        return PlanAddStatus.计调报账时添加;
                    //case PlanChangeChangeClass.销售报账:
                    //    return PlanAddStatus.销售报账时添加;
                }
                return null;
            }
        }
        protected int IntParentType
        {
            get { return ParentType == null ? -1 : (int)ParentType; }
        }
        /// <summary>
        /// 获取或设置团队编号
        /// </summary>
        public string TourID
        {
            get;
            set;
        }
    }
}