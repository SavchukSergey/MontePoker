using System.ComponentModel;

namespace Poker.Calculator.Wpf.Models {
    public class PokerPlayerViewModel : INotifyPropertyChanged {

        private readonly PokerCardViewModel _cardA = new PokerCardViewModel();
        private readonly PokerCardViewModel _cardB = new PokerCardViewModel();
        private bool _inGame = true;

        private readonly PokerPlayerStatisticsViewModel _statistics = new PokerPlayerStatisticsViewModel();
        public PokerPlayerStatisticsViewModel Statistics {
            get { return _statistics; }
        }

        private string _name;
        public string Name {
            get { return _name; }
            set {
                if (_name != value) {
                    _name = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public PokerCardViewModel CardA {
            get { return _cardA; }
        }
        public PokerCardViewModel CardB {
            get { return _cardB; }
        }

        public bool InGame {
            get { return _inGame; }
            set {
                if (_inGame != value) {
                    _inGame = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("InGame"));
                }
            }
        }

        public void Fold() {
            InGame = false;
            Statistics.Reset();
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
        }
    }
}
