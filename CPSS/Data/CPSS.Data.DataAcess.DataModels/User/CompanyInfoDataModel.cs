namespace CPSS.Data.DataAcess.DataModels.User
{
    public class CompanyInfoDataModel
    {
        //comid,dbserver,dbase,timeout,uid,pwd
        /// <summary>
        /// 用户公司ID
        /// </summary>
        public int comid { set; get; }

        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string dbserver { set; get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string dbase { set; get; }

        /// <summary>
        /// 连接超时时间(毫秒)
        /// </summary>
        public int timeout { set; get; }

        /// <summary>
        /// 连接数据库服务的数据库的账户ID
        /// </summary>
        public string uid { set; get; }

        /// <summary>
        /// 连接数据库服务的数据库的账户密码
        /// </summary>
        public string pwd { set; get; }
    }
}