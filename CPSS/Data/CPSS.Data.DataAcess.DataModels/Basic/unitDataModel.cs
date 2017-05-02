namespace CPSS.Data.DataAcess.DataModels
{
    public partial class unitDataModel
    {
        public int unitid { get; set; }
        public string name { get; set; }
        public short status { get; set; }
        public short deleted { get; set; }
        public int sort { get; set; }
    }
}