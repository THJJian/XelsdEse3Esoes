using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Helper.Generated;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Employee;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Employee.Request;
using CPSS.Service.ViewService.ViewModels.Employee.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class EmployeeViewService: BaseViewService, IEmployeeViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.EmployeeViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.EmployeeViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IemployeeDataAccess mEmployeeDataAccess;


        public EmployeeViewService(IMongoDbDataAccess mongoDbDataAccess, IDbConnection dbConnection, IemployeeDataAccess employeeDataAccess) : base(mongoDbDataAccess)
        {
            this.mDbConnection = dbConnection;
            this.mEmployeeDataAccess = employeeDataAccess;
        }

        public RespondWebViewData<List<RespondQueryEmployeeViewModel>> GetQueryEmployeeList(RequestWebViewData<RequestQueryEmployeeViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryEmployeeViewModel();
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryEmployeeViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryEmployeeList"),

                #region =================
                CallBackFunc = () =>
                {
                    var parameter = new QueryEmployeeListParameter()
                    {
                        Comment = request.data.Comment,
                        Deleted = request.data.Deleted,
                        DepId = request.data.DepId,
                        SerialNumber = request.data.SerialNumber,
                        Mobile = request.data.Mobile,
                        Name = request.data.Name,
                        PageIndex = request.page,
                        PageSize = request.rows,
                        Status = request.data.Status,
                        ParentId = request.data.ParentId,
                        Spelling = request.data.Spelling,
                    };
                    var pageDataList = this.mEmployeeDataAccess.GetQueryEmployeeList(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryEmployeeViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item => new RespondQueryEmployeeViewModel
                        {
                            Address = item.address,
                            ChildNumber = item.childnumber,
                            ClassId = item.classid,
                            Comment = item.comment,
                            Deleted = item.deleted.HasValue ? item.deleted.Value : (short)CommonDeleted.NotDeleted,
                            DepName = item.depname,
                            EmpId = item.empid,
                            LowestDiscount = item.lowestdiscount.HasValue ? item.lowestdiscount.Value : (short)100,
                            Mobile = item.mobile,
                            Name = item.name,
                            ParentId = item.parentid,
                            PreInAdvanceTotal = item.preinadvancetotal.HasValue ? item.preinadvancetotal.Value.ToCurrencyString(5) : "0.00000",
                            PrePayFeeTotal = item.prepayfeetotal.HasValue ? item.prepayfeetotal.Value.ToCurrencyString(5) : "0.00000",
                            SerialNumber = item.serialnumber,
                            Sort = item.sort.HasValue ? item.sort.Value : 0,
                            Spelling = item.pinyin,
                            Status = item.status.HasValue ? item.status.Value : (short)CommonStatus.Used
                        }).ToList()
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.Status,
                    request.data.Comment,
                    request.data.DepId,
                    request.data.Mobile,
                    request.data.Deleted,
                    request.data.Name,
                    request.data.ParentId,
                    request.data.SerialNumber,
                    request.data.Spelling,
                    request.page,
                    request.rows
                }
            });
        }

        public RespondWebViewData<RespondAddEmployeeViewModel> AddEmployee(RequestWebViewData<RequestAddEmployeeViewModel> request)
        {
            var respond = new RespondWebViewData<RespondAddEmployeeViewModel>(WebViewErrorCode.Success);
            var rData = request.data;
            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var parameter = new QueryEmployeeListParameter()
                {
                    ParentId = rData.ParentId
                };
                var classId = string.Concat(rData.ParentId, "000001");
                var depList = this.mEmployeeDataAccess.GetEmployeeListByParentID(parameter);
                if (depList.Count > 0)
                    classId = BuildNewClassIdByLastClassId.GeneratedNewClassIdByLastClassId(depList[0].classid);

                var data = new employeeDataModel
                {
                    address = rData.Address,
                    childnumber = 0,
                    classid = classId,
                    comment = rData.Comment,
                    deleted = 1,
                    depid = rData.DepId,
                    depname = rData.DepName,
                    lowestdiscount = rData.LowestDiscount,
                    mobile = rData.Mobile,
                    name = rData.Name,
                    parentid = rData.ParentId,
                    pinyin = rData.Spelling,
                    serialnumber = rData.SerialNumber,
                    sort = rData.Sort,
                    status = 1,
                    preinadvancetotal = rData.PreInAdvanceTotal,
                    prepayfeetotal = rData.PrepayFeeTotal
                };
                var addResult = this.mEmployeeDataAccess.Add(data, tran);
                if (addResult > 0) this.mEmployeeDataAccess.UpdateChildNumberByClassId(tran, parameter);
                MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.SaveMongoDbData("新增职员资料", request, respond, this.GetType());
            });
            return respond;
        }

        public RespondWebViewData<RespondQueryEmployeeViewModel> GetEmployeeByEmpId(RequestWebViewData<RequestGetEmployeeByIdViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQueryEmployeeViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetEmployeeByEmpId"),

                #region ====================
                CallBackFunc = () =>
                {
                    var respond = new RespondWebViewData<RespondQueryEmployeeViewModel>
                    {
                        rows = new RespondQueryEmployeeViewModel()
                    };
                    var employee = this.mEmployeeDataAccess.GetemployeeDataModelById(request.data.EmpId);
                    if (employee == null) return respond;
                    if (employee.deleted == (short)CommonDeleted.Deleted || employee.status != (short)CommonStatus.Used) return respond;
                    
                    respond = new RespondWebViewData<RespondQueryEmployeeViewModel>
                    {
                        rows = new RespondQueryEmployeeViewModel
                        {
                            Address = employee.address,
                            ChildNumber = employee.childnumber,
                            ClassId = employee.classid,
                            Comment = employee.comment,
                            Deleted = employee.deleted.HasValue ? employee.deleted.Value : (short)CommonDeleted.NotDeleted,
                            DepId = employee.depid,
                            DepName = employee.depname,
                            EmpId = employee.empid,
                            LowestDiscount = employee.lowestdiscount.HasValue ? employee.lowestdiscount.Value : (short)100,
                            Mobile = employee.mobile,
                            Name = employee.name,
                            ParentId = employee.parentid,
                            PreInAdvanceTotal = employee.preinadvancetotal.HasValue ? employee.preinadvancetotal.Value.ToNumberString(5) : "0.00000",
                            PrePayFeeTotal = employee.prepayfeetotal.HasValue ? employee.prepayfeetotal.Value.ToNumberString(5) : "0.00000",
                            SerialNumber = employee.serialnumber,
                            Status = employee.status.Value,
                            Spelling = employee.pinyin,
                            Sort = employee.sort.HasValue ? employee.sort.Value : 0
                        }
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.EmpId
                }
            });
        }

        public RespondWebViewData<RequestEditEmployeeViewModel> EditEmployee(RequestWebViewData<RequestEditEmployeeViewModel> request)
        {
            var respond = new RespondWebViewData<RequestEditEmployeeViewModel>(WebViewErrorCode.Success);
            var rData = request.data;

            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var employee = this.mEmployeeDataAccess.GetemployeeDataModelById(rData.DepId);
                if (employee == null)
                {
                    respond = new RespondWebViewData<RequestEditEmployeeViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }
                if (employee.deleted == (short)CommonDeleted.Deleted)
                {
                    respond = new RespondWebViewData<RequestEditEmployeeViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return;
                }

                //TODO 开账后部门、名称、折扣、预收、预付等字段是不允许修改的，未实现。
                var data = new employeeDataModel
                {
                    address = rData.Address,
                    childnumber = employee.childnumber,
                    classid = employee.classid,
                    comment = rData.Comment,
                    depid = employee.depid,
                    depname = employee.depname,
                    deleted = employee.deleted,
                    empid = rData.EepId,
                    mobile = rData.Mobile,
                    name = employee.name,
                    prepayfeetotal = employee.prepayfeetotal,
                    preinadvancetotal = employee.preinadvancetotal,
                    pinyin = employee.pinyin,
                    parentid = employee.parentid,
                    serialnumber = rData.SerialNumber,
                    sort = rData.Sort,
                    status = employee.status,
                    lowestdiscount = employee.lowestdiscount
                };
                this.mEmployeeDataAccess.Update(data, tran);
                MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.SaveMongoDbData("编辑职员资料", request, respond, this.GetType());
            });
            return respond;
        }

        public RespondWebViewData<RespondDeleteEmployeeViewModel> DeleteEmployee(RequestWebViewData<RequestDeleteEmployeeViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteEmployeeViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteEmployeeParameter
            {
                empid = request.data.EmpId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mEmployeeDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteEmployeeViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除删除资料", request, respond, this.GetType());
            return respond;
        }

        public RespondWebViewData<RespondDeleteEmployeeViewModel> ReDeleteEmployee(RequestWebViewData<RequestDeleteEmployeeViewModel> request)
        {
            var respond = new RespondWebViewData<RespondDeleteEmployeeViewModel>(WebViewErrorCode.Exception);
            var parameter = new DeleteEmployeeParameter
            {
                empid = request.data.EmpId,
                Deleted = (short)CommonDeleted.Deleted
            };
            var dataResult = this.mEmployeeDataAccess.Delete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteEmployeeViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除职员资料", request, respond, this.GetType());
            return respond;
        }
    }
}