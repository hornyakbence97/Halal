using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.GeneticAlgorithm.Interfaces
{
    interface IParentsAndMAtingPool
    {
         List<ISolution> Parents { get; set; }
         List<ISolution> MatingPool { get; set; }
      
    }
}
