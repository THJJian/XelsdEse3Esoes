using System.Collections.Generic;
using CPSS.Data.DataAccess.Interfaces.SystemParameterConfig.Parameters;
using CPSS.Data.DataAcess.DataModels.SystemParameterConfig;

namespace CPSS.Data.DataAccess.Interfaces.SystemParameterConfig
{
    public interface ISystemParameterConfigDataAccess
    {

        /// <summary>
        /// 获取系统配置参数
        /// </summary>
        /// <returns></returns>
        IList<SystemParameterConfigDataModel> GetSystemParameterConfigDataModels();

        /// <summary>
        /// 获取指定系统配置参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>

        SystemParameterConfigDataModel GetSystemParameterConfigDataModel(SystemParameterConfigParameter parameter);

    }
}