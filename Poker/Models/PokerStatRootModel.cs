using System.Collections.ObjectModel;

namespace Poker.Models {
    public class PokerStatRootModel {

        private readonly PokerCardDeckViewModel _cardDeck = new PokerCardDeckViewModel();
        public PokerCardDeckViewModel CardDeck {
            get { return _cardDeck; }
        }

        private readonly PokerTableCardsViewModel _tableCards = new PokerTableCardsViewModel();
        public PokerTableCardsViewModel TableCards {
            get { return _tableCards; }
        }

        private readonly ObservableCollection<PokerPlayerViewModel> _players = new ObservableCollection<PokerPlayerViewModel>();
        public ObservableCollection<PokerPlayerViewModel> Players {
            get { return _players; }
        }
    }
}
