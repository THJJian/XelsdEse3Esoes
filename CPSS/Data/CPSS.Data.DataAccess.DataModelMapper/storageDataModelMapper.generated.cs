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
    /// 手动映射DataReader到storageDataModel
    /// </summary>
    public partial class DataReaderTostorageDataModelMapper
    {
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void Map(IDataReader dataReader, CPSS.Data.DataAcess.DataModels.storageDataModel entity)
	    {            
            entity.stoid = (int)dataReader["stoid"];
            entity.classid = (string)dataReader["classid"];
            entity.parentid = (string)dataReader["parentid"];
            entity.childnumber = (int)dataReader["childnumber"];
            entity.childcount = (int)dataReader["childcount"];
            entity.serialnumber = (string)dataReader["serialnumber"];
            entity.name = (string)dataReader["name"];
            entity.PinYin = (string)dataReader["PinYin"];
            entity.alias = (string)dataReader["alias"];
            entity.status = (short)dataReader["status"];
            entity.deleted = (short)dataReader["deleted"];
            entity.comment = (string)dataReader["comment"];
            entity.sort = (int)dataReader["sort"];
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
	    public CPSS.Data.DataAcess.DataModels.storageDataModel Map(IDataReader dataReader)
	    {
			var result = new CPSS.Data.DataAcess.DataModels.storageDataModel();
            result.stoid = (int)dataReader["stoid"];
            result.classid = (string)dataReader["classid"];
            result.parentid = (string)dataReader["parentid"];
            result.childnumber = (int)dataReader["childnumber"];
            result.childcount = (int)dataReader["childcount"];
            result.serialnumber = (string)dataReader["serialnumber"];
            result.name = (string)dataReader["name"];
            result.PinYin = (string)dataReader["PinYin"];
            result.alias = (string)dataReader["alias"];
            result.status = (short)dataReader["status"];
			result.deleted = (short)dataReader["deleted"];
            result.comment = (string)dataReader["comment"];
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
	        this.Map(dataReader, entity as CPSS.Data.DataAcess.DataModels.storageDataModel);
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
