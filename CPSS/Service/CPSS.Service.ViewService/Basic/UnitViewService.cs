using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Unit;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Unit.Request;
using CPSS.Service.ViewService.ViewModels.Unit.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class UnitViewService : BaseViewService, IUnitViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.UnitViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.UnitViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IunitDataAccess mUnitDataAccess;

        public UnitViewService(IDbConnection dbConnection, IunitDataAccess unitDataAccess, IMongoDbDataAccess mongoDbDataAccess) : base(mongoDbDataAccess)
        {
            mDbConnection = dbConnection;
            this.mUnitDataAccess = unitDataAccess;
        }


        public RespondWebViewData<List<RespondQueryUnitViewModel>> GetQueryUnitList(RequestWebViewData<RequestQueryUnitViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryUnitViewModel();
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryUnitViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryUnitList"),

                #region =================
                CallBackFunc = () =>
                {
                    var parameter = new QueryUnitListParameter
                    {
                        Deleted = request.data.Deleted,
                        Name = request.data.Name,
                        PageIndex = request.page,
                        PageSize = request.rows,
                        Status = request.data.Status
                    };
                    var pageDataList = this.mUnitDataAccess.GetQueryUnitList(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryUnitViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item => new RespondQueryUnitViewModel
                        {
                            UnitId = item.unitid,
                            Name = item.name,
                            Deleted = item.deleted.HasValue ? item.deleted.Value : (short)CommonDeleted.NotDeleted,
                            Status = item.status,
                            Sort = item.sort.ToString()
                        }).ToList()
                    };
                    return respond;
                },

                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.Name,
                    request.data.Status,
                    request.data.Deleted,
                    request.page,
                    request.rows
                }
            });
        }

        public RespondWebViewData<RespondAddUnitViewModel> AddUnit(RequestWebViewData<RequestAddUnitViewModel> request)
        {
            var rData = request.data;
            if(this.mUnitDataAccess.CheckUnitIsExist(new QueryUnitListParameter{Name = rData.Name}))
                return new RespondWebViewData<RespondAddUnitViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]的计量单位已经存在", rData.Name));

            var respond = new RespondWebViewData<RespondAddUnitViewModel>(WebViewErrorCode.Success);
            try
            {

                this.mDbConnection.ExecuteTransaction(tran =>
                {
                    var data = new unitDataModel
                    {
                        name = rData.Name,
                        status = (short)CommonStatus.Used,
                        deleted = (short)CommonDeleted.NotDeleted,
                        sort = rData.Sort
                    };
                    this.mUnitDataAccess.Add(data, tran);
                    MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("新增部门资料", request, respond, this.GetType());
                });
            }
            catch (Exception ex)
            {
                respond = new RespondWebViewData<RespondAddUnitViewModel>(new ErrorCodeItem(WebViewErrorCode.Exception.ErrorCode, ex.Message));
            }
            return respond;
        }

        public RespondWebViewData<RespondQueryUnitViewModel> GetUnitByUnitId(RequestWebViewData<RequestGetUnitByIdViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQueryUnitViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetUnitByUnitId"),

                #region ====================
                CallBackFunc = () =>
                {
                    var respond = new RespondWebViewData<RespondQueryUnitViewModel>
                    {
                        rows = new RespondQueryUnitViewModel()
                    };
                    var unit = this.mUnitDataAccess.GetunitDataModelById(request.data.UnitId);
                    if (unit == null) return respond;
                    if (unit.deleted == (short)CommonDeleted.Deleted || unit.status != (short)CommonStatus.Used) return respond;
                    respond = new RespondWebViewData<RespondQueryUnitViewModel>
                    {
                        rows = new RespondQueryUnitViewModel
                        {
                            UnitId = unit.unitid,
                            Deleted = unit.deleted.HasValue ? unit.deleted.Value : (short)CommonDeleted.NotDeleted,
                            Name = unit.name,
                            Status = unit.status,
                            Sort = unit.sort.ToString()
                        }
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.UnitId
                }
            });
        }

        public RespondWebViewData<RespondEditUnitViewModel> EditUnit(RequestWebViewData<RequestEditUnitViewModel> request)
        {
            var rData = request.data;
            if (this.mUnitDataAccess.CheckUnitIsExist(new QueryUnitListParameter { Name = rData.Name }))
                return new RespondWebViewData<RespondEditUnitViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]的计量单位已经存在", rData.Name));

            var respond = new RespondWebViewData<RespondEditUnitViewModel>(WebViewErrorCode.Success);
            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var unit = this.mUnitDataAccess.GetunitDataModelById(rData.UnitId);
                if (unit == null)
                {
                    respond = new RespondWebViewData<RespondEditUnitViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }
                if (unit.deleted == (short)CommonDeleted.Deleted)
                {
                    respond = new RespondWebViewData<RespondEditUnitViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }

                var data = new unitDataModel()
                {
                    unitid = rData.UnitId,
                    name = rData.Name,
                    status = unit.status,
                    deleted = unit.deleted,
                    sort = rData.Sort
                };
                this.mUnitDataAccess.Update(data, tran);
                MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.SaveMongoDbData("编辑分公司资料", request, respond, this.GetType());
            });
            return respond;
        }

        public RespondWebViewData<RespondDeleteUnitViewModel> DeleteUnit(RequestWebViewData<RequestDeleteUnitViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteUnitViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteUnitParameter
            {
                UnitId = request.data.UnitId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mUnitDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteUnitViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除部门资料", request, respond, this.GetType());
            return respond;
        }

        public RespondWebViewData<RespondDeleteUnitViewModel> ReDeleteUnit(RequestWebViewData<RequestDeleteUnitViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteUnitViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteUnitParameter
            {
                UnitId = request.data.UnitId,
                Deleted = (short)CommonDeleted.NotDeleted
            };
            var dataResult = this.mUnitDataAccess.ReDelete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteUnitViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("恢复删除部门资料", request, respond, this.GetType());
            return respond;
        }
    }
}