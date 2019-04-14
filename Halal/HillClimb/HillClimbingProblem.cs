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
    class HillClimbingProblem : IHillClimbProblem
    {
        private Random random;
        private WorkToDo[] workToDos;
        private WorkerMan[] workerMens;
        private int maxIterations { get; set; }
      //  private int epszilon { get; set; }

        public HillClimbingProblem(List<WorkToDo> workToDos, List<WorkerMan> workerMens, int maxIterations = 5000000)
        {
            this.workToDos = workToDos.ToArray();
            this.workerMens = workerMens.ToArray();
            this.maxIterations = maxIterations;
           // this.epszilon = epszilon;
            this.random = new Random();

        }

        public ISolution GetSolutionAtDistance(ISolution problem, int distance)
        {
            return this.GetSolutionAtDistance(distance, problem.GetSolutionPairs().ToArray());
        }

        public ISolution InitializeStart()
        {
            ISolutionPair[] solutionPairs = new SolutionPair[this.workToDos.Length];
            for (int i = 0; i < this.workToDos.Length; i++)
            {
                solutionPairs[i] = new SolutionPair() { WorkToDo = workToDos[i], WorkerMan = this.workerMens[random.Next(this.workerMens.Length)] };
            }

            return new Solution() { SolutionPairs = solutionPairs.ToList() };
        }

        public bool CanStop()
        {
            this.maxIterations--;
            if (this.maxIterations <= 0)
            {
                return true;
            }
            return false;
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



        private Solution GetSolutionAtDistance(int distance, ISolutionPair[] solutionPairsIn)
        {
            ISolutionPair[] solutionPairs = new SolutionPair[solutionPairsIn.Length];
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
    }

}
