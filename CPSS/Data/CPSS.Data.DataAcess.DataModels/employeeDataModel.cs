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
    
    public partial class employeeDataModel
    {
        public int empid { get; set; }
        public string classid { get; set; }
        public string parentid { get; set; }
        public int childnumber { get; set; }
        public string serialnumber { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
        public int depid { get; set; }
        public string depname { get; set; }
        public Nullable<short> lowestdiscount { get; set; }
        public Nullable<decimal> preinadvancetotal { get; set; }
        public Nullable<decimal> prepayfeetotal { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<short> deleted { get; set; }
        public Nullable<int> sort { get; set; }
        public string comment { get; set; }
    }
}
