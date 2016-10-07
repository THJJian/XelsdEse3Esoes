namespace CPSS.Data.DataAcess.DataModels.HeadButtons
{
    public class HeadButtonsDataModel
    {
        /// <summary>
        /// button显示文本
        /// </summary>
        public string ButtonText { set; get; }

        /// <summary>
        /// buttonID
        /// </summary>
        public string ButtonName { set; get; }

        /// <summary>
        /// buttong是否不可用
        /// </summary>
        public bool ButtonDisabled { set; get; }

        /// <summary>
        /// button按钮的Icon图片样式
        /// </summary>
        public string ButtonIconCls { set; get; }
    }
}