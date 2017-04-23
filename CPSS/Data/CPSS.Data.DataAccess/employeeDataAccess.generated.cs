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
    /// employee的数据访问层
    /// </summary>
	public partial class employeeDataAccess : GenericDataAccessBase<employeeDataModel>, IemployeeDataAccess
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public employeeDataAccess(IDbConnection connection) : base(connection)
        {
        }

		/// <summary>
		/// 根据编号获取employee
		/// </summary>
		public employeeDataModel GetemployeeDataModelById(int empid)
		{
			this.ExecuteSQL = @"SELECT empid,classid,parentid,childnumber,serialnumber,name,pinyin,depid,lowestdiscount,prepaidmenttotal,prepayfeetotal,mobile,address,status,deleted,sort,comment From employee  WHERE  empid = @empid ";
			this.DataParameter = new DbParameter[]
			{
				new SqlParameter("@empid", empid),
			};
            return this.ExecuteReadSqlToemployeeDataModel();
		}

		/// <summary>
		/// 新增
		/// </summary>
		public int Add(employeeDataModel data, IDbTransaction tansaction)
	    {
            this.ExecuteSQL = @"INSERT INTO [employee] ([classid],[parentid],[childnumber],[serialnumber],[name],[pinyin],[depid],[lowestdiscount],[preinadvancetotal],[prepayfeetotal],[mobile],[address],[status],[deleted],[sort],[comment]) VALUES (@classid,@parentid,@childnumber,@serialnumber,@name,@pinyin,@depid,@lowestdiscount,@preinadvancetotal,@prepayfeetotal,@mobile,@address,@status,@deleted,@sort,@comment) 
 SELECT SCOPE_IDENTITY()";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@classid", data.classid),
                new SqlParameter("@parentid", data.parentid),
                new SqlParameter("@childnumber", data.childnumber),
                new SqlParameter("@serialnumber", data.serialnumber),
                new SqlParameter("@name", data.name),
                new SqlParameter("@pinyin", data.pinyin),
                new SqlParameter("@depid", data.depid),
                new SqlParameter("@lowestdiscount", data.lowestdiscount),
                new SqlParameter("@preinadvancetotal", data.preinadvancetotal),
                new SqlParameter("@prepayfeetotal", data.prepayfeetotal),
                new SqlParameter("@mobile", data.mobile),
                new SqlParameter("@address", data.address),
                new SqlParameter("@status", data.status),
                new SqlParameter("@deleted", data.deleted),
                new SqlParameter("@sort", data.sort),
                new SqlParameter("@comment", data.comment),
            };
	        return this.ExecuteNonQuery(tansaction, false);
	    }

		/// <summary>
		/// 新增
		/// </summary>
		public override int Add(employeeDataModel data)
	    {
			return this.Add(data, null);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public int Update(employeeDataModel data, IDbTransaction tansaction)
	    {
            this.ExecuteSQL = @"UPDATE employee SET  [classid] = @classid, [parentid] = @parentid, [childnumber] = @childnumber, [serialnumber] = @serialnumber, [name] = @name, [pinyin] = @pinyin, [depid] = @depid, [lowestdiscount] = @lowestdiscount, [preinadvancetotal] = @preinadvancetotal, [prepayfeetotal] = @prepayfeetotal, [mobile] = @mobile, [address] = @address, [status] = @status, [deleted] = @deleted, [sort] = @sort, [comment] = @comment WHERE  [empid] = @empid ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@empid", data.empid),
                new SqlParameter("@classid", data.classid),
                new SqlParameter("@parentid", data.parentid),
                new SqlParameter("@childnumber", data.childnumber),
                new SqlParameter("@serialnumber", data.serialnumber),
                new SqlParameter("@name", data.name),
                new SqlParameter("@pinyin", data.pinyin),
                new SqlParameter("@depid", data.depid),
                new SqlParameter("@lowestdiscount", data.lowestdiscount),
                new SqlParameter("@preinadvancetotal", data.preinadvancetotal),
                new SqlParameter("@prepayfeetotal", data.prepayfeetotal),
                new SqlParameter("@mobile", data.mobile),
                new SqlParameter("@address", data.address),
                new SqlParameter("@status", data.status),
                new SqlParameter("@deleted", data.deleted),
                new SqlParameter("@sort", data.sort),
                new SqlParameter("@comment", data.comment),
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public override int Update(employeeDataModel data)
	    {
			return this.Update(data, null);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public int Delete(employeeDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"DELETE FROM employee WHERE  [empid] = @empid ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@empid", data.empid),
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public override int Delete(employeeDataModel data)
	    {
			return this.Delete(data, null);
	    }
	    
	}
}
