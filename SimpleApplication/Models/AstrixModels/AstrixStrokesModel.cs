using CoordinateSystem;

namespace SimpleApplication.Models.AstrixModels
{
    internal class AstrixStrokesModel : IStrokesModel
    {
        private readonly AstrixPointsModel _pointsModel = new(new Point<GraphicSystem>(100, 150), 280, 3);
        private readonly Shift<GraphicSystem> _panelSize = new(200, 300);

        public void TickProcess()
        {
            _pointsModel.TickProcess();
        }

        public IEnumerable<Stroke<GraphicSystem>> Strokes
        {
            get
            {
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(0, 0), new Point<GraphicSystem>(0, _panelSize.DY));
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(0, _panelSize.DY), new Point<GraphicSystem>(_panelSize.DX, _panelSize.DY));
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(_panelSize.DX, _panelSize.DY), new Point<GraphicSystem>(_panelSize.DX, 0));
                yield return new Stroke<GraphicSystem>(new Point<GraphicSystem>(_panelSize.DX, 0), new Point<GraphicSystem>(0, 0));

                foreach (Point<GraphicSystem> point in _pointsModel.Points)
                {
                    yield return new Stroke<GraphicSystem>(_pointsModel.Center, point);
                }

                Point<GraphicSystem> pFirst = _pointsModel.Points.First();
                Point<GraphicSystem> pCurrent = pFirst;

                foreach (Point<GraphicSystem> point in _pointsModel.Points.Skip(1))
                {
                    yield return new Stroke<GraphicSystem>(pCurrent, point);

                    pCurrent = point;
                }

                yield return new Stroke<GraphicSystem>(pFirst, pCurrent);
            }
        }
    }
}
