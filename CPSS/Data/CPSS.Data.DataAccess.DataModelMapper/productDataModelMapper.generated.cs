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
    /// 手动映射DataReader到productDataModel
    /// </summary>
    public partial class DataReaderToproductDataModelMapper
    {
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void Map(IDataReader dataReader, CPSS.Data.DataAcess.DataModels.productDataModel entity)
	    {            
            entity.proid = (int)dataReader["proid"];
            entity.classid = (string)dataReader["classid"];
            entity.parentid = (string)dataReader["parentid"];
            entity.childcount = (int)dataReader["childcount"];
            entity.serialnumber = (string)dataReader["serialnumber"];
            entity.name = (string)dataReader["name"];
            entity.pinyin = (string)dataReader["pinyin"];
            entity.alias = (string)dataReader["alias"];
            entity.standard = (string)dataReader["standard"];
            entity.modal = (string)dataReader["modal"];
            entity.permitcode = (string)dataReader["permitcode"];
            entity.brand = (string)dataReader["brand"];
            entity.trademark = (string)dataReader["trademark"];
            entity.makearea = (string)dataReader["makearea"];
            entity.barcode = (string)dataReader["barcode"];
            entity.price = (decimal)dataReader["price"];
            entity.taxrate = (decimal)dataReader["taxrate"];
            entity.unitid = (short)dataReader["unitid"];
            entity.validmonth = (short)dataReader["validmonth"];
            entity.validday = (short)dataReader["validday"];
            entity.status =  (short)dataReader["status"];
            entity.costmethod = (short)dataReader["costmethod"];
            entity.snmanage = (int)dataReader["snmanage"];
            entity.snlength = (short)dataReader["snlength"];
            entity.deleted = (short)dataReader["deleted"];
            entity.sort = (int)dataReader["sort"];
            entity.comment = (string)dataReader["comment"];
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
	    public CPSS.Data.DataAcess.DataModels.productDataModel Map(IDataReader dataReader)
	    {
			var result = new CPSS.Data.DataAcess.DataModels.productDataModel();
            result.proid = (int)dataReader["proid"];
            result.classid = (string)dataReader["classid"];
            result.parentid = (string)dataReader["parentid"];
            result.childcount = (int)dataReader["childcount"];
            result.serialnumber = (string)dataReader["serialnumber"];
            result.name = (string)dataReader["name"];
            result.pinyin = (string)dataReader["pinyin"];
            result.alias = (string)dataReader["alias"];
            result.standard = (string)dataReader["standard"];
            result.modal = (string)dataReader["modal"];
            result.permitcode = (string)dataReader["permitcode"];
            result.brand = (string)dataReader["brand"];
            result.trademark = (string)dataReader["trademark"];
            result.makearea = (string)dataReader["makearea"];
            result.barcode = (string)dataReader["barcode"];
			result.price =  (decimal)dataReader["price"];
			result.taxrate = (decimal)dataReader["taxrate"];
            result.unitid = (short)dataReader["unitid"];
			result.validmonth = (short)dataReader["validmonth"];
			result.validday = (short)dataReader["validday"];
			result.status = (short)dataReader["status"];
			result.costmethod = (short)dataReader["costmethod"];
            result.snmanage = (int)dataReader["snmanage"];
            result.snlength = (short)dataReader["snlength"];
			result.deleted = (short)dataReader["deleted"];
            result.sort = (int)dataReader["sort"];
            result.comment = (string)dataReader["comment"];
			return result;
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void MapObject(IDataReader dataReader, object entity)
	    {
	        this.Map(dataReader, entity as CPSS.Data.DataAcess.DataModels.productDataModel);
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
