using System.Linq;
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
        [TestCase(HandType.Flush, "AC 10S 10C 10D 4C 9C 7C", Description = "Flush")]
        [TestCase(HandType.FullHouse, "AH AC 10C 10D 10S 9C 7C", Description = "Full House")]
        [TestCase(HandType.FourOfKind, "AH 10S 10C 10D 4S 9C 10H", Description = "Four of Kind")]
        [TestCase(HandType.StraightFlush, "2S 3S 4S AC 5S JD 6S", Description = "Straight Flush")]
        [TestCase(HandType.RoyalFlush, "AD KD 2C JD 10D 4S QD", Description = "Royal Flush")]
        public void HandTypeTest(HandType handType, string cardsString) {
            var cards = PokerCard.ParseList(cardsString);
            HandEvaluation eval;
            HandEvaluator.EvaluateHand(cards, out eval);
            Assert.AreEqual(handType, eval.HandType);
        }

        [Test]
        [TestCase("1 AH 9C 10C 2D 4S 7S JS", "0 KH 9C 10C 2D 4S 7S JS", Description = "High Card - Kicker")]
        [TestCase("1 AH AC JC 2D 4S 9C 7C", "0 AH AC 10C 2D 4S 9C 7C", Description = "One Pair - Kicker")]
        [TestCase("1 AH AC 10C 10D 4S JC 7C", "0 AH AC 10C 10D 4S 9C 7C", Description = "Two Pairs - Kicker")]
        [TestCase("1 AH 10S 10C 10D 4S 9C 7C", "0 KH 10S 10C 10D 4S 9C 7C", Description = "Three of Kind - Kicker")]
        [TestCase("1 AD KD 2C JD 10D 4S QD", "1 AH KH 2C JH 10H 4S QH", Description = "Royal Flush - Kicker")]
        public void OneKickerTest(params string[] cardsList) {
            var bestEval = new HandEvaluation { Scores = -1 };
            foreach (var playerInfo in cardsList) {
                var playerCards = PokerCard.ParseList(playerInfo.Substring(2));
                HandEvaluation eval;
                HandEvaluator.EvaluateHand(playerCards.ToArray(), out eval);
                if (eval.Scores > bestEval.Scores) {
                    bestEval = eval;
                }
            }
            for (int i = 0; i < cardsList.Length; i++) {
                var playerCards = PokerCard.ParseList(cardsList[i].Substring(2));

                HandEvaluation eval;
                HandEvaluator.EvaluateHand(playerCards.ToArray(), out eval);
                var win = cardsList[i].StartsWith("1 ");
                Assert.AreEqual(win, eval.Scores == bestEval.Scores);
            }
        }
    }
}
