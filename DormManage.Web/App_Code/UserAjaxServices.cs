using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxPro;
using DormManage.BLL.UserManage;


public class UserAjaxServices
{
    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="intAdminID"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    [AjaxMethod]
    public string ModifyPassword(int intAdminID,string oldPassword, string newPassword)
    {
        return new UserBLL().ModifyPassword(intAdminID,oldPassword, newPassword);
    }
}