namespace Poker.Calculator {
    public class PokerCalculatorHand {

        public int Count {
            get { return Wins + Losts + Splits; }
        }

        public int Wins { get; set; }

        public int Losts { get; set; }

        public int Splits { get; set; }

        public double Scores {
            get { return Wins + Splits * 0.5; }
        }

        public double WinsPercentage {
            get {
                var cnt = Count;
                if (cnt > 0) return Round((double)Wins / cnt);
                return 0;
            }
        }

        public double LostsPercentage {
            get {
                var cnt = Count;
                if (cnt > 0) return Round((double)Losts / cnt);
                return 0;
            }
        }

        public double SplitsPercentage {
            get {
                var cnt = Count;
                if (cnt > 0) return Round((double)Splits / cnt);
                return 0;
            }
        }

        public double ScoresPercentage {
            get {
                var max = Count;
                if (max > 0) return Round(Scores / max);
                return 0;
            }
        }

        private double Round(double val) {
            return val;
        }

        public PokerCalculatorHand Clone() {
            return new PokerCalculatorHand {
                Wins = Wins,
                Splits = Splits,
                Losts = Losts
            };
        }
    }
}
