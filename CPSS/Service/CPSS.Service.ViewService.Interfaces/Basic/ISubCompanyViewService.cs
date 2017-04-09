using System.Collections.Generic;
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

        /// <summary>
        /// 根据comid查询对应行公司信息
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<RespondQuerySubCompanyViewModel> GetSubCompanyByComId(RequestWebViewData<RequestGetSubCompanyByIdViewModel> request);

        /// <summary>
        /// 修改子公司资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondEditSubCompanyViewModel> EditSubCompany(RequestWebViewData<RequestEditSubCompanyViewModel> request);
    }
}