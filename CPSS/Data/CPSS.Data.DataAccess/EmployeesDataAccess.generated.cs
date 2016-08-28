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
    /// Employees的数据访问层
    /// </summary>
	public partial class EmployeesDataAccess : GenericDataAccessBase<EmployeesDataModel>, IEmployeesDataAccess
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public EmployeesDataAccess(IDbConnection connection) : base(connection)
        {
        }

		/// <summary>
		/// 根据编号获取Employees
		/// </summary>
		public EmployeesDataModel GetEmployeesDataModelById(int emp_id)
		{
			this.ExecuteSQL = @"SELECT emp_id,class_id,parent_id,child_number,child_count,name,alias,serial_number,d_id,deleted,phone,address,comment,aplimit,arlimit,aptotal,artotal,discountlimit,ls,th,pinyin,LowPrice,HighPrice,SNCount,GSPSNCount,ModifyDate,Study,Duty,GraduateDate,szWork,InDate,Teach,deduct,lim,Tp_ID,RowIndex From Employees  WHERE  emp_id = @emp_id ";
			this.DataParameter = new DbParameter[]
			{
				new SqlParameter("@emp_id", emp_id),                 
			};
            return this.ExecuteReadSqlToEmployeesDataModel();
		}

		/// <summary>
		/// 新增
		/// </summary>
		public int Add(EmployeesDataModel data, IDbTransaction tansaction)
	    {
            this.ExecuteSQL = @"INSERT INTO [Employees] ([class_id],[parent_id],[child_number],[child_count],[name],[alias],[serial_number],[d_id],[deleted],[phone],[address],[comment],[aplimit],[arlimit],[aptotal],[artotal],[discountlimit],[ls],[th],[pinyin],[LowPrice],[HighPrice],[SNCount],[GSPSNCount],[ModifyDate],[Study],[Duty],[GraduateDate],[szWork],[InDate],[Teach],[deduct],[lim],[Tp_ID],[RowIndex]) VALUES (@class_id,@parent_id,@child_number,@child_count,@name,@alias,@serial_number,@d_id,@deleted,@phone,@address,@comment,@aplimit,@arlimit,@aptotal,@artotal,@discountlimit,@ls,@th,@pinyin,@LowPrice,@HighPrice,@SNCount,@GSPSNCount,@ModifyDate,@Study,@Duty,@GraduateDate,@szWork,@InDate,@Teach,@deduct,@lim,@Tp_ID,@RowIndex) 
 SELECT SCOPE_IDENTITY()";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@class_id", data.class_id),                 
                new SqlParameter("@parent_id", data.parent_id),                 
                new SqlParameter("@child_number", data.child_number),                 
                new SqlParameter("@child_count", data.child_count),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@serial_number", data.serial_number),                 
                new SqlParameter("@d_id", data.d_id),                 
                new SqlParameter("@deleted", data.deleted),                 
                new SqlParameter("@phone", data.phone),                 
                new SqlParameter("@address", data.address),                 
                new SqlParameter("@comment", data.comment),                 
                new SqlParameter("@aplimit", data.aplimit),                 
                new SqlParameter("@arlimit", data.arlimit),                 
                new SqlParameter("@aptotal", data.aptotal),                 
                new SqlParameter("@artotal", data.artotal),                 
                new SqlParameter("@discountlimit", data.discountlimit),                 
                new SqlParameter("@ls", data.ls),                 
                new SqlParameter("@th", data.th),                 
                new SqlParameter("@pinyin", data.pinyin),                 
                new SqlParameter("@LowPrice", data.LowPrice),                 
                new SqlParameter("@HighPrice", data.HighPrice),                 
                new SqlParameter("@SNCount", data.SNCount),                 
                new SqlParameter("@GSPSNCount", data.GSPSNCount),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
                new SqlParameter("@Study", data.Study),                 
                new SqlParameter("@Duty", data.Duty),                 
                new SqlParameter("@GraduateDate", data.GraduateDate),                 
                new SqlParameter("@szWork", data.szWork),                 
                new SqlParameter("@InDate", data.InDate),                 
                new SqlParameter("@Teach", data.Teach),                 
                new SqlParameter("@deduct", data.deduct),                 
                new SqlParameter("@lim", data.lim),                 
                new SqlParameter("@Tp_ID", data.Tp_ID),                 
                new SqlParameter("@RowIndex", data.RowIndex),                 
            };
	        return this.ExecuteNonQuery(tansaction, false);
	    }

		/// <summary>
		/// 新增
		/// </summary>
		public override int Add(EmployeesDataModel data)
	    {
			return this.Add(data, null);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public int Update(EmployeesDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"UPDATE Employees SET  [class_id] = @class_id, [parent_id] = @parent_id, [child_number] = @child_number, [child_count] = @child_count, [name] = @name, [alias] = @alias, [serial_number] = @serial_number, [d_id] = @d_id, [deleted] = @deleted, [phone] = @phone, [address] = @address, [comment] = @comment, [aplimit] = @aplimit, [arlimit] = @arlimit, [aptotal] = @aptotal, [artotal] = @artotal, [discountlimit] = @discountlimit, [ls] = @ls, [th] = @th, [pinyin] = @pinyin, [LowPrice] = @LowPrice, [HighPrice] = @HighPrice, [SNCount] = @SNCount, [GSPSNCount] = @GSPSNCount, [ModifyDate] = @ModifyDate, [Study] = @Study, [Duty] = @Duty, [GraduateDate] = @GraduateDate, [szWork] = @szWork, [InDate] = @InDate, [Teach] = @Teach, [deduct] = @deduct, [lim] = @lim, [Tp_ID] = @Tp_ID, [RowIndex] = @RowIndex WHERE  [emp_id] = @emp_id ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@emp_id", data.emp_id),                 
                new SqlParameter("@class_id", data.class_id),                 
                new SqlParameter("@parent_id", data.parent_id),                 
                new SqlParameter("@child_number", data.child_number),                 
                new SqlParameter("@child_count", data.child_count),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@serial_number", data.serial_number),                 
                new SqlParameter("@d_id", data.d_id),                 
                new SqlParameter("@deleted", data.deleted),                 
                new SqlParameter("@phone", data.phone),                 
                new SqlParameter("@address", data.address),                 
                new SqlParameter("@comment", data.comment),                 
                new SqlParameter("@aplimit", data.aplimit),                 
                new SqlParameter("@arlimit", data.arlimit),                 
                new SqlParameter("@aptotal", data.aptotal),                 
                new SqlParameter("@artotal", data.artotal),                 
                new SqlParameter("@discountlimit", data.discountlimit),                 
                new SqlParameter("@ls", data.ls),                 
                new SqlParameter("@th", data.th),                 
                new SqlParameter("@pinyin", data.pinyin),                 
                new SqlParameter("@LowPrice", data.LowPrice),                 
                new SqlParameter("@HighPrice", data.HighPrice),                 
                new SqlParameter("@SNCount", data.SNCount),                 
                new SqlParameter("@GSPSNCount", data.GSPSNCount),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
                new SqlParameter("@Study", data.Study),                 
                new SqlParameter("@Duty", data.Duty),                 
                new SqlParameter("@GraduateDate", data.GraduateDate),                 
                new SqlParameter("@szWork", data.szWork),                 
                new SqlParameter("@InDate", data.InDate),                 
                new SqlParameter("@Teach", data.Teach),                 
                new SqlParameter("@deduct", data.deduct),                 
                new SqlParameter("@lim", data.lim),                 
                new SqlParameter("@Tp_ID", data.Tp_ID),                 
                new SqlParameter("@RowIndex", data.RowIndex),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public override int Update(EmployeesDataModel data)
	    {
			return this.Update(data, null);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public int Delete(EmployeesDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"DELETE FROM Employees WHERE  [emp_id] = @emp_id ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@emp_id", data.emp_id),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public override int Delete(EmployeesDataModel data)
	    {
			return this.Delete(data, null);
	    }
	    
	}
}
