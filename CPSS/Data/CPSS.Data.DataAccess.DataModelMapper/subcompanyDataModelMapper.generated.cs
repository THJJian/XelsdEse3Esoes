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
    /// 手动映射DataReader到subcompanyDataModel
    /// </summary>
    public partial class DataReaderTosubcompanyDataModelMapper
    {
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void Map(IDataReader dataReader, CPSS.Data.DataAcess.DataModels.subcompanyDataModel entity)
	    {            
            entity.subcomid = (int)dataReader["subcomid"];
            entity.classid = (string)dataReader["classid"];
            entity.parentid = (string)dataReader["parentid"];
            entity.childnumber = (int)dataReader["childnumber"];
            entity.childcount = (int)dataReader["childcount"];
            entity.serialnumber = (string)dataReader["serialnumber"];
            entity.name = (string)dataReader["name"];
            entity.pinyin = (string)dataReader["pinyin"];
            entity.pricemode = (short)dataReader["pricemode"];
            entity.email = (string)dataReader["email"];
            entity.linkman = (string)dataReader["linkman"];
            entity.linktel = (string)dataReader["linktel"];
            entity.status = (short)dataReader["status"];
            entity.comment = (string)dataReader["comment"];
            entity.sort = (int)dataReader["sort"];
            entity.modifydate = (System.Byte[])dataReader["modifydate"];
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
	    public CPSS.Data.DataAcess.DataModels.subcompanyDataModel Map(IDataReader dataReader)
	    {
			var result = new CPSS.Data.DataAcess.DataModels.subcompanyDataModel();
            result.subcomid = (int)dataReader["subcomid"];
            result.classid = (string)dataReader["classid"];
            result.parentid = (string)dataReader["parentid"];
            result.childnumber = (int)dataReader["childnumber"];
            result.childcount = (int)dataReader["childcount"];
            result.serialnumber = (string)dataReader["serialnumber"];
            result.name = (string)dataReader["name"];
            result.pinyin = (string)dataReader["pinyin"];
            result.pricemode = (short)dataReader["pricemode"];
            result.email = (string)dataReader["email"];
            result.linkman = (string)dataReader["linkman"];
            result.linktel = (string)dataReader["linktel"];
            result.status = (short)dataReader["status"];
            result.comment = (string)dataReader["comment"];
            result.sort = (int)dataReader["sort"];
            result.modifydate = (System.Byte[])dataReader["modifydate"];
			return result;
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void MapObject(IDataReader dataReader, object entity)
	    {
	        this.Map(dataReader, entity as CPSS.Data.DataAcess.DataModels.subcompanyDataModel);
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
