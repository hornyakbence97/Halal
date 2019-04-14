using Halal.BL2;
using Halal.GeneticAlgorithm.Interfaces;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.GeneticAlgorithm
{
    class GeneticAlgorithmProblem : IGeneticAlgorithmProblem
    {
        private Random random;
        private WorkToDo[] workToDos;
        private WorkerMan[] workerMens;
        private int maxIterations { get; set; }

        public GeneticAlgorithmProblem(List<WorkToDo> workToDos, List<WorkerMan> workerMens, int maxIterations = 5000000)
        {
            this.workToDos = workToDos.ToArray();
            this.workerMens = workerMens.ToArray();
            this.maxIterations = maxIterations;
            this.random = new Random();
        }


        public bool CanStop()
        {
            if (this.maxIterations % 5000== 0)
            {
                Console.WriteLine(this.maxIterations);
            }
            this.maxIterations--;
            if (this.maxIterations <= 0)
            {
                return true;
            }
            return false;
        }

        public ISolution CrossOver(IEnumerable<ISolution> parents)
        {
            
            List<ISolutionPair> solutionPairs = new List<ISolutionPair>();
            int numberOfJobs = parents.FirstOrDefault().GetSolutionPairs().Count;
            int[] numberOfOccurance = new int[this.workerMens.Length];

           
            for (int i = 0; i < numberOfJobs; i++)
            {
                WorkToDo workToDo = this.workToDos[i];

                foreach (var item in parents)
                {
                    var current = item.GetSolutionPairs().Where(x => x.WorkToDo == workToDo).FirstOrDefault();
                    var man = current.WorkerMan;

                    var index = 0;
                    for (int k = 0; k < this.workerMens.Length; k++)
                    {
                        if (this.workerMens[k] == man)
                        {
                            index = k;
                            k = this.workerMens.Length + 2;
                        }
                    }

                    numberOfOccurance[index]++;
                }

                int maximum =  numberOfOccurance.Where(x => x == numberOfOccurance.Max()).FirstOrDefault();
                int maxIndex = 0;
                for (int o = 0; o < numberOfOccurance.Length; o++)
                {
                    if (numberOfOccurance[o] == maximum)
                    {
                        maxIndex = o;
                        o = numberOfOccurance.Length + 2;   
                    }
                }
              

                WorkerMan manCurrent = this.workerMens[maxIndex];

                solutionPairs.Add(new SolutionPair() { WorkerMan = manCurrent, WorkToDo = workToDo });
            }

            return new Solution() {SolutionPairs = solutionPairs };
        }

        public IEnumerable<IEvaulationElement> Evaulate(IEnumerable<ISolution> population)
        {
            var s = population.OrderBy(x => this.Fitness(x));
            int i = 0;
            foreach (var item in s)
            {
                var current = item;
                i++;
                yield return new EvaluationElement(i, current);
            }
        }

        public int Fitness(ISolution solution)
        {
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


        public IEnumerable<ISolution> InitializeStart(int numberOfInitParents)
        {
            // random populációt generálunk
            for (int i = 0; i < numberOfInitParents; i++)
            {
                yield return GenerateneRandomSolution();
            }
        }

        public ISolution Mutate(ISolution solution)
        {

            ISolutionPair[] solutionPairs = new ISolutionPair[solution.GetSolutionPairs().Count];
            int i = 0;
            foreach (var item in solution.GetSolutionPairs())
            {
                var temp = item;
                solutionPairs[i] = temp;
                i++;

            }

                int rnd = this.random.Next(solutionPairs.Length);
                var wm = solutionPairs[rnd].WorkerMan;
                do
                {
                    solutionPairs[rnd].WorkerMan = this.workerMens[random.Next(this.workerMens.Length)]; //megváltoztatom az egyik munkást

                } while (solutionPairs[rnd].WorkerMan == wm);


            

            return new Solution() { SolutionPairs = solutionPairs.ToList() };
        }

        public IEnumerable<ISolution> Selection(List<ISolution> matingPool, int numberOfParents)
        {
           if (matingPool.Count > numberOfParents)
            {
                var ordered = matingPool.OrderBy(x => this.Fitness(x));
                var all = ordered.Take(numberOfParents);

                foreach (var item in all)
                {
                    var temp = item;
                    matingPool.Remove(temp);
                }

                return all;
            }
           else
            {
                return new List<ISolution>();
            }
        }

        public ParentsAndMatingPool SelectParents(IEnumerable<ISolution> oldPaents, int numberOfSelectParents)
        {
            int mateNum = numberOfSelectParents;
            ParentsAndMatingPool parentsAndMatingPool = new ParentsAndMatingPool();

            var ord = oldPaents.OrderBy(x => this.Fitness(x));
            List<ISolution> mate = new List<ISolution>();
            //kiválasztjuk a legjob szülőket
            foreach (var item in ord)
            {
                var temp = item;
                if (numberOfSelectParents > 0)
                {
                    parentsAndMatingPool.Parents.Add(temp);
                    numberOfSelectParents--;
                }
                else
                {
                    mate.Add(temp);
                }
            }

            var ordMate = mate.OrderBy(x => this.Fitness(x));
            foreach (var item in ordMate)
            {
                if (mateNum > 0)
                {
                    var temp = item;
                    parentsAndMatingPool.MatingPool.Add(temp);
                    mateNum--;
                }
                else
                {
                    break;
                }
            }

            return parentsAndMatingPool;
            
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
    }
}
