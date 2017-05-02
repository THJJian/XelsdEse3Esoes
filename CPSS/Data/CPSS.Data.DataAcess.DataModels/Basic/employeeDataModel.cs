namespace CPSS.Data.DataAcess.DataModels
{
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
        public short lowestdiscount { get; set; }
        public decimal preinadvancetotal { get; set; }
        public decimal prepayfeetotal { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public short status { get; set; }
        public short deleted { get; set; }
        public int sort { get; set; }
        public string comment { get; set; }
    }
}