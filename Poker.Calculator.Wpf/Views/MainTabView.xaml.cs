using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Poker.Calculator.Wpf.Models;

namespace Poker.Calculator.Wpf.Views {
    /// <summary>
    /// Interaction logic for MainTabView.xaml
    /// </summary>
    public partial class MainTabView : UserControl {

        private Thread _calcThread;
        private Timer _refreshTimer;
        private PokerStatRootModel _model;
        private PokerCardViewModel _selectedCard;

        public MainTabView() {
            InitializeComponent();

            RefreshModel();
            DataContextChanged += OnDataContextChanged;
        }

        private void RefreshModel() {
            _model = DataContext as PokerStatRootModel;
            if (_model == null) {
                if (_calcThread != null) {
                    _calcThread.Abort();
                    _calcThread = null;
                }
                if (_refreshTimer != null) {
                    _refreshTimer.Dispose();
                    _refreshTimer = null;
                }
            } else {
                if (_calcThread == null) {
                    _calcThread = new Thread(CalcThreadEntryPoint);
                    _calcThread.Start();
                }
                if (_refreshTimer == null) {
                    _refreshTimer = new Timer(OnTimerTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
                }
            }
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            RefreshModel();
        }

        private void OnTimerTick(object state) {
            Dispatcher.Invoke(new Action(OnTimerTick));
        }

        private void OnTimerTick() {
            if (_model != null) {
                _model.Sync();
            }
        }

        private void CalcThreadEntryPoint() {
            while (true) {
                if (_model != null) {
                    _model.PlayGame();
                }
            }
        }


        private void OnTableCardsClick(object sender, CardClickEventArgs e) {
            var view = e.CardView;
            var card = (PokerCardViewModel)view.DataContext;
            var sc = _model.CardDeck.SelectedCard;
            if (sc == null || !card.IsEmpty) {
                _model.ReturnCardToDeck(card);
            }
            if (sc != null) {
                card.Rank = sc.Rank;
                card.Suit = sc.Suit;
                card.Visible = true;
                sc.Visible = false;
            }
            _model.CardDeck.DeselectAll();
        }


        private void OnPlayerCardsClick(object sender, CardClickEventArgs e) {
            var view = e.CardView;
            var card = (PokerCardViewModel)view.DataContext;
            var sc = _model.CardDeck.SelectedCard;
            if (sc == null || !card.IsEmpty) {
                _model.ReturnCardToDeck(card);
            }
            if (sc != null) {
                card.Rank = sc.Rank;
                card.Suit = sc.Suit;
                card.Visible = true;
                sc.Visible = false;
            }
            _model.CardDeck.DeselectAll();
        }

        private void OnDeckCardClick(object sender, CardClickEventArgs args) {
            var view = args.CardView;
            var card = (PokerCardViewModel)view.DataContext;

            if (_model.TryDefaultDestination(card)) return;

            if (_selectedCard != null && _selectedCard == card) {
                _selectedCard.Highlighted = false;
                _selectedCard = null;
            } else {
                if (_selectedCard != null) {
                    _selectedCard.Highlighted = false;
                }
                _selectedCard = card;
                _selectedCard.Highlighted = true;
            }
        }

        private void OnNewHandClick(object sender, RoutedEventArgs e) {
            var model = DataContext as PokerStatRootModel;
            if (model != null) {
                model.NewHand();
            }
        }
    }
}
