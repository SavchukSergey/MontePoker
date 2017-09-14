using System.Threading;

namespace Poker.Calculator.Wpf.Models {
    public class LeakyBucket {

        private double _state = 0;
        private double _size = 10000;

        public double Speed { get; set; }

        public double Interval { get; set; } = 250;

        private double IntervalSpeed => Speed / (1000.0 / Interval);

        private double LeakCoeff => (1 - IntervalSpeed / _size);

        public bool Request(double val) {
            InterlockedAdd(ref _state, val);
            if (_state > _size) {
                InterlockedAdd(ref _state, -val);
                return false;
            }
            return true;
        }

        public void Leak() {
            InterlockedMul(ref _state, LeakCoeff);
        }

        private static double InterlockedAdd(ref double location1, double value) {
            double newCurrentValue = location1; // non-volatile read, so may be stale
            while (true) {
                double currentValue = newCurrentValue;
                double newValue = currentValue + value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                    return newValue;
            }
        }

        private static double InterlockedMul(ref double location1, double value) {
            double newCurrentValue = location1; // non-volatile read, so may be stale
            while (true) {
                double currentValue = newCurrentValue;
                double newValue = currentValue * value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                    return newValue;
            }
        }

    }
}
