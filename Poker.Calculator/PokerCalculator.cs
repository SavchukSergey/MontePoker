using System.Collections.Generic;
using System.Linq;
using Poker.Cards;
using Poker.Evaluation;

namespace Poker.Calculator {
    public class PokerCalculator {

        private readonly IList<PokerCalculatorPlayer> _players;
        private PokerCalculatorTable _table;
        private readonly CardDeck _deck;
        private readonly IList<PokerPlayerHand> _hands = new List<PokerPlayerHand>();
        private int _gamesPlayed;

        public PokerCalculator(PokerCalculatorData data) {
            _players = data.Players.ToList();
            _table = data.Table;
            _deck = data.Deck;

            foreach (var player in _players) {
                var hand = new PokerPlayerHand { Player = player };
                _hands.Add(hand);
            }
        }

        private class PokerPlayerHand {

            public PokerCalculatorPlayer Player;

            public readonly PokerCard[] Cards = new PokerCard[7];

            public HandEvaluation Evaluation;
        }

        public PokerCalculatorResult GetResult() {
            var res = new PokerCalculatorResult { GamesPlayed = _gamesPlayed };
            foreach (var player in _players) {
                res.Players.Add(player.Clone());
            }
            return res;
        }

        public void PlayGame() {
            _gamesPlayed++;
            var deck = _deck.Clone();
            deck.Shuffle();
            for (var i = 0; i < _players.Count; i++) {
                var player = _players[i];
                var hand = _hands[i];
                if (!player.CardA.IsEmpty()) {
                    hand.Cards[0] = player.CardA;
                } else {
                    PokerCard card;
                    deck.DealOne(out card);
                    hand.Cards[0] = card;
                }
                if (!player.CardB.IsEmpty()) {
                    hand.Cards[1] = player.CardB;
                } else {
                    PokerCard card;
                    deck.DealOne(out card);
                    hand.Cards[1] = card;
                }
            }
            PokerCard cardA, cardB, cardC, cardD, cardE;
            if (!_table.CardA.IsEmpty()) {
                cardA = _table.CardA;
            } else {
                deck.DealOne(out cardA);
            }
            if (!_table.CardB.IsEmpty()) {
                cardB = _table.CardB;
            } else {
                deck.DealOne(out cardB);
            }
            if (!_table.CardC.IsEmpty()) {
                cardC = _table.CardC;
            } else {
                deck.DealOne(out cardC);
            }
            if (!_table.CardD.IsEmpty()) {
                cardD = _table.CardD;
            } else {
                deck.DealOne(out cardD);
            }
            if (!_table.CardE.IsEmpty()) {
                cardE = _table.CardE;
            } else {
                deck.DealOne(out cardE);
            }

            var bestScore = -1;
            var split = false;
            for (var i = 0; i < _hands.Count; i++) {
                var hand = _hands[i];
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
            for (var i = 0; i < _hands.Count; i++) {
                var hand = _hands[i];
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

    }
}
