using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Poker.Models {
    public class PokerStatRootModel {

        public PokerStatRootModel() {
            _tableCards.CardA.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardB.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardC.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardD.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardE.PropertyChanged += TableCardsOnPropertyChanged;
        }

        private void TableCardsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            switch (propertyChangedEventArgs.PropertyName) {
                case "Card":
                    InvalidateState();
                    break;
            }
        }

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

        private bool _dirty = true;
        public bool Dirty {
            get { return _dirty; }
        }

        public void ResetDirty() {
            _dirty = false;
        }

        public void InvalidateState() {
            _dirty = true;
        }

    }
}
