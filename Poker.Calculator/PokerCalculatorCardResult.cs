using Poker.Cards;

namespace Poker.Calculator {
    public class PokerCalculatorCardResult : PokerCalculatorHand {

        public PokerCard Card { get; set; }

        public new PokerCalculatorCardResult Clone() {
            return new PokerCalculatorCardResult {
                Wins = Wins,
                Splits = Splits,
                Losts = Losts,
                Card = Card
            };
        }

        public override string ToString() {
            return Card.ToString() + " " + ScoresPercentage;
        }
    }
}
