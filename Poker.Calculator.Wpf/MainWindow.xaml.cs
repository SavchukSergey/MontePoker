using System;
using System.Threading;
using System.Windows;
using Poker.Models;
using Poker.Statistics;
using Poker.Views;

namespace Poker {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly PokerStatRootModel _model = new PokerStatRootModel();
        private Thread _calcThread;
        private Timer _refreshTimer;

        public MainWindow() {
            InitializeComponent();
            for (var i = 0; i < 3; i++) {
                var player = new PokerPlayerViewModel();
                player.Name = "Player " + (i + 1).ToString();
                _model.Players.Add(player);
            }
            DataContext = _model;
            _calcThread = new Thread(CalcThreadEntryPoint);
            _calcThread.Start();
            _refreshTimer = new Timer(onTimerTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
        }

        private void onTimerTick(object state) {
            Dispatcher.Invoke(new Action(OnTimerTick));
        }

        private void OnTimerTick() {
            _model.Sync();

        }

        private void CalcThreadEntryPoint() {
            while (true) {
                _model.PlayGame();
            }
        }

        protected void OnTableCardsClick(object sender, CardClickEventArgs e) {
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


        protected void OnPlayerCardsClick(object sender, CardClickEventArgs e) {
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

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            _calcThread.Abort();
            _refreshTimer.Dispose();
        }

        private PokerCardViewModel _selectedCard;


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
