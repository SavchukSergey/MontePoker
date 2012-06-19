using System.Collections.Generic;

namespace Poker.Statistics {
    public class PokerCalculatorResult {

        private readonly IList<PokerCalculatorPlayer> _players = new List<PokerCalculatorPlayer>();
        public IList<PokerCalculatorPlayer> Players {
            get { return _players; }
        }

    }
}
