using System.ComponentModel;
using Poker.Cards;

namespace Poker.Models {
    public class PokerCardViewModel : INotifyPropertyChanged {

        public PokerCardViewModel() {
            _suit = CardSuit.None;
            _rank = CardRank.None;
        }

        public PokerCard Card {
            get { return new PokerCard(Rank, Suit); }
        }

        private CardSuit _suit;
        public CardSuit Suit {
            get { return _suit; }
            set {
                if (_suit != value) {
                    _suit = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Suit"));
                    OnPropertyChanged(new PropertyChangedEventArgs("IsEmpty"));
                }
            }
        }

        private CardRank _rank;
        public CardRank Rank {
            get { return _rank; }
            set {
                if (_rank != value) {
                    _rank = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Rank"));
                    OnPropertyChanged(new PropertyChangedEventArgs("IsEmpty"));
                }
            }
        }

        private bool _visible = true;
        public bool Visible {
            get { return _visible; }
            set {
                if (_visible != value) {
                    _visible = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Visible"));
                }
            }
        }


        private bool _highlighted;
        public bool Highlighted {
            get { return _highlighted; }
            set {
                if (_highlighted != value) {
                    _highlighted = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Highlighted"));
                }
            }
        }

        public bool IsEmpty {
            get { return _rank == CardRank.None || _suit == CardSuit.None; }
        }

        public void Empty() {
            _rank = CardRank.None;
            _suit = CardSuit.None;
            OnPropertyChanged(new PropertyChangedEventArgs("Rank"));
            OnPropertyChanged(new PropertyChangedEventArgs("Suit"));
            OnPropertyChanged(new PropertyChangedEventArgs("IsEmpty"));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
