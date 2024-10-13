using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoordinateSystem;
using CoordinateSystem.Privitives;

namespace SimpleApplication.Models
{
    internal class CircleModel : IShapesModel
    {
        private Point<DisplaySystem> _circlePosition = new(50, 50);
        private Shift<DisplaySystem> _delta = new(2, 2);

        private int _drawingPanelWidth = 0, _drawingPanelHeight = 0;

        private readonly IEnumerable<IShape<GraphicSystem>> _shapes = [];

        public void TickProcess()
        {
            if (_drawingPanelHeight == 0 || _drawingPanelWidth == 0)
            {
                return;
            }

            // Изменяем координаты круга
            _circlePosition += _delta;

            // Проверяем границы панели
            if (_circlePosition.X < 0 || _circlePosition.X + 50 > _drawingPanelWidth)
            {
                Transform<DisplaySystem, DisplaySystem> transform = new Transform<DisplaySystem, DisplaySystem>();
                transform.AddFlipX();

                _delta = transform * _delta;
            }

            if (_circlePosition.Y < 0 || _circlePosition.Y + 50 > _drawingPanelHeight)
            {
                Transform<DisplaySystem, DisplaySystem> transform = new Transform<DisplaySystem, DisplaySystem>();
                transform.AddFlipY();

                _delta = transform * _delta;
            }
        }

        public void SetDrawPanelSize(int width, int height)
        {
            _drawingPanelWidth = width;
            _drawingPanelHeight = height;
        }

        public Point<DisplaySystem> CirclePosition => _circlePosition;

        public IEnumerable<IShape<GraphicSystem>> Shapes => _shapes;
    }
}
