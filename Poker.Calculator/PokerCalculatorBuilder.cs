using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Cards;

namespace Poker.Calculator {
    public class PokerCalculatorBuilder {

        private readonly PokerCalculatorData _data = new PokerCalculatorData();

        public PokerCalculator Build() {
            _data.Deck = new CardDeck(GetUnusedCards());
            var calc = new PokerCalculator(_data);
            return calc;
        }

        public void AddPlayer(PokerCard cardA, PokerCard cardB) {
            var calcPlayer = new PokerCalculatorPlayer { CardA = cardA, CardB = cardB };
            _data.Players.Add(calcPlayer);
        }

        public void AddTableCard(PokerCard card) {
            if (_data.Table.CardA.IsEmpty()) _data.Table.CardA = card;
            else if (_data.Table.CardB.IsEmpty()) _data.Table.CardB = card;
            else if (_data.Table.CardC.IsEmpty()) _data.Table.CardC = card;
            else if (_data.Table.CardD.IsEmpty()) _data.Table.CardD = card;
            else if (_data.Table.CardE.IsEmpty()) _data.Table.CardE = card;
            else throw new InvalidOperationException("Only five cards are allowed to be on table");
        }

        protected virtual IEnumerable<PokerCard> GetUnusedCards() {
            IList<PokerCard> res = new List<PokerCard>();
            var cards = CardDeck.GetAllCards();
            for (var i = 0; i < cards.Length; i++) {
                if (_data.Table.Contains(ref cards[i])) continue;
                if (_data.Players.Any(player => player.Contains(ref cards[i]))) continue;
                res.Add(cards[i]);
            }
            return res;
        }
    }
}
