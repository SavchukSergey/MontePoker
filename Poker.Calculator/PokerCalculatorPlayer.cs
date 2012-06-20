using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Calculator {
    public class PokerCalculatorPlayer {

        public PokerCard CardA;

        public PokerCard CardB;

        public int Wins { get; set; }

        public int Losts { get; set; }

        public int Splits { get; set; }

        private int _royalFlush;
        public int RoyalFlush {
            get { return _royalFlush; }
        }

        private int _straightFlush;
        public int StraightFlush {
            get { return _straightFlush; }
        }

        private int _fourOfKind;
        public int FourOfKind {
            get { return _fourOfKind; }
        }

        private int _fullHouse;
        public int FullHouse {
            get { return _fullHouse; }
        }

        private int _flush;
        public int Flush {
            get { return _flush; }
        }

        private PokerCalculatorHand _straight;
        public PokerCalculatorHand Straight {
            get { return _straight; }
        }

        private PokerCalculatorHand _threeOfKind;
        public PokerCalculatorHand ThreeOfKind {
            get { return _threeOfKind; }
        }

        private PokerCalculatorHand _twoPairs;
        public PokerCalculatorHand TwoPairs {
            get { return _twoPairs; }
        }

        private PokerCalculatorHand _onePair;
        public PokerCalculatorHand OnePair {
            get { return _onePair; }
        }

        private PokerCalculatorHand _highCard;
        public PokerCalculatorHand HighCard {
            get { return _highCard; }
        }

        public void IncreaseWins(HandType handType) {
            Wins++;
            switch (handType) {
                case HandType.RoyalFlush:
                    _royalFlush++;
                    break;
                case HandType.StraightFlush:
                    _straightFlush++;
                    break;
                case HandType.FourOfKind:
                    _fourOfKind++;
                    break;
                case HandType.FullHouse:
                    _fullHouse++;
                    break;
                case HandType.Flush:
                    _flush++;
                    break;
                case HandType.Straight:
                    _straight.Wins++;
                    break;
                case HandType.ThreeOfKind:
                    _threeOfKind.Wins++;
                    break;
                case HandType.TwoPairs:
                    _twoPairs.Wins++;
                    break;
                case HandType.OnePair:
                    _onePair.Wins++;
                    break;
                case HandType.HighCard:
                    _highCard.Wins++;
                    break;
            }
        }

        public void IncreaseSplits(HandType handType) {
            Splits++;
            switch (handType) {
                case HandType.RoyalFlush:
                    _royalFlush++;
                    break;
                case HandType.StraightFlush:
                    _straightFlush++;
                    break;
                case HandType.FourOfKind:
                    _fourOfKind++;
                    break;
                case HandType.FullHouse:
                    _fullHouse++;
                    break;
                case HandType.Flush:
                    _flush++;
                    break;
                case HandType.Straight:
                    _straight.Splits++;
                    break;
                case HandType.ThreeOfKind:
                    _threeOfKind.Splits++;
                    break;
                case HandType.TwoPairs:
                    _twoPairs.Splits++;
                    break;
                case HandType.OnePair:
                    _onePair.Splits++;
                    break;
                case HandType.HighCard:
                    _highCard.Splits++;
                    break;
            }
        }

        public void IncreaseLosts(HandType handType) {
            Losts++;
            switch (handType) {
                case HandType.RoyalFlush:
                    _royalFlush++;
                    break;
                case HandType.StraightFlush:
                    _straightFlush++;
                    break;
                case HandType.FourOfKind:
                    _fourOfKind++;
                    break;
                case HandType.FullHouse:
                    _fullHouse++;
                    break;
                case HandType.Flush:
                    _flush++;
                    break;
                case HandType.Straight:
                    _straight.Losts++;
                    break;
                case HandType.ThreeOfKind:
                    _threeOfKind.Losts++;
                    break;
                case HandType.TwoPairs:
                    _twoPairs.Losts++;
                    break;
                case HandType.OnePair:
                    _onePair.Losts++;
                    break;
                case HandType.HighCard:
                    _highCard.Losts++;
                    break;
            }
        }


        public bool Contains(ref PokerCard card) {
            return CardA == card || CardB == card;
        }

        public PokerCalculatorPlayer Clone() {
            return new PokerCalculatorPlayer {
                CardA = CardA,
                CardB = CardB,
                Wins = Wins,
                Losts = Losts,
                Splits = Splits,

                _royalFlush = _royalFlush,
                _straightFlush = _straightFlush,
                _fourOfKind = _fourOfKind,
                _fullHouse = _fullHouse,
                _flush = _flush,
                _straight = _straight,
                _threeOfKind = _threeOfKind,
                _twoPairs = _twoPairs,
                _onePair = _onePair,
                _highCard = _highCard
            };
        }
    }
}
