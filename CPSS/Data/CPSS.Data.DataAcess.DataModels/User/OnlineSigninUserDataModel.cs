using System;

namespace CPSS.Data.DataAcess.DataModels.User
{
    public class OnlineSigninUserDataModel
    {
        public int userid { get; set; }

        public string username { get; set; }

        public string userip { get; set; }

        public string browser { get; set; }

        public DateTime logintime { get; set; }

        public DateTime overtime { get; set; }

        public DateTime exptime { get; set; }

        public Guid sguid { get; set; }
    }
}