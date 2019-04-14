using Halal.BL2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.Interfaces
{
    interface ISolution
    {
        List<ISolutionPair> GetSolutionPairs();
        void SetSolutionPairs(List<ISolutionPair> solutionPairs);
        void ConsoleWrite();
    }
}
