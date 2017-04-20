namespace CPSS.Service.ViewService.ViewModels.Department.Request
{
    public class RequestAddDepartmentViewModel
    {

        public string ParentId { get; set; }

        public string Comment { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public string SerialNumber { get; set; }

        public int Sort { get; set; }
    }
}