using System;
using System.Data;
using System.Linq;

namespace CPSS.Common.Helper.DataAccess
{
    public class DataAccessBase
    {
        #region Fields

        private readonly IDbConnection mConnection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="_connection">数据库连接</param>
        public DataAccessBase(IDbConnection _connection)
        {
            this.mConnection = _connection;
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        ///     连接器
        /// </summary>
        public IDbConnection Connection
        {
            get { return this.mConnection; }
        }

        /// <summary>
        ///     参数集合
        /// </summary>
        public IDbDataParameter[] DataParameter { get; set; }

        /// <summary>
        ///     执行的sql
        /// </summary>
        public string ExecuteSQL { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 获取SQL返回参数的错误消息，如果错误信息有返回值将直接抛出异常
        /// 并清空IDbDataParameter数据组。
        /// </summary>
        /// <param name="dbDataParameters"></param>
        public void AfterDataAccessHandler(IDbDataParameter[] dbDataParameters)
        {
            string errMsg = string.Empty;
            if (dbDataParameters != null)
            {
                var parameters = dbDataParameters.Where(p => p.ParameterName.ToLower() == "@chvnerrmsg");
                foreach (var parameter in parameters)
                {
                    errMsg = parameter.Value.ToString();  
                }
            }
            this.DataParameter = null;
            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="isProc">是否是存储过程</param>
        public int ExecuteNonQuery(bool isProc = false)
        {
            var result = DataAccessHelper.ExecuteNonQuery(this.Connection, this.ExecuteSQL, this.DataParameter, isProc);
            this.AfterDataAccessHandler(this.DataParameter);
            return result;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="beforeClearDbParms">清除缓存前需要执行的函数，主要操作带返回参数的SQL语句的返回参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns></returns>
        public int ExecuteNonQuery(Action<IDbDataParameter[], Action<IDbDataParameter[]>> beforeClearDbParms, bool isProc = false)
        {
            var result = DataAccessHelper.ExecuteNonQuery(this.Connection, this.ExecuteSQL, this.DataParameter, isProc);
            beforeClearDbParms(this.DataParameter, this.AfterDataAccessHandler);
            return result;
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="isProc">是否是存储过程</param>
        public int ExecuteNonQuery(IDbTransaction transaction, bool isProc = false)
        {
            if (transaction == null) throw new Exception("IDbTransaction");
            var result = DataAccessHelper.ExecuteNonQuery(transaction, this.ExecuteSQL, this.DataParameter, isProc);
            this.AfterDataAccessHandler(this.DataParameter);
            return result;
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="beforeClearDbParms">处理返回参数</param>
        /// <param name="isProc">是否是存储过程</param>
        public int ExecuteNonQuery(IDbTransaction transaction, Action<IDbDataParameter[], Action<IDbDataParameter[]>> beforeClearDbParms, bool isProc = false)
        {
            if (transaction == null) throw new Exception("IDbTransaction");
            var result = DataAccessHelper.ExecuteNonQuery(transaction, this.ExecuteSQL, this.DataParameter, isProc);
            beforeClearDbParms(this.DataParameter, this.AfterDataAccessHandler);
            return result;
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="isProc">是否是存储过程</param>
        public object ExecuteScalar(bool isProc = false)
        {
            var result = DataAccessHelper.ExecuteScalar(this.Connection, this.ExecuteSQL, this.DataParameter, isProc);
            this.AfterDataAccessHandler(this.DataParameter);
            return result;
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="beforeClearDbParams">处理返回参数</param>
        /// <param name="isProc">是否是存储过程</param>
        public object ExecuteScalar(Action<IDbDataParameter[], Action<IDbDataParameter[]>> beforeClearDbParams,  bool isProc = false)
        {
            var result = DataAccessHelper.ExecuteScalar(this.Connection, this.ExecuteSQL, this.DataParameter, isProc);
            beforeClearDbParams(this.DataParameter, this.AfterDataAccessHandler);
            return result;
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="isProc">是否是存储过程</param>
        public object ExecuteScalar(IDbTransaction transaction, bool isProc = false)
        {
            if (transaction == null) throw new Exception("IDbTransaction");
            var result = DataAccessHelper.ExecuteScalar(transaction, this.ExecuteSQL, this.DataParameter, isProc);
            this.AfterDataAccessHandler(this.DataParameter);
            return result;
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="beforeClearDbParams">处理返回参数</param>
        /// <param name="isProc">是否是存储过程</param>
        public object ExecuteScalar(IDbTransaction transaction, Action<IDbDataParameter[], Action<IDbDataParameter[]>> beforeClearDbParams,  bool isProc = false)
        {
            if (transaction == null) throw new Exception("IDbTransaction");
            var result = DataAccessHelper.ExecuteScalar(transaction, this.ExecuteSQL, this.DataParameter, isProc);
            beforeClearDbParams(this.DataParameter, this.AfterDataAccessHandler);
            return result;
        }

        /// <summary>
        ///     批量插入数据库
        /// </summary>
        /// <param name="table">数据表，列字段及类型必须要与目标表列字段名及类型一致</param>
        /// <param name="tableName">数据库目标表名</param>
        /// <param name="batchSize">每批插入的行数</param>
        /// <param name="transaction">事务</param>
        public void BatchExecuteScalar(DataTable table, string tableName, int batchSize, IDbTransaction transaction = null)
        {
            if (transaction == null)
                DataAccessHelper.BatchExecuteScalar(this.Connection, table, tableName, batchSize);
            else
                DataAccessHelper.BatchExecuteScalar(transaction, table, tableName, batchSize);
        }

        /// <summary>
        ///     查询得到DataReader
        /// </summary>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns></returns>
        public IDataReader QueryGetDataReader(bool isProc = false)
        {
            var result = DataAccessHelper.QueryGetDataReader(this.Connection, this.ExecuteSQL, this.DataParameter, isProc);
            this.AfterDataAccessHandler(this.DataParameter);
            return result;
        }

        /// <summary>
        ///     查询得到DataReader
        /// </summary>
        /// <param name="isProc">是否是存储过程</param>
        /// <param name="beforeClearDbParams">输出参数处理</param>
        /// <returns></returns>
        public IDataReader QueryGetDataReader(Action<IDbDataParameter[], Action<IDbDataParameter[]>> beforeClearDbParams, bool isProc = false)
        {
            var result = DataAccessHelper.QueryGetDataReader(this.Connection, this.ExecuteSQL, this.DataParameter, isProc);
            beforeClearDbParams(this.DataParameter, this.AfterDataAccessHandler);
            return result;
        }

        /// <summary>
        ///     查询得到DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns></returns>
        public IDataReader QueryGetDataReader(IDbTransaction trans, bool isProc = false)
        {
            var result = DataAccessHelper.QueryGetDataReader(trans, this.ExecuteSQL, this.DataParameter, isProc);
            this.AfterDataAccessHandler(this.DataParameter);
            return result;
        }

        /// <summary>
        ///     查询得到DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="beforeClearDbParams">处理数据参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns></returns>
        public IDataReader QueryGetDataReader(IDbTransaction trans, Action<IDbDataParameter[], Action<IDbDataParameter[]>> beforeClearDbParams, bool isProc = false)
        {
            var result = DataAccessHelper.QueryGetDataReader(trans, this.ExecuteSQL, this.DataParameter, isProc);
            beforeClearDbParams(this.DataParameter, this.AfterDataAccessHandler);
            return result;
        }

        /// <summary>
        /// 需要在同一个事务里面执行的SQL相关操作调用此方法。
        /// </summary>
        /// <param name="action">需要执行的委托函数(需要在同一个事务内执行的代码写在此委托函数内)</param>
        public void ExecuteTransaction(Action<IDbTransaction> action)
        {
            if (this.mConnection.State == ConnectionState.Closed)
                this.mConnection.Open();

            var transaction = this.mConnection.BeginTransaction();
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
                this.mConnection.Close();
            }
        }

        #endregion
    }
}