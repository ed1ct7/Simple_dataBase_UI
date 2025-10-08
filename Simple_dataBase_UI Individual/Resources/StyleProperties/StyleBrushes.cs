using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Simple_dataBase_UI_Individual.Resources.StyleProperties
{
    public class StyleBrushes
    {
        public static Brush BGB_Default = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3f284d")); //
        public static Brush FGB_Default = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF")); //
        public static Brush BGB_Hovered = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3f284d")); //
        public static Brush FGB_Hovered = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3f284d")); //
        public static Brush BGB_Clicked = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3f284d")); //
        public static Brush FGB_Clicked = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3f284d")); //
    }
}
