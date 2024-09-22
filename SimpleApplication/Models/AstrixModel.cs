using CoordinateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace SimpleApplication.Models
{
    internal class AstrixModel
    {
        private readonly List<Point<GraphicSystem>> _points = new List<Point<GraphicSystem>>();

        public AstrixModel(int length, int count)
        {
            _points.Add(new Point<GraphicSystem>(0, length));

            Transform<GraphicSystem, GraphicSystem> tr = new Transform<GraphicSystem, GraphicSystem>();
            tr.AddRotate(PI * 2 / count);

            for (int i = 0; i < count - 1; i++)
            {
                _points.Add(tr * _points[^1]);
            }
        }

        public void AddTick()
        {
            Transform<GraphicSystem, GraphicSystem> tr = new Transform<GraphicSystem, GraphicSystem>();
            tr.AddRotate(PI / 36);

            var newPoints = _points.Select(p => tr * p).ToArray();

            _points.Clear();
            _points.AddRange(newPoints);
        }

        public IEnumerable<Point<GraphicSystem>> Points => _points;
    }
}
