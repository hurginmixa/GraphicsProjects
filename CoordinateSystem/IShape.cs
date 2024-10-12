using CoordinateSystem.Privitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateSystem
{
    public interface IShape<TCoordinateSystem> where TCoordinateSystem : CoordinateSystem
    {
        Point<TCoordinateSystem> MinPoint { get; }

        Point<TCoordinateSystem> MaxPoint { get; }

        IEnumerable<Stroke<TCoordinateSystem>> GetStrokes();

        IShape<TTargetSystem> Transform<TTargetSystem>(Transform<TCoordinateSystem, TTargetSystem> transform) where TTargetSystem : CoordinateSystem;
    }
}
