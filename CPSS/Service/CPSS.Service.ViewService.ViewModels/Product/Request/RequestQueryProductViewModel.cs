namespace CPSS.Service.ViewService.ViewModels.Product.Request
{
    public class RequestQueryProductViewModel
    {
        public string ParentId { get; set; }

        public int Deleted { get; set; }

        public int Status { get; set; }

        public string Spelling { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }
    }
}