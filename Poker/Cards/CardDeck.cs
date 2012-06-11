using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Cards {
    public class CardDeck {

        private Queue<PokerCard> _cards = new Queue<PokerCard>();

        public CardDeck() {

        }

        public CardDeck(IEnumerable<PokerCard> cards) {
            _cards = new Queue<PokerCard>(cards);
        }

        public void Reset() {
            _cards.Clear();
            for (var i = 1; i <= 13; i++) {
                for (var j = 0; j < 4; j++) {
                    var card = new PokerCard {
                        Rank = (CardRank)i,
                        Suit = (CardSuit)j
                    };
                    _cards.Enqueue(card);
                }
            }
        }

        public void Shuffle() {
            _cards = new Queue<PokerCard>(_cards.OrderBy(item => Guid.NewGuid()));
        }

        public void DealOne(out PokerCard card) {
            if (_cards.Count > 0) {
                card = _cards.Dequeue();
            } else {
                card = new PokerCard(CardRank.None, CardSuit.None);
            }
        }

        public CardDeck Clone() {
            var res = new CardDeck { _cards = new Queue<PokerCard>(_cards) };
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
