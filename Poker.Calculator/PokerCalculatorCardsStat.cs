using System.Collections.Generic;
using System.Linq;
using Poker.Cards;

namespace Poker.Calculator {
    public class PokerCalculatorCardsStat {

        private readonly PokerCalculatorCardResult[] _cards = new PokerCalculatorCardResult[52];

        public PokerCalculatorCardsStat() {
            var deck = new CardDeck();
            deck.Reset();
            PokerCard card;
            while (deck.DealOne(out card)) {
                this[card] = new PokerCalculatorCardResult {
                    Card = card
                };
            }
        }

        public PokerCalculatorCardResult this[PokerCard card] {
            get { return _cards[card.GetIndex()]; }
            private set { _cards[card.GetIndex()] = value; }
        }

        public IList<PokerCalculatorCardResult> GetOutCards(ulong opened) {
            return _cards
                .Where(item => (opened & (1ul << item.Card.GetIndex())) == 0)
                .OrderByDescending(item => item.Scores)
                .ToList();
        }

        public PokerCalculatorCardsStat Clone() {
            var res = new PokerCalculatorCardsStat();
            for (var i = 0; i < 52; i++) {
                res._cards[i] = _cards[i].Clone();
            }
            return res;
        }
    }
}
