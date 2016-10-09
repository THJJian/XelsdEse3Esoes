using System;
using System.Data;

namespace CPSS.Common.Core.DataAccess.DataAccess
{
    public static class DbConnectionExtension
    {
        /// <summary>
        /// 需要在同一个事务里面执行的SQL相关操作调用此方法。
        /// </summary>
        /// <param name="dbConnection">连接对象</param>
        /// <param name="action">需要执行的委托函数(需要在同一个事务内执行的代码写在此委托函数内)</param>
        public static void ExecuteTransaction(this IDbConnection dbConnection, Action<IDbTransaction> action)
        {
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            var transaction = dbConnection.BeginTransaction();
            try
            {
                action(transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }
}