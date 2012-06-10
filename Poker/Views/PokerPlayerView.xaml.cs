﻿using System.Windows;
using System.Windows.Controls;

namespace Poker.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerPlayerView : UserControl {
        public static readonly RoutedEvent CardClickEvent = EventManager.RegisterRoutedEvent("CardClick", RoutingStrategy.Direct, typeof(CardEventHandler), typeof(PokerPlayerView));

        public PokerPlayerView() {
            InitializeComponent();
        }

        protected void OnCardClick(object sender, CardClickEventArgs e) {
            var view = (PokerCardView)sender;
            RaiseEvent(new CardClickEventArgs(CardClickEvent, view));
        }

        public event CardEventHandler CardClick {
            add { AddHandler(CardClickEvent, value); }
            remove { RemoveHandler(CardClickEvent, value); }
        }
    }
}
