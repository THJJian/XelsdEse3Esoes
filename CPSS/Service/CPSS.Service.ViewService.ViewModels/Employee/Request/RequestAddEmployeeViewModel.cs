namespace CPSS.Service.ViewService.ViewModels.Employee.Request
{
    public class RequestAddEmployeeViewModel
    {

        public string ParentId { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Spelling { get; set; }

        public int DepId { set; get; }

        public string DepName { set; get; }

        public short LowestDiscount { set; get; }

        /// <summary>
        /// 预收金额上限
        /// </summary>
        public decimal PreInAdvanceTotal { set; get; }

        /// <summary>
        /// 预付金额上限
        /// </summary>
        public decimal PrepayFeeTotal { set; get; }

        public string Mobile { set; get; }

        public string Address { set; get; }

        public int Sort { get; set; }

        public string Comment { get; set; }
    }
}