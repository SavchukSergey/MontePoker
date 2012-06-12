using System.ComponentModel;
using Poker.Cards;

namespace Poker.Models {
    public class PokerCardViewModel : INotifyPropertyChanged {

        private PokerCard _card;

        public PokerCardViewModel() {
            _card.Suit = CardSuit.None;
            _card.Rank = CardRank.None;
        }

        public PokerCard Card {
            get { return _card; }
            set {
                if (_card != value) {
                    _card = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Card"));
                    OnPropertyChanged(new PropertyChangedEventArgs("Suit"));
                    OnPropertyChanged(new PropertyChangedEventArgs("Rank"));
                    OnPropertyChanged(new PropertyChangedEventArgs("IsEmpty"));
                }
            }
        }

        public CardSuit Suit {
            get { return _card.Suit; }
            set {
                if (_card.Suit != value) {
                    _card.Suit = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Card"));
                    OnPropertyChanged(new PropertyChangedEventArgs("Suit"));
                    OnPropertyChanged(new PropertyChangedEventArgs("IsEmpty"));
                }
            }
        }

        public CardRank Rank {
            get { return _card.Rank; }
            set {
                if (_card.Rank != value) {
                    _card.Rank = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Card"));
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
            get { return _card.Empty(); }
        }

        public void Empty() {
            _card.Rank = CardRank.None;
            _card.Suit = CardSuit.None;
            OnPropertyChanged(new PropertyChangedEventArgs("Card"));
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
