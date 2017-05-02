using System;

namespace CPSS.Service.ViewService.ViewModels.Product.Respond
{
    [Serializable]
    public class RespondQueryProductViewModel
    {

        public string ClassId { get; set; }

        public string Comment { get; set; }

        public short Deleted { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public string SerialNumber { get; set; }

        public short Status { get; set; }

        public string Spelling { get; set; }

        public int Sort { get; set; }

        public short ValidMonth { get; set; }

        public short ValidDay { get; set; }

        public short UnitId { get; set; }

        public string TradeMark { get; set; }

        public decimal TaxRate { get; set; }

        public string Standard { get; set; }

        public int SNManage { get; set; }

        public short SNLength { get; set; }

        public int ProId { get; set; }

        public string PermitCode { get; set; }

        public string Modal { get; set; }

        public string MakeArea { get; set; }

        public short CostMethod { get; set; }

        public int ChildCount { get; set; }

        public string Brand { get; set; }

        public string BarCode { get; set; }

        public string Alias { get; set; }
    }
}