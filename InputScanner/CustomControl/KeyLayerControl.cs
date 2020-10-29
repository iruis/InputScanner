using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InputScanner.CustomControl
{
    public class KeyLayerControl : Control
    {
        static KeyLayerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyLayerControl), new FrameworkPropertyMetadata(typeof(KeyLayerControl)));
            BorderBrushProperty.OverrideMetadata(typeof(KeyLayerControl), new FrameworkPropertyMetadata(Brushes.Gray));
            BorderThicknessProperty.OverrideMetadata(typeof(KeyLayerControl), new FrameworkPropertyMetadata(new Thickness(1.0)));
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(KeyLayerControl), new PropertyMetadata(string.Empty));

        public string Count
        {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(string), typeof(KeyLayerControl), new PropertyMetadata(string.Empty));

        public string Percent
        {
            get { return (string)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        public static readonly DependencyProperty PercentProperty =
            DependencyProperty.Register("Percent", typeof(string), typeof(KeyLayerControl), new PropertyMetadata(string.Empty));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(KeyLayerControl), new FrameworkPropertyMetadata(new CornerRadius(5.0)));
    }
}
