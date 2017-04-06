using System;

namespace CPSS.Common.Core
{
    [Serializable]
    public class RequestWebViewData<T> where T: class, new()
    {
        public RequestWebViewData()
        {
            this.page = 1;
            this.rows = 1;
        }

        /// <summary>
        /// pageindex
        /// </summary>
        public int page { set; get; }

        /// <summary>
        /// pagesize
        /// </summary>
        public int rows { set; get; }

        public T data { set; get; }

    }
}