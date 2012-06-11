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

        public override string ToString() {
            var str = "";
            switch (Rank) {
                case CardRank.Ace:
                    str += 'A';
                    break;
                case CardRank.Two:
                    str += '2';
                    break;
                case CardRank.Three:
                    str += '3';
                    break;
                case CardRank.Four:
                    str += '4';
                    break;
                case CardRank.Five:
                    str += '5';
                    break;
                case CardRank.Six:
                    str += '6';
                    break;
                case CardRank.Seven:
                    str += '7';
                    break;
                case CardRank.Eight:
                    str += '8';
                    break;
                case CardRank.Nine:
                    str += '9';
                    break;
                case CardRank.Ten:
                    str += "10";
                    break;
                case CardRank.Jack:
                    str += "J";
                    break;
                case CardRank.Queen:
                    str += "Q";
                    break;
                case CardRank.King:
                    str += "K";
                    break;
                default:
                    str += Rank.ToString();
                    break;
            }
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


    }
}
