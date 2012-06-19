using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Calculator {
    public class PokerCalculatorPlayer {

        public PokerCard CardA;

        public PokerCard CardB;

        public int Wins { get; set; }

        public int Losts { get; set; }

        public int Splits { get; set; }

        public int RoyalFlush { get; set; }

        public int StraightFlush { get; set; }

        public int FourOfKind { get; set; }

        public int FullHouse { get; set; }

        public int Flush { get; set; }

        public int Straight { get; set; }

        public int ThreeOfKind { get; set; }

        public int TwoPairs { get; set; }

        public int OnePair { get; set; }

        public int HighCard { get; set; }

        public void Increase(HandType handType) {
            switch (handType) {
                case HandType.RoyalFlush:
                    RoyalFlush++;
                    break;
                case HandType.StraightFlush:
                    StraightFlush++;
                    break;
                case HandType.FourOfKind:
                    FourOfKind++;
                    break;
                case HandType.FullHouse:
                    FullHouse++;
                    break;
                case HandType.Flush:
                    Flush++;
                    break;
                case HandType.Straight:
                    Straight++;
                    break;
                case HandType.ThreeOfKind:
                    ThreeOfKind++;
                    break;
                case HandType.TwoPairs:
                    TwoPairs++;
                    break;
                case HandType.OnePair:
                    OnePair++;
                    break;
                case HandType.HighCard:
                    HighCard++;
                    break;
            }
        }

        public bool Contains(ref PokerCard card) {
            return CardA == card || CardB == card;
        }
    }
}
