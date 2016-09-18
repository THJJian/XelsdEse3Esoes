using System;

namespace CPSS.Service.ViewService.ViewModels.User.Respond
{
    [Serializable]
    public class RespondOnlineSigninUserViewModel
    {
        public int UserID { set; get; }

        public string Browser { set; get; }

        public DateTime ExpTime { set; get; }

        public string LoginName { set; get; }

        public DateTime LoginTime { set; get; }

        public DateTime OverTime { set; get; }

        public Guid SGuid { set; get; }

        public string UserIP { set; get; }
    }
}