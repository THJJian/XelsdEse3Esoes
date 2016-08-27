namespace CPSS.Common.Helper.Config
{
    public class RSASecretConfig
    {
        /// <summary>
        /// 被请求端的RSA公钥
        /// </summary>
        public string RequestedRSAPublicKey { set; get; }

        /// <summary>
        /// 公钥
        /// </summary>
        public string SelfRSAPublicKey { set; get; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string SelfRSAPrivateKey { set; get; }
    }
}