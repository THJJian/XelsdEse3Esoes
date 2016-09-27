﻿namespace CPSS.Service.ViewService.ViewModels.MainPage.Respond
{
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
    }
}