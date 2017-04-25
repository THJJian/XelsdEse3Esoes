namespace CPSS.Common.Core.Mvc.UIControl
{
    public class EasyUIBaseControl
    {
        public EasyUIBaseControl()
        {
            this.LabelPosition = "before";
            this.LabelAlign = "right";
            this.LabelWidth = 80;
            this.Width = 260;
            this.Height = 30;
            this.TabIndex = 0;
            this.Value = string.Empty;
        }

        public string Id { set; get; }
        
        public string LabelName { set; get; }

        public string LabelPosition { set; get; }

        public string LabelAlign { set; get; }

        public int LabelWidth { set; get; }

        public int Width { set; get; }

        public int Height { set; get; }

        public int TabIndex { set; get; }

        public string Value { set; get; }
    }
}