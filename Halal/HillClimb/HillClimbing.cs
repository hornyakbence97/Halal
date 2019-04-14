using Halal.BL2.Interfaces;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL2
{
    /*
     *Van X perc munkaidő. Van M db munkás. Van N db feladat, és minden feladatnak van nehézségi szintje. 
     * Nehézségi szint alapján tudni minden munkásról, hogy mennyi ideig fog tartani neki a munka.
     * Illetve minden munkásnak van valami minőség értéke és hogy mennyit dolgozik egy nap.
     * 
     * Feladatok hozzárendelése az emberekhez a leghatékonyabban. 
     */
    class HillClimbing : ISolver
    {
        IHillClimbProblem problem;
        int epszilon;


        public HillClimbing(IHillClimbProblem problem, int epszilon)
        {
            this.problem = problem;
            this.epszilon = epszilon;
        }

        public ISolution Start()
        {
            ISolution solution = problem.InitializeStart();
            solution.ConsoleWrite();
            int currentIteration = 0;
            while (!problem.CanStop())
            {
                ISolution newSolution = problem.GetSolutionAtDistance(solution, this.epszilon);
                int fNew = problem.Fitness(newSolution);
                int fCurrent = problem.Fitness(solution);
                if (fNew < fCurrent)
                {
                    solution = newSolution;

                    Console.WriteLine();
                    Console.WriteLine(currentIteration);
                    Console.WriteLine(solution.ToString());
                    solution.ConsoleWrite();
                    Console.WriteLine("Fitnesz: " + fNew);

                }

                currentIteration++;

            }
            Console.WriteLine("Done");
            return solution;
        }
    }
}
