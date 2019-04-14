using Halal.GeneticAlgorithm.Interfaces;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.GeneticAlgorithm
{
    class ParentsAndMatingPool : IParentsAndMAtingPool
    {
        public List<ISolution> Parents { get; set; }
        public List<ISolution> MatingPool { get; set; }

        public ParentsAndMatingPool()
        {
            Parents = new List<ISolution>();
            MatingPool = new List<ISolution>();
        }
    }
}
