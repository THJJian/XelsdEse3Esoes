using System;

namespace CPSS.Common.Core
{
    [Serializable]
    public class SigninUser
    {
        public SigninUser()
        {
            this.UserID = 0;
            this.UserID_g = Guid.Empty;
            this.UserName = string.Empty;
            this.AddressIP = string.Empty;
            this.ConnectionConfig = null;
            this.IsSystem = false;
            this.IsManager = false;
        }

        public int UserID { set; get; }

        public Guid UserID_g { set; get; }

        public string UserName { set; get; }

        public int CompanySerialNum { set; get; }

        public string AddressIP { set; get; }

        public DbConnectionConfig ConnectionConfig { set; get; }

        /// <summary>
        /// 是否为整个saas系统的超级管理员
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 是否为公司超级管理员
        /// </summary>
        public bool IsManager { get; set; }
    }
}