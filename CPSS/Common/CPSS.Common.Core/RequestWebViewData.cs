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

        /// <summary>
        /// 是否为重新刷新页面(用户页面内容更新后再查下时需要清除缓存)。如果为0保持不变，其它任意值都将清除之前的缓存
        /// </summary>
        public short reload { set; get; }

        public T data { set; get; }

    }
}