﻿// #region << 版 本 注 释 >>
// /*
//  * ========================================================================
//  * 文件名：  DataAccessHelper.cs 
//  * 创建人：  唐洪建 
//  * 创建时间：2016-08-02 11:07
//  * 版本号：  V1.0.0.0 
//  * 描述：
//  * =====================================================================
//  * 修改人：
//  * 修改时间：2016-08-02 11:07
//  * 版本号： V1.0.0.1
//  * 描述：
// */
// #endregion

using System;
using System.Data;
using System.Data.SqlClient;

namespace Usableness.Framework.DatabaseDb
{
    public class DataAccessHelper
    {
        #region Constants

        private const string NULLSQLCOMMAND = "SQL语句不能为空！";
        private const string NULLTABLEDATA = "表数据不能为空！";
        private const string NULLTABLENAME = "数据库表名不能为空！";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="dbParameters">参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <param name="connect">数据库连接</param>
        /// <param name="executeSQL">执行的SQL</param>
        public static int ExecuteNonQuery(IDbConnection connect, string executeSQL, IDbDataParameter[] dbParameters, bool isProc = false)
        {
            if (string.IsNullOrEmpty(executeSQL))
            {
                throw new Exception(NULLSQLCOMMAND);
            }
            var result = 0;

            try
            {
                if (connect != null && connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                if (connect != null)
                {
                    var command = connect.CreateCommand();
                    command.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
                    command.CommandText = executeSQL;
                    if (dbParameters != null)
                    {
                        foreach (var param in dbParameters)
                        {
                            command.Parameters.Add(param);
                        }
                    }
                    result = command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connect != null && connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
            return result;
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="dbParameters">参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <param name="transaction">事务</param>
        /// <param name="executeSQL">执行的SQL</param>
        public static int ExecuteNonQuery(IDbTransaction transaction, string executeSQL, IDbDataParameter[] dbParameters, bool isProc = false)
        {
            if (string.IsNullOrEmpty(executeSQL))
            {
                throw new Exception(NULLSQLCOMMAND);
            }
            var connect = transaction.Connection;
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            var command = connect.CreateCommand();
            command.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
            command.CommandText = executeSQL;
            command.Transaction = transaction;
            if (dbParameters == null) return command.ExecuteNonQuery();
            foreach (var param in dbParameters)
            {
                command.Parameters.Add(param);
            }
            return command.ExecuteNonQuery();
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="dbParameters">参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <param name="transaction">事务</param>
        /// <param name="executeSQL">执行的SQL</param>
        public static object ExecuteScalar(IDbTransaction transaction, string executeSQL, IDbDataParameter[] dbParameters, bool isProc = false)
        {
            if (string.IsNullOrEmpty(executeSQL))
            {
                throw new Exception(NULLSQLCOMMAND);
            }
            var connect = transaction.Connection;
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            var command = connect.CreateCommand();
            command.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
            command.CommandText = executeSQL;
            command.Transaction = transaction;
            if (dbParameters == null) return command.ExecuteScalar();
            foreach (var param in dbParameters)
            {
                command.Parameters.Add(param);
            }
            return command.ExecuteScalar();
        }

        /// <summary>
        ///     执行SQL
        /// </summary>
        /// <param name="dbParameters">参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <param name="connect">数据库连接</param>
        /// <param name="executeSQL">执行的SQL</param>
        public static object ExecuteScalar(IDbConnection connect, string executeSQL, IDbDataParameter[] dbParameters, bool isProc = false)
        {
            if (string.IsNullOrEmpty(executeSQL))
            {
                throw new Exception(NULLSQLCOMMAND);
            }
            object result = null;
            try
            {
                if (connect != null && connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                if (connect != null)
                {
                    var command = connect.CreateCommand();
                    command.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
                    command.CommandText = executeSQL;
                    if (dbParameters != null)
                    {
                        foreach (var param in dbParameters)
                        {
                            command.Parameters.Add(param);
                        }
                    }
                    result = command.ExecuteScalar();
                }
            }
            finally
            {
                if (connect != null && connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }

            return result;
        }

        /// <summary>
        ///     无事务的批量更新
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static void BatchExecuteScalar(IDbConnection connect, DataTable table, string tableName, int batchSize)
        {
            if (table == null || table.Rows.Count < 1)
            {
                throw new Exception(NULLTABLEDATA);
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception(NULLTABLENAME);
            }

            try
            {
                if (connect == null) throw new Exception("IDbConnection");
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                using (var bulkCopy = new SqlBulkCopy((SqlConnection)connect))
                {
                    bulkCopy.DestinationTableName = tableName;
                    foreach (DataColumn dc in table.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    }
                    bulkCopy.BatchSize = batchSize;
                    bulkCopy.BulkCopyTimeout = 10 * 60;
                    bulkCopy.WriteToServer(table);
                }
            }
            finally
            {
                if (connect != null && connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        /// <summary>
        ///     有事务的批量更新
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static void BatchExecuteScalar(IDbTransaction transaction, DataTable table, string tableName, int batchSize)
        {
            if (table == null || table.Rows.Count < 1)
            {
                throw new Exception(NULLTABLEDATA);
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception(NULLTABLENAME);
            }

            var connect = transaction.Connection as SqlConnection;
            if (connect == null) throw new Exception("IDbConnection");
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            using (var bulkCopy = new SqlBulkCopy(connect, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
            {
                bulkCopy.DestinationTableName = tableName;
                foreach (DataColumn dc in table.Columns)
                {
                    bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                }
                bulkCopy.BatchSize = batchSize;
                bulkCopy.BulkCopyTimeout = 10 * 60;
                bulkCopy.WriteToServer(table);
            }
        }

        /// <summary>
        ///     查询得到DataReader
        /// </summary>
        /// <param name="executeSql">执行的SQL</param>
        /// <param name="dbParameters">参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <param name="connect">数据库连接</param>
        /// <returns></returns>
        public static IDataReader QueryGetDataReader(IDbConnection connect, string executeSql, IDbDataParameter[] dbParameters, bool isProc = false)
        {
            if (string.IsNullOrEmpty(executeSql))
            {
                throw new Exception(NULLSQLCOMMAND);
            }
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            var command = connect.CreateCommand();
            command.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
            command.CommandText = executeSql;
            if (dbParameters == null) return command.ExecuteReader(CommandBehavior.CloseConnection);
            foreach (var param in dbParameters)
            {
                command.Parameters.Add(param);
            }
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        ///     查询得到DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="executeSql">执行的SQL</param>
        /// <param name="dbParameters">参数</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns></returns>
        public static IDataReader QueryGetDataReader(IDbTransaction trans, string executeSql, IDbDataParameter[] dbParameters, bool isProc = false)
        {
            var connect = trans.Connection;
            if (string.IsNullOrEmpty(executeSql))
            {
                throw new Exception(NULLSQLCOMMAND);
            }
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            var command = connect.CreateCommand();
            command.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
            command.CommandText = executeSql;
            command.Transaction = trans;
            if (dbParameters == null) return command.ExecuteReader(CommandBehavior.Default);
            foreach (var param in dbParameters)
            {
                command.Parameters.Add(param);
            }
            return command.ExecuteReader(CommandBehavior.Default);
        }

        #endregion
         
    }
}