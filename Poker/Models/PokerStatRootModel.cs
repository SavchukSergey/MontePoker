using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Poker.Models {
    public class PokerStatRootModel {

        public PokerStatRootModel() {
            _tableCards.CardA.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardB.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardC.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardD.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardE.PropertyChanged += TableCardsOnPropertyChanged;

            _players.CollectionChanged += PlayersOnCollectionChanged;
        }

        private void PlayersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args) {
            if (args.OldItems != null) {
                foreach (PokerPlayerViewModel player in args.OldItems) {
                    player.CardA.PropertyChanged -= PlayerCardsOnPropertyChanged;
                    player.CardB.PropertyChanged -= PlayerCardsOnPropertyChanged;
                }
            }
            if (args.NewItems != null) {
                foreach (PokerPlayerViewModel player in args.NewItems) {
                    player.CardA.PropertyChanged += PlayerCardsOnPropertyChanged;
                    player.CardB.PropertyChanged += PlayerCardsOnPropertyChanged;
                }
            }
        }

        private void PlayerCardsOnPropertyChanged(object sender, PropertyChangedEventArgs args) {
            switch (args.PropertyName) {
                case "Card":
                    InvalidateState();
                    break;
            }
        }

        private void TableCardsOnPropertyChanged(object sender, PropertyChangedEventArgs args) {
            switch (args.PropertyName) {
                case "Card":
                    InvalidateState();
                    break;
            }
        }

        public bool TryDefaultDestination(PokerCardViewModel card) {
            var destinations = IterateDefaultDestinations();
            var destination = destinations.FirstOrDefault();
            if (destination != null) {
                destination.Card = card.Card;
                destination.Visible = true;
                card.Visible = false;
                return true;
            }
            return false;
        }

        private IEnumerable<PokerCardViewModel> IterateDefaultDestinations() {
            var me = _players.Count > 0 ? _players[0] : null;
            if (me != null) {
                if (me.CardA.IsEmpty) yield return me.CardA;
                if (me.CardB.IsEmpty) yield return me.CardB;
            }
            if (_tableCards.CardA.IsEmpty) yield return _tableCards.CardA;
            if (_tableCards.CardB.IsEmpty) yield return _tableCards.CardB;
            if (_tableCards.CardC.IsEmpty) yield return _tableCards.CardC;
            if (_tableCards.CardD.IsEmpty) yield return _tableCards.CardD;
            if (_tableCards.CardE.IsEmpty) yield return _tableCards.CardE;
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
