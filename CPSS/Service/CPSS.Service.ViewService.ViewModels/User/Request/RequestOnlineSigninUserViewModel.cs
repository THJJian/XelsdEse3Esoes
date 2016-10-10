using System;

namespace CPSS.Service.ViewService.ViewModels.User.Request
{
    [Serializable]
    public class RequestOnlineSigninUserViewModel
    {
        public Guid SGuid { set; get; }

        public int UserID { set; get; }

        public string AddressIP { set; get; }
    }
}