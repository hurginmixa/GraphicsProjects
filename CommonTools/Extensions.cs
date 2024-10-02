namespace CommonTools
{
    public static class Extensions
    {
        public static bool IsEquals(this double d1, double d2, double eps = 1e-6) => Math.Abs(d1 - d2) < eps;
    }
}
