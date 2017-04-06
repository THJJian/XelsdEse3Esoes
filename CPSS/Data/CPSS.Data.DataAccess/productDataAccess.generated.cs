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
    /// product的数据访问层
    /// </summary>
	public partial class productDataAccess : GenericDataAccessBase<productDataModel>, IproductDataAccess
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public productDataAccess(IDbConnection connection) : base(connection)
        {
        }

		/// <summary>
		/// 根据编号获取product
		/// </summary>
		public productDataModel GetproductDataModelById(int proid)
		{
			this.ExecuteSQL = @"SELECT proid,classid,parentid,childcount,serialnumber,name,pinyin,alias,standard,modal,permitcode,brand,trademark,makearea,barcode,price,taxrate,unitid,validmonth,validday,status,costmethod,snmanage,snlength,sort,comment,ModifyDate From product  WHERE  proid = @proid ";
			this.DataParameter = new DbParameter[]
			{
				new SqlParameter("@proid", proid),                 
			};
            return this.ExecuteReadSqlToproductDataModel();
		}

		/// <summary>
		/// 新增
		/// </summary>
		public int Add(productDataModel data, IDbTransaction tansaction)
	    {
            this.ExecuteSQL = @"INSERT INTO [product] ([classid],[parentid],[childcount],[serialnumber],[name],[pinyin],[alias],[standard],[modal],[permitcode],[brand],[trademark],[makearea],[barcode],[price],[taxrate],[unitid],[validmonth],[validday],[status],[costmethod],[snmanage],[snlength],[sort],[comment],[ModifyDate]) VALUES (@classid,@parentid,@childcount,@serialnumber,@name,@pinyin,@alias,@standard,@modal,@permitcode,@brand,@trademark,@makearea,@barcode,@price,@taxrate,@unitid,@validmonth,@validday,@status,@costmethod,@snmanage,@snlength,@sort,@comment,@ModifyDate) 
 SELECT SCOPE_IDENTITY()";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@classid", data.classid),                 
                new SqlParameter("@parentid", data.parentid),                 
                new SqlParameter("@childcount", data.childcount),                 
                new SqlParameter("@serialnumber", data.serialnumber),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@pinyin", data.pinyin),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@standard", data.standard),                 
                new SqlParameter("@modal", data.modal),                 
                new SqlParameter("@permitcode", data.permitcode),                 
                new SqlParameter("@brand", data.brand),                 
                new SqlParameter("@trademark", data.trademark),                 
                new SqlParameter("@makearea", data.makearea),                 
                new SqlParameter("@barcode", data.barcode),                 
                new SqlParameter("@price", data.price),                 
                new SqlParameter("@taxrate", data.taxrate),                 
                new SqlParameter("@unitid", data.unitid),                 
                new SqlParameter("@validmonth", data.validmonth),                 
                new SqlParameter("@validday", data.validday),                 
                new SqlParameter("@status", data.status),                 
                new SqlParameter("@costmethod", data.costmethod),                 
                new SqlParameter("@snmanage", data.snmanage),                 
                new SqlParameter("@snlength", data.snlength),                 
                new SqlParameter("@sort", data.sort),                 
                new SqlParameter("@comment", data.comment),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
            };
	        return this.ExecuteNonQuery(tansaction, false);
	    }

		/// <summary>
		/// 新增
		/// </summary>
		public override int Add(productDataModel data)
	    {
			return this.Add(data, null);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public int Update(productDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"UPDATE product SET  [classid] = @classid, [parentid] = @parentid, [childcount] = @childcount, [serialnumber] = @serialnumber, [name] = @name, [pinyin] = @pinyin, [alias] = @alias, [standard] = @standard, [modal] = @modal, [permitcode] = @permitcode, [brand] = @brand, [trademark] = @trademark, [makearea] = @makearea, [barcode] = @barcode, [price] = @price, [taxrate] = @taxrate, [unitid] = @unitid, [validmonth] = @validmonth, [validday] = @validday, [status] = @status, [costmethod] = @costmethod, [snmanage] = @snmanage, [snlength] = @snlength, [sort] = @sort, [comment] = @comment, [ModifyDate] = @ModifyDate WHERE  [proid] = @proid ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@proid", data.proid),                 
                new SqlParameter("@classid", data.classid),                 
                new SqlParameter("@parentid", data.parentid),                 
                new SqlParameter("@childcount", data.childcount),                 
                new SqlParameter("@serialnumber", data.serialnumber),                 
                new SqlParameter("@name", data.name),                 
                new SqlParameter("@pinyin", data.pinyin),                 
                new SqlParameter("@alias", data.alias),                 
                new SqlParameter("@standard", data.standard),                 
                new SqlParameter("@modal", data.modal),                 
                new SqlParameter("@permitcode", data.permitcode),                 
                new SqlParameter("@brand", data.brand),                 
                new SqlParameter("@trademark", data.trademark),                 
                new SqlParameter("@makearea", data.makearea),                 
                new SqlParameter("@barcode", data.barcode),                 
                new SqlParameter("@price", data.price),                 
                new SqlParameter("@taxrate", data.taxrate),                 
                new SqlParameter("@unitid", data.unitid),                 
                new SqlParameter("@validmonth", data.validmonth),                 
                new SqlParameter("@validday", data.validday),                 
                new SqlParameter("@status", data.status),                 
                new SqlParameter("@costmethod", data.costmethod),                 
                new SqlParameter("@snmanage", data.snmanage),                 
                new SqlParameter("@snlength", data.snlength),                 
                new SqlParameter("@sort", data.sort),                 
                new SqlParameter("@comment", data.comment),                 
                new SqlParameter("@ModifyDate", data.ModifyDate),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 修改
		/// </summary>
		public override int Update(productDataModel data)
	    {
			return this.Update(data, null);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public int Delete(productDataModel data, IDbTransaction tansaction)
	    {
			this.ExecuteSQL = @"DELETE FROM product WHERE  [proid] = @proid ";
			this.DataParameter = new DbParameter[]
            {
                new SqlParameter("@proid", data.proid),                 
            };
	        return this.ExecuteNonQuery(tansaction);
	    }

		/// <summary>
		/// 删除
		/// </summary>
	    public override int Delete(productDataModel data)
	    {
			return this.Delete(data, null);
	    }
	    
	}
}