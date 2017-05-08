using System.Collections.Generic;
using CPSS.Data.DataAccess.Interfaces.SystemManage.Parameters;
using CPSS.Data.DataAcess.DataModels.SystemParameterConfig;

namespace CPSS.Data.DataAccess.Interfaces.SystemManage
{
    public interface ISystemParameterConfigDataAccess
    {

        /// <summary>
        /// 获取系统配置参数
        /// </summary>
        /// <returns></returns>
        List<SystemParameterConfigDataModel> GetSystemParameterConfigDataModels();

        /// <summary>
        /// 获取指定系统配置参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>

        SystemParameterConfigDataModel GetSystemParameterConfigDataModel(SystemParameterConfigParameter parameter);

        /// <summary>
        /// 保存系统参数设置
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool SaveSystemParameterConfig(List<SystemParameterConfigParameter> parameters);
    }
}