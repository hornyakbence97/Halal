using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL2.Interfaces
{
    interface IHillClimbProblem
    {
        int Fitness(ISolution solution);
        ISolution InitializeStart();
        ISolution GetSolutionAtDistance(ISolution solution, int distance);
        bool CanStop();
    }
}
