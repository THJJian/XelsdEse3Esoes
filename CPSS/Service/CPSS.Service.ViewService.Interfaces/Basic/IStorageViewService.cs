using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.Storage.Request;
using CPSS.Service.ViewService.ViewModels.Storage.Respond;

namespace CPSS.Service.ViewService.Interfaces.Basic
{
    public interface IStorageViewService
    {

        /// <summary>
        /// 获取仓库信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQueryStorageViewModel>> GetQueryStorageList(RequestWebViewData<RequestQueryStorageViewModel> request);

        /// <summary>
        /// 新增仓库信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondAddStorageViewModel> AddStorage(RequestWebViewData<RequestAddStorageViewModel> request);

        /// <summary>
        /// 根据depid查询对应行仓库信息
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<RespondQueryStorageViewModel> GetStorageByStorageId(RequestWebViewData<RequestGetStorageByIdViewModel> request);

        /// <summary>
        /// 修改仓库资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondEditStorageViewModel> EditStorage(RequestWebViewData<RequestEditStorageViewModel> request);

        /// <summary>
        /// 删除仓库资料(逻辑删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteStorageViewModel> DeleteStorage(RequestWebViewData<RequestDeleteStorageViewModel> request);

        /// <summary>
        /// 恢复删除仓库资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteStorageViewModel> ReDeleteStorage(RequestWebViewData<RequestDeleteStorageViewModel> request);
    }
}