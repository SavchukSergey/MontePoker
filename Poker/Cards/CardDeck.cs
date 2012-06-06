using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Cards {
    public class CardDeck {

        private IList<PokerCard> _cards = new List<PokerCard>();

        public void Reset() {
            _cards.Clear();
            for (var i = 1; i <= 13; i++) {
                for (var j = 0; j < 4; j++) {
                    var card = new PokerCard {
                        Rank = (CardRank)i,
                        Suit = (CardSuit)j
                    };
                    _cards.Add(card);
                }
            }
        }

        public void Shuffle() {
            throw new NotImplementedException();
        }

        public void DealOne(out PokerCard card) {
            throw new NotImplementedException();
        }

        public CardDeck Clone() {
            var res = new CardDeck { _cards = _cards.ToList() };
            return res;
        }
    }
}
