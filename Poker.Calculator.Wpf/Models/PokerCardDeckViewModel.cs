using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Poker.Cards;

namespace Poker.Calculator.Wpf.Models {
    public class PokerCardDeckViewModel : INotifyPropertyChanged {

        private readonly ObservableCollection<PokerCardViewModel> _cards = new ObservableCollection<PokerCardViewModel>();


        private readonly CardSuit[] _suits = new[] { CardSuit.Hearts, CardSuit.Diamonds, CardSuit.Clubs, CardSuit.Spades };

        private readonly CardRank[] _ranks = new[] {
            CardRank.Ace,
            CardRank.Two,
            CardRank.Three,
            CardRank.Four,
            CardRank.Five, 
            CardRank.Six,
            CardRank.Seven,
            CardRank.Eight, 
            CardRank.Nine, 
            CardRank.Ten, 
            CardRank.Jack, 
            CardRank.Queen, 
            CardRank.King
        };

        public PokerCardDeckViewModel() {
            for (var i = 0; i < _suits.Length; i++) {
                var suit = _suits[i];
                for (var j = 0; j < _ranks.Length; j++) {
                    var rank = _ranks[j];
                    var item = new PokerCardViewModel { Rank = rank, Suit = suit };
                    _cards.Add(item);
                }
            }
        }

        public ObservableCollection<PokerCardViewModel> Cards {
            get { return _cards; }
        }

        public PokerCardViewModel SelectedCard {
            get { return Cards.FirstOrDefault(item => item.Highlighted); }
        }


        public void ShowCard(CardRank rank, CardSuit suit) {
            foreach (var card in _cards.Where(item => item.Rank == rank && item.Suit == suit)) {
                card.Visible = true;
            }
        }

        public void DeselectAll() {
            foreach (var card in _cards) {
                card.Highlighted = false;
            }
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
