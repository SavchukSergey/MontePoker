using NUnit.Framework;
using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Tests.Evaluation {
    [TestFixture]
    public class HandEvaluatorTest {

        [Test]
        public void EvaluateHighCard() {
            var cards = PokerCard.ParseList("AH 9C 10C 2D 4S 7S JS");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.HighCard, eval.HandType);
        }

        [Test]
        public void EvaluateOnePairCard() {
            var cards = PokerCard.ParseList("AH AC 10C 2D 4S 9C 7C");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.OnePair, eval.HandType);
        }


        [Test]
        public void EvaluateTwoPairsCard() {
            var cards = PokerCard.ParseList("AH AC 10C 10D 4S 9C 7C");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.TwoPairs, eval.HandType);
        }

        [Test]
        public void EvaluateThreeOfKindCard() {
            var cards = PokerCard.ParseList("AH 10S 10C 10D 4S 9C 7C");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.ThreeOfKind, eval.HandType);
        }

        [Test]
        public void EvaluateStraightCard() {
            var cards = PokerCard.ParseList("JH 10S 10C 10D 8S 9C 7C");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.Straight, eval.HandType);
        }

        [Test]
        public void EvaluateFlushCard() {
            var cards = PokerCard.ParseList("AC 10S 10C 10D 4C 9C 7C");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.Flush, eval.HandType);
        }

        [Test]
        public void EvaluateFullHouseCard() {
            var cards = PokerCard.ParseList("AH AC 10C 10D 10S 9C 7C");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.FullHouse, eval.HandType);
        }

        [Test]
        public void EvaluateFourOfKindCard() {
            var cards = PokerCard.ParseList("AH 10S 10C 10D 4S 9C 10H");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.FourOfKind, eval.HandType);
        }

        [Test]
        public void EvaluateStraightFlushCard() {
            var cards = PokerCard.ParseList("2S 3S 4S AC 5S JD 6S");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.StraightFlush, eval.HandType);
        }

        [Test]
        public void EvaluateFlusRoyalCard() {
            var cards = PokerCard.ParseList("AD KD 2C JD 10D 4S QD");
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(HandType.RoyalFlush, eval.HandType);
        }
    }
}
