namespace CPSS.Service.ViewService.ViewModels.Product.Request
{
    public class RequestEditProductViewModel
    {

        public int ProId { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string Spelling { get; set; }

        public string Comment { get; set; }

        public int Sort { get; set; }
    }
}