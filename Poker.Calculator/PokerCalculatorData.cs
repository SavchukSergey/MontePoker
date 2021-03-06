﻿using System.Collections.Generic;
using Poker.Cards;

namespace Poker.Calculator {
    public class PokerCalculatorData {

        public PokerCalculatorData() {
            Table.CardA = PokerCard.Empty;
            Table.CardB = PokerCard.Empty;
            Table.CardC = PokerCard.Empty;
            Table.CardD = PokerCard.Empty;
            Table.CardE = PokerCard.Empty;
        }

        private readonly IList<PokerCalculatorPlayer> _players = new List<PokerCalculatorPlayer>();
        public IList<PokerCalculatorPlayer> Players {
            get { return _players; }
        }

        public PokerCalculatorTable Table;

        public CardDeck Deck { get; set; }

    }
}
