using System.Collections.Generic;

namespace Controls.Entity
{
    public enum LayoutDirection
    { 
        LeftToRight = 0,
        RightToLeft = 1,
    }

    public class ButtonItem
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public bool Visible { get; set; }
    }

    public class ButtonGroup
    {
        public string Id { get; set; }
        public LayoutDirection Direction { get; set; }
        public List<ButtonItem> Buttons { get; set; }
    }
}
