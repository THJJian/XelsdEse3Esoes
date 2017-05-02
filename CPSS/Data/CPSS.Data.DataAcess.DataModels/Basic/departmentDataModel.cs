namespace CPSS.Data.DataAcess.DataModels
{
    public partial class departmentDataModel
    {
        public int depid { get; set; }
        public string classid { get; set; }
        public string parentid { get; set; }
        public int childnumber { get; set; }
        public int childcount { get; set; }
        public string serialnumber { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
        public int status { get; set; }
        public short deleted { get; set; }
        public int sort { get; set; }
        public string comment { get; set; }
    }
}