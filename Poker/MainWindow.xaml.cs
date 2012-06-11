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
        private readonly PokerCalculator _calculator = new PokerCalculator();
        private Thread _calcThread;
        private Timer _refreshTimer;

        public MainWindow() {
            InitializeComponent();
            for (var i = 0; i < 6; i++) {
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
            if (_model.Dirty) {
                _calculator.Reset();
                _model.ResetDirty();
            }
            for (int i = 0; i < _model.Players.Count; i++) {
                var playerModel = _model.Players[i];
                var player = _calculator.Players[i];
            }
        }

        private void CalcThreadEntryPoint() {
            while (true) {
                _calculator.PlayGame();
            }
        }

        protected void OnTableCardsClick(object sender, CardClickEventArgs e) {
            var view = e.CardView;
            var card = (PokerCardViewModel)view.DataContext;
            var sc = _model.CardDeck.SelectedCard;
            if (sc == null || !card.IsEmpty) {
                _model.CardDeck.ShowCard(card.Rank, card.Suit);
                card.Empty();
            }
            if (sc != null) {
                card.Rank = sc.Rank;
                card.Suit = sc.Suit;
                card.Visible = true;
                sc.Visible = false;
            }
            _model.CardDeck.DeselectAll();
            _model.InvalidateState();
        }


        protected void OnPlayerCardsClick(object sender, CardClickEventArgs e) {
            var view = e.CardView;
            var card = (PokerCardViewModel)view.DataContext;
            var sc = _model.CardDeck.SelectedCard;
            if (sc == null || !card.IsEmpty) {
                _model.CardDeck.ShowCard(card.Rank, card.Suit);
                card.Empty();
            }
            if (sc != null) {
                card.Rank = sc.Rank;
                card.Suit = sc.Suit;
                card.Visible = true;
                sc.Visible = false;
            }
            _model.CardDeck.DeselectAll();
            _model.InvalidateState();
        }

    }
}
