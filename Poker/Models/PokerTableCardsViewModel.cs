using System.ComponentModel;

namespace Poker.Models {
    public class PokerTableCardsViewModel : INotifyPropertyChanged {

        private readonly PokerCardViewModel _cardA = new PokerCardViewModel();
        private readonly PokerCardViewModel _cardB = new PokerCardViewModel();
        private readonly PokerCardViewModel _cardC = new PokerCardViewModel();
        private readonly PokerCardViewModel _cardD = new PokerCardViewModel();
        private readonly PokerCardViewModel _cardE = new PokerCardViewModel();

        public PokerCardViewModel CardA {
            get { return _cardA; }
        }
        public PokerCardViewModel CardB {
            get { return _cardB; }
        }
        public PokerCardViewModel CardC {
            get { return _cardC; }
        }
        public PokerCardViewModel CardD {
            get { return _cardD; }
        }
        public PokerCardViewModel CardE {
            get { return _cardE; }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Reset() {
            _cardA.Empty();
            _cardB.Empty();
            _cardC.Empty();
            _cardD.Empty();
            _cardE.Empty();
        }
    }
}
