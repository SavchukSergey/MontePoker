using System.Windows;
using System.Windows.Controls;

namespace Poker.Calculator.Wpf.Views {
    public class CountedLabel : Label {

        public static readonly DependencyProperty CountProperty;

        static CountedLabel() {
            CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(CountedLabel), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender, OnCountChanged));
        }

        private static void OnCountChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            ((CountedLabel)dependencyObject).OnCountChanged(args);
        }

        protected virtual void OnCountChanged(DependencyPropertyChangedEventArgs args) {
            InvalidateContent();
        }

        private string _label;
        public string Label {
            get { return _label; }
            set {
                _label = value;
                InvalidateContent();
            }
        }

        public int Count {
            get {
                return (int)GetValue(CountProperty);
            }
            set {
                SetValue(CountProperty, value);
            }
        }

        private void InvalidateContent() {
            var hand = Label ?? "";
            var cnt = Count;

            Content = hand + " (" + cnt + ")";
        }
    }
}
