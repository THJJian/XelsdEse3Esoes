using System.Web.Mvc;
using CPSS.Common.Core.Mvc;

namespace CPSS.Web.Controllers
{
    public class DemoController : WebBaseController
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }
    }
}