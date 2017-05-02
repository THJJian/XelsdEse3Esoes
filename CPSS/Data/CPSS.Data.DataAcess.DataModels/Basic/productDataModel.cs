namespace CPSS.Data.DataAcess.DataModels
{
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

        public decimal price { get; set; }

        public decimal taxrate { get; set; }

        public short unitid { get; set; }

        public short validmonth { get; set; }

        public short validday { get; set; }

        public short status { get; set; }

        public short costmethod { get; set; }

        public int snmanage { get; set; }

        public short snlength { get; set; }

        public short deleted { get; set; }

        public int sort { get; set; }

        public string comment { get; set; }
    }
}