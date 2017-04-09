namespace CPSS.Service.ViewService.ViewModels.SubCompany.Request
{
    public class RequestEditSubCompanyViewModel
    {
        public int ComId { set; get; }

        public string Email { get; set; }

        public string LinkMan { get; set; }

        public string LinkTel { get; set; }

        public string Name { get; set; }

        public short PriceMode { get; set; }

        public string SerialNumber { get; set; }

        public string Spelling { get; set; }

        public string Comment { set; get; }

        public int Sort { set; get; }
    }
}