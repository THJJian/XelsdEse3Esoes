using System.Collections.Generic;
using CPSS.Common.Core.Paging;

namespace CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Employee
{
    public class QueryEmployeeListParameter: PagingBase
    {

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string Spelling { set; get; }

        public string SerialNumber { get; set; }

        public int DepId { set; get; }

        public string Mobile { set; get; }

        public short Status { get; set; }

        public short Deleted { get; set; }

        public string Comment { get; set; }
    }
}