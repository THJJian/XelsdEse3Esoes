using System.Web.Mvc;
using CPSS.Common.Core.Mvc;

namespace CPSS.Web.Controllers.Main
{
    public class MainController : WebBaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
    }
}