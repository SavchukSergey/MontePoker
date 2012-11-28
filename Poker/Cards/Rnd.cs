using System;

namespace Poker.Cards {
    public static class Rnd {

        private static readonly Random _rnd = new Random(DateTime.Now.Millisecond);

        public static int Next() {
            return _rnd.Next();
        }
    }
}
