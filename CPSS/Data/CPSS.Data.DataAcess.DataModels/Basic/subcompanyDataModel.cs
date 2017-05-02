namespace CPSS.Data.DataAcess.DataModels
{
    public partial class subcompanyDataModel
    {
        public int subcomid { get; set; }
        public string classid { get; set; }
        public string parentid { get; set; }
        public int childnumber { get; set; }
        public string serialnumber { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
        public short pricemode { get; set; }
        public string email { get; set; }
        public string linkman { get; set; }
        public string linktel { get; set; }
        public short status { get; set; }
        public string comment { get; set; }
        public int sort { get; set; }
        public short deleted { get; set; }
    }
}