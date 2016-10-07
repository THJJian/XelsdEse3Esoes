using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Service.ViewService.Interfaces.HeadButtons;
using CPSS.Service.ViewService.Interfaces.MainPage;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Request;
using CPSS.Service.ViewService.ViewModels.MainPage.Respond;

namespace CPSS.Web.Controllers
{
    //[HaveToLogin]
    public class CommonPartialController : WebBaseController
    {
        private readonly ILeftNavMenuViewService mLeftNavMenuViewService;
        private readonly IHeadButtonsViewService mHeadButtonsViewService;

        public CommonPartialController(ILeftNavMenuViewService _leftNavMenuViewService, IHeadButtonsViewService _headButtonsViewService)
        {
            this.mLeftNavMenuViewService = _leftNavMenuViewService;
            this.mHeadButtonsViewService = _headButtonsViewService;
        }

        /// <summary>
        /// 主页左边导航栏
        /// </summary>
        /// <returns></returns>
        public PartialViewResult LeftNavMenu()
        {
            #region 事例

            //var model = new List<RespondPanelViewModel>
            //{
            //    new RespondPanelViewModel
            //    {
            //        IconCls = string.Empty,
            //        Title = "行业动态",
            //        Menus = new List<RespondMenuViewModel>
            //        {
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息",
            //                Url = "http://www.baidu.com"
            //            },
            //        }
            //    },
            //    new RespondPanelViewModel
            //    {
            //        IconCls = string.Empty,
            //        Title = "行业动态2",
            //        Menus = new List<RespondMenuViewModel>
            //        {
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规21",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告22",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息23",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规24",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告25",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息26",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规27",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告28",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息29",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规20",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告19",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息18",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告17",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息16",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告15",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息14",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告13",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息12",
            //                Url = "http://www.baidu.com"
            //            },
            //        }
            //    },
            //    new RespondPanelViewModel
            //    {
            //        IconCls = string.Empty,
            //        Title = "行业动态3",
            //        Menus = new List<RespondMenuViewModel>
            //        {
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规31",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告32",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息33",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规34",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告35",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息36",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规37",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告38",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息39",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规41",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告42",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息43",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规41",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告42",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息43",
            //                Url = "http://www.baidu.com"
            //            },
            //        }
            //    },
            //    new RespondPanelViewModel
            //    {
            //        IconCls = string.Empty,
            //        Title = "行业动态4",
            //        Menus = new List<RespondMenuViewModel>
            //        {
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规41",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告42",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息43",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规44",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告45",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息46",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "政策法规47",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "通知公告48",
            //                Url = "http://www.baidu.com"
            //            },
            //            new RespondMenuViewModel
            //            {
            //                IconCls = string.Empty,
            //                Title = "动态信息49",
            //                Url = "http://www.baidu.com"
            //            },
            //        }
            //    }
            //};

            #endregion
            var model = this.mLeftNavMenuViewService.GetLeftNavMenuDataModels(0);

            return PartialView("~/Views/Shared/CommonPartial/LeftNavMenu.cshtml", model);
        }

        /// <summary>
        /// 页面按钮
        /// </summary>
        /// <returns></returns>
        public PartialViewResult HeadButtons(RequestHeadButtonsViewModel request)
        {
            var model = this.mHeadButtonsViewService.QueryHeadButtonsViewModelsByMenuID(request);
            return PartialView("~/Views/Shared/CommonPartial/HeadButtons.cshtml", model);
        }

        /// <summary>
        /// 无权访问的内容
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UnAuthorizedVisit()
        {
            return PartialView("~/Views/Shared/CommonPartial/UnAuthorizedVisit.cshtml");
        }
    }
}