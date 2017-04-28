using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.Unit.Request;
using CPSS.Service.ViewService.ViewModels.Unit.Respond;

namespace CPSS.Service.ViewService.Interfaces.Basic
{
    public interface IUnitViewService
    {
        /// <summary>
        /// 获取计量单位信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQueryUnitViewModel>> GetQueryUnitList(RequestWebViewData<RequestQueryUnitViewModel> request);

        /// <summary>
        /// 新增计量单位信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondAddUnitViewModel> AddUnit(RequestWebViewData<RequestAddUnitViewModel> request);

        /// <summary>
        /// 根据depid查询对应行计量单位信息
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<RespondQueryUnitViewModel> GetUnitByUnitId(RequestWebViewData<RequestGetUnitByIdViewModel> request);

        /// <summary>
        /// 修改计量单位资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondEditUnitViewModel> EditUnit(RequestWebViewData<RequestEditUnitViewModel> request);
        
        /// <summary>
        /// 删除计量单位资料(逻辑删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteUnitViewModel> DeleteUnit(RequestWebViewData<RequestDeleteUnitViewModel> request);

        /// <summary>
        /// 恢复删除计量单位资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteUnitViewModel> ReDeleteUnit(RequestWebViewData<RequestDeleteUnitViewModel> request);
    }
}