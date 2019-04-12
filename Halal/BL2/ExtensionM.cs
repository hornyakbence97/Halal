using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL2
{
    static class ExtensionM
    {
        public static void ConsoleWrite(this Solution solution)
        {
            foreach (var item in solution.SolutionPairs)
            {
                if (item.WorkerMan.WorkingMinutes < solution.SolutionPairs.Where(x => x.WorkerMan == item.WorkerMan).Select(x => x.WorkerMan.WorkingTimeByComplexity(x.WorkToDo.Complexity)).Sum())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                var z = item.WorkerMan.Name + " can work " + item.WorkerMan.WorkingMinutes + " and works: " + solution.SolutionPairs.Where(x => x.WorkerMan == item.WorkerMan).Select(x => x.WorkerMan.WorkingTimeByComplexity(x.WorkToDo.Complexity)).Sum();
                Console.WriteLine(z);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
