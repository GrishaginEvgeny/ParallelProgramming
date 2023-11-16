using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParallellProgram1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int minBound = -5;
            int maxBound = 5;
            MonteCarlo monteCarlo = new MonteCarlo();
            List<SolutionCheckItem> solutionCheckItems = new List<SolutionCheckItem>();
            List<int> threadCount = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> interationsPow = new List<int>() { 2, 3, 4, 5, 6, 7, 8 };
            for (int j = 0; j < interationsPow.Count; j++)
            {
                for (int i = 0; i < threadCount.Count; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    Solution solution = threadCount[i] == 1
                        ? monteCarlo.SolutionSync(minBound, maxBound, (int)Math.Pow(10, interationsPow[j]))
                        : monteCarlo.SolutionParallel(minBound, maxBound, (int)Math.Pow(10, 6), interationsPow[j])
                        ;
                    stopwatch.Stop();
                    solutionCheckItems.Add(new SolutionCheckItem
                    {
                        milliseconds = stopwatch.ElapsedMilliseconds,
                        threads = threadCount[i],
                    });
                }

                List<SolutionCheckItem> sortedSolutions = solutionCheckItems.OrderBy(x => x.milliseconds).ToList();

                foreach (SolutionCheckItem solutionCheckItem in sortedSolutions)
                {
                    Console.WriteLine("Количество параллельных потоков: {0}; Время выполнения программы (в миллисекундах): {1}; Кол-во итераций: 10^{2}",
                        solutionCheckItem.threads,
                        solutionCheckItem.milliseconds,
                        interationsPow[j]
                    );

                }

                Console.WriteLine();
                solutionCheckItems.Clear();
            }
            Console.ReadKey();
        }
    }
}
