using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Poker.Models {
    public class PokerCardDeckViewModel : INotifyPropertyChanged {

        private readonly ObservableCollection<PokerCardViewModel> _cards = new ObservableCollection<PokerCardViewModel>();

        public ObservableCollection<PokerCardViewModel> Cards {
            get { return _cards; }
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
