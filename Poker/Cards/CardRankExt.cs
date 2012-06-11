namespace Poker.Cards {
    public static class CardRankExt {

        private static readonly int[] RankScores = new[] { -1, 12, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        private static readonly int[] RankScoresMasks = new[] {
            0x0,
            0x1000,
            0x1,
            0x2,
            0x4,
            0x8,
            0x10,
            0x20,
            0x40,
            0x80,
            0x100,
            0x200,
            0x400,
            0x800,
        };

        public static int Scores(this CardRank rank) {
            return RankScores[(int)rank];
        }

        public static int ScoresMask(this CardRank rank) {
            return RankScoresMasks[(int)rank];
        }

    }
}
