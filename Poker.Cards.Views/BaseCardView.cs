using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Poker.Cards.Views {
    public class BaseCardView : Canvas {

        public static readonly DependencyProperty CardBackgroundProperty;

        static BaseCardView()
        {
            CardBackgroundProperty = DependencyProperty.Register("CardBackground", typeof(Brush), typeof(BaseCardView), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255)), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));
        }

        public Brush CardBackground {
            get {
                return (Brush)GetValue(CardBackgroundProperty);
            }
            set {
                SetValue(CardBackgroundProperty, value);
            }
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
        }
    }
}
