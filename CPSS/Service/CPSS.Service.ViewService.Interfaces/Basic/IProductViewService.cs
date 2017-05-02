using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.Employee.Respond;
using CPSS.Service.ViewService.ViewModels.Product.Request;
using CPSS.Service.ViewService.ViewModels.Product.Respond;

namespace CPSS.Service.ViewService.Interfaces.Basic
{
    public interface IProductViewService
    {
        /// <summary>
        /// 获取商品信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQueryProductViewModel>> GetQueryProductList(RequestWebViewData<RequestQueryProductViewModel> request);

        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondAddProductViewModel> AddProduct(RequestWebViewData<RequestAddProductViewModel> request);

        /// <summary>
        /// 根据depid查询对应行商品信息
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<RespondQueryProductViewModel> GetProductByProId(RequestWebViewData<RequestGetProductByIdViewModel> request);

        /// <summary>
        /// 修改商品资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondEditProductViewModel> EditProduct(RequestWebViewData<RequestEditProductViewModel> request);

        /// <summary>
        /// 删除商品资料(逻辑删除)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteProductViewModel> DeleteProduct(RequestWebViewData<RequestDeleteProductViewModel> request);

        /// <summary>
        /// 恢复删除商品资料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondDeleteProductViewModel> ReDeleteProduct(RequestWebViewData<RequestDeleteProductViewModel> request);
    }
}