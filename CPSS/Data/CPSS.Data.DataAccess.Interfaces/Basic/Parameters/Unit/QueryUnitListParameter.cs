using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Unit
{
    public class QueryUnitListParameter : PagingBase
    {
        public QueryUnitListParameter()
        {
            this.UnitId = 0;
        }

        public short Deleted { get; set; }

        public string Name { get; set; }

        public short Status { get; set; }

        public int UnitId { get; set; }
    }
}