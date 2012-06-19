using Poker.Cards;

namespace Poker.Calculator {
    public struct PokerCalculatorTable {

        public PokerCard CardA;
        public PokerCard CardB;
        public PokerCard CardC;
        public PokerCard CardD;
        public PokerCard CardE;

        public bool Contains(ref PokerCard card) {
            return CardA == card || CardB == card || CardC == card || CardD == card || CardE == card;
        }
    }
}
