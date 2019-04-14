using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.GeneticAlgorithm.Interfaces
{
    interface IGeneticAlgorithmProblem
    {
        int Fitness(ISolution solution);
        IEnumerable<ISolution> InitializeStart();
        ISolution GetSolutionAtDistance(ISolution solution, int distance);
        bool CanStop();
    }
}
