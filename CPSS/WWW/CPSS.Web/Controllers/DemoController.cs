using System.Data;
using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Web.Controllers.Filters;

namespace CPSS.Web.Controllers
{
    public class DemoController : WebBaseController
    {
        public readonly IDbConnection mDbConnection;

        public DemoController(IDbConnection _dbConnection)
        {
            this.mDbConnection = _dbConnection;
        }

        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBuyOrderBill)]
        public JsonResult demo()
        {
            var _value = new
            {
                success = "成功访问"
            };
            return Json(_value);
        }
    }
}