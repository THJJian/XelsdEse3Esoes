using System;

namespace CPSS.Service.ViewService.ViewModels.Department.Respond
{
    [Serializable]
    public class RespondQueryDepartmentViewModel
    {
        public string ClassId { get; set; }

        public int ComId { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public string LinkMan { get; set; }

        public string LinkTel { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public string PriceMode { get; set; }

        public string SerialNumber { get; set; }

        public string sort { get; set; }

        public string Status { get; set; }

        public string Spelling { get; set; }

        public int ChildNumber { get; set; }

        public short Deleted { get; set; }
    }
}