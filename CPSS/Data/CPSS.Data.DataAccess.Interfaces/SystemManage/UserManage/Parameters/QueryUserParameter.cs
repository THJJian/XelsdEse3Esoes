using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.SystemManage.UserManage.Parameters
{
    public class QueryUserParameter : PagingBase
    {

        public string UserName { get; set; }

        public int EmpId { get; set; }
    }
}