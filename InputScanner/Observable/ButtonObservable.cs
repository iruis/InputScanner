using System.Windows;

namespace InputScanner.Observable
{
    public class ButtonObservable : DependencyObject
    {
        public long KeyCode
        {
            get { return (long)GetValue(KeyCodeProperty); }
            set { SetValue(KeyCodeProperty, value); }
        }

        public static readonly DependencyProperty KeyCodeProperty =
            DependencyProperty.Register("KeyCode", typeof(long), typeof(ButtonObservable), new PropertyMetadata(0L));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(ButtonObservable), new PropertyMetadata(string.Empty));

        public int Top
        {
            get { return (int)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(int), typeof(ButtonObservable), new PropertyMetadata(0));

        public int Left
        {
            get { return (int)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(int), typeof(ButtonObservable), new PropertyMetadata(0));

        public int Width
        {
            get { return (int)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(int), typeof(ButtonObservable), new PropertyMetadata(0));

        public int Height
        {
            get { return (int)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(int), typeof(ButtonObservable), new PropertyMetadata(0));

        public long Count
        {
            get { return (long)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(long), typeof(ButtonObservable), new PropertyMetadata(0L));

        public double? Percent
        {
            get { return (double?)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        public static readonly DependencyProperty PercentProperty =
            DependencyProperty.Register("Percent", typeof(double?), typeof(ButtonObservable), new PropertyMetadata(null));
    }
}
