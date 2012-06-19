using System.ComponentModel;

namespace Poker.Calculator.Wpf.Models {
    public class PokerGlobalStatistics : INotifyPropertyChanged {

        private int _gamesPlayed;
        public int GamesPlayed {
            get { return _gamesPlayed; }
            set {
                if (_gamesPlayed != value) {
                    _gamesPlayed = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("GamesPlayed"));
                }
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
