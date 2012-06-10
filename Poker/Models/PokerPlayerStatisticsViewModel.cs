﻿using System.ComponentModel;

namespace Poker.Models {
    public class PokerPlayerStatisticsViewModel : INotifyPropertyChanged {

        private int _wins;
        public int Wins {
            get { return _wins; }
            set {
                if (_wins != value) {
                    _wins = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Wins"));
                }
            }
        }

        private int _losts;
        public int Losts {
            get { return _losts; }
            set {
                if (_losts != value) {
                    _losts = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Losts"));
                }
            }
        }

        private int _splits;
        public int Splits {
            get { return _splits; }
            set {
                if (_splits != value) {
                    _splits = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Splits"));
                }
            }
        }

        private int _royalFlush;
        public int RoyalFlush {
            get { return _royalFlush; }
            set {
                if (_royalFlush != value) {
                    _royalFlush = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("RoyalFlush"));
                }
            }
        }

        private int _straightFlush;
        public int StraightFlush {
            get { return _straightFlush; }
            set {
                if (_straightFlush != value) {
                    _straightFlush = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("StraightFlush"));
                }
            }
        }

        private int _fourOfKind;
        public int FourOfKind {
            get { return _fourOfKind; }
            set {
                if (_fourOfKind != value) {
                    _fourOfKind = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FourOfKind"));
                }
            }
        }

        private int _fullHouse;
        public int FullHouse {
            get { return _fullHouse; }
            set {
                if (_fullHouse != value) {
                    _fullHouse = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FullHouse"));
                }
            }
        }

        private int _flush;
        public int Flush {
            get { return _flush; }
            set {
                if (_flush != value) {
                    _flush = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Flush"));
                }
            }
        }

        private int _straight;
        public int Straight {
            get { return _straight; }
            set {
                if (_straight != value) {
                    _straight = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Straight"));
                }
            }
        }

        private int _threeOfKind;
        public int ThreeOfKind {
            get { return _threeOfKind; }
            set {
                if (_threeOfKind != value) {
                    _threeOfKind = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ThreeOfKind"));
                }
            }
        }

        private int _twoPairs;
        public int TwoPairs {
            get { return _twoPairs; }
            set {
                if (_twoPairs != value) {
                    _twoPairs = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("TwoPairs"));
                }
            }
        }

        private int _onePair;
        public int OnePair {
            get { return _onePair; }
            set {
                if (_onePair != value) {
                    _onePair = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("OnePair"));
                }
            }
        }

        private int _highCard;
        public int HighCard {
            get { return _highCard; }
            set {
                if (_highCard != value) {
                    _highCard = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("HighCard"));
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