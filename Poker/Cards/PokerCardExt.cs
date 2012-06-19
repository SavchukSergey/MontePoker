namespace Poker.Cards {
    public static class PokerCardExt {

        public static bool IsEmpty(this PokerCard card) {
            return card.Rank == CardRank.None || card.Suit == CardSuit.None;
        }

    }
}
