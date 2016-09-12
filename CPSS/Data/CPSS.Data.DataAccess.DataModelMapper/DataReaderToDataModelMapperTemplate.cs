
namespace CPSS.Data.DataAccess
{
	using System.Collections.Generic;
    using System.Data;
    using CPSS.Common.Core.DataAccess;

    #region DataReaderMapHelper

	/// <summary>
    /// 映射帮助方法
    /// </summary>
	public static class DataReaderMapHelper
	{
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.AccountDataModel entity)
		{
			var mapper = new DataReaderToAccountDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.ClientsDataModel entity)
		{
			var mapper = new DataReaderToClientsDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.CompanyDataModel entity)
		{
			var mapper = new DataReaderToCompanyDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.DepartmentDataModel entity)
		{
			var mapper = new DataReaderToDepartmentDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.EmployeesDataModel entity)
		{
			var mapper = new DataReaderToEmployeesDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel entity)
		{
			var mapper = new DataReaderToOnlineSigninUserDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel entity)
		{
			var mapper = new DataReaderToSigninUserDataModelMapper();
			mapper.Map(reader, entity);
		}
		
	}
    #endregion

    #region DataAccessExecuteReaderExtentions

	/// <summary>
    /// 数据访问层扩展方法
    /// </summary>
	public static class DataAccessExecuteReaderExtentions
    {
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.AccountDataModel ExecuteReadSqlToAccountDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.AccountDataModel();
					DataReaderMapHelper.Map(reader, result);
					reader.Close();
					dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
					return result;
				}
                dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return null;
        }

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.AccountDataModel ExecuteReadSqlToAccountDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.AccountDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.AccountDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.AccountDataModel> ExecuteReadSqlToAccountDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.AccountDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.AccountDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
		///// <summary>
		///// 执行sql转换翻页数据
		///// </summary>
		//public static PageData<CPSS.Data.DataAcess.DataModels.AccountDataModel> ExecuteReadSqlToAccountDataModelPageData(this DataAccessBase dataAccessBase,  string sqlString, int pageIndex, int pageSize, string primaryKey, string pageSort, bool isReturnTotalCount)
        //{			
			//var result = new  PageData<CPSS.Data.DataAcess.DataModels.AccountDataModel>();
            //using(var reader = DataAccessHelper.GetPageList(dataAccessBase.Connection, sqlString, pageIndex, pageSize, primaryKey, pageSort, isReturnTotalCount))
			//{
				//var listData = new List<CPSS.Data.DataAcess.DataModels.AccountDataModel>();
				//while(reader.Read())
				//{
					//var entity = new CPSS.Data.DataAcess.DataModels.AccountDataModel();
					//DataReaderMapHelper.Map(reader, entity);
					//listData.Add(entity);
				//}
				//result.Datas = listData;
				//result.PageIndex = pageIndex;
			    //result.PageSize = pageSize;
				//if (isReturnTotalCount)
                //{
                    //reader.NextResult();
                    //if (reader.Read())
                    //{
                        //result.DataCount = (int) reader[0];
                    //}
                //}
			//}
			//return result;
        //}
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.ClientsDataModel ExecuteReadSqlToClientsDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.ClientsDataModel();
					DataReaderMapHelper.Map(reader, result);
					reader.Close();
					dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
					return result;
				}
                dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return null;
        }

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.ClientsDataModel ExecuteReadSqlToClientsDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.ClientsDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.ClientsDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.ClientsDataModel> ExecuteReadSqlToClientsDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.ClientsDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.ClientsDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
		///// <summary>
		///// 执行sql转换翻页数据
		///// </summary>
		//public static PageData<CPSS.Data.DataAcess.DataModels.ClientsDataModel> ExecuteReadSqlToClientsDataModelPageData(this DataAccessBase dataAccessBase,  string sqlString, int pageIndex, int pageSize, string primaryKey, string pageSort, bool isReturnTotalCount)
        //{			
			//var result = new  PageData<CPSS.Data.DataAcess.DataModels.ClientsDataModel>();
            //using(var reader = DataAccessHelper.GetPageList(dataAccessBase.Connection, sqlString, pageIndex, pageSize, primaryKey, pageSort, isReturnTotalCount))
			//{
				//var listData = new List<CPSS.Data.DataAcess.DataModels.ClientsDataModel>();
				//while(reader.Read())
				//{
					//var entity = new CPSS.Data.DataAcess.DataModels.ClientsDataModel();
					//DataReaderMapHelper.Map(reader, entity);
					//listData.Add(entity);
				//}
				//result.Datas = listData;
				//result.PageIndex = pageIndex;
			    //result.PageSize = pageSize;
				//if (isReturnTotalCount)
                //{
                    //reader.NextResult();
                    //if (reader.Read())
                    //{
                        //result.DataCount = (int) reader[0];
                    //}
                //}
			//}
			//return result;
        //}
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.CompanyDataModel ExecuteReadSqlToCompanyDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.CompanyDataModel();
					DataReaderMapHelper.Map(reader, result);
					reader.Close();
					dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
					return result;
				}
                dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return null;
        }

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.CompanyDataModel ExecuteReadSqlToCompanyDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.CompanyDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.CompanyDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.CompanyDataModel> ExecuteReadSqlToCompanyDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.CompanyDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.CompanyDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
		///// <summary>
		///// 执行sql转换翻页数据
		///// </summary>
		//public static PageData<CPSS.Data.DataAcess.DataModels.CompanyDataModel> ExecuteReadSqlToCompanyDataModelPageData(this DataAccessBase dataAccessBase,  string sqlString, int pageIndex, int pageSize, string primaryKey, string pageSort, bool isReturnTotalCount)
        //{			
			//var result = new  PageData<CPSS.Data.DataAcess.DataModels.CompanyDataModel>();
            //using(var reader = DataAccessHelper.GetPageList(dataAccessBase.Connection, sqlString, pageIndex, pageSize, primaryKey, pageSort, isReturnTotalCount))
			//{
				//var listData = new List<CPSS.Data.DataAcess.DataModels.CompanyDataModel>();
				//while(reader.Read())
				//{
					//var entity = new CPSS.Data.DataAcess.DataModels.CompanyDataModel();
					//DataReaderMapHelper.Map(reader, entity);
					//listData.Add(entity);
				//}
				//result.Datas = listData;
				//result.PageIndex = pageIndex;
			    //result.PageSize = pageSize;
				//if (isReturnTotalCount)
                //{
                    //reader.NextResult();
                    //if (reader.Read())
                    //{
                        //result.DataCount = (int) reader[0];
                    //}
                //}
			//}
			//return result;
        //}
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.DepartmentDataModel ExecuteReadSqlToDepartmentDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.DepartmentDataModel();
					DataReaderMapHelper.Map(reader, result);
					reader.Close();
					dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
					return result;
				}
                dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return null;
        }

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.DepartmentDataModel ExecuteReadSqlToDepartmentDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.DepartmentDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.DepartmentDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.DepartmentDataModel> ExecuteReadSqlToDepartmentDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.DepartmentDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.DepartmentDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
		///// <summary>
		///// 执行sql转换翻页数据
		///// </summary>
		//public static PageData<CPSS.Data.DataAcess.DataModels.DepartmentDataModel> ExecuteReadSqlToDepartmentDataModelPageData(this DataAccessBase dataAccessBase,  string sqlString, int pageIndex, int pageSize, string primaryKey, string pageSort, bool isReturnTotalCount)
        //{			
			//var result = new  PageData<CPSS.Data.DataAcess.DataModels.DepartmentDataModel>();
            //using(var reader = DataAccessHelper.GetPageList(dataAccessBase.Connection, sqlString, pageIndex, pageSize, primaryKey, pageSort, isReturnTotalCount))
			//{
				//var listData = new List<CPSS.Data.DataAcess.DataModels.DepartmentDataModel>();
				//while(reader.Read())
				//{
					//var entity = new CPSS.Data.DataAcess.DataModels.DepartmentDataModel();
					//DataReaderMapHelper.Map(reader, entity);
					//listData.Add(entity);
				//}
				//result.Datas = listData;
				//result.PageIndex = pageIndex;
			    //result.PageSize = pageSize;
				//if (isReturnTotalCount)
                //{
                    //reader.NextResult();
                    //if (reader.Read())
                    //{
                        //result.DataCount = (int) reader[0];
                    //}
                //}
			//}
			//return result;
        //}
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.EmployeesDataModel ExecuteReadSqlToEmployeesDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.EmployeesDataModel();
					DataReaderMapHelper.Map(reader, result);
					reader.Close();
					dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
					return result;
				}
                dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return null;
        }

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.EmployeesDataModel ExecuteReadSqlToEmployeesDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.EmployeesDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.EmployeesDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.EmployeesDataModel> ExecuteReadSqlToEmployeesDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.EmployeesDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.EmployeesDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
		///// <summary>
		///// 执行sql转换翻页数据
		///// </summary>
		//public static PageData<CPSS.Data.DataAcess.DataModels.EmployeesDataModel> ExecuteReadSqlToEmployeesDataModelPageData(this DataAccessBase dataAccessBase,  string sqlString, int pageIndex, int pageSize, string primaryKey, string pageSort, bool isReturnTotalCount)
        //{			
			//var result = new  PageData<CPSS.Data.DataAcess.DataModels.EmployeesDataModel>();
            //using(var reader = DataAccessHelper.GetPageList(dataAccessBase.Connection, sqlString, pageIndex, pageSize, primaryKey, pageSort, isReturnTotalCount))
			//{
				//var listData = new List<CPSS.Data.DataAcess.DataModels.EmployeesDataModel>();
				//while(reader.Read())
				//{
					//var entity = new CPSS.Data.DataAcess.DataModels.EmployeesDataModel();
					//DataReaderMapHelper.Map(reader, entity);
					//listData.Add(entity);
				//}
				//result.Datas = listData;
				//result.PageIndex = pageIndex;
			    //result.PageSize = pageSize;
				//if (isReturnTotalCount)
                //{
                    //reader.NextResult();
                    //if (reader.Read())
                    //{
                        //result.DataCount = (int) reader[0];
                    //}
                //}
			//}
			//return result;
        //}
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel ExecuteReadSqlToOnlineSigninUserDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel();
					DataReaderMapHelper.Map(reader, result);
					reader.Close();
					dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
					return result;
				}
                dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return null;
        }

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel ExecuteReadSqlToOnlineSigninUserDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel> ExecuteReadSqlToOnlineSigninUserDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
		///// <summary>
		///// 执行sql转换翻页数据
		///// </summary>
		//public static PageData<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel> ExecuteReadSqlToOnlineSigninUserDataModelPageData(this DataAccessBase dataAccessBase,  string sqlString, int pageIndex, int pageSize, string primaryKey, string pageSort, bool isReturnTotalCount)
        //{			
			//var result = new  PageData<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel>();
            //using(var reader = DataAccessHelper.GetPageList(dataAccessBase.Connection, sqlString, pageIndex, pageSize, primaryKey, pageSort, isReturnTotalCount))
			//{
				//var listData = new List<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel>();
				//while(reader.Read())
				//{
					//var entity = new CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel();
					//DataReaderMapHelper.Map(reader, entity);
					//listData.Add(entity);
				//}
				//result.Datas = listData;
				//result.PageIndex = pageIndex;
			    //result.PageSize = pageSize;
				//if (isReturnTotalCount)
                //{
                    //reader.NextResult();
                    //if (reader.Read())
                    //{
                        //result.DataCount = (int) reader[0];
                    //}
                //}
			//}
			//return result;
        //}
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel ExecuteReadSqlToSigninUserDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel();
					DataReaderMapHelper.Map(reader, result);
					reader.Close();
					dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
					return result;
				}
                dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return null;
        }

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel ExecuteReadSqlToSigninUserDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel> ExecuteReadSqlToSigninUserDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
		///// <summary>
		///// 执行sql转换翻页数据
		///// </summary>
		//public static PageData<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel> ExecuteReadSqlToSigninUserDataModelPageData(this DataAccessBase dataAccessBase,  string sqlString, int pageIndex, int pageSize, string primaryKey, string pageSort, bool isReturnTotalCount)
        //{			
			//var result = new  PageData<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel>();
            //using(var reader = DataAccessHelper.GetPageList(dataAccessBase.Connection, sqlString, pageIndex, pageSize, primaryKey, pageSort, isReturnTotalCount))
			//{
				//var listData = new List<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel>();
				//while(reader.Read())
				//{
					//var entity = new CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel();
					//DataReaderMapHelper.Map(reader, entity);
					//listData.Add(entity);
				//}
				//result.Datas = listData;
				//result.PageIndex = pageIndex;
			    //result.PageSize = pageSize;
				//if (isReturnTotalCount)
                //{
                    //reader.NextResult();
                    //if (reader.Read())
                    //{
                        //result.DataCount = (int) reader[0];
                    //}
                //}
			//}
			//return result;
        //}
	}
    #endregion
}

