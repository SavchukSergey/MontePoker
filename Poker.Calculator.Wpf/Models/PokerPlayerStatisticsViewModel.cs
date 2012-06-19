using System;
using System.ComponentModel;
using Poker.Statistics;

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

        private readonly PokerStatisticsItem _royalFlush = new PokerStatisticsItem();
        public PokerStatisticsItem RoyalFlush {
            get { return _royalFlush; }
        }

        private readonly PokerStatisticsItem _straightFlush = new PokerStatisticsItem();
        public PokerStatisticsItem StraightFlush {
            get { return _straightFlush; }
        }

        private readonly PokerStatisticsItem _fourOfKind = new PokerStatisticsItem();
        public PokerStatisticsItem FourOfKind {
            get { return _fourOfKind; }
        }

        private readonly PokerStatisticsItem _fullHouse = new PokerStatisticsItem();
        public PokerStatisticsItem FullHouse {
            get { return _fullHouse; }
        }

        private readonly PokerStatisticsItem _flush = new PokerStatisticsItem();
        public PokerStatisticsItem Flush {
            get { return _flush; }
        }

        private readonly PokerStatisticsItem _straight = new PokerStatisticsItem();
        public PokerStatisticsItem Straight {
            get { return _straight; }
        }

        private readonly PokerStatisticsItem _threeOfKind = new PokerStatisticsItem();
        public PokerStatisticsItem ThreeOfKind {
            get { return _threeOfKind; }
        }

        private readonly PokerStatisticsItem _twoPairs = new PokerStatisticsItem();
        public PokerStatisticsItem TwoPairs {
            get { return _twoPairs; }
        }

        private readonly PokerStatisticsItem _onePair = new PokerStatisticsItem();
        public PokerStatisticsItem OnePair {
            get { return _onePair; }
        }

        private readonly PokerStatisticsItem _highCard = new PokerStatisticsItem();
        public PokerStatisticsItem HighCard {
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

            _royalFlush.Count = player.RoyalFlush;
            _straightFlush.Count = player.StraightFlush;
            _fourOfKind.Count = player.FourOfKind;
            _fullHouse.Count = player.FullHouse;
            _flush.Count = player.Flush;
            _straight.Count = player.Straight;
            _threeOfKind.Count = player.ThreeOfKind;
            _twoPairs.Count = player.TwoPairs;
            _onePair.Count = player.OnePair;
            _highCard.Count = player.HighCard;

            _royalFlush.Percentage = Percentage(rootModel, player.RoyalFlush);
            _straightFlush.Percentage = Percentage(rootModel, player.StraightFlush);
            _fourOfKind.Percentage = Percentage(rootModel, player.FourOfKind);
            _fullHouse.Percentage = Percentage(rootModel, player.FullHouse);
            _flush.Percentage = Percentage(rootModel, player.Flush);
            _straight.Percentage = Percentage(rootModel, player.Straight);
            _threeOfKind.Percentage = Percentage(rootModel, player.ThreeOfKind);
            _twoPairs.Percentage = Percentage(rootModel, player.TwoPairs);
            _onePair.Percentage = Percentage(rootModel, player.OnePair);
            _highCard.Percentage = Percentage(rootModel, player.HighCard);
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
