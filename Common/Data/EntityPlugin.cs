namespace Common.Data
{
    public class EntityMenu : Menu
    {
        public int Id { get; set; }
    }

    public class Menu
    {
        public object Icon { get; set; }
        public string Label { get; set; }
        public string ToolTip { get; set; }
    }

    public class Plugin : Menu
    {
        public object Tag { get; set; }
    }
}