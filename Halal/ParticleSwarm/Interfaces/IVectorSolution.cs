﻿using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm.Interfaces
{
    interface IVectorSolution
    {
        List<ISolutionPair> GetSolutionPairs();
        void SetSolutionPairs(List<ISolutionPair> solutionPairs);
        void ConsoleWrite();
        IVectorSolution Add(IVectorSolution item);
    }
}
