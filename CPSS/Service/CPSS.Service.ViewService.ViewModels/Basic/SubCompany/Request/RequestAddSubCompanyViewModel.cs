namespace CPSS.Service.ViewService.ViewModels.SubCompany.Request
{
    public class RequestAddSubCompanyViewModel
    {
        public RequestAddSubCompanyViewModel()
        {
            this.ParentId = "000001";
        }

        public string Email { get; set; }

        public string LinkMan { get; set; }

        public string LinkTel { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public short PriceMode { get; set; }

        public string SerialNumber { get; set; }

        public string Spelling { get; set; }

        public string Comment { set; get; }

        public int Sort { set; get; }
    }
}