using System;

namespace CPSS.Common.Core.Exception
{
    /// <summary>
    /// 请求返回数据封装类基类
    /// </summary>
    [Serializable]
    public class RespondWebViewDataBase
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public RespondWebViewDataBase()
        {
        }

        public RespondWebViewDataBase(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public RespondWebViewDataBase(ErrorCodeItem errorCodeItem)
        {
            this.ErrorCode = errorCodeItem.ErrorCode;
            this.ErrorMessage = errorCodeItem.ErrorMessage;
        }
    }
}