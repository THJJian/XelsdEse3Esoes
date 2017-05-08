using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.SystemManage.UserManage;

namespace CPSS.Service.ViewService.SystemParameterConfig
{
    public class UserManageViewService : BaseViewService, IUserManageViewService
    {
        public UserManageViewService(IMongoDbDataAccess mongoDbDataAccess) : base(mongoDbDataAccess)
        {
        }

    }
}