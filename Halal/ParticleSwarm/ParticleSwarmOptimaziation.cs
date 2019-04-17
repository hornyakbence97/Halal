using Halal.Interfaces;
using Halal.ParticleSwarm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm
{
    class ParticleSwarmOptimaziation : ISolver
    {
        IParticleSwarmProblem problem;

        public ParticleSwarmOptimaziation(IParticleSwarmProblem problem)
        {
            this.problem = problem;
        }

        public ISolution Start()
        {
            List<ISolutionPosSpeed> population = problem.InitializeStart();
            ISolutionPosSpeed globalOptimum = population.FirstOrDefault().LocalOptimum;
            population = problem.Evaluation(population, ref globalOptimum);
            while (!problem.CanStop())
            {
                population = problem.CalcualteVelocy(population, globalOptimum);
                foreach (var item in population)
                {
                   item.Position = item.Position.AddVelocy(item.Speed);
                }
                population = problem.Evaluation(population, ref globalOptimum);

            }

            Console.WriteLine();

            var best = globalOptimum.GetSolution(this.problem);
            Console.WriteLine(best.ToString());
            best.ConsoleWrite();
            Console.WriteLine("Fitnesz: " + this.problem.Fitness(globalOptimum));

            return best;
        }
    }
}
