using CoordinateSystem;

namespace SimpleApplication.Models;

internal interface IShapesModel
{
    void TickProcess();

    IEnumerable<IShape<GraphicSystem>> Shapes { get; }
}