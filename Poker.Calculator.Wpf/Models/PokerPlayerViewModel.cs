using System.ComponentModel;

namespace Poker.Calculator.Wpf.Models {
    public class PokerPlayerViewModel : INotifyPropertyChanged {

        private readonly PokerCardViewModel _cardA = new PokerCardViewModel();
        private readonly PokerCardViewModel _cardB = new PokerCardViewModel();
        private bool _inGame = true;
        private bool _visible = true;

        public PokerPlayerViewModel() {
            _cardA.PropertyChanged += CardPropertyChanged;
        }

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

        public bool Visible {
            get { return _visible; }
            set {
                if (_visible != value) {
                    _visible = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Visible"));
                }
            }
        }

        public bool HasAnyCard {
            get { return !_cardA.IsEmpty || !_cardB.IsEmpty; }
        }

        public void Fold() {
            InGame = false;
            Statistics.Reset();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ReturnCardsTo(PokerStatRootModel root) {
            if (!_cardA.IsEmpty) root.ReturnCardToDeck(_cardA);
            if (!_cardB.IsEmpty) root.ReturnCardToDeck(_cardB);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, args);
            }
        }

        private void CardPropertyChanged(object sender, PropertyChangedEventArgs e) {
            OnPropertyChanged(new PropertyChangedEventArgs("HasAnyCard"));
        }

    }
}
