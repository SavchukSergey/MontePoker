using Poker.Cards;

namespace Poker.Evaluation {
    public class HandEvaluator {

        private const uint ROYAL_FLUSH_MASK = 0x1f00;

        private static readonly int[] FlushScores = new int[8192];
        private static readonly int[] StraightScores = new int[8192];
        private static readonly int[] HighCardScores = new int[8192];

        static HandEvaluator() {
            InitializeFlushes();
            InitializeStraights();
            InitializeHighCards();
        }

        private static void InitializeFlushes() {
            for (var i = 0; i < 8192; i++) {
                var count = 0;
                var highest5 = 0;
                var walker = 1 << 12;
                while (walker > 0) {
                    if ((i & walker) > 0) {
                        if (count < 5) {
                            highest5 |= walker;
                        }
                        count++;
                    }
                    walker >>= 1;
                }
                if (count >= 5) {
                    FlushScores[i] = highest5;
                } else {
                    FlushScores[i] = -1;
                }
            }
        }

        private static void InitializeStraights() {
            var straightMasks = new[] { 0x100f, 0x001f, 0x003e, 0x007c, 0x00f8, 0x01f0, 0x03e0, 0x07c0, 0x0f80, 0x1f00 };
            for (var i = 0; i < 8192; i++) {
                StraightScores[i] = 0;
                for (var j = straightMasks.Length - 1; j >= 0; j--) {
                    var mask = straightMasks[j];
                    if ((i & mask) == mask) {
                        StraightScores[i] = mask;
                        break;
                    }
                }
            }
        }

        private static void InitializeHighCards() {
            for (var i = 0; i < 8192; i++) {
                var count = 0;
                var highest5 = 0;
                var walker = i << 12;
                while (walker > 0) {
                    if ((i & walker) > 0) {
                        if (count < 5) {
                            highest5 |= walker;
                        }
                        count++;
                    }
                    walker >>= 1;
                }
                HighCardScores[i] = highest5;
            }
        }

        private static unsafe void SummarizeCards(PokerCard[] cards, out CardsEvaluation groups) {
            groups.HeartsMask = 0;
            groups.DiamondsMask = 0;
            groups.SpadesMask = 0;
            groups.ClubsMask = 0;
            var ranks = stackalloc CardRankGroup[13];
            for (var i = 0; i < 13; i++) {
                var rank = (CardRank)(i + 1);
                ranks[i].Rank = rank;
                ranks[i].Score = rank.Scores();
                ranks[i].Count = 0;
            }
            for (var i = 0; i < cards.Length; i++) {
                var rank = cards[i].Rank;
                switch (cards[i].Suit) {
                    case CardSuit.Hearts:
                        groups.HeartsMask |= rank.ScoresMask();
                        break;
                    case CardSuit.Diamonds:
                        groups.DiamondsMask |= rank.ScoresMask();
                        break;
                    case CardSuit.Spades:
                        groups.SpadesMask |= rank.ScoresMask();
                        break;
                    case CardSuit.Clubs:
                        groups.ClubsMask |= rank.ScoresMask();
                        break;
                }
                ranks[(int)rank - 1].Count++;
            }
            for (var i = 0; i < 4; i++) {
                var groupA = ranks[i];
                for (var j = i + 1; j < 13; j++) {
                    var groupB = ranks[j];
                    if (groupB.Count > groupA.Count || (groupB.Count == groupA.Count && groupB.Score > groupA.Score)) {
                        ranks[i] = groupB;
                        ranks[j] = groupA;
                        groupA = groupB;
                    }
                }
            }
            groups.RanksA = ranks[0];
            groups.RanksB = ranks[1];
            groups.RanksC = ranks[2];
            groups.RanksD = ranks[3];
        }

        public static void EvaluateHand(PokerCard[] cards, out HandEvaluation eval) {
            CardsEvaluation summary;
            SummarizeCards(cards, out summary);
            var straightScores = -1;
            if (StraightScores[summary.HeartsMask] > 0) {
                straightScores = StraightScores[summary.HeartsMask];
            } else if (StraightScores[summary.DiamondsMask] > 0) {
                straightScores = StraightScores[summary.DiamondsMask];
            } else if (StraightScores[summary.ClubsMask] > 0) {
                straightScores = StraightScores[summary.ClubsMask];
            } else if (StraightScores[summary.SpadesMask] > 0) {
                straightScores = StraightScores[summary.SpadesMask];
            }
            if (straightScores > 0) {
                if (straightScores == ROYAL_FLUSH_MASK) {
                    eval.HandType = HandType.RoyalFlush;
                    eval.Scores = (int)HandScoresBase.RoyalFlush;
                    return;
                }
                eval.HandType = HandType.StraightFlush;
                eval.Scores = (int)HandScoresBase.StraighFlush + straightScores;
                return;
            }

            if (summary.RanksA.Count == 4) {
                eval.HandType = HandType.FourOfKind;
                eval.Scores = (int)HandScoresBase.FourOfKind + summary.RanksA.Score;
                return;
            }
            if (summary.RanksA.Count == 3 && summary.RanksB.Count >= 2) {
                eval.HandType = HandType.FullHouse;
                eval.Scores = (int)HandScoresBase.FullHouse + (summary.RanksA.Score << 4) + summary.RanksB.Score;
                return;
            }

            var flushScores = -1;
            if (FlushScores[summary.HeartsMask] >= 0) {
                flushScores = FlushScores[summary.HeartsMask];
            } else if (FlushScores[summary.DiamondsMask] >= 0) {
                flushScores = FlushScores[summary.DiamondsMask];
            } else if (FlushScores[summary.ClubsMask] >= 0) {
                flushScores = FlushScores[summary.ClubsMask];
            } else if (FlushScores[summary.SpadesMask] >= 0) {
                flushScores = FlushScores[summary.SpadesMask];
            }
            if (flushScores >= 0) {
                eval.HandType = HandType.Flush;
                eval.Scores = (int)HandScoresBase.Flush + flushScores;
                return;
            }

            straightScores = StraightScores[summary.HeartsMask | summary.DiamondsMask | summary.ClubsMask | summary.SpadesMask];
            if (straightScores > 0) {
                eval.HandType = HandType.Straight;
                eval.Scores = (int)HandScoresBase.Straight + straightScores;
                return;
            }

            if (summary.RanksA.Count == 3) {
                eval.HandType = HandType.ThreeOfKind;
                eval.Scores = (int)HandScoresBase.ThreeOfKind + (summary.RanksA.Score << 8) + (summary.RanksB.Score << 4) + (summary.RanksC.Score);
                return;
            }

            if (summary.RanksA.Count == 2 && summary.RanksB.Count == 2) {
                eval.HandType = HandType.TwoPairs;
                eval.Scores = (int)HandScoresBase.TwoPairs + (summary.RanksA.Score << 8) + (summary.RanksB.Score << 4) + (summary.RanksC.Score);
                return;
            }

            if (summary.RanksA.Count == 2) {
                eval.HandType = HandType.OnePair;
                eval.Scores = (int)HandScoresBase.OnePair + (summary.RanksA.Score << 12) + (summary.RanksB.Score << 8) + (summary.RanksC.Score << 4) + (summary.RanksD.Score);
                return;
            }
            eval.HandType = HandType.HighCard;
            eval.Scores = (int)HandScoresBase.HighCard + HighCardScores[straightScores];
        }

    }
}
