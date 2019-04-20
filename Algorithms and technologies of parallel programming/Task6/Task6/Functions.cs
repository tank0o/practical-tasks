using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public struct TaskResult
    {
        public string FileName;
        public string MethodName;
        public double Result;
        public int StepCount;
    }
    class Functions
    {
        public delegate double CalculateMethod(Func<double, double> function, double a, double b, int n);
        public delegate double SolveMethod(Func<double, double> function, double a, double b, double eps, out int k);

        public static TaskResult CalculateIntegral(string MethodName, CalculateMethod Method, Func<double, double> function, double a, double b, int n)
        {
            double x = Method(function, a, b, n);
            return new TaskResult { FileName = MethodName + ".txt", MethodName = MethodName, Result = x, StepCount = n };
        }

        public static TaskResult SolveEquation(string MethodName, SolveMethod Method, Func<double, double> function, double a, double b, double eps)
        {
            int k;
            double x = Method(function, a, b, eps, out k);
            return new TaskResult { FileName = MethodName + ".txt", MethodName = MethodName, Result = x, StepCount = k };
        }

        public static void WriteResultsToFile(Task<TaskResult> task)
        {
            var res = task.Result;
            string str = $"Метод вычисления интеграла: {res.MethodName}\r\n" +
                         $"Ответ: {res.Result}\r\n" +
                         $"Число разбиений: {res.StepCount}\r\n";
            File.AppendAllText(res.FileName, str);
        }

        public static double Trapeze(Func<double, double> function, double a, double b, int n)
        {
            double h = (b - a) / n;
            double result = 0;
            double x = a;
            for (int i = 0; i < n; i++)
            {
                result += function(x) + function(x + h);
                x += h;
            }
            result *= h / 2;
            Console.WriteLine("Метод трапеций завершен");
            return result;
        }

        public static double RightRect(Func<double, double> function, double a, double b, int n)
        {
            double h = (b - a) / n;
            double result = 0;
            double x = a + h;
            for (int i = 0; i < n; i++)
            {
                result += function(x);
                x += h;
            }
            result *= h;
            Console.WriteLine("Метод правых прямоугольников завершен");
            return result;
        }

        static double FirstDerivativeFunction(Func<double, double> function, double x)
        {
            double h = 1e-6;
            return (function(x + h) - function(x)) / h;
        }

        static double SecondDerivativeFunction(Func<double, double> function, double x)
        {
            double h = 1e-6;
            return (function(x + 2 * h) - 2 * function(x + h) + function(x)) / h / h;
        }

        public static double Bisection(Func<double, double> function, double a, double b, double eps, out int k)
        {
            double c;
            k = 0;
            do
            {
                k++;
                c = (a + b) / 2;
                if (function(c) == 0) return c;
                if (function(a) * function(c) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
            } while (b - a > 2 * eps);
            Console.WriteLine("Метод бисекций завершен");
            return (a + b) / 2;
        }

        public static double Newton(Func<double, double> function, double a, double b, double eps, out int k)
        {
            double x = a, x1 = 0;
            x1 = x - function(x) / FirstDerivativeFunction(function, x);
            k = 0;
            do
            {
                k++;
                x = x1;
                x1 = x - function(x) / FirstDerivativeFunction(function, x);
            } while (Math.Abs(x1 - x) > eps);
            Console.WriteLine(k);
            Console.WriteLine("Метод Ньютона завершен");
            return x1;
        }
    }
}
