// #region << 版 本 注 释 >>
// /*
//  * ========================================================================
//  * 文件名：  GenericDataAccessBase.cs 
//  * 创建人：  唐洪建 
//  * 创建时间：2016-08-02 16:09
//  * 版本号：  V1.0.0.0 
//  * 描述：
//  * =====================================================================
//  * 修改人：
//  * 修改时间：2016-08-02 16:09
//  * 版本号： V1.0.0.1
//  * 描述：
// */
// #endregion

using System;
using System.Data;

namespace Usableness.Framework.DatabaseDb
{
    public abstract class GenericDataAccessBase<T> : DataAccessBase where T : class
    {
        protected GenericDataAccessBase(IDbConnection _connection) : base(_connection)
        {
        }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns>返回受影响的行数</returns>
        public virtual int Add(T data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns>返回受影响的行数</returns>
        public virtual int Update(T data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns>返回受影响的行数</returns>
        public virtual int Delete(T data)
        {
            throw new NotImplementedException();
        }
    }
}