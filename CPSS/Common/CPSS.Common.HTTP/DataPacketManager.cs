using System;
using CPSS.Common.Helper.Config;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.HTTP
{
    /// <summary>
    /// 数据包管理类
    /// </summary>
    public class DataPacketManager
    {
        private readonly string mRequestPublicKey;

        /// <summary>
        ///     默认私钥
        /// </summary>
        private readonly string mSelfPrivateKey;

        /// <summary>
        ///     默认公钥
        /// </summary>
        private readonly string mSelfPublicKey;

        /// <summary>
        ///     构造数据包管理对象
        /// </summary>
        public DataPacketManager()
        {
        }

        /// <summary>
        ///     构造数据包管理对象
        /// </summary>
        /// <param name="requestedPublicKey">被请求端RSA公钥</param>
        /// <param name="selfPublicKey">rsa公钥</param>
        /// <param name="selfPrivateKey">rsa私钥</param>
        public DataPacketManager(string requestedPublicKey, string selfPublicKey, string selfPrivateKey)
        {
            if (string.IsNullOrEmpty(requestedPublicKey))
                throw new Exception("被请求端的公钥丢失。");
            if (string.IsNullOrEmpty(selfPublicKey))
                throw new Exception("公钥丢失(self)");
            if (string.IsNullOrEmpty(selfPrivateKey))
                throw new Exception("私钥丢失(self)");

            mRequestPublicKey = requestedPublicKey;
            mSelfPublicKey = selfPublicKey;
            mSelfPrivateKey = selfPrivateKey;
        }

        /// <summary>
        ///     构造数据包管理对象
        /// </summary>
        /// <param name="config"></param>
        public DataPacketManager(RSASecretConfig config)
        {
            if (string.IsNullOrEmpty(config.RequestedRSAPublicKey))
                throw new Exception("被请求端的公钥丢失。");
            if (string.IsNullOrEmpty(config.SelfRSAPublicKey))
                throw new Exception("公钥丢失(self)");
            if (string.IsNullOrEmpty(config.SelfRSAPrivateKey))
                throw new Exception("私钥丢失(self)");

            mRequestPublicKey = config.RequestedRSAPublicKey;
            mSelfPublicKey = config.SelfRSAPublicKey;
            mSelfPrivateKey = config.SelfRSAPrivateKey;
        }

        private void CheckKey()
        {
            if (string.IsNullOrEmpty(mRequestPublicKey))
                throw new Exception("被请求端的公钥丢失。");
            if (string.IsNullOrEmpty(mSelfPublicKey))
                throw new Exception("公钥丢失(self)");
            if (string.IsNullOrEmpty(mSelfPrivateKey))
                throw new Exception("私钥丢失(self)");
        }

        private EncryptDataPacket GetEncryptDataPacket()
        {
            CheckKey();
            return new EncryptDataPacket(mRequestPublicKey);
        }

        /// <summary>
        ///     加密数据包
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public JObject EncryptData(JObject requestData)
        {
            var encryptDataPacket = GetEncryptDataPacket();
            requestData["ClientPublicKey"] = mSelfPublicKey;
            return encryptDataPacket.EncryptData(requestData);
        }

        private DecryptionDataPacket GetDecryptionDataPacket()
        {
            CheckKey();
            return new DecryptionDataPacket(mSelfPrivateKey);
        }

        /// <summary>
        ///     解密数据包
        /// </summary>
        /// <param name="respondData"></param>
        /// <returns></returns>
        public JObject DecryptData(JObject respondData)
        {
            var decryptionDataPacket = GetDecryptionDataPacket();
            return decryptionDataPacket.DecryptData(respondData);
        }
    }
}