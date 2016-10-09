using System.Web.Mvc;
using CPSS.Common.Core.Helper.VerifyImg;

namespace CPSS.Web.Controllers
{
    public class VerifyCodeImageController : Controller
    {
        // GET: VerifyCodeImage
        public ActionResult Index()
        {
            VerifyCode vCode = new VerifyCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

    }
}