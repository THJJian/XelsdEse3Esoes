using System;

namespace CPSS.Data.DataAcess.DataModels.User
{
    public class OnlineSigninUserDataModel
    {
        public int UserID { get; set; }
        public string UserIP { get; set; }
        public string LoginName { get; set; }
        public string Browser { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime OverTime { get; set; }
        public DateTime ExpTime { get; set; }
        public Guid SGuid { get; set; }
    }
}