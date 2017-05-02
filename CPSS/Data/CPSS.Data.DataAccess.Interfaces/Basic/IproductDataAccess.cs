using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Product;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IproductDataAccess
    {
        /// <summary>
        /// 获取商品资料列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<productDataModel> GetQueryProductList(QueryProductListParameter parameter);

        /// <summary>
        /// 检查商品是否已经存在
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool CheckProductIsExist(QueryProductListParameter parameter);

        /// <summary>
        /// 根据parentid查询商品信息列表(ORDER BY classid DESC)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<productDataModel> GetProductListByParentID(QueryProductListParameter parameter);

        /// <summary>
        /// 根据classid获取对应行数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        productDataModel GetProductByClassID(QueryProductListParameter parameter);

        /// <summary>
        /// 根据classid更新childnumber字段的值
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int UpdateChildNumberByClassId(IDbTransaction tran, QueryProductListParameter parameter);

        /// <summary>
        /// 删除商品资料(逻辑删除)
        /// </summary>
        /// <returns></returns>
        int Delete(DeleteProductParameter parameter);

        /// <summary>
        /// 恢复删除商品资料
        /// </summary>
        /// <returns></returns>
        int ReDelete(DeleteProductParameter parameter);
    }
}