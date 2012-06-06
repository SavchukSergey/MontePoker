using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Poker.Cards;
using Poker.Models;

namespace Poker.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerCardView : UserControl {

        private const double DIM_X = 2;
        private const double DIM_Y = 2.5;

        public PokerCardView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var card = (PokerCardViewModel)e.NewValue;
            var id = GetImageId(card);
            try {
                var uri = new Uri(string.Format("pack://application:,,,/Resources/Cards/{0}.png", id));
                var bi = new BitmapImage(uri);
                image.Source = bi;
            } catch {
                image.Source = null;
            }
        }

        private static string GetImageId(PokerCardViewModel card) {
            var id = "";
            if (card.Suit == CardSuit.None || card.Rank == CardRank.None) return "empty";
            switch (card.Suit) {
                case CardSuit.Clubs:
                    id += 'c';
                    break;
                case CardSuit.Diamonds:
                    id += 'd';
                    break;
                case CardSuit.Spades:
                    id += 's';
                    break;
                case CardSuit.Hearts:
                    id += 'h';
                    break;
            }
            switch (card.Rank) {
                case CardRank.Ace:
                    id += 'a';
                    break;
                case CardRank.Two:
                    id += '2';
                    break;
                case CardRank.Three:
                    id += '3';
                    break;
                case CardRank.Four:
                    id += '4';
                    break;
                case CardRank.Five:
                    id += '5';
                    break;
                case CardRank.Six:
                    id += '6';
                    break;
                case CardRank.Seven:
                    id += '7';
                    break;
                case CardRank.Eight:
                    id += '8';
                    break;
                case CardRank.Nine:
                    id += '9';
                    break;
                case CardRank.Ten:
                    id += "10";
                    break;
                case CardRank.Jack:
                    id += 'j';
                    break;
                case CardRank.Queen:
                    id += 'q';
                    break;
                case CardRank.King:
                    id += 'k';
                    break;
            }
            return id;
        }


        protected override Size ArrangeOverride(Size arrangeBounds) {
            var w = arrangeBounds.Width;
            var h = arrangeBounds.Height;
            var kw = w / DIM_X;
            var kh = h / DIM_Y;
            var k = Math.Min(kw, kh);
            var hp = (w - k * DIM_X) / 2;
            var vp = (h - k * DIM_Y) / 2;
            inner.Arrange(new Rect(hp, vp, k * DIM_X, k * DIM_Y));
            //inner.Margin = new Thickness(hp, vp, hp, vp);
            return arrangeBounds;
        }

        protected override Size MeasureOverride(Size constraint) {
            return base.MeasureOverride(constraint);
        }

    }
}
