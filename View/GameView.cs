using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Wpf_2048
{
    public class TextToBackGroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "2": return Brushes.GhostWhite;
                case "4": return Brushes.LightGreen;
                case "8": return Brushes.YellowGreen;
                case "16": return Brushes.LightSeaGreen;
                case "32": return Brushes.Gold;
                case "64": return Brushes.DarkOrange;
                case "128": return Brushes.Tomato;
                case "256": return Brushes.OrangeRed;
                case "512": return Brushes.LightBlue;
                case "1024": return Brushes.DeepSkyBlue;
                case "2048": return Brushes.SlateBlue;
                default: return Brushes.DarkGray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
    public partial class GameView
    {
        TextToBackGroundConverter textToBackGround;

        public GameView()
        {
            textToBackGround = new TextToBackGroundConverter();
        }
        public void AddElements(UniformGrid parent)
        {
            for (int i = 0; i < parent.Columns * parent.Rows; i++)
            {
                Border border = new Border()
                {
                    Background = Brushes.LightGray
                    ,
                    Margin = new Thickness(3)
                    ,
                    CornerRadius = new CornerRadius(8)
                };

                TextBlock textBlock = new TextBlock()
                {
                    FontSize = 30
                    ,
                    HorizontalAlignment = HorizontalAlignment.Center
                    ,
                    VerticalAlignment = VerticalAlignment.Center
                    ,
                    Name = $"TextBlock{i}"
                };

                Binding binding = new Binding();
                binding.Path = new PropertyPath($"observableCollection[{i}]");
                textBlock.SetBinding(TextBlock.TextProperty, binding);

                binding = new Binding();
                binding.Path = new PropertyPath($"observableCollection[{i}]");
                binding.Converter = textToBackGround;
                border.SetBinding(Border.BackgroundProperty, binding);

                border.Child = textBlock;
                parent.Children.Add(border);
            }
        }
    }
}

