using System;

namespace CPSS.Service.ViewService.ViewModels.Client.Respond
{
    [Serializable]
    public class RespondQueryClientViewModel
    {
        public string ZipCode { get; set; }

        public int Sort { get; set; }

        public short Status { get; set; }

        public string SerialNumber { get; set; }

        public int PriceMode { get; set; }

        public string Spelling { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string LinkTel { get; set; }

        public string LinkMan { get; set; }

        public string LinkAddress { get; set; }

        public short Deleted { get; set; }

        public string Credits { get; set; }

        public string Comment { get; set; }

        public int ClientId { get; set; }

        public string ClassId { get; set; }

        public int ChildNumber { get; set; }

        public string Alias { get; set; }

        public string Address { get; set; }
    }
}