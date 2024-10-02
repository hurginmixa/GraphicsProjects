using CoordinateSystem;

namespace SimpleApplication.Models;

internal interface IStrokesModel
{
    void TickProcess();

    IEnumerable<Stroke<GraphicSystem>> Strokes { get; }
}