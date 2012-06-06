namespace Poker.Cards {
    public static class CardRankExt {

        private static readonly int[] RankScores = new[] { 12, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        public static int Scores(this CardRank rank) {
            return RankScores[(int)rank];
        }

    }
}
