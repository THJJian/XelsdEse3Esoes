using System;

namespace CPSS.Data.DataAccess.Interfaces.User.Parameters
{
    public class OnlineSigninUserParameter
    {
        public int UserID;
        public string UserIP;
        public string LoginName { get; set; }
        public string Browser { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime OverTime { get; set; }
        public DateTime ExpTime { get; set; }
        public Guid SGuid { get; set; }
    }
}