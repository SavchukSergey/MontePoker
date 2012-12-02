namespace Poker.Cards {
    public static class PokerCardExt {

        public static bool IsEmpty(this PokerCard card) {
            return card.Rank == CardRank.None || card.Suit == CardSuit.None;
        }

        public static int GetIndex(this PokerCard card) {
            return (int)card.Suit * 13 + (int)(card.Rank) - 1;
        }

    }
}
