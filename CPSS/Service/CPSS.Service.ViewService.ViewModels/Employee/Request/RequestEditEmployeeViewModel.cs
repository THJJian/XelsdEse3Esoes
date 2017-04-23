namespace CPSS.Service.ViewService.ViewModels.Employee.Request
{
    public class RequestEditEmployeeViewModel
    {

        public int EepId { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public int DepId { set; get; }

        public string DepName { set; get; }

        public string Mobile { set; get; }

        public string Address { set; get; }

        public int Sort { get; set; }

        public string Comment { get; set; }
    }
}