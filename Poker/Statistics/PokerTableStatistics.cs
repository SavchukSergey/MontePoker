using System;
using System.Collections.Generic;
using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Statistics {
    public class PokerTableStatistics {

        private int _gamesPlayed;
        public int GamesPlayed {
            get { return _gamesPlayed; }
        }

        private readonly IList<PokerPlayerStatistics> _players = new List<PokerPlayerStatistics>();
        public IList<PokerPlayerStatistics> Players {
            get { return _players; }
        }

        private class PokerPlayerHand {

            public PokerPlayerStatistics Player;

            public readonly PokerCard[] Cards = new PokerCard[7];

            public HandEvaluation Evaluation;
        }

        public PokerCard CardA;
        public PokerCard CardB;
        public PokerCard CardC;
        public PokerCard CardD;
        public PokerCard CardE;

        public void PlayGame() {

            _gamesPlayed++;
            var deck = GetAvailableCardsSnapShot();
            IList<PokerPlayerHand> hands = new List<PokerPlayerHand>();
            for (var i = 0; i < _players.Count; i++) {
                var player = _players[i];
                var hand = new PokerPlayerHand { Player = player };
                if (!player.CardA.Empty()) {
                    hand.Cards[0] = player.CardA;
                } else {
                    PokerCard card;
                    deck.DealOne(out card);
                    hand.Cards[0] = card;
                }
                if (!player.CardB.Empty()) {
                    hand.Cards[1] = player.CardB;
                } else {
                    PokerCard card;
                    deck.DealOne(out card);
                    hand.Cards[1] = card;
                }
                hands.Add(hand);
            }
            PokerCard cardA, cardB, cardC, cardD, cardE;
            if (!CardA.Empty()) {
                cardA = CardA;
            } else {
                deck.DealOne(out cardA);
            }
            if (!CardB.Empty()) {
                cardB = CardB;
            } else {
                deck.DealOne(out cardB);
            }
            if (!CardC.Empty()) {
                cardC = CardC;
            } else {
                deck.DealOne(out cardC);
            }
            if (!CardD.Empty()) {
                cardD = CardD;
            } else {
                deck.DealOne(out cardD);
            }
            if (!CardE.Empty()) {
                cardE = CardE;
            } else {
                deck.DealOne(out cardE);
            }
          
            var bestScore = -1;
            var split = false;
            for (var i = 0; i < hands.Count; i++) {
                var hand = hands[i];
                hand.Cards[2] = cardA;
                hand.Cards[3] = cardB;
                hand.Cards[4] = cardC;
                hand.Cards[5] = cardD;
                hand.Cards[6] = cardE;

                HandEvaluation eval;
                HandEvaluator.EvaluateHand(hand.Cards, out eval);
                if (eval.Scores > bestScore) {
                    bestScore = eval.Scores;
                    split = false;
                } else if (eval.Scores == bestScore) {
                    split = true;
                }
                hand.Player.Increase(eval.HandType);
                hand.Evaluation = eval;
            }
            for (var i = 0; i < hands.Count; i++) {
                var hand = hands[i];
                if (hand.Evaluation.Scores == bestScore) {
                    if (!split) {
                        hand.Player.Wins++;
                    } else {
                        hand.Player.Splits++;
                    }
                } else {
                    hand.Player.Losts++;
                }
            }
        }

        protected CardDeck GetAvailableCardsSnapShot() {
            throw new NotImplementedException();
        }


        public void Reset() {
            _gamesPlayed = 0;
            CardA.Rank = CardRank.None;
            CardA.Suit = CardSuit.None;
            CardB.Rank = CardRank.None;
            CardB.Suit = CardSuit.None;
            CardC.Rank = CardRank.None;
            CardC.Suit = CardSuit.None;
            CardD.Rank = CardRank.None;
            CardD.Suit = CardSuit.None;
            CardE.Rank = CardRank.None;
            CardE.Suit = CardSuit.None;
        }

    }
}
