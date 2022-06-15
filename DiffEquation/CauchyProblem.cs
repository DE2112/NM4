using System;
using static DiffEquation.Operations;

namespace DiffEquation
{
    public class CauchyProblem
    {
        public Func<double, double> F { get; }
        public Func<double, double> P { get; }
        public Func<double, double> Solution { get; }
        public double[] C { get; } 
        public double[] Bounds { get; }

        public CauchyProblem(Func<double, double> f, Func<double, double> p, Func<double, double> solution, double[] c,
            double[] bounds)
        {
            F = f;
            P = p;
            Solution = solution;
            C = c;
            Bounds = bounds;
        }

        public Matrix MakeSystem(Vector x, out Vector solutions, double h)
        {
            var h2 = Sqr(h);
            var n = x.Size;
            solutions = new Vector(n);
            var elements = new double[n,n];
            elements[0, 0] = 1d;
            solutions[0] = C[0];
            
            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    elements[i,j] = 0d;
                }
                
                elements[i, i - 1] = 1d;
                elements[i, i] = -2d - h2 * P(x[i]);
                elements[i, i + 1] = 1d;
                
                solutions[i] = h2 * F(x[i]);
            }
            
            for (int j = 0; j < n; j++)
            {
                elements[n-1,j] = 0d;
            }
            
            elements[n-1, n-1] = 1d;
            solutions[n-1] = C[1];
            
            return new Matrix(elements);
        }
    }
}