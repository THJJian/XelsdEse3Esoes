using System.Web.Mvc;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Mvc;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;

namespace CPSS.Web.Controllers
{
    public class SigninController : WebBaseController
    {
        private readonly ISigninUserViewService mSigninUserViewService;
        public SigninController(ISigninUserViewService _signinUserViewService)
        {
            this.mSigninUserViewService = _signinUserViewService;
        }

        // GET: Signin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(RequestSigninUserViewModel request)
        {
            bool isValid;
            var errorMessage = this.GenerateModelValidateErrMessage(ModelState, out isValid);
            if (!isValid) return Json(errorMessage);

            //TODO 验证通过将执行的操作
            var respond = this.mSigninUserViewService.QuerySigninUserViewModel(request);
            if (respond.Data.CurrentUser.UserID > 0)
            {

            }
            return Json(errorMessage);
        }
    }
}