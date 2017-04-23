using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Department
{
    public class QueryDepartmentListParameter : PagingBase
    {

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string Spelling { set; get; }

        public string SerialNumber { get; set; }

        public short Status { get; set; }

        public short Deleted { get; set; }
    }
}