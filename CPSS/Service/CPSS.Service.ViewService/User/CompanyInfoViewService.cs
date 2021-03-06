﻿using CPSS.Common.Core;
using CPSS.Common.Core.Helper.BuildFilePath;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Config;
using CPSS.Data.DataAccess.Interfaces.User;
using CPSS.Data.DataAccess.Interfaces.User.Parameters;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;

namespace CPSS.Service.ViewService.User
{
    public class CompanyInfoViewService : ICompanyInfoViewService
    {
        private const string preCachedKey = "CPSS.Service.ViewService.User.CompanyInfoViewService.{0}";
        private readonly ICompanyInfoDataAccess mCompanyInfoDataAccess;
        public CompanyInfoViewService(ICompanyInfoDataAccess _companyInfoDataAccess)
        {
            this.mCompanyInfoDataAccess = _companyInfoDataAccess;
        }

        public RespondCompanyInfoViewModel GetCompanyInfoViewModel(RequestCompanyInfoViewModel request)
        {
            var respond = MemcacheHelper.Get(() =>
            {
                var filePath = BuildConnectionFilePath.BuildFilePath(request.CompanyID);
                var _respond = new RespondCompanyInfoViewModel();
                if (ExistsFileHelper.ExistsFile(filePath))
                {
                    var connectionConfig = ConfigHelper.GetConfig<DbConnectionConfig>(filePath);
                    _respond = new RespondCompanyInfoViewModel
                    {
                        CompanyID = request.CompanyID,
                        ConnectTimeout = connectionConfig.ConnectTimeout,
                        Database = connectionConfig.Database,
                        Password = connectionConfig.Password,
                        Server = connectionConfig.Server,
                        UserID = connectionConfig.UserID
                    };
                    return _respond;
                }

                var parameter = new CompanyInfoParameter
                {
                    CompanyID = request.CompanyID
                };
                var dataModel = this.mCompanyInfoDataAccess.GetCompanyInfoDataModelByID(parameter);
                if (dataModel == null) return _respond;
                _respond = new RespondCompanyInfoViewModel
                {
                    CompanyID = dataModel.comid,
                    ConnectTimeout = dataModel.timeout,
                    Database = dataModel.dbase,
                    Password = dataModel.pwd,
                    Server = dataModel.dbserver,
                    UserID = dataModel.uid
                };
                var _DbconnectionConfig = new DbConnectionConfig
                {
                        ConnectTimeout = dataModel.timeout,
                        Database = dataModel.dbase,
                        Password = dataModel.pwd,
                        Server = dataModel.dbserver,
                        UserID = dataModel.uid
                };
                ConfigHelper.Save(_DbconnectionConfig, filePath);
                return _respond;
            }, string.Format(preCachedKey, "GetCompanyInfoViewModel"), false, request.CompanyID);
            return respond;
        }
    }
}