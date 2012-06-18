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

        public void ReturnCardsTo(PokerStatRootModel root) {
            if (!_cardA.IsEmpty) root.ReturnCardToDeck(_cardA);
            if (!_cardB.IsEmpty) root.ReturnCardToDeck(_cardB);
            if (!_cardC.IsEmpty) root.ReturnCardToDeck(_cardC);
            if (!_cardD.IsEmpty) root.ReturnCardToDeck(_cardD);
            if (!_cardE.IsEmpty) root.ReturnCardToDeck(_cardE);
        }
    }
}
