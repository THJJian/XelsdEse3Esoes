//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
	/// <summary>
    /// Account的数据访问层
    /// </summary>
	public partial class AccountDataAccess : GenericDataAccessBase<AccountDataModel>, IAccountDataAccess
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public AccountDataAccess(IDbConnection connection) : base(connection)
        {
        }

		/// <summary>
		/// 根据编号获取Account
		/// </summary>
		public AccountDataModel GetAccountDataModelById(int account_id)
		{
			this.ExecuteSQL = @"SELECT account_id,class_id,parent_id,child_number,child_count,name,deleted,alias,serial_number,Comment,cur_total,ini_total,sysflag,sysrow,PinYin,total_01,total_02,total_03,total_04,total_05,total_06,total_07,total_08,total_09,total_10,total_11,total_12,bqtotal,sumtotal,cashaudit,direction,ModifyDate From Account  WHERE  account_id = @account_id ";
			this.DataParameter = new DbParameter[]
			{
				new SqlParameter("@account_id", account_id),                 
			};
            return this.ExecuteReadSqlToAccountDataModel();
		}

		/// <summary>
		/// 新增
		/// </summary>
		public int Add(AccountDataModel data, IDbTransaction tansaction)
	    {
            this.ExecuteSQL = @"INSERT INTO [Account] ([class_id],[parent_id],[child_number],[child_count],[name],[deleted],[alias],[serial_number],[Comment],[cur_total],[ini_total],[sysflag],[sysrow],[PinYin],[total_01],[total_02],[total_03],[total_04],[total_05],[total_06],[total_07],[total_08],[total_09],[total_10],[total_11],[total_12],[bqtotal],[sumtotal],[cashaudit],[direction],[ModifyDate]) VALUES (@class_id,@parent_id,@child_number,@child_count,@name,@deleted,@alias,@serial_number,@Comment,@cur_total,@ini_total,@sysflag,@sysrow,@PinYin,@total_01,@total_02,@total_03,@total_04,@total_05,@total_06,@total_07,@total_08,@total_09,@total_10,@total_11,@total_12,@bqtotal,@sumtotal,@cashaudit,@direction,@ModifyDate) 
 SELECT SCOPE_IDENTITY()";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@class_id", data.class_id),                 
                new SqlParameter("@parent_id", data.parent_id),                 
                new SqlParameter("@child_number", data.child_number),                 
                new SqlParameter("@child_count", data.child_count),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@deleted", data.deleted),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@serial_number", data.serial_number),                 
                new SqlParameter("@Comment", data.Comment),                 
                new SqlParameter("@cur_total", data.cur_total),                 
                new SqlParameter("@ini_total", data.ini_total),                 
                new SqlParameter("@sysflag", data.sysflag),                 
                new SqlParameter("@sysrow", data.sysrow),                 
                new SqlParameter("@PinYin", data.PinYin),                 
                new SqlParameter("@total_01", data.total_01),                 
                new SqlParameter("@total_02", data.total_02),                 
                new SqlParameter("@total_03", data.total_03),                 
                new SqlParameter("@total_04", data.total_04),                 
                new SqlParameter("@total_05", data.total_05),                 
                new SqlParameter("@total_06", data.total_06),                 
                new SqlParameter("@total_07", data.total_07),                 
                new SqlParameter("@total_08", data.total_08),                 
                new SqlParameter("@total_09", data.total_09),                 
                new SqlParameter("@total_10", data.total_10),                 
                new SqlParameter("@total_11", data.total_11),                 
                new SqlParameter("@total_12", data.total_12),                 
                new SqlParameter("@bqtotal", data.bqtotal),                 
                new SqlParameter("@sumtotal", data.sumtotal),                 
                new SqlParameter("@cashaudit", data.cashaudit),                 
                new SqlParameter("@direction", data.direction),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
            };
	        return this.ExecuteNonQuery(tansaction, false);
	    }

		/// <summary>
		/// 新增
		/// </summary>
		public override int Add(AccountDataModel data)
	    {
			return this.Add(data, null);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public int Update(AccountDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"UPDATE Account SET  [class_id] = @class_id, [parent_id] = @parent_id, [child_number] = @child_number, [child_count] = @child_count, [name] = @name, [deleted] = @deleted, [alias] = @alias, [serial_number] = @serial_number, [Comment] = @Comment, [cur_total] = @cur_total, [ini_total] = @ini_total, [sysflag] = @sysflag, [sysrow] = @sysrow, [PinYin] = @PinYin, [total_01] = @total_01, [total_02] = @total_02, [total_03] = @total_03, [total_04] = @total_04, [total_05] = @total_05, [total_06] = @total_06, [total_07] = @total_07, [total_08] = @total_08, [total_09] = @total_09, [total_10] = @total_10, [total_11] = @total_11, [total_12] = @total_12, [bqtotal] = @bqtotal, [sumtotal] = @sumtotal, [cashaudit] = @cashaudit, [direction] = @direction, [ModifyDate] = @ModifyDate WHERE  [account_id] = @account_id ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@account_id", data.account_id),                 
                new SqlParameter("@class_id", data.class_id),                 
                new SqlParameter("@parent_id", data.parent_id),                 
                new SqlParameter("@child_number", data.child_number),                 
                new SqlParameter("@child_count", data.child_count),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@deleted", data.deleted),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@serial_number", data.serial_number),                 
                new SqlParameter("@Comment", data.Comment),                 
                new SqlParameter("@cur_total", data.cur_total),                 
                new SqlParameter("@ini_total", data.ini_total),                 
                new SqlParameter("@sysflag", data.sysflag),                 
                new SqlParameter("@sysrow", data.sysrow),                 
                new SqlParameter("@PinYin", data.PinYin),                 
                new SqlParameter("@total_01", data.total_01),                 
                new SqlParameter("@total_02", data.total_02),                 
                new SqlParameter("@total_03", data.total_03),                 
                new SqlParameter("@total_04", data.total_04),                 
                new SqlParameter("@total_05", data.total_05),                 
                new SqlParameter("@total_06", data.total_06),                 
                new SqlParameter("@total_07", data.total_07),                 
                new SqlParameter("@total_08", data.total_08),                 
                new SqlParameter("@total_09", data.total_09),                 
                new SqlParameter("@total_10", data.total_10),                 
                new SqlParameter("@total_11", data.total_11),                 
                new SqlParameter("@total_12", data.total_12),                 
                new SqlParameter("@bqtotal", data.bqtotal),                 
                new SqlParameter("@sumtotal", data.sumtotal),                 
                new SqlParameter("@cashaudit", data.cashaudit),                 
                new SqlParameter("@direction", data.direction),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public override int Update(AccountDataModel data)
	    {
			return this.Update(data, null);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public int Delete(AccountDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"DELETE FROM Account WHERE  [account_id] = @account_id ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@account_id", data.account_id),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public override int Delete(AccountDataModel data)
	    {
			return this.Delete(data, null);
	    }
	    
	}
}
