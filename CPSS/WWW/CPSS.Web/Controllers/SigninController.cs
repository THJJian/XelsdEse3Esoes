using System.Web.Mvc;
using CPSS.Common.Core.Mvc;

namespace CPSS.Web.Controllers
{
    public class SigninController : WebBaseController
    {
        // GET: Signin
        public ActionResult Login()
        {
            return View();
        }
    }
}