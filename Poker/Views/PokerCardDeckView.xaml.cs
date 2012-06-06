using System;
using System.Windows;
using System.Windows.Controls;
using Poker.Cards;
using Poker.Models;

namespace Poker.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerCardDeckView : UserControl {

        private readonly CardSuit[] _suits = new[] { CardSuit.Hearts, CardSuit.Diamonds, CardSuit.Clubs, CardSuit.Spades };

        private readonly CardRank[] _ranks = new[] {
            CardRank.Ace,
            CardRank.Two,
            CardRank.Three,
            CardRank.Four,
            CardRank.Five, 
            CardRank.Six,
            CardRank.Seven,
            CardRank.Eight, 
            CardRank.Nine, 
            CardRank.Ten, 
            CardRank.Jack, 
            CardRank.Queen, 
            CardRank.King
        };

        public PokerCardDeckView() {
            InitializeComponent();
            for (var i = 0; i < _suits.Length; i++) {
                var suit = _suits[i];
                for (var j = 0; j < _ranks.Length; j++) {
                    var rank = _ranks[j];
                    var view = new PokerCardView();
                    Grid.SetRow(view, i);
                    Grid.SetColumn(view, j);
                    view.Margin = new Thickness(2);
                    view.DataContext = new PokerCardViewModel { Rank = rank, Suit = suit };
                    view.MouseDown += onCardMouseDown;
                    inner.Children.Add(view);
                }
            }
        }

        private PokerCardViewModel _selectedCard;

        private void onCardMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var view = (PokerCardView)sender;
            var card = (PokerCardViewModel)view.DataContext;
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
            OnCardClick(new CardClickEventArgs(view));
            InvalidateCards();
        }

        private void InvalidateCards() {
            foreach (PokerCardView child in inner.Children) {
                child.InvalidateVisual();
            }
        }

        protected virtual void OnCardClick(CardClickEventArgs args) {
            var handler = CardClick;
            if (handler != null) {
                handler(this, args);
            }
        }
        public event EventHandler<CardClickEventArgs> CardClick;
    }
}
