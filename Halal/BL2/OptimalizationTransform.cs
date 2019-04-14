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
    class OptimalizationTransform
    {
        private Random random;
        private WorkToDo[] workToDos;
        private WorkerMan[] workerMens;
        private int maxIterations { get; set; }
        private int epszilon { get; set; }

        public OptimalizationTransform(List<WorkToDo> workToDos, List<WorkerMan> workerMens, int maxIterations = 5000000, int epszilon = 3)
        {
            this.workToDos = workToDos.ToArray();
            this.workerMens = workerMens.ToArray();
            this.maxIterations = maxIterations;
            this.epszilon = epszilon;
            this.random = new Random();

        }

        private int Fitness(Solution solution)
        {
            int fitnes = 0;
            foreach (var item in solution.SolutionPairs)
            {
                fitnes += GetFitnessForSolutionPair(item, solution.SolutionPairs);
            }

            var wms = solution.SolutionPairs.GroupBy(x => x.WorkerMan);
            int overWork = 0;
            foreach (var item in wms)
            {
                overWork += GetOverWorkAllTimeForWorker(item.Key, solution.SolutionPairs);
            }

            if (overWork > 0)
            {
                fitnes += overWork * 10;
            }

            return fitnes;
        }

        private int GetOverWorkAllTimeForWorker(WorkerMan worker, List<SolutionPair> solutionPairs)
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

        private int GetFitnessForSolutionPair(SolutionPair pair, List<SolutionPair> solutionPairs)
        {
            WorkerMan workerMan = pair.WorkerMan;
            WorkToDo workToDo = pair.WorkToDo;

            //int maxWork = workerMan.WorkingMinutes;
            //var thisMenAllWorkTime = 
            //    solutionPairs
            //    .Where(x => x.WorkerMan == workerMan)
            //    .Select(x => x.WorkerMan.WorkingTimeByComplexity(x.WorkToDo.Complexity)).Sum();
            //if (thisMenAllWorkTime > maxWork)
            //{
            //    return ((workerMan.WorkingTimeByComplexity(workToDo.Complexity) / workerMan.Quality)+1) * 1000; //ha nem fér bele a munka idejébe, akkor durván lehúzzuk
            //}
            //else
            //{
                return (workerMan.WorkingTimeByComplexity(workToDo.Complexity) / workerMan.Quality); // idő/minőség = fitnessz
          //  }
        }


        private Solution GetSolutionAtDistance(int distance, SolutionPair[] solutionPairsIn)
        {
            SolutionPair[] solutionPairs = new SolutionPair[solutionPairsIn.Length];
            for (int i = 0; i < solutionPairsIn.Length; i++)
            {
                solutionPairs[i] = new SolutionPair() { WorkerMan = solutionPairsIn[i].WorkerMan, WorkToDo = solutionPairsIn[i].WorkToDo };
            }

            for (int i = 0; i < distance; i++) //distance-szer megváltoztatom az egyik munkást
            {
                int rnd = this.random.Next(solutionPairs.Length);
                var wm = solutionPairs[rnd].WorkerMan;
                do
                {
                    solutionPairs[rnd].WorkerMan = this.workerMens[random.Next(this.workerMens.Length)]; //megváltoztatom az egyik munkást

                } while (solutionPairs[rnd].WorkerMan == wm);

                
            }

            return new Solution() { SolutionPairs = solutionPairs.ToList() };
        }

        public Solution HillClimbing()
        {
            Solution solution = InitializeStart();
            solution.ConsoleWrite();
            int currentIteration = 0;
            while (currentIteration <= this.maxIterations)
            {
                Solution newSolution = GetSolutionAtDistance(this.epszilon, solution.SolutionPairs.ToArray());
                int fNew = Fitness(newSolution);
                int fCurrent = Fitness(solution);
                if (fNew < fCurrent)
                {
                    solution = newSolution;
                   

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

        private Solution InitializeStart()
        {
            SolutionPair[] solutionPairs = new SolutionPair[this.workToDos.Length];
            for (int i = 0; i < this.workToDos.Length; i++)
            {
                solutionPairs[i] = new SolutionPair() { WorkToDo = workToDos[i], WorkerMan = this.workerMens[random.Next(this.workerMens.Length)] };
            }

            return new Solution() { SolutionPairs = solutionPairs.ToList() };
        }


    }

}
