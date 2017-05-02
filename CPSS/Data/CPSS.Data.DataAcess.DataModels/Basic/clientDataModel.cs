namespace CPSS.Data.DataAcess.DataModels
{
    public partial class clientDataModel
    {
        public int clientid { get; set; }
        public string classid { get; set; }
        public string parentid { get; set; }
        public int childnumber { get; set; }
        public int childcount { get; set; }
        public string serialnumber { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
        public string alias { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string linkman { get; set; }
        public string linktel { get; set; }
        public string linkaddress { get; set; }
        public decimal credits { get; set; }
        public short pricemode { get; set; }
        public string comment { get; set; }
        public short status { get; set; }
        public short deleted { get; set; }
        public int sort { get; set; }
    }
}