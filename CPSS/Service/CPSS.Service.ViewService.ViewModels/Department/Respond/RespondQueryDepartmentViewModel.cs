using System;

namespace CPSS.Service.ViewService.ViewModels.Department.Respond
{
    [Serializable]
    public class RespondQueryDepartmentViewModel
    {
        public int DepId { get; set; }
        public string ClassId { get; set; }
        public string ParentId { get; set; }
        public int ChildNumber { get; set; }
        public int ChildCount { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Spelling { get; set; }
        public int Status { get; set; }
        public short Deleted { get; set; }
        public string Sort { get; set; }
        public string Comment { get; set; }
    }
}