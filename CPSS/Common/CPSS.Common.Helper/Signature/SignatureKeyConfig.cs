using System;

namespace CPSS.Common.Core.Helper.Signature
{
    public class SignatureKeyConfig
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpTime { set; get; }

        /// <summary>
        /// 数字签名加密密钥
        /// </summary>
        public string SignaturePrivateKey { set; get; }
    }
}