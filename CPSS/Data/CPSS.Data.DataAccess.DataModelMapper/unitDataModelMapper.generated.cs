//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;

namespace CPSS.Data.DataAccess
{
	/// <summary>
    /// 手动映射DataReader到unitDataModel
    /// </summary>
    public partial class DataReaderTounitDataModelMapper
    {
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void Map(IDataReader dataReader, CPSS.Data.DataAcess.DataModels.unitDataModel entity)
	    {            
            entity.unitid = (int)dataReader["unitid"];
            entity.name = (string)dataReader["name"];
            entity.status = (short)dataReader["status"];
            entity.deleted = (short)dataReader["deleted"];
            entity.sort = (int)dataReader["sort"];
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
	    public CPSS.Data.DataAcess.DataModels.unitDataModel Map(IDataReader dataReader)
	    {
			var result = new CPSS.Data.DataAcess.DataModels.unitDataModel();
            result.unitid = (int)dataReader["unitid"];
            result.name = (string)dataReader["name"];
            result.status = (short)dataReader["status"];
			result.deleted = (short)dataReader["deleted"];
			result.sort = (int)dataReader["sort"];
			return result;
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void MapObject(IDataReader dataReader, object entity)
	    {
	        this.Map(dataReader, entity as CPSS.Data.DataAcess.DataModels.unitDataModel);
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
	    public object MapObject(IDataReader dataReader)
	    {
	        return this.Map(dataReader);
	    }
	}
}
