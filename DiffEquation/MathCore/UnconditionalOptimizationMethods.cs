using System;
using System.Collections.Generic;
using static System.Math;

namespace DiffEquation
{
    public static class Methods
    {
        public static void Svenn(Func<double, double> f, double x0, double t, out double a, out double b, out int k)
        {

            double left = x0 - t, right = x0 + t;
            double fLeft = f(left), fRight = f(right), fX = f(x0);
            k = 0;
            a = fLeft;
            b = fRight;

            if (fLeft >= fX && fRight >= fX)
            {
                a = fLeft;
                b = fRight;
                k = 1;
                return;
            } 
            else if (fLeft <= fX && fX >= fRight)
            {
                x0 = Operations.GetRandomNumber(x0 - 10d, x0 + 10d);
                Svenn(f, x0, t, out a, out b, out k);
                return;
            }
            else
            {
                double delta;
                if (fLeft >= fX && fX >= fRight)
                {
                    delta = t;
                    a = x0;
                }
                else
                {
                    delta = -t;
                    b = x0;
                }

                var x = x0;
                var xNext = x0 + delta;
                var fNext = f(xNext);
                k = 1;

                while (fNext < fX)
                {
                    if (delta == t) a = x;
                    else b = x;

                    x = xNext;
                    fX = f(x);
                    xNext = x + Pow(2, k) * delta;
                    fNext = f(xNext);
                    k++;
                }

                if (delta == t) b = x;
                else a = x;
            }
        }
        
        public static double HalfDivision(Func<double, double> f, double a, double b, double eps, int M, out int k)
        {
            var L = Abs(b - a);
            var l = 2d * eps;
            var x = (a + b) / 2d;
            
            k = 0;
            while (L > l && k < M)
            {
                L = Abs(b - a);
                var y = a + L / 4d;
                var z = b - L / 4d;
                //
                // Console.WriteLine($"x = {x}, a = {a}, b = {b}");
                if (f(y) < f(x))
                {
                    b = x;
                    x = y;
                }
                else
                {
                    if (f(z) < f(x))
                    {
                        a = x;
                        x = z;
                    }
                    else
                    {
                        a = y;
                        b = z;
                    }
                }

                k++;
            }

            return x;
        }

        public static double GoldenRatio(Func<double, double> f, double a, double b, double eps, double M, out int k)
        {
            var L = Abs(b - a);
            var grConst = (3 - Sqrt(5)) / 2;
            var y = a + grConst * (b - a);
            var z = a + b - y;

            k = 0;
            while (L > eps && k < M)
            {
                L = Abs(b - a);

                if (f(y) <= f(z))
                {
                    b = z;
                    z = y;
                    y = a + b - y;
                }
                else if (f(y) > f(z))
                {
                    a = y;
                    y = z;
                    z = a + b - z;
                }

                k++;
            }
            
            var x = (a + b) / 2d;
            return x;
        }

        public static double Fibonacci(Func<double, double> f, double a, double b, double l, double eps, out int k)
        {
            var F = new List<double>();
            var L = Abs(b - a);

            int n = 0;
            do
            {
                F.Add(Operations.GetFibonacci(n));
                n++;
            } while (F[n - 1] < L / l);
            n--;
            
            k = 0;
            var y = a + (F[n - 2] / F[n]) * (b - a);
            var z = a + (F[n - 1] / F[n]) * (b - a);

            double prevA, prevB, prevY, prevZ;
            double fY = f(y), fZ = f(z);
            do
            {
                k++;
                
                prevA = a;
                prevB = b;
                prevY = y;
                prevZ = z;

                fY = f(y);
                fZ = f(z);
                if (fY <= fZ)
                {
                    b = z;
                    z = y;
                    y = a + (F[n - k - 3] / F[n - k - 1]) * (b - a);
                }
                else
                {
                    a = y;
                    y = z;
                    z = a + (F[n - k - 2] / F[n - k - 1]) * (b - a);
                }
            } while (k < n - 3);

            z = y + eps;

            fY = f(y);
            fZ = f(z);
            if (fY <= fZ) b = z;
            else a = y;

            return (a + b) / 2d;
        }

        public static double UniformSearch(Func<double, double> f, double a, double b, double eps)
        {
            var n = (b - a) / eps;
            var minY = f(a);
            var minX = a;

            for (int i = 1; i <= n; i++)
            {
                var x = a + i * (b - a) / n;
                var y = f(x);
                if (minY > y)
                {
                    minY = y;
                    minX = x;
                }
            }

            return minX;
        }
        
        public static Vector GradientDescent(Func<Vector, double> f, Vector x0, double eps1, double eps2, double M, double t0, out int k)
        {
            Vector grad = f.Gradient(x0);
            var x = x0.Clone();
            var prevX = x.Clone();

            k = 0;
            var isMatching = false;
            while (grad.Norm > eps1 && k < M)
            {
                grad = f.Gradient(x);

                var t = t0;
                do
                {
                    x = prevX - t * grad;
                    t /= 2;
                } while (f(x) - f(prevX) > 0d);

                if ((x - prevX).Norm < eps2 && Abs(f(x) - f(prevX)) < eps2)
                {
                    if (isMatching)
                    {
                        return x;
                    }

                    isMatching = true;
                }
                else
                {
                    isMatching = false;
                }
                
                prevX = x.Clone();
                k++;
            }

            return x;
        }
        
        public static Vector FastGradientDescent(Func<Vector, double> f, Vector x0, double eps1, double eps2, double M, out int k)
        {
            Vector grad = f.Gradient(x0);
            var x = x0.Clone();
            var prevX = x.Clone();

            k = 0;
            var isMatching = false;
            while (grad.Norm > eps1 && k < M)
            {
                grad = f.Gradient(x);

                var tk = Round(UniformSearch(t => f(prevX - t * grad), 0d, 1d, eps1), 4);
                x = prevX - tk * grad;

                if ((x - prevX).Norm < eps2 && Abs(f(x) - f(prevX)) < eps2)
                {
                    if (isMatching)
                    {
                        return x;
                    }

                    isMatching = true;
                }
                else
                {
                    isMatching = false;
                }
                
                prevX = x.Clone();
                k++;
            }

            return x;
        }

        public static Vector FletcherReeves(Func<Vector, double> f, Vector x0, double eps1, double eps2, int M,
            double t0, out int k)
        {
            var grad = f.Gradient(x0);
            var prevGrad = grad.Clone();
            var x = x0.Clone(); 
            var prevX = x.Clone(); 
            Vector d = new Vector(grad.Size);
            
            k = 0; 
            var isMatching = false;
            while (grad.Norm > eps1 && k < M) 
            {
                grad = f.Gradient(x);
                var beta = Pow(grad.Norm, 2) / Pow(prevGrad.Norm, 2);
        
                if (k != 0)
                {
                    d = -grad + beta * d;
                }
                else
                {
                    d = -grad;
                }
        
                var tk = Round(UniformSearch(t => f(prevX + t * d), 0d, 1d, eps1), 4);
                x = prevX + tk * d;
        
                if ((x - prevX).Norm < eps2 && Abs(f(x) - f(prevX)) < eps2)
                {
                    if (isMatching)
                    {
                        return x;
                    }
        
                    isMatching = true;
                }
                else
                {
                    isMatching = false;
                }
        
                prevX = x.Clone();
                prevGrad = grad.Clone();
                k++;
            }
            
            return x;
        }

        public static Vector Newton(Func<Vector, double> f, Vector x0, double eps1, double eps2, double M, double t0,
            out int k)
        {
            var grad = f.Gradient(x0);
            var x = x0.Clone();
            var H = f.HessianMatrix(x0);
            var prevX = x.Clone(); 
            Vector d = new Vector(grad.Size);
            
            k = 0; 
            var isMatching = false;
            while (grad.Norm > eps1 && k < M) 
            {
                grad = f.Gradient(x);
                H = f.HessianMatrix(x);
                var InverseH = H.Inverse();

                if (InverseH.IsPositive())
                {
                    d = -InverseH * grad;
                }
                else
                {
                    d = -grad;
                }
                
                var t = t0;
                do
                {
                    x = prevX + t * d;
                    t /= 2;
                } while (f(x) - f(prevX) > 0d);
                
                if ((x - prevX).Norm < eps2 && Abs(f(x) - f(prevX)) < eps2)
                {
                    if (isMatching)
                    {
                        return x;
                    }
        
                    isMatching = true;
                }
                else
                {
                    isMatching = false;
                }
        
                prevX = x.Clone();
                k++;
            }
            
            return x;
        }
        
        public static Vector NewtonRaphson(Func<Vector, double> f, Vector x0, double eps1, double eps2, double M,
            out int k)
        {
            var grad = f.Gradient(x0);
            var x = x0.Clone();
            var H = f.HessianMatrix(x0);
            var prevX = x.Clone(); 
            Vector d = new Vector(grad.Size);
            
            k = 0; 
            var isMatching = false;
            while (grad.Norm > eps1 && k < M) 
            {
                grad = f.Gradient(x);
                H = f.HessianMatrix(x);
                var InverseH = H.Inverse();
                
                if (InverseH.IsPositive())
                {
                    d = -InverseH * grad;
                }
                else
                {
                    d = -grad;
                }
                
                var tk = Round(UniformSearch(t => f(prevX + t * d), 0d, 1d, eps1), 4);
                x = prevX + tk * d;
                
                if ((x - prevX).Norm < eps2 && Abs(f(x) - f(prevX)) < eps2)
                {
                    if (isMatching)
                    {
                        return x;
                    }
        
                    isMatching = true;
                }
                else
                {
                    isMatching = false;
                }
        
                prevX = x.Clone();
                k++;
            }
            
            return x;
        }
    }
}