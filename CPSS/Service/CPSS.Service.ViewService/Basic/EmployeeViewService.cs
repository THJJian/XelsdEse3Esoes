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
using CPSS.Common.Core.SysConfig;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Employee;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.Interfaces.SystemManage;
using CPSS.Service.ViewService.ViewModels.Employee.Request;
using CPSS.Service.ViewService.ViewModels.Employee.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class EmployeeViewService : BaseViewService, IEmployeeViewService
    {
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.EmployeeViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IemployeeDataAccess mEmployeeDataAccess;
        private readonly ISystemParameterConfigViewService mSystemParameterConfigViewService;

        public EmployeeViewService(IMongoDbDataAccess mongoDbDataAccess, IDbConnection dbConnection, IemployeeDataAccess employeeDataAccess, ISystemParameterConfigViewService systemParameterConfigViewService) : base(mongoDbDataAccess)
        {
            this.mDbConnection = dbConnection;
            this.mEmployeeDataAccess = employeeDataAccess;
            this.mSystemParameterConfigViewService = systemParameterConfigViewService;
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
                    var totalScale = this.mSystemParameterConfigViewService.SystemParameterConfig()[SystemParameterConfigConst.TotalScale].ParameterConfigValue.ToInt16();
                    var respond = new RespondWebViewData<List<RespondQueryEmployeeViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item => new RespondQueryEmployeeViewModel
                        {
                            Address = item.address,
                            ChildNumber = item.childnumber,
                            ClassId = item.classid,
                            Comment = item.comment,
                            Deleted = item.deleted,
                            DepName = item.depname,
                            EmpId = item.empid,
                            LowestDiscount = item.lowestdiscount,
                            Mobile = item.mobile,
                            Name = item.name,
                            ParentId = item.parentid,
                            PreInAdvanceTotal = item.preinadvancetotal.ToCurrencyString(totalScale),
                            PrePayFeeTotal = item.prepayfeetotal.ToCurrencyString(totalScale),
                            SerialNumber = item.serialnumber,
                            Sort = item.sort,
                            Spelling = item.pinyin,
                            Status = item.status
                        }).ToList()
                    };
                    return respond;
                },

                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.BasicEmployee,
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
            var rData = request.data;
            if (this.mEmployeeDataAccess.CheckEmployeeIsExist(new QueryEmployeeListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber, DepId = rData.DepId }))
                return new RespondWebViewData<RespondAddEmployeeViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的职员已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RespondAddEmployeeViewModel>(WebViewErrorCode.Success);
            try
            {
                var employee = this.mEmployeeDataAccess.GetEmployeeByClassID(new QueryEmployeeListParameter {ParentId = rData.ParentId});
                if (employee == null) return new RespondWebViewData<RespondAddEmployeeViewModel>(WebViewErrorCode.NotExistsDataInfo);
                if (employee.deleted == (short) CommonDeleted.Deleted) return new RespondWebViewData<RespondAddEmployeeViewModel>(WebViewErrorCode.NotExistsDataInfo);
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
                        deleted = (short)CommonDeleted.NotDeleted,
                        depid = rData.DepId,
                        depname = rData.DepName,
                        lowestdiscount = rData.LowestDiscount,
                        mobile = rData.Mobile,
                        name = rData.Name,
                        parentid = rData.ParentId,
                        pinyin = rData.Spelling,
                        serialnumber = rData.SerialNumber,
                        sort = rData.Sort,
                        status = (short)CommonStatus.Used,
                        preinadvancetotal = rData.PreInAdvanceTotal,
                        prepayfeetotal = rData.PrepayFeeTotal
                    };
                    var addResult = this.mEmployeeDataAccess.Add(data, tran);
                    if (addResult > 0) this.mEmployeeDataAccess.UpdateChildNumberByClassId(tran, parameter);
                    MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicEmployee);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("新增职员资料", request, respond, this.GetType());
                });
            }
            catch (Exception ex)
            {
                respond = new RespondWebViewData<RespondAddEmployeeViewModel>(new ErrorCodeItem(WebViewErrorCode.Exception.ErrorCode, ex.Message));
            }
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
                    var totalScale = this.mSystemParameterConfigViewService.SystemParameterConfig()[SystemParameterConfigConst.TotalScale].ParameterConfigValue.ToInt16();
                    
                    respond = new RespondWebViewData<RespondQueryEmployeeViewModel>
                    {
                        rows = new RespondQueryEmployeeViewModel
                        {
                            Address = employee.address,
                            ChildNumber = employee.childnumber,
                            ClassId = employee.classid,
                            Comment = employee.comment,
                            Deleted = employee.deleted,
                            DepId = employee.depid,
                            DepName = employee.depname,
                            EmpId = employee.empid,
                            LowestDiscount = employee.lowestdiscount,
                            Mobile = employee.mobile,
                            Name = employee.name,
                            ParentId = employee.parentid,
                            PreInAdvanceTotal = employee.preinadvancetotal.ToNumberString(totalScale),
                            PrePayFeeTotal = employee.prepayfeetotal.ToNumberString(totalScale),
                            SerialNumber = employee.serialnumber,
                            Status = employee.status,
                            Spelling = employee.pinyin,
                            Sort = employee.sort
                        }
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.BasicEmployee,
                ParamsKeys = new object[]
                {
                    request.data.EmpId
                }
            });
        }

        public RespondWebViewData<RespondEditEmployeeViewModel> EditEmployee(RequestWebViewData<RequestEditEmployeeViewModel> request)
        {
            var rData = request.data;
            var employee = this.mEmployeeDataAccess.GetemployeeDataModelById(rData.EmpId);
            if (employee == null) return new RespondWebViewData<RespondEditEmployeeViewModel>(WebViewErrorCode.NotExistsDataInfo);
            if (employee.deleted == (short)CommonDeleted.Deleted) return new RespondWebViewData<RespondEditEmployeeViewModel>(WebViewErrorCode.NotExistsDataInfo);
            if (this.mEmployeeDataAccess.CheckEmployeeIsExist(new QueryEmployeeListParameter { Name = rData.Name, SerialNumber = rData.SerialNumber, DepId = employee.depid, EmpId = rData.EmpId }))
                return new RespondWebViewData<RespondEditEmployeeViewModel>(WebViewErrorCode.ExistsDataInfo.ErrorCode, string.Format("名称为[{0}]或编号为[{1}]的职员已经存在", rData.Name, rData.SerialNumber));

            var respond = new RespondWebViewData<RespondEditEmployeeViewModel>(WebViewErrorCode.Success);
            try
            {
                this.mDbConnection.ExecuteTransaction(tran =>
                {
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
                        empid = rData.EmpId,
                        mobile = rData.Mobile,
                        name = rData.Name,
                        prepayfeetotal = employee.prepayfeetotal,
                        preinadvancetotal = employee.preinadvancetotal,
                        pinyin = rData.Spelling,
                        parentid = employee.parentid,
                        serialnumber = rData.SerialNumber,
                        sort = rData.Sort,
                        status = employee.status,
                        lowestdiscount = employee.lowestdiscount
                    };
                    this.mEmployeeDataAccess.Update(data, tran);
                    MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicEmployee);

                    //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                    //this.SaveMongoDbData("编辑职员资料", request, respond, this.GetType());
                });
            }
            catch (Exception ex)
            {
                respond = new RespondWebViewData<RespondEditEmployeeViewModel>(WebViewErrorCode.Exception.ErrorCode, ex.Message);
            }
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
            MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicEmployee);

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
                Deleted = (short)CommonDeleted.NotDeleted
            };
            var dataResult = this.mEmployeeDataAccess.ReDelete(parameter);
            if (dataResult <= 0) return respond;
            respond = new RespondWebViewData<RespondDeleteEmployeeViewModel>(WebViewErrorCode.Success);
            MemcacheHelper.RemoveBy(ServiceMemcachedKeyManageConst.BasicEmployee);

            //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
            //this.SaveMongoDbData("删除职员资料", request, respond, this.GetType());
            return respond;
        }
    }
}