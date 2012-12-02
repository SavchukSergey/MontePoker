using System.Windows;
using System.Windows.Controls;

namespace Poker.Calculator.Wpf.Views {
    public class PlayerHandType : Label {

        public static readonly DependencyProperty CountProperty;

        static PlayerHandType() {
            CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(PlayerHandType), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender, OnCountChanged));
        }

        private static void OnCountChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            ((PlayerHandType)dependencyObject).OnCountChanged(args);
        }

        protected virtual void OnCountChanged(DependencyPropertyChangedEventArgs args) {
            InvalidateContent();
        }


        private string _handName;
        public string HandName {
            get { return _handName; }
            set {
                _handName = value;
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
            var hand = HandName ?? "";
            var cnt = Count;

            Content = hand + " (" + cnt + ")";
        }
    }
}
