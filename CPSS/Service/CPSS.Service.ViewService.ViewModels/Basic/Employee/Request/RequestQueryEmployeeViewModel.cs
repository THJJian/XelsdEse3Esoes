using System.Collections.Generic;

namespace CPSS.Service.ViewService.ViewModels.Employee.Request
{
    public class RequestQueryEmployeeViewModel
    {

        public string ParentId { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public int DepId { set; get; }

        public string Mobile { set; get; }

        public string Comment { set; get; }

        public short Deleted { get; set; }

        public short Status { get; set; }
    }
}