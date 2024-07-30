namespace CoordinateSystem;

public class Transform<TSrcCoordSystem, TDestCoordSystem>
    where TSrcCoordSystem : CoordinateSystem
    where TDestCoordSystem : CoordinateSystem
{
    public static Point<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> p1, Point<TSrcCoordSystem> p2) => new(0, 0);
    
    public static Shift<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> p1, Shift<TSrcCoordSystem> p2) => new(0, 0);

    public static Point<TSrcCoordSystem> operator% (Transform<TSrcCoordSystem, TDestCoordSystem> p1, Point<TDestCoordSystem> p2) => new(0, 0);
    
    public static Shift<TSrcCoordSystem> operator% (Transform<TSrcCoordSystem, TDestCoordSystem> p1, Shift<TDestCoordSystem> p2) => new(0, 0);
}