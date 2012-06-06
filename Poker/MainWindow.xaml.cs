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

        protected void OnTableCardsClick(object sender, RoutedEventArgs e) {
        }
    }
}
