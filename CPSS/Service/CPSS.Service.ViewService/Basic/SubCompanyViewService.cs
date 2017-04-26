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
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.SubCompany;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.SubCompany.Request;
using CPSS.Service.ViewService.ViewModels.SubCompany.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class SubCompanyViewService : BaseViewService, ISubCompanyViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.SubCompanyViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.SubCompanyViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IsubcompanyDataAccess mSubCompanyDataAccess;

        public SubCompanyViewService(IDbConnection dbConnection, IsubcompanyDataAccess subCompanyDataAccess, IMongoDbDataAccess mongoDbDataAccess) : base(mongoDbDataAccess)
        {
            this.mDbConnection = dbConnection;
            this.mSubCompanyDataAccess = subCompanyDataAccess;
        }

        public RespondWebViewData<List<RespondQuerySubCompanyViewModel>> GetQueryCompanyList(RequestWebViewData<RequestQuerySubCompanyViewModel> request)
        {
            if(request.data == null) request.data = new RequestQuerySubCompanyViewModel();

            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQuerySubCompanyViewModel>>>
                    {
                        CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryCompanyList"),

                        #region ==========================
                
                        CallBackFunc = () =>
                        {
                            var parameter = new QuerySubCompanyListParameter
                            {
                                Email = request.data.Email,
                                LinkMan = request.data.LinkMan,
                                LinkTel = request.data.LinkTel,
                                Name = request.data.Name,
                                PageIndex = request.page,
                                PageSize = request.rows,
                                ParentId = request.data.ParentId,
                                PriceMode = request.data.PriceMode,
                                SerialNumber = request.data.SerialNumber,
                                Spelling = request.data.Spelling,
                                Status = request.data.Status,
                                Deleted = request.data.Deleted
                            };
                            var pageDataList = this.mSubCompanyDataAccess.GetQuerySubCompanyList(parameter);
                            var respond = new RespondWebViewData<List<RespondQuerySubCompanyViewModel>>
                            {
                                total = pageDataList.DataCount,
                                rows = pageDataList.Datas.Select(item => new RespondQuerySubCompanyViewModel
                                {
                                    ClassId = item.classid,
                                    ComId = item.subcomid,
                                    Email = item.email,
                                    Comment = item.comment,
                                    LinkMan = item.linkman,
                                    LinkTel = item.linktel,
                                    Name = item.name,
                                    ParentId = item.parentid,
                                    PriceMode = item.pricemode.ToString(),
                                    SerialNumber = item.serialnumber,
                                    sort = item.sort.ToString(),
                                    Status = item.status.ToString(),
                                    Spelling = item.pinyin,
                                    ChildNumber = item.childnumber,
                                    Deleted = item.deleted.HasValue ? item.deleted.Value : (short)CommonDeleted.NotDeleted
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
                            request.data.ParentId,
                            request.data.Email,
                            request.data.LinkMan,
                            request.data.LinkTel,
                            request.data.Name,
                            request.data.PriceMode,
                            request.data.SerialNumber,
                            request.data.Spelling,
                            request.data.Status,
                            request.data.Deleted
                        }
                    });
        }

        public RespondWebViewData<RespondAddSubCompanyViewModel> AddSubCompany(RequestWebViewData<RequestAddSubCompanyViewModel> request)
        {
            var respond = new RespondWebViewData<RespondAddSubCompanyViewModel>(WebViewErrorCode.Success);
            var rData = request.data;
            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var parameter = new QuerySubCompanyListParameter
                {
                    ParentId = rData.ParentId
                };
                var classId = string.Concat(rData.ParentId, "000001");
                var companyList = this.mSubCompanyDataAccess.GetSubCompanyListByParentID(parameter);
                if (companyList.Count > 0)
                    classId = BuildNewClassIdByLastClassId.GeneratedNewClassIdByLastClassId(companyList[0].classid);

                var data = new subcompanyDataModel
                {
                    childnumber = 0,
                    classid = classId,
                    comment = rData.Comment,
                    email = rData.Email,
                    linktel = rData.LinkTel,
                    linkman = rData.LinkMan,
                    name = rData.Name,
                    parentid = rData.ParentId,
                    pinyin = rData.Spelling,
                    pricemode = rData.PriceMode,
                    serialnumber = rData.SerialNumber,
                    sort = rData.Sort,
                    status = 1,
                    deleted = 1
                };
                var addResult = this.mSubCompanyDataAccess.Add(data, tran);
                if (addResult > 0) this.mSubCompanyDataAccess.UpdateChildNumberByClassId(tran, parameter);
                MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);
                
                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.SaveMongoDbData("新增分公司资料", request, respond, this.GetType());
            });
            return respond;
        }

        public RespondWebViewData<RespondQuerySubCompanyViewModel> GetSubCompanyByComId(RequestWebViewData<RequestGetSubCompanyByIdViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQuerySubCompanyViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetSubCompanyByComId"),

                #region =================

                CallBackFunc = () =>
                {
                    var respond = new RespondWebViewData<RespondQuerySubCompanyViewModel>(WebViewErrorCode.NotExistsDataInfo)
                    {
                        rows = new RespondQuerySubCompanyViewModel()
                    };
                    var subCompany = this.mSubCompanyDataAccess.GetsubcompanyDataModelById(request.data.ComId);
                    if (subCompany == null) return respond;
                    if (subCompany.deleted == (short)CommonDeleted.Deleted || subCompany.status != (short)CommonStatus.Used) return respond;
                    respond = new RespondWebViewData<RespondQuerySubCompanyViewModel>
                    {
                        rows = new RespondQuerySubCompanyViewModel
                        {
                            ComId = subCompany.subcomid,
                            Comment = subCompany.comment,
                            Email = subCompany.email,
                            LinkMan = subCompany.linkman,
                            LinkTel = subCompany.linktel,
                            Name = subCompany.name,
                            PriceMode = subCompany.pricemode.ToString(),
                            SerialNumber = subCompany.serialnumber,
                            sort = subCompany.sort.ToString(),
                            Spelling = subCompany.pinyin
                        }
                    };
                    return respond;
                },

                #endregion
            
                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.ComId
                }
            });
        }

        public RespondWebViewData<RespondEditSubCompanyViewModel> EditSubCompany(RequestWebViewData<RequestEditSubCompanyViewModel> request)
        {
            var respond = new RespondWebViewData<RespondEditSubCompanyViewModel>(WebViewErrorCode.Success);
            var rData = request.data;

            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var company = this.mSubCompanyDataAccess.GetsubcompanyDataModelById(rData.ComId);
                if (company == null)
                {
                    respond = new RespondWebViewData<RespondEditSubCompanyViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }
                if (company.deleted == 1)
                {
                    respond = new RespondWebViewData<RespondEditSubCompanyViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }

                var data = new subcompanyDataModel
                {
                    subcomid = rData.ComId,
                    comment = rData.Comment,
                    email = rData.Email,
                    linktel = rData.LinkTel,
                    linkman = rData.LinkMan,
                    name = rData.Name,
                    pinyin = rData.Spelling,
                    pricemode = rData.PriceMode,
                    serialnumber = rData.SerialNumber,
                    sort = rData.Sort,
                    status = company.status,
                    deleted = company.deleted,
                    childnumber = company.childnumber,
                    classid = company.classid,
                    parentid = company.parentid
                };
                this.mSubCompanyDataAccess.Update(data, tran);
                MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.SaveMongoDbData("编辑分公司资料", request, respond, this.GetType());
            });
            return respond;
        }

        public RespondWebViewData<RespondDeleteSubCompanyViewModel> DeleteSubCompany(RequestWebViewData<RequestDeleteSubCompanyViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteSubCompanyViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteSubCompanyParameter
            {
                ComId = request.data.ComId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mSubCompanyDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteSubCompanyViewModel>(WebViewErrorCode.Success); 
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);
           
            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除分公司资料", request, respond, this.GetType());
            return respond;
        }

        public RespondWebViewData<RespondDeleteSubCompanyViewModel> ReDeleteSubCompany(RequestWebViewData<RequestDeleteSubCompanyViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteSubCompanyViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteSubCompanyParameter
            {
                ComId = request.data.ComId,
                Deleted = (short)CommonDeleted.NotDeleted
            };
            var dataResult = this.mSubCompanyDataAccess.ReDelete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteSubCompanyViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);
            
            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("恢复删除分公司资料", request, respond, this.GetType());
            return respond;
        }
    }
}