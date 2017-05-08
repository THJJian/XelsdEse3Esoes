using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Data.DataAccess.Interfaces.SystemManage;
using CPSS.Data.DataAccess.Interfaces.SystemManage.Parameters;
using CPSS.Data.DataAcess.DataModels.SystemManage;

namespace CPSS.Data.DataAccess.SystemParameterConfig
{
    public class SystemParameterConfigDataAccess : GenericDataAccessBase<SystemParameterConfigDataModel>, ISystemParameterConfigDataAccess
    {
        public SystemParameterConfigDataAccess(IDbConnection _connection) : base(_connection)
        {
        }

        public List<SystemParameterConfigDataModel> GetSystemParameterConfigDataModels()
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

        public bool SaveSystemParameterConfig(List<SystemParameterConfigParameter> parameters)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var parameter in parameters)
            {
                builder.AppendFormat("UPDATE dbo.sysconfig SET configvlaue='{1}' WHERE configname='{0}'\n\r", parameter.ParameterConfigName, parameter.ParameterConfigValue);
            }
            this.ExecuteSQL = builder.ToString();
            return this.ExecuteNonQuery() > 0;
        }
    }
}