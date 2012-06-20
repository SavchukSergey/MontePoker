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

        private double Percentage(PokerStatRootModel rootModel, double val) {
            var total = rootModel.Statistics.GamesPlayed > 0 ? rootModel.Statistics.GamesPlayed : 1;
            return Math.Round(val / total * 100, 3);
        }

        public void CopyFrom(PokerStatRootModel rootModel, PokerCalculatorPlayer player) {
            _wins.Count = player.Wins;
            _losts.Count = player.Losts;
            _splits.Count = player.Splits;

            _wins.Percentage = Percentage(rootModel, player.Wins);
            _losts.Percentage = Percentage(rootModel, player.Losts);
            _splits.Percentage = Percentage(rootModel, player.Splits);

            var gamesPlayed = rootModel.Statistics.GamesPlayed;

            _royalFlush.Count = player.RoyalFlush;
            _straightFlush.Count = player.StraightFlush;
            _fourOfKind.Count = player.FourOfKind;
            _fullHouse.Count = player.FullHouse;
            _flush.Count = player.Flush;
            _straight.Count = player.Straight;
            _threeOfKind.Count = player.ThreeOfKind;
            _twoPairs.Count = player.TwoPairs;

            _royalFlush.Percentage = Percentage(rootModel, player.RoyalFlush);
            _straightFlush.Percentage = Percentage(rootModel, player.StraightFlush);
            _fourOfKind.Percentage = Percentage(rootModel, player.FourOfKind);
            _fullHouse.Percentage = Percentage(rootModel, player.FullHouse);
            _flush.Percentage = Percentage(rootModel, player.Flush);
            _straight.Percentage = Percentage(rootModel, player.Straight);
            _threeOfKind.Percentage = Percentage(rootModel, player.ThreeOfKind);
            _twoPairs.Percentage = Percentage(rootModel, player.TwoPairs);

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
