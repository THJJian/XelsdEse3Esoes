using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using CPSS.Service.ViewService.ViewModels.MainPage.Respond;
using System.Collections.Generic;

namespace CPSS.Web.Controllers
{
    public class CommonPartialController : WebBaseController
    {
        public PartialViewResult LeftNavMenu()
        {
            var model = new List<RespondPanelViewModel>();
            return PartialView("~/Views/Shared/CommonPartial/LeftNavMenu.cshtml", model);
        }
    }
}