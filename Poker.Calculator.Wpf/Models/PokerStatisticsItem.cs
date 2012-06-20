using System;
using System.ComponentModel;

namespace Poker.Calculator.Wpf.Models {
    public class PokerStatisticsItem : INotifyPropertyChanged {

        private int _count;
        public int Count {
            get { return _count; }
            set {
                if (_count != value) {
                    _count = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                }
            }
        }

        private double _percentage;
        public double Percentage {
            get { return _percentage; }
            set {
                if (Math.Abs(_percentage - value) > 0.0001) {
                    _percentage = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Percentage"));
                }
            }
        }

        public void Reset() {
            Count = 0;
            Percentage = 0;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetValue(int val, int total) {
            Count = val;
            Percentage = 100.0 * val / (total > 0 ? total : 1);
        }
    }
}
