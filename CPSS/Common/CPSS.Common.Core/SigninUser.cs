using System;
using System.Security.Principal;

namespace CPSS.Common.Core
{
    [Serializable]
    public class SigninUser
    {
        public int UserID { set; get; }

        public Guid User_g { set; get; }

        public string UserName { set; get; }

        public string CompanySerialNum { set; get; }

        public string AddressIP { set; get; }

        public DbConnectionConfig ConnectionConfig { set; get; }
    }

    [Serializable]
    public class Identity : IIdentity
    {

        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        public bool IsAuthenticated
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }
}