using System;
using System.Collections.Generic;
using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Statistics {
    public class PokerCalculator {

        private int _gamesPlayed;
        public int GamesPlayed {
            get { return _gamesPlayed; }
        }

        private readonly IList<PokerCalculatorPlayer> _players = new List<PokerCalculatorPlayer>();
        public IList<PokerCalculatorPlayer> Players {
            get { return _players; }
        }

        public PokerCalculatorTable Table;
        
        private readonly CardDeck _cardDeck = new CardDeck();
        public CardDeck CardDeck {
            get { return _cardDeck; }
        }

        private class PokerPlayerHand {

            public PokerCalculatorPlayer Player;

            public readonly PokerCard[] Cards = new PokerCard[7];

            public HandEvaluation Evaluation;
        }

        public void PlayGame() {

            _gamesPlayed++;
            var deck = GetDeckSnapShot();
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
            if (!Table.CardA.Empty()) {
                cardA = Table.CardA;
            } else {
                deck.DealOne(out cardA);
            }
            if (!Table.CardB.Empty()) {
                cardB = Table.CardB;
            } else {
                deck.DealOne(out cardB);
            }
            if (!Table.CardC.Empty()) {
                cardC = Table.CardC;
            } else {
                deck.DealOne(out cardC);
            }
            if (!Table.CardD.Empty()) {
                cardD = Table.CardD;
            } else {
                deck.DealOne(out cardD);
            }
            if (!Table.CardE.Empty()) {
                cardE = Table.CardE;
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

        protected CardDeck GetDeckSnapShot() {
            throw new NotImplementedException();
        }


        public void Reset() {
            _gamesPlayed = 0;
            Table.Reset();
        }

    }
}
