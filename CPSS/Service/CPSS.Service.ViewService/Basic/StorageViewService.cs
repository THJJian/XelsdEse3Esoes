using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Generated;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Storage;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Storage.Request;
using CPSS.Service.ViewService.ViewModels.Storage.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class StorageViewService : BaseViewService, IStorageViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.ClientViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.ClientViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IstorageDataAccess mStorageDataAccess;

        public StorageViewService(IMongoDbDataAccess mongoDbDataAccess, IDbConnection dbConnection,
            IstorageDataAccess storageDataAccess) : base(mongoDbDataAccess)
        {
            this.mDbConnection = dbConnection;
            this.mStorageDataAccess = storageDataAccess;
        }

        public RespondWebViewData<List<RespondQueryStorageViewModel>> GetQueryStorageList(RequestWebViewData<RequestQueryStorageViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryStorageViewModel();
            return 
                MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryStorageViewModel>>>
                {
                    CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryStorageList"),

                    #region =================
                    CallBackFunc = () =>
                    {
                        var parameter = new QueryStorageListParameter
                        {
                            Alias = request.data.Alias,
                            Deleted = request.data.Deleted,
                            Name = request.data.Name,
                            PageSize = request.rows,
                            PageIndex = request.page,
                            ParentId = request.data.ParentId,
                            SerialNumber = request.data.SerialNumber,
                            Spelling = request.data.Spelling,
                            Status = request.data.Status
                        };
                        var pageDataList = this.mStorageDataAccess.GetQueryStorageList(parameter);
                        var respond = new RespondWebViewData<List<RespondQueryStorageViewModel>>
                        {
                            total = pageDataList.DataCount,
                            rows = pageDataList.Datas.Select(item => new RespondQueryStorageViewModel
                            {
                                Alias = item.alias,
                                ParentId = item.parentid,
                                Spelling = item.PinYin,
                                ChildNumber = item.childnumber,
                                ClassId= item.classid,
                                Comment = item.comment,
                                Deleted = item.deleted,
                                Name = item.name,
                                SerialNumber = item.serialnumber,
                                Sort = item.sort,
                                StorageId = item.stoid,
                                Status = item.status
                            }).ToList()
                        };
                        return respond;
                    },

                    #endregion

                    ExpiresAt = DateTime.Now.AddMinutes(30),
                    ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                    ParamsKeys = new object[]
                    {
                        request.page,
                        request.rows,
                        request.data.Alias,
                        request.data.Deleted,
                        request.data.Name,
                        request.data.ParentId,
                        request.data.SerialNumber,
                        request.data.Spelling,
                        request.data.Status
                    }
                });
        }

        public RespondWebViewData<RespondAddStorageViewModel> AddStorage(RequestWebViewData<RequestAddStorageViewModel> request)
        {
            var respond = new RespondWebViewData<RespondAddStorageViewModel>(WebViewErrorCode.Success);
            var rData = request.data;
            try
            {
                this.mDbConnection.ExecuteTransaction(tran =>
                {
                    var parameter = new QueryStorageListParameter
                    {
                        ParentId = rData.ParentId
                    };
                    var classId = string.Concat(rData.ParentId, "000001");
                    var clientList = this.mStorageDataAccess.GetStorageListByParentID(parameter);
                    if (clientList.Count > 0)
                        classId = BuildNewClassIdByLastClassId.GeneratedNewClassIdByLastClassId(clientList[0].classid);

                    var data = new storageDataModel
                    {
                        classid = classId,
                        alias = rData.Alias,
                        childnumber = 0,
                        childcount = 0,
                        comment = rData.Comment,
                        deleted = (short)CommonDeleted.NotDeleted,
                        name = rData.Name,
                        parentid = rData.ParentId,
                        PinYin = rData.Spelling,
                        serialnumber = rData.SerialNumber,
                        status = (short)CommonStatus.Used,
                        sort = rData.Sort
                    };
                    var addResult = this.mStorageDataAccess.Add(data, tran);
                    if (addResult > 0) this.mStorageDataAccess.UpdateChildNumberByClassId(tran, parameter);
                    MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("新增往来客户资料", request, respond, this.GetType());
                });
            }
            catch (Exception exception)
            {
                respond = new RespondWebViewData<RespondAddStorageViewModel>(new ErrorCodeItem(WebViewErrorCode.Exception.ErrorCode, exception.Message));
            }
            return respond;
        }

        public RespondWebViewData<RespondQueryStorageViewModel> GetStorageByStorageId(RequestWebViewData<RequestGetStorageByIdViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQueryStorageViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetStorageByStorageId"),

                #region ====================
                CallBackFunc = () =>
                {
                    var respond = new RespondWebViewData<RespondQueryStorageViewModel>
                    {
                        rows = new RespondQueryStorageViewModel()
                    };
                    var storage = this.mStorageDataAccess.GetstorageDataModelById(request.data.StorageId);
                    if (storage == null) return respond;
                    if (storage.deleted == (short) CommonDeleted.Deleted || storage.status != (short) CommonStatus.Used)
                        return respond;

                    respond = new RespondWebViewData<RespondQueryStorageViewModel>
                    {
                        rows = new RespondQueryStorageViewModel
                        {
                            Alias = storage.alias,
                            ParentId = storage.parentid,
                            Spelling = storage.PinYin,
                            ChildNumber = storage.childnumber,
                            ClassId = storage.classid,
                            Comment = storage.comment,
                            Deleted = storage.deleted,
                            Name = storage.name,
                            SerialNumber = storage.serialnumber,
                            Sort = storage.sort,
                            StorageId = storage.stoid,
                            Status = storage.status
                        }
                    };
                    return respond;
                },

                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.StorageId
                }
            });
        }

        public RespondWebViewData<RespondEditStorageViewModel> EditStorage(RequestWebViewData<RequestEditStorageViewModel> request)
        {
            var respond = new RespondWebViewData<RespondEditStorageViewModel>(WebViewErrorCode.Success);
            try
            {
                var rData = request.data;
                this.mDbConnection.ExecuteTransaction(tran =>
                {
                    var storage = this.mStorageDataAccess.GetstorageDataModelById(rData.StorageId);
                    if (storage == null)
                    {
                        respond = new RespondWebViewData<RespondEditStorageViewModel>(WebViewErrorCode.NotExistsDataInfo);
                        return;
                    }
                    if (storage.deleted == (short)CommonDeleted.Deleted)
                    {
                        respond = new RespondWebViewData<RespondEditStorageViewModel>(WebViewErrorCode.NotExistsDataInfo);
                        return;
                    }

                    var data = new storageDataModel
                    {
                        alias = rData.Alias,
                        comment = rData.Comment,
                        name = rData.Name,
                        PinYin = rData.Spelling,
                        serialnumber = rData.SerialNumber,
                        sort = rData.Sort,
                        stoid = rData.StorageId,
                        childcount = storage.childcount,
                        classid = storage.classid,
                        childnumber = storage.childnumber,
                        deleted = storage.deleted,
                        parentid = storage.parentid,
                        status = storage.status
                    };
                    this.mStorageDataAccess.Update(data, tran);
                    MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("编辑往来客户资料", request, respond, this.GetType());
                });

            }
            catch (Exception exception)
            {
                respond = new RespondWebViewData<RespondEditStorageViewModel>(new ErrorCodeItem(WebViewErrorCode.Exception.ErrorCode, exception.Message));
            }
            return respond;
        }

        public RespondWebViewData<RespondDeleteStorageViewModel> DeleteStorage(RequestWebViewData<RequestDeleteStorageViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteStorageViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteStorageParameter
            {
                StorageId = request.data.StorageId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mStorageDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteStorageViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除往来客户资料", request, respond, this.GetType());
            return respond;
        }

        public RespondWebViewData<RespondDeleteStorageViewModel> ReDeleteStorage(RequestWebViewData<RequestDeleteStorageViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteStorageViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteStorageParameter
            {
                StorageId = request.data.StorageId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mStorageDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteStorageViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除往来客户资料", request, respond, this.GetType());
            return respond;
        }
    }
}