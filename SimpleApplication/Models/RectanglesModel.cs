using CoordinateSystem;
using CoordinateSystem.Privitives;
using System.Collections.Generic;
using CommonTools;

namespace SimpleApplication.Models;

internal class RectanglesModel : IShapesModel
{
    private readonly List<Rect<GraphicSystem>> _rects;
    private readonly Transform<GraphicSystem, GraphicSystem> _transform;
    private TimeDivider _timeDivider;
    private int _currentStepNumer;
    private const int _maxStepCount = 2; //100;

    public RectanglesModel()
    {
        _transform = new Transform<GraphicSystem, GraphicSystem>();
        _transform.AddRotate(Math.PI * 2.0 / _maxStepCount);

        _rects = [];

        _timeDivider = new TimeDivider(3);
    }

    public void TickProcess()
    {
        if (!_timeDivider.TickProcess())
        {
            return;
        }

        if (_currentStepNumer == 2)
        {
            return;
        }

        _currentStepNumer++;

        if (_currentStepNumer == _maxStepCount + 1)
        {
            _rects.Clear();
            _currentStepNumer = 0;

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