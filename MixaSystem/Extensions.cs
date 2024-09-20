namespace MixaSystem
{
    public static class Extensions
    {
        public static bool IsEquals1(this double d1, double d2, double eps) => Math.Abs(d1 - d2) < eps;
    }
}
