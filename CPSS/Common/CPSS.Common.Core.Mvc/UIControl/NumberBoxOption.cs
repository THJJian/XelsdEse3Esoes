namespace CPSS.Common.Core.Mvc.UIControl
{
    public class NumberBoxOption : EasyUIBaseControl
    {
        public NumberBoxOption()
        {
            this.Min = 0;
            this.Max = int.MaxValue;
            this.Precision = 0;
            this.DecimalSeparator = ".";
            this.GroupSeparator = ",";
            this.Prefix = "￥";
        }

        public int Min { set; get; }

        public int Max { set; get; }

        /// <summary>
        /// 小数精度
        /// </summary>
        public short Precision { set; get; }

        /// <summary>
        /// 精度分隔字符
        /// </summary>
        public string DecimalSeparator { set; get; }

        /// <summary>
        /// 分组分隔字符
        /// </summary>
        public string GroupSeparator { set; get; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { set; get; }
    }
}