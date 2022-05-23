namespace TestLibrary
{
    internal class TriangleAreaClass
    {
        public double TriangleArea(double A, double B, double C, double p, double S)
        {

            p = (A + B + C) / 2;

            S = Math.Sqrt(p * (p - A) * (p - B) * (p - C));

            return S;
        }
    }
}
