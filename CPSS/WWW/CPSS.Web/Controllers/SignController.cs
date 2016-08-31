using System.Web.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Helper.Extension;
using CPSS.Web.Models;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class SignController : Controller
    {
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

            return Json(errorMessage);
        }
    }
}