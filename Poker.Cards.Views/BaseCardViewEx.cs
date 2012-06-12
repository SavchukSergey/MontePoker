using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Poker.Cards.Views {
    public class BaseCardViewEx : BaseCardView {

        private FixedPage _page;
        private Canvas _canvas;
        private Path[] _paths;

        public BaseCardViewEx() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            _page = new FixedPage { RenderTransform = new MatrixTransform(1, 0, 0, 1, -305, -404) };
            _canvas = new Canvas { Clip = Geometry.Parse("M 72,72 L 742,72 742,984 72,984 72,72 Z") };
            _page.Children.Add(_canvas);
            Children.Add(_page);
        }

        public override void EndInit() {
            _paths = GetPaths();
            AddPaths(_canvas, _paths);
            base.EndInit();
        }

        protected override void OnBackgroundChanged(DependencyPropertyChangedEventArgs args) {
            var path = GetBgPath();
            if (path != null) {
                path.Fill = (Brush)args.NewValue;
            }
            base.OnBackgroundChanged(args);
        }

        protected virtual void AddPaths(Canvas canvas, Path[] paths) {
            foreach (var path in paths) {
                canvas.Children.Add(path);
            }
        }

        protected virtual Path[] GetPaths() {
            var res = Resources["paths"] as Path[];
            if (res == null) return new Path[0];
            return res;
        }

        protected virtual Path GetBgPath() {
            return _paths.Length > 0 ? _paths[0] : null;
        }

    }
}
