﻿using System;
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
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Department;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Department.Request;
using CPSS.Service.ViewService.ViewModels.Department.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class DepartmentViewService : BaseViewService, IDepartmentViewService
    {
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.DepartmentViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IdepartmentDataAccess mDepartmentDataAccess;

        public DepartmentViewService(IDbConnection dbConnection, IdepartmentDataAccess departmentDataAccess, IMongoDbDataAccess mongoDbDataAccess) : base(mongoDbDataAccess)
        {
            mDbConnection = dbConnection;
            this.mDepartmentDataAccess = departmentDataAccess;
        }


        public RespondWebViewData<List<RespondQueryDepartmentViewModel>> GetQueryDepartmentList(RequestWebViewData<RequestQueryDepartmentViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryDepartmentViewModel();
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryDepartmentViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryDepartmentList"),

                #region =================
                CallBackFunc = () =>
                {
                    var parameter = new QueryDepartmentListParameter
                    {
                        Deleted = request.data.Deleted,
                        SerialNumber = request.data.SerialNumber,
                        Name = request.data.Name,
                        PageIndex = request.page,
                        PageSize = request.rows,
                        Status = request.data.Status,
                        ParentId = request.data.ParentId,
                        Spelling = request.data.Spelling
                    };
                    var pageDataList = this.mDepartmentDataAccess.GetQueryDepartmentList(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryDepartmentViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item => new RespondQueryDepartmentViewModel
                        {
                            ChildNumber = item.childnumber,
                            ClassId = item.classid,
                            ChildCount = item.childcount,
                            Comment = item.comment,
                            Deleted = item.deleted,
                            DepId = item.depid,
                            Name = item.name,
                            ParentId = item.parentid,
                            SerialNumber = item.serialnumber,
                            Sort = item.sort.ToString(),
                            Spelling = item.pinyin,
                            Status = item.status
                        }).ToList()
                    };
                    return respond;
                },

                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.BasicDepartment,
                ParamsKeys = new object[]
                {
                    request.data.Deleted,
                    request.data.SerialNumber,
                    request.data.Name,
                    request.page,
                    request.rows,
                    request.data.Status,
                    request.data.ParentId
                }
            });
        }

        public RespondWebViewData<RespondAddDepartmentViewModel> AddDepartment(RequestWebViewData<RequestAddDepartmentViewModel> request)
        {
            var rData = request.data;
            if (this.mDepartmentDataAccess.CheckDepartmentIsExist(new QueryDepartmentListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber }))
                return new RespondWebViewData<RespondAddDepartmentViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的部门已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RespondAddDepartmentViewModel>(WebViewErrorCode.Success);
            try
            {
                var deparment = this.mDepartmentDataAccess.GetDepartmentByClassID(new QueryDepartmentListParameter{ ParentId = rData.ParentId });
                if (deparment == null) return new RespondWebViewData<RespondAddDepartmentViewModel>(WebViewErrorCode.NotExistsDataInfo);
                if (deparment.deleted == (short)CommonDeleted.Deleted) return new RespondWebViewData<RespondAddDepartmentViewModel>(WebViewErrorCode.NotExistsDataInfo);

                this.mDbConnection.ExecuteTransaction(tran =>
                {
                    var parameter = new QueryDepartmentListParameter()
                    {
                        ParentId = rData.ParentId
                    };
                    var classId = string.Concat(rData.ParentId, "000001");
                    var depList = this.mDepartmentDataAccess.GetDepartmentListByParentID(parameter);
                    if (depList.Count > 0)
                        classId = BuildNewClassIdByLastClassId.GeneratedNewClassIdByLastClassId(depList[0].classid);

                    var data = new departmentDataModel
                    {
                        childnumber = 0,
                        classid = classId,
                        comment = rData.Comment,
                        name = rData.Name,
                        parentid = rData.ParentId,
                        pinyin = rData.Spelling,
                        serialnumber = rData.SerialNumber,
                        sort = rData.Sort,
                        status = (short)CommonStatus.Used,
                        deleted = (short)CommonDeleted.NotDeleted
                    };
                    var addResult = this.mDepartmentDataAccess.Add(data, tran);
                    if (addResult > 0) this.mDepartmentDataAccess.UpdateChildNumberByClassId(tran, parameter);
                    MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicDepartment);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("新增部门资料", request, respond, this.GetType());
                });
            }
            catch (Exception ex)
            {
                respond = new RespondWebViewData<RespondAddDepartmentViewModel>(new ErrorCodeItem(WebViewErrorCode.Exception.ErrorCode, ex.Message));
            }
            return respond;
        }

        public RespondWebViewData<RespondQueryDepartmentViewModel> GetDepartmentByDepId(RequestWebViewData<RequestGetDepartmentByIdViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQueryDepartmentViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetDepartmentByDepId"),

                #region ====================
                CallBackFunc = () =>
                {
                    var respond = new RespondWebViewData<RespondQueryDepartmentViewModel>
                    {
                        rows = new RespondQueryDepartmentViewModel()
                    };
                    var department = this.mDepartmentDataAccess.GetdepartmentDataModelById(request.data.DepId);
                    if (department == null) return respond;
                    if (department.deleted == (short)CommonDeleted.Deleted || department.status != (short)CommonStatus.Used) return respond;
                    respond = new RespondWebViewData<RespondQueryDepartmentViewModel>
                    {
                        rows = new RespondQueryDepartmentViewModel
                        {
                            ChildCount = department.childcount,
                            ChildNumber = department.childnumber,
                            ClassId = department.classid,
                            Comment = department.comment,
                            Deleted = department.deleted,
                            DepId = department.depid,
                            Name = department.name,
                            ParentId = department.parentid,
                            SerialNumber = department.serialnumber,
                            Status = department.status,
                            Spelling = department.pinyin,
                            Sort = department.sort.ToString()
                        }
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.BasicDepartment,
                ParamsKeys = new object[]
                {
                    request.data.DepId
                }
            });
        }

        public RespondWebViewData<RespondEditDepartmentViewModel> EditDepartment(RequestWebViewData<RequestEditDepartmentViewModel> request)
        {
            var rData = request.data;
            if (this.mDepartmentDataAccess.CheckDepartmentIsExist(new QueryDepartmentListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber, DepId = rData.DepId }))
                return new RespondWebViewData<RespondEditDepartmentViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的部门已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RespondEditDepartmentViewModel>(WebViewErrorCode.Success);
            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var department = this.mDepartmentDataAccess.GetdepartmentDataModelById(rData.DepId);
                if (department == null)
                {
                    respond = new RespondWebViewData<RespondEditDepartmentViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }
                if (department.deleted == (short)CommonDeleted.Deleted)
                {
                    respond = new RespondWebViewData<RespondEditDepartmentViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }

                var data = new departmentDataModel()
                {
                    depid = rData.DepId,
                    comment = rData.Comment,
                    name = rData.Name,
                    pinyin = rData.Spelling,
                    serialnumber = rData.SerialNumber,
                    sort = rData.Sort,
                    status = department.status,
                    deleted = department.deleted,
                    childnumber = department.childnumber,
                    classid = department.classid,
                    parentid = department.parentid
                };
                this.mDepartmentDataAccess.Update(data, tran);
                MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicDepartment);

                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.SaveMongoDbData("编辑分公司资料", request, respond, this.GetType());
            });
            return respond;
        }

        public RespondWebViewData<RespondDeleteDepartmentViewModel> DeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteDepartmentViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteDepartmentParameter
            {
                depid = request.data.DepId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mDepartmentDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteDepartmentViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicDepartment);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除部门资料", request, respond, this.GetType());
            return respond;
        }

        public RespondWebViewData<RespondDeleteDepartmentViewModel> ReDeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteDepartmentViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteDepartmentParameter
            {
                depid = request.data.DepId,
                Deleted = (short)CommonDeleted.NotDeleted
            };
            var dataResult = this.mDepartmentDataAccess.ReDelete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteDepartmentViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicDepartment);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("恢复删除部门资料", request, respond, this.GetType());
            return respond;
        }
    }
}