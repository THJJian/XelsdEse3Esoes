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
    
    public partial class storageDataModel
    {
        public int stoid { get; set; }
        public string classid { get; set; }
        public string parentid { get; set; }
        public int childnumber { get; set; }
        public int childcount { get; set; }
        public string serialnumber { get; set; }
        public string name { get; set; }
        public string PinYin { get; set; }
        public string alias { get; set; }
        public short status { get; set; }
        public int sort { get; set; }
        public string comment { get; set; }
        public byte[] modifydate { get; set; }
    }
}
