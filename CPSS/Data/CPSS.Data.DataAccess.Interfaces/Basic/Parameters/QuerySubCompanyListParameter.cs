using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.Basic.Parameters
{
    public class QuerySubCompanyListParameter : PagingBase
    {
        public QuerySubCompanyListParameter()
        {
            this.ParentId = "000001";
        }

        public string ParentId { set; get; }

        public string SerialNumber { set; get; }

        public string Name { set; get; }

        public string Spelling { set; get; }

        public int PriceMode { set; get; }

        public string Email { set; get; }

        public string LinkMan { set; get; }

        public string LinkTel { set; get; }

        public short Status { set; get; }

        public short Deleted { get; set; }
    }
}