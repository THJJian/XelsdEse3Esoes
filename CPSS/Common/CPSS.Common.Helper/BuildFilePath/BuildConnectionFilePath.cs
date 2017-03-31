namespace CPSS.Common.Core.Helper.BuildFilePath
{
    public class BuildConnectionFilePath
    {
        public static string BuildFilePath(int companyID)
        {
            return string.Format("~/config/{0}/connection.config", companyID);
        }
    }
}