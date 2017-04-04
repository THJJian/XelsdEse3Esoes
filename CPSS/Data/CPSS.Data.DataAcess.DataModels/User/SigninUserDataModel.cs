namespace CPSS.Data.DataAcess.DataModels.User
{
    public class SigninUserDataModel
    {
        public int userid { set; get; }

        public string username { set; get; }

        public int comid { set; get; }

        public string comname { set; get; }

        public bool ismanager { get; set; }

        public bool issystem { set; get; }
    }
}