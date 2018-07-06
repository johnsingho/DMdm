using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AjaxPro;
using DormManage.BLL.AssignRoom;
using DormManage.BLL.DormManage;
using DormManage.Framework;
using DormManage.Models;
using DormManage.BLL;

public class DormManageAjaxServices
{
    /// <summary>
    /// 根据宿舍区ID获取楼栋
    /// </summary>
    /// <param name="intDormAreaID"></param>
    /// <returns></returns>
    [AjaxMethod]
    public DataTable GetBuildingByDormAreaID(int intDormAreaID)
    {
        return new BuildingBLL().GetBuildingByDormAreaID(intDormAreaID);
    }

    [AjaxMethod]
    public void ChangeBegEnable(int id,string value)
    {
       
         new CommonBLL().ChangeBegEnable(id,value);
    }
    [AjaxMethod]
    public void BatchChangeBedEnable(List<int> ids, bool bEnable)
    {
        new CommonBLL().BatchChangeBedEnable(ids, bEnable);
    }
    

    [AjaxMethod]
    public void ChangeRoomEnable(int id, string value)
    {
        new CommonBLL().ChangeRoomEnable(id, value);
    }

    [AjaxMethod]
    public void BatchChangeRoomEnable(List<int> ids, bool bEnable)
    {
        new CommonBLL().ChangeRoomEnable(ids, bEnable);
    }

    [AjaxMethod]
    public void ChangeBuildingEnable(int id, string value)
    {
        new CommonBLL().ChangeBuildingEnable(id, value);
    }

    [AjaxMethod]
    public void ChangeDormAreaEnable(int id, string value)
    {
        new CommonBLL().ChangeDormAreaEnable(id, value);
    }

    /// <summary>
    /// 根据楼栋ID获取单元
    /// </summary>
    /// <param name="intBuildingID"></param>
    /// <returns></returns>
    [AjaxMethod]
    public DataTable GetUnitByBuildingID(int intBuildingID)
    {
        return new UnitBLL().GetUnitByBuildingID(intBuildingID);
    }

    /// <summary>
    /// 根据单元ID获取到楼层
    /// </summary>
    /// <param name="intUnitID"></param>
    /// <returns></returns>
    [AjaxMethod]
    public DataTable GetFloorByUnitID(int intUnitID)
    {
        return new FloorBLL().GetFloorByUnitID(intUnitID);
    }

    /// <summary>
    /// 根据楼层ID获取到房间
    /// </summary>
    /// <param name="intFloorID"></param>
    /// <returns></returns>
    [AjaxMethod]
    public DataTable GetRoomByFloorID(int intFloorID)
    {
        return new RoomBLL().GetRoomByFloorID(intFloorID);
    }

    /// <summary>
    /// 根据楼栋ID获取到房间
    /// </summary>
    /// <param name="intBuildingID"></param>
    /// <returns></returns>
    [AjaxMethod]
    public DataTable GetRoomByBuildingID(int intBuildingID)
    {
        return new RoomBLL().GetRoomByBuildingID(intBuildingID);
    }

    /// <summary>
    /// 编辑楼层信息
    /// </summary>
    /// <param name="tb_Floor"></param>
    /// <returns></returns>
    [AjaxMethod]
    public string EditFloor(TB_Floor tb_Floor)
    {
        FloorBLL mFloorBLL = new FloorBLL();
        mFloorBLL.ErrMessage = string.Empty;
        mFloorBLL.Edit(tb_Floor);
        return mFloorBLL.ErrMessage;
    }

    /// <summary>
    /// 编辑房间信息
    /// </summary>
    /// <param name="tb_Room"></param>
    /// <returns></returns>
    [AjaxMethod]
    public string EditRoom(TB_Room tb_Room)
    {
        RoomBLL mRoomBLL = new RoomBLL();
        mRoomBLL.ErrMessage = string.Empty;
        mRoomBLL.Edit(tb_Room);
        return mRoomBLL.ErrMessage;
    }

    /// <summary>
    /// 编辑床位信息
    /// </summary>
    /// <param name="tb_Bed"></param>
    /// <returns></returns>
    [AjaxMethod]
    public string EditBed(TB_Bed tb_Bed)
    {
        BedBLL mBedBLL = new BedBLL();
        mBedBLL.ErrMessage = string.Empty;
        mBedBLL.Edit(tb_Bed);
        return mBedBLL.ErrMessage;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="intRoomID"></param>
    /// <returns></returns>
    [AjaxMethod]
    public DataTable GetBedByRoomID(int intRoomID)
    {
        return new BedBLL().GetBedByRoomID(intRoomID);
    }

    [AjaxMethod]
    public IEnumerable<object> GetLockBuildingByDormAreaID(int intDormAreaID)
    {
        return new AssignRoomBLL().GetLockBuildingByDormAreaID(intDormAreaID);
    }

    [AjaxMethod]
    public CommonBLL.TEmpInfo GetEmployeeInfo(string snr)
    {
        return CommonBLL.GetEmployeeInfo(snr.Trim());
    }

}