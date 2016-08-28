using System;
using System.Data;

namespace CPSS.Common.Helper.DataAccess
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