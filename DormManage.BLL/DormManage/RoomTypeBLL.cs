using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DormManage.Data.DAL;

namespace DormManage.BLL.DormManage
{
   public class RoomTypeBLL
    {
       private TB_RoomTypeDAL _mTB_RoomTypeDAL;

       public RoomTypeBLL()
       {
           _mTB_RoomTypeDAL = new TB_RoomTypeDAL();
       }

       public DataTable GetTable(int intSiteID)
       {
           return _mTB_RoomTypeDAL.GetTable(intSiteID);
       }
    }
}
