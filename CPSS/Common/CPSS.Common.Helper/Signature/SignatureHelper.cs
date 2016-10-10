using System.Security.Cryptography;
using CPSS.Common.Core.Helper.Extension;
using System;
using CPSS.Common.Core.Helper.Config;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.Core.Helper.Signature
{
    public class SignatureHelper
    {
        private static readonly RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();

        /// <summary>
        /// 创建数字签名
        /// </summary>
        /// <param name="jsonSignature">用于创建数字签名的json对象字符串,JObject对象</param>
        /// <returns></returns>
        public static string BuildSignature(JObject jsonSignature)
        {
            var rsaSignatureFormatter = new RSAPKCS1SignatureFormatter(rsaCryptoServiceProvider);
            rsaSignatureFormatter.SetHashAlgorithm("SHA1");
            var sha1 = new SHA1CryptoServiceProvider();

            var jsonSignatureString = jsonSignature.ToString();
            var sha1HashResultBytes = sha1.ComputeHash(jsonSignatureString.ToUTF8Bytes());
            byte[] signData = rsaSignatureFormatter.CreateSignature(sha1HashResultBytes);

            var obj = new SignatureKeyConfig
            {
                ExpTime = DateTime.Now.AddDays(1).ToShortDateString().ToDateTime().AddSeconds(-1),
                SignaturePrivateKey = rsaCryptoServiceProvider.ToXmlString(true)
            };

            int companyId = jsonSignature["CompanySerialNum"].Value<int>(),
                userId = jsonSignature["UserID"].Value<int>();
            var signatureFilePath = BuildConfigFilePath.BuildSignatureConfigFilePath(companyId, userId);
            ConfigHelper.Save(JObject.FromObject(obj).ToString(), signatureFilePath);

            return Convert.ToBase64String(signData);
        }

        /// <summary>
        /// 验证数字签名
        /// </summary>
        /// <param name="jsonSignature">用于创建数字签名的json对象字符串,JObject对象</param>
        /// <param name="signatureText">已经获得的数字签名</param>
        /// <returns></returns>
        public static bool VerifySignature(JObject jsonSignature, string signatureText)
        {
            int companyId = jsonSignature["CompanySerialNum"].Value<int>(),
                userId = jsonSignature["UserID"].Value<int>();
            var signatureFilePath = BuildConfigFilePath.BuildSignatureConfigFilePath(companyId, userId);

            var config = ConfigHelper.GetConfig<SignatureKeyConfig>(signatureFilePath);
            if (config == null || config.ExpTime < DateTime.Now) return false;

            byte[] signature = Convert.FromBase64String(signatureText);
            rsaCryptoServiceProvider.FromXmlString(config.SignaturePrivateKey);

            rsaCryptoServiceProvider.ImportCspBlob(rsaCryptoServiceProvider.ExportCspBlob(false));
            var rsaSignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsaCryptoServiceProvider);
            rsaSignatureDeformatter.SetHashAlgorithm("SHA1");
            var sha1 = new SHA1CryptoServiceProvider();

            string jsonSignatureString = jsonSignature.ToString();
            var sha1HashResultBytes = sha1.ComputeHash(jsonSignatureString.ToUTF8Bytes());
            return rsaSignatureDeformatter.VerifySignature(sha1HashResultBytes, signature);
        }
    }
}