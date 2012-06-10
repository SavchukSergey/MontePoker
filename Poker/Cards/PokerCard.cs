namespace Poker.Cards {
    public struct PokerCard {

        public PokerCard(CardRank rank, CardSuit suit) {
            Rank = rank;
            Suit = suit;
        }

        public CardRank Rank;

        public CardSuit Suit;

        public static bool operator ==(PokerCard cardA, PokerCard cardB) {
            return cardA.Rank == cardB.Rank && cardA.Suit == cardB.Suit;
        }

        public static bool operator !=(PokerCard cardA, PokerCard cardB) {
            return cardA.Rank != cardB.Rank || cardA.Suit != cardB.Suit;
        }

    }
}
