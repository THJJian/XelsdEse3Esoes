using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class MainController : WebBaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DefaultHome()
        {
            return View();
        }

        public ActionResult Window()
        {
            return View();
        }
    }
}