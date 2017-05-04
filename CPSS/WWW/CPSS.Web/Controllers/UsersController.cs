using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class UsersController : WebBaseController
    {

        // GET: User
        public ActionResult UserManageList()
        {
            return View();
        }
    }
}