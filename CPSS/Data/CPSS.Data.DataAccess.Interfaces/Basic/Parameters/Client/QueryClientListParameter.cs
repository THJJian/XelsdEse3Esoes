using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Client
{
    public class QueryClientListParameter : PagingBase
    {
        public string ParentId { set; get; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public string Alias { get; set; }

        public string LinkMan { get; set; }

        public string LinkTel { get; set; }

        public short PriceMode { get; set; }

        public short Status { get; set; }

        public short Deleted { get; set; }
    }
}