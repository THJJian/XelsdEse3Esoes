using System;

namespace CPSS.Service.ViewService.ViewModels.MainPage.Respond
{
    [Serializable]
    public class RespondMenuViewModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 菜单的CCS样式ICON，暂定16*16的icon图片
        /// </summary>
        public string IconCls { set; get; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// menuid
        /// </summary>
        public int MenuID { set; get; }

        /// <summary>
        /// 按钮ID
        /// </summary>
        public string ButtonID { set; get; }

        /// <summary>
        /// 分级ID
        /// </summary>
        public string ClassID { get; set; }
    }
}