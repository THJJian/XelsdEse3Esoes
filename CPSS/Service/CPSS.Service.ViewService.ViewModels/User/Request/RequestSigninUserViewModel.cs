using System;

namespace CPSS.Service.ViewService.ViewModels.User.Request
{
    [Serializable]
    public class RequestSigninUserViewModel
    {
        public string UserName { set; get; }

        public string UserPwd { set; get; }

        public string ValidCode { set; get; }
    }
}