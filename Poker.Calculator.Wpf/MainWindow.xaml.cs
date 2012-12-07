using System;
using System.Threading;
using System.Windows;
using Poker.Calculator.Wpf.Models;
using Poker.Calculator.Wpf.Views;

namespace Poker {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly PokerStatRootModel _model = new PokerStatRootModel();

        public MainWindow() {
            InitializeComponent();
            _model.SetPlayers(6);
            DataContext = _model;
        }


        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            DataContext = null;
        }

        private void TwoPlayersChecked(object sender, RoutedEventArgs e) {
            _model.SetPlayers(2);
        }

        private void ThreePlayersChecked(object sender, RoutedEventArgs e) {
            _model.SetPlayers(3);
        }

        private void FourPlayersChecked(object sender, RoutedEventArgs e) {
            _model.SetPlayers(4);
        }

        private void FivePlayersChecked(object sender, RoutedEventArgs e) {
            _model.SetPlayers(5);
        }

        private void SixPlayersChecked(object sender, RoutedEventArgs e) {
            _model.SetPlayers(6);
        }
    }
}
