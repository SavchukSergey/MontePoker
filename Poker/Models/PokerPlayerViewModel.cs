using System.ComponentModel;

namespace Poker.Models {
    public class PokerPlayerViewModel : INotifyPropertyChanged {

        private readonly PokerCardViewModel _cardA = new PokerCardViewModel();
        private readonly PokerCardViewModel _cardB = new PokerCardViewModel();

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

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ResetStat() {
            throw new System.NotImplementedException();
        }
    }
}
