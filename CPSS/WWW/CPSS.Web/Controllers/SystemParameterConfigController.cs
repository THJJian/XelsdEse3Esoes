using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;

namespace CPSS.Web.Controllers
{
    //[HaveToLogin]
    public class SystemParameterConfigController : WebBaseController
    {
        // GET: SystemParameterConfig
        public ActionResult Index()
        {
            return View();
        }
    }
}