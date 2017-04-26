using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.Common.Request;
using CPSS.Service.ViewService.ViewModels.Common.Respond;

namespace CPSS.Service.ViewService.Interfaces.Common
{
    public interface ICommonAjaxViewService
    {
        /// <summary>
        /// 获取所有没有子节点的部门信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<RespondGetAllDepartmentViewModel> GetAllDepartment(RequestWebViewData<RequestGetAllDepartmentViewModel> request);
    }
}