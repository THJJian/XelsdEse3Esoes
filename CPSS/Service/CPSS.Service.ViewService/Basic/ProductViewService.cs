using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Generated;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Product;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Employee.Respond;
using CPSS.Service.ViewService.ViewModels.Product.Request;
using CPSS.Service.ViewService.ViewModels.Product.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class ProductViewService : BaseViewService, IProductViewService
    {
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.ProductViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IproductDataAccess mProductDataAccess;


        public ProductViewService(IMongoDbDataAccess mongoDbDataAccess, IDbConnection dbConnection, IproductDataAccess productDataAccess) : base(mongoDbDataAccess)
        {
            this.mDbConnection = dbConnection;
            this.mProductDataAccess = productDataAccess;
        }

        public RespondWebViewData<List<RespondQueryProductViewModel>> GetQueryProductList(RequestWebViewData<RequestQueryProductViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryProductViewModel();
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryProductViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryProductList"),

                #region =================
                CallBackFunc = () =>
                {
                    var parameter = new QueryProductListParameter
                    {
                        PageSize = request.rows,
                        PageIndex = request.page,
                        Deleted = request.data.Deleted,
                        Name = request.data.Name,
                        ParentId = request.data.ParentId,
                        SerialNumber = request.data.SerialNumber,
                        Spelling = request.data.Spelling,
                        Status = request.data.Status
                    };
                    var pageDataList = this.mProductDataAccess.GetQueryProductList(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryProductViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item => new RespondQueryProductViewModel
                        {
                            Alias = item.alias,
                            BarCode = item.barcode,
                            Brand = item.brand,
                            ClassId = item.classid,
                            Comment = item.comment,
                            ChildCount = item.childcount,
                            CostMethod = item.costmethod,
                            Deleted = item.deleted,
                            MakeArea = item.makearea,
                            Modal = item.modal,
                            ParentId = item.parentid,
                            PermitCode = item.permitcode,
                            Spelling = item.pinyin,
                            ProId = item.proid,
                            Status = item.status,
                            SerialNumber = item.serialnumber,
                            SNLength = item.snlength,
                            SNManage = item.snmanage,
                            Sort = item.sort,
                            Standard = item.standard,
                            TaxRate = item.taxrate,
                            TradeMark = item.trademark,
                            UnitId = item.unitid,
                            ValidDay = item.validday,
                            ValidMonth = item.validmonth
                        }).ToList()
                    };
                    return respond;
                },

                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceClassNameConst.BasicProduct,
                ParamsKeys = new object[]
                    {
                        request.data.ParentId,
                        request.page,
                        request.rows
                    }
            });
        }

        public RespondWebViewData<RespondAddProductViewModel> AddProduct(RequestWebViewData<RequestAddProductViewModel> request)
        {
            var rData = request.data;
            if (this.mProductDataAccess.CheckProductIsExist(new QueryProductListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber }))
                return new RespondWebViewData<RespondAddProductViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的商品已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RespondAddProductViewModel>(WebViewErrorCode.Success);
            try
            {
                var product = this.mProductDataAccess.GetProductByClassID(new QueryProductListParameter {ParentId = rData.ParentId});
                if (product == null) return new RespondWebViewData<RespondAddProductViewModel>(WebViewErrorCode.NotExistsDataInfo);
                if (product.deleted == (short) CommonDeleted.Deleted) return new RespondWebViewData<RespondAddProductViewModel>(WebViewErrorCode.NotExistsDataInfo);
                this.mDbConnection.ExecuteTransaction(tran =>
                {
                    var parameter = new QueryProductListParameter()
                    {
                        ParentId = rData.ParentId
                    };
                    var classId = string.Concat(rData.ParentId, "000001");
                    var depList = this.mProductDataAccess.GetProductListByParentID(parameter);
                    if (depList.Count > 0)
                        classId = BuildNewClassIdByLastClassId.GeneratedNewClassIdByLastClassId(depList[0].classid);

                    var data = new productDataModel
                    {
                        classid = classId,
                        deleted = (short)CommonDeleted.NotDeleted,
                        name = rData.Name,
                        parentid = rData.ParentId,
                        pinyin = rData.Spelling,
                        serialnumber = rData.SerialNumber,
                        sort = rData.Sort,
                        status = (short)CommonStatus.Used
                    };
                    var addResult = this.mProductDataAccess.Add(data, tran);
                    if (addResult > 0) this.mProductDataAccess.UpdateChildNumberByClassId(tran, parameter);
                    MemcacheHelper.RemoveBy(ServiceClassNameConst.BasicProduct);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("新增商品资料", request, respond, this.GetType());
                });
            }
            catch (Exception ex)
            {
                respond = new RespondWebViewData<RespondAddProductViewModel>(new ErrorCodeItem(WebViewErrorCode.Exception.ErrorCode, ex.Message));
            }
            return respond;
        }

        public RespondWebViewData<RespondQueryProductViewModel> GetProductByProId(RequestWebViewData<RequestGetProductByIdViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQueryProductViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetProductByProId"),

                #region ====================
                CallBackFunc = () =>
                {
                    var respond = new RespondWebViewData<RespondQueryProductViewModel>
                    {
                        rows = new RespondQueryProductViewModel()
                    };
                    var product = this.mProductDataAccess.GetproductDataModelById(request.data.ProId);
                    if (product == null) return respond;
                    if (product.deleted == (short)CommonDeleted.Deleted || product.status != (short)CommonStatus.Used) return respond;
                    
                    respond = new RespondWebViewData<RespondQueryProductViewModel>
                    {
                        rows = new RespondQueryProductViewModel
                        {
                            ClassId = product.classid,
                            Comment = product.comment,
                            Deleted = product.deleted,
                            Name = product.name,
                            ParentId = product.parentid,
                            SerialNumber = product.serialnumber,
                            Status = product.status,
                            Spelling = product.pinyin,
                            Sort = product.sort
                        }
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceClassNameConst.BasicProduct,
                ParamsKeys = new object[]
                {
                    request.data.ProId
                }
            });
        }

        public RespondWebViewData<RespondEditProductViewModel> EditProduct(RequestWebViewData<RequestEditProductViewModel> request)
        {
            var rData = request.data;
            var product = this.mProductDataAccess.GetproductDataModelById(rData.ProId);
            if (product == null) return new RespondWebViewData<RespondEditProductViewModel>(WebViewErrorCode.NotExistsDataInfo);
            if (product.deleted == (short)CommonDeleted.Deleted) return new RespondWebViewData<RespondEditProductViewModel>(WebViewErrorCode.NotExistsDataInfo);
            if (this.mProductDataAccess.CheckProductIsExist(new QueryProductListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber }))
                return new RespondWebViewData<RespondEditProductViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的商品已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RespondEditProductViewModel>(WebViewErrorCode.Success);
            try
            {
                this.mDbConnection.ExecuteTransaction(tran =>
                {
                    var data = new productDataModel
                    {
                        classid = product.classid,
                        comment = rData.Comment,
                        deleted = product.deleted,
                        proid = rData.ProId,
                        name = rData.Name,
                        pinyin = rData.Spelling,
                        parentid = product.parentid,
                        serialnumber = rData.SerialNumber,
                        sort = rData.Sort,
                        status = product.status
                    };
                    this.mProductDataAccess.Update(data, tran);
                    MemcacheHelper.RemoveBy(ServiceClassNameConst.BasicProduct);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("编辑商品资料", request, respond, this.GetType());
                });
            }
            catch (Exception ex)
            {
                respond = new RespondWebViewData<RespondEditProductViewModel>(WebViewErrorCode.Exception.ErrorCode, ex.Message);
            }
            return respond;
        }

        public RespondWebViewData<RespondDeleteProductViewModel> DeleteProduct(RequestWebViewData<RequestDeleteProductViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteProductViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteProductParameter
            {
                Proid = request.data.ProId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mProductDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteProductViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(ServiceClassNameConst.BasicProduct);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除删除资料", request, respond, this.GetType());
            return respond;
        }

        public RespondWebViewData<RespondDeleteProductViewModel> ReDeleteProduct(RequestWebViewData<RequestDeleteProductViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteProductViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteProductParameter
            {
                Proid = request.data.ProId,
                Deleted = (short)CommonDeleted.NotDeleted
            };
            var dataResult = this.mProductDataAccess.ReDelete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteProductViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(ServiceClassNameConst.BasicProduct);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除商品资料", request, respond, this.GetType());
            return respond;
        }
    }
}