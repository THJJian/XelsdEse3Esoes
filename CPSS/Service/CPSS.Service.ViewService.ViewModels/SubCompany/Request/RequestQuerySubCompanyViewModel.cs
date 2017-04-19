namespace CPSS.Service.ViewService.ViewModels.SubCompany.Request
{
    public class RequestQuerySubCompanyViewModel
    {
        public RequestQuerySubCompanyViewModel()
        {
            this.ParentId = "000001";
        }

        public string Email { get; set; }

        public string LinkMan { get; set; }

        public string LinkTel { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public int PriceMode { get; set; }

        public string SerialNumber { get; set; }

        public string Spelling { get; set; }

        public int Status { get; set; }

        public short Deleted { get; set; }
    }
}