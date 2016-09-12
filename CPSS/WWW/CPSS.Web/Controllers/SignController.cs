using System.Web.Mvc;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Web.Models;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class SignController : WebBaseController
    {
        private readonly ISigninUserViewService mSigninUserViewService;
        public SignController(ISigninUserViewService _signinUserViewService)
        {
            this.mSigninUserViewService = _signinUserViewService;
        }

        [OperateRight(OperateRightIDs = "")]
        public ViewResult Login()
        {
            return View();
        }

        // GET: Sign
        [HttpPost]
        public JsonResult Login(RequestUserInfo request)
        {
            bool isValid;
            var errorMessage = this.GenerateModelValidateErrMessage(ModelState, out isValid);
            if (!isValid) return Json(errorMessage);

            //TODO 验证通过将执行的操作
            const int errorCode = 3232;
            errorMessage = string.Concat(errorCode, errorMessage);
            var _request = new RequestSigninUserViewModel
            {
                UserName = request.LoginName,
                UserPwd = request.LoginPwd,
                ValidCode = request.ValidCode
            };
            var respond = this.mSigninUserViewService.QuerySigninUserViewModel(_request);
            if (respond.CurrentUser.UserID > 0)
            {

            }
            return Json(errorMessage);
        }
    }
}