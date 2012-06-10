using System.Windows;
using Poker.Models;
using Poker.Views;

namespace Poker {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly PokerStatRootModel _model = new PokerStatRootModel();

        public MainWindow() {
            InitializeComponent();
            for (var i = 0; i < 6; i++) {
                var player = new PokerPlayerViewModel();
                player.Name = "Player " + (i + 1).ToString();
                _model.Players.Add(player);
            }
            DataContext = _model;
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

    }
}
