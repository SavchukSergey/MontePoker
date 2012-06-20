using System.Linq;
using NUnit.Framework;
using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Tests.Evaluation {
    [TestFixture]
    public class HandEvaluatorTest {

        [Test]
        [TestCase(HandType.HighCard, "AH 9C 10C 2D 4S 7S JS", Description = "High card")]
        [TestCase(HandType.OnePair, "AH AC 10C 2D 4S 9C 7C", Description = "One pair")]
        [TestCase(HandType.TwoPairs, "AH AC 10C 10D 4S 9C 7C", Description = "Two pairs")]
        [TestCase(HandType.ThreeOfKind, "AH 10S 10C 10D 4S 9C 7C", Description = "Three of Kind")]
        [TestCase(HandType.Straight, "JH 10S 10C 10D 8S 9C 7C", Description = "Straight")]
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
        [TestCase("1 JH 10S 10C 10D 8S 9C QC", "0 JH 10S 10C 10D 8S 9C 7C", Description = "Straight - Kicker")]
        [TestCase("1 AC 10S 10C 10D 4C 9C 7C", "0 KC 10S 10C 10D 4C 9C 7C", Description = "Flush - Kicker")]
        [TestCase("1 AH AC AD 10D 10S 9C 7C", "0 AH AC 10C 10D 10S 9C 7C", Description = "Full House - Kicker")]
        [TestCase("1 AH 10S 10C 10D 4S 9C 10H", "0 AH 9S 9C 9D 4S 9H 10H", Description = "Four of Kind - Kicker")]
        [TestCase("1 7S 3S 4S AC 5S JD 6S", "0 2S 3S 4S AC 5S JD 6S", Description = "Straight Flush - Kicker")]
        [TestCase("1 AD KD 2C JD 10D 4S QD", "1 AH KH 2C JH 10H 4S QH", Description = "Royal Flush - Kicker")]
        public void OneKickerTest(params string[] cardsList) {
           GameTest(cardsList);
        }

        [Test]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 AH AC 10C 2D 4S 9C 7C", Description = "High Card Vs One Pair")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 AH AC 10C 10D 4S 9C 7C", Description = "High Card Vs Two Pairs")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 10S 10C 10D 4S 9C 7C", Description = "High Card Vs Three of Kind")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 JH 10S 10C 10D 8S 9C 7C", Description = "High Card Vs Straight")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 KC 10S 10C 10D 4C 9C 7C", Description = "High Card Vs Flush")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 AH AC 10C 10D 10S 9C 7C", Description = "High Card Vs Full House")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 AH 9S 9C 9D 4S 9H 10H", Description = "High Card Vs Four of Kind")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 2S 3S 4S AC 5S JD 6S", Description = "High Card Vs Straight Flush")]
        [TestCase("0 AH 9C 10C 2D 4S 7S JS", "1 AH KH 2C JH 10H 4S QH", Description = "High Card Vs Royal Flush")]
        public void HighCardVersusTest(params string[] cardsList) {
            GameTest(cardsList);
        }

        [Test]
        [TestCase("1 AH AC 10C 2D 4S 9C 7C", "0 AH 9C 10C 2D 4S 7S JS", Description = "One Pair Vs High Card")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 AH AC 10C 10D 4S 9C 7C", Description = "One Pair Vs Two Pairs")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 10S 10C 10D 4S 9C 7C", Description = "One Pair Vs Three of Kind")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 JH 10S 10C 10D 8S 9C 7C", Description = "One Pair Vs Straight")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 KC 10S 10C 10D 4C 9C 7C", Description = "One Pair Vs Flush")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 AH AC 10C 10D 10S 9C 7C", Description = "One Pair Vs Full House")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 AH 9S 9C 9D 4S 9H 10H", Description = "One Pair Vs Four of Kind")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 2S 3S 4S AC 5S JD 6S", Description = "One Pair Vs Straight Flush")]
        [TestCase("0 AH AC 10C 2D 4S 9C 7C", "1 AH KH 2C JH 10H 4S QH", Description = "One Pair Vs Royal Flush")]
        public void OnePairVersusTest(params string[] cardsList) {
            GameTest(cardsList);
        }

        [Test]
        [TestCase("1 AH AC 10C 10D 4S 9C 7C", "0 AH 9C 10C 2D 4S 7S JS", Description = "Two Pairs Vs High Card")]
        [TestCase("1 AH AC 10C 10D 4S 9C 7C", "0 AH AC 10C 2D 4S 9C 7C", Description = "Two Pairs Vs One Pair")]
        [TestCase("0 AH AC 10C 10D 4S 9C 7C", "1 10S 10C 10D 4S 9C 7C", Description = "Two Pairs Vs Three of Kind")]
        [TestCase("0 AH AC 10C 10D 4S 9C 7C", "1 JH 10S 10C 10D 8S 9C 7C", Description = "Two Pairs Vs Straight")]
        [TestCase("0 AH AC 10C 10D 4S 9C 7C", "1 KC 10S 10C 10D 4C 9C 7C", Description = "Two Pairs Vs Flush")]
        [TestCase("0 AH AC 10C 10D 4S 9C 7C", "1 AH AC 10C 10D 10S 9C 7C", Description = "Two Pairs Vs Full House")]
        [TestCase("0 AH AC 10C 10D 4S 9C 7C", "1 AH 9S 9C 9D 4S 9H 10H", Description = "Two Pairs Vs Four of Kind")]
        [TestCase("0 AH AC 10C 10D 4S 9C 7C", "1 2S 3S 4S AC 5S JD 6S", Description = "Two Pairs Vs Straight Flush")]
        [TestCase("0 AH AC 10C 10D 4S 9C 7C", "1 AH KH 2C JH 10H 4S QH", Description = "Two Pairs Vs Royal Flush")]
        public void TwoPairsVersusTest(params string[] cardsList) {
            GameTest(cardsList);
        }

        [Test]
        [TestCase("1 10S 10C 10D 4S 9C 7C", "0 AH 9C 10C 2D 4S 7S JS", Description = "Three of Kind Vs High Card")]
        [TestCase("1 10S 10C 10D 4S 9C 7C", "0 AH AC 10C 2D 4S 9C 7C", Description = "Three of Kind Vs One Pair")]
        [TestCase("1 10S 10C 10D 4S 9C 7C", "0 AH AC 10C 10D 4S 9C 7C", Description = "Three of Kind Vs Two Pairs")]
        [TestCase("0 10S 10C 10D 4S 9C 7C", "1 JH 10S 10C 10D 8S 9C 7C", Description = "Three of Kind Vs Straight")]
        [TestCase("0 10S 10C 10D 4S 9C 7C", "1 KC 10S 10C 10D 4C 9C 7C", Description = "Three of Kind Vs Flush")]
        [TestCase("0 10S 10C 10D 4S 9C 7C", "1 AH AC 10C 10D 10S 9C 7C", Description = "Three of Kind Vs Full House")]
        [TestCase("0 10S 10C 10D 4S 9C 7C", "1 AH 9S 9C 9D 4S 9H 10H", Description = "Three of Kind Vs Four of Kind")]
        [TestCase("0 10S 10C 10D 4S 9C 7C", "1 2S 3S 4S AC 5S JD 6S", Description = "Three of Kind Vs Straight Flush")]
        [TestCase("0 10S 10C 10D 4S 9C 7C", "1 AH KH 2C JH 10H 4S QH", Description = "Three of Kind Vs Royal Flush")]
        public void ThreeOfKindVersusTest(params string[] cardsList) {
            GameTest(cardsList);
        }

        [Test]
        [TestCase("1 JH 10S 10C 10D 8S 9C 7C", "0 AH 9C 10C 2D 4S 7S JS", Description = "Straight Vs High Card")]
        [TestCase("1 JH 10S 10C 10D 8S 9C 7C", "0 AH AC 10C 2D 4S 9C 7C", Description = "Straight Vs One Pair")]
        [TestCase("1 JH 10S 10C 10D 8S 9C 7C", "0 AH AC 10C 10D 4S 9C 7C", Description = "Straight Vs Two Pairs")]
        [TestCase("1 JH 10S 10C 10D 8S 9C 7C", "0 10S 10C 10D 4S 9C 7C", Description = "Straight Vs Three of Kind")]
        [TestCase("0 JH 10S 10C 10D 8S 9C 7C", "1 KC 10S 10C 10D 4C 9C 7C", Description = "Straight Vs Flush")]
        [TestCase("0 JH 10S 10C 10D 8S 9C 7C", "1 AH AC 10C 10D 10S 9C 7C", Description = "Straight Vs Full House")]
        [TestCase("0 JH 10S 10C 10D 8S 9C 7C", "1 AH 9S 9C 9D 4S 9H 10H", Description = "Straight Vs Four of Kind")]
        [TestCase("0 JH 10S 10C 10D 8S 9C 7C", "1 2S 3S 4S AC 5S JD 6S", Description = "Straight Vs Straight Flush")]
        [TestCase("0 JH 10S 10C 10D 8S 9C 7C", "1 AH KH 2C JH 10H 4S QH", Description = "Straight Vs Royal Flush")]
        public void StraightVersusTest(params string[] cardsList) {
            GameTest(cardsList);
        }

        [Test]
        [TestCase("1 KC 10S 10C 10D 4C 9C 7C", "0 AH 9C 10C 2D 4S 7S JS", Description = "Flush Vs High Card")]
        [TestCase("1 KC 10S 10C 10D 4C 9C 7C", "0 AH AC 10C 2D 4S 9C 7C", Description = "Flush Vs One Pair")]
        [TestCase("1 KC 10S 10C 10D 4C 9C 7C", "0 AH AC 10C 10D 4S 9C 7C", Description = "Flush Vs Two Pairs")]
        [TestCase("1 KC 10S 10C 10D 4C 9C 7C", "0 10S 10C 10D 4S 9C 7C", Description = "Flush Vs Three of Kind")]
        [TestCase("1 KC 10S 10C 10D 4C 9C 7C", "0 JH 10S 10C 10D 8S 9C 7C", Description = "Flush Vs Straight")]
        [TestCase("0 KC 10S 10C 10D 4C 9C 7C", "1 AH AC 10C 10D 10S 9C 7C", Description = "Flush Vs Full House")]
        [TestCase("0 KC 10S 10C 10D 4C 9C 7C", "1 AH 9S 9C 9D 4S 9H 10H", Description = "Flush Vs Four of Kind")]
        [TestCase("0 KC 10S 10C 10D 4C 9C 7C", "1 2S 3S 4S AC 5S JD 6S", Description = "Flush Vs Straight Flush")]
        [TestCase("0 KC 10S 10C 10D 4C 9C 7C", "1 AH KH 2C JH 10H 4S QH", Description = "Flush Vs Royal Flush")]
        public void FlushVersusTest(params string[] cardsList) {
            GameTest(cardsList);
        }

        protected void GameTest(params string[] cardsList)
        {
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
