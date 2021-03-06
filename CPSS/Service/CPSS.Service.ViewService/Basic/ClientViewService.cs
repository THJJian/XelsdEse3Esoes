﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Helper.Generated;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Client;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Client.Request;
using CPSS.Service.ViewService.ViewModels.Client.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class ClientViewService: BaseViewService, IClientViewService
    {
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.ClientViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IclientDataAccess mClientDataAccess;


        public ClientViewService(IMongoDbDataAccess mongoDbDataAccess, IDbConnection dbConnection, IclientDataAccess clientDataAccess) : base(mongoDbDataAccess)
        {
            this.mDbConnection = dbConnection;
            this.mClientDataAccess = clientDataAccess;
        }

        public RespondWebViewData<List<RespondQueryClientViewModel>> GetQueryClientList(RequestWebViewData<RequestQueryClientViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryClientViewModel();
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryClientViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryClientList"),

                #region =================
                CallBackFunc = () =>
                {
                    var parameter = new QueryClientListParameter
                    {
                        Alias = request.data.Alias,
                        Deleted = request.data.Deleted,
                        LinkMan = request.data.LinkMan,
                        LinkTel = request.data.LinkTel,
                        Name = request.data.Name,
                        PageSize = request.rows,
                        PageIndex = request.page,
                        ParentId = request.data.ParentId,
                        PriceMode = request.data.PriceMode,
                        SerialNumber = request.data.SerialNumber,
                        Spelling = request.data.Spelling,
                        Status = request.data.Status
                    };
                    var pageDataList = this.mClientDataAccess.GetQueryClientList(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryClientViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item => new RespondQueryClientViewModel
                        {
                            Address = item.address,
                            Alias = item.alias,
                            ChildNumber = item.childnumber,
                            ClassId = item.classid,
                            ClientId = item.clientid,
                            Comment = item.comment,
                            Credits = item.credits.ToCurrencyString(5),
                            Deleted = item.deleted,
                            LinkAddress = item.linkaddress,
                            LinkMan = item.linkman,
                            LinkTel = item.linktel,
                            Name = item.name,
                            ParentId = item.parentid,
                            Spelling = item.pinyin,
                            PriceMode = item.pricemode,
                            SerialNumber = item.serialnumber,
                            Status = item.status,
                            Sort = item.sort,
                            ZipCode = item.zipcode
                        }).ToList()
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.BasicClient,
                ParamsKeys = new object[]
                {
                    request.page,
                    request.rows,
                    request.data.Alias,
                    request.data.Deleted,
                    request.data.LinkMan,
                    request.data.LinkTel,
                    request.data.Name,
                    request.data.ParentId,
                    request.data.PriceMode,
                    request.data.SerialNumber,
                    request.data.Spelling,
                    request.data.Status
                }
            });
        }

        public RespondWebViewData<RespondAddClientViewModel> AddClient(RequestWebViewData<RequestAddClientViewModel> request)
        {
            var rData = request.data;
            if (this.mClientDataAccess.CheckClientIsExist(new QueryClientListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber }))
                return new RespondWebViewData<RespondAddClientViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的往来单位已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RespondAddClientViewModel>(WebViewErrorCode.Success);
            try
            {
                var client = this.mClientDataAccess.GetClientByClassID(new QueryClientListParameter { ParentId = rData.ParentId });
                if (client == null) return new RespondWebViewData<RespondAddClientViewModel>(WebViewErrorCode.NotExistsDataInfo);
                if (client.deleted == (short)CommonDeleted.Deleted) return new RespondWebViewData<RespondAddClientViewModel>(WebViewErrorCode.NotExistsDataInfo);

                this.mDbConnection.ExecuteTransaction(tran =>
                {
                    var parameter = new QueryClientListParameter
                    {
                        ParentId = rData.ParentId
                    };
                    var classId = string.Concat(rData.ParentId, "000001");
                    var clientList = this.mClientDataAccess.GetClientListByParentID(parameter);
                    if (clientList.Count > 0)
                        classId = BuildNewClassIdByLastClassId.GeneratedNewClassIdByLastClassId(clientList[0].classid);

                    var data = new clientDataModel
                    {
                        address = rData.Address,
                        alias = rData.Alias,
                        childnumber = 0,
                        childcount = 0,
                        classid = classId,
                        comment = rData.Comment,
                        credits = rData.Credits,
                        deleted = (short)CommonDeleted.NotDeleted,
                        linktel = rData.LinkTel,
                        linkman = rData.LinkMan,
                        linkaddress = rData.LinkAddress,
                        name = rData.Name,
                        pinyin = rData.Spelling,
                        parentid = rData.ParentId,
                        pricemode = rData.PriceMode,
                        status = (short)CommonStatus.Used,
                        serialnumber = rData.SerialNumber,
                        sort = rData.Sort,
                        zipcode = rData.ZipCode
                    };
                    var addResult = this.mClientDataAccess.Add(data, tran);
                    if (addResult > 0) this.mClientDataAccess.UpdateChildNumberByClassId(tran, parameter);
                    MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicClient);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("新增往来客户资料", request, respond, this.GetType());
                });
            }
            catch (Exception exception)
            {
                respond = new RespondWebViewData<RespondAddClientViewModel>(new ErrorCodeItem(WebViewErrorCode.Exception.ErrorCode, exception.Message));
            }
            return respond;
        }

        public RespondWebViewData<RespondQueryClientViewModel> GetClientByClientId(RequestWebViewData<RequestGetClientByIdViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQueryClientViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetClientByClientId"),

                #region ====================
                CallBackFunc = () =>
                {
                    var respond = new RespondWebViewData<RespondQueryClientViewModel>
                    {
                        rows = new RespondQueryClientViewModel()
                    };
                    var client = this.mClientDataAccess.GetclientDataModelById(request.data.ClientId);
                    if (client == null) return respond;
                    if (client.deleted == (short)CommonDeleted.Deleted || client.status != (short)CommonStatus.Used) return respond;
                    
                    respond = new RespondWebViewData<RespondQueryClientViewModel>
                    {
                        rows = new RespondQueryClientViewModel
                        {
                            Address = client.address,
                            Alias = client.alias,
                            ChildNumber = client.childnumber,
                            ClassId = client.classid,
                            ClientId = client.clientid,
                            Comment = client.comment,
                            Credits = client.credits.ToNumberString(5),
                            LinkAddress = client.linkaddress,
                            LinkMan = client.linkman,
                            LinkTel = client.linktel,
                            Name = client.name,
                            ParentId = client.parentid,
                            Spelling = client.pinyin,
                            PriceMode = client.pricemode,
                            SerialNumber = client.serialnumber,
                            Sort = client.sort,
                            ZipCode = client.zipcode
                        }
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.BasicClient,
                ParamsKeys = new object[]
                {
                    request.data.ClientId
                }
            });
        }

        public RespondWebViewData<RequestEditClientViewModel> EditClient(RequestWebViewData<RequestEditClientViewModel> request)
        {
            var rData = request.data;
            if (this.mClientDataAccess.CheckClientIsExist(new QueryClientListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber, ClientId = rData.ClientId }))
                return new RespondWebViewData<RequestEditClientViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的往来单位已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RequestEditClientViewModel>(WebViewErrorCode.Success);
            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var client = this.mClientDataAccess.GetclientDataModelById(rData.ClientId);
                if (client == null)
                {
                    respond = new RespondWebViewData<RequestEditClientViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }
                if (client.deleted == (short)CommonDeleted.Deleted)
                {
                    respond = new RespondWebViewData<RequestEditClientViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }

                var data = new clientDataModel
                {
                    address = rData.Address,
                    alias = rData.Alias,
                    clientid = rData.ClientId,
                    comment = rData.Comment,
                    credits = rData.Credits,
                    linkaddress = rData.LinkAddress,
                    linktel = rData.LinkTel,
                    linkman = rData.LinkMan,
                    name = rData.Name,
                    pinyin = rData.Spelling,
                    pricemode = rData.PriceMode,
                    serialnumber = rData.SerialNumber,
                    sort = rData.Sort,
                    zipcode = rData.ZipCode,
                    status = client.status,
                    deleted = client.deleted,
                    parentid = client.parentid,
                    childnumber = client.childnumber,
                    childcount = client.childcount,
                    classid = client.classid
                };
                this.mClientDataAccess.Update(data, tran);
                MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicClient);

                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.SaveMongoDbData("编辑往来客户资料", request, respond, this.GetType());
            });
            return respond;
        }

        public RespondWebViewData<RespondDeleteClientViewModel> DeleteClient(RequestWebViewData<RequestDeleteClientViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteClientViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteClientParameter
            {
                ClientId = request.data.ClientId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mClientDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteClientViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicClient);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除往来客户资料", request, respond, this.GetType());
            return respond;
        }

        public RespondWebViewData<RespondDeleteClientViewModel> ReDeleteClient(RequestWebViewData<RequestDeleteClientViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteClientViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteClientParameter
            {
                ClientId = request.data.ClientId,
                Deleted = (short)CommonDeleted.NotDeleted
            };
            var dataResult = this.mClientDataAccess.ReDelete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteClientViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicClient);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除往来客户资料", request, respond, this.GetType());
            return respond;
        }
    }
}