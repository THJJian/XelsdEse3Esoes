//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using CPSS.Data.DataAcess.DataModels;
using System.Data;

namespace CPSS.Data.DataAccess.Interfaces
{
	/// <summary>
    /// clientDataModel的数据访问层接口
    /// </summary>
	public partial interface IclientDataAccess
	{
		/// <summary>
		/// 根据编号获取clientDataModel
		/// </summary>
		clientDataModel GetclientDataModelById(int clientid);
		
        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="data"></param>
		/// <param name="tansaction">事务</param>
        /// <returns></returns>
        int Add(clientDataModel data, IDbTransaction tansaction);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="data"></param>
		/// <param name="tansaction">事务</param>
        int Update(clientDataModel data, IDbTransaction tansaction);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data"></param>
		/// <param name="tansaction">事务</param>
        int Delete(clientDataModel data, IDbTransaction tansaction);
	}

}
