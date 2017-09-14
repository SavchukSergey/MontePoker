using System;
using System.Windows;
using Poker.Calculator.Wpf.Models;

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


        private void Gps100Checked(object sender, RoutedEventArgs e) {
            _model.SetGps(100);
        }

        private void Gps500Checked(object sender, RoutedEventArgs e) {
            _model.SetGps(500);
        }

        private void Gps1000Checked(object sender, RoutedEventArgs e) {
            _model.SetGps(1000);
        }

        private void Gps5000Checked(object sender, RoutedEventArgs e) {
            _model.SetGps(5000);
        }

        private void Gps10000Checked(object sender, RoutedEventArgs e) {
            _model.SetGps(10000);
        }

        private void Gps50000Checked(object sender, RoutedEventArgs e) {
            _model.SetGps(50000);
        }

        private void Gps100000Checked(object sender, RoutedEventArgs e) {
            _model.SetGps(100000);
        }

    }
}
