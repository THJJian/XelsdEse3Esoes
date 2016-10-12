namespace CPSS.Data.DataAcess.DataModels.User
{
    public class SigninUserDataModel
    {
        public int UserID { set; get; }

        public string UserName { set; get; }

        public int CompanySerialNum { set; get; }

        public string CompanyName { set; get; }

        public bool Manager { get; set; }

        public bool isSystem { set; get; }
    }
}