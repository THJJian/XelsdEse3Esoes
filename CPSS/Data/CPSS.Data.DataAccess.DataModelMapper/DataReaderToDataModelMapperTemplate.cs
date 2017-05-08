
namespace CPSS.Data.DataAccess
{
	using System.Collections.Generic;
    using System.Data;
	using CPSS.Common.Core.DataAccess.DataAccess;
	using CPSS.Common.Core.Paging;

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
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.clientDataModel entity)
		{
			var mapper = new DataReaderToclientDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.departmentDataModel entity)
		{
			var mapper = new DataReaderTodepartmentDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.employeeDataModel entity)
		{
			var mapper = new DataReaderToemployeeDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.productDataModel entity)
		{
			var mapper = new DataReaderToproductDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.storageDataModel entity)
		{
			var mapper = new DataReaderTostorageDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.subcompanyDataModel entity)
		{
			var mapper = new DataReaderTosubcompanyDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.unitDataModel entity)
		{
			var mapper = new DataReaderTounitDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel entity)
		{
			var mapper = new DataReaderToHeadButtonsDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel entity)
		{
			var mapper = new DataReaderToLeftNavMenuDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel entity)
		{
			var mapper = new DataReaderToMenuRightCheckDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel entity)
		{
			var mapper = new DataReaderToSystemParameterConfigDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel entity)
		{
			var mapper = new DataReaderToUserDataModelMapper();
			mapper.Map(reader, entity);
		}
	
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
		public static void Map(IDataReader reader, CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel entity)
		{
			var mapper = new DataReaderToCompanyInfoDataModelMapper();
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
		public static CPSS.Data.DataAcess.DataModels.clientDataModel ExecuteReadSqlToclientDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.clientDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.clientDataModel ExecuteReadSqlToclientDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.clientDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.clientDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.clientDataModel> ExecuteReadSqlToclientDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.clientDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.clientDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.clientDataModel> ExecuteReadSqlToclientDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.clientDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.clientDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.clientDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.departmentDataModel ExecuteReadSqlTodepartmentDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.departmentDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.departmentDataModel ExecuteReadSqlTodepartmentDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.departmentDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.departmentDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.departmentDataModel> ExecuteReadSqlTodepartmentDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.departmentDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.departmentDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.departmentDataModel> ExecuteReadSqlTodepartmentDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.departmentDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.departmentDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.departmentDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.employeeDataModel ExecuteReadSqlToemployeeDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.employeeDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.employeeDataModel ExecuteReadSqlToemployeeDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.employeeDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.employeeDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.employeeDataModel> ExecuteReadSqlToemployeeDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.employeeDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.employeeDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.employeeDataModel> ExecuteReadSqlToemployeeDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.employeeDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.employeeDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.employeeDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.productDataModel ExecuteReadSqlToproductDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.productDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.productDataModel ExecuteReadSqlToproductDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.productDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.productDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.productDataModel> ExecuteReadSqlToproductDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.productDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.productDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.productDataModel> ExecuteReadSqlToproductDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.productDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.productDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.productDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.storageDataModel ExecuteReadSqlTostorageDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.storageDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.storageDataModel ExecuteReadSqlTostorageDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.storageDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.storageDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.storageDataModel> ExecuteReadSqlTostorageDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.storageDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.storageDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.storageDataModel> ExecuteReadSqlTostorageDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.storageDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.storageDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.storageDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.subcompanyDataModel ExecuteReadSqlTosubcompanyDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.subcompanyDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.subcompanyDataModel ExecuteReadSqlTosubcompanyDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.subcompanyDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.subcompanyDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.subcompanyDataModel> ExecuteReadSqlTosubcompanyDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.subcompanyDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.subcompanyDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.subcompanyDataModel> ExecuteReadSqlTosubcompanyDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.subcompanyDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.subcompanyDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.subcompanyDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.unitDataModel ExecuteReadSqlTounitDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.unitDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.unitDataModel ExecuteReadSqlTounitDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.unitDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.unitDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.unitDataModel> ExecuteReadSqlTounitDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.unitDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.unitDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.unitDataModel> ExecuteReadSqlTounitDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.unitDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.unitDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.unitDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel ExecuteReadSqlToHeadButtonsDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel ExecuteReadSqlToHeadButtonsDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel> ExecuteReadSqlToHeadButtonsDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel> ExecuteReadSqlToHeadButtonsDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.HeadButtons.HeadButtonsDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel ExecuteReadSqlToLeftNavMenuDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel ExecuteReadSqlToLeftNavMenuDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel> ExecuteReadSqlToLeftNavMenuDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel> ExecuteReadSqlToLeftNavMenuDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.MainPage.LeftNavMenuDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel ExecuteReadSqlToMenuRightCheckDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel ExecuteReadSqlToMenuRightCheckDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel> ExecuteReadSqlToMenuRightCheckDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel> ExecuteReadSqlToMenuRightCheckDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.MenuRight.MenuRightCheckDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}
		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel ExecuteReadSqlToSystemParameterConfigDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel ExecuteReadSqlToSystemParameterConfigDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel> ExecuteReadSqlToSystemParameterConfigDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel> ExecuteReadSqlToSystemParameterConfigDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.SystemManage.SystemParameterConfigDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel ExecuteReadSqlToUserDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel ExecuteReadSqlToUserDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel> ExecuteReadSqlToUserDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel> ExecuteReadSqlToUserDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.SystemManage.UserManage.UserDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

		/// <summary>
		/// 执行sql转换到单个实体
		/// </summary>
		public static CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel ExecuteReadSqlToCompanyInfoDataModel(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				if(reader.Read())
				{
					var result = new CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel();
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
		public static CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel ExecuteReadSqlToCompanyInfoDataModel(this DataAccessBase dataAccessBase,IDbTransaction trans, bool isProc = false)
        {			
            var reader = dataAccessBase.QueryGetDataReader(trans,isProc);
			CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel result = null;
			if(reader.Read())
			{
				result = new CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel();
				DataReaderMapHelper.Map(reader, result);
			}
			reader.Close();
			dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			return result;
        }
		
		/// <summary>
		/// 执行sql转换到实体列表
		/// </summary>
		public static List<CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel> ExecuteReadSqlToCompanyInfoDataModelList(this DataAccessBase dataAccessBase, bool isProc = false)
        {			
			var result = new  List<CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel>();
            using(var reader = dataAccessBase.QueryGetDataReader(isProc))
			{
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel();
					DataReaderMapHelper.Map(reader, entity);
					result.Add(entity);
				}
				reader.Close();
				dataAccessBase.AfterDataAccessHandler(dataAccessBase.DataParameter);
			}
			return result;
        }
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel> ExecuteReadSqlToCompanyInfoDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.User.CompanyInfoDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

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
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel> ExecuteReadSqlToOnlineSigninUserDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.User.OnlineSigninUserDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}

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
		
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="dataAccessBase"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="pageSort">排序</param>
        /// <param name="isReturnTotalCount">是否返回总记录数</param>
        /// <returns></returns>
		public static PageData<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel> ExecuteReadSqlToSigninUserDataModelPageData(this DataAccessBase dataAccessBase, string primaryKey, int pageIndex = 1, int pageSize = 30, string pageSort = "", bool isReturnTotalCount = true)
		{			
			var result = new  PageData<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel>();
			using(var reader = dataAccessBase.GetPagingList(primaryKey, pageIndex, pageSize, pageSort, isReturnTotalCount))
			{
				var listData = new List<CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel>();
				while(reader.Read())
				{
					var entity = new CPSS.Data.DataAcess.DataModels.User.SigninUserDataModel();
					DataReaderMapHelper.Map(reader, entity);
					listData.Add(entity);
				}
				result.Datas = listData;
				result.PageIndex = pageIndex;
				result.PageSize = pageSize;
				if (!isReturnTotalCount) return result;
				reader.NextResult();
				if (reader.Read()) result.DataCount = (int) reader[0];
			}
			return result;
		}
	}
    #endregion
}

