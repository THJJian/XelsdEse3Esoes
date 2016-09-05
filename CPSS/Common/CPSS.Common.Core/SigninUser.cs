using System;

namespace CPSS.Common.Core
{
    public class SigninUser
    {
        public int UserID { set; get; }

        public Guid User_g { set; get; }

        public string UserName { set; get; }

        public string CompanySerialNum { set; get; }

        public string ConnectionString { set; get; }
    }
}