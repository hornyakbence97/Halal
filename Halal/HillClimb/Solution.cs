using Halal.BL2.Interfaces;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL2
{
    class Solution : ISolution
    {
        public List<ISolutionPair> SolutionPairs { get; set; }

        public List<ISolutionPair> GetSolutionPairs()
        {
            return SolutionPairs;
        }

        public void SetSolutionPairs(List<ISolutionPair> solutionPairs)
        {
            this.SolutionPairs = solutionPairs;
        }

        public override string ToString()
        {
            string o = "";
            for (int i = 0; i < SolutionPairs.Count; i++)
            {
                o += SolutionPairs[i].WorkToDo.Name + ": " + SolutionPairs[i].WorkerMan.Name + " \t";
            }
            return o;
        }

        public void ConsoleWrite()
        {
            foreach (var item in this.GetSolutionPairs())
            {
                if (item.WorkerMan.WorkingMinutes < this.GetSolutionPairs().Where(x => x.WorkerMan == item.WorkerMan).Select(x => x.WorkerMan.WorkingTimeByComplexity(x.WorkToDo.Complexity)).Sum())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                var z = item.WorkerMan.Name + " can work " + item.WorkerMan.WorkingMinutes + " and works: " + this.GetSolutionPairs().Where(x => x.WorkerMan == item.WorkerMan).Select(x => x.WorkerMan.WorkingTimeByComplexity(x.WorkToDo.Complexity)).Sum();
                Console.WriteLine(z);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

    }

}
