namespace CPSS.Common.Core.Mvc.UIControl
{
    public class ComboGridColumnOption
    {
        public ComboGridColumnOption()
        {
            this.width = 120;
            this.hidden = false;
            this.align = "right";
        }

        public string field { set; get; }

        public string title { set; get; }

        public int width { set; get; }

        public bool hidden { set; get; }

        public string align { set; get; }

    }
}