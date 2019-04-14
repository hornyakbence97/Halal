using Halal.BL2;
using Halal.Interfaces;
using Halal.ParticleSwarm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm
{
    class SolutionPosSpeed : ISolutionPosSpeed
    {
        public IPosition Position { get; set; }
        public ISpeed Speed { get; set; }
        public ISolutionPosSpeed LocalOptimum { get; set; }


        public SolutionPosSpeed(int numberOfJobs, int numberOfWorkers)
        {
            Position = new Position((int)Math.Ceiling(Math.Pow(numberOfWorkers, numberOfJobs)));
            Speed = new Speed(Position.GetCurrent() % 25);
            LocalOptimum = this;

        }

        public ISolution GetSolution(List<WorkToDo> workToDos, List<WorkerMan> workerMens)
        {
            Solution solution = new Solution();
            int workers = workerMens.Count;
            int work = workToDos.Count;

            for (int i = 0; i < work; i++)
            {
                int cu = this.Position.GetCurrent() % i;
                while (cu >= workers)
                {
                    cu -= workers;
                }
                int index = cu;
                solution.SolutionPairs.Add(new SolutionPair() { WorkerMan = workerMens.ElementAt(index), WorkToDo = workToDos.ElementAt(i) });
            }

            return solution;
        }

        public ISolution GetSolution(IParticleSwarmProblem problem)
        {
            Solution solution = new Solution() { SolutionPairs = new List<ISolutionPair>() };
            int workers = problem.GetWorkerMans().Count;
            int work = problem.GetWorkToDos().Count;

            for (int i = 0; i < work; i++)
            {
                int cu = this.Position.GetCurrent() / (i+1);
                cu += this.Position.GetCurrent();
                while (cu >= workers)
                {
                    cu -= workers;
                }
                int index = cu;
                WorkerMan wm = problem.GetWorkerMans().ElementAt(index);
                WorkToDo wtd = problem.GetWorkToDos().ElementAt(i);
                solution.SolutionPairs.Add(new SolutionPair() { WorkerMan = wm, WorkToDo = wtd });
            }

            return solution;
        }

    }
}
