using System;
using CPSS.Common.Core;

namespace CPSS.Service.ViewService.ViewModels.User.Respond
{
    [Serializable]
    public class RespondSigninUserViewModel
    {
        public SigninUser CurrentUser { set; get; }
    }
}