using System;

namespace CPSS.Common.Core
{
    [Serializable]
    public class RequestWebViewData<T> where T: class, new()
    {
        public int page { set; get; }

        public int rows { set; get; }

        public T data { set; get; }

    }
}