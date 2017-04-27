namespace CPSS.Service.ViewService.ViewModels.Employee.Request
{
    public class RequestEditEmployeeViewModel
    {

        public int EmpId { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public string Mobile { set; get; }

        public string Address { set; get; }

        public int Sort { get; set; }

        public string Comment { get; set; }
    }
}