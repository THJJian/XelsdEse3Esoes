namespace CPSS.Data.DataAcess.DataModels.MainPage
{
    public class LeftNavMenuDataModel
    {
        public int MenuID { set; get; }

        public string ClassID { set; get; }

        public string ParentClassID { set; get; }

        public string Title { set; get; }

        public string IconCls { set; get; }

        public string Url { set; get; }
    }
}