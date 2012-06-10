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
            _model.InvalidateStat();
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
            _model.InvalidateStat();
        }

    }
}
