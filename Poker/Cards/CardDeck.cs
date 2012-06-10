using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Cards {
    public class CardDeck {

        private IList<PokerCard> _cards = new List<PokerCard>();

        public CardDeck() {

        }

        public CardDeck(IList<PokerCard> cards) {
            _cards = cards;
        }

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
            if (_cards.Count > 0) {
                card = _cards[0];
            } else {
                card = new PokerCard(CardRank.None, CardSuit.None);
            }
        }

        public CardDeck Clone() {
            var res = new CardDeck { _cards = _cards.ToList() };
            return res;
        }

        public static PokerCard[] GetAllCards() {
            var cards = new PokerCard[52];
            for (var i = 0; i < 52; i++) {
                cards[i] = new PokerCard { Rank = (CardRank)((i % 13) + 1), Suit = (CardSuit)(i / 13) };
            }
            return cards;
        }
    }
}
