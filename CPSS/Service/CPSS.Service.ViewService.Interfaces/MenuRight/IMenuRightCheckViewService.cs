using CPSS.Service.ViewService.ViewModels.MenuRight.Request;

namespace CPSS.Service.ViewService.Interfaces.MenuRight
{
    public interface IMenuRightCheckViewService
    {
        /// <summary>
        /// 检查是否具有访问或操作权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool CheckMenuRightByMenuID(RequestMenuRightCheckViewModel request);
    }
}