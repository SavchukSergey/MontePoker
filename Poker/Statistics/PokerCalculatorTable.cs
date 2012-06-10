using Poker.Cards;

namespace Poker.Statistics {
    public struct PokerCalculatorTable {

        public PokerCard CardA;
        public PokerCard CardB;
        public PokerCard CardC;
        public PokerCard CardD;
        public PokerCard CardE;

        public void Reset() {
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
