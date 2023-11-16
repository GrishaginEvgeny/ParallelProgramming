using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallellProgram1
{
    class MonteCarlo
    {
        public double BielFunction(double x, double y)
        {
            return Math.Pow((1.5 - x + x * y), 2) + Math.Pow((2.25 - x + x * y * y), 2) + Math.Pow((2.625 - x + x * y * y * y), 2);
        }

        public Solution SolutionParallel(double minBorder, double maxBorder, int iterations, int threadsCount)
        {
            Random random = new Random();

            object lockObj = new object();

            double minF = double.MaxValue;
            double minAgrY = double.NaN;
            double minArgX = double.NaN;

            Parallel.For(0, threadsCount, threadIndex =>
            {
                for (int i = 0; i < iterations / threadsCount; i++)
                {
                    double randomX = minBorder + (maxBorder - minBorder) * random.NextDouble();
                    double randomY = minBorder + (maxBorder - minBorder) * random.NextDouble();
                    double randomValue = BielFunction(randomX, randomY);

                    if (randomValue < minF)
                    {
                        lock (lockObj)
                        {
                            minF = randomValue;
                            minArgX = randomX;
                            minAgrY = randomY;
                        }
                    }
                }
            });

           return new Solution { F = minF, x = minArgX, y = minAgrY};
        }

        public Solution SolutionSync(double minBorder, double maxBorder, int iterations)
        {
            Random random = new Random();

            double minF = double.MaxValue;
            double minAgrY = double.NaN;
            double minArgX = double.NaN;

            for (int i = 0; i < iterations; i++)
            {
                double randomX = minBorder + (maxBorder - minBorder) * random.NextDouble();
                double randomY = minBorder + (maxBorder - minBorder) * random.NextDouble();
                double randomValue = BielFunction(randomX, randomY);

                if (randomValue < minF)
                {
                        minF = randomValue;
                        minArgX = randomX;
                        minAgrY = randomY;
                }
            }

            return new Solution { F = minF, x = minArgX, y = minAgrY };
        }
    }
}
