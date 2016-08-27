using System;
using System.IO;
using System.Security.Cryptography;
using CPSS.Common.Helper.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.HTTP
{
    /// <summary>
    ///     加密数据包
    /// </summary>
    public class EncryptDataPacket
    {
        /// <summary>
        ///     非对称加密服务对象
        /// </summary>
        private readonly RSACryptoServiceProvider rasCryptoService;

        /// <summary>
        ///     对称加密服务对象
        /// </summary>
        private readonly TripleDESCryptoServiceProvider tripleDescCryptoService;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="publicKey">公钥</param>
        public EncryptDataPacket(string publicKey)
        {
            this.tripleDescCryptoService = new TripleDESCryptoServiceProvider();
            this.tripleDescCryptoService.GenerateIV();
            this.tripleDescCryptoService.GenerateKey();
            this.rasCryptoService = new RSACryptoServiceProvider();
            this.rasCryptoService.FromXmlString(publicKey);
        }

        /// <summary>
        ///     加密对称加密的密钥Key
        /// </summary>
        /// <returns></returns>
        private byte[] EncryptTripleDesKey()
        {
            var key = this.tripleDescCryptoService.Key;
            return this.rasCryptoService.Encrypt(key, false);
        }

        /// <summary>
        ///     加密对称加密的向量IV
        /// </summary>
        /// <returns></returns>
        private byte[] EncryptTripleDesIV()
        {
            var iv = this.tripleDescCryptoService.IV;
            return this.rasCryptoService.Encrypt(iv, false);
        }

        /// <summary>
        ///     加密业务数据
        /// </summary>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        private byte[] EncryptBussinessData(byte[] dataBuffer)
        {
            try
            {
                var result = new MemoryStream();
                var encryptor = this.tripleDescCryptoService.CreateEncryptor();
                var cryptoStream = new CryptoStream(result, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(dataBuffer, 0, dataBuffer.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
                return result.ToArray();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        ///     加密数据
        /// </summary>
        /// <returns></returns>
        public JObject EncryptData(JObject data)
        {
            data["TripleDesKey"] = this.EncryptTripleDesKey();
            data["TripleDesIV"] = this.EncryptTripleDesIV();
            if (data["Data"] == null) return data;

            var buffer = data["Data"].ToString(Formatting.None).ToUTF8Bytes();
            data["EncryptData"] = this.EncryptBussinessData(buffer);
            data["Data"] = null;
            return data;
        }
    }
}