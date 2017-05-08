namespace CPSS.Service.ViewService.ViewModels.Client.Request
{
    public class RequestEditClientViewModel
    {
        public int ClientId { get; set; }

        public string ZipCode { get; set; }

        public int Sort { get; set; }

        public string SerialNumber { get; set; }

        public short PriceMode { get; set; }

        public string Spelling { get; set; }

        public string Name { get; set; }

        public string LinkMan { get; set; }

        public string LinkTel { get; set; }

        public string LinkAddress { get; set; }

        public decimal Credits { get; set; }

        public string Comment { get; set; }

        public string Alias { get; set; }

        public string Address { get; set; }
    }
}