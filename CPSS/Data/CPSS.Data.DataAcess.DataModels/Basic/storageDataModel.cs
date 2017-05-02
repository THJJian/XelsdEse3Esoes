namespace CPSS.Data.DataAcess.DataModels
{
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
        public short deleted { get; set; }
        public string comment { get; set; }
        public int sort { get; set; }
    }
}