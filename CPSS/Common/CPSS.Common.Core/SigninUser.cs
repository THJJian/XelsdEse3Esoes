﻿using System;

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
            this.CompanySerialNum = string.Empty;
            this.AddressIP = string.Empty;
            this.ConnectionConfig = null;
        }

        public int UserID { set; get; }

        public Guid UserID_g { set; get; }

        public string UserName { set; get; }

        public string CompanySerialNum { set; get; }

        public string AddressIP { set; get; }

        public DbConnectionConfig ConnectionConfig { set; get; }
    }
}