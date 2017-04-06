using System;
using CPSS.Common.Core.Exception;

namespace CPSS.Common.Core
{
    /// <summary>
    /// 请求返回数据封装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class RespondWebViewData<T> : RespondWebViewDataBase where T : class ,new ()
    {
        public RespondWebViewData() : base(WebViewErrorCode.Success)
        {
        }

        public RespondWebViewData(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }

        public RespondWebViewData(ErrorCodeItem errorCodeItem) : base(errorCodeItem)
        {
        }

        public RespondWebViewData(int errorCode, string errorMessage, T data) : base(errorCode, errorMessage)
        {
            this.rows = data;
        }

        public RespondWebViewData(ErrorCodeItem errorCodeItem, T data) : base(errorCodeItem)
        {
            this.rows = data;
        }

        public RespondWebViewData(T data) : base(WebViewErrorCode.Success.ErrorCode, string.Empty)
        {
            this.rows = data;
        }
        
        public int total { set; get; }

        public T rows { get; set; }

    }
}