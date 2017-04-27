namespace CPSS.Service.ViewService.ViewModels.Storage.Request
{
    public class RequestQueryStorageViewModel
    {

        public string ParentId { get; set; }

        public string Alias { get; set; }

        public short Deleted { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string Spelling { get; set; }

        public short Status { get; set; }
    }
}