using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Web.Controllers.Filters;

namespace CPSS.Web.Controllers
{
    public class BuyController : WebBaseController
    {
        // GET: Buy
        public ActionResult Index()
        {
            return View();
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBuyOrderBill)]
        public ActionResult BuyOrder()
        {
            return View();
        }
    }
}