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
        static Random rnd = new Random();
        public IPositionPS Position { get; set; }
        public IVelocy Speed { get; set; }
        public ISolutionPosSpeed LocalOptimum { get; set; }


        public SolutionPosSpeed(int numberOfJobs, int numberOfWorkers, SolutionPosSpeed localOptimum)
        {
            Position = new PositionPS(numberOfJobs, numberOfWorkers);
            Speed = new Velocy(numberOfJobs, numberOfWorkers);
            LocalOptimum = localOptimum;

        }
        public SolutionPosSpeed(IPositionPS position, IVelocy velocy)
        {
            this.Position = position;
            this.Speed = velocy;
        }

        public ISolution GetSolution(List<WorkToDo> workToDos, List<WorkerMan> workerMens)
        {
            //Solution solution = new Solution();
            //int workers = workerMens.Count;
            //int work = workToDos.Count;

            //for (int i = 0; i < work; i++)
            //{
            //    int cu = this.Position.GetCurrent() % i;
            //    while (cu >= workers)
            //    {
            //        cu -= workers;
            //    }
            //    int index = cu;
            //    solution.SolutionPairs.Add(new SolutionPair() { WorkerMan = workerMens.ElementAt(index), WorkToDo = workToDos.ElementAt(i) });
            //}

            //return solution;

            Solution solution = new Solution();
            PositionPS pos = (this.Position as PositionPS);
            int[] positions = pos.GetPosition();
            for (int i = 0; i < positions.Length; i++)
            {
                solution.SolutionPairs.Add(new SolutionPair() { WorkerMan = workerMens.ElementAt(positions[i]), WorkToDo = workToDos.ElementAt(i) });
            }

            return solution;
        }

        public ISolution GetSolution(IParticleSwarmProblem problem)
        {

            var wms = problem.GetWorkerMans();
            var wtd = problem.GetWorkToDos();

            Solution solution = new Solution() { SolutionPairs = new List<ISolutionPair>() };
            PositionPS pos = (this.Position as PositionPS);
            int[] positions = pos.GetPosition();
            for (int i = 0; i < positions.Length; i++)
            {
                solution.SolutionPairs.Add(new SolutionPair() { WorkerMan = wms.ElementAt(positions[i]), WorkToDo = wtd.ElementAt(i) });
            }

            return solution;


            //Solution solution = new Solution() { SolutionPairs = new List<ISolutionPair>() };
            //int workers = problem.GetWorkerMans().Count;
            //int work = problem.GetWorkToDos().Count;

            //for (int i = 0; i < work; i++)
            //{
            //    int cu = this.Position.GetCurrent() / (i+1);
            //    cu += this.Position.GetCurrent();
            //    if (cu < 0)
            //    {
            //        cu = -1 * cu;
            //    }
            //    while (cu >= workers)
            //    {
            //        cu -= workers;
            //    }
            //    int index = cu;
            //    WorkerMan wm = problem.GetWorkerMans().ElementAt(index);
            //    WorkToDo wtd = problem.GetWorkToDos().ElementAt(i);
            //    solution.SolutionPairs.Add(new SolutionPair() { WorkerMan = wm, WorkToDo = wtd });
            //}

            //return solution;
        }

    }
}
