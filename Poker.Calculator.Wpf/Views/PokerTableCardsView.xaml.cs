using System.Windows;
using System.Windows.Controls;

namespace Poker.Calculator.Wpf.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerTableCardsView : UserControl {

        public static readonly RoutedEvent CardClickEvent = EventManager.RegisterRoutedEvent("CardClick", RoutingStrategy.Direct, typeof(CardEventHandler), typeof(PokerTableCardsView));

        public PokerTableCardsView() {
            InitializeComponent();
        }

        protected void OnCardClick(object sender, CardClickEventArgs e) {
            var view = (PokerCardView)sender;
            RaiseEvent(new CardClickEventArgs(CardClickEvent, view));
        }

        public event CardEventHandler CardClick {
            add { AddHandler(CardClickEvent, value); }
            remove { RemoveHandler(CardClickEvent, value); }
        }

    }
}
