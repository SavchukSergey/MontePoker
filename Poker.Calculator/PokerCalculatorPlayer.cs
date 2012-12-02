using System.Collections.Generic;
using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Calculator {
    public class PokerCalculatorPlayer {

        public PokerCard CardA;

        public PokerCard CardB;

        public int Wins { get; set; }

        public int Losts { get; set; }

        public int Splits { get; set; }

        private PokerCalculatorHand _royalFlush = new PokerCalculatorHand();
        public PokerCalculatorHand RoyalFlush {
            get { return _royalFlush; }
        }

        private PokerCalculatorHand _straightFlush = new PokerCalculatorHand();
        public PokerCalculatorHand StraightFlush {
            get { return _straightFlush; }
        }

        private PokerCalculatorHand _fourOfKind = new PokerCalculatorHand();
        public PokerCalculatorHand FourOfKind {
            get { return _fourOfKind; }
        }

        private PokerCalculatorHand _fullHouse = new PokerCalculatorHand();
        public PokerCalculatorHand FullHouse {
            get { return _fullHouse; }
        }

        private PokerCalculatorHand _flush = new PokerCalculatorHand();
        public PokerCalculatorHand Flush {
            get { return _flush; }
        }

        private PokerCalculatorHand _straight = new PokerCalculatorHand();
        public PokerCalculatorHand Straight {
            get { return _straight; }
        }

        private PokerCalculatorHand _threeOfKind = new PokerCalculatorHand();
        public PokerCalculatorHand ThreeOfKind {
            get { return _threeOfKind; }
        }

        private PokerCalculatorHand _twoPairs = new PokerCalculatorHand();
        public PokerCalculatorHand TwoPairs {
            get { return _twoPairs; }
        }

        private PokerCalculatorHand _onePair = new PokerCalculatorHand();
        public PokerCalculatorHand OnePair {
            get { return _onePair; }
        }

        private PokerCalculatorHand _highCard = new PokerCalculatorHand();
        public PokerCalculatorHand HighCard {
            get { return _highCard; }
        }

        private PokerCalculatorCardsStat _cards = new PokerCalculatorCardsStat();
        public PokerCalculatorCardsStat Cards {
            get { return _cards; }
        }

        public ulong OpenedCardsMask { get; set; }

        public IList<PokerCalculatorCardResult> GetOutCards() {
            return Cards.GetOutCards(OpenedCardsMask);
        }

        public void IncreaseWins(HandType handType) {
            Wins++;
            switch (handType) {
                case HandType.RoyalFlush:
                    _royalFlush.Wins++;
                    break;
                case HandType.StraightFlush:
                    _straightFlush.Wins++;
                    break;
                case HandType.FourOfKind:
                    _fourOfKind.Wins++;
                    break;
                case HandType.FullHouse:
                    _fullHouse.Wins++;
                    break;
                case HandType.Flush:
                    _flush.Wins++;
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
                    _royalFlush.Splits++;
                    break;
                case HandType.StraightFlush:
                    _straightFlush.Splits++;
                    break;
                case HandType.FourOfKind:
                    _fourOfKind.Splits++;
                    break;
                case HandType.FullHouse:
                    _fullHouse.Splits++;
                    break;
                case HandType.Flush:
                    _flush.Splits++;
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
                    _royalFlush.Losts++;
                    break;
                case HandType.StraightFlush:
                    _straightFlush.Losts++;
                    break;
                case HandType.FourOfKind:
                    _fourOfKind.Losts++;
                    break;
                case HandType.FullHouse:
                    _fullHouse.Losts++;
                    break;
                case HandType.Flush:
                    _flush.Losts++;
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

                OpenedCardsMask = OpenedCardsMask,

                _royalFlush = _royalFlush.Clone(),
                _straightFlush = _straightFlush.Clone(),
                _fourOfKind = _fourOfKind.Clone(),
                _fullHouse = _fullHouse.Clone(),
                _flush = _flush.Clone(),
                _straight = _straight.Clone(),
                _threeOfKind = _threeOfKind.Clone(),
                _twoPairs = _twoPairs.Clone(),
                _onePair = _onePair.Clone(),
                _highCard = _highCard.Clone(),
                _cards = _cards.Clone()
            };
        }
    }
}
