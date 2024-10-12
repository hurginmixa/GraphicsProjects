using CoordinateSystem;
using CoordinateSystem.Privitives;
using System.Collections.Generic;

namespace SimpleApplication.Models;

internal class RectanglesModel : IShapesModel
{
    private readonly List<Rect<GraphicSystem>> _rects;
    private readonly Transform<GraphicSystem, GraphicSystem> _transform;
    private int _count;
    private const int _maxStepCount = 2; //100;
    private int _timeDivider = 0;

    public RectanglesModel()
    {
        _transform = new Transform<GraphicSystem, GraphicSystem>();
        _transform.AddRotate(Math.PI * 2.0 / _maxStepCount);

        _rects = [];
    }

    public void TickProcess()
    {
        if (++_timeDivider % 3 != 0)
        {
            return;
        }
        _timeDivider = 0;

        if (_count == 2)
        {
            return;
        }

        _count++;

        if (_count == _maxStepCount + 1)
        {
            _rects.Clear();
            _count = 0;

            return;
        }

        Rect<GraphicSystem> rect =  _rects.Count > 0  
            ? _transform * _rects[^1] 
            : new Rect<GraphicSystem>(new Point<GraphicSystem>(0, 0), new Point<GraphicSystem>(300, 200));

        _rects.Add(rect);
    }

    //public IEnumerable<IShape<GraphicSystem>> Shapes => _rects.SelectMany(r => r.GetStrokes()).Cast<IShape<GraphicSystem>>();


    public IEnumerable<IShape<GraphicSystem>> Shapes => _rects.Cast<IShape<GraphicSystem>>();
}