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
            ISolutionPosSpeed globalOptimum = population.FirstOrDefault();
            problem.Evaluation(population, globalOptimum);
            while (!problem.CanStop())
            {
                problem.CalcualteVelocy(population, globalOptimum);
                foreach (var item in population)
                {
                    item.Position.AddSpeed(item.Speed);
                }
                problem.Evaluation(population, globalOptimum);

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
