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
    /// 手动映射DataReader到employeeDataModel
    /// </summary>
    public partial class DataReaderToemployeeDataModelMapper
    {
		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="entity"></param>
		public void Map(IDataReader dataReader, CPSS.Data.DataAcess.DataModels.employeeDataModel entity)
	    {            
            entity.empid = (int)dataReader["empid"];
            entity.classid = (string)dataReader["classid"];
            entity.parentid = (string)dataReader["parentid"];
            entity.childnumber = (int)dataReader["childnumber"];
            entity.serialnumber = (string)dataReader["serialnumber"];
            entity.name = (string)dataReader["name"];
            entity.pinyin = (string)dataReader["pinyin"];
            entity.depid = (int)dataReader["depid"];
            entity.depname = (string)dataReader["depname"];
            entity.lowestdiscount = (short)dataReader["lowestdiscount"];
            entity.preinadvancetotal = (System.Decimal)dataReader["preinadvancetotal"];
            entity.prepayfeetotal = (System.Decimal)dataReader["prepayfeetotal"];
            entity.mobile = (string)dataReader["mobile"];
            entity.address = (string)dataReader["address"];
            entity.status = (short)dataReader["status"];
            entity.deleted = (short)dataReader["deleted"];
            entity.sort = (int)dataReader["sort"];
            entity.comment = (string)dataReader["comment"];
	    }

		/// <summary>
        /// 映射
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
	    public CPSS.Data.DataAcess.DataModels.employeeDataModel Map(IDataReader dataReader)
	    {
			var result = new CPSS.Data.DataAcess.DataModels.employeeDataModel();
            result.empid = (int)dataReader["empid"];
            result.classid = (string)dataReader["classid"];
            result.parentid = (string)dataReader["parentid"];
            result.childnumber = (int)dataReader["childnumber"];
            result.serialnumber = (string)dataReader["serialnumber"];
            result.name = (string)dataReader["name"];
            result.pinyin = (string)dataReader["pinyin"];
            result.depid = (int)dataReader["depid"];
            result.depname = (string)dataReader["depname"];
            result.lowestdiscount = (short)dataReader["lowestdiscount"];
            result.preinadvancetotal = (System.Decimal)dataReader["preinadvancetotal"];
            result.prepayfeetotal = (System.Decimal)dataReader["prepayfeetotal"];
            result.mobile = (string)dataReader["mobile"];
            result.address = (string)dataReader["address"];
            result.status = (short)dataReader["status"];
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
	        this.Map(dataReader, entity as CPSS.Data.DataAcess.DataModels.employeeDataModel);
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
