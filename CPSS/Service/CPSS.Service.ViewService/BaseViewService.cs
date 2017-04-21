using System;
using CPSS.Common.Core;
using CPSS.Common.Core.Authenticate;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.ViewModels.MongoDb.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPSS.Service.ViewService
{
    public class BaseViewService
    {
        private readonly IMongoDbDataAccess mMongoDbDataAccess;
        protected readonly SigninUser mSigninUser;

        public BaseViewService(IMongoDbDataAccess mongoDbDataAccess)
        {
            this.mMongoDbDataAccess = mongoDbDataAccess;
            this.mSigninUser = CPSSAuthenticate.GetCurrentUser();
        }

        protected bool SaveMongoDbData<request, respond>(string logName, request  req, respond res, Type classType)
        {
            var mongo_db_request = new RequestMongoDbViewModel
            {
                LogName = logName,
                RespondLogData = JObject.FromObject(res).ToString(Formatting.None),
                RequestLogData = JObject.FromObject(req).ToString(Formatting.None),
                LogTime = DateTime.Now,
                SpecialType = classType,
                OpUserID = this.mSigninUser.UserID.ToString(),
                OpUserName = this.mSigninUser.UserName
            };
            return this.mMongoDbDataAccess.Save(mongo_db_request);
        }
    }
}