using CPSS.Data.DataAccess.Interfaces.User.Parameters;
using CPSS.Data.DataAcess.DataModels.User;

namespace CPSS.Data.DataAccess.Interfaces.User
{
    public interface ICompanyInfoDataAccess
    {
        /// <summary>
        /// 新增用户库信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool AddCompanyInfo(CompanyInfoDataModel parameter);

        /// <summary>
        /// 根据用户公司ID获取用户库信息
        /// </summary>
        /// <returns></returns>
        CompanyInfoDataModel GetCompanyInfoDataModelByID(CompanyInfoParameter parameter);
    }
}