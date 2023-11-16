using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgram2
{
    public class Individual
    {
        public double[] Genes { get; }
        public double Fitness { get; }

        public Individual(Random random, double minBound, double maxBound)
        {
            Genes = new double[3];

            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = minBound + (maxBound - minBound) * random.NextDouble();
            }
            Fitness = CalculateFitness(Genes);
        }
        public Individual(double[] genes)
        {
            Genes = genes;
            Fitness = CalculateFitness(genes);
        }

        private double CalculateFitness(double[] genes)
        {
            double A = 10.0;
            int n = genes.Length;
            double sum = 0;

            for (int i = 0; i < n; i++)
            {
                sum += genes[i] * genes[i] - A * Math.Cos(2 * Math.PI * genes[i]);
            }

            return A * n + sum;
        }

        public override string ToString()
        {
            return $"Genes: {string.Join(", ", Genes)}, Fitness: {Fitness}";
        }

    }
}
