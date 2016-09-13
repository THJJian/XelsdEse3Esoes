using System.Web;

namespace CPSS.Common.Core.Helper.BuildFilePath
{
    public class BuildConnectionFilePath
    {
        public static string BuildFilePath(int companyID)
        {
            return $"~/config/{companyID}/connection.config";
        }
    }
}