using Halal.BL2;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm.Interfaces
{
    interface IParticleSwarmProblem
    {
        int Fitness(ISolutionPosSpeed solution);
        List<ISolutionPosSpeed> InitializeStart();
        bool CanStop();
        List<ISolutionPosSpeed> Evaluation(List<ISolutionPosSpeed> population, ref ISolutionPosSpeed globalOptimum);
        List<ISolutionPosSpeed> CalcualteVelocy(List<ISolutionPosSpeed> population, ISolutionPosSpeed globalOptimum);
        List<WorkerMan> GetWorkerMans();
        List<WorkToDo> GetWorkToDos();
    }
}
