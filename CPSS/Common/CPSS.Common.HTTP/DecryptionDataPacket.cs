using System;
using System.IO;
using System.Security.Cryptography;
using CPSS.Common.Helper.Extension;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.HTTP
{
    /// <summary>
    ///     解密数据包
    /// </summary>
    public class DecryptionDataPacket
    {
        private readonly RSACryptoServiceProvider rsaCryptoService;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="privateKey">私钥</param>
        public DecryptionDataPacket(string privateKey)
        {
            this.rsaCryptoService = new RSACryptoServiceProvider();
            this.rsaCryptoService.FromXmlString(privateKey);
        }

        /// <summary>
        ///     解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JObject DecryptData(JObject data)
        {
            try
            {
                var desIV = this.rsaCryptoService.Decrypt(data["TripleDesIV"].ToObject<byte[]>(), false);
                var desKey = this.rsaCryptoService.Decrypt(data["TripleDesKey"].ToObject<byte[]>(), false);
                var desProvider = new TripleDESCryptoServiceProvider();
                var memoryStream = new MemoryStream();
                var cryptoTransform = desProvider.CreateDecryptor(desKey, desIV);
                var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
                var encryptData = data["EncryptData"].ToObject<byte[]>();
                cryptoStream.Write(encryptData, 0, encryptData.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
                var result = memoryStream.ToArray();
                data["Data"] = JObject.Parse(result.ToUTF8String());
                return data;
            }
            catch
            {
                throw new Exception("解密失败");
            }
        }
    }
}