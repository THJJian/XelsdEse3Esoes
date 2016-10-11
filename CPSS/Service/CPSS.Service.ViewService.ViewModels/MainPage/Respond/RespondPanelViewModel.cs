using System;
using System.Collections.Generic;

namespace CPSS.Service.ViewService.ViewModels.MainPage.Respond
{
    [Serializable]
    public class RespondPanelViewModel
    {
        /// <summary>
        /// 面板标题
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// Panel的CCS样式ICON，16*16的icon图片
        /// </summary>
        public string IconCls { set; get; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<RespondMenuViewModel> Menus { set; get; }
    }
}