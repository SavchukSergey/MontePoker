namespace Poker.Evaluation {
    public enum HandScoresBase {
        RoyalFlush = 9 << 20,
        StraighFlush = 8 << 20,
        FourOfKind = 7 << 20,
        FullHouse = 6 << 20,
        Flush = 5 << 20,
        Straight = 4 << 20,
        ThreeOfKind = 3 << 20,
        TwoPairs = 2 << 20,
        OnePair = 1 << 20,
        HighCard = 0
    }
}
