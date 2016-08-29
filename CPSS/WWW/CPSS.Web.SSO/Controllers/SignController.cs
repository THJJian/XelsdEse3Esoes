using System.Web.Mvc;
using CPSS.Common.Helper.Extension;
using CPSS.Web.SSO.Models;

namespace CPSS.Web.SSO.Controllers
{
    public class SignController : Controller
    {

        /// <summary>
        /// 必须要写构造函数，构造函数用于依赖注入。
        /// </summary>
        public SignController()
        {

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