namespace Poker.Calculator {
    public struct PokerCalculatorHand {

        public int Count {
            get { return Wins + Losts + Splits; }
        }

        public int Wins { get; set; }

        public int Losts { get; set; }

        public int Splits { get; set; }

    }
}
