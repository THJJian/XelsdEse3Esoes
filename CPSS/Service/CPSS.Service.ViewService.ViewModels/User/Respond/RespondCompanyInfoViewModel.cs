using System;

namespace CPSS.Service.ViewService.ViewModels.User.Respond
{
    [Serializable]
    public class RespondCompanyInfoViewModel
    {
        /// <summary>
        /// 用户公司ID
        /// </summary>
        public int CompanyID { set; get; }

        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string Server { set; get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { set; get; }

        /// <summary>
        /// 连接超时时间(毫秒)
        /// </summary>
        public int ConnectTimeout { set; get; }

        /// <summary>
        /// 连接数据库服务的数据库的账户ID
        /// </summary>
        public string UserID { set; get; }

        /// <summary>
        /// 连接数据库服务的数据库的账户密码
        /// </summary>
        public string Password { set; get; }
    }
}