using System;
using System.Collections.Generic;
using static System.Math;

namespace DiffEquation
{
    public static class EquationSystemsSolutionMethods
    {
        public static double Newton(Func<double, double> f, double x0, double eps)
        {
            var x = x0;
            var prevX = 0d;
            do
            {
                prevX = x;
                x -= f(x) / f.Derivative(x);
            } while (Abs(x - prevX) > eps);

            return x;
        }

        public static Vector NewtonSystem(this List<Func<Vector, double>> F, Vector x0, double eps)
        {
            var x = x0.Clone();
            var prevX = x.Clone();
            do
            {
                prevX = x.Clone();
                var jacobiMatrix = F.JacobiMatrix(x);
                var inverseJacobiMatrix = jacobiMatrix.Inverse();
                var solution = F.Solution(x);
                x -= inverseJacobiMatrix * solution;
                Console.WriteLine((x - prevX).Norm);
            } while ((x - prevX).Norm > eps);

            return x;
        }

        public static Vector ThomasAlgorithm(this Matrix A, Vector solutions)
        {
            var n = A.Size;
            var y = new Vector(n);
            var a = y.Clone();
            var b = y.Clone();

            y[0] = A[0, 0];
            a[0] = -A[0, 1] / y[0];
            b[0] = solutions[0] / y[0];
            for (int i = 1; i < n - 2; i++)
            {
                y[i] = A[i, i] + A[i, i - 1] * a[i - 1];
                a[i] = -A[i, i + 1] / y[i];
                b[i] = (solutions[i] - A[i, i - 1] * b[i - 1]) / y[i];
            }

            y[n - 1] = A[n - 1, n - 1] + A[n - 1, n - 2] * a[n - 2];
            b[n - 1] = (solutions[n - 1] - A[n - 1, n - 2] * b[n - 2]) / y[n - 1];

            var x = new Vector(n);
            x[n - 1] = b[n - 1];

            for (int i = n-2; i >= 0; i--)
            {
                x[i] = a[i] * x[i + 1] + b[i];
            }

            return x;
        }
    }
}