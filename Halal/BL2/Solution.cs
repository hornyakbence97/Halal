using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL2
{
    class Solution
    {
        public List<SolutionPair> SolutionPairs { get; set; }

        public override string ToString()
        {
            string o = "";
            for (int i = 0; i < SolutionPairs.Count; i++)
            {
                o += SolutionPairs[i].WorkToDo.Name + ": " + SolutionPairs[i].WorkerMan.Name + " \t";
            }
            return o;
        }

    }

    class SolutionPair
    {
        public WorkerMan WorkerMan { get; set; }
        public WorkToDo WorkToDo { get; set; }

    }
}
