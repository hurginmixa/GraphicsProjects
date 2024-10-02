using CoordinateSystem;

namespace SimpleApplication.Models
{
    internal class StrokesModel
    {
        private readonly AstrixModel _astrixModel = new(new Point<GraphicSystem>(100, 150), 280, 3);
        private readonly Shift<GraphicSystem> _panelSize = new(200, 300);

        public void TickProcess()
        {
            _astrixModel.TickProcess();
        }

        public IEnumerable<Stroke<GraphicSystem>> Strokes
        {
            get
            {
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(0, 0), new Point<GraphicSystem>(0, _panelSize.DY));
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(0, _panelSize.DY), new Point<GraphicSystem>(_panelSize.DX, _panelSize.DY));
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(_panelSize.DX, _panelSize.DY), new Point<GraphicSystem>(_panelSize.DX, 0));
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(_panelSize.DX, 0), new Point<GraphicSystem>(0, 0));

                foreach (Point<GraphicSystem> point in _astrixModel.Points)
                {
                    yield return new Stroke<GraphicSystem>(_astrixModel.Center, point);
                }

                Point<GraphicSystem> pFirst = _astrixModel.Points.First();
                Point<GraphicSystem> pCurrent = pFirst;

                foreach (Point<GraphicSystem> point in _astrixModel.Points.Skip(1))
                {
                    yield return new Stroke<GraphicSystem>(pCurrent, point);

                    pCurrent = point;
                }

                yield return new Stroke<GraphicSystem>(pFirst, pCurrent);
            }
        }
    }
}
