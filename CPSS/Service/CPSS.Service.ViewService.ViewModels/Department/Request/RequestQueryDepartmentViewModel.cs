namespace CPSS.Service.ViewService.ViewModels.Department.Request
{
    public class RequestQueryDepartmentViewModel
    {

        public string ParentId { get; set; }

        public string SerialNumber { get; set; }

        public short Deleted { get; set; }

        public string Name { get; set; }

        public short Status { get; set; }

        public string Spelling { get; set; }
    }
}