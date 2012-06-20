namespace Poker.Calculator.Wpf.Models {
    public class PokerStatisticsHandItem : PokerStatisticsItem {

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

        public void CopyFrom(PokerCalculatorHand hand, int gamesPlayed) {
            SetValue(hand.Count, gamesPlayed);
            _wins.SetValue(hand.Wins, hand.Count);
            _splits.SetValue(hand.Splits, hand.Count);
            _losts.SetValue(hand.Losts, hand.Count);
        }
    }
}
