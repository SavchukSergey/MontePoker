namespace Poker.Cards {
    public struct PokerCard {
        
        public PokerCard(CardRank rank, CardSuit suit) {
            Rank = rank;
            Suit = suit;
        }

        public CardRank Rank;

        public CardSuit Suit;

    }
}
