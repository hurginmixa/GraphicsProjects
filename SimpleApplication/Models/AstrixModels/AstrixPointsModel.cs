using CoordinateSystem;
using CoordinateSystem.Privitives;
using static System.Math;

namespace SimpleApplication.Models.AstrixModels
{
    internal class AstrixPointsModel
    {
        private readonly Point<GraphicSystem> _center;
        private readonly List<Point<GraphicSystem>> _points = new();

        public AstrixPointsModel(Point<GraphicSystem> center, int length, int count)
        {
            _center = center;

            Transform<GraphicSystem, GraphicSystem> tr = new Transform<GraphicSystem, GraphicSystem>();
            tr.AddShift(Point<GraphicSystem>.Origin - _center);
            tr.AddRotate(PI * 2 / count);
            tr.AddShift(_center - Point<GraphicSystem>.Origin);

            _points.Add(_center + new Shift<GraphicSystem>(0, length));

            for (int i = 0; i < count - 1; i++)
            {
                _points.Add(tr * _points[^1]);
            }
        }

        public void TickProcess()
        {
            Transform<GraphicSystem, GraphicSystem> tr = new Transform<GraphicSystem, GraphicSystem>();
            tr.AddShift(Point<GraphicSystem>.Origin - _center);
            tr.AddRotate(PI / 36);
            tr.AddShift(_center - Point<GraphicSystem>.Origin);

            var newPoints = _points.Select(p => tr * p).ToArray();

            _points.Clear();
            _points.AddRange(newPoints);
        }

        public Point<GraphicSystem> Center => _center;

        public IEnumerable<Point<GraphicSystem>> Points => _points;
    }
}
