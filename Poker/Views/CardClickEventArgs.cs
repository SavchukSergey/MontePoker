using System.Windows;

namespace Poker.Views {
    public class CardClickEventArgs : RoutedEventArgs {
        private readonly PokerCardView _cardView;

        public CardClickEventArgs(RoutedEvent @event, PokerCardView cardView)
            : base(@event) {
            _cardView = cardView;
        }

        public PokerCardView CardView {
            get { return _cardView; }
        }
    }

    public delegate void CardEventHandler(object sender, CardClickEventArgs args);

}
