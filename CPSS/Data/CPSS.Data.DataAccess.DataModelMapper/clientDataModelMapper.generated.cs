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
    /// 手动映射DataReader到clientDataModel
    /// </summary>
    public partial class DataReaderToclientDataModelMapper
    {
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void Map(IDataReader dataReader, CPSS.Data.DataAcess.DataModels.clientDataModel entity)
	    {            
            entity.clientid = (int)dataReader["clientid"];
            entity.classid = (string)dataReader["classid"];
            entity.parentid = (string)dataReader["parentid"];
            entity.childnumber = (int)dataReader["childnumber"];
            entity.childcount = (int)dataReader["childcount"];
            entity.serialnumber = (string)dataReader["serialnumber"];
            entity.name = (string)dataReader["name"];
            entity.pinyin = (string)dataReader["pinyin"];
            entity.alias = (string)dataReader["alias"];
            entity.address = (string)dataReader["address"];
            entity.zipcode = (string)dataReader["zipcode"];
            entity.linkman = (string)dataReader["linkman"];
            entity.linktel = (string)dataReader["linktel"];
            entity.linkaddress = (string)dataReader["linkaddress"];
            entity.credits = dataReader.IsDBNull(dataReader.GetOrdinal("credits"))? null: (System.Nullable<System.Decimal>)dataReader["credits"];
            entity.pricemode = dataReader.IsDBNull(dataReader.GetOrdinal("pricemode"))? null: (System.Nullable<short>)dataReader["pricemode"];
            entity.comment = (string)dataReader["comment"];
            entity.status = dataReader.IsDBNull(dataReader.GetOrdinal("status"))? null: (int?)dataReader["status"];
            entity.deleted = dataReader.IsDBNull(dataReader.GetOrdinal("deleted"))? null: (System.Nullable<short>)dataReader["deleted"];
            entity.sort = dataReader.IsDBNull(dataReader.GetOrdinal("sort"))? null: (int?)dataReader["sort"];
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
	    public CPSS.Data.DataAcess.DataModels.clientDataModel Map(IDataReader dataReader)
	    {
			var result = new CPSS.Data.DataAcess.DataModels.clientDataModel();
            result.clientid = (int)dataReader["clientid"];
            result.classid = (string)dataReader["classid"];
            result.parentid = (string)dataReader["parentid"];
            result.childnumber = (int)dataReader["childnumber"];
            result.childcount = (int)dataReader["childcount"];
            result.serialnumber = (string)dataReader["serialnumber"];
            result.name = (string)dataReader["name"];
            result.pinyin = (string)dataReader["pinyin"];
            result.alias = (string)dataReader["alias"];
            result.address = (string)dataReader["address"];
            result.zipcode = (string)dataReader["zipcode"];
            result.linkman = (string)dataReader["linkman"];
            result.linktel = (string)dataReader["linktel"];
            result.linkaddress = (string)dataReader["linkaddress"];
			result.credits = dataReader.IsDBNull(dataReader.GetOrdinal("credits"))? null: (System.Nullable<System.Decimal>)dataReader["credits"];
			result.pricemode = dataReader.IsDBNull(dataReader.GetOrdinal("pricemode"))? null: (System.Nullable<short>)dataReader["pricemode"];
            result.comment = (string)dataReader["comment"];
			result.status = dataReader.IsDBNull(dataReader.GetOrdinal("status"))? null: (int?)dataReader["status"];
			result.deleted = dataReader.IsDBNull(dataReader.GetOrdinal("deleted"))? null: (System.Nullable<short>)dataReader["deleted"];
			result.sort = dataReader.IsDBNull(dataReader.GetOrdinal("sort"))? null: (int?)dataReader["sort"];
			return result;
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void MapObject(IDataReader dataReader, object entity)
	    {
	        this.Map(dataReader, entity as CPSS.Data.DataAcess.DataModels.clientDataModel);
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
