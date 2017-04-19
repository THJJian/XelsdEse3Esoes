using System;
using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core;
using CPSS.Common.Core.Authenticate;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Department.Request;
using CPSS.Service.ViewService.ViewModels.Department.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class DepartmentViewService : IDepartmentViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.DepartmentViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.DepartmentViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IMongoDbDataAccess mMongoDbDataAccess;
        private readonly SigninUser mSigninUser;

        public DepartmentViewService(IDbConnection dbConnection, IMongoDbDataAccess mongoDbDataAccess)
        {
            mDbConnection = dbConnection;
            mMongoDbDataAccess = mongoDbDataAccess;
            mSigninUser = CPSSAuthenticate.GetCurrentUser();
        }


        public RespondWebViewData<List<RespondQueryDepartmentViewModel>> GetQueryCompanyList(RequestWebViewData<RequestQueryDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RespondAddDepartmentViewModel> AddSubCompany(RequestWebViewData<RequestAddDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RespondQueryDepartmentViewModel> GetSubCompanyByComId(RequestWebViewData<RequestGetDepartmentByIdViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RequestEditDepartmentViewModel> EditSubCompany(RequestWebViewData<RequestEditDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RespondDeleteDepartmentViewModel> DeleteSubCompany(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RespondDeleteDepartmentViewModel> ReDeleteSubCompany(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }
    }
}