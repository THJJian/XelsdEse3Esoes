using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Type;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;

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
                return Json(respond);
            }
            var _sessionVerifyCode = HttpContext.Session[BeforeCompileConstDefined.HttpContext_Login_Img_Verify_Code].ToString().ToLower();
            if (_sessionVerifyCode != request.ValidCode.ToLower())
            {
                respond = new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.VerifyCodeError);
                return Json(respond);
            }
            
            respond = this.mSigninUserViewService.QuerySigninUserViewModel(request);
            HttpContext.Session[BeforeCompileConstDefined.HttpContext_Login_Img_Verify_Code] = string.Empty;
            if (respond.rows.CurrentUser.UserID > 0)
            {
                //var _signature_text = SignatureHelper.BuildSignature(JObject.FromObject(respond.Data.CurrentUser));
                //return Json(new RespondWebViewData<object>
                //{
                //    Data = _signature_text
                //});
                return Json(respond);
            }
            respond = new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.NotExistUserInfo);
            return Json(respond);
        }
        
    }
}