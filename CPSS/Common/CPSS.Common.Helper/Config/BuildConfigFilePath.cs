namespace CPSS.Common.Helper.Config
{
    /// <summary>
    /// 生成配置文件的路径
    /// </summary>
    public class BuildConfigFilePath
    {
        /// <summary>
        /// 生成数字签名的密钥key存放路径
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string BuildSignatureConfigFilePath(int companyId, int userId)
        {
            return string.Format("~/Config/Signature/company_{0}/user_{1}/signaturekey.config", companyId, userId);
        }


    }
}