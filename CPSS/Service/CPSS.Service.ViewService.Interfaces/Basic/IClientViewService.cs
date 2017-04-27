using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.Client.Request;
using CPSS.Service.ViewService.ViewModels.Client.Respond;

namespace CPSS.Service.ViewService.Interfaces.Basic
{
    public interface IClientViewService
    {

        /// <summary>
        /// 获取往来客户信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQueryClientViewModel>> GetQueryClientList(RequestWebViewData<RequestQueryClientViewModel> request);

        /// <summary>
        /// 新增往来客户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondAddClientViewModel> AddClient(RequestWebViewData<RequestAddClientViewModel> request);

        /// <summary>
        /// 根据depid查询对应行往来客户信息
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<RespondQueryClientViewModel> GetClientByClientId(RequestWebViewData<RequestGetClientByIdViewModel> request);

        /// <summary>
        /// 修改往来客户资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RequestEditClientViewModel> EditClient(RequestWebViewData<RequestEditClientViewModel> request);

        /// <summary>
        /// 删除往来客户资料(逻辑删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteClientViewModel> DeleteClient(RequestWebViewData<RequestDeleteClientViewModel> request);

        /// <summary>
        /// 恢复删除往来客户资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteClientViewModel> ReDeleteClient(RequestWebViewData<RequestDeleteClientViewModel> request);
    }
}