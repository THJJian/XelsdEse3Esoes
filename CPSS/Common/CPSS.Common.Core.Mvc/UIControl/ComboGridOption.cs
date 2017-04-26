using System.Collections.Generic;
namespace CPSS.Common.Core.Mvc.UIControl
{
    public class ComboGridOption : EasyUIBaseControl
    {
        public ComboGridOption()
        {
            this.Method = "post";
            this.Mode = "remote";
            this.FitColumns = true;
            this.SelectPanelWidth = 500;
            this.SelectPanelHeight = 300;
        }

        public string IdField { set; get; }

        public string TextField { set; get; }

        public string Url { set; get; }

        public string Method { set; get; }

        public List<ComboGridColumnOption> GridColumns { set; get; }

        public bool FitColumns { set; get; }

        public int SelectPanelHeight { set; get; }

        public int SelectPanelWidth { set; get; }

        public string Mode { get; set; }
    }
}