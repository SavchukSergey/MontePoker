using System;
using System.Windows;
using System.Windows.Controls;

namespace Poker.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerTableCardsView : UserControl {

        public static readonly RoutedEvent CardClickEvent = EventManager.RegisterRoutedEvent("CardClick", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PokerTableCardsView));

        public PokerTableCardsView() {
            InitializeComponent();
        }

        protected void OnCardClick(object sender, RoutedEventArgs e) {
            var view = (PokerCardView)sender;
            RaiseEvent(new RoutedEventArgs(CardClickEvent, view));
        }

        public event EventHandler<CardClickEventArgs> CardClick {
            add { AddHandler(CardClickEvent, value); }
            remove { RemoveHandler(CardClickEvent, value); }
        }
    }
}
