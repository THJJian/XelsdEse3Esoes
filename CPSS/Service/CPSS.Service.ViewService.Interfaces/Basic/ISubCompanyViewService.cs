﻿using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.SubCompany.Request;
using CPSS.Service.ViewService.ViewModels.SubCompany.Respond;

namespace CPSS.Service.ViewService.Interfaces.Basic
{
    public interface ISubCompanyViewService
    {
        /// <summary>
        /// 获取公司信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQuerySubCompanyViewModel>> GetQueryCompanyList(RequestWebViewData<RequestQuerySubCompanyViewModel> request);

        /// <summary>
        /// 新增子公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondAddSubCompanyViewModel> AddSubCompany(RequestWebViewData<RequestAddSubCompanyViewModel> request);
    }
}