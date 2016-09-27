using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPSS.Common.Core.Mvc;

namespace CPSS.Web.Controllers
{
    public class InformationController : WebBaseController
    {
        // GET: Information
        public ActionResult PolicyLaw()
        {
            return View();
        }
    }
}