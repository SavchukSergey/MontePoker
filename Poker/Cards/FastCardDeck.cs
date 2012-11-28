using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Cards {
    public class FastCardDeck {

        private ulong _busyCards;
        private byte _cardsDealt;

        private static readonly byte[] _bitsCountMap;

        static FastCardDeck() {
            _bitsCountMap = new byte[256];
            for (var i = 0; i < 256; i++) {
                var count = 0;
                for (var j = 0; j < 8; j++) {
                    if ((i & (1 << j)) != 0) count++;
                }
                _bitsCountMap[i] = (byte)count;
            }
        }

        public bool DealOne(out PokerCard card) {
            var cardsLeft = 52 - _cardsDealt;
            var cardIndex = Rnd.Next() % cardsLeft;
            _cardsDealt++;
            card = new PokerCard();
            return true;
        }

    }
}
