using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UI.Home.Data.DesignTime
{
    public class CardViewModelDesignTime
    {
        public CardViewModelDesignTime()
        {
            Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Spark;component/spark.png")) };
                    Label = "СПАРК";
            ToolTip = "Поиск предприятий в базе СПАРК";
        }
        public object Icon { get; set; }
        public string Label { get; set; }
        public string ToolTip { get; set; }
    }
}