using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Poker.Statistics;

namespace Poker.Models {
    public class PokerStatRootModel : INotifyPropertyChanged {

        private readonly PokerCalculator _calculator = new PokerCalculator();
        private readonly PokerCardDeckViewModel _cardDeck = new PokerCardDeckViewModel();
        private readonly PokerTableCardsViewModel _tableCards = new PokerTableCardsViewModel();
        private readonly ObservableCollection<PokerPlayerViewModel> _players = new ObservableCollection<PokerPlayerViewModel>();
        private readonly object _sync = new object();

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
                    _dirty = true;
                    Sync();
                    break;
            }
        }

        private void TableCardsOnPropertyChanged(object sender, PropertyChangedEventArgs args) {
            switch (args.PropertyName) {
                case "Card":
                    _dirty = true;
                    Sync();
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

        public PokerCardDeckViewModel CardDeck {
            get { return _cardDeck; }
        }

        public PokerTableCardsViewModel TableCards {
            get { return _tableCards; }
        }

        public ObservableCollection<PokerPlayerViewModel> Players {
            get { return _players; }
        }

        private bool _dirty = true;
        public bool Dirty {
            get { return _dirty; }
        }

        private int _gamesPlayed;
        public int GamesPlayed {
            get { return _gamesPlayed; }
            set {
                if (_gamesPlayed != value) {
                    _gamesPlayed = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("GamesPlayed"));
                }
            }
        }

        public void Sync() {
            if (_dirty) {
                lock (_sync) {
                    _calculator.Reset();
                    _dirty = false;
                    foreach (var player in _players) {
                        var calcPlayer = new PokerCalculatorPlayer { CardA = player.CardA.Card, CardB = player.CardB.Card };
                        _calculator.Players.Add(calcPlayer);
                    }
                    _calculator.Table.CardA = _tableCards.CardA.Card;
                    _calculator.Table.CardB = _tableCards.CardB.Card;
                    _calculator.Table.CardC = _tableCards.CardC.Card;
                    _calculator.Table.CardD = _tableCards.CardD.Card;
                    _calculator.Table.CardE = _tableCards.CardE.Card;
                }
            }
            for (int i = 0; i < _players.Count; i++) {
                var playerModel = _players[i];
                var player = _calculator.Players[i];
                playerModel.Statistics.CopyFrom(player);
            }
        }

        public void PlayGame() {
            lock (_sync) {
                _calculator.PlayGame();
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
