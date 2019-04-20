using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tech.CodeGeneration;

namespace Task6
{
    class Program
    {
        static double a = 1, b = 3, eps = 0.001d;
        static int n = 10000, time = 20;
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №6 Филицин Андрей");
            string code = "";
            Match m;
            Regex regex = new Regex(@"[^\d\+=*/ x-]");

            Console.Write("f(x) = ");
            code = Console.ReadLine();

            Console.WriteLine("\nРабота с задачами:\n\t1.Ожидание всех задач\n\t2.Ожидание всех задач в течении заданного времени\n\t3.Ожидание хотя бы одного вывода в файл");
            int choose;
            do
            {
                Console.Write("\t: ");
            } while (!int.TryParse(Console.ReadLine(), out choose) || choose < 1 || choose > 3);

            Func<double, double> Function;

            try
            {
                var expression = CodeGenerator.CreateCode<double>(@"return " + code + ";", new CodeParameter("x", typeof(double)));
                Function = (double arg) => expression.Execute(arg);
            }
            catch
            {
                Console.WriteLine("Error");
                return;
            }


            var tasks = new Task<TaskResult>[4];
            var tasksContinuations = new Task[4];
            tasks[0] = new Task<TaskResult>(() => Functions.SolveEquation("Метод хорд", Functions.Bisection, Function, a, b, eps));
            tasksContinuations[0] = tasks[0].ContinueWith(Functions.WriteResultsToFile);
            tasks[1] = new Task<TaskResult>(() => Functions.SolveEquation("Метод Ньютона", Functions.Newton, Function, a, b, eps));
            tasksContinuations[1] = tasks[1].ContinueWith(Functions.WriteResultsToFile);
            tasks[2] = new Task<TaskResult>(() => Functions.CalculateIntegral("Метод правых прямоугольников", Functions.RightRect, Function, a, b, n));
            tasksContinuations[2] = tasks[2].ContinueWith(Functions.WriteResultsToFile);
            tasks[3] = new Task<TaskResult>(() => Functions.CalculateIntegral("Метод трапеций", Functions.Trapeze, Function, a, b, n));
            tasksContinuations[3] = tasks[3].ContinueWith(Functions.WriteResultsToFile);

            foreach (var t in tasks)
            {
                t.Start();
            }

            switch (choose)
            {
                case 1:
                    Task.WaitAll(tasks);
                    break;
                case 2:
                    Task.WaitAll(tasks, time);
                    break;
                case 3:
                    Task.WaitAny(tasksContinuations);
                    break;
            }
            Console.WriteLine("Условие выполнено");
            Task.WaitAll(tasksContinuations);
        }
    }
}
