//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CPSS.Data.DataAcess.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class productDataModel
    {
        public int proid { get; set; }
        public string classid { get; set; }
        public string parentid { get; set; }
        public int childcount { get; set; }
        public string serialnumber { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
        public string alias { get; set; }
        public string standard { get; set; }
        public string modal { get; set; }
        public string permitcode { get; set; }
        public string brand { get; set; }
        public string trademark { get; set; }
        public string makearea { get; set; }
        public string barcode { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> taxrate { get; set; }
        public short unitid { get; set; }
        public Nullable<short> validmonth { get; set; }
        public Nullable<short> validday { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<short> costmethod { get; set; }
        public int snmanage { get; set; }
        public short snlength { get; set; }
        public int sort { get; set; }
        public string comment { get; set; }
        public byte[] ModifyDate { get; set; }
    }
}
