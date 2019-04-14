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
    class ParticleSwarmProblem : IParticleSwarmProblem
    {
        private Random random;
        private WorkToDo[] workToDos;
        private WorkerMan[] workerMens;
        private int maxIterations;
        private int numberOfPopulation;

        public ParticleSwarmProblem(List<WorkToDo> workToDos, List<WorkerMan> workerMens, int maxIterations = 5000000, int numberOfPopulation = 500)
        {
            this.workToDos = workToDos.ToArray();
            this.workerMens = workerMens.ToArray();
            this.maxIterations = maxIterations;
            this.numberOfPopulation = numberOfPopulation;
            this.random = new Random();
        }

        public void CalcualteVelocy(List<ISolutionPosSpeed> population, ISolutionPosSpeed globalOptimum)
        {
            foreach (var item in population)
            {
                var rp = random.NextDouble(); //0...1
                var rg = random.NextDouble();

                double w = 0.2;
                double omegap = 0.3;
                double omegag = 0.4;

                item.Speed = item.Speed
                    .Multiple(w)
                    .Add(item.LocalOptimum.Position.Minus(item.Position).Multiple(omegap).Multiple(rp))
                    .Add(globalOptimum.Position.Minus(item.Position).Multiple(omegag).Multiple(rg));
            }
        }

        public bool CanStop()
        {
            this.maxIterations--;
            if (maxIterations > 0)
            {
                return false;
            }
            return true;
        }

        public void Evaluation(List<ISolutionPosSpeed> population, ISolutionPosSpeed globalOptimum)
        {
            foreach (var item in population)
            {
                var temp = item;
                int fitnesItem = this.Fitness(temp);
                if (fitnesItem < this.Fitness(temp.LocalOptimum))
                {
                    
                    temp.LocalOptimum = temp;
                }
                if (fitnesItem < this.Fitness(globalOptimum))
                {
                    globalOptimum = temp;
                }
            }
        }

        public int Fitness(ISolutionPosSpeed solutionIn)
        {
            
            ISolution solution = solutionIn.GetSolution(this);

            int fitnes = 0;
            foreach (var item in solution.GetSolutionPairs())
            {
                fitnes += GetFitnessForSolutionPair(item, solution.GetSolutionPairs());
            }

            var wms = solution.GetSolutionPairs().GroupBy(x => x.WorkerMan);
            int overWork = 0;
            foreach (var item in wms)
            {
                overWork += GetOverWorkAllTimeForWorker(item.Key, solution.GetSolutionPairs());
            }

            if (overWork > 0)
            {
                fitnes += overWork * 10;
            }

            return fitnes;
        }

        public List<ISolutionPosSpeed> InitializeStart()
        {
            List<ISolutionPosSpeed> t = new List<ISolutionPosSpeed>();
            for (int i = 0; i < this.numberOfPopulation; i++)
            {
                ISolutionPosSpeed te = new SolutionPosSpeed(this.workToDos.Length, this.workerMens.Length);
                t.Add(te);                
            }
            return t;
        }




        private int GetOverWorkAllTimeForWorker(WorkerMan worker, List<ISolutionPair> solutionPairs)
        {
            int all = 0;
            foreach (var item in solutionPairs)
            {
                if (item.WorkerMan == worker)
                {
                    all += worker.WorkingTimeByComplexity(item.WorkToDo.Complexity);
                }
            }

            if (worker.WorkingMinutes < all)
            {
                return all - worker.WorkingMinutes;
            }

            return 0;
        }

        private int GetFitnessForSolutionPair(ISolutionPair pair, List<ISolutionPair> solutionPairs)
        {
            WorkerMan workerMan = pair.WorkerMan;
            WorkToDo workToDo = pair.WorkToDo;
            return (workerMan.WorkingTimeByComplexity(workToDo.Complexity) / workerMan.Quality); // idő/minőség = fitnessz

        }
        private ISolution GenerateneRandomSolution()
        {
            ISolutionPair[] solutionPairs = new SolutionPair[this.workToDos.Length];
            for (int i = 0; i < this.workToDos.Length; i++)
            {
                solutionPairs[i] = new SolutionPair() { WorkToDo = workToDos[i], WorkerMan = this.workerMens[random.Next(this.workerMens.Length)] };
            }

            return new Solution() { SolutionPairs = solutionPairs.ToList() };
        }

        public List<WorkerMan> GetWorkerMans()
        {
            return this.workerMens.ToList();
        }

        public List<WorkToDo> GetWorkToDos()
        {
            var w =  this.workToDos.ToList();
            return w;
        }
    }
}
