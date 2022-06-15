using System;
using System.Collections.Generic;

namespace DiffEquation
{
    public static class Operations
    {
        private static double DELTA = 0.00001d;

        public static double Derivative(this Func<double, double> f, double x)
        {
            return (f(x + DELTA) - f(x)) / DELTA;
        }
        
        public static double FirstPartialDerivative(this Func<Vector, double> f, Vector x, int i)
        {
            var xPlusDelta = x.Clone();
            xPlusDelta[i] += DELTA;
            
            var partial = (f(xPlusDelta) - f(x)) / DELTA;

            return partial;
        }

        public static double SecondPartialDerivative(this Func<Vector, double> f, Vector x, int first, int second)
        {
            var x1 = x.CloneAndAddAt(DELTA, first).CloneAndAddAt(DELTA, second);
            var x2 = x.CloneAndAddAt(DELTA, first).CloneAndAddAt(-DELTA, second);
            var x3 = x.CloneAndAddAt(-DELTA, first).CloneAndAddAt(DELTA, second);
            var x4 = x.CloneAndAddAt(-DELTA, first).CloneAndAddAt(-DELTA, second);

            return (f(x1) - f(x2) - f(x3) + f(x4)) / (4d * DELTA * DELTA);
        }

        public static Vector Gradient(this Func<Vector, double> f, Vector x)
        {
            var grad = new Vector(x.Size);
            for (int i = 0; i < grad.Size; i++)
            {
                grad[i] = f.FirstPartialDerivative(x, i);
            }

            return grad;
        }

        public static Matrix HessianMatrix(this Func<Vector, double> f, Vector x)
        {
            var elements = new double[x.Size, x.Size];

            for (int i = 0; i < x.Size; i++)
            {
                for (int j = 0; j < x.Size; j++)
                {
                    elements[i, j] = SecondPartialDerivative(f, x, i, j);
                }
            }

            return new Matrix(elements);
        }

        public static Vector Solution(this List<Func<Vector, double>> f, Vector x)
        {
            var elements = new double[x.Size];
            
            for (int i = 0; i < x.Size; i++)
            {
                for (int j = 0; j < x.Size; j++)
                {
                    elements[i] = f[i](x);
                }
            }

            return new Vector(elements);
        }

        public static Matrix JacobiMatrix(this List<Func<Vector, double>> f, Vector x)
        {
            if (f.Count == x.Size)
            {
                var elements = new double[x.Size, x.Size];

                for (int i = 0; i < x.Size; i++)
                {
                    for (int j = 0; j < x.Size; j++)
                    {
                        elements[i, j] = FirstPartialDerivative(f[i], x, j);
                    }
                }

                return new Matrix(elements);
            }
            
            return null;
        }
        
        public static double GetRandomNumber(double minimum, double maximum)
        { 
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static double GetFibonacci(int index)
        {
            int f1 = 1, f2 = 1;

            if (index < 3)
            {
                return f1;
            }

            int f3 = 0;
            int i = 2;
            while (i < index)
            {
                f3 = f1 + f2;
                f1 = f2;
                f2 = f3;
                i++;
            }

            return f3;
        }

        public static double Sqr(double x)
        {
            return x * x;
        }
    }
}