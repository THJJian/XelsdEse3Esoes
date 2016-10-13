using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Data.DataAccess.Interfaces.SystemParameterConfig;
using CPSS.Data.DataAccess.Interfaces.SystemParameterConfig.Parameters;
using CPSS.Data.DataAcess.DataModels.SystemParameterConfig;

namespace CPSS.Data.DataAccess.SystemParameterConfig
{
    public class SystemParameterConfigDataAccess : GenericDataAccessBase<SystemParameterConfigDataModel>, ISystemParameterConfigDataAccess
    {
        public SystemParameterConfigDataAccess(IDbConnection _connection) : base(_connection)
        {
        }

        public IList<SystemParameterConfigDataModel> GetSystemParameterConfigDataModels()
        {
            this.ExecuteSQL = "SELECT configname ParameterConfigName,configvlaue ParameterConfigValue FROM sysconfig";
            return this.ExecuteReadSqlToSystemParameterConfigDataModelList();
        }

        public SystemParameterConfigDataModel GetSystemParameterConfigDataModel(SystemParameterConfigParameter parameter)
        {
            this.ExecuteSQL = "SELECT configname ParameterConfigName,configvlaue ParameterConfigValue FROM sysconfig WHERE configname=@ParameterConfigName";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@ParameterConfigName", parameter.ParameterConfigName)
            };
            return this.ExecuteReadSqlToSystemParameterConfigDataModel();
        }
    }
}