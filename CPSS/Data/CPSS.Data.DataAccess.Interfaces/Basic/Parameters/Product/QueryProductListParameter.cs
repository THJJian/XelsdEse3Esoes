using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Product
{
    public class QueryProductListParameter : PagingBase
    {

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string Spelling { get; set; }

        public int Status { get; set; }

        public int Deleted { get; set; }

        public string ParentId { get; set; }
    }
}