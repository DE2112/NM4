using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Eval = Z.Expressions.Eval;
using Random = System.Random;
using static System.Math;
using static System.Console;
using static DiffEquation.Operations;

namespace DiffEquation
{
    public partial class EquationWindow : Form
    {
        private static CauchyProblem[] _tests = new[]
        {
            new CauchyProblem(
                x => 2*x,
                x => 1d,
                x => 2*(Exp(2)*x-x+Exp(1-x)-Exp(x+1)) / (1-Exp(2)),
                new []{0d, 0d},
                new []{0d, 1d}
            ),
            new CauchyProblem(
                x => 2*Sin(x)-4*Cos(x),
                x => 1,
                x => (2*Exp(PI-x)-2*Exp(x)+(Exp(PI)-1)*Sin(x))/(1-Exp(PI))+2*Cos(x),
                new []{0d,0d},
                new []{0d,PI}
            ),
            new CauchyProblem(
                x => Exp(2*x) * Sin(2*x),
                x => 4d,
                x => -(1 / 20d) * Exp(2*x)*(Sin(2*x)+2*Cos(2*x)-2),
                new []{0d, 0d},
                new []{0d, PI}
            ),
        };
        
        private const int TEST_INDEX = ;
        private static CauchyProblem test = _tests[TEST_INDEX];
        private static double step;
        public static int n = 3;
        private static double a = test.Bounds[0];
        private static double b = test.Bounds[1];

        public EquationWindow()
        {
            InitializeComponent();
            AddSeriesToGraph();
            SetGraph();
        }

        private void AddSeriesToGraph()
        {
            var random = new Random();
            var color = Color.FromArgb(random.Next(255), random.Next(255), 
                random.Next(255));
            for (int i = 0; i < 2; i++)
            {
                var series = new Series($"{i}");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 2;
                series.BorderDashStyle = i % 2 == 0 ? ChartDashStyle.Solid : ChartDashStyle.DashDot;
                series.Color = color;

                graph.Series.Add(series);
            }
        }

        private void SetGraph()
        {
            step = (b - a) / n;
            foreach (var series in graph.Series)
            {
                series.Points.Clear();
            }

            var x = new List<double>();
            var yCorrect = new List<double>();

            for (double xi = a; xi <= b; xi += step)
            {
                x.Add(xi);
                yCorrect.Add(test.Solution(xi));
            }
            
            var maxDif = 0d;
            var xVector = new Vector(x.ToArray());
            var y = test.MakeSystem(xVector, out Vector solutions, step).ThomasAlgorithm(solutions);
            
            
            for (int i = 0; i < x.Count - 1; i++)
            {
                graph.Series[0].Points.AddXY(x[i], yCorrect[i]);
                graph.Series[1].Points.AddXY(x[i], y[i]);

                if (Abs(yCorrect[i] - y[i]) > maxDif && i !=0) maxDif = Abs(yCorrect[i] - y[i]);
            }

            textBox.Text = $"Максимальная невязка: {Round(maxDif, 5)}";
        }

        private void OnInputValueChanged(object sender, EventArgs e)
        {
            if (countInput.Text != "")
            {
                n = (int)countInput.Value;
            }

            SetGraph();
        }
    }
}
