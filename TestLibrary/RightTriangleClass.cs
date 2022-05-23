namespace TestLibrary
{
    internal class RightTriangleClass
    {
        public bool RightTriangle(double A, double B, double C)
        {
            if ((A * A + B * B == C * C) || (A * A + C * C == B * B) || (C * C + B * B == A * A))
                return true;
            
            return false;
        }
    }
}
