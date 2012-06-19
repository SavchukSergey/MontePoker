using System.Windows;
using System.Windows.Controls;
using Poker.Models;

namespace Poker.Calculator.Wpf.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerCardDeckView : UserControl {

        public static readonly RoutedEvent CardClickEvent = EventManager.RegisterRoutedEvent("CardClick", RoutingStrategy.Direct, typeof(CardEventHandler), typeof(PokerCardDeckView));

        public PokerCardDeckView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var deck = e.NewValue as PokerCardDeckViewModel;
            if (deck != null) {
                for (int i = 0; i < deck.Cards.Count; i++) {
                    var item = deck.Cards[i];
                    var view = new PokerCardView();
                    Grid.SetRow(view, i / 13);
                    Grid.SetColumn(view, i % 13);
                    view.Margin = new Thickness(2);
                    view.DataContext = item;
                    view.CardClick += ViewOnCardClick;
                    inner.Children.Add(view);
                }
            }
        }

        private void ViewOnCardClick(object sender, CardClickEventArgs args) {
            RaiseEvent(new CardClickEventArgs(CardClickEvent, args.CardView));
            InvalidateCards();
        }

        private void InvalidateCards() {
            foreach (PokerCardView child in inner.Children) {
                child.InvalidateVisual();
            }
        }

        public event CardEventHandler CardClick {
            add { AddHandler(CardClickEvent, value); }
            remove { RemoveHandler(CardClickEvent, value); }
        }

    }
}
