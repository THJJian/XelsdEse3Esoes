using System;

namespace CPSS.Common.Core.Exception
{
    /// <summary>
    /// 请求返回数据封装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class RespondWebViewData<T> : RespondWebViewDataBase where T : class ,new ()
    {
        public RespondWebViewData(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }

        public RespondWebViewData(ErrorCodeItem errorCodeItem) : base(errorCodeItem)
        {
        }

        public RespondWebViewData(int errorCode, string errorMessage, T data) : base(errorCode, errorMessage)
        {
            this.Data = data;
        }

        public RespondWebViewData(ErrorCodeItem errorCodeItem, T data) : base(errorCodeItem)
        {
            this.Data = data;
        }

        public RespondWebViewData(T data) : base(WebViewErrorCode.Success.ErrorCode, string.Empty)
        {
            this.Data = data;
        }

        public T Data { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public object RequestWebViewData { set; get; }

    }
}