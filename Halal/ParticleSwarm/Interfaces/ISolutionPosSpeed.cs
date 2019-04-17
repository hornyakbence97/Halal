using Halal.BL2;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm.Interfaces
{
    interface ISolutionPosSpeed
    {
        IPositionPS Position { get; set; }
        IVelocy Speed { get; set; }
        ISolution GetSolution(List<WorkToDo> workToDos, List<WorkerMan> workerMens);
        ISolution GetSolution(IParticleSwarmProblem problem);
        ISolutionPosSpeed LocalOptimum { get; set; }
    }
}
