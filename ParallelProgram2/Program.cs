using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelProgram2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double minBound = -5.12;
            double maxBound = 5.12;
            double crossoverProb = 0.95;
            double mutationProb = 0.2;
            double eliteCoef = 0.15;
            int PopSize = 1000;
            List<SolutionCheckItem> solutionCheckItems = new List<SolutionCheckItem>();
            List<int> threadCount = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> interationsPow = new List<int>() { 1, 2, 3};
            for (int j = 0; j < interationsPow.Count; j++)
            {
                for (int i = 0; i < threadCount.Count; i++)
                {
                    GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(minBound, maxBound, threadCount[i], PopSize, (int)Math.Pow(10,interationsPow[j]), crossoverProb, mutationProb, eliteCoef);
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    Individual solution = geneticAlgorithm.Run();
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