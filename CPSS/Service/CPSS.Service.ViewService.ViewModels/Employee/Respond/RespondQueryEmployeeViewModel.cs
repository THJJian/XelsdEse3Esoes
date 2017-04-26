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

        /// <summary>
        /// 最低折扣
        /// </summary>
        public short LowestDiscount { set; get; }

        /// <summary>
        /// 预收金额上限
        /// </summary>
        public string PreInAdvanceTotal { set; get; }

        /// <summary>
        /// 预付金额上限
        /// </summary>
        public string PrePayFeeTotal { set; get; }

        public string Mobile { set; get; }

        public string Address { set; get; }

        public short Status { get; set; }
        
        public short Deleted { get; set; }
        
        public int Sort { get; set; }
        
        public string Comment { get; set; }
    }
}