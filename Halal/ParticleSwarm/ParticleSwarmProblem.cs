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

        public List<ISolutionPosSpeed> CalcualteVelocy(List<ISolutionPosSpeed> population, ISolutionPosSpeed globalOptimum)
        {
            List<ISolutionPosSpeed> newPop = new List<ISolutionPosSpeed>();
            foreach (var item in population)
            {
                var rp = random.NextDouble() * 1001; //0...1
                var rg = random.NextDouble() *1001;

                double w = 4;
                double omegap = 5;
                double omegag = 4;

                var temp = item;
                temp.Speed = temp.Speed
                    .Multiple(w)
                    .Add(item.LocalOptimum.Position.Minus(item.Position).Multiple(omegap).Multiple(rp))
                    .Add(globalOptimum.Position.Minus(item.Position).Multiple(omegag).Multiple(rg));

                newPop.Add(new SolutionPosSpeed(
                    new PositionPS((temp.Position as PositionPS).GetPosition(), this.workerMens.Length),
                    new Velocy(this.workerMens.Length, (temp.Speed as Velocy).GetVelocy()))
                {  LocalOptimum = temp.LocalOptimum});
            }
            return newPop;
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

        public List<ISolutionPosSpeed> Evaluation(List<ISolutionPosSpeed> population, ref ISolutionPosSpeed globalOptimum)
        {
            List<ISolutionPosSpeed> newPop = new List<ISolutionPosSpeed>(); 
            foreach (var item in population)
            {
                var temp = item;
                int fitnesItem = this.Fitness(temp);

                //TODO Megkérdezni, hogy erre miért van szükség, felesleg munka csupán!!!
                if (fitnesItem < this.Fitness(temp.LocalOptimum))
                {

                    temp.LocalOptimum = new SolutionPosSpeed(
                        new PositionPS((temp.Position as PositionPS).GetPosition(), this.workerMens.Length),
                        new Velocy(this.workerMens.Length, (temp.Speed as Velocy).GetVelocy())
                        );
                    Console.WriteLine("Fitnessz kisebb, mint a lokális optimumé, Round" + this.maxIterations);
                }

                if (fitnesItem < this.Fitness(globalOptimum))
                {
                    Console.WriteLine("Fitnessz kisebb, mint a GLOBÁLIS optimumé, Round: " + this.maxIterations);
                    globalOptimum = new SolutionPosSpeed(
                        new PositionPS((temp.Position as PositionPS).GetPosition(), this.workerMens.Length),
                        new Velocy(this.workerMens.Length, (temp.Speed as Velocy).GetVelocy())
                        );

                }

                newPop.Add(new SolutionPosSpeed(
    new PositionPS((temp.Position as PositionPS).GetPosition(), this.workerMens.Length),
    new Velocy(this.workerMens.Length, (temp.Speed as Velocy).GetVelocy()))
                { LocalOptimum = temp.LocalOptimum });
            }
            return newPop;
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
                ISolutionPosSpeed te = new SolutionPosSpeed(this.workToDos.Length, this.workerMens.Length, null);
                t.Add(te);             
            }

            foreach (var item in t)
            {
                //var ran = random.Next(t.Count);
                var localOptimum = new SolutionPosSpeed(this.workToDos.Length, this.workerMens.Length, null);
                item.LocalOptimum = localOptimum;
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
