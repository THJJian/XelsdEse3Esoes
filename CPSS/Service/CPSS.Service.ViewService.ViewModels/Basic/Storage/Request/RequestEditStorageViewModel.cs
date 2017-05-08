namespace CPSS.Service.ViewService.ViewModels.Storage.Request
{
    public class RequestEditStorageViewModel
    {

        public int StorageId { get; set; }

        public string Alias { get; set; }

        public string Comment { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public string SerialNumber { get; set; }

        public int Sort { get; set; }
    }
}