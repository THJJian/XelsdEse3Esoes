using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.Employee.Request;
using CPSS.Service.ViewService.ViewModels.Employee.Respond;

namespace CPSS.Service.ViewService.Interfaces.Basic
{
    public interface IEmployeeViewService
    {

        /// <summary>
        /// 获取职员信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQueryEmployeeViewModel>> GetQueryEmployeeList(RequestWebViewData<RequestQueryEmployeeViewModel> request);

        /// <summary>
        /// 新增职员信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondAddEmployeeViewModel> AddEmployee(RequestWebViewData<RequestAddEmployeeViewModel> request);

        /// <summary>
        /// 根据depid查询对应行职员信息
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<RespondQueryEmployeeViewModel> GetEmployeeByDepId(RequestWebViewData<RequestGetEmployeeByIdViewModel> request);

        /// <summary>
        /// 修改职员资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RequestEditEmployeeViewModel> EditEmployee(RequestWebViewData<RequestEditEmployeeViewModel> request);

        /// <summary>
        /// 删除职员资料(逻辑删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteEmployeeViewModel> DeleteEmployee(RequestWebViewData<RequestDeleteEmployeeViewModel> request);

        /// <summary>
        /// 恢复删除职员资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteEmployeeViewModel> ReDeleteEmployee(RequestWebViewData<RequestDeleteEmployeeViewModel> request);
    }
}