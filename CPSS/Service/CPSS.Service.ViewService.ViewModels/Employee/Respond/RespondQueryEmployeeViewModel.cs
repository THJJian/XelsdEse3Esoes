using System;

namespace CPSS.Service.ViewService.ViewModels.Employee.Respond
{
    [Serializable]
    public class RespondQueryEmployeeViewModel
    {
        public int EmpId { get; set; }
        
        public string ClassId { get; set; }
        
        public string ParentId { get; set; }
        
        public int ChildNumber { get; set; }

        public string SerialNumber { get; set; }
        
        public string Name { get; set; }
        
        public string Spelling { get; set; }

        public string DepName { set; get; }

        public short LowestDiscount { set; get; }

        public decimal PreInAdvanceTotal { set; get; }

        public decimal PrePayFeeTotal { set; get; }

        public string Mobile { set; get; }

        public string Address { set; get; }

        public int Status { get; set; }
        
        public short Deleted { get; set; }
        
        public int Sort { get; set; }
        
        public string Comment { get; set; }
    }
}