namespace CPSS.Common.Core.Mvc.UIControl
{
    public class TextBoxOption : EasyUIBaseControl
    {
        public TextBoxOption()
        {
            this.ButtonText = "…";
            this.Multiline = false;
        }

        public string ButtonText { set; get; }

        public bool Multiline { set; get; }
        
    }
}