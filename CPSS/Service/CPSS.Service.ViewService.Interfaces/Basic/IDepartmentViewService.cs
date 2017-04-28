using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.Department.Request;
using CPSS.Service.ViewService.ViewModels.Department.Respond;

namespace CPSS.Service.ViewService.Interfaces.Basic
{
    public interface IDepartmentViewService
    {
        /// <summary>
        /// 获取部门信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQueryDepartmentViewModel>> GetQueryDepartmentList(RequestWebViewData<RequestQueryDepartmentViewModel> request);

        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondAddDepartmentViewModel> AddDepartment(RequestWebViewData<RequestAddDepartmentViewModel> request);

        /// <summary>
        /// 根据depid查询对应行部门信息
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<RespondQueryDepartmentViewModel> GetDepartmentByDepId(RequestWebViewData<RequestGetDepartmentByIdViewModel> request);

        /// <summary>
        /// 修改部门资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondEditDepartmentViewModel> EditDepartment(RequestWebViewData<RequestEditDepartmentViewModel> request);
        
        /// <summary>
        /// 删除部门资料(逻辑删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteDepartmentViewModel> DeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request);

        /// <summary>
        /// 恢复删除部门资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteDepartmentViewModel> ReDeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request);
    }
}