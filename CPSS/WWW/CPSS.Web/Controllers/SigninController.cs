using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Mvc;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;
//using Newtonsoft.Json.Linq;

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
            RespondWebViewData<RespondSigninUserViewModel> respond;
            if (!isValid)
            {
                respond = new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.ModelValidateError.ErrorCode, errorMessage);
                //var result = JObject.FromObject(respond);
                return Json(respond);
            }

            //TODO 验证通过将执行的操作
            respond = this.mSigninUserViewService.QuerySigninUserViewModel(request);
            if (respond.Data.CurrentUser.UserID > 0)
            {

            }
            return Json(errorMessage);
        }
        
    }
}