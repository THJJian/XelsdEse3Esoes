using System.Collections.Generic;
namespace CPSS.Common.Core.Mvc.UIControl
{
    public class ComboGridOption : EasyUIBaseControl
    {
        public string IdField { set; get; }

        public string TextField { set; get; }

        public string Url { set; get; }

        public string Method { set; get; }

        public List<ComboGridColumnOption> GridColumns { set; get; }

        public bool FitColumns { set; get; }

        public int SelectPanelHeight { set; get; }

        public int SelectPanelWidth { set; get; }
    }
}