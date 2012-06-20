using System;
using System.ComponentModel;

namespace Poker.Calculator.Wpf.Models {
    public class PokerPlayerStatisticsViewModel : INotifyPropertyChanged {

        private readonly PokerStatisticsItem _wins = new PokerStatisticsItem();
        public PokerStatisticsItem Wins {
            get { return _wins; }
        }

        private readonly PokerStatisticsItem _losts = new PokerStatisticsItem();
        public PokerStatisticsItem Losts {
            get { return _losts; }
        }

        private readonly PokerStatisticsItem _splits = new PokerStatisticsItem();
        public PokerStatisticsItem Splits {
            get { return _splits; }
        }

        private readonly PokerStatisticsHandItem _royalFlush = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem RoyalFlush {
            get { return _royalFlush; }
        }

        private readonly PokerStatisticsHandItem _straightFlush = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem StraightFlush {
            get { return _straightFlush; }
        }

        private readonly PokerStatisticsHandItem _fourOfKind = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem FourOfKind {
            get { return _fourOfKind; }
        }

        private readonly PokerStatisticsHandItem _fullHouse = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem FullHouse {
            get { return _fullHouse; }
        }

        private readonly PokerStatisticsHandItem _flush = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem Flush {
            get { return _flush; }
        }

        private readonly PokerStatisticsHandItem _straight = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem Straight {
            get { return _straight; }
        }

        private readonly PokerStatisticsHandItem _threeOfKind = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem ThreeOfKind {
            get { return _threeOfKind; }
        }

        private readonly PokerStatisticsHandItem _twoPairs = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem TwoPairs {
            get { return _twoPairs; }
        }

        private readonly PokerStatisticsHandItem _onePair = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem OnePair {
            get { return _onePair; }
        }

        private readonly PokerStatisticsHandItem _highCard = new PokerStatisticsHandItem();
        public PokerStatisticsHandItem HighCard {
            get { return _highCard; }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(PokerStatRootModel rootModel, PokerCalculatorPlayer player) {
            var gamesPlayed = rootModel.Statistics.GamesPlayed;

            _wins.SetValue(player.Wins, gamesPlayed);
            _splits.SetValue(player.Splits, gamesPlayed);
            _losts.SetValue(player.Losts, gamesPlayed);

            _royalFlush.CopyFrom(player.RoyalFlush, gamesPlayed);
            _straightFlush.CopyFrom(player.StraightFlush, gamesPlayed);
            _fourOfKind.CopyFrom(player.FourOfKind, gamesPlayed);
            _fullHouse.CopyFrom(player.FullHouse, gamesPlayed);
            _flush.CopyFrom(player.Flush, gamesPlayed);
            _straight.CopyFrom(player.Straight, gamesPlayed);
            _threeOfKind.CopyFrom(player.ThreeOfKind, gamesPlayed);
            _twoPairs.CopyFrom(player.TwoPairs, gamesPlayed);
            _onePair.CopyFrom(player.OnePair, gamesPlayed);
            _highCard.CopyFrom(player.HighCard, gamesPlayed);
        }


        public void Reset() {
            _wins.Reset();
            _losts.Reset();
            _splits.Reset();

            _royalFlush.Reset();
            _straightFlush.Reset();
            _fourOfKind.Reset();
            _fullHouse.Reset();
            _flush.Reset();
            _straight.Reset();
            _threeOfKind.Reset();
            _twoPairs.Reset();
            _onePair.Reset();
            _highCard.Reset();
        }
    }
}
