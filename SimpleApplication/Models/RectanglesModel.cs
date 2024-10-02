using CoordinateSystem;

namespace SimpleApplication.Models;

internal class RectanglesModel : IStrokesModel
{
    private readonly List<Stroke<GraphicSystem>> _strokes;

    public RectanglesModel()
    {
        _strokes = new List<Stroke<GraphicSystem>>();

        _strokes.Add(new Stroke<GraphicSystem>(new Point<GraphicSystem>(0, 0), new Point<GraphicSystem>(0, 300)));
        _strokes.Add(new Stroke<GraphicSystem>(_strokes[^1].Point2, new Point<GraphicSystem>(300, 300)));
        _strokes.Add(new Stroke<GraphicSystem>(_strokes[^1].Point2, new Point<GraphicSystem>(300, 200)));
    }

    public void TickProcess()
    {
    }

    public IEnumerable<Stroke<GraphicSystem>> Strokes => _strokes;
}