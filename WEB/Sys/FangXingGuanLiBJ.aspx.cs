using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace EyouSoft.Web.Sys
{
    public partial class FangXingGuanLiBJ : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mark = Utils.GetQueryStringValue("mark");
            string id = Utils.GetQueryStringValue("id");
            if (mark != "")
            {
                save(mark);
            }
            initPage(id);
        }
        protected void initPage(string id)
        {
            if (id != "")
            {
                var model = new EyouSoft.BLL.SysStructure.BSysRoom().getRoom(id.Trim(), SiteUserInfo.CompanyId);
                //if (model == null) txtRoomName.Text = "";
                //txtRoomName.Text = model.TypeName;
                if (model != null)
                {
                    txtRoomName.Text = model.TypeName;
                }
            }
        }
        protected void save(string dotype)
        {
            int result = 0;
            string id = Utils.GetQueryStringValue("id").Trim();
            EyouSoft.Model.SysStructure.MSysRoom model = new EyouSoft.Model.SysStructure.MSysRoom();
            model.TypeName = Utils.GetFormValue(txtRoomName.UniqueID);
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Operator = SiteUserInfo.Name;
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.OperatorId = SiteUserInfo.UserId;

            if (dotype == "add")
            {
                result = new EyouSoft.BLL.SysStructure.BSysRoom().AddRoom(model);
                if (result == 1) AjaxResponse(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                if (result == 0) AjaxResponse(UtilsCommons.AjaxReturnJson("0", "添加失败"));
                if (result == 2) AjaxResponse(UtilsCommons.AjaxReturnJson("0", "该房型已存在"));

            }
            else
            {
                model.RoomId = id;
                result = new EyouSoft.BLL.SysStructure.BSysRoom().UpdateRoom(model);
                if (result == 1) AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                if (result == 0) AjaxResponse(UtilsCommons.AjaxReturnJson("0", "修改失败"));

            }
        }
    }
}
