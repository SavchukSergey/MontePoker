using System.Collections.Generic;

namespace Poker.Calculator {
    public class PokerCalculatorResult {

        public int GamesPlayed { get; set; }

        private readonly IList<PokerCalculatorPlayer> _players = new List<PokerCalculatorPlayer>();
        public IList<PokerCalculatorPlayer> Players {
            get { return _players; }
        }

    }
}
