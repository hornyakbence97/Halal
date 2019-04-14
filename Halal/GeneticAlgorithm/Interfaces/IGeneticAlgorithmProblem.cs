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
        IEnumerable<ISolution> InitializeStart(int numberOfInitParents);
        
        bool CanStop();
        IEnumerable<IEvaulationElement> Evaulate(IEnumerable<ISolution> population);
        ParentsAndMatingPool SelectParents(IEnumerable<ISolution> oldPaents, int numberOfSelectParents);
        IEnumerable<ISolution> Selection(List<ISolution> matingPool, int numberOfParents);
        ISolution CrossOver(IEnumerable<ISolution> parents);
        ISolution Mutate(ISolution solution);
    }
}
