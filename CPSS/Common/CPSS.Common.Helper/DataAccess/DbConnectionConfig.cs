// #region << 版 本 注 释 >>
// /*
//  * ========================================================================
//  * 文件名：  DbConnectionConfig.cs 
//  * 创建人：  唐洪建 
//  * 创建时间：2016-08-02 11:54
//  * 版本号：  V1.0.0.0 
//  * 描述：
//  * =====================================================================
//  * 修改人：
//  * 修改时间：2016-08-02 11:54
//  * 版本号： V1.0.0.1
//  * 描述：
// */
// #endregion

using System;

namespace CPSS.Common.Helper.DataAccess
{
    [Serializable]
    public class DbConnectionConfig
    {
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