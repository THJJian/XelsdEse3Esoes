using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Storage
{
    public class QueryStorageListParameter : PagingBase
    {

        public object ParentId { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public string Alias { get; set; }

        public short Status { get; set; }

        public short Deleted { get; set; }
    }
}