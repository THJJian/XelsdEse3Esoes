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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CPSS.Common.Helper.Cached;
using CPSS.Common.Helper.DataAccess;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
	/// <summary>
    /// Clients的数据访问层
    /// </summary>
	public partial class ClientsDataAccess : GenericDataAccessBase<ClientsDataModel>, IClientsDataAccess
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public ClientsDataAccess(IDbConnection connection) : base(connection)
        {
        }

		/// <summary>
		/// 根据编号获取Clients
		/// </summary>
		public ClientsDataModel GetClientsDataModelById(int client_id)
		{
			this.ExecuteSQL = @"SELECT client_id,class_id,parent_id,child_number,child_count,serial_number,name,alias,region_id,phone_number,address,zipcode,contact_personal,tax_number,acount_number,credit_total,pinyin,pricemode,sklimit,artotal,artotal_ini,aptotal,aptotal_ini,pre_artotal,pre_artotal_ini,pre_aptotal,pre_aptotal_ini,firstcheck,comment,csflag,deleted,ProtocolDate,ModifyDate,GMPNo,GSPNo,Cetype,property,comment1,comment2,comment3,comment4,comment5,RowIndex,e_id,StopUse,incrate From Clients  WHERE  client_id = @client_id ";
			this.DataParameter = new DbParameter[]
			{
				new SqlParameter("@client_id", client_id),                 
			};
            return this.ExecuteReadSqlToClientsDataModel();
		}

		/// <summary>
		/// 新增
		/// </summary>
		public int Add(ClientsDataModel data, IDbTransaction tansaction)
	    {
            this.ExecuteSQL = @"INSERT INTO [Clients] ([class_id],[parent_id],[child_number],[child_count],[serial_number],[name],[alias],[region_id],[phone_number],[address],[zipcode],[contact_personal],[tax_number],[acount_number],[credit_total],[pinyin],[pricemode],[sklimit],[artotal],[artotal_ini],[aptotal],[aptotal_ini],[pre_artotal],[pre_artotal_ini],[pre_aptotal],[pre_aptotal_ini],[firstcheck],[comment],[csflag],[deleted],[ProtocolDate],[ModifyDate],[GMPNo],[GSPNo],[Cetype],[property],[comment1],[comment2],[comment3],[comment4],[comment5],[RowIndex],[e_id],[StopUse],[incrate]) VALUES (@class_id,@parent_id,@child_number,@child_count,@serial_number,@name,@alias,@region_id,@phone_number,@address,@zipcode,@contact_personal,@tax_number,@acount_number,@credit_total,@pinyin,@pricemode,@sklimit,@artotal,@artotal_ini,@aptotal,@aptotal_ini,@pre_artotal,@pre_artotal_ini,@pre_aptotal,@pre_aptotal_ini,@firstcheck,@comment,@csflag,@deleted,@ProtocolDate,@ModifyDate,@GMPNo,@GSPNo,@Cetype,@property,@comment1,@comment2,@comment3,@comment4,@comment5,@RowIndex,@e_id,@StopUse,@incrate) 
 SELECT SCOPE_IDENTITY()";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@class_id", data.class_id),                 
                new SqlParameter("@parent_id", data.parent_id),                 
                new SqlParameter("@child_number", data.child_number),                 
                new SqlParameter("@child_count", data.child_count),                 
                new SqlParameter("@serial_number", data.serial_number),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@region_id", data.region_id),                 
                new SqlParameter("@phone_number", data.phone_number),                 
                new SqlParameter("@address", data.address),                 
                new SqlParameter("@zipcode", data.zipcode),                 
                new SqlParameter("@contact_personal", data.contact_personal),                 
                new SqlParameter("@tax_number", data.tax_number),                 
                new SqlParameter("@acount_number", data.acount_number),                 
                new SqlParameter("@credit_total", data.credit_total),                 
                new SqlParameter("@pinyin", data.pinyin),                 
                new SqlParameter("@pricemode", data.pricemode),                 
                new SqlParameter("@sklimit", data.sklimit),                 
                new SqlParameter("@artotal", data.artotal),                 
                new SqlParameter("@artotal_ini", data.artotal_ini),                 
                new SqlParameter("@aptotal", data.aptotal),                 
                new SqlParameter("@aptotal_ini", data.aptotal_ini),                 
                new SqlParameter("@pre_artotal", data.pre_artotal),                 
                new SqlParameter("@pre_artotal_ini", data.pre_artotal_ini),                 
                new SqlParameter("@pre_aptotal", data.pre_aptotal),                 
                new SqlParameter("@pre_aptotal_ini", data.pre_aptotal_ini),                 
                new SqlParameter("@firstcheck", data.firstcheck),                 
                new SqlParameter("@comment", data.comment),                 
                new SqlParameter("@csflag", data.csflag),                 
                new SqlParameter("@deleted", data.deleted),                 
                new SqlParameter("@ProtocolDate", data.ProtocolDate),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
                new SqlParameter("@GMPNo", data.GMPNo),                 
                new SqlParameter("@GSPNo", data.GSPNo),                 
                new SqlParameter("@Cetype", data.Cetype),                 
                new SqlParameter("@property", data.property),                 
                new SqlParameter("@comment1", data.comment1),                 
                new SqlParameter("@comment2", data.comment2),                 
                new SqlParameter("@comment3", data.comment3),                 
                new SqlParameter("@comment4", data.comment4),                 
                new SqlParameter("@comment5", data.comment5),                 
                new SqlParameter("@RowIndex", data.RowIndex),                 
                new SqlParameter("@e_id", data.e_id),                 
                new SqlParameter("@StopUse", data.StopUse),                 
                new SqlParameter("@incrate", data.incrate),                 
            };
	        return this.ExecuteNonQuery(tansaction, false);
	    }

		/// <summary>
		/// 新增
		/// </summary>
		public override int Add(ClientsDataModel data)
	    {
			return this.Add(data, null);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public int Update(ClientsDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"UPDATE Clients SET  [class_id] = @class_id, [parent_id] = @parent_id, [child_number] = @child_number, [child_count] = @child_count, [serial_number] = @serial_number, [name] = @name, [alias] = @alias, [region_id] = @region_id, [phone_number] = @phone_number, [address] = @address, [zipcode] = @zipcode, [contact_personal] = @contact_personal, [tax_number] = @tax_number, [acount_number] = @acount_number, [credit_total] = @credit_total, [pinyin] = @pinyin, [pricemode] = @pricemode, [sklimit] = @sklimit, [artotal] = @artotal, [artotal_ini] = @artotal_ini, [aptotal] = @aptotal, [aptotal_ini] = @aptotal_ini, [pre_artotal] = @pre_artotal, [pre_artotal_ini] = @pre_artotal_ini, [pre_aptotal] = @pre_aptotal, [pre_aptotal_ini] = @pre_aptotal_ini, [firstcheck] = @firstcheck, [comment] = @comment, [csflag] = @csflag, [deleted] = @deleted, [ProtocolDate] = @ProtocolDate, [ModifyDate] = @ModifyDate, [GMPNo] = @GMPNo, [GSPNo] = @GSPNo, [Cetype] = @Cetype, [property] = @property, [comment1] = @comment1, [comment2] = @comment2, [comment3] = @comment3, [comment4] = @comment4, [comment5] = @comment5, [RowIndex] = @RowIndex, [e_id] = @e_id, [StopUse] = @StopUse, [incrate] = @incrate WHERE  [client_id] = @client_id ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@client_id", data.client_id),                 
                new SqlParameter("@class_id", data.class_id),                 
                new SqlParameter("@parent_id", data.parent_id),                 
                new SqlParameter("@child_number", data.child_number),                 
                new SqlParameter("@child_count", data.child_count),                 
                new SqlParameter("@serial_number", data.serial_number),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@region_id", data.region_id),                 
                new SqlParameter("@phone_number", data.phone_number),                 
                new SqlParameter("@address", data.address),                 
                new SqlParameter("@zipcode", data.zipcode),                 
                new SqlParameter("@contact_personal", data.contact_personal),                 
                new SqlParameter("@tax_number", data.tax_number),                 
                new SqlParameter("@acount_number", data.acount_number),                 
                new SqlParameter("@credit_total", data.credit_total),                 
                new SqlParameter("@pinyin", data.pinyin),                 
                new SqlParameter("@pricemode", data.pricemode),                 
                new SqlParameter("@sklimit", data.sklimit),                 
                new SqlParameter("@artotal", data.artotal),                 
                new SqlParameter("@artotal_ini", data.artotal_ini),                 
                new SqlParameter("@aptotal", data.aptotal),                 
                new SqlParameter("@aptotal_ini", data.aptotal_ini),                 
                new SqlParameter("@pre_artotal", data.pre_artotal),                 
                new SqlParameter("@pre_artotal_ini", data.pre_artotal_ini),                 
                new SqlParameter("@pre_aptotal", data.pre_aptotal),                 
                new SqlParameter("@pre_aptotal_ini", data.pre_aptotal_ini),                 
                new SqlParameter("@firstcheck", data.firstcheck),                 
                new SqlParameter("@comment", data.comment),                 
                new SqlParameter("@csflag", data.csflag),                 
                new SqlParameter("@deleted", data.deleted),                 
                new SqlParameter("@ProtocolDate", data.ProtocolDate),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
                new SqlParameter("@GMPNo", data.GMPNo),                 
                new SqlParameter("@GSPNo", data.GSPNo),                 
                new SqlParameter("@Cetype", data.Cetype),                 
                new SqlParameter("@property", data.property),                 
                new SqlParameter("@comment1", data.comment1),                 
                new SqlParameter("@comment2", data.comment2),                 
                new SqlParameter("@comment3", data.comment3),                 
                new SqlParameter("@comment4", data.comment4),                 
                new SqlParameter("@comment5", data.comment5),                 
                new SqlParameter("@RowIndex", data.RowIndex),                 
                new SqlParameter("@e_id", data.e_id),                 
                new SqlParameter("@StopUse", data.StopUse),                 
                new SqlParameter("@incrate", data.incrate),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public override int Update(ClientsDataModel data)
	    {
			return this.Update(data, null);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public int Delete(ClientsDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"DELETE FROM Clients WHERE  [client_id] = @client_id ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@client_id", data.client_id),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public override int Delete(ClientsDataModel data)
	    {
			return this.Delete(data, null);
	    }
	    
	}
}
