using System;

namespace CPSS.Data.DataAcess.DataModels.SystemManage.UserManage
{
    public class UserDataModel
    {
        public int UserId { get; set; }
        
        public int ComId { get; set; }
        
        public int EmpId { get; set; }
        
        public string UserName { get; set; }
        
        public string UsePwd { get; set; }
        
        public string Prefix { get; set; }
        
        public short Manager { get; set; }
        
        public short Status { get; set; }
        
        public short Deleted { get; set; }
        
        public short Synchron { get; set; }
        
        public DateTime CTime { get; set; }

        public string Comment { get; set; }
    }
}