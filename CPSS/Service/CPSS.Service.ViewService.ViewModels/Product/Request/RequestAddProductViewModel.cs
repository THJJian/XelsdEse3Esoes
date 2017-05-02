namespace CPSS.Service.ViewService.ViewModels.Product.Request
{
    public class RequestAddProductViewModel
    {

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string ParentId { get; set; }

        public string Spelling { get; set; }

        public int Sort { get; set; }
    }
}