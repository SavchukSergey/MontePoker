using System.Collections.Generic;
using System.Linq;

namespace Poker.Cards {
    public struct PokerCard {

        public PokerCard(CardRank rank, CardSuit suit) {
            Rank = rank;
            Suit = suit;
        }

        public static PokerCard Empty {
            get { return new PokerCard(CardRank.None, CardSuit.None); }
        }

        public CardRank Rank;

        public CardSuit Suit;

        public static bool operator ==(PokerCard cardA, PokerCard cardB) {
            return cardA.Rank == cardB.Rank && cardA.Suit == cardB.Suit;
        }

        public static bool operator !=(PokerCard cardA, PokerCard cardB) {
            return cardA.Rank != cardB.Rank || cardA.Suit != cardB.Suit;
        }

        public static PokerCard[] ParseList(string src) {
            var cardsStrings = src.Split(' ');
            IList<PokerCard> cards = new List<PokerCard>();
            foreach (var cardString in cardsStrings) {
                var card = Parse(cardString);
                cards.Add(card);
            }
            return cards.ToArray();
        }

        public static PokerCard Parse(string src) {
            var rank = ParseRank(ref src);
            var suit = ParseSuit(ref src);
            return new PokerCard(rank, suit);
        }

        private static CardRank ParseRank(ref string src) {
            CardRank rank;
            if (src.StartsWith("A")) {
                rank = CardRank.Ace;
            } else if (src.StartsWith("K")) {
                rank = CardRank.King;
            } else if (src.StartsWith("Q")) {
                rank = CardRank.Queen;
            } else if (src.StartsWith("J")) {
                rank = CardRank.Jack;
            } else if (src.StartsWith("10")) {
                rank = CardRank.Ten;
                src = src.Substring(1);
            } else if (src.StartsWith("9")) {
                rank = CardRank.Nine;
            } else if (src.StartsWith("8")) {
                rank = CardRank.Eight;
            } else if (src.StartsWith("7")) {
                rank = CardRank.Seven;
            } else if (src.StartsWith("6")) {
                rank = CardRank.Six;
            } else if (src.StartsWith("5")) {
                rank = CardRank.Five;
            } else if (src.StartsWith("4")) {
                rank = CardRank.Four;
            } else if (src.StartsWith("3")) {
                rank = CardRank.Three;
            } else if (src.StartsWith("2")) {
                rank = CardRank.Two;
            } else {
                rank = CardRank.None;
            }
            src = src.Substring(1);
            return rank;
        }

        private static CardSuit ParseSuit(ref string src) {
            if (src.StartsWith("S")) {
                return CardSuit.Spades;
            }
            if (src.StartsWith("C")) {
                return CardSuit.Clubs;
            }
            if (src.StartsWith("D")) {
                return CardSuit.Diamonds;
            }
            if (src.StartsWith("H")) {
                return CardSuit.Hearts;
            }
            return CardSuit.None;
        }

        public override string ToString() {
            string str = GetRankString();
            switch (Suit) {
                case CardSuit.Spades:
                    str += (char)0x2660;
                    break;
                case CardSuit.Hearts:
                    str += (char)0x2665;
                    break;
                case CardSuit.Clubs:
                    str += (char)0x2663;
                    break;
                case CardSuit.Diamonds:
                    str += (char)0x2666;
                    break;
                default:
                    str += Suit.ToString();
                    break;

            }
            return str;
        }

        public string ToSimpleString() {
            string str = GetRankString();
            switch (Suit) {
                case CardSuit.Spades:
                    str += "S";
                    break;
                case CardSuit.Hearts:
                    str += "H";
                    break;
                case CardSuit.Clubs:
                    str += "C";
                    break;
                case CardSuit.Diamonds:
                    str += "D";
                    break;
                default:
                    str += Suit.ToString();
                    break;

            }
            return str;
        }

        private string GetRankString() {
            switch (Rank) {
                case CardRank.Ace:
                    return "A";
                case CardRank.Two:
                    return "2";
                case CardRank.Three:
                    return "3";
                case CardRank.Four:
                    return "4";
                case CardRank.Five:
                    return "5";
                case CardRank.Six:
                    return "6";
                case CardRank.Seven:
                    return "7";
                case CardRank.Eight:
                    return "8";
                case CardRank.Nine:
                    return "9";
                case CardRank.Ten:
                    return "10";
                case CardRank.Jack:
                    return "J";
                case CardRank.Queen:
                    return "Q";
                case CardRank.King:
                    return "K";
                default:
                    return Rank.ToString();
            }
        }

        public bool Equals(PokerCard other) {
            return Equals(other.Rank, Rank) && Equals(other.Suit, Suit);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(PokerCard)) return false;
            return Equals((PokerCard)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (Rank.GetHashCode() * 397) ^ Suit.GetHashCode();
            }
        }
    }
}
