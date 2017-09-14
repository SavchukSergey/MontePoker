using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Poker.Calculator.Wpf.Models {
    public class PokerStatRootModel : INotifyPropertyChanged {

        private PokerCalculator _calculator;
        private readonly PokerCardDeckViewModel _cardDeck = new PokerCardDeckViewModel();
        private readonly PokerTableCardsViewModel _tableCards = new PokerTableCardsViewModel();
        private readonly ObservableCollection<PokerPlayerViewModel> _players = new ObservableCollection<PokerPlayerViewModel>();
        private readonly ObservableCollection<PokerPlayerViewModel> _visiblePlayers = new ObservableCollection<PokerPlayerViewModel>();
        private readonly PokerGlobalStatistics _statistics = new PokerGlobalStatistics();
        private bool _dirty = true;

        private readonly object _sync = new object();

        public PokerStatRootModel() {
            _calculator = new PokerCalculatorBuilder().Build();
            _tableCards.CardA.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardB.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardC.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardD.PropertyChanged += TableCardsOnPropertyChanged;
            _tableCards.CardE.PropertyChanged += TableCardsOnPropertyChanged;

            _players.CollectionChanged += OnPlayersCollectionChanged;
        }

        private void OnPlayersCollectionChanged(object sender, NotifyCollectionChangedEventArgs args) {
            if (args.OldItems != null) {
                foreach (PokerPlayerViewModel player in args.OldItems) {
                    player.PropertyChanged -= OnPlayerPropertyChanged;
                    player.CardA.PropertyChanged -= PlayerCardsOnPropertyChanged;
                    player.CardB.PropertyChanged -= PlayerCardsOnPropertyChanged;
                }
            }
            if (args.NewItems != null) {
                foreach (PokerPlayerViewModel player in args.NewItems) {
                    player.PropertyChanged += OnPlayerPropertyChanged;
                    player.CardA.PropertyChanged += PlayerCardsOnPropertyChanged;
                    player.CardB.PropertyChanged += PlayerCardsOnPropertyChanged;
                }
            }
            _dirty = true;
            RefreshVisiblePlayers();
            OnPropertyChanged("Is2Players");
            OnPropertyChanged("Is3Players");
            OnPropertyChanged("Is4Players");
            OnPropertyChanged("Is5Players");
            OnPropertyChanged("Is6Players");
        }

        private void OnPlayerPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            switch (propertyChangedEventArgs.PropertyName) {
                case "InGame":
                    _dirty = true;
                    Sync();
                    break;
            }
            RefreshVisiblePlayers();
        }

        private void PlayerCardsOnPropertyChanged(object sender, PropertyChangedEventArgs args) {
            switch (args.PropertyName) {
                case "Card":
                    _dirty = true;
                    Sync();
                    break;
            }
            RefreshVisiblePlayers();
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

        public ObservableCollection<PokerPlayerViewModel> VisiblePlayers {
            get { return _visiblePlayers; }
        }

        public PokerGlobalStatistics Statistics {
            get { return _statistics; }
        }

        public void ReturnCardToDeck(PokerCardViewModel card) {
            _cardDeck.ShowCard(card.Rank, card.Suit);
            card.Empty();
        }

        public void NewHand() {
            _tableCards.ReturnCardsTo(this);
            foreach (var player in _players) {
                player.ReturnCardsTo(this);
                player.InGame = true;
            }
        }

        public void Sync() {
            if (_dirty) {
                lock (_sync) {
                    var builder = new PokerCalculatorBuilder();

                    _dirty = false;
                    foreach (var player in _players) {
                        if (player.InGame) {
                            builder.AddPlayer(player.CardA.Card, player.CardB.Card);
                        }
                    }
                    builder.AddTableCard(_tableCards.CardA.Card);
                    builder.AddTableCard(_tableCards.CardB.Card);
                    builder.AddTableCard(_tableCards.CardC.Card);
                    builder.AddTableCard(_tableCards.CardD.Card);
                    builder.AddTableCard(_tableCards.CardE.Card);
                    _calculator = builder.Build();
                }
            }
            var info = _calculator.GetResult();
            _statistics.GamesPlayed = info.GamesPlayed;
            var calcIndex = 0;
            for (int i = 0; i < _players.Count; i++) {
                var playerModel = _players[i];
                if (playerModel.InGame) {
                    var player = info.Players[calcIndex];
                    playerModel.Statistics.CopyFrom(this, player);
                    calcIndex++;
                }
            }
        }

        public void PlayGame() {
            lock (_sync) {
                _calculator.PlayGame();
            }
        }

        public bool Is2Players {
            get { return _players.Count == 2; }
        }
        
        public bool Is3Players {
            get { return _players.Count == 3; }
        }

        public bool Is4Players {
            get { return _players.Count == 4; }
        }

        public bool Is5Players {
            get { return _players.Count == 5; }
        }

        public bool Is6Players {
            get { return _players.Count == 6; }
        }

        public void SetPlayers(int count) {
            while (_players.Count > count) {
                _players.RemoveAt(count);
            }
            while (_players.Count < count) {
                var player = new PokerPlayerViewModel {
                    Name = "Player " + (_players.Count + 1).ToString()
                };
                _players.Add(player);
            }
        }

        private void RefreshVisiblePlayers() {
            var foundEmpty = false;
            foreach (var player in _players) {
                player.Visible = !foundEmpty;
                var hasCards = player.HasAnyCard;
                if (!hasCards) foundEmpty = true;
            }
            _visiblePlayers.Clear();
            foreach (var player in _players) {
                if (player.Visible) _visiblePlayers.Add(player);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
