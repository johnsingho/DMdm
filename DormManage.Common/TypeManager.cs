using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
///------------------------------------------------------------------------------
///描  述：常量配置管理类
///版本号：
///作  者：
///日  期：
///修  改：
///原  因：
///------------------------------------------------------------------------------
namespace DormManage.Common
{
    public class TypeManager
    {
        public const int IntDefault = -1;                        //默认返回值,-1表示失败
        public const string Unselect = "";                       //缺省下拉框的“请选择”值
        public const string User = "User";//系统用户
        public const string Admin = "Admin";//系统超级管理员
        public const string SESSIONKEY_ImpErrCheckIn = "ImpErrCheckIn";//入住记录导入失败
        public const string SESSIONKEY_ImpErrAllowanceApply = "ImpErrAllowanceApply";
        public const string SESSIONKEY_ImpErrAllowanceAppCancel = "ImpErrAllowanceAppCancel";
        public const string SESSIONKEY_ImpErrCharing = "ImpErrCharing";

        public const string ModuleID = "M_ID";                   //模块ID
        public const string OperationID = "O_ID";                //操作ID

        #region  入住人员导入模板字段

        public const string Col_DormArea = "宿舍区";
        public const string Col_Building = "楼栋";
        public const string Col_RoomNo = "房号";
        public const string Col_BedNo = "床位号";
        public const string Col_Name = "姓名";
        public const string Col_Sex = "性别";
        public const string Col_CardNo = "身份证号码";
        public const string Col_EmployeeNo = "工号";
        public const string Col_Company = "公司";
        public const string Col_BU = "事业部";
        public const string Col_CheckInDate = "入住日期";

        public const string Col_Charging = "扣费金额";
        public const string Col_ChargingContent = "扣费内容";
        public const string Col_ChargingDate = "日期";
        #endregion

        /// <summary>
        /// 记录状态
        /// </summary>
        public enum IsActive
        {
            [Remark("无效")]
            Invalid=0,
            [Remark("有效")]
            Valid=1,
        }

        /// <summary>
        /// 房间类型
        /// </summary>
        public enum RoomType
        {
            [Remark("员工宿舍")]
            Dormitory = 1,
            [Remark("家庭房")]
            FamilyRoom = 2,
        }

        /// <summary>
        /// 床位状态
        /// </summary>
        public enum BedStatus
        {
            [Remark("空闲")]
            Free = 1,
            [Remark("已分配未入住")]
            Occupy = 2,
            [Remark("已入住")]
            Busy = 3,
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            [Remark("男")]
            Male = 1,
            [Remark("女")]
            Female = 2,
        }
    }

}
