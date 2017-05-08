using System;

namespace CPSS.Service.ViewService.ViewModels.Storage.Respond
{
    [Serializable]
    public class RespondQueryStorageViewModel
    {
        public short Status { get; set; }

        public int StorageId { get; set; }

        public int Sort { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public short? Deleted { get; set; }

        public string Comment { get; set; }

        public string ClassId { get; set; }

        public int ChildNumber { get; set; }

        public string Spelling { get; set; }

        public string ParentId { get; set; }

        public string Alias { get; set; }
    }
}