using System;

namespace Poker.Views {
    public class CardClickEventArgs : EventArgs {
        private readonly PokerCardView _cardView;

        public CardClickEventArgs(PokerCardView cardView) {
            _cardView = cardView;
        }

        public PokerCardView CardView {
            get { return _cardView; }
        }
    }
}
