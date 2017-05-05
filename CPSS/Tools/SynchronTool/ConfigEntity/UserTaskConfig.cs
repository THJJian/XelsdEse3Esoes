using System;

namespace SynchronTool.ConfigEntity
{
    [Serializable]
    public class UserTaskConfig
    {
        /// <summary>
        /// 延迟分钟数
        /// </summary>
        public short Interval { set; get; }
    }
}