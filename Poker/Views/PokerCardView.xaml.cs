using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Poker.Cards;
using Poker.Cards.Views;
using Poker.Cards.Views.Clubs;
using Poker.Cards.Views.Diamonds;
using Poker.Cards.Views.Hearts;
using Poker.Cards.Views.Spades;
using Poker.Models;

namespace Poker.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerCardView : UserControl {

        public static readonly RoutedEvent CardClickEvent = EventManager.RegisterRoutedEvent("CardClick", RoutingStrategy.Direct, typeof(CardEventHandler), typeof(PokerCardView));

        public PokerCardView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var oldCard = e.OldValue as PokerCardViewModel;
            if (oldCard != null) {
                oldCard.PropertyChanged -= OnCardPropertyChanged;
            }
            var newCard = e.NewValue as PokerCardViewModel;
            if (newCard != null) {
                newCard.PropertyChanged += OnCardPropertyChanged;
            }
            UpdateImage(newCard);
        }

        private void OnCardPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            var card = DataContext as PokerCardViewModel;
            var innerView = inner.Child as BaseCardView;

            switch (propertyChangedEventArgs.PropertyName) {
                case "Rank":
                case "Suit":
                    UpdateImage(card);
                    break;
                case "Highlighted":
                    if (card != null && innerView != null) {
                        Color color = card.Highlighted ? Color.FromRgb(192, 192, 0) : Color.FromRgb(255, 255, 255);
                        innerView.CardBackground = new SolidColorBrush(color);
                    }
                    break;
                case "Visible":
                    if (card != null && innerView != null) {
                        innerView.Visibility = card.Visible ? Visibility.Visible : Visibility.Hidden;
                    }
                    break;
            }
        }

        private BaseCardView GetCardView(PokerCard card) {
            switch (card.ToSimpleString()) {
                case "AC":
                    return new CA();
                case "2C":
                    return new C2();
                case "3C":
                    return new C3();
                case "4C":
                    return new C4();
                case "5C":
                    return new C5();
                case "6C":
                    return new C6();
                case "7C":
                    return new C7();
                case "8C":
                    return new C8();
                case "9C":
                    return new C9();
                case "10C":
                    return new C10();
                case "JC":
                    return new CJ();
                case "QC":
                    return new CQ();
                case "KC":
                    return new CK();

                case "AH":
                    return new HA();
                case "2H":
                    return new H2();
                case "3H":
                    return new H3();
                case "4H":
                    return new H4();
                case "5H":
                    return new H5();
                case "6H":
                    return new H6();
                case "7H":
                    return new H7();
                case "8H":
                    return new H8();
                case "9H":
                    return new H9();
                case "10H":
                    return new H10();
                case "JH":
                    return new HJ();
                case "QH":
                    return new HQ();
                case "KH":
                    return new HK();

                case "AD":
                    return new DA();
                case "2D":
                    return new D2();
                case "3D":
                    return new D3();
                case "4D":
                    return new D4();
                case "5D":
                    return new D5();
                case "6D":
                    return new D6();
                case "7D":
                    return new D7();
                case "8D":
                    return new D8();
                case "9D":
                    return new D9();
                case "10D":
                    return new D10();
                case "JD":
                    return new DJ();
                case "QD":
                    return new DQ();
                case "KD":
                    return new DK();

                case "AS":
                    return new SA();
                case "2S":
                    return new S2();
                case "3S":
                    return new S3();
                case "4S":
                    return new S4();
                case "5S":
                    return new S5();
                case "6S":
                    return new S6();
                case "7S":
                    return new S7();
                case "8S":
                    return new S8();
                case "9S":
                    return new S9();
                case "10S":
                    return new S10();
                case "JS":
                    return new SJ();
                case "QS":
                    return new SQ();
                case "KS":
                    return new SK();

                default:
                    return new Empty();
            }
        }

        private void UpdateImage(PokerCardViewModel model) {
            var cardView = GetCardView(model.Card);
            inner.Child = cardView;
        }

        public event CardEventHandler CardClick {
            add { AddHandler(CardClickEvent, value); }
            remove { RemoveHandler(CardClickEvent, value); }
        }

        private void UserControl_Click(object sender, RoutedEventArgs e) {
            RaiseEvent(new CardClickEventArgs(CardClickEvent, this));
        }

    }
}
