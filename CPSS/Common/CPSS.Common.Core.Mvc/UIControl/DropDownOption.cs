using System.Collections.Generic;
namespace CPSS.Common.Core.Mvc.UIControl
{
    public class DropDownOption : EasyUIBaseControl
    {
        public int SelectPanelHeight { set; get; }

        public List<DropDownSelectItem> SelectItems { set; get; }
    }

    public class DropDownSelectItem
    {
        public string Text { set; get; }

        public string Value { set; get; }
    }
}